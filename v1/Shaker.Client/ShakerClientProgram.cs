using System;
using System.ServiceModel;
using Recipes.Contract;

namespace Shaker.Client
{
	static class ShakerClientProgram
	{
		static int Main()
		{
			const string name = "ShakerService";
			const string address = "http://localhost:5110";

			using( var channel = new ChannelFactory< RecipeBookContract >( name ) )
			{
				var recipes = channel.CreateChannel( new EndpointAddress( address ) );
				recipes.Add( 
					new DrinkDto{ Name = "G&T", Method = "Mix with ice and lime" },
					new[] {
						new IngredientDto{ Name = "Gin", 
										   Amount = IngredientDto.Measurement.Measure, 
										   Qty = 2 },
						new IngredientDto{ Name = "Tonic Water", 
										   Amount = IngredientDto.Measurement.Fill, 
										   Qty = 1 }
					} );
				foreach( var d in recipes.AllDrinks() )
				{
					Console.WriteLine( d.Name );
				}
				Console.WriteLine( "Press [Enter] to exit" );
				Console.ReadLine();

				return 0;
			}
		}
	}
}
