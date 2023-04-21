using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KundenDatenWebsite.Pages.Kunden;

public class DetailsModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string KundeID {get; set;}
    private readonly ILogger<DetailsModel> _logger;
    public string Title {get; set;}
    public string Vorname {get; set;}
    public string Nachname {get; set;}
    public string Firmenname {get; set;}
    public string Adresse {get; set;}
    public string Postleizahl {get; set;}
    public string Telefonnummer {get; set;}
    public string Email {get; set;}
    public string Umsatzsteuernummer {get; set;}
    public string PreisKategorie {get; set;}


    public DetailsModel(ILogger<DetailsModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

        DbConnection dbConnection = new DbConnection();
        dbConnection.Connect();
        var Customer = dbConnection.ReadCustomer(KundeID);
        Title = Customer["AnsprechTitle"];
        Vorname = Customer["AnsprechVorname"];
        Nachname = Customer["AnsprechNachname"];
        Firmenname = Customer["Firmenname"];
        Adresse = Customer["Adresse"];
        Postleizahl = Customer["Postleizahl"];
        Telefonnummer = Customer["Telefonnummer"];
        Email = Customer["Email"];
        Umsatzsteuernummer = Customer["Umsatzsteuernummer"];
        PreisKategorie = Customer["PreisKategorie"];

    }
}
