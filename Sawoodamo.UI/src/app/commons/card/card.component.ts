import { NgOptimizedImage } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'card',
  standalone: true,
  imports: [RouterModule, NgOptimizedImage],
  templateUrl: './card.component.html',
  styleUrl: './card.component.scss'
})
export class CardComponent {
  @Input() imageUrl: string | undefined;
  @Input() price: string | undefined;
  @Input() shortDescription: string | undefined;
  @Input() fullDescription: string | undefined;
  @Input() name: string | undefined;
  @Input() slug: string | undefined;
}
