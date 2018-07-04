# ContactCRUDProject

Requirements:
1. Visual Studio 2017 : I have made this project on VS2017.
2. SQL Server 2008 and above.

Prerequsite:
1. Change the connection string in Web.cofig file of CRUD project to local database.
2. Change the connection string in app.cofig file of ContactManager project ContactManager.Data folder to local database.

Once you Build the project and run and create a conatact information, the database(ContactDetails)will be created in SQL server with a table named as (Contacts). I have used code-first approach to build the project.

Design:
 I have used Repository pattern to do CRUD operation on Contact Information.
 I have divided this project into 3 levels:
	1. ContactManager
	2. Framework
	3. CRUD
	
	1. ContactManager:
		This projects is further splitted into:
		a. ContactManager.Core :
			1. This contains the domain models such as Contact and Status.
			2. This also contains the services that need to be performed such as Get(), GetALL(), Create(), Modify(),ActiveDeactivate()
		b. ContactManager.Data
		c. ContactManager.UnitTests: This part is still pending,due to insufficient time.
		
	2. Framework:
		Here all the context operations are performed.This is very generic library, that can be used by any project,when required for context operations.
	
	3. CRUD:
		This layer conatins the Controller, Views and Viewmodels for data manipuration and rendering of data onto the view.
 
Note: I have not written Unit Tests, due to time Constraint.