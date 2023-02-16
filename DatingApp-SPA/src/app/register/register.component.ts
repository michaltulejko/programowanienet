import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RegisterUser } from '../_models/registerUser';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  @Input() usersFromHomeComponent: any;
  @Output() cancelRegister = new EventEmitter();

  model: RegisterUser = {
    username: '',
    password: '',
    gender: '',
    knownAs: '',
    dateOfBirth: new Date(),
    city: '',
    country: '',
  };

  constructor(
    private authService: AuthService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  register() {
    this.authService.register(this.model).subscribe({
      next: () => this.cancel(),
      error: error => this.toastr.error(error.error.title),
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
