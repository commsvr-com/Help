//=============================================================================
// System  : HelpAssistant
// File    : ProjectContentAnalyzer.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This class is being used to analyze *.content file, which contains topics and adresses.
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
using System.Xml;

namespace HelpAssistant.Analyzers
{
  internal class ProjectContentAnalyzer
  {
    #region internal
    /// <summary>
    /// Gets the topics names.
    /// </summary>
    /// <param name="contentFile">The content file.</param>
    /// <returns></returns>
    internal static SortedDictionary<Guid, string> GetTopicsNames( FileInfo contentFile )
    {
      SortedDictionary<Guid, string> topicsWithGuids = new SortedDictionary<Guid, string>();
      try
      {
        //FileInfo contentFile = pa.ProjectContentFile;
        if ( !contentFile.Exists )
          return null;
        using ( XmlTextReader xtr = new XmlTextReader( contentFile.FullName ) )
        {
          xtr.WhitespaceHandling = WhitespaceHandling.None;
          XmlDocument xd = new XmlDocument();
          xd.Load( xtr );
          XmlNode xnode = xd.DocumentElement;
          AnalyzeContentFile( xnode, topicsWithGuids );
        }
      }
      catch ( Exception )
      {
        return null;
      }
      return topicsWithGuids;
    }
    #endregion

    #region private
    /// <summary>
    /// Analyzes the content file.
    /// </summary>
    /// <param name="xnod">The xnod.</param>
    /// <param name="topicsWithGuids">The topics with guids.</param>
    private static void AnalyzeContentFile( XmlNode xnod, SortedDictionary<Guid, string> topicsWithGuids )
    {
      string tocTitle;
      if ( xnod.NodeType != XmlNodeType.Element )
        return;
      if ( ( xnod.Name == "Topic" ) && ( xnod.Attributes[ "id" ] != null ) && ( xnod.Attributes[ "tocTitle" ] != null) ) {
        tocTitle = xnod.Attributes[ "tocTitle" ].Value;
        topicsWithGuids.Add( new Guid( xnod.Attributes[ "id" ].Value ), tocTitle );
      }
      if ( !xnod.HasChildNodes )
        return;
      foreach ( XmlNode xnodWorking in xnod.ChildNodes )
        AnalyzeContentFile( xnodWorking, topicsWithGuids );
    }
    #endregion
  }
}
