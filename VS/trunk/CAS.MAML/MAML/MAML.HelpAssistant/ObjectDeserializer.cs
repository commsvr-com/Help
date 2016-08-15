//=============================================================================
// System  : HelpAssistant
// File    : ObjectDeserializer.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This file contains generic class which deserializes any type of XML file.  
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy
// of the license should be distributed with the code.  It can also be found
// at the project website: http://www.codeplex.com/HelpAssistant. This notice, the
// author's name, and all copyright notices must remain intact in all
// applications, documentation, and source files.
//=============================================================================

using System.IO;
using System.Xml.Serialization;

namespace HelpAssistant
{
  internal class ObjectDeserializer<T>
  {
    #region internal
    /// <summary>
    /// Reads the XML file.
    /// </summary>
    /// <param name="fileData">The file data.</param>
    /// <returns></returns>
    internal static T ReadXMLFile( FileInfo fileData )
    {
      if ( !fileData.Exists )
        throw new IOException( "File not found" );
        T obj;
        using ( FileStream fileStream = new FileStream( fileData.FullName, FileMode.Open, FileAccess.Read ) )
        {
          XmlSerializer xmlSerializer = new XmlSerializer( typeof( T ) );
          obj = (T)xmlSerializer.Deserialize( fileStream );
        }
        return obj;
    }
    #endregion
  }
}
