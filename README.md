
# Project README

## Overview

This is an MVC-based web application designed to manage **Suppliers** and **Materials** efficiently. Built using **C#**, **TypeScript**, **Angular**, and **MySQL**, this application allows users to perform CRUD (Create, Read, Update, Delete) operations on supplier and material data. It also provides advanced filtering for easy data management.

## Features

- **Supplier and Material Management**: Full CRUD functionality to manage supplier and material data.
- **Data Filtering**: Filter materials and suppliers by name and date of creation using an intuitive search interface.
- **Responsive User Interface**: Built with Angular and Bootstrap, ensuring a clean and responsive design across all devices.
- **Database Integration**: MySQL database is used for persistent storage of supplier and material data, ensuring reliable and fast data access.

## Tech Stack

- **Frontend**: 
  - **Angular**: Framework for building dynamic single-page applications.
  - **TypeScript**: Superset of JavaScript used for building scalable, maintainable applications.
  - **Bootstrap**: For responsive, mobile-first web development.
  
- **Backend**: 
  - **C#**: Primary language for the backend, utilizing the .NET MVC architecture for efficient data handling and routing.
  
- **Database**:
  - **MySQL**: A relational database used for storing supplier and material information.

## Installation

To run this project locally, follow these steps:

### Prerequisites

1. Install **Node.js** (version 16 or higher) to run the Angular frontend.
2. Install **MySQL** for the database.

### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/martins158/Vega-Test
   ```

2. Navigate to the frontend directory and install dependencies:

   ```bash
   cd frontend
   npm install
   ```

3. Set up the **MySQL** database:

   - Create a new database `BD`.
   - The database integration is already set up. Make sure that the MySQL database is running and the connection is active. 
   - The migration has been executed to automatically set up the required tables. 
   - Configure the backend in **C#** to connect to the MySQL database by updating the `connectionString` in the configuration file.

4. The database can be created with any name of your choice; however, I recommend using the name 'BD' as I have done in my setup.   The database is configured on a local server **localhost**, with the database name 'projetovega'. For the initial access, the database uses the following credentials:
**Username: root**
**Password: 12345678**
Please ensure that these details match your local setup to establish a successful connection.

5. Build and run the backend API:

   ```bash
   cd backend
   dotnet build
   dotnet run
   ```

6. Run the Angular frontend:

   ```bash
   ng serve
   ```

   The application will be available at `http://localhost:4200`.


## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgements

- **Angular** for the frontend framework.
- **Bootstrap** for responsive design.
- **MySQL** for the database solution.
- **C#** and **.NET** for robust backend development.
