using System.Collections.Generic;
using System.Linq;

namespace Recipes
{
	public class DrinksCabinet
	{
		public DrinksCabinet( RecipeBook recipes )
		{
			this.recipes = recipes;
		}

		public Drink Find( string name )
		{
			return recipes.AllDrinks.Single( d => d.Name == name );
		}

		public IEnumerable< string > Ingredients
		{
			get { return recipes.AllIngredients; }
		}

		public IEnumerable< Drink > NotContaining( params string [] selected )
		{
			var remain = Ingredients.Except( selected );
			return recipes.WithIngredients( remain.ToArray() );
		}

		private readonly RecipeBook recipes;
	}
}
