import { Component, DestroyRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { AuthService } from 'src/app/modules/shared/services/auth.service';
import { DialogService } from 'src/app/modules/shared/services/dialog.service';
import { UserService } from 'src/app/modules/shared/services/web-api/domain/user.service';

@Component({
  selector: 'app-account-button',
  templateUrl: './account-button.component.html',
  styleUrls: ['./account-button.component.scss'],
  standalone: true,
  imports: [MatButtonModule, MatTooltipModule, MatMenuModule, MatIconModule]
})
export class AccountButtonComponent {
  constructor(private authService: AuthService,
    private userService: UserService,
    private dialogService: DialogService,
    private destroyRef: DestroyRef
  ) {

  }

  protected logout(): void {
    this.authService.logout();
  }

  protected deleteAccount(): void {
    this.dialogService.openConfirmationDialog("Are you sure you want to delete your account?")
      .afterClosed()
      .subscribe(x => {
        if (x) {
          this.delete();
        }
      });
  }

  private delete(): void {
    this.userService.delete()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(_ => this.logout());
  }
}
