using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RecipeBox.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RecipeBox.Tests
{
[TestClass]
public class RecipeTest : IDisposable
{
public void Dispose()
{
	Tag.ClearAll();
	Recipe.ClearAll();
}

public RecipeTest()
{
	DBConfiguration.ConnectionString =  "server=localhost;user id=root;password=root;port=8889;database=recipe_box_test";
}
[TestMethod]
public void Save_SaveRecipeToDatebase()
{
	Recipe recipe = new Recipe("neapoleon", "lllala", 30,3);
	recipe.Save();
	List<Recipe> allRecipes = new List<Recipe> {
		recipe
	};
	List<Recipe> result = Recipe.GetAll();
	CollectionAssert.AreEqual(allRecipes,result);

}

[TestMethod]
public void Equals_EvaluteEqualsBasedOnIdName()
{
	Recipe newRecipe = new Recipe ("neapoleon", "lllala", 30,3,1);
	Recipe newRecipe2 = new Recipe ("neapoleon", "lllala", 30,3,1);
	Assert.AreEqual(newRecipe,newRecipe2);
}

[TestMethod]
public void GetAll_ReturnAllRecipes()
{
	Recipe newRecipe = new Recipe("neapoleon", "lllala", 30,3,1);
	newRecipe.Save();
	Recipe newRecipe1 = new Recipe("neapoleon", "lllala", 30,3,1);
	newRecipe1.Save();
	List<Recipe> allRecipes = new List<Recipe> {
		newRecipe,newRecipe1
	};
	List<Recipe> result = Recipe.GetAll();
	CollectionAssert.AreEqual (allRecipes,result);

}

[TestMethod]
public void GetTags_ReturnAllTagsWithThisRecipe()
{
	Recipe newRecipe = new Recipe("neapoleon", "lllala", 30,3,1);
	newRecipe.Save();
	Tag newTag = new Tag("cokie",1);
	newTag.Save();
	Tag newTag1 = new Tag("gluteenfree",1);
	newTag1.Save();
	newRecipe.AddTag(newTag);
	newRecipe.AddTag(newTag1);
	List<Tag> allTags = new List<Tag> {
		newTag,newTag1
	};
	List<Tag> result = newRecipe.GetTags();
	Console.WriteLine(allTags.Count);
	Console.WriteLine(result.Count);

	CollectionAssert.AreEqual(allTags,result);

}
[TestMethod]
public void AddTag_ReturnRecipeWithTag()
{
	Recipe newRecipe = new Recipe("neapoleon", "lllala", 30,3,1);
	Tag newTag = new Tag("cokie",1);
	newRecipe.Save();
	newTag.Save();
	newRecipe.AddTag(newTag);
	List<Tag> newList = new List<Tag> {
		newTag
	};
	List<Tag> result = newRecipe.GetTags();
	CollectionAssert.AreEqual(newList,result);

}
[TestMethod]
public void ClearAll_ReturnEmptyList()
{
	Recipe newRecipe = new Recipe("neapoleon", "lllala", 30,3,1);
	newRecipe.Save();
	Recipe.ClearAll();
	List<Recipe> newList = new List<Recipe> {
	};
	List<Recipe> result = Recipe.GetAll();
	CollectionAssert.AreEqual (newList,result);
}


}
}
