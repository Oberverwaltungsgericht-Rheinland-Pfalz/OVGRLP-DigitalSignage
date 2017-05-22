import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';

import { TerminService, Termin } from 'ds-core';

@Component({
  selector: 'app-termine',
  templateUrl: './termine.component.html',
  styleUrls: ['./termine.component.css']
})
export class TermineComponent implements OnInit {

  _displayName: string;
  termineForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private terminService: TerminService) {
    this.createForm();
  }

  createForm() {
    this.termineForm = this.formBuilder.group({
      termine: this.formBuilder.array([])
    });
  }

  setTermine(termine: Termin[]) {
    const termineFormGroups = termine.map(termin => this.formBuilder.group({
      uhrzeit: termin.uhrzeitAktuell,
      az: termin.az
    }));
    const termineFormArray = this.formBuilder.array(termineFormGroups);
    this.termineForm.setControl('termine', termineFormArray);
  }

  get termine(): FormArray {
    return this.termineForm.get('termine') as FormArray;
  }

  @Input()
  set displayName(displayName: string) {
    this._displayName = displayName;
    this.terminService.getTermine(displayName)
      .subscribe(termine => {
        this.setTermine(termine);
      });
  }
  get displayName(): string { return this._displayName; }

  ngOnInit() {
  }

  onSubmit() {
  }
}
