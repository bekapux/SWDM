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

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HeaderComponent, FooterComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  private destroyRef= inject(DestroyRef);
  constructor(
    private authService: AuthService,
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
  }
}
