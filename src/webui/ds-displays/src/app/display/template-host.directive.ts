import { Directive, ViewContainerRef } from "@angular/core";

@Directive({
  selector: "[template-host]"
})
export class TemplateHostDirective {
  constructor(public viewContainerRef: ViewContainerRef) {}
}
