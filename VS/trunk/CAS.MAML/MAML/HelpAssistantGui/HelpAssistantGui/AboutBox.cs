//=============================================================================
// System  : HelpAssistant
// File    : AboutBox.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// This class represents AboutBox with information about application
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy
// of the license should be distributed with the code.  It can also be found
// at the project website: http://www.codeplex.com/HelpAssistant. This notice, the
// author's name, and all copyright notices must remain intact in all
// applications, documentation, and source files.
//=============================================================================

using System;
using System.Reflection;
using System.Windows.Forms;

namespace HelpAssistantGui
{
  partial class AboutBox: Form
  {
    public AboutBox()
    {
      InitializeComponent();
      this.Text = String.Format( "About {0}", AssemblyTitle );
      this.labelProductName.Text = AssemblyProduct;
      this.labelVersion.Text = String.Format( "Version {0}", AssemblyVersion );
      this.labelCopyright.Text = AssemblyCopyright;
      this.labelCompanyName.Text = AssemblyCompany;
      this.textBoxDescription.Text = AssemblyDescription;
    }

    #region Assembly Attribute Accessors

    public string AssemblyTitle
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyTitleAttribute ), false );
        if ( attributes.Length > 0 )
        {
          AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[ 0 ];
          if ( titleAttribute.Title != "" )
          {
            return titleAttribute.Title;
          }
        }
        return System.IO.Path.GetFileNameWithoutExtension( Assembly.GetExecutingAssembly().CodeBase );
      }
    }

    public string AssemblyVersion
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }
    }

    public string AssemblyDescription
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyDescriptionAttribute ), false );
        if ( attributes.Length == 0 )
        {
          return "";
        }
        return ( (AssemblyDescriptionAttribute)attributes[ 0 ] ).Description;
      }
    }

    public string AssemblyProduct
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyProductAttribute ), false );
        if ( attributes.Length == 0 )
        {
          return "";
        }
        return ( (AssemblyProductAttribute)attributes[ 0 ] ).Product;
      }
    }

    public string AssemblyCopyright
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyCopyrightAttribute ), false );
        if ( attributes.Length == 0 )
        {
          return "";
        }
        return ( (AssemblyCopyrightAttribute)attributes[ 0 ] ).Copyright;
      }
    }

    public string AssemblyCompany
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes( typeof( AssemblyCompanyAttribute ), false );
        if ( attributes.Length == 0 )
        {
          return "";
        }
        return ( (AssemblyCompanyAttribute)attributes[ 0 ] ).Company;
      }
    }
    #endregion
  }
}
