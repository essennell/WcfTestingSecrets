using System.Runtime.Serialization;

namespace Recipes.Contract
{
	[ DataContract ]
	public class DrinkDto
	{
		[ DataMember ]
		public string Name { get; set; }

		[ DataMember ]
		public string Method { get; set; }
	}
}
