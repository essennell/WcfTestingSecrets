using System;

namespace Recipes
{
	public enum Measurement
	{
		Fill, Measure, Drop, Tsp,
	}

	public sealed class Ingredient : IEquatable< Ingredient >
	{
		public Ingredient( string name, Measurement mmt, int qty )
		{
			if( string.IsNullOrEmpty( name ) )
				throw new ArgumentNullException( "name" );

			Name = name;
			Amount = mmt;
			Qty = qty;
		}

		public string Name { get; private set; }
		public Measurement Amount { get; private set; }
		public int Qty { get; private set; }

		public bool Equals( Ingredient other )
		{
			return other != null && Name == other.Name;
		}

		public override bool Equals( object obj )
		{
			return obj != null && Equals( obj as Ingredient );
		}

		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}
	}
}
