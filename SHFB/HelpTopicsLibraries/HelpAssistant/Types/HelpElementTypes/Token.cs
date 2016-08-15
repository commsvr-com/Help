//=============================================================================
// System  : HelpAssistant
// File    : Token.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This class represents data of token link.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy
// of the license should be distributed with the code.  It can also be found
// at the project website: http://www.codeplex.com/HelpAssistant. This notice, the
// author's name, and all copyright notices must remain intact in all
// applications, documentation, and source files.
//=============================================================================

namespace HelpAssistant.HelpElementTypes
{
  public class Token: HelpEntity
  {
    private string tokenId;
    private string tokenFullName;

    /// <summary>
    /// Gets or sets the token id.
    /// </summary>
    /// <value>The token id.</value>
    public string TokenId
    {
      get { return tokenId; }
      set { tokenId = value; }
    }

    /// <summary>
    /// Gets or sets the full name of the token.
    /// </summary>
    /// <value>The full name of the token.</value>
    public string TokenFullName
    {
      get { return tokenFullName; }
      set { tokenFullName = value; }
    }

    /// <summary>
    /// Gets the type of the element.
    /// </summary>
    /// <returns></returns>
    public override elementType GetElementType()
    {
      return elementType.Token;
    }
  }
}
