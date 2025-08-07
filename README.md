**BankApplication**

The Bank Application is the final project for the **"Creating Database Applications"** course, developed during the 6th semester of Informatics studies. 
As the main contributor, I am responsible for leading the team, managing pull requests, merging code, assigning tasks, and providing guidance. 

**The application requires:**  
1).Net Core SDK (minimum 6.0)  
2) Node.js (minimum v 20)  
3) SQL Server management studio (to manage your database) //application uses localdb by EF core .sqlserver package  
4) Visual studio (backend) and Visual Studio Code (frontend)  


**Running the application on your local machine:**  

1) Frontend (React): Clone the repository. Navigate to the frontend folder and install the necessary dependencies by running: **npm install**. Make sure you have Node.js installed on your system (version >= 20). Follow any additional instructions provided in the project to run the app locally.

2) Backend (ASP.NET): Ensure that you have the .NET SDK installed on your machine. Install the required dependencies for the backend. (if it didnt download automatically, you will need to download nuget package for EF, Fluent Validation and AutoMapper) In the NuGet Package Manager Console, run the: **update-database** command to update the database with the latest migrations.
3) As a contributor please do not write code at the main branch, createh feature branch push and then create pull reques to the main one.


Default login to admin account:  
**login**: adminTab  
**password**: Admin!  
**email**: admin@admin.pl  
**verification code**: 1234567


**📄 Technical Documentation**  
The app is module oriented project, each module has its own controllers and models.  

On the backend side there are:  
1) BankApplication.Data (entities and enums connected to database models)  
2) BankApplication.App (web api solution; contains controllers and services)  
