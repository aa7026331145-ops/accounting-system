import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InvoiceService {
  private apiUrl = '/api/invoices';

  constructor(private http: HttpClient) { }

  getAllInvoices(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getInvoiceById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  createInvoice(invoice: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, invoice);
  }

  updateInvoice(id: number, invoice: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, invoice);
  }

  deleteInvoice(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }

  confirmInvoice(id: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/${id}/confirm`, {});
  }

  getInvoiceTotal(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}/total`);
  }
}