using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RaumfahrtMission;

class Program
{
    static void Main(string[] args)
    {
        // Aufgabe 1: Himmelskörper erstellen (aus Aufgabe 02)
        Stern sonne = new Stern("Sonne", 10001, 'G', -26.74f);
        Planet erde = new Planet("Erde", 20001, 1.0f, 10001);
        Mond mond = new Mond("Mond", 30001, 0.0748f, 20001);
        var halley = new Komet(
            name: "Halley'scher Komet",
            katalogNummer: 40001,
            umlaufzeit: 76.0f,
            katalogNummerReferenz: 10001
        );

        // Aufgabe 1: Interfaces testen
        IMissionsobjekt[] objekte = { sonne, erde, mond, halley };
        foreach (var obj in objekte)
        {
            Console.WriteLine(obj.GetStatusBericht());
        }
        Console.WriteLine(erde.IstGroesserAls(mond));
        Console.WriteLine(sonne.VergleicheMit(erde));//

        // Aufgabe 2: Raumschiffe
        var cargoShip = new CargoShip(
            name: "CargoMaster 3000",
            katalogNummer: 50001,
            crewGroesse: 5,
            ladungInTonnen: 10000.0f
        );
        var researchShip = new ResearchShip(
            name: "Explorer X",
            katalogNummer: 50002,
            crewGroesse: 10,
            forschungsGebiet: "Astrophysik"
        );
        IMissionsobjekt[] schiffe = { cargoShip, researchShip };
        foreach (var obj in schiffe.Concat(objekte))
        {
            MissionsReport(obj);
        }
    }
    static void MissionsReport(IMissionsobjekt objekt)
    {
        Console.WriteLine("Missionsbericht:");
        Console.WriteLine("--------------------");
        Console.WriteLine(objekt.ToString());
        Console.WriteLine(objekt.GetStatusBericht());
        Console.WriteLine("--------------------");
    }
}
