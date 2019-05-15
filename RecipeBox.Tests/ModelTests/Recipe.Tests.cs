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
      // Recipe.ClearAll();
    }

    public RecipeTest()
    {
        DBConfiguration.ConnectionString =  "server=localhost;user id=root;password=root;port=8889;database=recipe_box_test";
    }
  }
}
