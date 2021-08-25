import { Component, Input, OnInit } from '@angular/core';
import { EmailBody, EmailItem } from 'src/app/Model/EmailModels';
import { ServerConfiguration } from 'src/app/Model/PetitionsModels';
import { EmailProviderService } from 'src/app/services/email-provider/email-provider.service';

@Component({
  selector: 'app-email-detail',
  templateUrl: './email-detail.component.html',
  styleUrls: ['./email-detail.component.scss']
})
export class EmailDetailComponent implements OnInit {
  @Input() emailData?: EmailItem;
  @Input() serveConfig?: ServerConfiguration;

  isLoading = false;

  emailBody: EmailBody | undefined;
    
  constructor(
    private emailProviderService: EmailProviderService) { }

  ngOnInit(): void {
  }

  ngOnChanges() {
    if(this.emailData != null && this.serveConfig){
      this.isLoading = true;
      
      this.emailProviderService.getEmailBody(this.serveConfig, this.emailData.id)
        .subscribe((data: EmailBody) => {
          this.emailBody = data;
          this.isLoading = false;
        });
    }
  }
}
