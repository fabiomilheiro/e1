using E1.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace E1.Web.Components
{
    public class MessageViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (TempData.ContainsKey("Success"))
            {
                return View(new MessageComponentViewModel
                {
                    Type = "success",
                    Text = TempData["Success"].ToString()
                });
            }

            if (TempData.ContainsKey("Error"))
            {
                return View(new MessageComponentViewModel
                {
                    Type = "error",
                    Text = TempData["Error"].ToString()
                });
            }

            return null;
        }
    }
}