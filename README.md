# Employee API

## Context

This repository contains the work developed during my internship thesis. The project involves creating an application that connects a front-end application with a back-end application through an MQTT broker. The goal is to build a fully functional dashboard that can be displayed on the factory floors at Hoppenbrouwers locations across the Netherlands. The dashboard remains updated by subscribing to various topics on an MQTT broker, which are updated by the back-end application.

https://github.com/user-attachments/assets/8fdc59af-69a0-413c-a6c5-0151c41a6ee8

## Description

The idea was to build a fully functional dashboard which could be shown at the Hoppenbrouwers factory floors in every location across the Netherlands. To keep the dashboard updated, the software subscribes to different topics on an MQTT-Broker which is being updated by the back-end application. The back-end application is a REST API coded in c# using .NET, placed in a folder named "employee-api". The API uses a local database to store the data regarding personal information about each employee (name, id, location). The application knows each employee state by overlaying his work pattern with the check-in state, the absent registers and the current time. The API is also equipped with an MQTT publisher, which updates the broker when a client tries to alter any kind of data in the local database.

## Project Structure

- **employee-api**: This folder contains the back-end application, a REST API coded in C# using .NET. It manages a local database storing personal information about employees, such as name, ID, and location. The API determines each employee's state by comparing their work pattern with check-in status, absence records, and the current time. It also includes an MQTT publisher to update the broker when data in the local database is altered.

- **employee-front-end**: This folder contains the front-end application, which interacts with the back-end through the MQTT broker to display real-time data on the dashboard.

## Technologies and Tools

- **C# and .NET**: Utilized for developing the back-end REST API.
- **MQTT**: Employed for message brokering between the front-end and back-end applications.
- **Docker**: Used for containerizing the applications and managing dependencies.
- **Swagger UI**: Implemented for API documentation and testing.
- **Unit Tests**: Written to ensure the reliability and correctness of the codebase, using appropriate testing frameworks for both front-end and back-end applications.

## Deployment

To start the application:

1. Clone the repository:
   ```
   git clone https://github.com/Miguel-Pinto99/Employee-API.git
   ```

2. Navigate to the main directory:
   ```
   cd Employee-API
   ```

3. Run the following command in the terminal:
   ```
   docker-compose up
   ```


## Running the Application

### Build the Application
To build the application, use the following command:
   ```
   dotnet build
   ```

### Run Tests
To execute the tests, use the command:
   ```
   dotnet test
   ```

### Run Backend or Frontend Individually
To run either the backend or frontend application individually, use:
   ```
   dotnet run
   ```

### Accessing the Applications
- **API Documentation and Testing**: The Swagger UI for API documentation and testing is available at `http://localhost:5000`.
- **Front-end Application**: The front-end application can be accessed at `http://localhost:7212`.
