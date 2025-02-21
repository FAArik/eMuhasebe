import { AfterViewInit, Component } from '@angular/core';
import { SharedModule } from '../../modules/shared.module';
import { HttpService } from '../../services/http.service';
import { PurchaseReportModel } from '../../models/purchase-report.model';
import { DatePipe } from '@angular/common';
import { SignalRService } from '../../services/signal-r.service';
declare const Chart: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  providers: [DatePipe]
})
export class HomeComponent implements AfterViewInit {
  chart: any;
  response: PurchaseReportModel = new PurchaseReportModel();
  ngAfterViewInit(): void {
    this.showChart();
    this.getPurchaseReport();
    this.signalR.connect(() => {
      this.signalR.hub?.on("purchaseReport", (res: { date: string, amount: number }) => {
        if (this.response.dates.find(x => x == res.date)) {
          const index = this.response.dates.indexOf(res.date);
          this.response.amount[index] += res.amount;
        } else {
          this.response.amount.push(res.amount)
          this.response.dates.push(res.date);
        }

        this.response.dates.sort((a,b)=>{
          return a.localeCompare(b);
        })

        this.updateChart();
      });
    })
  }
  constructor(
    private http: HttpService,
    private date: DatePipe,
    private signalR: SignalRService
  ) { }

  getPurchaseReport() {
    this.http.get<PurchaseReportModel>('Reports/PurchaseReport', (res: any) => {
      this.response = res;
      this.updateChart();
    });
  }

  updateChart() {
    this.chart.data.labels = this.response.dates.map((dt: string) => {
      return this.date.transform(dt, 'dd.MM.yyyy');
    });
    this.chart.data.datasets[0].data = this.response.amount;
    this.chart.update();
  }

  showChart() {
    const ctx = document.getElementById('myChart');
    this.chart = new Chart(ctx, {
      type: 'line',
      data: {
        labels: [],
        datasets: [{
          label: '# Günlük satışlar',
          data: [],
          borderWidth: 1
        }]
      },
      options: {
        scales: {
          y: {
            beginAtZero: true
          }
        }
      }
    });
  }
}
