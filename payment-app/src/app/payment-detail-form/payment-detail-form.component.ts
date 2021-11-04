import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { PaymentDetailService } from 'src/app/services/payment-detail.service';

import { ToastrService } from 'ngx-toastr';
import { PaymentDetail } from '../models/payment-details.model';




@Component({
  selector: 'app-payment-detail-form',
  templateUrl: './payment-detail-form.component.html',
  styleUrls: ['./payment-detail-form.component.css']
})
export class PaymentDetailFormComponent implements OnInit {

  constructor(public service: PaymentDetailService, private toastr: ToastrService) { }


  ngOnInit(): void {

  }


  onSubmit(form: NgForm) {
    if(this.service.formMaterial.paymentDetailId == 0) {
      this.addPaymentDetail(form)
    }
    else{
      this.editPaymentDetail(form)
    }
  }

  addPaymentDetail(form: NgForm) {
    this.service.createPaymentDetail()
      .subscribe(data => {
        this.clearForm(form)
        this.service.getPaymentDetailsAndRefreshTable()
        this.toastr.success('Entry Successfully')
      });
  }

  editPaymentDetail(form: NgForm) {
    this.service.updatePaymentDetail()
      .subscribe(data => {
        this.clearForm(form)
        this.service.getPaymentDetailsAndRefreshTable()
        this.toastr.info('Updated Successfully')
      });
  }


  clearForm(form: NgForm) {
    form.form.reset();
    this.service.formMaterial = {
      paymentDetailId: 0,
      cardOwnerName: '',
      cardNumber: '',
      securityCode: '',
      expirationDate: ''
    } as PaymentDetail;
  }

}
