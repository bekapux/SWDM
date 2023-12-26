import { Component } from '@angular/core';
import { FooterLinksWrapperComponent } from './footer-links-wrapper/footer-links-wrapper.component';
import { FooterLinkComponent } from './footer-link/footer-link.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'global-footer',
  standalone: true,
  imports: [FooterLinksWrapperComponent, FooterLinkComponent, RouterModule],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent {

}
