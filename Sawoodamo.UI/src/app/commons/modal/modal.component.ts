import { CommonModule, isPlatformBrowser } from '@angular/common';
import {
  Component,
  ElementRef,
  EventEmitter,
  Inject,
  Input,
  OnDestroy,
  OnInit,
  Output,
  PLATFORM_ID,
} from '@angular/core';
import { ModalService } from '../../_services/modal.service';
import { ModalCloseButtonComponent } from './modal-close-button/modal-close-button.component';

@Component({
  selector: 'modal',
  templateUrl: './modal.component.html',
  standalone: true,
  imports: [CommonModule, ModalCloseButtonComponent],
  styleUrls: ['./modal.component.scss'],
})
export class ModalComponent implements OnInit, OnDestroy {
  @Output() onModalClosed = new EventEmitter<void>();
  @Input() modalID = '';

  constructor(
    public modal: ModalService,
    private el: ElementRef,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  ngOnInit(): void {
    this.el.nativeElement.remove();
    if (isPlatformBrowser(this.platformId)) {
      document.body.appendChild(this.el.nativeElement);
    }
  }

  closeModal() {
    this.onModalClosed.emit();
    this.modal.toggleModal(this.modalID);
  }

  refreshModal() {
    document.body.removeChild(this.el.nativeElement);
  }

  ngOnDestroy(): void {
    if (isPlatformBrowser(this.platformId)) {
      document.body.removeChild(this.el.nativeElement);
    }
  }
}
