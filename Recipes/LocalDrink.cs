using System;
using System.Collections.Generic;

namespace Recipes
{
	public sealed class LocalDrink : Drink
	{
		public LocalDrink( string name, string method, IEnumerable< Ingredient > ingredients )
		{
			if( ingredients == null )
				throw new ArgumentNullException( "ingredients" );
			if( method == null )
				throw new ArgumentNullException( "method" );
			if( string.IsNullOrEmpty( name ) )
				throw new ArgumentNullException( "name" );

			Name = name;
			Method = method;
			Ingredients = ingredients;
		}

		public string Name { get; private set; }
		public string Method { get; set; }
		public IEnumerable< Ingredient > Ingredients { get; private set; }

		public bool Equals( Drink other )
		{
			return other != null && Name == other.Name;
		}

		public override bool Equals( object obj )
		{
			return obj != null && Equals( obj as Drink );
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}
	}
}
