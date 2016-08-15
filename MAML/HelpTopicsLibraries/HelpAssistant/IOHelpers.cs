//=============================================================================
// System  : HelpAssistant
// File    : IOHelper.cs
// Author  : Jakub Budzynski (jkbudzynski@gmail.com)
// Updated : 17/04/2009
// Note    : Copyright 2009, code developed at Technical University of Lodz, Poland 
//           by Jakub Budzynski, All rights reserved
// Removes every two dots ("..") in a string
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy
// of the license should be distributed with the code.  It can also be found
// at the project website: http://www.codeplex.com/HelpAssistant. This notice, the
// author's name, and all copyright notices must remain intact in all
// applications, documentation, and source files.
//=============================================================================

using System.IO;
using System.Text.RegularExpressions;

namespace HelpAssistant
{
  public class IOHelpers
  {    
    private static Regex isGuid = new Regex( @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled );

    /// <summary>
    /// Creates the and normalize file info.
    /// </summary>
    /// <param name="directoryName">Name of the directory.</param>
    /// <param name="fileLocation">The file location.</param>
    /// <returns>FileInfo of the file</returns>
    public static FileInfo CreateAndNormalizeFileInfo(string directoryName, string fileLocation)
    {
      return new FileInfo( Path.GetFullPath( ( Path.Combine( directoryName, fileLocation ) ).Replace( "..", "" ) ) );
    }


    /// <summary>
    /// Determines whether the specified candidate is GUID.
    /// </summary>
    /// <param name="candidate">The candidate.</param>
    /// <returns>
    /// 	<c>true</c> if the specified candidate is GUID; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsGuid( string candidate )
    {
      if ( candidate != null )
        if ( isGuid.IsMatch( candidate ) )
          return true;
      return false;
    }

  }
}
