import { NgFor, NgTemplateOutlet } from '@angular/common';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { map } from 'rxjs';
import { UserRoleFieldComponent } from 'src/app/modules/shared/components/fields/user-role-field/user-role-field.component';
import { USER_ROLE_KEY_VALUE_MAP } from 'src/app/modules/shared/enums/user-role.enum';
import { UserViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/users/userViewModel';
import { DialogService } from 'src/app/modules/shared/services/dialog.service';
import { UserService } from 'src/app/modules/shared/services/user.service';
import { UsersService } from 'src/app/modules/shared/services/web-api/dashboard/users.service';
import { UserTableModel } from '../../models/userTableModel';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatTableModule, MatSortModule, NgTemplateOutlet, MatButtonModule, MatIconModule, MatPaginatorModule, MatSelectModule, NgFor, MatOptionModule, UserRoleFieldComponent]
})
export class TableComponent implements OnInit, AfterViewInit {
  protected displayedColumns: string[] = ['firstName', 'lastName', 'email', 'role', 'action'];
  protected pageSizeOptions = [100, 50, 25, 10];
  protected dataSource = new MatTableDataSource<UserTableModel>([]);
  protected expandedElement: UserTableModel | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private usersService: UsersService,
    private userService: UserService,
    private dialogService: DialogService) {

  }

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.usersService.get()
      .pipe(map(x => this.toUserTableModel(x)))
      .subscribe(x => this.dataSource.data = x);
  }

  private toUserTableModel(models: UserViewModel[]): UserTableModel[] {
    return models.map(x => ({
      id: x.id,
      firstName: x.firstName,
      lastName: x.lastName,
      email: x.email,
      role: USER_ROLE_KEY_VALUE_MAP[x.role],
      canDelete: this.userService.userId != x.id
    }));
  }

  protected applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  updateRole(model: UserTableModel): void {
    this.usersService.updateUserRole(model.id, model.role.key)
      .subscribe();
  }

  protected openDeleteConfirmationDialog(id: number): void {
    this.dialogService.openConfirmationDialog("Are you sure you want to delete this user?")
      .afterClosed()
      .subscribe(x => {
        if (x) {
          this.delete(id);
        }
      });
  }

  private delete(id: number): void {
    this.usersService.deleteUser(id)
      .subscribe(_ => this.deleteFromDataSource(id));
  }

  private deleteFromDataSource(id: number): void {
    const index = this.dataSource.data.findIndex(x => x.id == id);
    if (index) {
      this.dataSource.data.splice(index, 1);
      this.dataSource._updateChangeSubscription();
    }
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
}
