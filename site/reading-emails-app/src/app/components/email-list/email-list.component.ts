import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { EmailItem } from 'src/app/Model/EmailModels';
import { EncryptionItem } from 'src/app/Model/EncriptionModels';
import { ServerConfiguration } from 'src/app/Model/PetitionsModels';
import { EmailProviderService } from 'src/app/services/email-provider/email-provider.service';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-email-list',
  templateUrl: './email-list.component.html',
  styleUrls: ['./email-list.component.scss']
})

export class EmailListComponent implements OnInit {

  submitted = false;
  isLoadingEmails = false;
  showConfiguration = true;
  hasEmailSelected = false;

  emailList: EmailItem[] = [];
  selectedEmail?: EmailItem;
  serveConfig?: ServerConfiguration;
  errorMessages: string[] = []; 
  
  encryptionList: EncryptionItem[];
  serverTypes: string[];

  mailConfigurationForm = this.formBuilder.group({
    serverType: ['', Validators.required],
    server: ['', Validators.required],
    port: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
    encryption: ['', Validators.required],
    username: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  constructor(
    private formBuilder: FormBuilder,
    private emailProviderService: EmailProviderService) {
      this.encryptionList = environment.encryptions;
      this.serverTypes = environment.serverTypes;
    }

  ngOnInit(): void {
  }

  get f(): { [key: string]: AbstractControl } {
    return this.mailConfigurationForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;
    this.resetValues();

    if (this.mailConfigurationForm.invalid) {
      return;
    }
    this.serveConfig = this.mailConfigurationForm.value;
    this.isLoadingEmails = true;
    this.retrieveAllEmails(this.mailConfigurationForm.value);
  }

  changeSelectedEmail(selectedEmail: EmailItem)
  {
    this.selectedEmail = selectedEmail;
    this.hasEmailSelected = true;
  }

  async retrieveAllEmails(serveConfig: ServerConfiguration){
    let finish = false;
    let index = 0;
    while (!finish){
     await this.emailProviderService.getEmails(serveConfig, index, environment.itemsPerLoad)
              .toPromise()
              .then(data=> {
                if(data.list.length > 0){
                  for(var i = 0; i < data.list.length;i++){
                    this.emailList.push(data.list[i]);  
                  } 
                  index += environment.itemsPerLoad;
                  finish = index >= data.totalEmails;
                }
              })
              .catch(errorResponse => 
              {
                this.manageErrors(errorResponse);
                finish = true;   
              });
    }
    this.isLoadingEmails = false;
  }

  onReset(){
    this.mailConfigurationForm.reset();
    this.submitted = false;
    this.resetValues();
  }

  resetValues() {
    this.errorMessages = [];
    this.serveConfig = undefined;
    this.selectedEmail = undefined;
    this.emailList = [];
  }

  removeAlertError(){
    this.errorMessages = [];
  }
  
  private manageErrors(errorResponse: any) {
    if (errorResponse.status == 400) {
      for (var i = 0; i < errorResponse.error.length; i++) {
        this.errorMessages.push(errorResponse.error[i]);
      }
    }
    else if (errorResponse.status == 0) {
      this.errorMessages.push(errorResponse.message);
    } else {
      this.errorMessages.push(errorResponse.error.detail);
    }
  }
}
