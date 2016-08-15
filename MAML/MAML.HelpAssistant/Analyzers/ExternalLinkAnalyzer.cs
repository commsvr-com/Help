//=============================================================================
// System  : HelpAssistant
// File    : ExternalLinkAnalyzer.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This class is being used to analyze file externalLinks.exlf with previously 
// saved external links and get infomration about all those links.
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
  internal class ExternalLinkAnalyzer
  {
    #region internal
    /// <summary>
    /// Gets all external links.
    /// </summary>
    /// <param name="allElements">All elements.</param>
    /// <param name="linksFileInfo">The links file info.</param>
    internal static void GetAllExternalLinks( SortedDictionary<string, HelpEntity> allElements, FileInfo linksFileInfo )
    {
      try
      {
        using ( XmlTextReader xtr = new XmlTextReader( linksFileInfo.FullName ) )
        {
          xtr.WhitespaceHandling = WhitespaceHandling.None;
          XmlDocument xd = new XmlDocument();
          xd.Load( xtr );
          XmlNode xnode = xd.DocumentElement;
          AnalyzeExternalLinks( xnode, allElements );
        }
      }
      catch ( Exception )
      {
        return;
      }
      return;
    }
    #endregion

    #region private
    /// <summary>
    /// Analyzes the external links.
    /// </summary>
    /// <param name="xnod">The xml node.</param>
    /// <param name="allElements">All elements.</param>
    private static void AnalyzeExternalLinks( XmlNode xnod, SortedDictionary<string, HelpEntity> allElements )
    {
      ExternalLink externalLink;
      if ( xnod.NodeType != XmlNodeType.Element )
        return;
      if ( xnod.Name == "externalLink" )
      {
        externalLink = new ExternalLink();
        xnod = xnod.FirstChild;
        if ( !string.IsNullOrEmpty( xnod.InnerText ) )
          externalLink.LinkText = xnod.InnerText;
        else
          return;
        xnod = xnod.NextSibling;
        if ( !string.IsNullOrEmpty( xnod.InnerText ) )
          externalLink.LinkAlternateText = xnod.InnerText;
        else
          return;
        xnod = xnod.NextSibling;
        if ( !string.IsNullOrEmpty( xnod.InnerText ) && ( Uri.IsWellFormedUriString( xnod.InnerText, UriKind.RelativeOrAbsolute ) ) )
          externalLink.LinkUri = new Uri( xnod.InnerText );
        else
          return;
        if ( allElements.ContainsKey( externalLink.LinkText ) )
          MessageBox.Show( "The external link " + externalLink.LinkText + " is doubled. The doubled link cannot be added to the list ", "External link doubled!", MessageBoxButton.OK, MessageBoxImage.Exclamation );
        else
          allElements.Add( externalLink.LinkText, externalLink );
      }
      if ( !xnod.HasChildNodes )
        return;
      foreach ( XmlNode xnodWorking in xnod.ChildNodes )
        AnalyzeExternalLinks( xnodWorking, allElements );
    }
    #endregion
  }
}
