//=============================================================================
// System  : HelpAssistant
// File    : TopicsAnalyzer.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This class is being used to analyze each topic file and add informamation about 
// this topic and adress in it to the collection of all SHFB help project elements.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy
// of the license should be distributed with the code.  It can also be found
// at the project website: http://www.codeplex.com/HelpAssistant. This notice, the
// author's name, and all copyright notices must remain intact in all
// applications, documentation, and source files.
//=============================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;
using HelpAssistant.HelpElementTypes;

namespace HelpAssistant.Analyzers
{
  internal class TopicsAnalyzer
  {
    #region internal
    /// <summary>
    /// Gets all topics.
    /// </summary>
    /// <param name="elementsDictionary">The elements dictionary.</param>
    /// <param name="AllMamlFiles">All maml files.</param>
    /// <param name="topicsNames">The topics names.</param>
    internal static void GetAllTopics( SortedDictionary<string, HelpEntity> elementsDictionary, List<FileInfo> AllMamlFiles, SortedDictionary<Guid, string> topicsNames )
    {
      XmlElement mainElement = null;
      foreach ( FileInfo topicFilePath in AllMamlFiles )
      {
        if ( !topicFilePath.Exists )
          continue;
        if ( topicFilePath.Name.Contains( ".aml" ) == true )
        {
          Topic topic;
          topic = new Topic();
          topic.Anchors = new List<string>();
          try
          {
            using ( XmlTextReader xtr = new XmlTextReader( topicFilePath.FullName ) )
            {
              xtr.WhitespaceHandling = WhitespaceHandling.None;
              XmlDocument xd = new XmlDocument();
              xd.Load( xtr );
              XmlNode xnode = xd.DocumentElement;
              mainElement = GetMainElement( xnode, topicsNames, topic );
              if ( ( mainElement != null ) && ( mainElement is XmlNode )  )
              {
                AnalyzeDocument( mainElement, topic );
                if ( elementsDictionary.ContainsKey( topic.TopicsGuid.ToString() ) )
                  MessageBox.Show( "The GUID " + topic.TopicsGuid.ToString() + " of the topic: " + topic.TopicsTitle + " is doubled. Two elements wiht the same GUID cannot be added to the list ", "Topics GUID doubled!", MessageBoxButton.OK, MessageBoxImage.Exclamation );
                else
                  elementsDictionary.Add( topic.TopicsGuid.ToString(), topic );
              }
            }
          }
          catch ( Exception )
          {
            continue;
          }
        }
      }
      return;
    }
    #endregion

    #region private
    /// <summary>
    /// Gets the main element.
    /// </summary>
    /// <param name="xnod">The XML node.</param>
    /// <param name="topicsNames">The topics names.</param>
    /// <param name="topic">The topic.</param>
    /// <returns>Main element of the XML</returns>
    private static XmlElement GetMainElement( XmlNode xnod, SortedDictionary<Guid, string> topicsNames, Topic topic )
    {
      XmlElement mainElementName = null;
      if ( xnod.NodeType != XmlNodeType.Element )
        return null;
      if ( xnod.Name == "topic" )
      {
        if ( !IOHelpers.IsGuid( xnod.Attributes[ "id" ].Value ) )
        {
          MessageBox.Show( "GUID of the topic is invalid", "Invalid GUID!", MessageBoxButton.OK, MessageBoxImage.Exclamation );
        }
        else
        {
          topic.TopicsGuid = new Guid( xnod.Attributes[ "id" ].Value );
          if ( !topicsNames.ContainsKey( topic.TopicsGuid ) )
            return null;
          topic.TopicsTitle = topicsNames[ topic.TopicsGuid ];
          xnod = xnod.FirstChild;
          mainElementName = xnod as XmlElement;
          return mainElementName;
        }
      }
      foreach ( XmlNode xnodWorking in xnod.ChildNodes )
        GetMainElement( xnodWorking, topicsNames, topic );
      return null;
    }

    /// <summary>
    /// Analyzes the document.
    /// </summary>
    /// <param name="mainElement">The main element.</param>
    /// <param name="topic">The topic.</param>
    private static void AnalyzeDocument( XmlNode mainElement, Topic topic )
    {
      if ( mainElement.NodeType == XmlNodeType.Element )
      {
        XmlAttribute address = mainElement.Attributes[ "address" ];
        if ( address != null )
          topic.Anchors.Add( address.Value );
      }
      foreach ( XmlNode xnodWorking in mainElement.ChildNodes )
        AnalyzeDocument( xnodWorking, topic );
      return;
    }
    #endregion
  }
}