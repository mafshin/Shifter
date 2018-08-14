import { Component, OnInit } from '@angular/core';
import { Rule } from '../models/Rule';
import { HttpClient } from '../../../node_modules/@angular/common/http';

@Component({
  selector: 'app-rule-list',
  templateUrl: './rule-list.component.html',
  styleUrls: ['./rule-list.component.css']
})
export class RuleListComponent implements OnInit {

  //todo: get url from settings service
  url:string = 'http://localhost:52057/api';

  rules: Rule[];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<Rule[]>(this.url + "/shifts/rules", {withCredentials: true})
    .subscribe((data) =>{
      this.rules = data;
    });
  }

}
