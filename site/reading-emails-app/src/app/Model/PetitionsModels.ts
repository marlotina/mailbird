export class ServerConfiguration {
    serverType!: string;
    server!: string;
    port!: number;
    encryption!: string;
    username!: string;
    password!: string;
}

export class EmailDetailPetition {
    serverType!: string;
    server!: string;
    port!: number
    encryption!: string;
    username!: string;
    password!: string;
    emailId!: string
}

export class EmailsPetition {
    serverType!: string;
    server!: string;
    port!: number;
    encryption!: string;
    username!: string;
    password!: string;
    page!: number;
    items!: number;
}