Sistem za pregled senzorjev

Člana ekipe:
Jan Hribar 63190121,
Peter Savnik 63200260

Namen: Upravljalna plošča, ki uporabniku predstavlja dovoljene senzorjev, kjer lahko spremlja njihove meritve.

Dostop: http://213.143.69.216/

Delovanje: Sistem se postavi iz strani odjemalca, npr. podjetje Atlantis. Uporabniki osnovno lahko dobijo 4 vloge; Admin, Manager, Staff in User. Te vloge se lahko spremenijo glede na notranjo hierarhijo podjetja. Osnovni uporabnik ima le dostop do pregleda senzorjev, ki jih mu je odobril admin ali menedžer. Uporabnik lahko za dovoljen senzor le pogleda podrobnosti, kjer vidi lokacijo, ime, tip, serijsko številko, verzijo firmwara, glede na serijsko številko pa se prikaže vmesnik Grafane, ki prikaže podatke senzorja iz influxDB baze. Admin lahko vidi/dodaja/spreminja vse podatke.

![image](https://user-images.githubusercontent.com/15855414/148589844-59eb7eca-a9de-4f2b-a552-a621ee750122.png)

![image](https://user-images.githubusercontent.com/15855414/148590128-33a5ac0c-4632-4ddd-9345-d0b1c0a00438.png)


Android aplikacija pridobi podatke iz baze preko API, ki zahteva splošno avtentikacijo. Uporabnik se v mobilno aplikacijo prijavi z istim računom kot na spletno aplikacijo. Iz tam vidi orizoma poišče senzor s povezavo na stran grafane z grafom podatkov senzorja.

![image](https://user-images.githubusercontent.com/15855414/148661255-962f4c18-b5a7-48e8-89a4-425c4c42299d.png)
![image](https://user-images.githubusercontent.com/15855414/148661260-4bd1f42a-48f4-4efe-8106-ed4902e79514.png)
![image](https://user-images.githubusercontent.com/15855414/148661263-7b946800-8e2d-4d02-90e4-dd5cae51fe0e.png)


Naloge glede na člana:
Jan Hribar: spletna aplikacija, mySQL, spletna storitev, API, poročilo
Peter Savnik: spletna aplikacija, mySQL, InfuxDB, Grafana, mobilna aplikacija

![image](https://user-images.githubusercontent.com/15855414/148661923-28e6ba5b-ceef-4d58-a2b9-0f0957da6193.png)




Uporabljene tehnologije: 

    Ogrodje C# dotnet 6.0
    
    Baze: mySQL, InfluxDB
    
    Izris podatkov: Grafana
