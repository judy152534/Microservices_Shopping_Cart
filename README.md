# Microservices

using .net6.0

Catalog API : 
1. add basic CURD with mongoDB
2. docker-compose

Basket API :
1. use Redis for distributed cache
2. docker-compose

Discount API :
1. add basic CURD with PostgresDB 
2. docker-compose

Discount gRPC :
1. provide server method & communicate with Basket.API
2. docker-compose

Order API :
1. add basic CURD with SqlServer 
2. docker-compose

RabbitMQ:
1. Pub/sub :Bridge between Basket API & Order API

API gateway:
Using Ocelot
