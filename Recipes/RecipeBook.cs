using System;
using System.Collections.Generic;

namespace Recipes
{
	public interface RecipeBook : IDisposable
	{
		IEnumerable< Drink > AllDrinks { get; }
		IEnumerable< string > AllIngredients { get; }
		void Add( params Drink [] newDrinks );
		IEnumerable< Drink > WithIngredients( params string [] selected );
	}
}
