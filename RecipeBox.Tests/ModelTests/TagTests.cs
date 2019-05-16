using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RecipeBox.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RecipeBox.Tests
{
[TestClass]
public class TagTest : IDisposable

{
public void Dispose()
{
	Tag.ClearAll();
	Recipe.ClearAll();
}

public TagTest()
{
	DBConfiguration.ConnectionString =  "server=localhost;user id=root;password=root;port=8889;database=recipe_box_test";
}

[TestMethod]
public void Equals_EvaluteEqualsBasedOnIdName()
{
	Tag tag = new Tag ("Chicken Recipes",1);
	Tag tag2 = new Tag ("Chicken Recipes",1);

	Assert.AreEqual (tag,tag2);
}

[TestMethod]
public void GetRecipes_ReturnAllRecipesWithSameTag()
{
	Tag tag = new Tag("Soups",9);
	tag.Save();
	Recipe recipe = new Recipe ("borscht","lalala",34,5,1);
	recipe.Save();
	Recipe recipe2 = new Recipe ("jushka","lalal",30,3,2);
	recipe2.Save();
	tag.AddRecipe(recipe);
	tag.AddRecipe(recipe2);

	List<Recipe> allRecipes = new List<Recipe> {
		recipe,recipe2
	};
	List<Recipe> result = tag.GetRecipes();

	CollectionAssert.AreEqual (allRecipes,result);

}

[TestMethod]
public void GetAll_ReturnAllTags()
{
	Tag newTag = new Tag("cookies",1);
	Tag newTag2 = new Tag ("soups",2);
	newTag.Save();
	newTag2.Save();

	List<Tag> allTags = new List<Tag> {
		newTag,newTag2
	};
	List<Tag> result = Tag.GetAll();
	CollectionAssert.AreEqual(allTags,result);
}

[TestMethod]
public void Save_SaveTagToDataVase()
{
	Tag newTag = new Tag ("itailan kitchen", 3);
	newTag.Save();
	List<Tag> allTags = new List<Tag> {
		newTag
	};
	List<Tag> result = Tag.GetAll();
	CollectionAssert.AreEqual (allTags,result);
}

[TestMethod]
public void ClearAll_ReturnEmptyList()
{
	Tag newTag = new Tag("pasta",1);
	newTag.Save();
	Tag.ClearAll();
	List<Tag> newList = new List<Tag>{};
	List<Tag> result = Tag.GetAll();
	CollectionAssert.AreEqual (newList,result);
}

[TestMethod]
public void GetRecipes_ReturnTagWithAllRecipes()
{
Tag newTag = new Tag("pasta",1);
Recipe newRecipe = new Recipe ("jushka","lalal",30,3,2);
Recipe newRecipe2 = new Recipe ("jushka","lalal",30,3,1);
newTag.Save();
newRecipe.Save();
newRecipe2.Save();
newTag.AddRecipe(newRecipe);
newTag.AddRecipe(newRecipe2);
List<Recipe> allRecipes = new List<Recipe>{newRecipe,newRecipe2};
List<Recipe> result = newTag.GetRecipes();
CollectionAssert.AreEqual(allRecipes,result);

}

}
}
