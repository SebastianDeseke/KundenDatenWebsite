using KundenDatenWebsite.Database;
namespace KundenDatenWebsite;

public class KundenErstellen
{

    string[] KundenDaten = new string[11];
    string Title;
    string Vorname;
    string Nachname;
    string Firmenname;
    string Adresse;
    string Postleizahl;
    string Telefonnummer;
    string Email;
    string Umsatzsteuernummer;
    string Kundennummer;
    string PreisKategorie;
    double Preis;
    string filePath = "C:\\Users\\Sebastian\\OneDrive - Codelogic\\Documents\\Visual Studio directory\\KundenDatenWebsite\\Kunden\\Kunden.txt";
    public string TestString { get; set; }
    Random rnd = new Random();
    DbConnection dbConnection = new DbConnection();

    public KundenErstellen()
    {

    }

    public string getVorname()
    {
        return Vorname;
    }
    public string getNachname()
    {
        return Nachname;
    }
    public string getFirmenname()
    {
        return Firmenname;
    }
    public string getKundennummer()
    {
        return Kundennummer;
    }
    public void setVorname(string newVorname)
    {
        Vorname = newVorname;
    }
    public void setNachname(string newNachname)
    {
        Nachname = newNachname;
    }
    public void setFirmenname(string newFirmenname)
    {
        Firmenname = newFirmenname;
    }
    public void setKundennummer(string newKundennummer)
    {
        Kundennummer = newKundennummer;
    }

    public void DatenAbfragen()
    {
        //I will posibly need to make a form to work in : https://stackoverflow.com/questions/40562192/windows-form-application-on-visual-studio-code
        //Abfragen der Daten und speichern
        Console.WriteLine(Environment.NewLine + "Bitte Ihre Title angeben");
        Title = Console.ReadLine();
        Console.WriteLine("Bitte Vorname einfüllen");
        Vorname = Console.ReadLine();
        Console.WriteLine("Bitte Familienname einfüllen");
        Nachname = Console.ReadLine();
        Console.WriteLine("Bitte Firmenname einfüllen");
        Firmenname = Console.ReadLine();
        Console.WriteLine("Bitte Adresse einfüllen");
        Adresse = Console.ReadLine();
        Console.WriteLine("Bitte Postleizahl einfüllen");
        Postleizahl = Console.ReadLine();
        Console.WriteLine("Bitte Telefonnummer einfüllen");
        Telefonnummer = Console.ReadLine();
        Console.WriteLine("Bitte Email einfüllen");
        Email = Console.ReadLine();
        Console.WriteLine("Bitte Umsatzsteuernummer einfüllen");
        Umsatzsteuernummer = Console.ReadLine();

        KundenDaten[0] = Title;
        KundenDaten[1] = Vorname;
        KundenDaten[2] = Nachname;
        KundenDaten[3] = Firmenname;
        KundenDaten[4] = Adresse;
        KundenDaten[5] = Postleizahl;
        KundenDaten[6] = Telefonnummer;
        KundenDaten[7] = Email;
        KundenDaten[8] = Umsatzsteuernummer;

        dbConnection.CreateCustomer(KundenummerGenerieren(), Title, Vorname, Nachname, Firmenname, Adresse, Postleizahl, Telefonnummer, Email, Umsatzsteuernummer, preisErmitteln());

    }

        public void DatenSpeichern()
    {
        //Easier System for smaller Data exchanges
        System.IO.File.AppendAllText(filePath, string.Join(',', KundenDaten) + Environment.NewLine);

        //StreamWrtiter Example
        //     using (StreamWriter writer = new StreamWriter(filePath, true))
        // {
        //     writer.WriteLine(string.Join(',', KundenDaten));
        // }

    }

    public string KundenummerGenerieren()
    {
        //Random 3 digit ID is made for the new Customer and Shown to them
        //Must be unique

        bool validID = false;
        Kundennummer = rnd.Next(150, 1000).ToString();
        var kunden = new Dictionary<string, string[]>();
        int rowCount = 0;
        var lines = File.ReadAllLines(filePath);
        rowCount = lines.Count();
        foreach (string line in lines)
        {
            var kundenParts = line.Split(',');
            kunden[kundenParts[10]] = kundenParts;
        }
        while (!validID)
        {
            if (kunden.ContainsKey(Kundennummer))
            {
                Kundennummer = rnd.Next(150 + rowCount, 1000).ToString();
                //could put the code above the if statement, and simply put continue; in the if statement
            }
            else
            {
                validID = true;
            }
        }
        Console.WriteLine("Ihre Kundenummer lautet " + Kundennummer);

        KundenDaten[10] = Kundennummer;
        return Kundennummer;
    }

    public int preisErmitteln()
    {
        //standard price for per minute usage and mehrwertsteuer, that is added if the company is in Germany
        //rethink the idea to extract from the text file, and then we determin the price accordingly
        double standardPreis = 0.25;
        double mehrwertsteuer = 0.19;
        Console.WriteLine("Geben Sie an ob Sie innerhalb Deutschland arbeiten \n1 - Deutschland \n2 -  Österreich \n3 - Niederlande \n4 - Ausland");
        int preisChoice = int.Parse(Console.ReadLine());
        switch (preisChoice)
        {
            case 1:
                //Deutschland
                Preis = (standardPreis * 300) * 1.19;
                Console.WriteLine("Deine aktuelle Preis liegt bei " + Preis + " wobei " + (standardPreis * 300 * mehrwertsteuer) + " als Mehrwertsteuer berechnet wird");
                break;
            case 2:
                //Niederlanden
                Preis = (standardPreis * 300) * 1.21;
                Console.WriteLine("Deine aktuelle Preis liegt bei " + Preis);
                break;
            case 3:
                //Österreich
                Preis = (standardPreis * 300) * 1.20;
                Console.WriteLine("Deine aktuelle Preis liegt bei " + Preis);
                break;
            case 4:
                //Ausland
                Preis = standardPreis * 300;
                Console.WriteLine("Deine aktuelle Preis liegt bei " + Preis);
                break;
            default:
                Console.WriteLine("Deine Angabe passt leider nicht zu unsere aktuelle Triff pläne. Bitte beachten sie dass unsere Standaartpreis " + standardPreis + " mit zukommenden kosten, abhängig wovon aus Sie arbeiten");
                break;
        }

        return preisChoice;
    }

    public string toString()
    {
        return "Hallo " + Vorname + " " + Nachname + ". Wir haben Sie als ansprechpartner für " + Firmenname + "unter den Kundennummer " + Kundennummer;
    }

}