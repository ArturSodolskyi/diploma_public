import { Component } from '@angular/core';
import { BaseDrawerComponent } from '../../../../../shared/components/base-drawer/base-drawer.component';
import { CompetenciesComponent } from '../competencies/competencies.component';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.scss'],
  standalone: true,
  imports: [BaseDrawerComponent, CompetenciesComponent]
})
export class JobComponent {

}
