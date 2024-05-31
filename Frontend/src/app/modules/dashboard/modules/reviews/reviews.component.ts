import { Component } from '@angular/core';
import { BaseDrawerComponent } from '../../../shared/components/base-drawer/base-drawer.component';
import { ListComponent } from './components/list/list.component';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.scss'],
  standalone: true,
  imports: [BaseDrawerComponent, ListComponent]
})
export class ReviewsComponent {

}
