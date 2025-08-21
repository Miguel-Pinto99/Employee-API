# Employee API

## Context

This repository highlights the work accomplished during my internship, where I developed an application
to connect a front-end and a back-end system through an MQTT broker. The main goal was to build a fully
functional dashboard for the Hoppenbrouwers factory floors across the Netherlands.
The dashboard remains updated by subscribing to various topics on an MQTT broker, which are continuously
refreshed by the back-end application.

https://github.com/user-attachments/assets/5f8edf18-4133-4907-b148-7e4e3e9877b7

## Project Structure

- **employee-api**: This folder contains the back-end application, a REST API coded in C# using .NET. It manages a local database storing personal information about employees, such as name, ID, and location. The API determines each employee's state by comparing their work pattern with check-in status, absence records, and the current time. It also includes an MQTT publisher to update the broker when data in the local database is altered.

- **employee-front-end**: This folder contains the front-end application, which interacts with the back-end through the MQTT broker to display real-time data on the dashboard.

## Technologies and Tools

- **C# and .NET 8.0**: Utilized for developing the back-end REST API.
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
- **API Documentation and Testing**: The Swagger UI for API documentation and testing is available at `http://localhost:5039/swagger/index.html`.
- **Front-end Application**: The front-end application can be accessed at `http://localhost:5241`.
