using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KundenDatenWebsite.Pages.Kunden;

public class DeleteModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string KundeID { get; set; }
    private readonly ILogger<DeleteModel> _logger;
    private readonly DbConnection _db;
    public string Title { get; set; }
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public string Firmenname { get; set; }
    public string Adresse { get; set; }
    public string Postleizahl { get; set; }
    public string Telefonnummer { get; set; }
    public string Email { get; set; }
    public string Umsatzsteuernummer { get; set; }
    public string PreisKategorie { get; set; }

    public DeleteModel(ILogger<DeleteModel> logger, DbConnection db)
    {
        _logger = logger;
        _db = db;
    }

    public void OnGet()
    {
        _db.Connect();
        var Customer = _db.ReadCustomer(KundeID);
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

    public void OnPost()
    {
        _db.DeleteCustomer(KundeID);
        Response.Redirect("/Privacy");
    }
}
