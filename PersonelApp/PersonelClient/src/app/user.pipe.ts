import { Pipe, PipeTransform } from '@angular/core';
import { UserModel } from './app.component';

@Pipe({
  name: 'userPipe',
  standalone: true
})
export class UserPipe implements PipeTransform {

  transform(value: UserModel[], search: string): UserModel[] {
    if(search === "") return value;

    return value.filter(p=> 
        p.name.toLowerCase().includes(search.toLowerCase()) ||
        p.lastname.toLowerCase().includes(search.toLowerCase()) ||
        p.email.toLowerCase().includes(search.toLowerCase()) ||
        p.phoneNumber.toLowerCase().includes(search.toLowerCase()) ||
        p.country.toLowerCase().includes(search.toLowerCase()) ||
        p.city.toLowerCase().includes(search.toLowerCase()) ||
        p.postalCode.toLowerCase().includes(search.toLowerCase()) ||
        p.fullAddress.toLowerCase().includes(search.toLowerCase()))
  }

}
