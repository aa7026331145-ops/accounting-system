import { Component, OnInit } from '@angular/core';
import { InvoiceService } from '../../services/invoice.service';

@Component({
  selector: 'app-invoices',
  templateUrl: './invoices.component.html',
  styleUrls: ['./invoices.component.scss']
})
export class InvoicesComponent implements OnInit {
  invoices: any[] = [];
  loading = false;
  error: string | null = null;

  constructor(private invoiceService: InvoiceService) { }

  ngOnInit(): void {
    this.loadInvoices();
  }

  loadInvoices(): void {
    this.loading = true;
    this.invoiceService.getAllInvoices().subscribe({
      next: (data) => {
        this.invoices = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'خطأ في تحميل الفواتير';
        this.loading = false;
        console.error(err);
      }
    });
  }

  deleteInvoice(id: number): void {
    if (confirm('هل أنت متأكد من حذف هذه الفاتورة؟')) {
      this.invoiceService.deleteInvoice(id).subscribe({
        next: () => {
          this.loadInvoices();
        },
        error: (err) => {
          this.error = 'خطأ في حذف الفاتورة';
          console.error(err);
        }
      });
    }
  }

  confirmInvoice(id: number): void {
    this.invoiceService.confirmInvoice(id).subscribe({
      next: () => {
        this.loadInvoices();
      },
      error: (err) => {
        this.error = 'خطأ في تأكيد الفاتورة';
        console.error(err);
      }
    });
  }
}