namespace CampusLeihsystem;

public static class Program
{
    public static void Main()
    {
        var buch = new Buch("Clean Code", 1001, "Robert C. Martin");
        var laptop = new Laptop("ThinkPad T14", 2001, "B-201");

        IAusleihbar[] leihObjekte = [buch, laptop];
        ICampusObjekt[] campusObjekte = [buch, laptop];

        foreach (var item in leihObjekte)
        {
            AusgabeLeihstatus(item);
            item.Ausleihen("Max Mustermann");
            AusgabeLeihstatus(item);
        }

        foreach (var item in campusObjekte)
        {
            Console.WriteLine(item.GetStatusBericht());
        }

        Console.WriteLine(buch.IstKleinerAls(laptop));
        Console.WriteLine(laptop.VergleicheMit(buch));
    }

    private static void AusgabeLeihstatus(IAusleihbar objekt)
    {
        Console.WriteLine($"Verfügbar: {objekt.IstVerfuegbar}");
    }
}
