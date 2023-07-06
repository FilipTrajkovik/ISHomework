using Microsoft.AspNetCore.Mvc;

namespace EShopTicketCinema.Web.Controllers.Api
{
    public class UserController : Controller
    {

        [HttpGet("[action]")]
        public IActionResult ImportUsers()
        {
            return View();
        }
    }
}
