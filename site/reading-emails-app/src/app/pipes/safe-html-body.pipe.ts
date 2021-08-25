import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Pipe({
  name: 'safeHtmlBody'
})
export class SafeHtmlBodyPipe implements PipeTransform {

  constructor(private domSanitizer: DomSanitizer) {}

  transform(value: string) {
    return this.domSanitizer.bypassSecurityTrustHtml(value);
  }

}
