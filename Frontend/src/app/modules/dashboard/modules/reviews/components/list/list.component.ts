import { SelectionModel } from '@angular/cdk/collections';
import { AsyncPipe, DatePipe, NgFor, NgIf } from '@angular/common';
import { Component, DestroyRef, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { RouterLink } from '@angular/router';
import { BehaviorSubject, filter, take } from 'rxjs';
import { ROUTES_MAP } from 'src/app/modules/shared/constants/routes-map.const';
import { ReviewViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/reviews/reviewViewModel';
import { UserService } from 'src/app/modules/shared/services/user.service';
import { ReviewsService } from 'src/app/modules/shared/services/web-api/dashboard/reviews.service';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
  standalone: true,
  imports: [MatDividerModule, MatListModule, NgFor, MatExpansionModule, NgIf, RouterLink, MatIconModule, AsyncPipe, DatePipe]
})
export class ListComponent implements OnInit {
  protected elements$ = new BehaviorSubject<{ header: string; elements: ReviewViewModel[]; }[]>([]);

  protected selection = new SelectionModel<ReviewViewModel>(false);

  protected ROUTES_MAP = ROUTES_MAP;

  constructor(private reviewsService: ReviewsService,
    private userService: UserService,
    private dataService: DataService,
    private destroyRef: DestroyRef) {

  }

  ngOnInit(): void {
    this.loadData();
    this.initSubscriptions();
  }

  private loadData(): void {
    this.reviewsService.get()
      .pipe(take(1))
      .subscribe(x => {
        this.dataService.reviews$.next(x);
        if (this.dataService.reviewId) {
          this.setSelectedById(this.dataService.reviewId)
        }
        this.updateElementsObservable();
      });
  }

  private initSubscriptions(): void {
    this.dataService.reviewId$
      .pipe(
        filter(x => x != null),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe(x => {
        if (!this.selected || x != this.selected.id) {
          this.setSelectedById(x!);
        }
      });
  }

  protected setSelectedById(id: number): void {
    const element = this.elements.find(x => x.id == id);
    if (element) {
      this.select(element);
    }
  }

  protected select(value: ReviewViewModel): void {
    if (!this.selection.isSelected(value)) {
      this.selection.select(value);
      this.dataService.reviewId$.next(value.id);
    }
  }

  //#region general

  private updateElementsObservable(): void {
    // @ts-ignore
    this.elements.sort((a, b) => b.inProgress - a.inProgress);
    this.elements$.next([
      { header: 'As a reviewee:', elements: this.elements.filter(x => x.revieweeId === this.userService.userId) },
      { header: 'As a reviewer:', elements: this.elements.filter(x => x.reviewerId === this.userService.userId) }
    ]);
  }

  private get selected(): ReviewViewModel | undefined {
    return this.selection.selected[0];
  }

  private get elements() {
    return this.dataService.reviews$.value;
  }

  //#endregion
}
