import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../components/confirmation-dialog/confirmation-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private dialog: MatDialog) { }

  public openConfirmationDialog(text: string): MatDialogRef<ConfirmationDialogComponent, boolean> {
    return this.dialog.open<ConfirmationDialogComponent, string, boolean>(ConfirmationDialogComponent, {
      data: text
    });
  }
}
