import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  public readonly jobId$ = new BehaviorSubject<number | undefined>(undefined);
  public readonly competenceId$ = new BehaviorSubject<number | undefined>(undefined);

  constructor() { }

  public get jobId() {
    return this.jobId$.value;
  }

  public get competenceId() {
    return this.competenceId$.value;
  }
}
