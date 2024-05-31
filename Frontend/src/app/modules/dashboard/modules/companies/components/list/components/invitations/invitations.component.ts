import { NgFor, NgIf } from '@angular/common';
import { Component, DestroyRef, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { finalize } from 'rxjs';
import { CompanyInvitationViewModel } from 'src/app/modules/shared/models/wep-api/dashboard/companies/companyInvitationViewModel';
import { RespondInvitationRequestModel } from 'src/app/modules/shared/models/wep-api/dashboard/companies/respondInvitationRequestModel';
import { CompaniesService } from 'src/app/modules/shared/services/web-api/dashboard/companies.service';

@Component({
  selector: 'app-invitations',
  templateUrl: './invitations.component.html',
  standalone: true,
  imports: [NgIf, MatListModule, NgFor, MatButtonModule, MatIconModule]
})
export class InvitationsComponent implements OnInit {
  protected isLoading = true;
  protected elements: CompanyInvitationViewModel[] = [];

  constructor(private companiesService: CompaniesService,
    private destroyRef: DestroyRef) {

  }

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.companiesService.getInvitations()
      .pipe(finalize(() => this.isLoading = false))
      .subscribe(x => this.elements = x);
  }

  protected accept(companyId: number): void {
    this.respondInvitation(companyId, true);
  }

  protected decline(companyId: number): void {
    this.respondInvitation(companyId, false);
  }

  private respondInvitation(companyId: number, accept: boolean): void {
    const model: RespondInvitationRequestModel = {
      companyId: companyId,
      accept: accept
    };
    this.companiesService.respondInvitation(model)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(_ => this.delete(companyId));
  }

  private delete(compantId: number): void {
    const index = this.elements.findIndex(x => x.companyId == compantId);
    if (index >= 0) {
      this.elements.splice(index, 1);
    }
  }
}
