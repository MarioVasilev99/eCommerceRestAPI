namespace eCommerceRestAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Security.Claims;

    public class ControllerBase : Controller
    {
        public int GetUserId()
        {
            string userIdString = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return int.Parse(userIdString);
        }
    }
}
