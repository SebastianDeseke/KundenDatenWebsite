using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KundenDatenWebsite.Pages;

public class DemoOverviewModel : PageModel
{
    private readonly ILogger<DemoOverviewModel> _logger;
    public string KundenID { get; set; }
    public List<Dictionary<string, string>> DemoCustomers { get; set; }
    private readonly DbConnection _db;
    public bool status { get; set; }

    public DemoOverviewModel(ILogger<DemoOverviewModel> logger, DbConnection db)
    {
        _logger = logger;
        _db = db;
    }

    public bool Gueltigkeitsstatus(Dictionary<string, string> demo)
    {
        //runs through the startTime of every demo account
        //and determinse if it is still valid or not
        //only accounts that are 1 day over the liimit are shown
        status = true;
        DemoCustomers = _db.GetAllDemoCustomers();

            DateTime startTime = DateTime.Parse(demo["startTime"]);
            //3 days max duration, so 4 days to show the limit
            DateTime endTime = startTime.AddDays(4);
            if (DateTime.Now > endTime)
            {
                status = false;
            }
            else
            {
                status = true;
            }
            
        return status;
        // return (DateTime.Now > endTime)
        //statement already returns a bool
        //so if statement becomes redundent.
        //only applies when if statement does
        //not execute any code and we are only interested in the bool
    }

    public void OnGet()
    {
        _db.DeleteDemoCustomers();
        DemoCustomers = _db.GetAllDemoCustomers();
    }
}