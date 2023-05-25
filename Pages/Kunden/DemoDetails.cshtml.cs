using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KundenDatenWebsite.Pages.Kunden;

public class DemoDetailsModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string DemoKundenID { get; set; }
    private readonly ILogger<DemoDetailsModel> _logger;
    private readonly DbConnection _db;
    public string Title { get; set; }
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public string Adresse { get; set; }
    public string Email { get; set; }
    public string Telefonnummer { get; set; }
    public string Geburtsdatum { get; set; }
    public string startTime { get; set; }



    public DemoDetailsModel(ILogger<DemoDetailsModel> logger, DbConnection db)
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
        Response.Redirect($"/Kunden/ConfirmDemoDelete/{DemoKundenID}");
    }
}
