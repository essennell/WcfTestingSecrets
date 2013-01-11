using System;

namespace Recipes
{
	public sealed class DuplicateDrinkException : ApplicationException
	{
		public DuplicateDrinkException( string msg, params object [] args )
			: base( string.Format( msg, args ) )
		{
		}	
	}
}
