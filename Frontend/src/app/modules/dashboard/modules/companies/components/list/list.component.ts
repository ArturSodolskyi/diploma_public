import { SelectionModel } from '@angular/cdk/collections';
import { AsyncPipe, NgFor, NgIf, NgTemplateOutlet } from '@angular/common';
import { Component, DestroyRef, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { RouterLink } from '@angular/router';
import { BehaviorSubject, Observable, filter, map, switchMap, tap } from 'rxjs';
import { DynamicTextInputComponent } from 'src/app/modules/shared/components/inputs/dynamic-text-input/dynamic-text-input.component';
import { ROUTES_MAP } from 'src/app/modules/shared/constants/routes-map.const';
import { CompanyRole } from 'src/app/modules/shared/enums/company-role.enum';
import { CompanyViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/companies/companyViewModel';
import { DialogService } from 'src/app/modules/shared/services/dialog.service';
import { UserService } from 'src/app/modules/shared/services/user.service';
import { CompaniesService } from 'src/app/modules/shared/services/web-api/dashboard/companies.service';
import { CompanyService } from 'src/app/modules/shared/services/web-api/domain/company.service';
import { UserService as UserApiService } from 'src/app/modules/shared/services/web-api/domain/user.service';
import { AutofocusDirective } from '../../../../../shared/directives/autofocus.directive';
import { CompanyListModel } from '../../models/companyListModel';
import { DataService } from '../../services/data.service';
import { InvitationsComponent } from './components/invitations/invitations.component';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
  standalone: true,
  imports: [MatButtonModule, MatIconModule, MatDividerModule, MatListModule, NgFor, MatExpansionModule, NgIf, RouterLink, NgTemplateOutlet, InvitationsComponent, MatInputModule, AutofocusDirective, AsyncPipe, DynamicTextInputComponent]
})
export class ListComponent implements OnInit {
  private elements: CompanyListModel[] = [];
  //TODO: create new model for { header: string; elements: CompanyViewModel[] }?
  protected elements$ = new BehaviorSubject<{ header: string; elements: CompanyListModel[]; }[]>([]);

  protected selection = new SelectionModel<CompanyListModel>(false);
  //TODO: set primarySelection when user company value will be stored somewhere
  protected primarySelection = new SelectionModel<CompanyListModel>(false);
  protected addSelection = new SelectionModel<CompanyListModel>(false);
  protected editSelection = new SelectionModel<CompanyListModel>(false);

  protected ROUTES_MAP = ROUTES_MAP;

  constructor(private companiesService: CompaniesService,
    private companyService: CompanyService,
    private userService: UserService,
    private userApiService: UserApiService,
    private dataService: DataService,
    private dialogService: DialogService,
    private destroyRef: DestroyRef) {
    this.updateElementsObservable();
  }

  ngOnInit(): void {
    this.getLoadDataObservable()
      .subscribe();
    this.initSubscriptions();
  }

  private initSubscriptions(): void {
    this.dataService.companyId$
      .pipe(
        filter(x => x != undefined),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe(x => this.selectById(x!));

    this.companiesService.onRespondInvitation$
      .pipe(
        switchMap(_ => this.getLoadDataObservable()),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe();
  }

  private getLoadDataObservable(): Observable<CompanyListModel[]> {
    return this.companiesService.get()
      .pipe(
        map(x => x.map(y => this.toCompanyListModel(y))),
        tap(x => {
          this.elements = x;
          this.setPrimary();
          if (this.dataService.companyId) {
            this.selectById(this.dataService.companyId);
          }

          this.updateElementsObservable();
        }));
  }

  private toCompanyListModel(model: CompanyViewModel): CompanyListModel {
    return {
      id: model.id,
      name: model.name,
      isCreatedByCurrentUser: model.isCreatedByCurrentUser,
      canCRUD: model.isCreatedByCurrentUser || model.role == CompanyRole.Administrator,
      canOpen: model.isCreatedByCurrentUser || model.role == CompanyRole.Administrator
    };
  }


  protected selectById(id: number): void {
    if (this.selected?.id == id) {
      return;
    }

    const element = this.elements.find(x => x.id == id);
    if (element) {
      this.selection.select(element);
    }
  }

  private setPrimary(): void {
    const company = this.elements.find(x => x.id === this.userService.companyId);
    if (company) {
      this.primarySelection.select(company);
    }
  }

  protected select(value: CompanyListModel): void {
    if (!this.selection.isSelected(value)) {
      this.selection.select(value);
    }
  }

  protected selectPrimary(): void {
    if (!this.selected) {
      return;
    }

    if (!this.primarySelection.isSelected(this.selected)) {
      this.userApiService.updateCompany(this.selected.id)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(_ => this.primarySelection.select(this.selected!));
    }
  }

  protected add(): void {
    const item = {
      id: 0,
      name: '',
      isCreatedByCurrentUser: true,
      canCRUD: true,
      canOpen: true
    };
    this.elements.push(item);
    this.selection.select(item);
    this.addSelection.select(item);
    this.updateElementsObservable();
  }

  protected edit(): void {
    if (this.selected) {
      this.editSelection.select(this.selected);
    }
  }

  protected openDeleteConfirmationDialog(): void {
    this.dialogService.openConfirmationDialog("Are you sure you want to delete this company?")
      .afterClosed()
      .subscribe(x => {
        if (x) {
          this.delete();
        }
      });
  }

  //TODO: refactor
  private delete(item: CompanyListModel | undefined = undefined, onlyLocaly = false): void {
    if (!item) {
      item = this.selected;
    }

    if (!item) {
      return;
    }

    const index = this.elements.indexOf(item);
    if (index >= 0) {
      if (onlyLocaly) {
        this.elements.splice(index, 1);
        this.clearSelections();
        this.updateElementsObservable();
        return;
      }

      this.companyService.delete(item.id)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(_ => {
          this.elements.splice(index, 1);
          this.clearSelections();
          this.updateElementsObservable();
        });
    }
  }

  protected applyChanges(value: string, item: CompanyListModel) {
    if (this.addSelection.isSelected(item)) {
      if (!value) {
        this.delete(item, true); // TODO: delete only locally add parameter
        return;
      }

      this.companyService.create({ name: value })
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(x => {
          item.id = x;
          item.name = value;
          this.clearSelections();
          this.updateElementsObservable();
        });
      return;
    }

    if (value) {
      this.companyService.update({ id: item.id, name: value })
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(_ => {
          item.name = value;
          this.clearSelections();
          this.updateElementsObservable();
        });
      return;
    }

    this.clearSelections();
    this.updateElementsObservable();
  }

  //#region general

  private updateElementsObservable(): void {
    this.elements$.next([
      { header: 'Your companies:', elements: this.getCreatedByCurrentUser(true) },
      { header: 'Not your companies:', elements: this.getCreatedByCurrentUser(false) }
    ]);
  }

  private getCreatedByCurrentUser(value: boolean): CompanyListModel[] {
    return this.elements.filter(x => x.isCreatedByCurrentUser === value);
  }

  private clearSelections(): void {
    this.selection.clear();
    this.addSelection.clear();
    this.editSelection.clear();
  }

  protected get selected(): CompanyListModel | undefined {
    return this.selection.selected[0];
  }

  //#endregion
}
