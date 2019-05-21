import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { CreateComponent } from '../Create/create.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmationGuard implements CanDeactivate<CreateComponent> {

  canDeactivate(
    component: CreateComponent,
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    console.log("canDeactivate");
    return component.confirm();
  }

}