using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace RecipeBox.Models
{
  public class Tag
  {
    private string _name;
    private int _id;

    public Tag (string name, int id =0)

    {
      _name = name;
      _id = id;
    }


    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public override bool Equals (System.Object obj)
    {
      if (!(obj is Tag))
      {
        return false;

      }
      else
      {
        Tag newTag = (Tag) obj;
        bool idEquality = this.GetId().Equals (newTag.GetId());
        bool nameEquality = this.GetName().Equals (newTag.GetName());
        return (idEquality && nameEquality);

      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"INSERT INTO tags (name) VALUES (@Name);";

      MySqlParameter parameterName = new MySqlParameter ("@Name",this._name);
      cmd.Parameters.Add (parameterName);

      cmd.ExecuteNonQuery();
      this._id = (int) cmd.LastInsertedId;


    conn.Close();
    if (conn != null) conn.Dispose();
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"DELETE FROM tags;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null )conn.Dispose();

    }

public static List<Tag> GetAll()
{

List<Tag> allTags = new List<Tag>{};
  MySqlConnection conn = DB.Connection();
  conn.Open();
  MySqlCommand cmd = conn.CreateCommand();
  cmd.CommandText = @"SELECT * FROM tags;";
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

public void AddRecipe (Recipe recipe)
{
  MySqlConnection conn = DB.Connection();
  conn.Open();
  MySqlCommand cmd = conn.CreateCommand();
  cmd.CommandText = @"INSERT INTO tags_recipes (tag_id, recipe_id) VALUES (@TagId, @RecipeId);";

  MySqlParameter tag_id = new MySqlParameter ();
  tag_id.ParameterName = "@TagId";
  tag_id.Value = _id;
  cmd.Parameters.Add (tag_id);

  MySqlParameter recipe_id = new MySqlParameter();
  recipe_id.ParameterName = "@RecipeId";
  recipe_id.Value = recipe.GetId();
  cmd.Parameters.Add(recipe_id);

  cmd.ExecuteNonQuery();
  conn.Close();
  if(conn != null) conn.Dispose();

}

public List<Recipe> GetRecipes()
{
  List<Recipe> allRecipes = new List<Recipe> {};

  MySqlConnection conn = DB.Connection();
  conn.Open();

  MySqlCommand cmd = conn.CreateCommand();
  cmd.CommandText = @"SELECT recipes. *
                      FROM tags
                      JOIN tags_recipes ON (tags.id = tags_recipes.tag_id)
                      JOIN recipes ON (recipes.id = tags_recipes.recipe_id)
                      WHERE  tags.id  = @TagId;";

  MySqlParameter recipeId = new MySqlParameter ("@TagId", this._id);
  cmd.Parameters.Add (recipeId);

  MySqlDataReader rdr = cmd.ExecuteReader();

  while (rdr.Read())
  {
    int id = rdr.GetInt32(0);
    string name = rdr.GetString(1);
    string description = rdr.GetString(2);
    int cookTime = rdr.GetInt32(3);
    int rate = rdr.GetInt32(4);

  allRecipes.Add(new Recipe (name,description,cookTime,rate,id));
  }

  conn.Close();
  if( conn != null) conn.Dispose();

  return allRecipes;
 }


 public static Tag Find (int id)
 {
   MySqlConnection conn = DB.Connection();
   conn.Open();
   MySqlCommand cmd = conn.CreateCommand();
   cmd.CommandText = @"SELECT * FROM tag WHERE id = (@searchId);";
   MySqlParameter idParameter = new MySqlParameter ("@searchId",id);
   cmd.Parameters.Add(idParameter);

MySqlDataReader rdr = cmd.ExecuteReader();

int foundId = 0;
string name ="";
while (rdr.Read())
{
  foundId = rdr.GetInt32(0);
  name = rdr.GetString(1);
}

conn.Close();
if(conn != null) conn.Dispose();

return new Tag (name,foundId);

 }




  }
}
