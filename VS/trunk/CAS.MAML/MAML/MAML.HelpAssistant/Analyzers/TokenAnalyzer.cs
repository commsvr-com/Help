//=============================================================================
// System  : HelpAssistant
// File    : TokenAnalyzer.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This class is being used to analyze file with tokens and add informamation about 
// them to the collection of all SHFB help project elements
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
  internal class TokenAnalyzer
  {
    #region internal
    /// <summary>
    /// Gets all tokens.
    /// </summary>
    /// <param name="allElements">All elements.</param>
    /// <param name="TokensFile">The tokens file.</param>
    internal static void GetAllTokens( SortedDictionary<string, HelpEntity> allElements, FileInfo TokensFile )
    {
      try
      {
        using ( XmlTextReader xtr = new XmlTextReader( TokensFile.FullName ) )
        {
          xtr.WhitespaceHandling = WhitespaceHandling.None;
          XmlDocument xd = new XmlDocument();
          xd.Load( xtr );
          XmlNode xnode = xd.DocumentElement;
          AnalyzeTokens( xnode, allElements );
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
    /// Analyzes the tokens.
    /// </summary>
    /// <param name="xnod">The xnod from tokens file.</param>
    /// <param name="allElements">All elements of the help.</param>
    private static void AnalyzeTokens( XmlNode xnod, SortedDictionary<string, HelpEntity> allElements )
    {
      Token token;
      if ( xnod.NodeType != XmlNodeType.Element )
        return;
      XmlAttribute id = xnod.Attributes[ "id" ];
      if ( ( id != null ) && ( xnod.Name == "item" ) )
      {
        token = new Token();
        token.TokenId = id.Value;
        token.TokenFullName = ReplaceEntities( xnod.InnerText );
        if ( ( token.TokenFullName != null ) && ( token.TokenId != null ) )
          if ( allElements.ContainsKey( token.TokenId ) )
            MessageBox.Show( "The token " + token.TokenId + " is doubled. The token with doubled name cannot be added to the list ", "Tokens name doubled!", MessageBoxButton.OK, MessageBoxImage.Exclamation );
          else
            allElements.Add( token.TokenId, token );
      }
      if ( !xnod.HasChildNodes )
        return;
      foreach ( XmlNode xnodWorking in xnod.ChildNodes )
        AnalyzeTokens( xnodWorking, allElements );
    }

    /// <summary>
    /// Replaces the entities.
    /// </summary>
    /// <param name="fullName">The full text which is being represented by the token.</param>
    /// <returns>
    /// Changed full text which is being represented by the token.
    /// </returns>
    /// <exception cref="System.ArgumentException"> Appears when there is a problem with argument</exception>
    /// <exception cref="System.ArgumentNullException"> Appears when argument is null</exception>
    private static string ReplaceEntities( string fullName )
    {
      string changedName = "";
      changedName = fullName.Replace( ">", "&gt;" );
      changedName = fullName.Replace( "<", "&lt;" );
      changedName = fullName.Replace( "'", "&apos;" );
      changedName = fullName.Replace( "\"", "&quot;" );
      changedName = fullName.Replace( "&", "&amp;" );
      return changedName;
    }
    #endregion
  }
}
