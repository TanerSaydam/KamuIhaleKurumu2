import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'todo',
  standalone: true
})
export class TodoPipe implements PipeTransform {

  transform(value: any[], search: string): any[] {
    return value.filter(p=> p.title.includes(search));
  }

}
