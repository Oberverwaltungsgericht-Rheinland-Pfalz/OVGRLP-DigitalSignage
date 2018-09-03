import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-display-dialog",
  templateUrl: "./display-dialog.component.html",
  styleUrls: ["./display-dialog.component.css"]
})
export class DisplayDialogComponent implements OnInit {
  public screenshot: String;
  public show: boolean = false;
  public increasePicture: boolean = false;

//http://blog.armstrongconsulting.com/?p=407
  constructor(  ) { }

  open(screenshot: String) {
    this.screenshot = screenshot;
    this.show = true;
  }

  ChangeSize(shouldSize: boolean) {
    this.increasePicture=shouldSize;
  }
  ChangeSize1(){
    this.ChangeSize(!this.increasePicture);
  }

  close() {
    this.show = false;
  }

  ngOnInit() {
    
  }
}
