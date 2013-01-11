using System;
using System.ServiceModel;

namespace Shaker.Service
{
	class ShakerServiceProgram
	{
		static int Main()
		{
			const string address = "http://localhost:5110";

			var recipes = new RecipeBookService();
			using( var host = new ServiceHost( recipes, new Uri( address ) ) )
			{
				host.Open();
				Console.WriteLine( "v1 Shaker Service running. Press [Enter] to close" );
				Console.ReadLine();
			}
			return 0;
		}
	}
}
