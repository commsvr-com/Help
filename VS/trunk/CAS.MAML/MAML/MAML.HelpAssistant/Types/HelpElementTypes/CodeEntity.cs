//=============================================================================
// System  : HelpAssistant
// File    : CodeEntity.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This class represents code entity from dll file.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy
// of the license should be distributed with the code.  It can also be found
// at the project website: http://www.codeplex.com/HelpAssistant. This notice, the
// author's name, and all copyright notices must remain intact in all
// applications, documentation, and source files.
//=============================================================================

namespace HelpAssistant.HelpElementTypes
{
  /// <summary>
  /// Class CodeEntity.
  /// </summary>
  /// <seealso cref="HelpAssistant.HelpEntity" />
  public class CodeEntity: HelpEntity
  {
    private string codeEntityName;
    private string codeEntitySummary;
    /// <summary>
    /// Gets or sets the name of the code entity.
    /// </summary>
    /// <value>The name of the code entity.</value>
    public string CodeEntityName
    {
      get { return codeEntityName; }
      set { codeEntityName = value; }
    }

    /// <summary>
    /// Gets or sets the code entity summary.
    /// </summary>
    /// <value>The code entity summary.</value>
    public string CodeEntitySummary
    {
      get { return codeEntitySummary; }
      set { codeEntitySummary = value; }
    }

    /// <summary>
    /// Gets the type of the element.
    /// </summary>
    /// <returns></returns>
    public override elementType GetElementType()
    {
      return elementType.CodeEntity;
    }
  }
}
