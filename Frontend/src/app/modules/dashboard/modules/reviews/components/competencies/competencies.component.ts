import { NgFor } from '@angular/common';
import { Component, DestroyRef, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { ActivatedRoute, RouterLink, RouterLinkActive } from '@angular/router';
import { take } from 'rxjs';
import { PARAMS_MAP } from 'src/app/modules/shared/constants/routes-map.const';
import { CompetenceViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/reviews/competenceViewModel';
import { ReviewsService } from 'src/app/modules/shared/services/web-api/dashboard/reviews.service';

@Component({
  selector: 'app-competencies',
  templateUrl: './competencies.component.html',
  styleUrls: ['./competencies.component.scss'],
  standalone: true,
  imports: [MatDividerModule, MatListModule, NgFor, RouterLinkActive, RouterLink]
})
export class CompetenciesComponent implements OnInit {
  protected elements: CompetenceViewModel[] = [];
  private reviewId!: number;

  constructor(private reviewsService: ReviewsService,
    private route: ActivatedRoute,
    private destroyRef: DestroyRef) {

  }

  ngOnInit(): void {
    this.initSubscriptions();
  }

  private initSubscriptions(): void {
    this.route.params
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(params => {
        this.reviewId = params[PARAMS_MAP.Id]; //TODO: ?
        this.loadData();
      });
  }

  private loadData(): void {
    if (!this.reviewId) {
      return;
    }

    this.reviewsService.getCompetencies(this.reviewId)
      .pipe(take(1))
      .subscribe(x => this.elements = x);
  }
}
