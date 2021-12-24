Docker compose dosyasının olduğu klasörde 

- docker-compose build
- docker-compose up

komutlarının çalıştırılmasıyla; bütün proje ayağa kalkacaktır.

Sonrasında;

http://localhost:8000/swagger/index.html

adresi browserda açılarak; örnek test datası gönderimi sağlanabilir.

{
    "events": [
        {
            "app": "1231232-321312-12312321-21312",
            "type": "HOTEL_CREATE",
            "time": "2020-02-10T13:40:27.650Z",
            "isSucceeded": true,
            "meta": {
            },
            "user": {
                "isAuthenticated": true,
                "provider": "b2c-internal",
                "id": 231213,
                "e-mail": "eser.ozvataf@setur.com"
            },
            "attributes": {
                "hotelId": 4123,
                "hotelRegion": "Antalya",
                "hotelName": "Rixos"
            }
        }
    ]
}

bu aşamada örnek olarak yazılan consumer projeleri de; bu datayı yakalayacak ve console'a yazacaktır.

Ayrıca IDE üzerinden sadece Producer için yazdığım Unit Test'de run edilebilir.

