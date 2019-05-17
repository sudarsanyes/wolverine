import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';

import { TestComponent } from './Test/test.component';
import { HomeComponent } from './Home/home.component';
import { SortComponent } from './Sort/sort.component';
import { CreateComponent } from './Create/create.component';

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
    path: 'sort', 
    component: SortComponent
  }, 
  {
    path: 'create/:name', 
    component: CreateComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }