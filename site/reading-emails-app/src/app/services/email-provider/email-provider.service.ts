import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmailBody, EmailList } from '../../Model/EmailModels';
import { ServerConfiguration } from '../../Model/PetitionsModels';

@Injectable({
  providedIn: 'root'
})
export class EmailProviderService {

  constructor(private httpClient: HttpClient) { }

  getEmails(data: ServerConfiguration, index: number, items: number) {
    let params = this.GetBaseParams(data);
    params = params.append('index', index);
    params = params.append('items', items);

    return this.httpClient.get<EmailList>(`${environment.emailApi}/api/email/GetEmails`, { headers: this.GetHeader(data), params: params });
  }

  getEmailBody(data: ServerConfiguration, emailId: number) {
    let params = this.GetBaseParams(data);
    params = params.append('emailId', emailId);

    return this.httpClient.get<EmailBody>(`${environment.emailApi}/api/email/GetEmaillBody`, { headers: this.GetHeader(data), params: params });
  }  

  private GetBaseParams(data: ServerConfiguration) {
    let params = new HttpParams();
    params = params.append('serverType', data.serverType);
    params = params.append('server', data.server);
    params = params.append('port', data.port);
    params = params.append('encryption', data.encryption);
    return params;
  }

  private GetHeader(data: ServerConfiguration){
    let headers = new HttpHeaders();
    headers = headers.append("Authorization", "Basic " + btoa(`${data.username}:${data.password}`));
    return headers;
  }
}

