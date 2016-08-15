//=============================================================================
// System  : HelpAssistant
// File    : HelpEntity.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This file contains class which is a base type for all types of help elements such as:
// images, tokens, topics, external links and code entities .  
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy
// of the license should be distributed with the code.  It can also be found
// at the project website: http://www.codeplex.com/HelpAssistant. This notice, the
// author's name, and all copyright notices must remain intact in all
// applications, documentation, and source files.
//=============================================================================
namespace HelpAssistant
{
  /// <summary>
  /// Class HelpEntity.
  /// </summary>
  public abstract class HelpEntity
  {
    /// <summary>
    /// Enum elementType
    /// </summary>
    public enum elementType
    {
      /// <summary>
      /// The image
      /// </summary>
      Image,
      /// <summary>
      /// The token
      /// </summary>
      Token,
      /// <summary>
      /// The topic
      /// </summary>
      Topic,
      /// <summary>
      /// The code entity
      /// </summary>
      CodeEntity,
      /// <summary>
      /// The external link
      /// </summary>
      ExternalLink,
    }
    /// <summary>
    /// Gets the type of the element.
    /// </summary>
    /// <returns></returns>
    public abstract elementType GetElementType();
  
  }
}
