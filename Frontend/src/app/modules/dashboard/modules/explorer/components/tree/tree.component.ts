import { SelectionModel } from '@angular/cdk/collections';
import { FlatTreeControl } from '@angular/cdk/tree';
import { NgFor, NgIf, NgTemplateOutlet } from '@angular/common';
import { Component, DestroyRef, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatTreeFlatDataSource, MatTreeFlattener, MatTreeModule } from '@angular/material/tree';
import { RouterLink } from '@angular/router';
import { Observable, filter, take } from 'rxjs';
import { DynamicTextInputComponent } from 'src/app/modules/shared/components/inputs/dynamic-text-input/dynamic-text-input.component';
import { ROUTES_MAP } from 'src/app/modules/shared/constants/routes-map.const';
import { TreeViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/explorers/treeViewModel';
import { CreateCategoryRequestModel } from 'src/app/modules/shared/models/wep-api/domain/categories/createCategoryRequestModel';
import { UpdateCategoryRequestModel } from 'src/app/modules/shared/models/wep-api/domain/categories/updateCategoryRequestModel';
import { CreateJobRequestModel } from 'src/app/modules/shared/models/wep-api/domain/jobs/createJobRequestModel';
import { UpdateJobRequestModel } from 'src/app/modules/shared/models/wep-api/domain/jobs/updateJobRequestModel';
import { DialogService } from 'src/app/modules/shared/services/dialog.service';
import { PermissionService } from 'src/app/modules/shared/services/permission.service';
import { UserService } from 'src/app/modules/shared/services/user.service';
import { ExplorerService } from 'src/app/modules/shared/services/web-api/dashboard/explorer.service';
import { CategoryService } from 'src/app/modules/shared/services/web-api/domain/category.service';
import { JobService } from 'src/app/modules/shared/services/web-api/domain/job.service';
import { AutofocusDirective } from '../../../../../shared/directives/autofocus.directive';
import { FlatNode } from '../../models/tree/flatNode';
import { Node } from '../../models/tree/node';
import { NodeType } from '../../models/tree/nodeType';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-tree',
  templateUrl: './tree.component.html',
  styleUrls: ['./tree.component.scss'],
  standalone: true,
  imports: [NgIf, MatButtonModule, MatMenuModule, MatIconModule, MatDividerModule, RouterLink, MatTreeModule, NgTemplateOutlet, NgFor, MatInputModule, AutofocusDirective, DynamicTextInputComponent]
})
export class TreeComponent implements OnInit {
  private nodeMap = new Map<Node, FlatNode>();
  private flatNodeMap = new Map<FlatNode, Node>();
  protected selection = new SelectionModel<FlatNode>(false);

  //#region treeFlattener

  private _transformer = (node: Node, level: number): FlatNode => {
    let existingNode = this.nodeMap.get(node);
    let flatNode = existingNode ? existingNode : new FlatNode();

    flatNode.id = node.id;
    flatNode.name = node.name;
    flatNode.type = node.type;
    flatNode.level = level;
    flatNode.icon = this.getNodeIcon(node.type);
    flatNode.expandable = !!node.children?.length;
    flatNode.new = node.name === '';
    flatNode.editable = false;

    this.nodeMap.set(node, flatNode);
    this.flatNodeMap.set(flatNode, node);
    return flatNode;
  };

  private getNodeIcon(type: NodeType): string {
    switch (type) {
      case NodeType.Job: return 'local_library'
      default: return '';
    }
  }

  private treeFlattener = new MatTreeFlattener(
    this._transformer,
    node => node.level,
    node => node.expandable,
    node => node.children,
  );

  //#endregion

  protected treeControl = new FlatTreeControl<FlatNode>(
    node => node.level,
    node => node.expandable,
  );

  protected dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  protected NodeType = NodeType;
  protected ROUTES_MAP = ROUTES_MAP;

  protected readonly addMenuItems = [
    { icon: 'folder_open', name: 'Category', type: NodeType.Category },
    { icon: 'local_library', name: 'Job', type: NodeType.Job },
  ];

  protected companyId: number | undefined;

  constructor(private explorerService: ExplorerService,
    private categoryService: CategoryService,
    private jobService: JobService,
    private userService: UserService,
    protected permissionService: PermissionService,
    private dialogService: DialogService,
    private dataService: DataService,
    private destroyRef: DestroyRef) {

  }

  ngOnInit(): void {
    this.initSubscriptions();
  }

  private initSubscriptions(): void {
    this.userService.user$
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(x => {
        this.companyId = x.companyId;
        if (this.companyId) {
          this.loadData();
        }
      });

    this.dataService.jobId$
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        filter(x => x != null)
      )
      .subscribe(x => {
        if (!this.selected || x != this.selected.id) {
          this.setSelectedJobById(x!);
        }
      });
  }

  private loadData(): void {
    this.explorerService.getTree()
      .pipe(take(1))
      .subscribe(x => {
        this.handleGetTreeResponse(x);
        if (this.dataService.jobId) {
          this.setSelectedJobById(this.dataService.jobId)
        }
      });
  }

  //#region handleGetTreeData

  private handleGetTreeResponse(model: TreeViewModel): void {
    this.dataSource.data = this.getNodesForTree(model, undefined);
  }

  private getNodesForTree(model: TreeViewModel, categoryId: number | undefined): Node[] {
    let data: Node[] = [];
    model.categories
      .filter(x => x.parentId == categoryId)
      .forEach(x => {
        const item: Node = {
          id: x.id,
          name: x.name,
          type: NodeType.Category,
          children: this.getNodesForTree(model, x.id)
        };
        data.push(item);
      });

    model.jobs
      .filter(x => x.categoryId == categoryId)
      .forEach(x => {
        const item: Node = {
          id: x.id,
          name: x.name,
          type: NodeType.Job,
          children: []
        };
        data.push(item);
      });

    return data;
  }

  //#endregion

  protected setSelectedJobById(id: number): void {
    let element;
    this.nodeMap.forEach(x => {
      if (x.id == id && x.type == NodeType.Job) {
        element = x;
      }
    });

    if (element) {
      this.select(element);
      this.treeControl.expandAll();
    }
  }

  protected select(node: FlatNode): void {
    if (!this.selection.isSelected(node)) {
      this.selection.select(node);
      this.dataService.jobId$.next(node.id);
    }
  }

  protected deselect(): void {
    this.selection.clear();
    this.dataService.jobId$.next(undefined);
  }

  protected add(type: NodeType): void {
    let children = this.selected
      ? this.flatNodeMap.get(this.selected)?.children
      : this.dataSource.data;

    const node = {
      id: 1,
      type: type,
      name: '',
      children: []
    };

    children?.push(node);

    this.updateDataSource();

    if (this.selected) {
      this.treeControl.expand(this.selected);
    }
  }

  protected edit(): void {
    if (this.selected) {
      this.selected.editable = true;
    }
  }

  protected openDeleteConfirmationDialog(): void {
    this.dialogService.openConfirmationDialog("Are you sure you want to delete this element?")
      .afterClosed()
      .subscribe(x => {
        if (x) {
          this.delete();
        }
      });
  }

  private delete(node: FlatNode | undefined = undefined, onlyLocally = false): void {
    if (!node) {
      node = this.selected;
    }

    if (!node) {
      return;
    }

    const children = this.getParentChildren(node);
    if (!children) {
      return;
    }

    const child = this.flatNodeMap.get(node);
    if (!child) {
      return;
    }

    const index = children.indexOf(child);
    if (index >= 0) {
      if (!onlyLocally) {
        this.getDeleteRequest(node)
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe(_ => {
            children.splice(index, 1);
            this.updateDataSource();
            this.selection.clear();
          });
        return;
      }

      children.splice(index, 1);
      this.updateDataSource();
      this.selection.clear();
    }
  }

  private getDeleteRequest(node: Node) {
    return node.type === NodeType.Category
      ? this.categoryService.delete(node.id)
      : this.jobService.delete(node.id);
  }

  protected applyChanges(value: string, flatNode: FlatNode): void {
    if (flatNode.new) {
      if (!value) {
        this.delete(flatNode, true);
        return;
      }

      let node = this.flatNodeMap.get(flatNode);
      if (!node) {
        this.delete(flatNode, true);
        return;
      }

      this.getCreateRequest(flatNode, node, value)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(x => {
          node!.id = x;
          node!.name = value;
          flatNode.new = false;
          this.updateDataSource();
        });
      return;
    }

    if (!value) {
      flatNode.editable = false;
      return;
    }

    let node = this.flatNodeMap.get(flatNode);
    if (node) {
      this.getUpdateRequest(node, value)
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(_ => {
          node!.name = value;
          this.updateDataSource();
        })
      return;
    }

    this.updateDataSource();
  }

  private getCreateRequest(flatNode: FlatNode, node: Node, name: string): Observable<number> {
    const parentId = this.getParent(flatNode)?.id;
    if (node.type === NodeType.Category) {
      const model: CreateCategoryRequestModel = {
        companyId: this.companyId!,
        parentId: parentId,
        name: name
      };

      return this.categoryService.create(model)
    }

    const model: CreateJobRequestModel = {
      companyId: this.companyId!,
      categoryId: parentId,
      name: name
    };
    return this.jobService.create(model);
  }

  private getUpdateRequest(node: Node, name: string) {
    if (node.type === NodeType.Category) {
      const model: UpdateCategoryRequestModel = {
        id: node.id,
        name: name
      };

      return this.categoryService.update(model)
    }

    const model: UpdateJobRequestModel = {
      id: node.id,
      name: name
    };
    return this.jobService.update(model);
  }

  //#region general

  private updateDataSource(): void {
    this.flatNodeMap.clear();
    let data = this.dataSource.data;
    this.dataSource.data = data;
  }

  private getParentChildren(node: FlatNode): Node[] | undefined {
    if (node.level === 0) {
      return this.dataSource.data;
    }

    const parent = this.getParent(node);
    return parent?.children;
  }

  private getParent(node: FlatNode): Node | undefined {
    if (node.level < 1) {
      return undefined;
    }

    const startIndex = this.treeControl.dataNodes.indexOf(node) - 1;
    for (let i = startIndex; i >= 0; i--) {
      let dataNode = this.treeControl.dataNodes[i];
      if (dataNode && dataNode.level < node.level) {
        return this.flatNodeMap.get(dataNode);
      }
    }

    return undefined;
  }

  protected get selected(): FlatNode | undefined {
    return this.selection.selected[0];
  }

  //#endregion
}
