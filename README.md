# P2000 for Raspberry Pi (Pi2000)
This project contains multiple tools for receiving, interpreting and displaying P2000 alerts. 

P2000 is a network used by Dutch Emergency Services to call up emergency staff to incidents. The information transmitted over this network is unencrypted, so anyone can receive these messages when using the right equipment.

## The project
This project contains all the software necessary in order to receive, interpret and display P2000 messages. The project consists of multiple services that handle one of these tasks each.
## Techniques used
* Dotnet Core 3
* Autofac
* RabbitMQ
* Microservice architecture
* ElasticSearch
* Kibana
* Health monitoring
* REST API
* SignalR

## Disclaimer
This project uses a microservice architecture and message queueing to separate the functionality this application has to different services. Keep in mind though this is a small project, which results in very small services with very little code each. The purpose of this project is not to create the most efficient application, but more a way for me to be able to apply these techniques to a project in order to create a better understanding of these techniques. Keep this in mind when reading the code and suggesting improvements.