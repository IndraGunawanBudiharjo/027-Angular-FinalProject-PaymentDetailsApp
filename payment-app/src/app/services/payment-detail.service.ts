import { Injectable } from '@angular/core';
import { PaymentDetail } from '../models/payment-details.model';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Observable, throwError} from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class PaymentDetailService {

  constructor(private http: HttpClient) { }

  apiUrl = 'http://localhost:5000/api/PaymentDetail';

  listPaymentDetails: PaymentDetail[]; 

  formMaterial : PaymentDetail = {
    paymentDetailId: 0,
    cardOwnerName: "",
    cardNumber: "",
    securityCode: "",
    expirationDate: ""
  } as PaymentDetail;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json' // handle cors
    })
  }

  async getPaymentDetailsAndRefreshTable() {
    return this.http.get(this.apiUrl)
      .toPromise()
      .then(res => this.listPaymentDetails = res as PaymentDetail[])
  }

  createPaymentDetail(): Observable<any> {
    return this.http.post(this.apiUrl, this.formMaterial, this.httpOptions).pipe(catchError(this.handleError));
  }

  updatePaymentDetail(): Observable<any> {
    return this.http.put(`${this.apiUrl}/${this.formMaterial.paymentDetailId}`, this.formMaterial, this.httpOptions).pipe(catchError(this.handleError));
  }

  deletePaymentDetail(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`).pipe(catchError(this.handleError));
  }

  
  handleError (e: HttpErrorResponse){
    let message = '';
    if (e.error instanceof ErrorEvent) {
      message = e.error.message;
    }else{
      message = `Error Code: ${e.status}\n Message: ${e.message}`;
    }
    return throwError(message);
  };

}
