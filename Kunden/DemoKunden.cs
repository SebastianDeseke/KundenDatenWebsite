using KundenDatenWebsite.Database;
namespace KundenDatenWebsite;

public class DemoKunden
{
    string[] DemoKundenDaten = new string[8];
    string DemoKundenname;
    string DemoKundenadresse;
    string DemoKundenemail;
    string DemoKundenTelefonnummer;
    string DemoKundenGeburtsdatum;
    string DemoKundenTitle;
    DateTime startTime;
    string DemoKundenID;
    string filePath = "C:\\Users\\Sebastian\\OneDrive - Codelogic\\Documents\\Visual Studio directory\\KundenDatenWebsite\\Kunden\\DemoKunden.txt";
    int rowCount = 0;


    public Dictionary<string, string[]> DemoDictionary()
    {
        //Declare a new dictionary
        var demoKunden = new Dictionary<string, string[]>();
        var lines = File.ReadAllLines(filePath);
        rowCount = lines.Count();

        foreach (string line in lines)
        {
            var demoKundenParts = line.Split(',');
            demoKunden[demoKundenParts[7]] = demoKundenParts;
        }

        return demoKunden;
    }

    public void DemoDatenBearbeiten(string verification)
    {
        //Bearbeiten der Demo Daten in der Datei
        //DemoID is validated in main
        var demoKunde = DemoDictionary();
        DemoKundeDatenAbrufen(verification);
        Console.WriteLine("Welche Information möchten Sie bearbeiten? Bitte folgende Auswahl benutzen: \n0 - Title \n1 - Vorname \n2 - Nachname \n3 - Firmenname \n4 - Adresse \n5 - Postleizahl \n6 - Telefonnummer \n7 - Email \n8 - Umsatzsteuernummer");
        var switchInput = int.Parse(Console.ReadLine());

        string[] demoKundeData = demoKunde[verification];
        //with this switch we ask what part of the Kunde Array they want to edit, and replace that part in our Array with the new input
        switch (switchInput)
        {
            case 1:
                Console.WriteLine("Bitte Geben Sie eine Neue Name des Ansprechpartners (Volle Name bitte)");
                string newVollename = Console.ReadLine();
                demoKunde[verification][switchInput] = newVollename;
                break;
            case 2:
                Console.WriteLine("Bitte Geben Sie eine Neue Title des Ansprechpartners");
                string newTitle = Console.ReadLine();
                demoKunde[verification][switchInput] = newTitle;
                break;
            case 3:
                Console.WriteLine("Bitte Geben Sie eine Neue Adresse an");
                string newAdresse = Console.ReadLine();
                demoKunde[verification][switchInput] = newAdresse;
                break;
            case 4:
                Console.WriteLine("Bitte Geben Sie eine Neue Telefonnummer an");
                string newTelefonnummer = Console.ReadLine();
                demoKunde[verification][switchInput] = newTelefonnummer;
                break;
            case 5:
                Console.WriteLine("Bitte Geben Sie eine Neue Email an");
                string newEmail = Console.ReadLine();
                demoKunde[verification][switchInput] = newEmail;
                break;
            default:
                Console.WriteLine("Deine angegeben position ist leider falsch. Bitte versuchen Sie es erneut");
                break;
        }
    }

    public void DemoDatenAbfragen()
    {
        //Abfragen der Daten aus der Datei
        Console.WriteLine(Environment.NewLine + "Bitte Volle name einfüllen");
        DemoKundenname = Console.ReadLine();
        Console.WriteLine("Bitte Title Angeben");
        DemoKundenTitle = Console.ReadLine();
        Console.WriteLine("Bitte Adresse einfüllen");
        DemoKundenadresse = Console.ReadLine();
        Console.WriteLine("Bitte Email einfüllen");
        DemoKundenemail = Console.ReadLine();
        Console.WriteLine("Bitte Telefonnummer einfüllen");
        DemoKundenTelefonnummer = Console.ReadLine();
        Console.WriteLine("Bitte Geburtsdatum einfüllen");
        DemoKundenGeburtsdatum = Console.ReadLine();
        startTime = DateTime.Now;


        DemoKundenDaten[0] = DemoKundenname;
        DemoKundenDaten[1] = DemoKundenTitle;
        DemoKundenDaten[2] = DemoKundenadresse;
        DemoKundenDaten[3] = DemoKundenemail;
        DemoKundenDaten[4] = DemoKundenTelefonnummer;
        DemoKundenDaten[5] = DemoKundenGeburtsdatum;
        DemoKundenDaten[6] = startTime.ToString("dd/MM/yyyy");

    }

    public void DemoKundeDatenSpeichern()
    {
        //Speichert die Daten in eine Datei
        System.IO.File.AppendAllText(filePath, string.Join(',', DemoKundenDaten + Environment.NewLine));

        //StreamWrtiter Example
        //     using (StreamWriter writer = new StreamWriter(filePath, true))
        // {
        //     writer.WriteLine(string.Join(',', DemoKundenDaten));
        // }
    }

    public void DemoKundeDatenAbrufen(string demoID)
    {
        //Abrufen der Daten aus der Datei
        var demoKunde = DemoDictionary();
        //DemoID is validated in main
        Console.WriteLine(string.Join(' ', demoKunde[demoID]));

    }

    public string GenerateDemoID()
    {
        //Generiert eine Demo Kunden ID
        var lines = File.ReadAllLines(filePath);
        DemoKundenID = (lines.Count() + 100).ToString();
        Console.WriteLine("Ihre Demo Kunden ID lautet: {DemoKundenID}");
        DemoKundenDaten[7] = DemoKundenID;
        //returns the ID, for use in dbConnections
        return DemoKundenID;
    }

    public void DemoDurationCalculator(string demoID)
    {
        //Berechnet die Dauer der Demo
        //Duration is never allowed to be longer than 3 days
        var demoKunde = DemoDictionary();

        startTime = DateTime.Parse(demoKunde[demoID][6]);
        DateTime endTime = startTime.AddDays(3);
        TimeSpan timeSpan = endTime - startTime;

        if (demoKunde.ContainsKey(demoID))
        {
            DateTime now = DateTime.Now;
            if (now >= endTime || timeSpan.Days > 3)
            {
                Console.WriteLine("Ihr Demo Zeitraum ist abgelaufen. bitte kontaktieren sie Support oder Ihre Account Manager für weitere Informationen");
            }
            else
            {
                Console.WriteLine("Die Demo endet am: {0}", endTime);
            }
        }
        else
        {
            Console.WriteLine("Die Demo ID ist nicht vorhanden. Ihre Zeit ist wahrscheinlich abgelaufen. Bitte kontaktieren Sie Support oder Ihre Account Manager für weitere Informationen");
        }
    }

    public void DemoKundeDatenLoeschen()
    {
        //Löscht die Daten aus der Datei
        //Goes through the file and deletes the lines, where the 
        //Demo time is over 3 days
        var demoKunde = DemoDictionary();

        for (int i = 0; i < rowCount; i++)
        {
            //Look for the Key in the dictionary
            //at element i, which will increment
            string demoID = demoKunde.Keys.ElementAt(i);
            startTime = DateTime.Parse(demoKunde[demoID][6]);
            DateTime endTime = startTime.AddDays(3);
            TimeSpan timeSpan = endTime - startTime;

            if (DateTime.Now >= endTime || timeSpan.Days > 3)
            {
                demoKunde.Remove(demoID);
                //now save the new dictionary to the file
                File.WriteAllText(filePath, string.Join(',', demoKunde) + Environment.NewLine);
            }
        }

    }

    public void DemoIDchecker(string verification)
    {
        //Checks if the Demo ID is in the file
        var demoKunde = DemoDictionary();
        bool validInpit = false;
        while (!validInpit)
        {
            Console.WriteLine("Bitte geben Sie die Demo ID ein");
            if (demoKunde.ContainsKey(verification))
            {
                validInpit = true;
            }
            else
            {
                Console.WriteLine(Environment.NewLine + "Die Demo ID ist nicht vorhanden. Bitte Nochmal Demo ID angeben");
            }
        }

    }
}