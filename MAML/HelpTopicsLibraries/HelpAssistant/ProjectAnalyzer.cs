//=============================================================================
// System  : HelpAssistant
// File    : ProjectAnalyzer.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This file contains methods which analyze SHFB project file.
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
using HelpAssistant.HelpElementTypes;
using HelpAssistant.HPFT;

namespace HelpAssistant
{
  /// <summary>
  /// Class analyzes and gets information about help elements from the project file. 
  /// </summary>
  internal class ProjectAnalyzer
  {
    #region creator
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectAnalyzer"/> class.
    /// </summary>
    /// <param name="pathToTheProject">The path to the project.</param>
    public ProjectAnalyzer( string pathToTheProject )
    {
      projectFile = null;
      try
      {
        projectFile = new FileInfo( pathToTheProject );
      }
      catch ( Exception )
      {
        return;
      }
      if ( !projectFile.Exists )
        return;
      try
      {
        m_project = ObjectDeserializer<Project>.ReadXMLFile( projectFile );
      }
      catch ( Exception )
      {
        return;
      }
      return;
    }
    #endregion

    #region Private
    private Project m_project;
    private FileInfo projectFile;
    /// <summary>
    /// Finds the maml files.
    /// </summary>
    /// <param name="projectFile">The project file data.</param>
    /// <returns>List of all MAML files</returns>
    private List<FileInfo> FindMamlFiles( FileInfo projectFile )
    {
      List<FileInfo> allTopicsFiles = new List<FileInfo>();
      for ( int ix = 0; ix < m_project.PropertyGroup[ 0 ].Items.Length; ix++ )
        foreach ( ItemGroupType itemFromAGroup in m_project.ItemGroup )
          if ( itemFromAGroup.Items[ 0 ].GetType().ToString() == "HelpAssistant.HPFT.ItemGroupTypeItemNone" )
          {
            foreach ( ItemGroupTypeItemNone itemNone in itemFromAGroup.Items )
              if ( itemNone.Include != null )
                allTopicsFiles.Add( IOHelpers.CreateAndNormalizeFileInfo( projectFile.Directory.ToString(), itemNone.Include ) );
            return allTopicsFiles;
          }
      return allTopicsFiles;
    }

    /// <summary>
    /// Finds the library files.
    /// </summary>
    /// <param name="projectFile">The project file data.</param>
    /// <returns>List of all library files</returns>
    private List<FileInfo> FindLibraryFiles( FileInfo projectFile )
    {
      List<FileInfo> allDocumentationSources = new List<FileInfo>();
      foreach ( var itemFromAGroup in m_project.PropertyGroup[ 0 ].Items )
      {
        if ( ( itemFromAGroup is PropertyGroupTypePropertyDocumentationSources ) && ( itemFromAGroup as PropertyGroupTypePropertyDocumentationSources ).DocumentationSource != null )
          foreach ( PropertyGroupTypePropertyDocumentationSourcesDocumentationSource ds in ( itemFromAGroup as PropertyGroupTypePropertyDocumentationSources ).DocumentationSource )
            if ( ds.sourceFile.ToLower().Contains( "xml" ) )
              allDocumentationSources.Add( IOHelpers.CreateAndNormalizeFileInfo( projectFile.Directory.ToString(), ds.sourceFile ) );
      }
      return allDocumentationSources;
    }

    /// <summary>
    /// Finds the token files.
    /// </summary>
    /// <param name="projectFile">The project file data.</param>
    /// <returns>Token file information</returns>
    private FileInfo FindTokenFiles( FileInfo projectFile )
    {
      FileInfo tokensFile = null;
      foreach ( ItemGroupType itemFromAGroup in m_project.ItemGroup )
      {
        if ( ( itemFromAGroup.Items != null ) && ( itemFromAGroup.Items.Length == 1 ) && ( itemFromAGroup.Items[ 0 ].Include.Contains( ".tokens" ) ) )
        {
          tokensFile = IOHelpers.CreateAndNormalizeFileInfo( projectFile.Directory.ToString(), itemFromAGroup.Items[ 0 ].Include );
          if ( !tokensFile.Exists )
            return null;
        }
      }
      return tokensFile;
    }

    /// <summary>
    /// Finds the images.
    /// </summary>
    /// <param name="projectFile">The FileInfo of project file.</param>
    /// <returns>All images</returns>
    private Dictionary<string, Image> FindImages( FileInfo projectFile )
    {
      Dictionary<string, Image> allImages = new Dictionary<string, Image>();
      foreach ( ItemGroupType itemFromAGroup in m_project.ItemGroup )
      {
        if ( ( itemFromAGroup.Items == null ) || ( itemFromAGroup.Items.Length == 0 ) || !( itemFromAGroup.Items[ 0 ] is ItemDefinitionGroupTypeItemImage ) )
          continue;
        ItemDefinitionGroupTypeItemImage imageFromAGroup;
        for ( int ix = 0; ix < itemFromAGroup.Items.Length; ix++ )
        {
          imageFromAGroup = (ItemDefinitionGroupTypeItemImage)itemFromAGroup.Items[ ix ];
          if ( imageFromAGroup != null )
          {
            Image image = new Image();
            image.ImageName = imageFromAGroup.Include;
            image.ImageAlternateText = imageFromAGroup.AlternateText;
            if ( !IOHelpers.IsGuid( imageFromAGroup.ImageId ) )
            {
              MessageBox.Show( "GUID of the Image: " + image.ImageName + " is invalid", "Invalid GUID!", MessageBoxButton.OK, MessageBoxImage.Exclamation );
            }
            else
            {
              image.ImageGuid = new Guid( imageFromAGroup.ImageId );
              if ( allImages.ContainsKey( image.ImageGuid.ToString() ) )
                MessageBox.Show( "The GUID :" + image.ImageGuid.ToString() + " of the image " + image.ImageName + " is doubled. The image with doubled GUID cannot be added to the list ", "GUID doubled!", MessageBoxButton.OK, MessageBoxImage.Exclamation );
              else
                allImages.Add( image.ImageGuid.ToString(), image );
            }
          }
        }
      }
      return allImages;
    }

    /// <summary>
    /// Finds the project content files.
    /// </summary>
    /// <param name="projectFile">The project file.</param>
    /// <returns>Project content file</returns>
    private FileInfo FindProjectContentFiles( FileInfo projectFile )
    {
      FileInfo projectContentFile = null;
      foreach ( ItemGroupType itemFromAGroup in m_project.ItemGroup )
      {
        if ( ( itemFromAGroup.Items != null ) && ( itemFromAGroup.Items.Length == 1 ) && ( itemFromAGroup.Items[ 0 ].Include.Contains( ".content" ) ) )
        {
          projectContentFile = IOHelpers.CreateAndNormalizeFileInfo( projectFile.Directory.ToString(), itemFromAGroup.Items[ 0 ].Include );
          if ( !projectContentFile.Exists )
            return null;
        }
      }
      return projectContentFile;
    }
    #endregion

    #region Getters
    /// <summary>
    /// Gets or sets the tokens file.
    /// </summary>
    /// <value>The tokens file.</value>
    public FileInfo TokensFile
    {
      get { return FindTokenFiles( projectFile ); }
    }

    /// <summary>
    /// Gets or sets all library files.
    /// </summary>
    /// <value>All library files.</value>
    public List<FileInfo> AllLibraryFiles
    {
      get { return FindLibraryFiles( projectFile ); }
    }

    /// <summary>
    /// Gets or sets all maml files.
    /// </summary>
    /// <value>All maml files.</value>
    public List<FileInfo> AllMamlFiles
    {
      get { return FindMamlFiles( projectFile ); }
    }

    /// <summary>
    /// Gets or sets all images.
    /// </summary>
    /// <value>All images.</value>
    public Dictionary<string, Image> AllImages
    {
      get { return FindImages( projectFile ); }
    }

    /// <summary>
    /// Gets or sets the project content file.
    /// </summary>
    /// <value>The project content file.</value>
    public FileInfo ProjectContentFile
    {
      get { return FindProjectContentFiles( projectFile ); }
    }

    /// <summary>
    /// Gets the m_ project.
    /// </summary>
    /// <value>The m_ project.</value>
    public Project M_Project
    {
      get { return m_project; }
    }


    #endregion SettersAndGetters
  }
}
