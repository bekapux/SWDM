import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'nav-item',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './nav-item.component.html',
  styleUrl: './nav-item.component.scss',
})
export class NavItemComponent {
  @Input({ required: true }) routerLink: string | undefined;
}
