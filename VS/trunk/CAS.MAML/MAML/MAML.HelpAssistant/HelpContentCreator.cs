//=============================================================================
// System  : HelpAssistant
// File    : HelpContentCreator.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This file contains methods which give an information about all elements in the
// SHFB project. 
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy
// of the license should be distributed with the code.  It can also be found
// at the project website: http://www.codeplex.com/HelpAssistant. This notice, the
// author's name, and all copyright notices must remain intact in all
// applications, documentation, and source files.
//=============================================================================

using System;
using System.Collections.Generic;
using HelpAssistant.Analyzers;
using System.IO;
using System.Windows;

namespace HelpAssistant
{
  /// <summary>
  /// Class HelpContentCreator.
  /// </summary>
  public class HelpContentCreator 
  {
    #region constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="HelpContentCreator"/> class.
    /// </summary>
    public HelpContentCreator() { }
    #endregion

    #region public
    /// <summary>
    /// Creates the list of topics.
    /// </summary>
    /// <param name="projectPath">The project path.</param>
    /// <returns></returns>
    public static SortedDictionary<string, HelpEntity> CreateListOfTopics( string projectPath )
    {
      SortedDictionary<string, HelpEntity> onlyTopics = new SortedDictionary<string, HelpEntity>();
      SortedDictionary<Guid, string> topicsNames = new SortedDictionary<Guid, string>();
      ProjectAnalyzer pa = new ProjectAnalyzer( projectPath );
      if ( pa.M_Project == null )
      {
        MessageBox.Show( "Deserialization of the project failed !", "Deserialization failed!", MessageBoxButton.OK, MessageBoxImage.Error );
        return null;
      }
      try
      {
        topicsNames = ProjectContentAnalyzer.GetTopicsNames( pa.ProjectContentFile );
      }
      catch ( Exception contentException )
      {
        throw ( new Exception( "Exception appears in Content analyzer: " + contentException +
          " Check if: \n - all topics are correctly added to the project \n - GUIDS are unique \n - check if the content file has correct structure" ) );
      }      
      TopicsAnalyzer.GetAllTopics( onlyTopics, pa.AllMamlFiles, topicsNames );
      return onlyTopics;
    }

    /// <summary>
    /// Creates the list of all elements.
    /// </summary>
    /// <param name="projectPath">The project path.</param>
    /// <param name="externalLinksFileInfo">The external links file info.</param>
    /// <returns></returns>
    public static SortedDictionary<string, HelpEntity> CreateListOfAllElements( string projectPath, FileInfo externalLinksFileInfo)
    {
      SortedDictionary<string, HelpEntity> allElements = new SortedDictionary<string, HelpEntity>();
      SortedDictionary<Guid, string> topicsNames = new SortedDictionary<Guid, string>();
      ExternalLinkAnalyzer.GetAllExternalLinks(allElements, externalLinksFileInfo);
      ProjectAnalyzer pa = new ProjectAnalyzer( projectPath );
      if ( pa.M_Project == null )
      {
        MessageBox.Show( "Deserialization of the project failed !", "Deserialization failed!", MessageBoxButton.OK, MessageBoxImage.Error );
        return null;
      }
      try
      {
        topicsNames = ProjectContentAnalyzer.GetTopicsNames( pa.ProjectContentFile );
      }
      catch ( Exception contentException )
      {
        MessageBox.Show(  "Exception appears in content analyzer: " + contentException +
          " Check if: \n - all topics are correctly added to the project \n - GUIDS are unique \n - check if the content file has correct structure", "Analyze failed!", MessageBoxButton.OK, MessageBoxImage.Error );
        return null;
      }     
      try
      {
        ImageAnalyzer.GetAllImages( allElements, pa.AllImages );
      }
      catch ( Exception imageException)
      {
        MessageBox.Show( "Exception appears in Image analyzer: " + imageException +
          " Check if: \n - all images are correctly added to the project \n - the build action is set to Image \n - GUIDS are unique", "Analyze failed!", MessageBoxButton.OK, MessageBoxImage.Error );
        return null;
      }
      try
      {
        TokenAnalyzer.GetAllTokens( allElements, pa.TokensFile );
      }
      catch ( Exception tokenException)
      {
        MessageBox.Show( "Exception appears in Token analyzer: " + tokenException +
          " Check if: \n - all tokens are correctly added to the project \n - tokens names are unique \n - token is added \"as token\"", "Analyze failed!", MessageBoxButton.OK, MessageBoxImage.Error );
        return null;
      }
      try
      {
        LibraryAnalyzer.GetAllLibraryLinks( allElements, pa.AllLibraryFiles );
      }
      catch ( Exception libraryException )
      {
        MessageBox.Show( "Exception appears in Library analyzer: " + libraryException +
          " Check if: \n - all libraries are correctly added to the project  \n - library is added \"as library\"", "Analyze failed!", MessageBoxButton.OK, MessageBoxImage.Error );
        return null;
      }
      try
      {
        TopicsAnalyzer.GetAllTopics( allElements, pa.AllMamlFiles, topicsNames );
      }
      catch ( Exception topicsException )
      {
        MessageBox.Show( "Exception appears in Topics analyzer: " + topicsException +
          " Check if: \n - all topics are correctly added to the project \n - topic is added \"as topic\"", "Analyze failed!", MessageBoxButton.OK, MessageBoxImage.Error );
        return null;
      }
      return allElements;
    }
    #endregion

  }
}
