using System;
using System.IO;
using System.Resources;
using System.Collections;

namespace Tools
{
	/// <summary>
	/// 
	/// </summary>
	public class ResourceAccess : System.Object
	{
		#region "Member Fields"
		private String ResourceFile;
		private IDictionaryEnumerator Enumerator;
		#endregion

		public ResourceAccess()
		{
		}
		
		public ResourceAccess( String ResourceFile )
		{
			this.ResourceFile = ResourceFile;
		}

		public Boolean Load()
		{
			if( ! File.Exists( ResourceFile ))
				throw new IOException();

			ResourceReader Reader = new ResourceReader( ResourceFile );
			Enumerator = Reader.GetEnumerator();

			if( Enumerator == null )
				return false;
			
			return true;
		}

		private String FindResourceByName( String Name )
		{
			if( Enumerator == null )
				throw new NullReferenceException();

			Enumerator.Reset();

			while ( ! Enumerator.MoveNext() ) 
			{
				if( Name == Enumerator.Key.ToString() )
					return Enumerator.Value.ToString();
			}

			return String.Empty;
		}

		public String GetString( String Name )
		{
			try
			{
				return FindResourceByName( Name );
			}
			catch ( Exception ) 
			{
				return String.Empty;
			}
		}
	}
}
