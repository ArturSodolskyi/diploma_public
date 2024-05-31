import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { CreateUserCompanyInvitationRequestModel } from 'src/app/modules/shared/models/wep-api/domain/user-company-invitations/createUserCompanyInvitationRequestModel';
import { UserCompanyInvitationService } from 'src/app/modules/shared/services/web-api/domain/user-company-invitation.service';
import { EmailFieldComponent } from '../../../../../../../shared/components/fields/email-field/email-field.component';
import { DataService } from '../../../../services/data.service';

@Component({
  selector: 'app-invite-user-dialog',
  templateUrl: './invite-user-dialog.component.html',
  standalone: true,
  imports: [MatDialogModule, FormsModule, ReactiveFormsModule, EmailFieldComponent, MatButtonModule]
})
export class InviteUserDialogComponent {
  protected form!: FormGroup<{
    email: FormControl<string>
  }>;

  constructor(private fb: FormBuilder,
    private dialogRef: MatDialogRef<InviteUserDialogComponent>,
    private userCompanyInvitationService: UserCompanyInvitationService,
    private dataService: DataService) {
    this.createForm();
  }

  private createForm(): void {
    this.form = this.fb.group({
      email: this.fb.nonNullable.control<string>(''),
    });
  }

  protected invite(): void {
    const model: CreateUserCompanyInvitationRequestModel = {
      email: this.form.controls.email.value,
      companyId: this.dataService.companyId!
    };
    this.userCompanyInvitationService.create(model)
      .subscribe(_ => this.dialogRef.close());
  }
}
