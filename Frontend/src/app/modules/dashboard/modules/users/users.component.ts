import { Component } from '@angular/core';
import { TableComponent } from './components/table/table.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  standalone: true,
  imports: [TableComponent]
})
export class UsersComponent {

}
