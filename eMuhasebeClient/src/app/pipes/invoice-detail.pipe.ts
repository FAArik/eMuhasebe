import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'invoiceDetail',
  standalone: true
})
export class InvoiceDetailPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    return null;
  }

}
