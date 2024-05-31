import { SelectionModel } from '@angular/cdk/collections';
import { NgFor, NgIf, NgTemplateOutlet } from '@angular/common';
import { Component, DestroyRef, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs';
import { DynamicTextInputComponent } from 'src/app/modules/shared/components/inputs/dynamic-text-input/dynamic-text-input.component';
import { PARAMS_MAP } from 'src/app/modules/shared/constants/routes-map.const';
import { TaskViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/explorers/taskViewModel';
import { CreateTaskRequestModel } from 'src/app/modules/shared/models/wep-api/domain/tasks/createTaskRequestModel';
import { UpdateTaskRequestModel } from 'src/app/modules/shared/models/wep-api/domain/tasks/updateTaskRequestModel';
import { DialogService } from 'src/app/modules/shared/services/dialog.service';
import { PermissionService } from 'src/app/modules/shared/services/permission.service';
import { ExplorerService } from 'src/app/modules/shared/services/web-api/dashboard/explorer.service';
import { TaskService } from 'src/app/modules/shared/services/web-api/domain/task.service';
import { AutofocusDirective } from '../../../../../shared/directives/autofocus.directive';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss'],
  standalone: true,
  imports: [MatExpansionModule, NgFor, NgTemplateOutlet, NgIf, MatButtonModule, MatIconModule, MatInputModule, AutofocusDirective, MatFormFieldModule, DynamicTextInputComponent]
})
export class TasksComponent implements OnInit {
  protected elements: TaskViewModel[] = [];

  protected addSelection = new SelectionModel<TaskViewModel>(false);
  protected editSelection = new SelectionModel<TaskViewModel>(false);

  protected competenceId!: number;

  constructor(private explorerService: ExplorerService,
    private taskService: TaskService,
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
        this.competenceId = params[PARAMS_MAP.Id];
        this.dataService.competenceId$.next(this.competenceId);
        this.loadData();
      });
  }

  private loadData(): void {
    this.explorerService.getTasks(this.competenceId)
      .pipe(take(1))
      .subscribe(x => this.elements = x);
  }

  protected add(): void {
    const item: TaskViewModel = {
      id: 0,
      name: '',
      text: ''
    };
    this.elements.push(item);
    this.addSelection.select(item);
  }

  protected edit(value: TaskViewModel): void {
    this.editSelection.select(value);
  }

  protected openDeleteConfirmationDialog(item: TaskViewModel): void {
    this.dialogService.openConfirmationDialog("Are you sure you want to delete this task?")
      .afterClosed()
      .subscribe(x => {
        if (x) {
          this.delete(item);
        }
      });
  }

  private delete(item: TaskViewModel, onlyLocally = false): void {
    if (!item) {
      return;
    }

    const index = this.elements.indexOf(item);
    if (index >= 0) {
      if (!onlyLocally) {
        this.taskService.delete(item.id)
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe(_ => {
            this.elements.splice(index, 1);
            this.clearSelections();
          });
        return;
      };

      this.elements.splice(index, 1);
      this.clearSelections();
    }
  }

  protected applyChanges(value: string, item: TaskViewModel) {
    if (this.addSelection.isSelected(item)) {
      if (!value) {
        this.delete(item, true);
        return;
      }

      const model: CreateTaskRequestModel = {
        competenceId: this.competenceId,
        name: value
      };
      this.taskService.create(model)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(x => {
          item.id = x;
          item.name = value;
          this.clearSelections();
          this.elements.sort();
        });
      return;
    }

    if (value) {
      const model: UpdateTaskRequestModel = {
        id: item.id,
        name: value,
        text: item.text
      };
      this.taskService.update(model)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(_ => {
          item.name = value;
          this.clearSelections();
          this.elements.sort();
        });
      return;
    }

    this.clearSelections();
    this.elements.sort();
  }

  protected updateText(value: string, item: TaskViewModel): void {
    const model: UpdateTaskRequestModel = {
      id: item.id,
      name: item.name,
      text: value
    };
    this.taskService.update(model)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(_ => {
        item.text = value;
        this.clearSelections();
        this.elements.sort();
      });
    return;
  }

  //#region general

  private clearSelections(): void {
    this.addSelection.clear();
    this.editSelection.clear();
  }

  //#endregion
}
