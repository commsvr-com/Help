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

using System.Collections.Generic;
using System.IO;
using HelpAssistant;
using HelpAssistant.HelpElementTypes;
using TopicsLibrary;
using SiteMapLibrary;

namespace HelpTopicsCommand
{
  class AllTopicsCreator
  {
    private static SortedDictionary<string, TopicNode> urls = new SortedDictionary<string, TopicNode>();
    /// <summary>
    /// Main method.
    /// </summary>
    /// <param name="args">Arguments</param>
    static void Main( string[] args )
    {
      SortedDictionary<string, HelpEntity> topicsAndAnchors = HelpContentCreator.CreateListOfTopics( "z:/trunk/PR34-Documentation/PR26-DataPorter_Help.shfbproj" );
      
      SortedDictionary<string, TopicNode> dictionaryOfTopics = new SortedDictionary<string, TopicNode>();
      Topic topic;
      TopicNode node;
      foreach ( HelpEntity he in topicsAndAnchors.Values )
      {
        topic = he as Topic;
        node = new TopicNode();
        node.Title = topic.TopicsTitle.Replace( " ", "" );
        node.Url = "html/" + topic.TopicsGuid.ToString() + ".htm";
        if ( !dictionaryOfTopics.ContainsKey( node.Title ) )
          dictionaryOfTopics.Add( node.Title, node );
        foreach ( string anchor in topic.Anchors )
        {
          node = new TopicNode();
          node.Title = anchor.Replace( " ", "" );
          node.Url = "html/" + topic.TopicsGuid.ToString() + ".htm#" + anchor;
          if ( !dictionaryOfTopics.ContainsKey( node.Title ) )
            dictionaryOfTopics.Add( node.Title, node );
        }
      }
      Topics tpcs = new Topics( dictionaryOfTopics );
      FileInfo topicsFile = new FileInfo( Properties.Settings.Default.HelpDocumentationAllTopics );
      tpcs.WriteToXML( topicsFile );


      SortedDictionary<string, url> dictionaryOfUrl = new SortedDictionary<string, url>();
      url url;
      //Topic topic;
      string topicTitle;
      url = new url();
      url.loc = "http://www.commsvr.com/UAModelDesigner/Index.aspx";
      url.priority = (decimal)1;
      url.prioritySpecified = true;
      url.changefreq = changefreq.monthly;
      url.changefreqSpecified = true;
      url.lastmod = System.DateTime.Now.Date.ToString();
      dictionaryOfUrl.Add( "RootElement", url );

      foreach ( HelpEntity he in topicsAndAnchors.Values )
      {
        topic = he as Topic;
        url = new url();
        topicTitle = topic.TopicsTitle.Replace( " ", "" );
        url.loc = "http://www.commsvr.com/UAModelDesigner/Index.aspx" + "?topic=" + "html/" + topic.TopicsGuid.ToString() + ".htm";
        url.priority = (decimal)0.8;
        url.prioritySpecified = true;
        url.changefreq = changefreq.monthly;
        url.changefreqSpecified = true;
        url.lastmod = System.DateTime.Now.Date.ToString();
        if ( !dictionaryOfUrl.ContainsKey( topicTitle ) )
          dictionaryOfUrl.Add( topicTitle, url );
      }
      urlset urlSet = new urlset( dictionaryOfUrl );
      FileInfo siteMapFile = new FileInfo( "z:/trunk/PR34-Documentation" + "/" + "sitMap.xml" );
      urlSet.WriteToXML( siteMapFile );
    }
  }
}
