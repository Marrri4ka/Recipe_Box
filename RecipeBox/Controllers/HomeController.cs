using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;

namespace RecipeBox.Controllers

{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {

          List<Tag> allTags = Tag.GetAll();
          return View (allTags);
    }

  }
}
