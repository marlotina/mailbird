#Mailbird test

###Application information
The test have one app in Angular and an one API in Net Core in c#. 
I’ve used the library MailKit for the implementation of the POP3 and IMAP. It has a MIT License and It is free for commercial use. It also is the most used and It has best valorations and It is part of .NET Foundation and Contributors.


### How run the site
Open command window and move where you download the application to the following path:

**mailbird-master\mailbird-master**

where there is the docker-compose.yml

Run the nexts commands:
**docker-compose pull**
**docker-compose up -d**
 
open in the browser: 
**http://localhost:5800/**

### Routes image in hub.docker
marlotina/mailappexercise:api
docker pull marlotina/mailappexercise:api


### Aclarations
During the build of angular application has several warnings of deprecated library, I updated the angular cli and everithing to the last version and I tried to solve all of them but I would need more time, these update usually not a trivial process. Anyway for my laptop and future develops I will done.

I couldn't finish all the unit tests but I did the most important to help me during the develop.




