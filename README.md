# Fonákolós játék
A "Bevezetés a Microsoft .NET 3.5 framework és Windows Communication Foundation-be" nevu tárgy gyakorlati jegyéhez szükséges, .NET -es grafikus (WPF keretrendszerben írt) játék elkészítése.
## A játék bemutatása

https://hu.wikipedia.org/wiki/Fon%C3%A1kol%C3%B3s

https://en.wikipedia.org/wiki/Reversi

## A program megvalósítása
* AI ellen lehessen játszani
* Kezdő játékos véletlenszerűen választva
* AI (Elemzés):
    * Először oda rakjon korongot, ahol a legtöbbet változtatja át (UNIT Tesztet írni hozzá pár bementre)
    * Véletlenszerűen helyezzen le egy korongot
 * Egymás ellen is lehessen játszani ugyanazon a gépen, ugyanabban az alkalmazásban 
##	Játékmenet
* Belépéskor kérjen nevet, majd ahhoz mentse az eredményeket
* Eredményjelző:
* Saját pontok
* AI pontjai
* Eltelt idő
##	Játék vége
* Mindenkori eredménylista (nyert – vesztett)
* Tárolja le az eredményt
* Adatok tárolása: Json, XML vagy adatbázis
