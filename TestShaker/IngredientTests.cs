using System;
using NUnit.Framework;
using Recipes;

namespace TestShaker
{
	[ TestFixture ]
	public class IngredientTests
	{
		[ Test ]
		public void IngredientsCompareEqualOnNameOnly()
		{
			const string name = "ing1";

			var ingredients = new[]
			{
				new Ingredient( name, Measurement.Fill, 1 ),
				new Ingredient( name, Measurement.Measure, 2 )
			};

			Assert.AreEqual( ingredients[ 0 ], ingredients[ 1 ] );
		}

		[ Test ]
		public void HashcodesDifferOnNameOnly()
		{
			const string name = "ing1";
			var ingredients = new[] { 
				new Ingredient( name, Measurement.Fill, 1 ),
				new Ingredient( name, Measurement.Measure, 2 ),
				new Ingredient( "dummy", Measurement.Measure, 2 )
			};

			Assert.AreEqual( ingredients[ 0 ].GetHashCode(), ingredients[ 1 ].GetHashCode() );
			Assert.AreNotEqual( ingredients[ 1 ].GetHashCode(), ingredients[ 2 ].GetHashCode() );
		}

		[ Test ]
		public void ThrowsArgumentNullExceptionWhenNameIsNull()
		{
			Assert.Throws< ArgumentNullException >( () => new Ingredient( null, Measurement.Tsp, 1 ) );
		}

		[ Test ]
		public void ThrowsArgumentNullExceptionWhenNameIsEmpty()
		{
			Assert.Throws< ArgumentNullException >( () => new Ingredient( "", Measurement.Tsp, 1 ) );
		}
	}
}
