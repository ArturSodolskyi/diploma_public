import { DatePipe, DecimalPipe, NgIf } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { finalize } from 'rxjs';
import { UserReviewViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/companies/userReviewViewModel';
import { CompaniesService } from 'src/app/modules/shared/services/web-api/dashboard/companies.service';
import { DataService } from '../../../../services/data.service';

@Component({
  selector: 'app-reviews[userId]',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.scss'],
  standalone: true,
  imports: [NgIf, MatTableModule, MatSortModule, DecimalPipe, DatePipe]
})
export class ReviewsComponent implements OnInit {
  protected isLoading = true;
  protected displayedColumns: string[] = ['jobName', 'startDate', 'endDate', 'coverage'];
  protected dataSource = new MatTableDataSource<UserReviewViewModel>([]);


  @Input() userId!: number;
  constructor(private companiesService: CompaniesService,
    private dataService: DataService) {

  }

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.companiesService.getUserReviews(this.userId, this.dataService.companyId!)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe(x => this.dataSource.data = x);
  }
}
