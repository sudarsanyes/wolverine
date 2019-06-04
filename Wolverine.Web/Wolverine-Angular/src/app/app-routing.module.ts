import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';

import { TestComponent } from './Test/test.component';
import { HomeComponent } from './Home/home.component';
import { SortComponent } from './Sort/sort.component';
import { CreateComponent } from './Create/create.component';
import { ConfirmationGuard, SortConfirmationGuard } from './Guards/confirmation.guard';
import { AnalyzeComponent } from './Analyze/analyze.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'test',
    component: TestComponent
  }, 
  {
    path: 'sort/:id', 
    component: SortComponent, 
    canDeactivate: [SortConfirmationGuard]
  }, 
  {
    path: 'create/:name', 
    component: CreateComponent, 
    canDeactivate: [ConfirmationGuard]
  }, 
  {
    path: 'analyze/:id', 
    component: AnalyzeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }