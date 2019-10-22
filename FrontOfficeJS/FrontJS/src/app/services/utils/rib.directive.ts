import { Directive, HostListener, ElementRef } from '@angular/core';

@Directive({
  selector: '[Rib]'
})
export class RibDirective {

  @HostListener('input', ['$event'])
  onKeyDown(event: KeyboardEvent) {
    const input = event.target as HTMLInputElement;

    let trimmed = input.value.replace(/-+/g, '');
    if (trimmed.length > 26) {
      trimmed = trimmed.substr(0, 26);
    }

    let numbers = [];
    let j=0;
    for (let i = 0; i < trimmed.length; i += j) {
      if(i<10){
        j = 5;
        numbers.push(trimmed.substr(i, j));
      }
      else{
        j = 11;
        numbers.push(trimmed.substr(i, j));
      }
    }

    input.value = numbers.join('-');

  }

}
