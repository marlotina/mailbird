# Mailbird test

### Application information
The test have one app in Angular and an one API in Net Core in c#. 
I’ve used the library MailKit for the implementation of the POP3 and IMAP. It has a MIT License and It is free for commercial use. It also is the most used and It has best valorations and It is part of .NET Foundation and Contributors.


### How run the site
Open command window and move yourself where you download the application folder, Then navigate inside the folder to the next path:
```
mailbird-master\mailbird-master
```
In this folder you will find the `docker-compose.yml` file.
Run the nexts commands:
```
docker-compose pull
docker-compose up -d
```
Open the following Url in the browser: 
**http://localhost:5800/**

### Image routes in hub.docker

hub.docker repository:
 - marlotina/mailappexercise:api
 - marlotina/mailappexercise:site