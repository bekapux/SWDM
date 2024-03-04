import { Component } from '@angular/core';

@Component({
  selector: 'modal-close-button',
  standalone: true,
  imports: [],
  template: `<button
    type="button"
    class="end-2.5 bg-transparent rounded-lg text-sm w-8 h-8 ms-auto inline-flex justify-center items-center"
    data-modal-hide="authentication-modal"
  >
    <svg
      class="w-3 h-3"
      aria-hidden="true"
      xmlns="http://www.w3.org/2000/svg"
      fill="none"
      viewBox="0 0 14 14"
    >
      <path
        stroke="currentColor"
        stroke-linecap="round"
        stroke-linejoin="round"
        stroke-width="2"
        d="m1 1 6 6m0 0 6 6M7 7l6-6M7 7l-6 6"
      />
    </svg>
    <span class="sr-only">Close modal</span>
  </button> `,
})
export class ModalCloseButtonComponent {}
