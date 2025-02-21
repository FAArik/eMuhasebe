import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { api, signalRApi } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  hub: signalR.HubConnection | undefined;

  constructor() { }

  connect(callback:()=>void) {
    this.hub = new signalR.HubConnectionBuilder()
      .withUrl(`${signalRApi}/report-hub`)
      .build();

    this.hub.start().then(() => {
      console.log('Connection started with /report-hub');
      callback();
    });
  }
}
