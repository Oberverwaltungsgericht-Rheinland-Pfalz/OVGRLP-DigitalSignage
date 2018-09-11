import { Component,  EventEmitter, OnInit, Input, Output  } from '@angular/core';
import { fadeSlide } from '@clr/angular';

@Component({
  selector: 'yes-no-dialog',
  templateUrl: './yes-no-dialog.component.html',
  styleUrls: ['./yes-no-dialog.component.css']
})
export class YesNoDialogComponent implements OnInit {
  @Input() title: string;
  @Input() message: string;
  @Output() result = new EventEmitter<boolean>();
  public show: boolean = false;

  constructor() { }

  open() {
    this.show = true;
  }

  ngOnInit() {
  }

  close(result:boolean) {
    this.result.emit(result);
    this.show=false;
  }

}
