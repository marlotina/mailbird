export class EmailBody {
    isHtml:false = false;
    text!: string;
}

export class EmailList 
{
    totalEmails!:number;
    list!: EmailItem[];
}

export class EmailItem {
    id!: number;
    isRead!:boolean;
    to!: AddressItem[];
    from!: AddressItem[];
    subject!: string;
    date!: Date;
    extraInfo!: EmailExtraInfo;
}

export class AddressItem {
    name!: string;
    email!: string;
}

export class EmailExtraInfo {
    isRead!: boolean;
}