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


    [HttpPost("/search")]
    public ActionResult Filter(string userInput)
    {
    	List<Tag> filteredTags = Tag.FilterAll(userInput);
    	return View("Index",filteredTags);
    }

    [HttpPost("/tags")]
    public ActionResult Create(string tagName)
    {
    	Tag newtag = new Tag(tagName);
    	newtag.Save();
    	List<Tag> allTags = Tag.GetAll();
    	return View("Index",allTags);
    }


  }
}
