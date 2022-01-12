https://canurek.com/basic-usage-of-kafka-in-an-assessment-project

For running this project you have to run these Docker commands in the directory that includes the docker-compose file.

- docker-compose build
- docker-compose up

Then, you can open the following URL in your browser.

http://localhost:8000/swagger/index.html

And this is sample data for the test;

{ "events": [ { "app": "1231232-321312-12312321-21312", "type": "HOTEL_CREATE", "time": "2020-02-10T13:40:27.650Z", "isSucceeded": true, "meta": { }, "user": { "isAuthenticated": true, "provider": "b2c-internal", "id": 231213, "e-mail": "an@emailaddress.com" }, "attributes": { "hotelId": 4123, "hotelRegion": "Antalya", "hotelName": "Rixos" } } ] }

When you post this data, consumer projects will catch it and show it on the console.

Also, you can run the Unit Test Project on the IDE. That was only created for the Producer project.
