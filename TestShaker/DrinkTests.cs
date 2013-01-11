using System;
using NUnit.Framework;
using Recipes;

namespace TestShaker
{
	[ TestFixture ]
	public class DrinkTests
	{
		[ Test ]
		public void CompareEqualOnNameOnly()
		{
			const string name = "drink1";
			var drinks = new[] { 
				new LocalDrink( name, "", new Ingredient[ 0 ] ), 
				new LocalDrink( name, "", new []{ new Ingredient( "foo", Measurement.Tsp, 1 ) } ) };

			Assert.AreEqual( drinks[ 0 ], drinks[ 1 ] );
		}

		[ Test ]
		public void HashcodesDifferOnNameOnly()
		{
			const string name = "drink1";
			var drinks = new[] { 
				new LocalDrink( name, "", new Ingredient[ 0 ] ), 
				new LocalDrink( name, "", new[] { new Ingredient( "foo", Measurement.Tsp, 1 ) } ), 
				new LocalDrink( "dummy", "", new[] { new Ingredient( "foo", Measurement.Tsp, 1 ) } ) 
			};

			Assert.AreEqual( drinks[ 0 ].GetHashCode(), drinks[ 1 ].GetHashCode() );
			Assert.AreNotEqual( drinks[ 1 ].GetHashCode(), drinks[ 2 ].GetHashCode() );
		}

		[ Test ]
		public void ThrowsArgumentNullExceptionWhenNameIsNull()
		{
			Assert.Throws< ArgumentNullException >( () => new LocalDrink( null, "", new Ingredient[ 0 ] ) );
		}

		[ Test ]
		public void ThrowsArgumentNullExceptionWhenNameIsEmpty()
		{
			Assert.Throws< ArgumentNullException >( () => new LocalDrink( "", "", new Ingredient[ 0 ] ) );
		}

		[ Test ]
		public void ThrowsArgumentNullExceptionWhenMethodIsNull()
		{
			Assert.Throws< ArgumentNullException >( () => new LocalDrink( "", null, new Ingredient[ 0 ] ) );
		}

		[ Test ]
		public void ThrowsArgumentNullExceptionWhenIngredientsIsNull()
		{
			Assert.Throws< ArgumentNullException >( () => new LocalDrink( "", "", null ) );
		}
	}
}
