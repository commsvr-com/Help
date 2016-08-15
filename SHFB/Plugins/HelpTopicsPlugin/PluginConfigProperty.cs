//<summary>
//  Title   : Property class for plugin configuration
//  System  : Microsoft Visual C# .NET 2008
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//
//  Copyright (C)2009, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto://techsupp@cas.eu
//  http://www.cas.eu
//</summary>
      

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace HelpTopicsPlugin
{
  public class PluginConfigProperty
  {
    private string _SiteURL;
    private bool _GenerateSiteMap;
    private bool _GenerateAllTopics;

    [CategoryAttribute( "SiteMap" ), DescriptionAttribute( "Add URL to use in SiteMap (default http://www.commsvr.com/UAModelDesigner/Index.aspx)" )]
    public string SiteUrl
    {
      get { return _SiteURL; }
      set { _SiteURL = value; }
    }

    [CategoryAttribute( "SiteMap" ), DescriptionAttribute( "Enable or disable SitMap generation." ), DefaultValue( true )]
    public bool GenerateSiteMap
    {
      get { return _GenerateSiteMap; }
      set { _GenerateSiteMap = value; }
    }

    [CategoryAttribute( "AllTopics" ), DescriptionAttribute( "Enable or disable AllTopics generation." ), DefaultValue( true )]
    public bool GenerateAllTopics
    {
      get { return _GenerateAllTopics; }
      set { _GenerateAllTopics = value; }
    }
  }
}
