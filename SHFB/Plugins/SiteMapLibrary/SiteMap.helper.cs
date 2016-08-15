//<summary>
//  Title   : Helper class for managing Google SiteMap schema
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2009, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SiteMapLibrary
{
  public partial class urlset
  {
    #region public
    /// <summary>
    /// Serialize this object and write it to XML file.
    /// </summary>
    /// <param name="filePathToSave">The path to the XML file.</param>
    public void WriteToXML( FileInfo filePathToSave )
    {
      try
      {
        using ( StreamWriter stWriter = new StreamWriter( filePathToSave.FullName ) )
        {
          XmlSerializer xmlSerializer = new XmlSerializer( typeof( urlset ) );
          xmlSerializer.Serialize( stWriter, this );
        }
      }
      catch { }
    }
    #endregion public

    #region creators

    /// <summary>
    /// Initializes a new instance of the <see cref="SiteMap"/> class.
    /// </summary>
    /// <param name="tableOfTopics">The table of topics.</param>
    public urlset( SortedDictionary<string, url> tableOfUrl )
    {
      this.url = new url[ tableOfUrl.Count ];
      try
      {
        tableOfUrl.Values.CopyTo( this.url, 0 );
      }
      catch ( Exception )
      {
        urlField = new url[ 0 ];
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SiteMap"/> class.
    /// </summary>
    public urlset() { }
    #endregion
  }
}
