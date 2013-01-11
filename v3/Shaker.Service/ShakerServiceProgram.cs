using System;
using RecipeBook.Server;

namespace Shaker.Service
{
	static class ShakerServiceProgram
	{
		static int Main()
		{
			const string address = "http://localhost:5110";
			using( var host = new RecipeBookHost( address ) )
			{
				Console.WriteLine( "v1 Shaker Service running. Press [Enter] to close" );
				Console.ReadLine();				
			}
			return 0;
		}
	}
}
