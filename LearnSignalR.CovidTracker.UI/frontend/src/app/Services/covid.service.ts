import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Covid } from '../Models/covid.model';
@Injectable({
  providedIn: 'root',
})
export class CovidService {
  covidChartList = new Array();
  private hubConnection!: signalR.HubConnection;

  constructor() {}

  private startInvoke = () => {
    this.hubConnection
    .invoke('GetCovidListAsync')
    .catch((err) => console.error(err));
  };

  startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7049/covidhub')
      .build();

    this.hubConnection
      .start()
      .then(() => {
        this.startInvoke();
        console.log('Connection started')
      })
      .catch((err) => console.log('Error while starting connection: ' + err));
  };

  startListener = () => {
    this.hubConnection.on("ReceiveCovidList", (covidCharts:Covid[]) => {
      this.covidChartList = [];
      covidCharts.forEach((covidChart:Covid) => {
        this.covidChartList.push([covidChart.covidDate, covidChart.counts[0], covidChart.counts[1], covidChart.counts[2], covidChart.counts[3], covidChart.counts[4]]);

      });
    });
  }

}
