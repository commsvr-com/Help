//=============================================================================
// System  : HelpAssistant
// File    : Image.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This class represents data of image media link.
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
  public class Image: HelpEntity
  {
    private string imageName;
    private Guid imageGuid;
    private string imageAlternateText;

    /// <summary>
    /// Gets or sets the name of the image.
    /// </summary>
    /// <value>The name of the image.</value>
    public string ImageName
    {
      get { return imageName; }
      set { imageName = value; }
    }
    /// <summary>
    /// Gets or sets the image ID.
    /// </summary>
    /// <value>The image ID.</value>
    public Guid ImageGuid
    {
      get { return imageGuid; }
      set { imageGuid = value; }
    }
    /// <summary>
    /// Gets or sets the image alternate text.
    /// </summary>
    /// <value>The image alternate text.</value>
    public string ImageAlternateText
    {
      get { return imageAlternateText; }
      set { imageAlternateText = value; }
    }
    /// <summary>
    /// Gets the type of the element.
    /// </summary>
    /// <returns></returns>
    public override elementType GetElementType()
    {
      return elementType.Image;
    }
  }
}
