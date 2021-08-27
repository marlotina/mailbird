export const environment = {
  production: false,
  emailApi:"http://localhost:44379",
  itemsPerLoad: 20,
  serverTypes: ['POP3', 'IMAP'],
  encryptions: [{id:'UNENCRYPTED', text:'Unencrypted'},{ id:'SSL_TLS', text:'SSL/TLS'}, { id:'STARTTLS', text:'STARTTLS'}]
};

