<!--

author:   Volker Göhler
email:    volker.goehler@informatik.tu-freiberg.de
version:  0.0.3
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

*18. Mai - 25. Mai 2026*

## Neue Aufgaben für diese Woche

In dieser Woche geht es um die Einführung von **Interfaces** in einem neuen **CampusLeihsystem**.

### **📌 Vorbereitung: Neues Projekt anlegen**

1. Erstelle ein neues **C#-Konsolenprojekt** `CampusLeihsystem`.
2. Lege alle Klassen im Namespace **`CampusLeihsystem`** an.

### **🔌 Aufgabe 1: Basis-Interface für Campus-Objekte**

*Lernziele: Interface-Definition, Implementierung, Polymorphismus*

---

#### **📝 Aufgabenstellung**

Definiere das Interface `ICampusObjekt`, das alle Objekte im Leihsystem beschreibt.

##### **🔧 Hilfestellungen**

**1. Interface `ICampusObjekt` definieren**

```text @plantUML
@startuml
interface ICampusObjekt {
  +Name : string
  +InventarNummer : uint
  +GetStatusBericht() : string
}
@enduml
```

**2. Abstrakte Basisklasse und konkrete Klassen**

- Erstelle die abstrakte Klasse `Leihobjekt`, die `ICampusObjekt` implementiert.
- Leite davon mindestens zwei konkrete Klassen ab:
  - `Buch` (z. B. mit `Autor : string`)
  - `Laptop` (z. B. mit `RaumNummer : string`)
- Implementiere `GetStatusBericht()` jeweils passend.
- Nutze konsistente Konstruktoren, z. B.:
  - `Buch(string name, uint inventarNummer, string autor)`
  - `Laptop(string name, uint inventarNummer, string raumNummer)`
- Optional: Ergänze einfache Eingabeprüfungen (z. B. leerer Name, `inventarNummer == 0`).

Beispielhafte Berichte:

- `Buch`: *"Buch [Name] von [Autor]"*
- `Laptop`: *"Laptop [Name] (Raum [RaumNummer])"*

**3. Zweites Interface `IVergleichbar<T>`**

```text @plantUML
@startuml
interface IVergleichbar<T> {
    +VergleicheMit(T anderer) : int
    +IstGroesserAls(T anderer) : bool
    +IstKleinerAls(T anderer) : bool
}
@enduml
```

- Implementiere `IVergleichbar<Leihobjekt>` in `Leihobjekt`.
- Vergleiche Objekte über `InventarNummer`.

💡 Tipps
====================

- Ein Interface beschreibt einen Vertrag, keine konkrete Implementierung.
- Eine Klasse kann mehrere Interfaces implementieren.

### **🚀 Aufgabe 2: Neues Interface `IAusleihbar` im selben System**

*Lernziele: mehrere Interfaces je Klasse, gemeinsame Verarbeitung*

---

#### **📝 Aufgabenstellung**

Definiere ein weiteres Interface `IAusleihbar` und implementiere es in `Buch` und `Laptop`.

##### **🔧 Hilfestellungen**

**1. Interface `IAusleihbar`**

```text @plantUML
@startuml
interface IAusleihbar {
  +IstVerfuegbar : bool
  +Ausleihen(string nutzer) : void
  +Zurueckgeben() : void
}
@enduml
```

**2. Implementierung**

- `Buch` und `Laptop` sollen `IAusleihbar` zusätzlich zu `ICampusObjekt` implementieren.
- Bei `Ausleihen(...)` wird `IstVerfuegbar` auf `false` gesetzt.
- Bei `Zurueckgeben()` wird `IstVerfuegbar` auf `true` gesetzt.

**3. Interface-Nutzung in einer Hilfsmethode**

```csharp
static void AusgabeLeihstatus(IAusleihbar objekt)
{
    Console.WriteLine($"Verfügbar: {objekt.IstVerfuegbar}");
}
```

💡 Tipps
====================

- Interfaces machen Klassen aus unterschiedlichen Bereichen gemeinsam verarbeitbar.
- `ICampusObjekt` beschreibt den Katalog-Vertrag.
- `IAusleihbar` beschreibt den Leihvorgang-Vertrag.
