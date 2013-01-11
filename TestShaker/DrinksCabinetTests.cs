﻿using System.Linq;
using NUnit.Framework;
using Recipes;

namespace TestShaker
{
	[ TestFixture ]
	public class DrinksCabinetTests
	{
		private RecipeBook recipes;

		[SetUp]
		public void Start()
		{
			recipes = new LocalRecipeBook();
		}

		[TearDown]
		public void End()
		{
			recipes.Dispose();
		}

		[Test]
		public void EmptyCabinetHasNoIngredients()
		{
			var cabinet = new DrinksCabinet( recipes );
			var results = cabinet.Ingredients;
			Assert.IsFalse( results.Any() );
		}

		[ Test ]
		public void CanLocateSpecificDrinkByName()
		{
			var cabinet = new DrinksCabinet( recipes );
			
			var expected = new LocalDrink( "a", "", new[] { 
				new Ingredient( "1", Measurement.Tsp, 1 ) 
			} );
			var error = new LocalDrink( "b", "", new[] { 
				new Ingredient( "2", Measurement.Tsp, 1 ) 
			} );

			recipes.Add( expected, error );

			var result = cabinet.Find( expected.Name );

			Assert.AreEqual( expected, result );
		}

		[ Test ]
		public void CanFilterDrinksOnNotSpecified()
		{
			var cabinet = new DrinksCabinet( recipes );
			
			var expected = new LocalDrink( "a", "", new[] { 
				new Ingredient( "1", Measurement.Tsp, 1 ) 
			} );
			var error = new LocalDrink( "b", "", new[] { 
				new Ingredient( "2", Measurement.Tsp, 1 ) 
			} );

			recipes.Add( expected, error );

			var results = cabinet.NotContaining( "2" );

			Assert.AreEqual( expected, results.Single() );
		}
	}
}
