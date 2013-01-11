using System.Linq;
using NUnit.Framework;
using RecipeBook.Client;
using RecipeBook.Server;
using Recipes;

namespace TestShakerService
{
	[ TestFixture, Explicit, Category( "Service" ) ]
	public class RecipeBookTests
	{
		private Recipes.RecipeBook recipes;
		private RecipeBookHost host;

		[ SetUp ]
		public void Start()
		{
			const string address = "http://localhost";
			host = new RecipeBookHost( address );
			recipes = new RecipeBookClient( address );
		}

		[ TearDown ]
		public void End()
		{
			recipes.Dispose();
			host.Close();
		}

		[ Test ]
		public void DefaultRecipeBookContainsNoDrinks()
		{
			var results = recipes.AllDrinks;
			Assert.False( results.Any() );
		}

		[ Test ]
		public void AddingDrinkWithSameNameAsExistingThrowsDuplicateDrinkException()
		{
			var drinks = new Drink[] { 
				new LocalDrink( "1", "", new Ingredient[ 0 ] ), 
				new LocalDrink( "1", "", new Ingredient[ 0 ] ) 
			};

			Assert.Throws< DuplicateDrinkException >( () => recipes.Add( drinks ) );
		}

		[ Test ]
		public void AllDrinksContainsAllAddedItems()
		{
			var expected = new Drink[] { 
				new LocalDrink( "1", "", new Ingredient[ 0 ] ), 
				new LocalDrink( "2", "", new Ingredient[ 0 ] ) 
			};
			recipes.Add( expected );

			var results = recipes.AllDrinks;

			Assert.IsTrue( expected.SequenceEqual( results ) );
		}

		[ Test ]
		public void AllIngredientsIsEmptyWhenNoDrinksAvailable()
		{
			var results = recipes.AllIngredients;
			Assert.IsFalse( results.Any() );
		}

		[ Test ]
		public void AllIngredientsContainsUniqueItems()
		{
			var expected = new Ingredient( "a", Measurement.Tsp, 1 );
			var drinks = new Drink[]
			{
				new LocalDrink( "1", "", new[] { expected } ),
				new LocalDrink( "2", "", new[] { expected } )
			};
			recipes.Add( drinks );

			var results = recipes.AllIngredients;

			Assert.IsTrue( expected.Name == results.Single() );
		}

		[ Test ]
		public void AllIngredientsContainsAllItemsFromAllDrinks()
		{
			var expected = new[]
				{
					new Ingredient( "a", Measurement.Tsp, 1 ),
					new Ingredient( "b", Measurement.Tsp, 1 )
				};
			var drinks = new Drink[]
			{
				new LocalDrink( "1", "", new[] { expected[ 0 ] } ),
				new LocalDrink( "2", "", new[] { expected[ 1 ] } )
			};
			recipes.Add( drinks );

			var results = recipes.AllIngredients;

			Assert.IsTrue( expected.Select( i => i.Name ).SequenceEqual( results ) );
		}

		[ Test ]
		public void FilteredDrinksHasNoResultsWhenNoMatchIsFound()
		{
			var drinks = new Drink[]
			{
				new LocalDrink( "1", "", new Ingredient[ 0 ] )
			};
			recipes.Add( drinks );

			var results = recipes.WithIngredients( "c" );

			Assert.IsFalse( results.Any() );
		}

		[ Test ]
		public void FilteredDrinksContainsOnlyThoseWithSpecifiedIngredients()
		{
			var expected = new LocalDrink( "1", "", new[] { new Ingredient( "a", Measurement.Tsp, 1 ) } );
			var error = new LocalDrink( "2", "", new[] { new Ingredient( "b", Measurement.Tsp, 1 ) } );
			recipes.Add( expected, error );

			var results = recipes.WithIngredients( "a" );

			Assert.AreEqual( expected, results.Single() );
		}

		[ Test ]
		public void FilteredDrinksContainsAllThoseWithSpecifiedIngredients()
		{
			var expected = new Drink[] {
				new LocalDrink( "1", "", new[] { new Ingredient( "a", Measurement.Tsp, 1 ) } ),
				new LocalDrink( "2", "", new[] { new Ingredient( "a", Measurement.Tsp, 1 ) } )
			};
			recipes.Add( expected );

			var results = recipes.WithIngredients( "a" );

			Assert.IsTrue( expected.SequenceEqual( results ) );
		}

		[ Test ]
		public void FilteredDrinksContainsAllThoseWithAllSpecifiedIngredients()
		{
			var expected = new Drink[] {
				new LocalDrink( "1", "", new[] { new Ingredient( "a", Measurement.Tsp, 1 ) } ),
				new LocalDrink( "2", "", new[] { new Ingredient( "b", Measurement.Tsp, 1 ) } )
			};
			recipes.Add( expected );

			var results = recipes.WithIngredients( "a", "b" );

			Assert.IsTrue( expected.SequenceEqual( results ) );
		}

		[ Test ]
		public void FilteredDrinksContainsResultsWithAnyIngredientMatch()
		{
			var expected = new LocalDrink( "1", "", new[] { 
				new Ingredient( "a", Measurement.Tsp, 1 ), 
				new Ingredient( "b", Measurement.Tsp, 2 ) 
			} );
			recipes.Add( expected );

			var results = recipes.WithIngredients( "b", "c" );

			Assert.IsTrue( expected.Equals( results.Single() ) );
		}
	}
}
