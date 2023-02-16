import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { NgbDropdownModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgbDropdownModule,
    ToastrModule.forRoot({ positionClass: 'toast-bottom-right' }),
    NgbModule,
  ],
  exports: [NgbDropdownModule, ToastrModule, NgbModule],
})
export class SharedModule {}
