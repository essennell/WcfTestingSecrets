using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;

namespace RecipeBook.Server
{
	public class RecipeBookHost : ServiceHost
	{
		public RecipeBookHost( string address )
		{
			var cfgMap = new ExeConfigurationFileMap 
				{ ExeConfigFilename = "RecipeBook.Server.config" };
			config = ConfigurationManager.OpenMappedExeConfiguration
				( cfgMap, ConfigurationUserLevel.None );

			var service = new RecipeBookService();
			InitializeDescription( service, 
				new UriSchemeKeyedCollection( new Uri( address ) ) );
			Open();
		}

		protected override void ApplyConfiguration()
		{
			var section = ServiceModelSectionGroup.GetSectionGroup( config );
			if( section == null )
				throw new ConfigurationErrorsException
					( "Failed to find service model configuration" );
			foreach( ServiceElement service in section.Services.Services )
			{
				if( service.Name == Description.ConfigurationName )
					base.LoadConfigurationSection( service );
				else
					throw new ConfigurationErrorsException
						( "No match for description in Service model config" );
			}
		}

		private readonly Configuration config;
	}
}
