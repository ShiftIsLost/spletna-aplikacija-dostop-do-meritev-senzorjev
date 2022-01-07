Sistem za pregled senzorjev

Člana ekipe:
Jan Hribar 63190121,
Peter Savnik 63200260

Namen: Upravljalna plošča, ki uporabniku predstavlja dovoljene senzorjev, kjer lahko spremlja njihove meritve.

Delovanje: Sistem se postavi iz strani odjemalca, npr. podjetje Atlantis. Uporabniki osnovno lahko dobijo 4 vloge; Admin, Manager, Staff in User. Te vloge se lahko spremenijo glede na notranjo hierarhijo podjetja. Osnovni uporabnik ima le dostop do pregleda senzorjev, ki jih mu je odobril admin ali menedžer. Uporabnik lahko za dovoljen senzor le pogleda podrobnosti, kjer vidi lokacijo, ime, tip, serijsko številko, verzijo firmwara, glede na serijsko številko pa se prikaže vmesnik Grafane, ki prikaže podatke senzorja iz influxDB baze. Admin lahko vidi/dodaja/spreminja vse podatke.

![image](https://user-images.githubusercontent.com/15855414/148589844-59eb7eca-a9de-4f2b-a552-a621ee750122.png)

![image](https://user-images.githubusercontent.com/15855414/148590128-33a5ac0c-4632-4ddd-9345-d0b1c0a00438.png)


Android aplikacija pridobi podatke iz baze preko API, ki zahteva splošno avtentikacijo. //kaj vidi user na aplikaciji

//screenshot mobilne app

Naloge glede na člana:
Jan Hribar: spletna aplikacija, mySQL, spletna storitev, API, poročilo
Peter Savnik: spletna aplikacija, mySQL, InfuxDB, Grafana, mobilna aplikacija

![image](https://user-images.githubusercontent.com/15855414/148589643-da68c4b9-e922-4278-a52e-e899898cdd2b.png)



Uporabljene tehnologije: 
    Ogrodje C# dotnet 6.0
    Baze: mySQL, InfluxDB
    Izris podatkov: Grafana
