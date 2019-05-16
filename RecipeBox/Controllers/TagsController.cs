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

[HttpPost("/tags")]
public ActionResult Create(string tagName)
{
	Tag newtag = new Tag(tagName);
	newtag.Save();
	List<Tag> allTags = Tag.GetAll();
	return View("Index",allTags);
}

[HttpGet("/tag/{id}")]
public ActionResult Show(int id)
{
	Tag newTag = Tag.Find(id);
	ViewBag.Recipes = Recipe.GetAll();
	ViewBag.Tag = Tag.Find(id);
	ViewBag.Recipes1 = newTag.GetRecipes();
	return View();
}

[HttpPost("/tags/{id}/addrecipe")]
public ActionResult AddRecipe(int id, int recipeId)
{

	Tag newTag = Tag.Find(id);
	ViewBag.Recipes = Recipe.GetAll();
	ViewBag.Tag = Tag.Find(id);
	ViewBag.Recipes1 = newTag.GetRecipes();

	return View ("Show");
}

[HttpPost("/search")]
public ActionResult Filter(string userInput)
{
	List<Tag> filteredTags = Tag.FilterAll(userInput);
	return View("Index",filteredTags);
}

}
}
