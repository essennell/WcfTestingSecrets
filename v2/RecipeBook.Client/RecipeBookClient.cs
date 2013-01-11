using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using Recipes;
using Recipes.Contract;

namespace RecipeBook.Client
{
	public class RecipeBookClient : Recipes.RecipeBook
	{
		public RecipeBookClient( string address )
		{
			const string name = "ShakerService";

			var cfgMap = new ExeConfigurationFileMap 
				{ ExeConfigFilename = "RecipeBook.Client.Config" };
			var config = ConfigurationManager.OpenMappedExeConfiguration
				( cfgMap, ConfigurationUserLevel.None );
			channel = new ConfigurationChannelFactory< RecipeBookContract >
				( name, config, new EndpointAddress( address ) );
			
			recipes = channel.CreateChannel();
		}

		public void Dispose()
		{
			channel.Close();
		}

		public IEnumerable< Drink > AllDrinks
		{
			get { return Call( () => recipes.AllDrinks()
				.Select( d => new DrinkAdapter( recipes, d ) ) ); }
		}

		public IEnumerable< string > AllIngredients
		{
			get { return Call( () => recipes.AllIngredients() ); }
		}

		public void Add( params Drink[] newDrinks )
		{
			foreach( var drink in newDrinks )
			{
				Call( () => 
					recipes.Add( new DrinkDto { Name = drink.Name, Method = drink.Method },
					drink.Ingredients.Select( i => new IngredientDto {	
						Name = i.Name, 
						Amount = ( IngredientDto.Measurement )i.Amount, 
						Qty = i.Qty 
					} ) ) );
			}
		}

		public IEnumerable< Drink > WithIngredients( params string[] s )
		{
			return Call( () => recipes.WithIngredients( s )
				.Select( d => new DrinkAdapter( recipes, d ) ) );
		}

		public ResultType Call< ResultType >( Func< ResultType > method )
		{
			var result = default( ResultType );
			Call( () => { result = method(); } );
			return result;
		}

		public void Call( Action method )
		{
			try
			{
				method();
			}
			catch( FaultException x )
			{
				switch( x.Message )
				{
					case "DuplicateDrink":
						throw new DuplicateDrinkException( x.Message );						
				}
				throw;
			}
		}

		private readonly ConfigurationChannelFactory< RecipeBookContract > channel;
		private readonly RecipeBookContract recipes;
	}
}
