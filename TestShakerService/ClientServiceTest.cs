using NUnit.Framework;
using RecipeBook.Client;
using RecipeBook.Server;

namespace TestShakerService
{
	[TestFixture, Explicit, Category("Service")]
	public class ClientServiceTest
	{
		[Test]
		public void ClientAndServiceStartAndCanCommunicate()
		{
			const string address = "http://localhost:5110";
			using( var host = new RecipeBookHost( address ) )
			using( var recipes = new RecipeBookClient( address ) )
			{
				Assert.IsNotNull( host );
				Assert.IsNotNull( recipes );
				Assert.DoesNotThrow( () => { var x = recipes.AllDrinks; } );
			}
		}
	}
}
