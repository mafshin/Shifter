import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ShiftListComponent } from './shift-list/shift-list.component';
import { RouterModule, Routes } from '@angular/router';
import { ShiftListItemComponent } from './shift-list-item/shift-list-item.component';
import { HttpClientModule } from '../../node_modules/@angular/common/http';
import { RuleListComponent } from './rule-list/rule-list.component';


const appRoutes: Routes = [
  {
    path: 'shift', // canActivate: [IsSecureGuardService],
    children: [        
      { path: 'list', component: ShiftListComponent},
      { path: 'rules', component: RuleListComponent}
    ]
  }];
      
@NgModule({
  declarations: [
    AppComponent,
    ShiftListComponent,
    ShiftListItemComponent,
    RuleListComponent,
  ],
  imports: [
    HttpClientModule,
    BrowserModule,    
    RouterModule.forRoot(
      appRoutes
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
