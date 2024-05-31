import { Component } from '@angular/core';
import { BaseDrawerComponent } from '../../../shared/components/base-drawer/base-drawer.component';
import { ListComponent } from './components/list/list.component';

@Component({
  selector: 'app-companies',
  templateUrl: './companies.component.html',
  standalone: true,
  imports: [BaseDrawerComponent, ListComponent]
})
export class CompaniesComponent {

}
