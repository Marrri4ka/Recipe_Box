using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;

namespace RecipeBox.Controllers
{
public class TagsController : Controller
{
[HttpGet("/tags")]
public ActionResult Index()
{
	List<Tag> allTags = Tag.GetAll();
	return View(allTags);
}

[HttpGet("tags/new")]
public ActionResult New()
{
	return View();
}
}
}
