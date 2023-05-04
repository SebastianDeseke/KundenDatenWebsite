using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KundenDatenWebsite.Pages.Kunden;

public class NeuModel : PageModel
{
    private readonly ILogger<NeuModel> _logger;

    public string KundenID { get; set; }
    private readonly DbConnection _db;
    private readonly KundenErstellen _kunde;


    public NeuModel(ILogger<NeuModel> logger, DbConnection db)
    {
        _logger = logger;
        _db = db;
        _kunde = new KundenErstellen();
    }

    public int convertPreiskategory(string Land)
    {
        int preisKategorie;
        switch (Land)
        {
            case "Deutschland":
                preisKategorie = 1;
                break;
            case "Niederlande":
                preisKategorie = 2;
                break;
            case "Österreich":
                preisKategorie = 3;
                break;
            case "Ausland":
                preisKategorie = 4;
                break;
            default:
                //default is Unbekannt
                preisKategorie = 0;
                break;
        }
        return preisKategorie;
    }

    public void OnGet()
    {

    }

    public void OnPost()
    {
        //Take the information that is inputed in the form
        //and save it in the database
        _db.CreateCustomer(_kunde.KundenummerGenerieren(), Request.Form["Title"], Request.Form["Vorname"], Request.Form["Nachname"], Request.Form["Firmenname"], Request.Form["Adresse"], Request.Form["Postleizahl"], Request.Form["Telefonnummer"], Request.Form["Email"], Request.Form["Umsatzsteuernummer"], convertPreiskategory(Request.Form["Land"]));

    }
}
