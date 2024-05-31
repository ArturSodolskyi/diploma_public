import { Component, DestroyRef, Input, OnInit, ViewChild } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatDrawer, MatSidenavModule } from '@angular/material/sidenav';
import { RouterOutlet } from '@angular/router';
import { NavigationService } from '../../services/navigation.service';

@Component({
  selector: 'app-base-drawer',
  templateUrl: './base-drawer.component.html',
  styleUrls: ['./base-drawer.component.scss'],
  standalone: true,
  imports: [MatSidenavModule, RouterOutlet]
})
export class BaseDrawerComponent implements OnInit {
  @Input() position: 'start' | 'end' = 'start';
  @Input() enableClosingOnLinkClick = true;

  @ViewChild('drawer', { static: true }) drawer!: MatDrawer;
  constructor(private navigationService: NavigationService,
    private destroyRef: DestroyRef) {

  }

  ngOnInit(): void {
    this.initSubscriptions();
  }

  private initSubscriptions(): void {
    if (this.enableClosingOnLinkClick) {
      this.navigationService.onLinkClick$
        .pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe(_ => this.drawer.toggle());
    }
  }
}
