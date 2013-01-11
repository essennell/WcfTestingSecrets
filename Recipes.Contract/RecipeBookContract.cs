using System.Collections.Generic;
using System.ServiceModel;

namespace Recipes.Contract
{
	[ ServiceContract ]
	public interface RecipeBookContract
	{
		[ OperationContract ]
		IEnumerable< DrinkDto > AllDrinks();

		[ OperationContract ]
		IEnumerable< string > AllIngredients();

		[ OperationContract ]
		IEnumerable< IngredientDto > IngredientsOf( string drink );

		[ OperationContract ]
		void Add( DrinkDto drink, IEnumerable< IngredientDto > newIngredients );

		[ OperationContract ]
		IEnumerable< DrinkDto > WithIngredients( IEnumerable< string > ingredients );
	}
}
