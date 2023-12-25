using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sawoodamo.API.Features.Auth;

public sealed record AuthenticateRequest(string Email, string Password) : IRequest<AuthResponse>;

public sealed class AuthenticateRequestHandler(UserManager<User> userManager, 
    SignInManager<User> signInManager, 
    IConfiguration configuration) : IRequestHandler<AuthenticateRequest, AuthResponse>
{
    public async Task<AuthResponse> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email) ?? 
                   throw new NotFoundException($"User with mail '{request.Email}' was not found.", request.Email);

        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded != true)
        {
            throw new Exception($"Credentials for '{request.Email} aren't valid'.");
        }

        var jwtSecurityToken = GenerateToken(user);

        var response = new AuthResponse(user.Id, request.Email, new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));

        return response;
    }

    private JwtSecurityToken GenerateToken(User user)
    {
        #region Might Add In Future
        //var userClaims = await _userManager.GetClaimsAsync(user);
        //var roles = await _userManager.GetRolesAsync(user);
        //var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();
        #endregion

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("uid", user.Id)
        };

        #region Might Add In Future
        //.Union(userClaims)
        //.Union(roleClaims);
        #endregion

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:Key").Value!));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
           issuer: configuration["JwtSettings:Issuer"],
           audience: configuration["JwtSettings:Audience"],
           claims: claims,
           expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["JwtSettings:DurationInMinutes"])),
           signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}

public sealed record AuthResponse(string UserId, string Email, string Token);
