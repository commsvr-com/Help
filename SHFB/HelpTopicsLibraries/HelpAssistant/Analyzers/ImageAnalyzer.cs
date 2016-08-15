//=============================================================================
// System  : HelpAssistant
// File    : ImageAnalyzer.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This class is being used to add information about images to collection of all SHFB project elements
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy
// of the license should be distributed with the code.  It can also be found
// at the project website: http://www.codeplex.com/HelpAssistant. This notice, the
// author's name, and all copyright notices must remain intact in all
// applications, documentation, and source files.
//=============================================================================

using System.Collections.Generic;
using HelpAssistant.HelpElementTypes;

namespace HelpAssistant.Analyzers
{
  internal class ImageAnalyzer
  {
    #region internal
    /// <summary>
    /// Gets all images.
    /// </summary>
    /// <param name="allElements">All elements.</param>
    /// <param name="AllImages">All images.</param>
    internal static void GetAllImages( SortedDictionary<string, HelpEntity> allElements, Dictionary<string, Image> AllImages )
    {
      foreach ( HelpEntity img in AllImages.Values )
        allElements.Add( ( img as HelpAssistant.HelpElementTypes.Image ).ImageGuid.ToString(), img );
    }
    #endregion
  }
}
