using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Recipes.Contract;

namespace RecipeBook.Server
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
			return drinks.SelectMany( d => IngredientsOf( d.Name ) ).Select( i => i.Name ).Distinct();
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
			var result = drinks.Where( drink => ingredients[ drink.Name ].Any( dto => selectedIngredients.Contains( dto.Name ) ) );
			return result;
		}

		private readonly List< DrinkDto > drinks;
		private readonly Dictionary< string, List< IngredientDto > > ingredients;

		//private static readonly LocalDrink[] LoadedDrinks = new[]{
		//		new LocalDrink( "G&T", "Mix with ice and lime", new[] {
		//			new Ingredient( "Gin", Measurement.Measure, 2 ),
		//			new Ingredient( "Tonic Water", Measurement.Fill, 1 ) 
		//		} ),
		//		new LocalDrink( "Screwdriver", "Mix with ice", new[] {
		//			new Ingredient( "Vodka", Measurement.Measure, 2 ),
		//			new Ingredient( "Orange Juice", Measurement.Fill, 1 ) 
		//		} ),
		//		new LocalDrink( "Martini", "Dip glass in vermouth, add an olive", new[] {
		//			new Ingredient( "Gin", Measurement.Measure, 2 ),
		//			new Ingredient( "Vermouth", Measurement.Drop, 2 ) 
		//		} ),
		//	};
	}
}
