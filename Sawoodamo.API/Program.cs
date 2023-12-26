var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#if DEBUG
app.UseCors("debug");
#endif
app.HandleExceptions();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMinimalControllers();

app.Run();