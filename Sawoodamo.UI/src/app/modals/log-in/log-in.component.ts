import { Component, inject } from '@angular/core';
import { AuthService } from '../../_services/auth.service';
import { ModalService } from '../../_services/modal.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ModalComponent } from '../../commons/modal/modal.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'log-in',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, ModalComponent, CommonModule],
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.scss'
})
export class LogInComponent {
  authService = inject(AuthService);
  modalService = inject(ModalService);

  constructor(){
    this.modalService.register('auth');
  }


  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.pattern(/^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/)]),
    rememberMe: new FormControl(false),
  });

  onSubmit() {
  }

  onRegisterClick(){}
  forgotPassword(){}
}
