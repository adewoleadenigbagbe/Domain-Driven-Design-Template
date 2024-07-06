# Domain-Driven-Design-Template

## Overview
The aim of this repo is to explain the structure of a domain driven design for an Project. Before further explanation, the solution contains some custom functions and endpoints, this is not a full blown application but just an example to explain how domain driven design is implemented. This is solution uses .Net 4.8

This domain design using CQRs Pattern (Command and Query Responsibility Segregation) , to read more about CQRS ,visit this link [CQRS](https://en.wikipedia.org/wiki/Command%E2%80%93query_separation). The solution has multiple projects in it which will be explained further below. 

A Standard domain driven design solution should start with a naming convention for project which goes [CompanyName].[ProductName].[ProjectName], it can be something like this for a project Microsoft.Aspire.Api, here in this solution i decided to use [ProductName].[ProjectName] for project because i dont have a company LOL

Project are arranged in grouped folders based on their functionality, for the root i have the src folder and the tests,src containing application code functionality and tests containing the unit test for the src project. Under the src folder, there are different sub folders which are 

Components (Application code functionality, third party integrations)
Hosts (Host for application as well as configuration)
Services (API services, Grpc services)
Shared (Common functionality referenced by other project in the solution)
Tools (Export service like Excel , csv)


Projects in this solution

Components
App.Infastructure 

Hosts
App.Host.Configuration
App.Host.SelfHost
App.Host.SystemWeb

Services
App.Api

Shared
App.Buisness
App.Common
App.Data
App.Data.MySQL


Tools
App.Export


The App.Infastructure uses the CQRS pattern using Commands, Queries And Handlers , here we use a CQRS Library called Mediatr. 
Commands for state creation and alteration. 
Queries to get and read state or object.
Handlers as the functions to execute an actions

Features/Entities are grouped in either the Commands or Query Folder, for example in the project , under the Commands folders we have a Product folder that keeps files related to product, CreateProduct, UpdateProduct, this is a correct pattern to arrange or grouped entities/features


The App.API contains conntrollers for endpoint calls, this should not include any logic or any form of validations , it should take request from the client , call the cqrs handlers and return response to the client. This references the App.Infastructure


App.Data contains entities that can be in a persistent storage, aka database entities and related entities function, it should contains database contexts used for object query or write. In this project we have ReadContext and WriteContext

App.Data.MySQL - for this project, entities are persisted in MySQL , this manages connection to MySQL database as well related MySQl Migrations, it is a good practice to have migration in this folder. If you have other Database provider you need to use, should create a different project for the provider, for ex: App.Data.SqlServer. it also contains MySQl Read and Write context which inherit from tha App.Data read/Write Contexts


App.Common - Groups related models, helper methods that are referenced in other part of the projects

App.Buisness - Contains core business logic for the application


App.Host.Configuration 
Contains configuration for application hosting , Service Registraion , managing connection strings and app settings
Service Registrations - We use Ninject in this project for service Registration for other project
Third Party Library Configurations- AutoMapper, Refit, Mediatr
ConnectionStrings - Shared.ConnectionStrings, Shared.AppSettings


App.Host.SystemWeb - A Website to host you endpoints , reference App.API


App.Host.SelfHost - If you have application that need to be self hosted in its own process , a project should be used for that, in this solutin we use OWIN


App.Export- Contains services for excel and csv













