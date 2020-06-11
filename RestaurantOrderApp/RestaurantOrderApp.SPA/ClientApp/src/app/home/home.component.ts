/// <reference path="../models/inputoutput.ts" />
import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { InputOutput } from '../models/inputoutput';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})


export class HomeComponent {

  orderOutput = "";
  orderInput = "";
  _http: HttpClient;
  _baseUrl = "";
  ios: InputOutput[];

  constructor(http: HttpClient) {
    this._http = http;
    this._baseUrl = environment.orderAPI;
    this.ios = [];
  }

  public Process() {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json', 'Accept': 'application/json', 'Access-Control-Allow-Headers': 'Content-Type' })
    }
    this._http.post(this._baseUrl, JSON.stringify(this.orderInput) , httpOptions).subscribe(result => {
      this.orderOutput = result.toString();
      this.ios.push(new InputOutput( this.orderInput, this.orderOutput));
    }, error => {
        this.orderOutput = "";
        if(error.status == 400)
          alert(error.error);
        else
          alert(error.message);

        console.log(error);
    });
    
  }
}


