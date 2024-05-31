import { AsyncPipe, NgFor } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatOptionModule } from '@angular/material/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Observable, debounceTime, distinctUntilChanged, map, startWith, switchMap } from 'rxjs';
import { GetJobsRequestModel } from 'src/app/modules/shared/models/wep-api/dashboard/companies/getJobsRequestModel';
import { GetReviewersRequestModel } from 'src/app/modules/shared/models/wep-api/dashboard/companies/getReviewersRequestModel';
import { JobViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/companies/jobViewModel';
import { ReviewerViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/companies/reviewerViewModel';
import { CreateReviewRequestModel } from 'src/app/modules/shared/models/wep-api/domain/reviews/createReviewRequestModel';
import { CompaniesService } from 'src/app/modules/shared/services/web-api/dashboard/companies.service';
import { ReviewService } from 'src/app/modules/shared/services/web-api/domain/review.service';
import { DataService } from '../../../../services/data.service';

@Component({
  selector: 'app-create-review-dialog',
  templateUrl: './create-review-dialog.component.html',
  standalone: true,
  imports: [MatDialogModule, FormsModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatAutocompleteModule, NgFor, MatOptionModule, MatButtonModule, AsyncPipe]
})
export class CreateReviewDialogComponent implements OnInit {
  private readonly amount = 10;
  private readonly debounceTime = 300;

  protected form!: FormGroup<{
    jobFilter: FormControl<string>,
    job: FormControl<JobViewModel | null>,
    reviewerFilter: FormControl<string>,
    reviewer: FormControl<ReviewerViewModel | null>
  }>;
  protected jobFilterOptions$!: Observable<JobViewModel[]>;
  protected reviewerFilterOptions$!: Observable<ReviewerViewModel[]>;

  constructor(@Inject(MAT_DIALOG_DATA) private revieweeId: number,
    private fb: FormBuilder,
    private companiesService: CompaniesService,
    private dataService: DataService,
    private reviewService: ReviewService,
    private dialogRef: MatDialogRef<CreateReviewDialogComponent>) {
    this.createForm();
  }

  private createForm(): void {
    this.form = this.fb.group({
      jobFilter: this.fb.nonNullable.control<string>('', Validators.required),
      job: this.fb.control<JobViewModel | null>(null, Validators.required),
      reviewerFilter: this.fb.nonNullable.control<string>('', Validators.required),
      reviewer: this.fb.control<ReviewerViewModel | null>(null, Validators.required),
    });
  }

  ngOnInit(): void {
    this.jobFilterOptions$ = this.jobFilter.valueChanges
      .pipe(
        startWith(''),
        debounceTime(this.debounceTime),
        distinctUntilChanged(),
        switchMap(x => this.getJobsObservable(x))
      );

    this.reviewerFilterOptions$ = this.reviewerFilter.valueChanges
      .pipe(
        startWith(''),
        debounceTime(this.debounceTime),
        distinctUntilChanged(),
        switchMap(x => this.getReviewersObservable(x)),
        map(x => x.filter(x => x.id != this.revieweeId))
      );
  }

  private getJobsObservable(filter: any): Observable<JobViewModel[]> {
    const model: GetJobsRequestModel = {
      companyId: this.dataService.companyId!,
      filter: filter,
      amount: this.amount
    };
    return this.companiesService.getJobs(model);
  }

  private getReviewersObservable(filter: any): Observable<ReviewerViewModel[]> {
    const model: GetReviewersRequestModel = {
      companyId: this.dataService.companyId!,
      filter: filter,
      amount: this.amount
    };
    return this.companiesService.getReviewers(model);
  }

  protected onJobFilterChange(): void {
    if (this.jobFilter.value == '') {
      return;
    }

    if (!this.job.value) {
      this.jobFilter.reset();
      return;
    }

    this.jobFilter.patchValue(this.job.value.name);
  }

  protected onReviewerFilterChange(): void {
    if (this.reviewerFilter.value == '') {
      return;
    }

    if (!this.reviewer.value) {
      this.reviewerFilter.reset();
      return;
    }

    this.reviewerFilter.patchValue(this.reviewer.value.email);
  }

  protected save(): void {
    const model: CreateReviewRequestModel = {
      jobId: this.job.value?.id!,
      reviewerId: this.reviewer.value?.id!,
      revieweeId: this.revieweeId
    }
    this.reviewService.create(model)
      .subscribe(_ => this.dialogRef.close(true));
  }

  private get jobFilter() {
    return this.form.controls.jobFilter;
  }

  protected get job() {
    return this.form.controls.job;
  }

  private get reviewerFilter() {
    return this.form.controls.reviewerFilter;
  }

  protected get reviewer() {
    return this.form.controls.reviewer;
  }
}
