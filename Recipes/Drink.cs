using System;
using System.Collections.Generic;

namespace Recipes
{
	public interface Drink : IEquatable< Drink >
	{
		string Name { get; }
		string Method { get; }
		IEnumerable< Ingredient > Ingredients { get; }
	}
}
