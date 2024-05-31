import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';
import { Component, DestroyRef, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatStepperModule } from '@angular/material/stepper';
import { ActivatedRoute } from '@angular/router';
import { PARAMS_MAP } from 'src/app/modules/shared/constants/routes-map.const';
import { BaseDrawerComponent } from '../../../../../shared/components/base-drawer/base-drawer.component';
import { DataService } from '../../services/data.service';
import { CompetenciesComponent } from '../competencies/competencies.component';
import { ResultsComponent } from '../results/results.component';
import { DetailsComponent } from './components/details/details.component';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss'],
  providers: [
    {
      provide: STEPPER_GLOBAL_OPTIONS,
      useValue: { displayDefaultIndicatorType: false },
    },
  ],
  standalone: true,
  imports: [MatStepperModule, MatSidenavModule, DetailsComponent, BaseDrawerComponent, CompetenciesComponent, ResultsComponent, MatIconModule]
})
export class ReviewComponent implements OnInit {
  constructor(private route: ActivatedRoute,
    private dataService: DataService,
    private destroyRef: DestroyRef) {

  }

  ngOnInit(): void {
    this.initSubscriptions();
  }

  private initSubscriptions(): void {
    this.route.params
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(params => {
        const reviewId = params[PARAMS_MAP.Id];
        this.dataService.reviewId$.next(reviewId);
      });
  }
}
