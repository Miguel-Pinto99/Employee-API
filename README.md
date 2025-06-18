# Employee API

## Deployment

To run the application, use the following command:

```
docker compose up
```

For API documentation and testing, the application is structured to run the Swagger UI on `http://localhost:5000`. The front-end application is accessible at `http://localhost:7212`.


## Description

https://github.com/user-attachments/assets/8fdc59af-69a0-413c-a6c5-0151c41a6ee8

Repository which contains my internship project at Hoppenbrouwers. The premise of this project was to connect a front-end application with a back-end application through an MQTT broker. The idea was to build a fully functional dashboard which could be shown at the Hoppenbrouwers factory floors in every location across the Netherlands. To keep the dashboard updated, the software subscribes to different topics on an MQTT-Broker which is being updated by the back-end application. The back-end application is a REST API coded in c# using .NET, placed in a folder named "employee-api". The API uses a local database to store the data regarding personal information about each employee (name, id, location). The application knows each employee state by overlaying his work pattern with the check-in state, the absent registers and the current time. The API is also equipped with an MQTT publisher, which updates the broker when a client tries to alter any kind of data in the local database.
