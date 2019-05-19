import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { ClipboardModule } from 'ngx-clipboard';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { ProjectService } from './Services/project.service';
import { TestComponent } from './Test/test.component';
import { HomeComponent } from './Home/home.component';
import { SortComponent } from './Sort/sort.component';
import { CreateComponent } from './Create/create.component';

@NgModule({
  declarations: [
    AppComponent, 
    TestComponent, 
    HomeComponent, 
    SortComponent, 
    CreateComponent
  ],
  imports: [
    NgbModule.forRoot(), 
    HttpClientModule, 
    BrowserModule, 
    FormsModule, 
    AppRoutingModule, 
    ClipboardModule
  ],
  providers: [
    ProjectService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
