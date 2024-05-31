import { NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { ROUTES_MAP } from '../shared/constants/routes-map.const';
import { NavigationService } from '../shared/services/navigation.service';
import { RoleService } from '../shared/services/role.service';
import { AccountButtonComponent } from './components/account-button/account-button.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  standalone: true,
  imports: [MatSidenavModule, NgFor, NgIf, MatButtonModule, RouterLinkActive, MatTooltipModule, RouterLink, MatIconModule, AccountButtonComponent, RouterOutlet]
})
export class DashboardComponent {
  protected navigationItems: any[] = [];

  constructor(protected navigationService: NavigationService,
    private roleService: RoleService) {
    this.setNavigationItems();
  }

  setNavigationItems(): void {
    this.navigationItems = [
      {
        icon: 'assessment',
        tooltip: 'Reviews',
        link: ROUTES_MAP.Reviews,
        show: true
      },
      {
        icon: 'school',
        tooltip: 'Explorer',
        link: ROUTES_MAP.Explorer,
        show: true
      },
      {
        icon: 'domain',
        tooltip: 'Companies',
        link: ROUTES_MAP.Companies,
        show: true
      },
      {
        icon: 'supervisor_account',
        tooltip: 'Users',
        link: ROUTES_MAP.Users,
        show: this.roleService.isAdministrator()
      }
    ];
  }
}
