//=============================================================================
// System  : HelpAssistantGui
// File    : WindowHelpAssistantGui.xaml.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This file contains handlers of the events, methods which create GUI elements and
// other methods which are being used to create links.
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
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HelpAssistant;
using HelpAssistant.HelpElementTypes;

namespace HelpAssistantGui
{
  /// <summary>
  /// Interaction logic for Window1.xaml
  /// </summary>
  public partial class HelpAssistantWindow: Window
  {
    FileInfo externalLinksFileInfo;
    SortedDictionary<string, HelpEntity> allHelpElements;
    private string filePath;
    bool externalLinksVisible = false;
    FileInfo startPath;

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Window1"/> class.
    /// </summary>
    public HelpAssistantWindow()
    {
      SplashScreen sc = new SplashScreen();
      sc.Show();
      System.Threading.Thread.Sleep( 3000 );
      sc.Close();
      startPath = new FileInfo( "HelpAssistantGui.exe" );
      externalLinksFileInfo = new FileInfo( "externalLinks.exlf" );
      InitializeComponent();
    }
    #endregion

    #region Private
    /// <summary>
    /// Handles the Click event of the buttonCreateExternalLink control. This control creates external link.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
    private void buttonCreateExternalLink_Click( object sender, RoutedEventArgs e )
    {
      string externalLink = "";
      string httpString = "http://";
      string linkText = textBoxLinkText.Text;
      string alternateText = textBoxLinkAlternateText.Text;
      string linkUri = textBoxUri.Text;
      if ( !Uri.IsWellFormedUriString( linkUri, UriKind.RelativeOrAbsolute ) )
      {
        MessageBox.Show( "URI is incorrect! Enter valid URI.", "Incorrect URI format!", MessageBoxButton.OK, MessageBoxImage.Error );
        return;
      }
      if ( ( linkText != "" ) && ( alternateText != "" ) && ( linkUri != "" ) )
      {
        if ( !linkUri.Contains( httpString ) )
          linkUri = httpString + linkUri;
        if ( checkBoxFormat.IsChecked == true )
          externalLink = string.Format( Properties.Resources.externalLinkStringFormatted, linkText, alternateText, linkUri );
        else
          externalLink = string.Format( Properties.Resources.externalLinkString, linkText, alternateText, linkUri );
        Clipboard.SetData( DataFormats.Text, externalLink );
        if ( allHelpElements != null )
        {
          ExternalLink el = new ExternalLink();
          el.LinkText = linkText;
          el.LinkUri = new Uri( linkUri.ToString() );
          el.LinkAlternateText = alternateText;
          if ( !allHelpElements.ContainsKey( linkText ) )
          {
            allHelpElements.Add( linkText, el );
            TreeViewItem itemExternLink = (TreeViewItem)treeViewTree.Items[ 4 ];
            if ( externalLinksVisible == false )
            {
              itemExternLink.Visibility = Visibility.Visible;
              externalLinksVisible = true;
            }
            TreeViewItem exLinkFromTheTree = new TreeViewItem();
            exLinkFromTheTree.Header = linkText;
            itemExternLink.Items.Add( exLinkFromTheTree );
            if ( WriteExternalLinkToFile( externalLink ) )
              MessageBox.Show( "External link has been copied to the system clipboard and copied to links file!", "Link created!", MessageBoxButton.OK, MessageBoxImage.Information );
          }
          else
            MessageBox.Show( "External link has been copied to the system clipboard! The file contains already this link", "Link created!", MessageBoxButton.OK, MessageBoxImage.Information );
        }
        else
        {
          if ( WriteExternalLinkToFile( externalLink ) )
            MessageBox.Show( "External link has been copied to the system clipboard and copied to links file!", "Link created!", MessageBoxButton.OK, MessageBoxImage.Information );
        }
      }
      else
        MessageBox.Show( "At least one of the fields is empty! All fields are required to create external link!", "Empty field!", MessageBoxButton.OK, MessageBoxImage.Warning );
    }

    /// <summary>
    /// Writes the external link to file.
    /// </summary>
    /// <param name="externalLink">The external link.</param>
    /// <returns>True if writing was succeful</returns>
    private bool WriteExternalLinkToFile( string externalLink )
    {
      try
      {
        StreamWriter externalLinkFileStream;
        if ( !externalLinksFileInfo.Exists )
          using ( externalLinkFileStream = new StreamWriter( new FileStream( externalLinksFileInfo.FullName, FileMode.Append, FileAccess.Write ) ) )
          {
            externalLinkFileStream.Write( "<externalLinks></externalLinks>" );
            externalLinkFileStream.Flush();
          }
        externalLinksFileInfo.Refresh();
        FileStream fs = new FileStream( externalLinksFileInfo.FullName, FileMode.Open, FileAccess.Write );
        fs.Seek( externalLinksFileInfo.Length - 16, SeekOrigin.Begin );
        using ( externalLinkFileStream = new StreamWriter( fs ) )
        {
          externalLinkFileStream.Write( "\n" + externalLink + "\r</externalLinks>" );
        }
      }
      catch ( Exception )
      {
        MessageBox.Show( "There was a problem with a file!", "Incorrect file!", MessageBoxButton.OK, MessageBoxImage.Information );
        return false;
      }
      return true;
    }

    /// <summary>
    /// Handles the Click event of the buttonClear control. It clears all fields in external link form.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
    private void buttonClear_Click( object sender, RoutedEventArgs e )
    {
      textBoxLinkText.Clear();
      textBoxUri.Clear();
      textBoxLinkAlternateText.Clear();
    }

    /// <summary>
    /// Handles the Click event of the About control. The About control shows information about application.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
    private void About_Click( object sender, RoutedEventArgs e )
    {
      AboutBox ab = new AboutBox();
      ab.ShowDialog();
      //MessageBox.Show( "Author: Jakub Budzyński \r\n\nThis computer program is protected by copyright law. Unauthorized reproduction or distribution of this program, or any portion of it, may result in severe civil and criminal penalties, and will be prosecuted to  maximum extent possible under law.", "About HelpAssistantGui", MessageBoxButton.OK, MessageBoxImage.Asterisk );
    }

    /// <summary>
    /// Handles the Click event of the Help control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
    private void Help_Click( object sender, RoutedEventArgs e )
    {
      System.Windows.Forms.Help.ShowHelp( null, Path.Combine(Path.GetFullPath( startPath.Directory.ToString() ),"HelpAssistant.chm"));
    }

    /// <summary>
    /// Handles the Click event of the Exit control. The Exit control close application.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
    private void Exit_Click( object sender, RoutedEventArgs e )
    {
      this.Close();
    }

    /// <summary>
    /// Handles the Click event of the OpenProject control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
    private void OpenProject_Click( object sender, RoutedEventArgs e )
    {
      string oldFilePath = filePath;
      System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
      ofd.Filter = "shfbproj files (*.shfbproj)|*.shfbproj";
      if ( ofd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel )
        return;
      filePath = Path.GetFullPath( ofd.FileName );
      if ( oldFilePath != filePath )
      {
        wrapPanelCodeEntity.Visibility = Visibility.Hidden;
        wrapPanelExternalLink.Visibility = Visibility.Hidden;
        wrapPanelImage.Visibility = Visibility.Hidden;
        wrapPanelToken.Visibility = Visibility.Hidden;
        wrapPanelTopic.Visibility = Visibility.Hidden;
        labelType.Visibility = Visibility.Hidden;
        labelTypeValue.Visibility = Visibility.Hidden;
        labelLink.Visibility = Visibility.Hidden;
        textBoxLink.Visibility = Visibility.Hidden;
      }
      else
        return;
      allHelpElements = HelpContentCreator.CreateListOfAllElements( filePath, externalLinksFileInfo );
      if ( allHelpElements != null )
        if ( allHelpElements.Count == 0 )
        {
          MessageBox.Show( "There are no useful elements!", "Useful elements not found!", MessageBoxButton.OK, MessageBoxImage.Warning );
          return;
        }
        else
        {
          treeViewTree.Items.Clear();
          DrawTree( allHelpElements );
          treeViewTree.Items.SortDescriptions.Clear();
          treeViewTree.Items.SortDescriptions.Add( new System.ComponentModel.SortDescription( "Header", System.ComponentModel.ListSortDirection.Ascending ) );
          foreach ( TreeViewItem tvi in treeViewTree.Items )
            tvi.Items.SortDescriptions.Add( new System.ComponentModel.SortDescription( "Header", System.ComponentModel.ListSortDirection.Ascending ) );
        }
      else
      {
        treeViewTree.Items.Clear();
        wrapPanelCodeEntity.Visibility = Visibility.Hidden;
        wrapPanelExternalLink.Visibility = Visibility.Hidden;
        wrapPanelImage.Visibility = Visibility.Hidden;
        wrapPanelToken.Visibility = Visibility.Hidden;
        wrapPanelTopic.Visibility = Visibility.Hidden;
        labelType.Visibility = Visibility.Hidden;
        labelTypeValue.Visibility = Visibility.Hidden;
        labelLink.Visibility = Visibility.Hidden;
        textBoxLink.Visibility = Visibility.Hidden;
      }
      return;
    }

    /// <summary>
    /// Draws the tree.
    /// </summary>
    /// <param name="allProjectElements">All project elements.</param>
    private void DrawTree( SortedDictionary<string, HelpEntity> allProjectElements )
    {
      CreateTreeStructure();
      bool imageVisible = false;
      bool tokensVisible = false;
      bool codeEntityVisible = false;
      bool topicsVisible = false;
      externalLinksVisible = false;
      foreach ( HelpEntity he in allProjectElements.Values )
      {
        switch ( he.GetElementType() )
        {
          case HelpEntity.elementType.Image:
            {
              TreeViewItem itemImages = (TreeViewItem)treeViewTree.Items[ 0 ];
              if ( imageVisible == false )
              {
                itemImages.Visibility = Visibility.Visible;
                imageVisible = true;
              }
              TreeViewItem image = new TreeViewItem();
              HelpAssistant.HelpElementTypes.Image img = he as HelpAssistant.HelpElementTypes.Image;
              image.Header = ( he as HelpAssistant.HelpElementTypes.Image ).ImageName;
              itemImages.Items.Add( image );
              break;
            }
          case HelpEntity.elementType.Token:
            {
              TreeViewItem itemTokens = (TreeViewItem)treeViewTree.Items[ 1 ];
              if ( tokensVisible == false )
              {
                itemTokens.Visibility = Visibility.Visible;
                tokensVisible = true;
              }
              TreeViewItem token = new TreeViewItem();
              token.Header = ( he as HelpAssistant.HelpElementTypes.Token ).TokenFullName;
              itemTokens.Items.Add( token );
              break;
            }
          case HelpEntity.elementType.CodeEntity:
            {
              TreeViewItem itemCodeEntity = (TreeViewItem)treeViewTree.Items[ 2 ];
              if ( codeEntityVisible == false )
              {
                itemCodeEntity.Visibility = Visibility.Visible;
                codeEntityVisible = true;
              }
              TreeViewItem codeEntity = new TreeViewItem();
              codeEntity.Header = ( he as HelpAssistant.HelpElementTypes.CodeEntity ).CodeEntityName;
              itemCodeEntity.Items.Add( codeEntity );
              break;
            }
          case HelpEntity.elementType.Topic:
            {
              TreeViewItem itemTopic = (TreeViewItem)treeViewTree.Items[ 3 ];
              if ( topicsVisible == false )
              {
                itemTopic.Visibility = Visibility.Visible;
                topicsVisible = true;
              }
              TreeViewItem topicFromTheTree = new TreeViewItem();
              topicFromTheTree.Header = ( he as HelpAssistant.HelpElementTypes.Topic ).TopicsTitle;
              foreach ( string anchor in ( he as HelpAssistant.HelpElementTypes.Topic ).Anchors )
              {
                TreeViewItem anchorItem = new TreeViewItem();
                anchorItem.Header = anchor;
                topicFromTheTree.Items.Add( anchorItem );
              }
              itemTopic.Items.Add( topicFromTheTree );
              break;
            }
          case HelpEntity.elementType.ExternalLink:
            {
              TreeViewItem itemExternLink = (TreeViewItem)treeViewTree.Items[ 4 ];
              if ( externalLinksVisible == false )
              {
                itemExternLink.Visibility = Visibility.Visible;
                externalLinksVisible = true;
              }
              TreeViewItem exLinkFromTheTree = new TreeViewItem();
              exLinkFromTheTree.Header = ( he as HelpAssistant.HelpElementTypes.ExternalLink ).LinkText;
              itemExternLink.Items.Add( exLinkFromTheTree );
              break;
            }
          default:
            {
              break;
            }
        }
      }
    }

    /// <summary>
    /// Creates the tree structure.
    /// </summary>
    private void CreateTreeStructure()
    {
      TreeViewItem tvi = new TreeViewItem();
      tvi = new TreeViewItem();
      tvi.Header = "Images";
      tvi.Visibility = Visibility.Collapsed;
      treeViewTree.Items.Add( tvi );
      tvi = new TreeViewItem();
      tvi.Header = "Tokens";
      tvi.Visibility = Visibility.Collapsed;
      treeViewTree.Items.Add( tvi );
      tvi = new TreeViewItem();
      tvi.Header = "Code entities";
      tvi.Visibility = Visibility.Collapsed;
      treeViewTree.Items.Add( tvi );
      tvi = new TreeViewItem();
      tvi.Header = "Topics";
      tvi.Visibility = Visibility.Collapsed;
      treeViewTree.Items.Add( tvi );
      tvi = new TreeViewItem();
      tvi.Header = "External links";
      tvi.Visibility = Visibility.Collapsed;
      treeViewTree.Items.Add( tvi );
    }

    /// <summary>
    /// Handles the Click event of the Refresh control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
    private void Refresh_Click( object sender, RoutedEventArgs e )
    {
      treeViewTree.Items.Clear();
      wrapPanelCodeEntity.Visibility = Visibility.Collapsed;
      wrapPanelImage.Visibility = Visibility.Collapsed;
      wrapPanelToken.Visibility = Visibility.Collapsed;
      wrapPanelTopic.Visibility = Visibility.Collapsed;
      textBoxLink.Visibility = Visibility.Collapsed;
      labelLink.Visibility = Visibility.Collapsed;
      labelType.Visibility = Visibility.Collapsed;
      labelTypeValue.Visibility = Visibility.Collapsed;
      try
      {
        FileInfo testProject = new FileInfo( filePath );
        if ( !testProject.Exists )
        {
          MessageBox.Show( "Project doesn't exist !", "Project doesn't exist !", MessageBoxButton.OK, MessageBoxImage.Stop );
          return;
        }
      }
      catch ( Exception )
      {
        MessageBox.Show( "Project file is incorrect or empty!", "The file is incorrect or empty!", MessageBoxButton.OK, MessageBoxImage.Stop );
        return;
      }
      allHelpElements = HelpContentCreator.CreateListOfAllElements( filePath, externalLinksFileInfo );
      if ( allHelpElements != null )
        if ( allHelpElements.Count == 0 )
        {
          MessageBox.Show( "There are no useful elements!", "Useful elements not found!", MessageBoxButton.OK, MessageBoxImage.Warning );
          return;
        }
        else
        {
          treeViewTree.Items.Clear();
          DrawTree( allHelpElements );
          treeViewTree.Items.SortDescriptions.Clear();
          treeViewTree.Items.SortDescriptions.Add( new System.ComponentModel.SortDescription( "Header", System.ComponentModel.ListSortDirection.Ascending ) );
          foreach ( TreeViewItem tvi in treeViewTree.Items )
            tvi.Items.SortDescriptions.Add( new System.ComponentModel.SortDescription( "Header", System.ComponentModel.ListSortDirection.Ascending ) );
          MessageBox.Show( "The project has been realoded", "Project reloaded", MessageBoxButton.OK, MessageBoxImage.Information );
        }
      else
      {
        treeViewTree.Items.Clear();
        wrapPanelCodeEntity.Visibility = Visibility.Hidden;
        wrapPanelExternalLink.Visibility = Visibility.Hidden;
        wrapPanelImage.Visibility = Visibility.Hidden;
        wrapPanelToken.Visibility = Visibility.Hidden;
        wrapPanelTopic.Visibility = Visibility.Hidden;
        labelType.Visibility = Visibility.Hidden;
        labelTypeValue.Visibility = Visibility.Hidden;
        labelLink.Visibility = Visibility.Hidden;
        textBoxLink.Visibility = Visibility.Hidden;
      }
      return;
    }

    /// <summary>
    /// Handles the SelectedItemChanged event of the treeViewTree control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedPropertyChangedEventArgs&lt;System.Object&gt;"/> instance containing the event data.</param>
    private void treeViewTree_SelectedItemChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
    {
      if ( treeViewTree.SelectedItem == null )
        return;
      TreeViewItem selectedItem = treeViewTree.SelectedItem as TreeViewItem;
      if ( selectedItem.Parent.GetType().ToString() == "System.Windows.Controls.TreeView" )
        return;
      textBoxLink.Visibility = Visibility.Visible;
      TreeViewItem parentItem = (TreeViewItem)selectedItem.Parent;
      if ( parentItem.Header.ToString() == "Images" )
      {
        if ( Properties.Settings.Default.doNotWantToSeeTheMessageAgain == false )
          ( new WindowInfoWithCheckbox() ).ShowDialog();
        CreateImageLink( selectedItem );
      }
      else if ( parentItem.Header.ToString() == "Tokens" )
      {
        if ( Properties.Settings.Default.doNotWantToSeeTheMessageAgain == false )
          ( new WindowInfoWithCheckbox() ).ShowDialog();
        CreateTokenLink( selectedItem );
      }
      else if ( parentItem.Header.ToString() == "Code entities" )
      {
        if ( Properties.Settings.Default.doNotWantToSeeTheMessageAgain == false )
          ( new WindowInfoWithCheckbox() ).ShowDialog();
        CreateCodeEntityLink( selectedItem );
      }
      else if ( parentItem.Header.ToString() == "Topics" )
      {
        if ( Properties.Settings.Default.doNotWantToSeeTheMessageAgain == false )
          ( new WindowInfoWithCheckbox() ).ShowDialog();
        CreateTopicLink( selectedItem );
      }
      else if ( parentItem.Header.ToString() == "External links" )
      {
        if ( Properties.Settings.Default.doNotWantToSeeTheMessageAgain == false )
          ( new WindowInfoWithCheckbox() ).ShowDialog();
        CreateExternalLink( selectedItem );
      }
      else
      {
        if ( Properties.Settings.Default.doNotWantToSeeTheMessageAgain == false )
          ( new WindowInfoWithCheckbox() ).ShowDialog();
        CreateAnchorLink( selectedItem, (TreeViewItem)selectedItem.Parent );
      }
      return;
    }

    /// <summary>
    /// Creates the external link.
    /// </summary>
    /// <param name="selectedItem">The selected item.</param>
    private void CreateExternalLink( TreeViewItem selectedItem )
    {
      wrapPanelTopic.Visibility = Visibility.Collapsed;
      wrapPanelToken.Visibility = Visibility.Collapsed;
      wrapPanelImage.Visibility = Visibility.Collapsed;
      wrapPanelCodeEntity.Visibility = Visibility.Collapsed;
      foreach ( HelpEntity he in allHelpElements.Values )
      {
        if ( ( he is ExternalLink ) && ( ( he as ExternalLink ).LinkText == selectedItem.Header.ToString() ) )
        {
          labelType.Visibility = Visibility.Visible;
          labelLink.Visibility = Visibility.Visible;
          wrapPanelExternalLink.Visibility = Visibility.Visible;
          labelTypeValue.Content = "External link";
          labelExternalLinkLinkTextValue.Content = ( he as ExternalLink ).LinkText;
          labelExternalLinkAlternateTextValue.Content = ( he as ExternalLink ).LinkAlternateText;
          labelExternalLinkUriValue.Content = ( he as ExternalLink ).LinkUri;
          string externalLink;
          if ( checkBoxFormat.IsChecked == true )
            externalLink = string.Format( Properties.Resources.externalLinkStringFormatted, ( he as ExternalLink ).LinkText, ( he as ExternalLink ).LinkAlternateText, ( he as ExternalLink ).LinkUri );
          else
            externalLink = string.Format( Properties.Resources.externalLinkString, ( he as ExternalLink ).LinkText, ( he as ExternalLink ).LinkAlternateText, ( he as ExternalLink ).LinkUri );
          textBoxLink.Text = externalLink;
          Clipboard.SetData( DataFormats.Text, externalLink );
          return;
        }
      }
    }

    /// <summary>
    /// Creates the anchor link.
    /// </summary>
    /// <param name="selectedItem">The selected item.</param>
    /// <param name="tviTopic">The topic.</param>
    private void CreateAnchorLink( TreeViewItem selectedItem, TreeViewItem tviTopic )
    {
      wrapPanelImage.Visibility = Visibility.Collapsed;
      wrapPanelToken.Visibility = Visibility.Collapsed;
      wrapPanelCodeEntity.Visibility = Visibility.Collapsed;
      wrapPanelExternalLink.Visibility = Visibility.Collapsed;
      foreach ( HelpEntity he in allHelpElements.Values )
      {
        if ( ( he is Topic ) && ( ( he as Topic ).TopicsTitle == tviTopic.Header.ToString() ) )
        {
          labelType.Visibility = Visibility.Visible;
          labelLink.Visibility = Visibility.Visible;
          labelTopicAnchor.Visibility = Visibility.Visible;
          labelTopicAnchorValue.Visibility = Visibility.Visible;
          wrapPanelTopic.Visibility = Visibility.Visible;
          labelTypeValue.Content = "Anchor";
          labelTopicGuidValue.Content = ( he as Topic ).TopicsGuid;
          labelTopicTitleValue.Content = ( he as Topic ).TopicsTitle;
          labelTopicAnchorValue.Content = selectedItem.Header;
          string topicWithAnchorLink = string.Format( Properties.Resources.anchorLinkString, ( he as Topic ).TopicsGuid.ToString(), labelTopicAnchorValue.Content.ToString() );
          textBoxLink.Text = topicWithAnchorLink;
          Clipboard.SetData( DataFormats.Text, topicWithAnchorLink );
          return;
        }
      }
    }

    /// <summary>
    /// Creates the topic link.
    /// </summary>
    /// <param name="selectedItem">The selected item.</param>
    private void CreateTopicLink( TreeViewItem selectedItem )
    {
      wrapPanelImage.Visibility = Visibility.Collapsed;
      wrapPanelToken.Visibility = Visibility.Collapsed;
      wrapPanelCodeEntity.Visibility = Visibility.Collapsed;
      wrapPanelExternalLink.Visibility = Visibility.Collapsed;
      labelTopicAnchor.Visibility = Visibility.Collapsed;
      labelTopicAnchorValue.Visibility = Visibility.Collapsed;
      foreach ( HelpEntity he in allHelpElements.Values )
      {
        if ( ( he is Topic ) && ( ( he as Topic ).TopicsTitle == selectedItem.Header.ToString() ) )
        {
          labelType.Visibility = Visibility.Visible;
          labelLink.Visibility = Visibility.Visible;
          wrapPanelTopic.Visibility = Visibility.Visible;
          labelTypeValue.Content = "Topic";
          labelTopicGuidValue.Content = ( he as Topic ).TopicsGuid;
          labelTopicTitleValue.Content = ( he as Topic ).TopicsTitle;
          textBoxLink.Text = string.Format( Properties.Resources.topicLinkString, ( he as Topic ).TopicsGuid.ToString() );
          Clipboard.SetData( DataFormats.Text, string.Format( Properties.Resources.topicLinkString, ( he as Topic ).TopicsGuid.ToString() ) );
          return;
        }
      }
    }

    /// <summary>
    /// Creates the image link.
    /// </summary>
    /// <param name="selectedItem">The selected item.</param>
    private void CreateImageLink( TreeViewItem selectedItem )
    {
      FileInfo projectFile = new FileInfo( filePath );
      wrapPanelTopic.Visibility = Visibility.Collapsed;
      wrapPanelToken.Visibility = Visibility.Collapsed;
      wrapPanelCodeEntity.Visibility = Visibility.Collapsed;
      wrapPanelExternalLink.Visibility = Visibility.Collapsed;
      foreach ( HelpEntity he in allHelpElements.Values )
      {
        if ( ( he is HelpAssistant.HelpElementTypes.Image ) && ( ( he as HelpAssistant.HelpElementTypes.Image ).ImageName == selectedItem.Header.ToString() ) )
        {
          labelType.Visibility = Visibility.Visible;
          labelLink.Visibility = Visibility.Visible;
          imagePreview.Visibility = Visibility.Visible;
          wrapPanelImage.Visibility = Visibility.Visible;
          labelTypeValue.Content = "Image";
          labelImageAlternateTextValue.Content = ( he as HelpAssistant.HelpElementTypes.Image ).ImageAlternateText;
          labelImageGuidValue.Content = ( he as HelpAssistant.HelpElementTypes.Image ).ImageGuid;
          try
          {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri( projectFile.Directory + "\\" + ( he as HelpAssistant.HelpElementTypes.Image ).ImageName );
            bi.EndInit();
            imagePreview.Source = bi;
          }
          catch ( Exception ) { continue; }
          string imageLink;
          if ( checkBoxFormat.IsChecked == true )
            imageLink = string.Format( Properties.Resources.imageLinkStringFormatted, labelImageAlternateTextValue.Content, labelImageGuidValue.Content.ToString() );
          else
            imageLink = string.Format( Properties.Resources.imageLinkString, labelImageAlternateTextValue.Content, labelImageGuidValue.Content.ToString() );
          textBoxLink.Text = imageLink;
          Clipboard.SetData( DataFormats.Text, imageLink );
          return;
        }
      }
    }

    /// <summary>
    /// Creates the code entity link.
    /// </summary>
    /// <param name="selectedItem">The selected item.</param>
    private void CreateCodeEntityLink( TreeViewItem selectedItem )
    {
      wrapPanelTopic.Visibility = Visibility.Collapsed;
      wrapPanelToken.Visibility = Visibility.Collapsed;
      wrapPanelImage.Visibility = Visibility.Collapsed;
      wrapPanelExternalLink.Visibility = Visibility.Collapsed;
      foreach ( HelpEntity he in allHelpElements.Values )
      {
        if ( ( he is CodeEntity ) && ( ( he as CodeEntity ).CodeEntityName == selectedItem.Header.ToString() ) )
        {
          labelType.Visibility = Visibility.Visible;
          labelLink.Visibility = Visibility.Visible;
          wrapPanelCodeEntity.Visibility = Visibility.Visible;
          labelTypeValue.Content = "Code entity";
          labelCodeEntityNameValue.Content = ( he as CodeEntity ).CodeEntityName;
          textBoxCodeEntitySummaryValue.Text = ( he as CodeEntity ).CodeEntitySummary;
          string codeEntityLink;
          if ( checkBoxFormat.IsChecked == true )
            codeEntityLink = string.Format( Properties.Resources.codeEntityLinkStringFormatted, ( he as CodeEntity ).CodeEntityName );
          else
            codeEntityLink = string.Format( Properties.Resources.codeEntityLinkString, ( he as CodeEntity ).CodeEntityName );
          textBoxLink.Text = codeEntityLink;
          Clipboard.SetData( DataFormats.Text, codeEntityLink );
          return;
        }
      }
    }

    /// <summary>
    /// Creates the token link.
    /// </summary>
    /// <param name="selectedItem">The selected item.</param>
    private void CreateTokenLink( TreeViewItem selectedItem )
    {
      wrapPanelTopic.Visibility = Visibility.Collapsed;
      wrapPanelCodeEntity.Visibility = Visibility.Collapsed;
      wrapPanelImage.Visibility = Visibility.Collapsed;
      wrapPanelExternalLink.Visibility = Visibility.Collapsed;
      foreach ( HelpEntity he in allHelpElements.Values )
      {
        if ( ( he is Token ) && ( ( he as Token ).TokenFullName == selectedItem.Header.ToString() ) )
        {
          labelType.Visibility = Visibility.Visible;
          labelLink.Visibility = Visibility.Visible;
          wrapPanelToken.Visibility = Visibility.Visible;
          labelTypeValue.Content = "Token";
          labelTokenIdValue.Content = ( he as Token ).TokenId;
          labelTokenFullNameValue.Content = ( he as Token ).TokenFullName;
          string tokenLink;
          if ( checkBoxFormat.IsChecked == true )
            tokenLink = string.Format( Properties.Resources.tokenLinkStringFormatted, ( he as Token ).TokenId );
          else
            tokenLink = string.Format( Properties.Resources.tokenLinkString, ( he as Token ).TokenId );
          textBoxLink.Text = tokenLink;
          Clipboard.SetData( DataFormats.Text, tokenLink );
          return;
        }
      }
    }
    #endregion

  }
}
