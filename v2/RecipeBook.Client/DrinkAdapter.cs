using System;
using System.Collections.Generic;
using System.Linq;
using Recipes;
using Recipes.Contract;

namespace RecipeBook.Client
{
	public class DrinkAdapter : Drink
	{
		public DrinkAdapter( RecipeBookContract recipes, DrinkDto drinkDto )
		{
			this.recipes = recipes;
			Name = drinkDto.Name;
			Method = drinkDto.Method;
		}

		public string Name { get; private set; }
		public string Method { get; private set; }

		public IEnumerable< Ingredient > Ingredients
		{
			get { return recipes.IngredientsOf( Name )
				.Select( i => new Ingredient( 
					i.Name, 
					( Measurement )i.Amount, 
					i.Qty ) ); }
		}

		public bool Equals( Drink other )
		{
			return other != null && Name == other.Name;
		}

		private readonly RecipeBookContract recipes;
	}
}
