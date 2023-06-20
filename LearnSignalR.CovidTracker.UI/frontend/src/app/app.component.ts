import { Component, OnInit } from '@angular/core';
import { CovidService } from './Services/covid.service';
import { ChartType } from 'angular-google-charts';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'frontend';
  columnNames = ["Tarih", "Istanbul", "Ankara", "Izmir", "Konya", "Antalya"]
  options:any = {legend: {position: 'bottom'}};
  chartType: ChartType = ChartType.LineChart;

  constructor(public covidService: CovidService) {}

  ngOnInit(): void {
    this.covidService.startConnection();
    this.covidService.startListener();
  }

}
