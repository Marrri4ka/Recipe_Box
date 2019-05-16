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

[HttpPost("/recipes")]
public ActionResult Create(string recipeName, string recipeDescription,int cookTime,int rate)
{
	Recipe newRecipe = new Recipe(recipeName,recipeDescription,cookTime,rate);
	newRecipe.Save();
	List<Recipe> allRecipes = Recipe.GetAll();
	return View("Index",allRecipes);
}
[HttpGet("/recipe/{id}")]
public ActionResult Show(int id)
{
	Recipe newRecipe = Recipe.Find(id);
	ViewBag.Tags = Tag.GetAll();
	ViewBag.Recipe = Recipe.Find(id);
	ViewBag.Tags1 = newRecipe.GetTags();


	return View();
}
[HttpPost("/recipes/{id}/addtag")]
public ActionResult AddTag(int id, int tagId)
{

	Recipe newRecipe = Recipe.Find(id);
	newRecipe.AddTag(Tag.Find(tagId));
	ViewBag.Tags = Tag.GetAll();
	ViewBag.Recipe = Recipe.Find(id);
	ViewBag.Tags1 = newRecipe.GetTags();

	return View ("Show");
}

}
}
