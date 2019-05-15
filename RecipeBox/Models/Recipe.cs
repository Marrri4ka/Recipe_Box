using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace RecipeBox.Models
{
  public class Recipe
  {
    private string _name;
    private string _description;
    public int _cookTime;
    public int _rate;
    private int _id;

    public Recipe (string name, string description, int cookTime, int rate, int id=0)
    {
      _name = name;
      _description = description;
      _cookTime = cookTime;
      _rate = rate;
      _id=id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetDescription()
    {
      return _description;
    }

    public int GetCookTime()
    {
      return _cookTime;
    }

    public int GetRate()
    {
      return _rate;
    }

    public int GetId()
    {
      return _id;
    }

public static List<Recipe> GetAll()


{
  List<Recipe> allRecipes = new List<Recipe>{};
  MySqlConnection conn = DB.Connection();
  conn.Open();
  MySqlCommand cmd = conn.CreateCommand();
  cmd.CommandText = @"SLECT * FROM recipes;";

  MySqlDataReader rdr = cmd.ExecuteReader();
  while(rdr.Read())
  {
    int id = rdr.GetInt32(0);
    string name = rdr.GetString(1);
    string description = rdr.GetString(2);
    int cookTime = rdr.GetInt32(3);
    int rate = rdr.GetInt32(4);

    allRecipes.Add (new Recipe (name,description,cookTime,rate));


  }
    return allRecipes;
}

public void Save()
{
  MySqlConnection conn = DB.Connection();
  conn.Open();
  MySqlCommand cmd = conn.CreateCommand();
  cmd.CommandText = @"INSERT INTO recipes (name, description, cooktime, rate ) VALUES (@Name, @Description, @CookTime, @Rate);";

  MySqlParameter nameParameter = new MySqlParameter ("@Name", this._name);
  cmd.Parameters.Add(nameParameter);


  MySqlParameter descriptionParameter = new MySqlParameter("@Description", this._description);
  cmd.Parameters.Add(descriptionParameter);

  MySqlParameter cookTimeParameter = new MySqlParameter("@CookTime", this._cookTime);
  cmd.Parameters.Add(cookTimeParameter);

  MySqlParameter rateParameter = new MySqlParameter ("@Rate", this._rate);
  cmd.Parameters.Add(rateParameter);

cmd.ExecuteNonQuery();

_id = (int)cmd.LastInsertedId;

conn.Close();
if(conn != null) conn.Dispose();
}

public static void DeleteAll()
{
  MySqlConnection conn = DB.Connection();
  conn.Open();
  MySqlCommand cmd = conn.CreateCommand();
  cmd.CommandText = @"DELETE * FROM recipes;";
  cmd.ExecuteNonQuery();

  conn.Close();
  if ( conn != null) conn.Dispose();
}


public void AddTag (Tag tag)
{
  MySqlConnection conn = DB.Connection();
  conn.Open();
  MySqlCommand cmd = conn.CreateCommand();
  cmd.CommandText = @"INSERT INTO tags_recipes (tag_id,recipe_id) VALUES (@TagId, @RecipeId);";

  MySqlParameter recipeId = new MySqlParameter ("@RecipeId", this._id);
  MySqlParameter tagId = new MySqlParameter ("@TagId", this._id);
  cmd.Parameters.Add(recipeId);
  cmd.Parameters.Add(tagId);

  cmd.ExecuteNonQuery();

  conn.Close();
  if(conn != null) conn.Dispose();
}


public List<Tag> GetTags()
{

  List<Tag> allTags = new List<Tag>{};
  MySqlConnection conn = DB.Connection();
  conn.Open();
  MySqlCommand cmd = conn.CreateCommand();
  cmd.CommandText = @"SELECT tags. *
                    FROM recipes
                    JOIN tags_recips ON (recipes.id = tags_recipes.recipe_id)
                    JOIN tags ON (tags.id = tags_recipes.tag_id)
                    WHERE recipes.id = @RecipeId;";


MySqlParameter recipeParameter = new MySqlParameter ("@RecipeId", this._id);
cmd.Parameters.Add(recipeParameter);
MySqlDataReader rdr = cmd.ExecuteReader();

while (rdr.Read())
{
  int id = rdr.GetInt32(0);
  string name = rdr.GetString(1);
  allTags.Add(new Tag(name,id));
}

conn.Close();
if(conn != null) conn.Dispose();
return allTags;
  }
}
}
