import { SelectionModel } from '@angular/cdk/collections';
import { NgFor, NgIf, NgTemplateOutlet } from '@angular/common';
import { Component, DestroyRef, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { filter, take } from 'rxjs';
import { DynamicTextInputComponent } from 'src/app/modules/shared/components/inputs/dynamic-text-input/dynamic-text-input.component';
import { PARAMS_MAP } from 'src/app/modules/shared/constants/routes-map.const';
import { CompetenceViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/explorers/competenceViewModel';
import { CreateCompetenceRequestModel } from 'src/app/modules/shared/models/wep-api/domain/competencies/createCompetenceRequestModel';
import { UpdateCompetenceRequestModel } from 'src/app/modules/shared/models/wep-api/domain/competencies/updateCompetenceRequestModel';
import { DialogService } from 'src/app/modules/shared/services/dialog.service';
import { PermissionService } from 'src/app/modules/shared/services/permission.service';
import { ExplorerService } from 'src/app/modules/shared/services/web-api/dashboard/explorer.service';
import { CompetenceService } from 'src/app/modules/shared/services/web-api/domain/competence.service';
import { AutofocusDirective } from '../../../../../shared/directives/autofocus.directive';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-competencies',
  templateUrl: './competencies.component.html',
  styleUrls: ['./competencies.component.scss'],
  standalone: true,
  imports: [NgIf, MatButtonModule, MatIconModule, MatDividerModule, MatListModule, NgFor, RouterLink, NgTemplateOutlet, MatInputModule, AutofocusDirective, DynamicTextInputComponent]
})
export class CompetenciesComponent implements OnInit {
  protected elements: CompetenceViewModel[] = [];

  protected selection = new SelectionModel<CompetenceViewModel>(false);
  protected addSelection = new SelectionModel<CompetenceViewModel>(false);
  protected editSelection = new SelectionModel<CompetenceViewModel>(false);

  private jobId!: number;

  constructor(private explorerService: ExplorerService,
    private competenceService: CompetenceService,
    protected permissionService: PermissionService,
    private route: ActivatedRoute,
    private dialogService: DialogService,
    private dataService: DataService,
    private destroyRef: DestroyRef) {

  }

  ngOnInit(): void {
    this.initSubscriptions();
  }

  private initSubscriptions(): void {
    this.route.params
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(params => {
        this.jobId = params[PARAMS_MAP.Id];
        this.dataService.jobId$.next(this.jobId);
        this.loadData();
      });

    this.dataService.competenceId$
      .pipe(
        filter(x => x != undefined),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe(x => this.selectById(x!));
  }

  private loadData(): void {
    this.explorerService.getCompetencies(this.jobId)
      .pipe(take(1))
      .subscribe(x => {
        this.elements = x
        if (this.dataService.competenceId) {
          this.selectById(this.dataService.competenceId);
        }
      });
  }

  private selectById(id: number): void {
    if (this.elements.length === 0) {
      return;
    }

    const element = this.elements.find(x => x.id == id);
    if (element) {
      this.selection.select(element);
    }
  }

  protected select(value: CompetenceViewModel): void {
    if (!this.selection.isSelected(value)) {
      this.selection.select(value);
      this.dataService.competenceId$.next(value.id);
    }
  }

  protected add(): void {
    const item: CompetenceViewModel = {
      id: 0,
      name: ''
    };
    this.elements.push(item);
    this.selection.select(item);
    this.addSelection.select(item);
  }

  protected edit(): void {
    if (this.selected) {
      this.editSelection.select(this.selected);
    }
  }

  protected openDeleteConfirmationDialog(): void {
    this.dialogService.openConfirmationDialog("Are you sure you want to delete this competence?")
      .afterClosed()
      .subscribe(x => {
        if (x) {
          this.delete();
        }
      });
  }

  private delete(item: CompetenceViewModel | undefined = undefined, onlyLocally = false): void {
    if (!item) {
      item = this.selected;
    }

    if (!item) {
      return;
    }

    const index = this.elements.indexOf(item);
    if (index >= 0) {
      if (!onlyLocally) {
        this.competenceService.delete(item.id)
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe(_ => {
            this.elements.splice(index, 1);
            this.clearSelections();
          });
        return;
      }

      this.elements.splice(index, 1);
      this.clearSelections();
    }
  }

  protected applyChanges(value: string, item: CompetenceViewModel) {
    if (this.addSelection.isSelected(item)) {
      if (!value) {
        this.delete(item, true);
        return;
      }

      const model: CreateCompetenceRequestModel = {
        jobId: this.jobId,
        name: value
      };
      this.competenceService.create(model)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(x => {
          item.id = x;
          item.name = value;
          this.clearSelections();
        });
      return;
    }

    if (value) {
      const model: UpdateCompetenceRequestModel = {
        id: item.id,
        name: value
      };
      this.competenceService.update(model)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(_ => {
          item.name = value;
          this.clearSelections();
        });
      return;
    }

    this.clearSelections();
  }


  //#region general

  private clearSelections(): void {
    this.selection.clear();
    this.addSelection.clear();
    this.editSelection.clear();
  }

  protected get selected(): CompetenceViewModel | undefined {
    return this.selection.selected[0];
  }

  //#endregion
}
