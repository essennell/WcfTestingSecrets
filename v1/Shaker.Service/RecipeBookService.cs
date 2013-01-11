using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Recipes.Contract;

namespace Shaker.Service
{
	[ ServiceBehavior( InstanceContextMode = InstanceContextMode.Single ) ]
	public class RecipeBookService : RecipeBookContract
	{
		public RecipeBookService()
		{
			drinks = new List< DrinkDto >();
			ingredients = new Dictionary< string, List< IngredientDto > >();
		}

		public IEnumerable< DrinkDto > AllDrinks()
		{
			return drinks;
		}

		public IEnumerable< string > AllIngredients()
		{
			return drinks.SelectMany( d => IngredientsOf( d.Name ) )
				.Select( i => i.Name ).Distinct();
		}

		public IEnumerable< IngredientDto > IngredientsOf( string drink )
		{
			return ingredients[ drink ];
		}

		public void Add( DrinkDto drink, IEnumerable< IngredientDto > i )
		{
			if( drinks.Any( d => d.Name == drink.Name ) )
			{
				throw new FaultException( "DuplicateDrink" );
			}
			drinks.Add( drink );
			ingredients.Add( drink.Name, i.ToList() );
		}

		public IEnumerable< DrinkDto > WithIngredients( IEnumerable< string > selectedIngredients )
		{
			var result = drinks.Where( drink => ingredients[ drink.Name ]
					.Any( dto => selectedIngredients.Contains( dto.Name ) ) );
			return result;
		}

		private readonly List< DrinkDto > drinks;
		private readonly Dictionary< string, List< IngredientDto > > ingredients;
	}
}
