# spletna-aplikacija-dostop-do-meritev-senzorjev

Člana ekipe:
63190121 Jan Hribar,
63200260 Peter Savnik

Namen: Upravljalna plošča uporabniku dostopnih oziroma dovoljenih senzorjev, kjer lahko spremlja njihove meritve. Določeni uporabniki lahko dodajajo in upravljajo senzorje, določeni le pregledujejo.

Spremljane entitete: 

    Uporabniki: Uporabniško ime/email, geslo, njegova organizacija - upravitelj, njegova skupina - pravice.

    Senzorji: Serijska številka, lokacija, tip, firmware verzija

        -> influxDB: ime meritev, značke (tag) - serijska številka, vrednost meritve

Uporabljene tehnologije: 

    Baze: mySQL, InfluxDB

    Izris podatkov: Grafana
