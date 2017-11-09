using System.Web.Mvc;

namespace LearnIt.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{user}",
                new { action = "Index", user = UrlParameter.Optional }
            );
        }
    }
}