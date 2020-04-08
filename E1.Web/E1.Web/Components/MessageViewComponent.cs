using E1.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

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

            return new ContentViewComponentResult(string.Empty);
        }
    }
}