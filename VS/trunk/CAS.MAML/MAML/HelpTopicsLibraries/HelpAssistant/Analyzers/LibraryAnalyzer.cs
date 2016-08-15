//=============================================================================
// System  : HelpAssistant
// File    : LibraryAnalyzer.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This class is being used to analyze XML files which represents methods in dll files
// and get information about 
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
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml;
using HelpAssistant.HelpElementTypes;

namespace HelpAssistant.Analyzers
{
  internal class LibraryAnalyzer
  {
    #region internal
    /// <summary>
    /// Gets all library links.
    /// </summary>
    /// <param name="allElements">All elements from help.</param>
    /// <param name="AllLibraryFiles">All library files.</param>
    internal static void GetAllLibraryLinks( SortedDictionary<string, HelpEntity> allElements, List<FileInfo> AllLibraryFiles )
    {
      foreach ( FileInfo libraryFilePath in AllLibraryFiles )
      {
        if ( !libraryFilePath.Exists )
          continue;
        try
        {
          using ( XmlTextReader xtr = new XmlTextReader( libraryFilePath.FullName ) )
          {
            xtr.WhitespaceHandling = WhitespaceHandling.None;
            XmlDocument xd = new XmlDocument();
            xd.Load( xtr );
            XmlNode xnode = xd.DocumentElement;
            AnalyzeLibrary( xnode, allElements );
          }
        }
        catch ( Exception )
        {
          return;
        }
      }
      return;
    }
    #endregion

    #region private
    /// <summary>
    /// Analyzes the library.
    /// </summary>
    /// <param name="xnod">The xnode which is being analyzed.</param>
    /// <param name="allElements">All elements from help.</param>
    private static void AnalyzeLibrary( XmlNode xnod, SortedDictionary<string, HelpEntity> allElements )
    {
      CodeEntity codeEntity;
      if ( xnod.NodeType != XmlNodeType.Element )
        return;
      if ( ( xnod.Name == "member" ) && ( xnod.Attributes[ "name" ].Value != null ) )
      {
        Regex regex = new Regex(@"\s+");
        codeEntity = new CodeEntity();
        codeEntity.CodeEntityName = xnod.Attributes[ "name" ].Value;
        XmlNode summaryXnode = xnod.FirstChild;
        if ( summaryXnode.Name == "summary" )
          codeEntity.CodeEntitySummary =  regex.Replace(summaryXnode.InnerText.Trim()," ");
        if ( allElements.ContainsKey( codeEntity.CodeEntityName ) )
          MessageBox.Show( "The code entity " + codeEntity.CodeEntityName + " is doubled. The doubled name of the code entity cannot be added to the list ", "Code entitys name doubled!", MessageBoxButton.OK, MessageBoxImage.Exclamation );
        else
          allElements.Add( codeEntity.CodeEntityName, codeEntity );
      }
      if ( !xnod.HasChildNodes )
        return;
      foreach ( XmlNode xnodWorking in xnod.ChildNodes )
        AnalyzeLibrary( xnodWorking, allElements );
    }
    #endregion
  }
}
