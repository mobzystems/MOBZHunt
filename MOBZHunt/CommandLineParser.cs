using System;
using System.Collections.Generic;
using System.Text;

namespace MOBZystems.MOBZHunt
{
  public class CommandLineParser
  {
    /// <summary>
    /// This method fixes a bug in .NET command line handling.
    /// When an argument on the command line is surrounded by
    /// double quotes *and* the argument ends in a backslash,
    /// the ending backslash as replaced by a double quote.
    /// 
    /// For example, when a program is started with:
    /// 
    /// test.exe "c:\"
    /// 
    /// args[1] will be c:" while it should be c:\
    /// </summary>
    public static string[] GetCommandLineArgs()
    {
      // First get the regular list of arguments (#0 is
      // the path to our own executable)
      string[] args = Environment.GetCommandLineArgs();

      // Loop over all argument except #0
      for (int i = 1; i < args.Length; i++)
      {
        // If the argument ends in a double quote:
        if (args[i].EndsWith("\""))
          // Remove the ending double quote, add backslash
          args[i] = args[i].Substring(0, args[i].Length - 1) + "\\";
      }

      // Return the (possible altered) argument list
      return args;
    }
  }
}
