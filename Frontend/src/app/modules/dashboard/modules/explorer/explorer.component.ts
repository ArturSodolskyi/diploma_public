import { Component } from '@angular/core';
import { BaseDrawerComponent } from '../../../shared/components/base-drawer/base-drawer.component';
import { TreeComponent } from './components/tree/tree.component';

@Component({
  selector: 'app-explorer',
  templateUrl: './explorer.component.html',
  styleUrls: ['./explorer.component.scss'],
  standalone: true,
  imports: [BaseDrawerComponent, TreeComponent]
})
export class ExplorerComponent {

}
