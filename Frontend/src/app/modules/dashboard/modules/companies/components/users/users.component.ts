import { animate, state, style, transition, trigger } from '@angular/animations';
import { KeyValue, NgFor, NgIf, NgTemplateOutlet } from '@angular/common';
import { AfterViewInit, Component, DestroyRef, OnInit, ViewChild } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatOptionModule } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { map, switchMap, take, tap } from 'rxjs';
import { CompanyRoleFieldComponent } from 'src/app/modules/shared/components/fields/company-role-field/company-role-field.component';
import { PARAMS_MAP } from 'src/app/modules/shared/constants/routes-map.const';
import { COMPANY_ROLE_KEY_VALUE_MAP, CompanyRole } from 'src/app/modules/shared/enums/company-role.enum';
import { UserViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/companies/userViewModel';
import { UpdateRoleRequestModel } from 'src/app/modules/shared/models/wep-api/domain/user-companies/updateRoleRequestModel';
import { DialogService } from 'src/app/modules/shared/services/dialog.service';
import { RoleService } from 'src/app/modules/shared/services/role.service';
import { UserService } from 'src/app/modules/shared/services/user.service';
import { CompaniesService } from 'src/app/modules/shared/services/web-api/dashboard/companies.service';
import { UserCompanyService } from 'src/app/modules/shared/services/web-api/domain/user-company.service';
import { UserTableModel } from '../../models/userTableModel';
import { DataService } from '../../services/data.service';
import { CreateReviewDialogComponent } from './components/create-review-dialog/create-review-dialog.component';
import { InviteUserDialogComponent } from './components/invite-user-dialog/invite-user-dialog.component';
import { ReviewsComponent } from './components/reviews/reviews.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('0ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatTableModule,
    MatSortModule,
    NgTemplateOutlet,
    MatIconModule,
    NgIf,
    ReviewsComponent,
    MatPaginatorModule,
    MatSelectModule,
    NgFor,
    MatOptionModule,
    CompanyRoleFieldComponent
  ],
})
export class UsersComponent implements OnInit, AfterViewInit {
  protected displayedColumns: string[] = ['firstName', 'lastName', 'email', 'role', 'action', 'expand'];
  protected pageSizeOptions = [100, 50, 25, 10];
  protected dataSource = new MatTableDataSource<UserTableModel>([]);
  protected roles: KeyValue<CompanyRole, string>[] = [];
  protected expandedElement: UserTableModel | undefined;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private companiesService: CompaniesService,
    private roleService: RoleService,
    private dialog: MatDialog,
    private route: ActivatedRoute,
    private dataService: DataService,
    private userCompanyService: UserCompanyService,
    private userService: UserService,
    private dialogService: DialogService,
    private destroyRef: DestroyRef) {

  }

  ngOnInit(): void {
    this.initSubscriptions();
  }

  private initSubscriptions(): void {
    this.route.params
      .pipe(
        tap(params => this.dataService.companyId$.next(params[PARAMS_MAP.Id])),
        switchMap(_ => this.companiesService.getUsers(this.dataService.companyId!)),
        map(x => this.toUserTableModel(x)),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe(x => this.dataSource.data = x);
  }

  private toUserTableModel(models: UserViewModel[]): UserTableModel[] {
    let currentUserRole = CompanyRole.Administrator;
    if (!this.roleService.isAdministrator()) {
      const currentUser = models.find(x => x.id == this.currentUserId);
      currentUserRole = currentUser!.role;
    }

    return models.map(x => ({
      id: x.id,
      firstName: x.firstName,
      lastName: x.lastName,
      email: x.email,
      role: COMPANY_ROLE_KEY_VALUE_MAP[x.role],
      canChangeRole: this.getCanChangeRole(x, currentUserRole),
      canDelete: this.getCanChangeRole(x, currentUserRole)
    }))
  }

  private getCanChangeRole(model: UserViewModel, currentUserRole: CompanyRole): boolean {
    if (this.currentUserId == model.id) {
      return false;
    }

    if (currentUserRole == CompanyRole.Administrator) {
      return true;
    }

    return false;
  }

  protected openInviteUserDialog() {
    this.dialog.open(InviteUserDialogComponent);
  }

  protected openCreateReviewDialog(userId: number) {
    this.dialog.open(CreateReviewDialogComponent, {
      data: userId
    });
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
    this.userCompanyService.delete(id, this.dataService.companyId!)
      .subscribe(_ => this.deleteFromDataSource(id));
  }

  protected deleteFromDataSource(id: number): void {
    const index = this.dataSource.data.findIndex(x => x.id == id);
    if (index) {
      this.dataSource.data.splice(index, 1);
      this.dataSource._updateChangeSubscription();
    }
  }

  protected applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  protected updateRole(element: UserTableModel): void {
    const model: UpdateRoleRequestModel = {
      userId: element.id,
      companyId: this.dataService.companyId!,
      role: element.role.key
    };
    this.userCompanyService.updateRole(model)
      .pipe(take(1))
      .subscribe();
  }

  protected get currentUserId() {
    return this.userService.userId;
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
}
