import { Component } from '@angular/core';
import { NavItemComponent } from './nav-item/nav-item.component';
import { NavContainerComponent } from './nav-container/nav-container.component';
import { ButtonComponent } from '../../commons/button/button.component';
import { RouterModule } from '@angular/router';
import { LogoComponent } from './logo/logo.component';

@Component({
  selector: 'global-header',
  standalone: true,
  imports: [NavItemComponent, NavContainerComponent, ButtonComponent, RouterModule, LogoComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {

}
