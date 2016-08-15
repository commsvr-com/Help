//=============================================================================
// System  : HelpAssistant
// File    : Topic.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This class represents data of topic link.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy
// of the license should be distributed with the code.  It can also be found
// at the project website: http://www.codeplex.com/HelpAssistant. This notice, the
// author's name, and all copyright notices must remain intact in all
// applications, documentation, and source files.
//=============================================================================

using System;
using System.Collections.Generic;

namespace HelpAssistant.HelpElementTypes
{
  public class Topic: HelpEntity
  {
    private Guid topicsGuid;
    private List<string> anchors;
    private string topicsTitle;

    /// <summary>
    /// Gets or sets the topics GUID.
    /// </summary>
    /// <value>The topics GUID.</value>
    public Guid TopicsGuid
    {
      get { return topicsGuid; }
      set { topicsGuid = value; }
    }

    /// <summary>
    /// Gets or sets the anchors.
    /// </summary>
    /// <value>The anchors.</value>
    public List<string> Anchors
    {
      get { return anchors; }
      set { anchors = value; }
    }

    /// <summary>
    /// Gets or sets the topics title.
    /// </summary>
    /// <value>The topics title.</value>
    public string TopicsTitle
    {
      get { return topicsTitle; }
      set { topicsTitle = value; }
    }

    /// <summary>
    /// Gets the type of the element.
    /// </summary>
    /// <returns></returns>
    public override elementType GetElementType()
    {
      return elementType.Topic;
    }
  }
}
