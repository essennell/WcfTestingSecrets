using System;
using RecipeBook.Client;
using Recipes;

namespace Shaker.Client
{
	static class ShakerClientProgram
	{
		static int Main()
		{
			const string address = "http://localhost:5110";
			using( var recipes = new RecipeBookClient( address ) )
			{
				recipes.Add(
					new LocalDrink( "G&T", "Mix with ice and lime",
					new[] {
						new Ingredient( "Gin", Measurement.Measure, 2 ),
						new Ingredient( "Tonic Water", Measurement.Fill, 1 )
					} ) );
				foreach (var d in recipes.AllDrinks)
				{
					Console.WriteLine( d.Name );
				}
			}
			Console.WriteLine( "Press [Enter] to exit" );
			Console.ReadLine();

			return 0;
		}
	}
}
