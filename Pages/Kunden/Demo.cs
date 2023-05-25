using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KundenDatenWebsite.Pages.Kunden;

public class DemoModel : PageModel
{
    private readonly ILogger<DemoModel> _logger;

    public string DemoKundenID { get; set; }
    private readonly DbConnection _db;
    private readonly DemoKunden _demo;

    public DemoModel(ILogger<DemoModel> logger, DbConnection db)
    {
        _logger = logger;
        _demo = new DemoKunden();
        _db = db;
    }


    public DateTime determinStartTime()
    {
        DateTime startTime = DateTime.Now;
        return startTime;
    }

    public void OnGet()
    {

    }

    public void OnPostAdd()
    {
        //Take the information that is inputed in the form
        //and save it in the database
        _db.CreateDemoCustomer(Request.Form["Vorname"], Request.Form["Nachname"], Request.Form["Title"], Request.Form["Adresse"], Request.Form["Email"], Request.Form["Telefonnummer"], Request.Form["Geburtsdatum"], determinStartTime(), _demo.GenerateDemoID());
    }

    public void OnPostSearch()
    {
        DemoKundenID = Request.Form["DemoKundenID"];
        Response.Redirect($"/Kunden/DemoDetails/{DemoKundenID}");
    }
}
