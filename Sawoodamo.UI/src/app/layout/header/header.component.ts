import { Component, inject } from '@angular/core';
import { NavItemComponent } from './nav-item/nav-item.component';
import { NavContainerComponent } from './nav-container/nav-container.component';
import { ButtonComponent } from '../../commons/button/button.component';
import { RouterModule } from '@angular/router';
import { LogoComponent } from './logo/logo.component';
import { AuthService } from '../../_services/auth.service';
import { ModalService } from '../../_services/modal.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'global-header',
  standalone: true,
  imports: [
    NavItemComponent,
    NavContainerComponent,
    ButtonComponent,
    RouterModule,
    LogoComponent,
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  public isAuthenticated;
  private modalService = inject(ModalService);

  constructor(private authService: AuthService) {
    this.isAuthenticated = authService.isAuthenticated;
  }

  openAuthModal() {
    this.modalService.toggleModal('auth');
  }

  logOut() {
    firstValueFrom(this.authService.logOut()).then((x) =>
      this.isAuthenticated.set(false)
    );
  }
}
