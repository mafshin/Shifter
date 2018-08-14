import { Component, OnInit } from '@angular/core';
import { Shift } from '../models/Shift';
import { HttpClient } from "@angular/common/http";
import { WorkingDay } from '../models/WorkingDay';

@Component({
  selector: 'shift-list',
  templateUrl: './shift-list.component.html',
  styleUrls: ['./shift-list.component.css']
})
export class ShiftListComponent implements OnInit {

  //todo: get url from settings service
  url:string = 'http://localhost:52057/api';

  days: WorkingDay[];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<WorkingDay[]>(this.url + "/shifts/generate", {withCredentials: true})
    .subscribe((data) =>{
      this.days = data;
    });
  }

}
