using System.Runtime.Serialization;

namespace Recipes.Contract
{
	[ DataContract ]
	public class IngredientDto
	{
		public enum Measurement { Fill, Measure, Drop, Tsp, }

		[ DataMember ]
		public string Name { get; set; }

		[ DataMember ]
		public Measurement Amount { get; set; }

		[ DataMember ]
		public int Qty { get; set; }
	}
}
