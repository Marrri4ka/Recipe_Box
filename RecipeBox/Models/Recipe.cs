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

public override bool Equals (System.Object obj)
{
	if (!(obj is Recipe))
	{
		return false;

	}
	else
	{
		Recipe newRecipe = (Recipe) obj;
		bool idEquality = this.GetId().Equals (newRecipe.GetId());
		bool nameEquality = this.GetName().Equals (newRecipe.GetName());
		bool descriptionEquality = this.GetDescription().Equals (newRecipe.GetDescription());

		bool cookTimeEquality = this.GetCookTime().Equals (newRecipe.GetCookTime());

		bool rateEquality = this.GetRate().Equals (newRecipe.GetRate());


		return (idEquality && nameEquality &&  descriptionEquality && cookTimeEquality &&rateEquality );

	}
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
	List<Recipe> allRecipes = new List<Recipe> {
	};
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = @"SELECT * FROM recipes;";

	MySqlDataReader rdr = cmd.ExecuteReader();
	while(rdr.Read())
	{
		int id = rdr.GetInt32(0);
		string name = rdr.GetString(1);
		string description = rdr.GetString(2);
		int cookTime = rdr.GetInt32(3);
		int rate = rdr.GetInt32(4);

		allRecipes.Add (new Recipe (name,description,cookTime,rate,id));


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

	this._id = (int)cmd.LastInsertedId;

	conn.Close();
	if(conn != null) conn.Dispose();
}



public void AddTag (Tag tag)
{
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = @"INSERT INTO tags_recipes (tag_id,recipe_id) VALUES (@TagId, @RecipeId);";

	MySqlParameter recipeId = new MySqlParameter ("@RecipeId", this._id);
	MySqlParameter tagId = new MySqlParameter ("@TagId", tag.GetId());
	cmd.Parameters.Add(recipeId);
	cmd.Parameters.Add(tagId);

	cmd.ExecuteNonQuery();

	conn.Close();
	if(conn != null) conn.Dispose();
}


public List<Tag> GetTags()
{

	List<Tag> allTags = new List<Tag> {
	};
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = @"SELECT tags. *
                    FROM recipes
                    JOIN tags_recipes ON (recipes.id = tags_recipes.recipe_id)
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

public static void ClearAll()
{
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = @"DELETE FROM recipes;";
	cmd.ExecuteNonQuery();
	conn.Close();
	if(conn != null) conn.Dispose();
}

public static Recipe Find(int id)
{
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = @"SELECT * FROM recipes WHERE id = (@searchid);";
	MySqlParameter idParameter = new MySqlParameter ("@searchid",id);
	cmd.Parameters.Add(idParameter);

	MySqlDataReader rdr = cmd.ExecuteReader();
	int foundId = 0;
	string name ="";
	string description ="";
	int cookTime = 0;
	int rate =0;
	while(rdr.Read())
	{
		foundId = rdr.GetInt32(0);
		name = rdr.GetString(1);
		description = rdr.GetString(2);
		cookTime = rdr.GetInt32(3);
		rate = rdr.GetInt32(4);

	}
	conn.Close();
	if ( conn != null) conn.Dispose();
	return (new Recipe (name,description,cookTime,rate,id));

}

public static List<Recipe> FilterRecipes(string userRecipe)
{
	List<Recipe> allRecipes = new List<Recipe>{};
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = @"SELECT * FROM recipes WHERE name = @userRecipe;";
	MySqlParameter nameParameter = new MySqlParameter ("@userRecipe",userRecipe);
	cmd.Parameters.Add(nameParameter);
	MySqlDataReader rdr = cmd.ExecuteReader();

	while(rdr.Read())
	{
		int id = rdr.GetInt32(0);
		string name = rdr.GetString(1);
		string description = rdr.GetString(2);
		int cookTime = rdr.GetInt32(3);
		int rate = rdr.GetInt32(4);
		allRecipes.Add(new Recipe(name,description,cookTime,rate,id));
	}

	conn.Close();
	if (conn != null) conn.Dispose();
	return allRecipes;
}

public static List<Recipe> Sort()
{
	List<Recipe> allRecipes = new List<Recipe>{};
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText =@"SELECT * FROM recipes ORDER BY rate;";

	MySqlDataReader rdr = cmd.ExecuteReader();
	while(rdr.Read())
	{
		int id = rdr.GetInt32(0);
		string name = rdr.GetString(1);
		string description = rdr.GetString(2);
		int cookTime = rdr.GetInt32(3);
		int rate = rdr.GetInt32(4);
		Recipe newRecipe = new Recipe(name,description,cookTime,rate,id);
		allRecipes.Add(newRecipe);
	}

	conn.Close();
	if(conn != null) conn.Dispose();
	return allRecipes;
}

}
}
