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
      // Tag.ClearAll();
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

      List<Recipe> allRecipes = new List<Recipe>{recipe,recipe2};
      List<Recipe> result = tag.GetRecipes();

      CollectionAssert.AreEqual (allRecipes,result);

    }
  }
}
