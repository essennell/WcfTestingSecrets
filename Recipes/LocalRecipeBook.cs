using System.Collections.Generic;
using System.Linq;

namespace Recipes
{
	public sealed class LocalRecipeBook : RecipeBook
	{
		public LocalRecipeBook()
		{
			drinks = new HashSet< Drink >();
		}

		public void Dispose()
		{
		}

		public IEnumerable< Drink > AllDrinks
		{
			get { return drinks; }
		}

		public IEnumerable< string > AllIngredients
		{
			get { return drinks.SelectMany( d => d.Ingredients ).Select( i => i.Name ).Distinct(); }
		}

		public void Add( params Drink [] newDrinks )
		{
			foreach( var drink in newDrinks )
			{
				if( drinks.Contains( drink ) )
					throw new DuplicateDrinkException( "{0} already exists", drink.Name );
				drinks.Add( drink );
			}
		}

		public IEnumerable< Drink > WithIngredients( params string [] s )
		{
			return drinks.Where( d => d.Ingredients
				.Select( i => i.Name )
				.Any( s.Contains ) );
		}
 
		private readonly HashSet< Drink > drinks;
	}
}
