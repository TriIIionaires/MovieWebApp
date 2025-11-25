# 🎬 MovieWebApp
A full-stack web app built with the .NET Framework that allows users to interact with movie data. This project was built for learning purposes to understand the basics of system design and full-stack development. The project uses a Kaggle dataset of movies (36,000+ movies) as a sample with the SQL file being provided. The database was hosted locally using MAMP that provides a MySQL server to test functionality.

## Technologies
[![My Skills](https://skillicons.dev/icons?i=cs,html,css,dotnet,bootstrap,mysql,visualstudio&theme=light)](https://skillicons.dev)  
**Including:** MAMP for local testing

## Features
- **CRUD Functionality:** Users can browse, search, and rate movies.
- **User Accounts:** Site viewers can create their own account for personalized recommendations.
- **Movie Recommendations:** User accounts can receive movie recommendations based on how they previously rated movies.

## Process
The project started out with importing the Kaggle dataset into a table for the database. I used a third-party API to also add a column for the movie's MPAA rating. Separate tables for genre and rating were created to inner join with the movie table. Finally, a user table and table for user ratings was created.

Next, I developed the CRUD functionality to able to interact with the MySQL database. I created a dynamic link library (DLL) to handle data access and parameterize SQL queries for app security. Then the RESTful API was created to communicate data to user clients.

I used Blazor and Bootstrap to develop the app's pages to display movie information and login/signup pages. Additionally, I used cookie authentication to login users to their account. 

## Running the Project
To run the project in your local environment, follow these steps:  
1. Clone the repository to your local machine into Visual Studio.
2. Import the SQL file to your own database.
3. Create a user account on your database and configure the `connectionString` in `SqlDataAccess.cs`.
4. Run both the MovieAPI and MovieUI projects to fire up the server and view the app.

## Video Demo
https://github.com/user-attachments/assets/842cef16-7a00-4ee5-acb5-ef08ee216d53

