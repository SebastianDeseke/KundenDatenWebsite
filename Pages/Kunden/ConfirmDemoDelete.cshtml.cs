using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KundenDatenWebsite.Pages.Kunden;

public class DemoDeleteModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string DemoKundenID { get; set; }
    private readonly ILogger<DemoDeleteModel> _logger;
    private readonly DbConnection _db;
    public string Title { get; set; }
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public string Adresse { get; set; }
    public string Email { get; set; }
    public string Telefonnummer { get; set; }
    public string Geburtsdatum { get; set; }
    public string startTime { get; set; }


    public DemoDeleteModel(ILogger<DemoDeleteModel> logger, DbConnection db)
    {
        _logger = logger;
        _db = db;
    }

    public void OnGet()
    {
        _db.Connect();
        var demoCustomer = _db.ReadDemoCustomer(DemoKundenID);
        Title = demoCustomer["DemoKundenTitle"];
        Vorname = demoCustomer["DemoKundenVorname"];
        Nachname = demoCustomer["DemoKundenName"];
        Adresse = demoCustomer["DemoKundenadresse"];
        Email = demoCustomer["DemoKundenemail"];
        Telefonnummer = demoCustomer["DemoKundenTelefonnummer"];
        Geburtsdatum = demoCustomer["DemoKundenGeburtsdatum"];
        startTime = demoCustomer["startTime"];
    }

    public void OnPost()
    {
        _db.DeleteSelectedDemoCustomer(DemoKundenID);
        Response.Redirect("/Privacy");
    }
}
