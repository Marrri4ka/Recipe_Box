using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;

namespace RecipeBox.Controllers
{
public class RecipesController : Controller
{
[HttpGet("/recipes")]
public ActionResult Index()
{
	List<Recipe> allRecipes = Recipe.GetAll();
	return View(allRecipes);
}

[HttpGet("recipes/new")]
public ActionResult New()
{
	return View();
}
}
}
