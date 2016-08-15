//<summary>
//  Title   : Helper class for managing Topics schema
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2008, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TopicsLibrary
{
  public partial class Topics
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
          XmlSerializer xmlSerializer = new XmlSerializer( typeof( Topics ) );
          XmlSerializerNamespaces xs = new XmlSerializerNamespaces();
          xs.Add( "cas", "http://cas.eu/2009/HelpContent.xsd" );
          xmlSerializer.Serialize( stWriter, this, xs );
        }
      }
      catch { }
    }

    /// <summary>
    /// Deserializes objects from XML file
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>Deserialized object</returns>
    public static SortedDictionary<string, TopicNode> FromXmlStream( Stream stream )
    {
      try
      {
        StreamReader reader = new StreamReader( stream );
        Topics tpcs = null;
        using ( reader )
        {
          XmlSerializer xmlSerializer = new XmlSerializer( typeof( Topics ) );
          tpcs = (Topics)xmlSerializer.Deserialize( reader );
        }
        if ( tpcs == null )
          return null;
        return tpcs.CreateDictionary();
      }
      catch ( Exception )
      {
        return null;
      }
    }

    /// <summary>
    /// Creates the sorted directory of topics.
    /// </summary>
    /// <returns>Sorted dictionary</returns>
    public SortedDictionary<string, TopicNode> CreateDictionary()
    {
      if ( TopicNode == null )
        return null;
      SortedDictionary<string, TopicNode> urls = new SortedDictionary<string, TopicNode>();
      foreach ( TopicNode tpc in TopicNode )
        try
        {
          urls.Add( tpc.Title, tpc );
        }
        catch ( System.Exception )
        {
          urls = null;
        }
      return urls;
    }
    #endregion public

    #region creators
    /// <summary>
    /// Initializes a new instance of the <see cref="Topics"/> class.
    /// </summary>
    /// <param name="tableOfTopics">The Sorted dictionary with topics.</param>
    public Topics( SortedDictionary<string, TopicNode> tableOfTopics )
    {
      this.TopicNode = new TopicNode[ tableOfTopics.Count ];
      try
      {
        tableOfTopics.Values.CopyTo( this.TopicNode, 0 );
      }
      catch ( Exception )
      {
        topicNodeField = new TopicNode[ 0 ];
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Topics"/> class.
    /// </summary>
    public Topics(){}
    #endregion
  }
}