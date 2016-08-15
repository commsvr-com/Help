//=============================================================================
// System  : HelpAssistant
// File    : ExternalLink.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This class represents link to external resource.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy
// of the license should be distributed with the code.  It can also be found
// at the project website: http://www.codeplex.com/HelpAssistant. This notice, the
// author's name, and all copyright notices must remain intact in all
// applications, documentation, and source files.
//=============================================================================

using System;

namespace HelpAssistant.HelpElementTypes
{
  /// <summary>
  /// Class ExternalLink.
  /// </summary>
  /// <seealso cref="HelpAssistant.HelpEntity" />
  public class ExternalLink: HelpEntity
  {
    private string linkText;
    private string linkAlternateText;
    private Uri linkUri;
    /// <summary>
    /// Gets or sets the link text.
    /// </summary>
    /// <value>The link text.</value>
    public string LinkText
    {
      get { return linkText; }
      set { linkText = value; }
    }

    /// <summary>
    /// Gets or sets the link alternate text.
    /// </summary>
    /// <value>The link alternate text.</value>
    public string LinkAlternateText
    {
      get { return linkAlternateText; }
      set { linkAlternateText = value; }
    }

    /// <summary>
    /// Gets or sets the link URI.
    /// </summary>
    /// <value>The link URI.</value>
    public Uri LinkUri
    {
      get { return linkUri; }
      set { linkUri = value; }
    }

    /// <summary>
    /// Gets the type of the element.
    /// </summary>
    /// <returns></returns>
    public override elementType GetElementType()
    {
      return elementType.ExternalLink;
    }
  }
}
