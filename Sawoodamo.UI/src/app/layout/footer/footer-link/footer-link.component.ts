import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'footer-link',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './footer-link.component.html',
  styleUrl: './footer-link.component.scss'
})
export class FooterLinkComponent {
  @Input() routerLink: string | undefined;
}
