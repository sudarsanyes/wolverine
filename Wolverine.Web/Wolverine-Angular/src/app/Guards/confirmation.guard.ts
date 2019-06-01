import { Injectable } from '@angular/core';
import { CanDeactivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { CreateComponent } from '../Create/create.component';
import { SortComponent } from '../Sort/sort.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmationGuard implements CanDeactivate<CreateComponent> {

  canDeactivate(
    component: CreateComponent,
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    return component.confirm();
  }

}

@Injectable({
  providedIn: 'root'
})
export class SortConfirmationGuard implements CanDeactivate<SortComponent> {

  canDeactivate(
    component: SortComponent,
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    return component.confirm();
  }

}