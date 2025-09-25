# 🎲 BlackJack Console App

En simpel konsolbaseret implementering af Blackjack skrevet i C#.
Projektet er struktureret i klasser, der matcher de vigtigste koncepter i spillet (kort, kortbunke, hånd, spiller og dealer).

## 📌 Funktioner

* Klassisk Blackjack med spiller mod dealer
* Kort blandes med Fisher-Yates shuffle
* Dynamisk beregning af håndens værdi (inkl. esset som 1 eller 11)
* Simpel konsolbrugerflade

## 🏗️ Projektstruktur
<pre>BlackJackMachine/
│
├── Program.cs        # Spillets entrypoint
├── Kort.cs           # Repræsenterer et kort (kulør og rang)
├── Kortbunke.cs      # Kortbunke med blanding og træk
├── Hånd.cs           # Pointberegning af kort på hånden
├── Spiller.cs        # Håndtere spillere og hvilke kort der er på
└── Spil.cs           # Spillets flow og logik dertil</pre>


## ▶️ Installation og Kørsel

**1. Klon repository og gå til mappen hvor der klones til:**
```
git clone https://github.com/SaneStreet/BlackJackMachine.git
cd BlackJackMachine
```
**2. Åbn og kør i VSCode eller kør i din favorit CLI:**
```
dotnet run
```
**3. Følg instruktionerne i konsollen**

## 🎮 Spilregler (kort fortalt)

* Spilleren starter med to kort, ligesom dealeren.
* Spilleren kan vælge at Hit (tage et kort) eller Stand (stå).
* Dealeren trækker kort indtil de har mindst 17 point.
* Den, der kommer tættest på 21 uden at gå over, vinder.

Mere uddybende regler i selve spillet.

## 🔮 Mulige fremtidige udvidelser

* Tilføje saldo og betting-system
* Understøtte flere spillere
* Bedre konsol-UI med ASCII-grafik af kort
* Gemme resultater/statistik
