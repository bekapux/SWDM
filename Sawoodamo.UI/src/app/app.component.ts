import { Component, DestroyRef, Inject, PLATFORM_ID, inject } from '@angular/core';
import {
  CommonModule,
  isPlatformBrowser,
} from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { AuthService } from './_services/auth.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ModalService } from './_services/modal.service';
import { LogInComponent } from './modals/log-in/log-in.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HeaderComponent, FooterComponent, LogInComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  public authService = inject(AuthService);
  public modalService = inject(ModalService);
  private destroyRef= inject(DestroyRef);

  constructor(
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    if (isPlatformBrowser(platformId)) {
      this.authService
        .checkAuthStatus()
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(
          (res) => {
            this.authService.isAuthenticated.set(true);
          },
          (err) => {
            this.authService.isAuthenticated.set(false);
          }
        );
    }

    this.modalService.register('auth');
  }
}
