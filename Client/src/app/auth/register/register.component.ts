import { tap } from 'rxjs/operators';
import { RegisterService } from './register.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { noop } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  constructor(
    private _registerService: RegisterService,
    private _router: Router
  ) {}

  ngOnInit(): void {}

  onSubmit(form: NgForm) {
    const { RegisterUsername, RegisterPassword } = form.value;
    if (form.valid) {
      this.register(RegisterUsername, RegisterPassword);
    }
    console.log(form.value);
    console.log(form.valid);
  }

  register(RegisterUsername: string, RegisterPassword: string) {
    this._registerService
      .register({ username: RegisterUsername, password: RegisterPassword })
      .pipe(
        tap((result) => {
          if (result.success) {
            alert('Account was Registered Successfuly!');
            this._router.navigateByUrl('');
          }
        })
      )
      .subscribe(noop, () => console.log('Error in register'));
  }
}
