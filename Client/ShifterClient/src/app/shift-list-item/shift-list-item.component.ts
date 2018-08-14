import { Component, OnInit, Input } from '@angular/core';
import { Shift } from '../models/Shift';
import { WorkingDay } from '../models/WorkingDay';

@Component({
  selector: 'shift-list-item',
  templateUrl: './shift-list-item.component.html',
  styleUrls: ['./shift-list-item.component.css']
})
export class ShiftListItemComponent implements OnInit {
  @Input() day :WorkingDay;

  constructor() { }

  ngOnInit() {
  }

}
