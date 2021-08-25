import { Pipe, PipeTransform } from '@angular/core';
import { AddressItem } from '../Model/EmailModels';

@Pipe({
  name: 'emailAddresses'
})
export class EmailAddressesPipe implements PipeTransform {

  transform(value: AddressItem[]):string {
      let emailText = '';
      for(let i =0; i < value.length;i++)
      {
        if(i> 0){
          emailText += ", ";  
        }

        if(value[i].email != '')
          emailText += `${value[i].email}` 
        
        
        if(value[i].name != '')
          emailText += `(${value[i].name})` 
      }
      
      return emailText;
  }



}
