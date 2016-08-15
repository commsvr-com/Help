//<summary>
//  Title   : Plugin configuration GUI
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelpTopicsPlugin
{
  public partial class PluginConfig: Form
  {
    public PluginConfig()
    {
      InitializeComponent();
      AddProperties();
    }

    public PluginConfig( string currentConfig )
    {
      InitializeComponent();
      AddProperties( currentConfig );
      return;
    }

    private void AddProperties()
    {
      PluginConfigProperty Configuration = new PluginConfigProperty();
      Configuration.SiteUrl = Properties.Settings.Default.website;
      propertyGridConfiguration.SelectedObject = Configuration;
    }

    private void AddProperties( string currentConfig )
    {
      PluginConfigProperty Configuration = new PluginConfigProperty();
      //TODO Add reading properties from configuration.
      propertyGridConfiguration.SelectedObject = Configuration;

    }
  }
}
