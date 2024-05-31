import { FormGroup } from "@angular/forms";

export function confirmPasswordValidator(controlName: string, confirmControlName: string) {
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[controlName];
    if (!control) {
      throw getNoControlError(controlName);
    }

    const confirmControl = formGroup.controls[confirmControlName]
    if (!confirmControl) {
      throw getNoControlError(confirmControlName);
    }

    if (confirmControl.errors && !confirmControl.hasError('confirmMismatch')) {
      return;
    }

    if (control.value !== confirmControl.value) {
      confirmControl.setErrors({ confirmMismatch: true });
      return;
    }

    confirmControl.setErrors(null);
  };
}

function getNoControlError(controlName: string): Error {
  return new Error(`There's no ${controlName} control in the form group.`);
}
