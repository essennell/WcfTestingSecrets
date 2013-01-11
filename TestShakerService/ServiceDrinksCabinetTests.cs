using System.Linq;
using NUnit.Framework;
using RecipeBook.Client;
using RecipeBook.Server;
using Recipes;

namespace TestShakerService
{
	[ TestFixture, Explicit, Category( "Service" ) ]
	public class ServiceDrinksCabinetTests
	{
		private RecipeBookHost host;
		private Recipes.RecipeBook recipes;

		[ SetUp ]
		public void Start()
		{
			const string address = "http://localhost:5110";
			host = new RecipeBookHost( address );
			recipes = new RecipeBookClient( address );
		}

		[ TearDown ]
		public void End()
		{
			recipes.Dispose();
			host.Close();
		}

		[Test]
		public void EmptyCabinetHasNoIngredients()
		{
			var cabinet = new DrinksCabinet(recipes);
			var results = cabinet.Ingredients;
			Assert.IsFalse(results.Any());
		}

		[Test]
		public void CanLocateSpecificDrinkByName()
		{
			var cabinet = new DrinksCabinet(recipes);

			var expected = new LocalDrink("a", "", new[] { 
				new Ingredient( "1", Measurement.Tsp, 1 ) 
			});
			var error = new LocalDrink("b", "", new[] { 
				new Ingredient( "2", Measurement.Tsp, 1 ) 
			});

			recipes.Add(expected, error);

			var result = cabinet.Find(expected.Name);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void CanFilterDrinksOnNotSpecified()
		{
			var cabinet = new DrinksCabinet(recipes);

			var expected = new LocalDrink("a", "", new[] { 
				new Ingredient( "1", Measurement.Tsp, 1 ) 
			});
			var error = new LocalDrink("b", "", new[] { 
				new Ingredient( "2", Measurement.Tsp, 1 ) 
			});

			recipes.Add(expected, error);

			var results = cabinet.NotContaining("2");

			Assert.AreEqual(expected, results.Single());
		}
	}
}
