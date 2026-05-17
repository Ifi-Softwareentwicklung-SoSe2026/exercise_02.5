namespace CampusLeihsystem;

public interface ICampusObjekt
{
    string Name { get; }
    uint InventarNummer { get; }
    string GetStatusBericht();
}

public interface IVergleichbar<T>
{
    int VergleicheMit(T anderer);
    bool IstGroesserAls(T anderer);
    bool IstKleinerAls(T anderer);
}

public interface IAusleihbar
{
    bool IstVerfuegbar { get; }
    void Ausleihen(string nutzer);
    void Zurueckgeben();
}

public abstract class Leihobjekt(string name, uint inventarNummer) : ICampusObjekt, IVergleichbar<Leihobjekt>
{
    public string Name { get; } = !string.IsNullOrWhiteSpace(name)
        ? name
        : throw new ArgumentException("Name darf nicht leer sein.", nameof(name));

    public uint InventarNummer { get; } = inventarNummer > 0
        ? inventarNummer
        : throw new ArgumentException("InventarNummer muss größer als 0 sein.", nameof(inventarNummer));

    public abstract string GetStatusBericht();

    public int VergleicheMit(Leihobjekt anderer)
    {
        ArgumentNullException.ThrowIfNull(anderer);
        return InventarNummer.CompareTo(anderer.InventarNummer);
    }

    public bool IstGroesserAls(Leihobjekt anderer) => VergleicheMit(anderer) > 0;

    public bool IstKleinerAls(Leihobjekt anderer) => VergleicheMit(anderer) < 0;
}

public abstract class AusleihbaresLeihobjekt(string name, uint inventarNummer) : Leihobjekt(name, inventarNummer), IAusleihbar
{
    public bool IstVerfuegbar { get; private set; } = true;

    public void Ausleihen(string nutzer)
    {
        if (!IstVerfuegbar)
        {
            throw new InvalidOperationException($"'{Name}' ist bereits ausgeliehen.");
        }

        if (string.IsNullOrWhiteSpace(nutzer))
        {
            throw new ArgumentException("Nutzer darf nicht leer sein.", nameof(nutzer));
        }

        IstVerfuegbar = false;
    }

    public void Zurueckgeben() => IstVerfuegbar = true;
}

public sealed class Buch(string name, uint inventarNummer, string autor) : AusleihbaresLeihobjekt(name, inventarNummer)
{
    public string Autor { get; } = !string.IsNullOrWhiteSpace(autor)
        ? autor
        : throw new ArgumentException("Autor darf nicht leer sein.", nameof(autor));

    public override string GetStatusBericht() => $"Buch {Name} von {Autor}";
}

public sealed class Laptop(string name, uint inventarNummer, string raumNummer) : AusleihbaresLeihobjekt(name, inventarNummer)
{
    public string RaumNummer { get; } = !string.IsNullOrWhiteSpace(raumNummer)
        ? raumNummer
        : throw new ArgumentException("RaumNummer darf nicht leer sein.", nameof(raumNummer));

    public override string GetStatusBericht() => $"Laptop {Name} (Raum {RaumNummer})";
}

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
