using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recipes;

namespace CocktailShaker
{
	class CocktailShakerProgram
	{
		static void Main(string[] args)
		{
			using( var recipes = new LocalRecipeBook() )
			{
				var cabinet = new DrinksCabinet( recipes );

			}
		}
	}
}
