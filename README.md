<!--

author:   Volker Göhler
email:    volker.goehler@informatik.tu-freiberg.de
version:  0.0.1
language: de
narrator: Deutsch Female

edit: true
date: 2026-05-13

comment:  Übung Softwareentwicklung 02.5

import: https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md

link:   https://raw.githubusercontent.com/vgoehler/LiaScript_CSS_Provider/refs/heads/main/dist/university.css

tags: [ Sommersemester2026, Softwareentwicklung, Übung02.5]

-->

[![LiaScript Course](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://raw.githubusercontent.com/Ifi-Softwareentwicklung-SoSe2026/exercise_02.5/refs/heads/main/README.md)

#  Aufgabe 02.5

Softwareentwicklung SoSe2026
============================

Bearbeitungszeitraum
====================

*13. Mai - 16. Mai 2026*

## Neue Aufgaben für diese Woche

Diese Zwischeneinheit zieht die **Interface-Aufgaben** aus `exercise_03` vor, damit die Konzepte vor dem Git/GitHub-Teil bearbeitet werden können.

### **📌 Vorbereitung: Projekt aktualisieren**

1. Nutze das bestehende **C#-Konsolenprojekt** `RaumfahrtMission` aus Aufgabe 02.
2. Füge die finalen Klassen aus Aufgabe 02 hinzu (falls noch nicht vorhanden), die Version aus `solutions` im `exercise_02` Repo kann als Ausgangspunkt genutzt werden.
3. Alle Klassen sollen weiterhin im Namespace **`RaumfahrtMission`** liegen.

### **🔌 Aufgabe 1: Interfaces für die Mission**

*Lernziele: Interface-Definition, Implementierung, Polymorphismus*

---

#### **📝 Aufgabenstellung**

Definiere das Interface `IMissionsobjekt`, das alle Objekte im Missionssystem beschreibt.

##### **🔧 Hilfestellungen**

**1. Interface `IMissionsobjekt` definieren**

```text @plantUML
@startuml
interface IMissionsobjekt {
  +Name : string
  +KatalogNummer : uint
  +GetStatusBericht() : string
}
@enduml
```

**2. Interface implementieren**

- Implementiere `IMissionsobjekt` in der abstrakten Klasse `Himmelskoerper`.
- Implementiere `GetStatusBericht()` in den Klassen `Stern`, `Planet` und `Mond`, um einen kurzen Statusbericht zurückzugeben.

  - Für `Stern`: *"Stern [Name] (Spektralklasse [Klasse]) mit Helligkeit [Helligkeit] mag"*
  - Für `Planet`: *"Planet [Name] umkreist Körper [KatalogNummerReferenz] in [Umlaufzeit] Jahren"*
  - Für `Mond`: *"Mond [Name] umkreist Körper [KatalogNummerReferenz] in [Umlaufzeit] Jahren"*

**3. Weiteres Interface `IVergleichbar<T>` definieren (mit Template)**

```text @plantUML
@startuml
interface IVergleichbar<T> {
    +VergleicheMit(T anderer) : int 
    +IstGroesserAls(T anderer) : bool 
    +IstKleinerAls(T anderer) : bool 
}
@enduml
```

- Implementiere `IVergleichbar<Himmelskoerper>` in `Himmelskoerper`, sodass Himmelskörper anhand ihrer `KatalogNummer` verglichen werden können.

<!-- class="lia-callout--note" style="width:100%" -->
> **💡 Bonusaufgabe – Operatorüberladung:**
> Wenn du `IVergleichbar<T>` implementiert hast, kannst du die gleiche Logik auch als überladene Operatoren anbieten.
> Füge dazu in `Himmelskoerper` statische Methoden hinzu, für:
>
> - `<` (Kleiner-als)
> - `>` (Größer-als)
> - `Equals` (Gleichheit)
> - `==` (Gleichheit)
> - `!=` (Ungleichheit)
> - `>=`, `<=` (optional)
> - `<=`, `>=` (optional)
>
> Damit kannst du statt `erde.IstGroesserAls(mond)` direkt `erde > mond` schreiben.
> Die Interface-Methoden bleiben die eigentliche Implementierung, die Operatoren sind nur **syntaktischer Zucker** darüber.

#### ✅ Testaufgabe

```csharp
IMissionsobjekt[] objekte = { sonne, erde, mond };
foreach (var obj in objekte)
{
    Console.WriteLine(obj.GetStatusBericht());
}

// Vergleich
Console.WriteLine(erde.IstGroesserAls(mond)); // false, da KatalogNummer 20001 < 30001
Console.WriteLine(sonne.VergleicheMit(erde));   // negativ, da 10001 < 20001
```

💡 Tipps
====================

- Ein Interface kann nicht instanziiert werden, definiert aber eine Vertragsstruktur.
- Eine Klasse kann mehrere Interfaces implementieren.
- `IComparable<T>` aus der .NET-Bibliothek funktioniert ähnlich wie `IVergleichbar<T>`.

### **🚀 Aufgabe 2: SpaceShip mit Unterklassen über IMissionsobjekt nutzen**

*Lernziele: Interface-Nutzung im Code, Polymorphismus, gemeinsame Verarbeitung unterschiedlicher Objekte*

---

#### **📝 Aufgabenstellung**

Erweitere das Missionssystem um eine neue Basisklasse `SpaceShip`, die ebenfalls das Interface `IMissionsobjekt` aus Aufgabe 1 implementiert.

Die Klasse `SpaceShip` dient als gemeinsame Basisklasse für verschiedene Raumschiffe. Leite davon mindestens zwei konkrete Unterklassen ab, damit sichtbar wird, warum ein Interface in einer Schleife oder einer Hilfsmethode nützlich ist.

##### **🔧 Hilfestellungen**

**1. Abstrakte Klasse `SpaceShip` einführen**

```text @plantUML
@startuml
interface IMissionsobjekt {
  +GetStatusBericht() : string
}

abstract class SpaceShip {
  +Name : string
  +KatalogNummer : uint
  +CrewGroesse : int
  +{abstract} GetStatusBericht() : string
  +ToString() : string
}

IMissionsobjekt <|.. SpaceShip
@enduml
```

- `SpaceShip` soll gemeinsame Eigenschaften aller Raumschiffe kapseln.
- Wie in Aufgabe 1 vorgegeben, gehört `GetStatusBericht()` dabei zum Vertrag von `IMissionsobjekt`.
- `GetStatusBericht()` bleibt abstrakt, damit jede Unterklasse ihren eigenen Bericht formuliert.
- `ToString()` soll in `SpaceShip` eine allgemeine Darstellung liefern, die von den Unterklassen erweitert oder überschrieben werden kann.

**2. Konkrete Unterklassen anlegen**

Leite mindestens zwei Klassen von `SpaceShip` ab, zum Beispiel:

- `CargoShip` mit einer Eigenschaft `LadungInTonnen`
- `ResearchShip` mit einer Eigenschaft `Forschungsgebiet`

Jede Unterklasse soll `GetStatusBericht()` und `ToString()` passend überschreiben.

Ein mögliches Klassendiagramm wäre zum Beispiel:

```text @plantUML
@startuml
interface IMissionsobjekt {
  +GetStatusBericht() : string
}

abstract class SpaceShip {
  +Name : string
  +KatalogNummer : uint
  +CrewGroesse : int
  +{abstract} GetStatusBericht() : string
  +ToString() : string
}

class CargoShip {
  +LadungInTonnen : float
  +GetStatusBericht() : string
  +ToString() : string
}

class ResearchShip {
  +Forschungsgebiet : string
  +GetStatusBericht() : string
  +ToString() : string
}

IMissionsobjekt <|.. SpaceShip
SpaceShip <|-- CargoShip
SpaceShip <|-- ResearchShip
@enduml
```

**3. Ausgaben**

**3.1 Interface gezielt nutzen**

Speichere Himmelskörper und Raumschiffe gemeinsam in einem Array oder in einer Liste vom Typ `IMissionsobjekt`.

So kannst du alle Objekte mit derselben Schleife verarbeiten, obwohl sie aus unterschiedlichen Klassenhierarchien stammen:

```csharp
// sonne, erde und mond wurden bereits in Aufgabe 1 erzeugt
IMissionsobjekt[] objekte = { sonne, erde, mond, cargoShip, researchShip };

foreach (var objekt in objekte)
{
    MissionsReport(objekt);
}
```

**3.2 Hilfsmethode `MissionsReport` für beliebige Missionsobjekte**

Die Methode `MissionsReport`, die ein beliebiges `IMissionsobjekt` entgegennimmt und sowohl die normale Objektausgabe über `ToString()` als auch den `StatusBericht` ausgibt, ist in `Program.cs` bereits vorgegeben:

```csharp
static void MissionsReport(IMissionsobjekt objekt)
{
    Console.WriteLine(objekt.ToString());
    Console.WriteLine(objekt.GetStatusBericht());
}
```

So wird deutlich, dass nicht der konkrete Typ wichtig ist, sondern nur der durch das Interface garantierte Vertrag.

#### ✅ Testaufgabe

```csharp
var cargoShip = new CargoShip("CargoMaster 3000", 50001, 5, 10000.0f);
var researchShip = new ResearchShip("Explorer X", 50002, 10, "Astrophysik");

static void MissionsReport(IMissionsobjekt objekt){
    Console.WriteLine(objekt.ToString());
    Console.WriteLine(objekt.GetStatusBericht());
}

IMissionsobjekt[] schiffe = { cargoShip, researchShip };
foreach (var obj in schiffe.Concat(objekte))
    MissionsReport(obj);
```

💡 Tipps
====================

- Ein Interface beschreibt, **was ein Objekt können muss**, nicht **von welcher Klasse es abstammt**.
- Durch `IMissionsobjekt` können `Planet`, `Mond`, `Stern` und `SpaceShip` gemeinsam verarbeitet werden.
- Die Basisklasse `SpaceShip` bündelt gemeinsame Eigenschaften, die Unterklassen ergänzen die Spezialisierung.
