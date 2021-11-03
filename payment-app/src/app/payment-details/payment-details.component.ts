import { Component, OnInit } from '@angular/core';
import { PaymentDetailService } from '../services/payment-detail.service';
import { PaymentDetail } from '../models/payment-details.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-payment-details',
  templateUrl: './payment-details.component.html',
  styleUrls: ['./payment-details.component.css']
})
export class PaymentDetailsComponent implements OnInit {

  listPaymentDetails: PaymentDetail[] = []

  constructor(public service: PaymentDetailService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.getPaymentDetailsAndRefreshTable();
   
  }

  fillFormDataWhenUpdate(selected: PaymentDetail) {
    this.service.formMaterial = Object.assign({}, selected);
  }

  onDelete(id: number){
    if(confirm("Are you sure?")){
      this.service.deletePaymentDetail(id)
      .subscribe(
        res => {
          this.service.getPaymentDetailsAndRefreshTable();
          this.toastr.error("Deleted");
        }
      )
    }
    
  }

}
