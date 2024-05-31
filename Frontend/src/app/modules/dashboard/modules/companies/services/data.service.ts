import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  public readonly companyId$ = new BehaviorSubject<number | undefined>(undefined);
  
  constructor() { }

  public get companyId() {
    return this.companyId$.value;
  }
}
