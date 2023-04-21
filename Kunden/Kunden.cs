using KundenDatenWebsite.Database;
namespace KundenDatenWebsite.Database;

public class KundenDaten
{

    public string Vorname;
    string Nachname;
    string Firmenname;
    int Kundennummer;
    string RechnungBetrag;
    string filePath = "C:\\Users\\Sebastian\\OneDrive - Codelogic\\Documents\\Visual Studio directory\\KundenDatenWebsite\\Kunden\\Kunden.txt";
    int rowCount = 0;
    KundenErstellen erstellteKunde = new KundenErstellen();
    DbConnection dbConnection = new DbConnection();

    public Dictionary<string, string[]> KundenDictionary()
    {

        var kunden = new Dictionary<string, string[]>();
        var lines = File.ReadAllLines(filePath);
        //row count is determined in this line, so whenever the Dictionary 
        //is called, the row count is updated anew
        rowCount = lines.Count();
        foreach (string line in lines)
        {
            var kundenParts = line.Split(',');
            kunden[kundenParts[10]] = kundenParts;
        }

        return kunden;
    }

    public void KundenDatenBearbeiten(string kundenID)
    {
        //Same method to sort and find the items form the file, namely dictionary
        //Also the same verification way, only now we have to figuer out how to replace the information
        //and rewrite it in the correct position without overwriting the current data
        var kunden = KundenDictionary();

        //first we present the existing data, then we ask the user to input what part of the info he wants to edit
        KundenDatenAbrufen(kundenID);
        Console.WriteLine("Welche Information möchten Sie bearbeiten? Bitte folgende Auswahl benutzen: \n0 - Title \n1 - Vorname \n2 - Nachname \n3 - Firmenname \n4 - Adresse \n5 - Postleizahl \n6 - Telefonnummer \n7 - Email \n8 - Umsatzsteuernummer");
        var switchInput = int.Parse(Console.ReadLine());
        //save the Dictionary input from the key (Kunden Nummer) a.k.a wherever this Customer ID is, save the info in this array
        string[] customerData = kunden[kundenID];
        //with this switch we ask what part of the Kunde Array they want to edit, and replace that part in our Array with the new input
        switch (switchInput)
        {
            case 0:
                Console.WriteLine("Bitte Geben Sie eine Neue Title des Ansprechpartners");
                string newTitle = Console.ReadLine();
                kunden[kundenID][switchInput] = newTitle;
                dbConnection.UpdateCustomer("AnsprechTitle", newTitle, kundenID);
                break;
            case 1:
                Console.WriteLine("Bitte Geben Sie eine Neue Vorname des Ansprechpartners");
                string newName = Console.ReadLine();
                kunden[kundenID][switchInput] = newName;
                dbConnection.UpdateCustomer("AnsprechVorname", newName, kundenID);
                break;
            case 2:
                Console.WriteLine("Bitte Geben Sie eine Neue Nachname des Ansprechpartners");
                string newSurname = Console.ReadLine();
                kunden[kundenID][switchInput] = newSurname;
                dbConnection.UpdateCustomer("AnsprechNachname", newSurname, kundenID);
                break;
            case 3:
                Console.WriteLine("Bitte Geben Sie eine Neue Firmenname");
                string newFirmenname = Console.ReadLine();
                kunden[kundenID][switchInput] = newFirmenname;
                dbConnection.UpdateCustomer("Firmenname", newFirmenname, kundenID);
                break;
            case 4:
                Console.WriteLine("Bitte Geben Sie eine Neue Adresse");
                string newAdresse = Console.ReadLine();
                kunden[kundenID][switchInput] = newAdresse;
                dbConnection.UpdateCustomer("Adresse", newAdresse, kundenID);
                break;
            case 5:
                Console.WriteLine("Bitte Geben Sie eine Neue Postleizahl");
                string newPostleizahl = Console.ReadLine();
                kunden[kundenID][switchInput] = newPostleizahl;
                dbConnection.UpdateCustomer("Postleizahl", newPostleizahl, kundenID);
                break;
            case 6:
                Console.WriteLine("Bitte Geben Sie eine Neue Telefonnummer");
                string newTelefonnummer = Console.ReadLine();
                kunden[kundenID][switchInput] = newTelefonnummer;
                dbConnection.UpdateCustomer("Telefonnummer", newTelefonnummer, kundenID);
                break;
            case 7:
                Console.WriteLine("Bitte Geben Sie eine Neue Email");
                string newEmail = Console.ReadLine();
                kunden[kundenID][switchInput] = newEmail;
                dbConnection.UpdateCustomer("Email", newEmail, kundenID);
                break;
            case 8:
                Console.WriteLine("Bitte Geben Sie eine Neue Umsatzsteuernummer");
                string newUmsatzsteuernummer = Console.ReadLine();
                kunden[kundenID][switchInput] = newUmsatzsteuernummer;
                dbConnection.UpdateCustomer("Umsatzsteuernummer", newUmsatzsteuernummer, kundenID);
                break;
            default:
                Console.WriteLine("Deine angegeben position war leider falsch. Bitte Geben Sie es erneut ein. {0}");
                break;
        }

        //Rewriting the data here. I chose to use streamwriter, 
        //because it is a efficient way to write to a file
        //and I wanted to try something different
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var item in kunden)
            {
                writer.WriteLine(string.Join(",", item.Value));
            }
        }

    }

    public void KundenDatenAbrufen(string kundeID)
    {
        //initial validation if user wants to edit data in main
        // string tempKundenArr[] = System.IO.File.ReadAllLines(filePath);
        var kunden = KundenDictionary();
        //Chat GTP suggestion, too complicated though
        //List<string[]> lines = File.ReadAllLines(filePath).Select(line => line.Split(',')).ToList();

        //verification is done in the main, so we can just call the dictionary here
        Console.WriteLine("Die Daten werden geladen...");
        Console.WriteLine(string.Join(' ' + Environment.NewLine, kunden[kundeID]));
    }

    public void preisAnzeigen(int preisChoice)
    {
        //it would be smart to save the input in my text file, so that the price can be retrieved via the string[] position, instead of asking for it every time
        double Preis;
        double standardPreis = 0.25;
        switch (preisChoice)
        {
            case 1:
                //Deutschland
                Preis = (standardPreis * 300) * 1.19;
                Console.WriteLine("Deine aktuelle Preis liegt bei " + Preis + " wobei " + (standardPreis * 300 * 0.19) + " als Mehrwertsteuer berechnet wird");
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
    }

    public void validityChecker(char input)
    {
        //this method is to check if the user input is valid
        //if the user input is not valid, then the program will ask the user to input again
        //if the user input is valid, then the program will continue
        //this method is used in the method "KundenDatenBearbeiten"
        //this method is used in the method "KundenDatenAbrufen"
        bool validInput = false;
        while (!validInput)
        {
            //validInput = char.TryParse(line.Trim().ToLower().FirstOrDefault(), out input);
            if (input == 'y' || input == 'n')
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine(Environment.NewLine + "Ungültige Eingabe. Bitte geben Sie entweder y oder n ein.");
            }
        }
    }

    public void KundenIDchecker(string verification)
    {
        //this method is to check if the user input is valid
        //if the user input is not valid, then the program will ask the user to input again
        //if the user input is valid, then the program will continue
        //this method is used in the method "KundenDatenBearbeiten"
        //this method is used in the method "KundenDatenAbrufen"
        //this method is used in the method "preisAnzeigen"
        var kunden = KundenDictionary();
        bool validInput = false;
        while (!validInput)
        {
            if (kunden.ContainsKey(verification))
            {
                validInput = true;
            }
            else
            {
                Console.WriteLine(Environment.NewLine + "Oops, die angegebene Kundennummer ist nicht bei uns vorhanden. Bitte geben Sie erneut Ihre Kundennummer ein.");
            }
        }
    }

}
