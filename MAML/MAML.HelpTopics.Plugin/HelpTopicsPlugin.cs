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
using System.Reflection;
using System.Xml.XPath;
using System.Windows.Forms;
using HelpAssistant;
using HelpAssistant.HelpElementTypes;
using SandcastleBuilder.Utils;
using SandcastleBuilder.Utils.BuildEngine;
using SandcastleBuilder.Utils.PlugIn;
using SiteMapLibrary;
using CAS.MAML.HelpTopics.Content;

namespace HelpTopicsPlugin
{
  /// <summary>
  /// Main plugin class
  /// </summary>
  public class PlugInTopicsCreator: SandcastleBuilder.Utils.PlugIn.IPlugIn
  {
    #region Private data members
    private ExecutionPointCollection executionPoints;
    private BuildProcess builder;
    #endregion

    #region IPlugIn implementation
    //=====================================================================
    // IPlugIn implementation

    /// <summary>
    /// This read-only property returns a friendly name for the plug-in
    /// </summary>
    public string Name
    {
      get { return "HelpTopicsGenerator"; }
    }
    /// <summary>
    /// This read-only property returns the version of the plug-in
    /// </summary>
    public Version Version
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version;
      }
    }
    /// <summary>
    /// This read-only property returns the copyright information for the
    /// plug-in.
    /// </summary>
    public string Copyright
    {
      get
      {
        Assembly asm = Assembly.GetExecutingAssembly();
        AssemblyCopyrightAttribute copyright = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute( asm, typeof( AssemblyCopyrightAttribute ) );
        return copyright.Copyright;
      }
    }
    /// <summary>
    /// This read-only property returns a brief description of the plug-in
    /// </summary>
    public string Description
    {
      get
      {
        return "HelpTopicsGenerator plug-in creates a file which contains titles of topics, URLs of topics and anchors";
      }
    }
    /// <summary>
    /// This read-only property returns true if the plug-in should run in a partial build or false if it should not.
    /// </summary>
    /// <value>If this returns false, the plug-in will not be loaded when a partial build is performed.</value>
    public bool RunsInPartialBuild
    {
      get { return false; }
    }
    /// <summary>
    /// This read-only property returns a collection of execution points that define when the plug-in should be invoked during the build process.
    /// </summary>
    public ExecutionPointCollection ExecutionPoints
    {
      get
      {
        if ( executionPoints == null )
        {
          executionPoints = new ExecutionPointCollection();
          executionPoints.Add( new ExecutionPoint( BuildStep.CleanIntermediates, ExecutionBehaviors.After ) );
        }
        return executionPoints;
      }
    }

    /// <summary>
    /// This method is used by the Sandcastle Help File Builder to let the plug-in perform its own configuration.
    /// </summary>
    /// <param name="project">A reference to the active project</param>
    /// <param name="currentConfig">The current configuration XML fragment</param>
    /// <returns>A string containing the new configuration XML fragment</returns>
    /// <remarks>The configuration data will be stored in the help file builder project.</remarks>
    public string ConfigurePlugIn( SandcastleProject project, string currentConfig )
    {
      //TODO przeniesc calosc do plugin.
      Form ConfigurationForm = new PluginConfig(currentConfig );
      ConfigurationForm.Show();
     
      //if ( currentConfig.ToLower().ToString() == "<configuration />" )
      //{
      //  StringBuilder sbWriteConfig = new StringBuilder();
      //  sbWriteConfig.AppendLine();
      //  XmlWriterSettings settingWriter = new XmlWriterSettings();
      //  settingWriter.OmitXmlDeclaration = true;
      //  XmlWriter xmlWriter = XmlWriter.Create( sbWriteConfig, settingWriter );
      //  xmlWriter.WriteStartElement( "configuration" );
      //  xmlWriter.WriteElementString( "website", Properties.Settings.Default.website );
      //  xmlWriter.WriteEndElement();
      //  xmlWriter.Flush();
      //  xmlWriter.Close();
      //  sbWriteConfig.AppendLine();
      //  currentConfig = sbWriteConfig.ToString();       
      //}

      return currentConfig;
    }
    /// <summary>
    /// This method is used to initialize the plug-in at the start of the build process.
    /// </summary>
    /// <param name="buildProcess">A reference to the current build process.</param>
    /// <param name="configuration">The configuration data that the plug-in should use to initialize itself.</param>
    public void Initialize( BuildProcess buildProcess, XPathNavigator configuration )
    {
      builder = buildProcess;
      builder.ReportProgress( "{0} Version {1}\r\n{2}\r\n", this.Name, this.Version, this.Copyright );
    }
    /// <summary>
    /// This method is used to execute the plug-in during the build process
    /// </summary>
    /// <param name="context">The current execution context</param>
    public void Execute( ExecutionContext context )
    {
      SortedDictionary<string, HelpEntity> topicsAndAnchors = HelpContentCreator.CreateListOfTopics( builder.ProjectFolder + "/" + builder.ProjectFilename );

      try
      {
        CreateAllTopics( topicsAndAnchors );
      }
      catch ( Exception eCreateAllTopics )
      {
        
        throw new Exception("CreateAllTopics exception"+ eCreateAllTopics);
      }

      try
      {
        CreateSiteMap( topicsAndAnchors );
      }
      catch ( Exception eCreateSiteMap )
      {

        throw new Exception( "CreateSiteMap exception" + eCreateSiteMap );
      }
    }


    #endregion

    #region IDisposable implementation
    //=====================================================================
    // IDisposable implementation

    /// <summary>
    /// This handles garbage collection to ensure proper disposal of the plug-in if not done explicity with <see cref="Dispose()"/>.
    /// </summary>
    ~PlugInTopicsCreator()
    {
      this.Dispose( false );
    }

    /// <summary>
    /// This implements the Dispose() interface to properly dispose of the plug-in object.
    /// </summary>
    /// <overloads>There are two overloads for this method.</overloads>
    public void Dispose()
    {
      this.Dispose( true );
      GC.SuppressFinalize( this );
    }

    /// <summary>
    /// This can be overridden by derived classes to add their own disposal code if necessary.
    /// </summary>
    /// <param name="disposing">Pass true to dispose of the managed and unmanaged resources or false to just dispose of the 
    /// unmanaged resources.</param>
    protected virtual void Dispose( bool disposing )
    {
      // TODO: Dispose of any resources here if necessary
    }
    #endregion

    #region ExecuteMethods

    private void CreateSiteMap( SortedDictionary<string, HelpEntity> topicsAndAnchors )
    {
      SortedDictionary<string, url> dictionaryOfUrl = new SortedDictionary<string, url>();
      url url;
      Topic topic;
      string topicTitle;

      url = new url();
      url.loc = Properties.Settings.Default.website;
      url.priority = (decimal)1;
      url.prioritySpecified = true;
      url.changefreq = changefreq.monthly;
      url.changefreqSpecified = true;
      url.lastmod = System.DateTime.Now.Date.ToString( "yyyy\"-\"MM\"-\"dd" );
      dictionaryOfUrl.Add( "AAARootElement", url );

      foreach ( HelpEntity he in topicsAndAnchors.Values )
      {
        topic = he as Topic;
        url = new url();
        topicTitle = topic.TopicsTitle.Replace( " ", "" );
        url.loc = Properties.Settings.Default.website + "?topic=" + "html/" + topic.TopicsGuid.ToString() + ".htm";
        url.priority = (decimal)0.8;
        url.prioritySpecified = true;
        url.changefreq = changefreq.monthly;
        url.changefreqSpecified = true;
        url.lastmod = System.DateTime.Now.Date.ToString( "yyyy\"-\"MM\"-\"dd" );
        if ( !dictionaryOfUrl.ContainsKey( topicTitle ) )
          dictionaryOfUrl.Add( topicTitle, url );
      }
      urlset urlSet = new urlset( dictionaryOfUrl );
      FileInfo siteMapFile = new FileInfo( builder.OutputFolder.ToString() + "/" + Properties.Settings.Default.SiteMapFile );
      urlSet.WriteToXML( siteMapFile );
    }

    private void CreateAllTopics( SortedDictionary<string, HelpEntity> topicsAndAnchors )
    {
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
      FileInfo topicsFile = new FileInfo( builder.OutputFolder.ToString() + "/" + Properties.Settings.Default.HelpDocumentationAllTopics );
      tpcs.WriteToXML( topicsFile );
    }
    #endregion

  }
}