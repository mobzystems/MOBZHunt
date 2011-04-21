using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MOBZystems.MOBZHunt
{
  // The structure to use to communicate with SHFileOperation
  [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
  struct SHFILEOPSTRUCT
  {
    public System.IntPtr hWnd;
    public System.UInt32 wFunc;
    [MarshalAs(UnmanagedType.LPTStr)]
    public System.String pFrom;
    [MarshalAs(UnmanagedType.LPTStr)]
    public System.String pTo;
    public System.UInt16 fFlags;
    public System.UInt32 fAnyOperationsAborted;
    public System.IntPtr hNameMappings;
    [MarshalAs(UnmanagedType.LPTStr)]
    public System.String lpszProgressTitle;
  }

  // The possible values for flag
  public enum foFileOperationFlag
  {
    FOF_MULTIDESTFILES = 0x1,             // Automatic
    FOF_CONFIRMMOUSE = 0x2,
    FOF_SILENT = 0x4,
    FOF_RENAMEONCOLLISION = 0x8,
    FOF_NOCONFIRMATION = 0x10,            // Automatic
    FOF_WANTMAPPINGHANDLE = 0x20,
    FOF_ALLOWUNDO = 0x40,
    FOF_FILESONLY = 0x80,
    FOF_SIMPLEPROGRESS = 0x100,
    FOF_NOCONFIRMMKDIR = 0x200
  }

  /// <summary>
  /// Exception, thrown when SHFileOperation failed
  /// </summary>
  public class FileOperationFailedException: ApplicationException
  {
    public FileOperationFailedException(string message) : base(message)
    {
      base.Source = "FileOperation";
    }
  }

  /// <summary>
  /// Exception, thrown when SHFileOperation aborted
  /// </summary>
  public class FileOperationAbortedException: ApplicationException
  {
    public FileOperationAbortedException() : base("Operation aborted")
    {
      this.Source = "FileOperation";
    }
  }

  /// <summary>
  /// Class FileOperation. Perfoms a number of file operations using the Windows interface.
  /// Supports Move, Delete and Copy for multiple files.
  /// </summary>
  public class FileOperation
  {
    // The file operations supported
    private enum foFileOperation
    {
      FO_MOVE = 0x1,
      FO_COPY = 0x2,
      FO_DELETE = 0x3,
      FO_RENAME = 0x4
    }
    
    [DllImport("shell32.Dll", CharSet=CharSet.Auto)]
    private static extern System.Int32 SHFileOperation(ref SHFILEOPSTRUCT lpFileOp);

    // The parent form for the status window
    private Form parentForm;
    // A collection of flags
    private ushort flags;
    // When true, skip confirmation messages
    private bool noConfirmation;

    /// <summary>
    /// Property ParentForm (Form). Get/Set the parent form of the progress window
    /// </summary>
    public Form ParentForm
    {
      get
      {
        return this.parentForm;
      }
      set
      {
        this.parentForm = value;
      }
    }

    /// <summary>
    /// Property Flags (short). Collection of flags. Set implicitly, but can be set explicitly as well.
    /// </summary>
    public ushort Flags
    {
      get
      {
        return this.flags;
      }
      set
      {
        this.flags = value;
      }
    }
    
    /// <summary>
    /// Property NoConfirmation (bool). Set to true to suppress confirmation messages
    /// </summary>
    public bool NoConfirmation
    {
      get
      {
        return this.noConfirmation;
      }
      set
      {
        this.noConfirmation = value;
      }
    }

    // Prepare an SHFILEOPSTRUCT for use, using the properties of this object:
    private void Prepare(ref SHFILEOPSTRUCT SH, foFileOperation fileOperation, string[] from, string[] to)
    {
      // Set Any operation Aborted to FALSE:
      SH.fAnyOperationsAborted = 0;
      // No name mappings wanted (TODO)
      SH.hNameMappings = System.IntPtr.Zero;
      // Set the parent form handle:
      if (parentForm == null)
        SH.hWnd = System.IntPtr.Zero;
      else
          SH.hWnd = parentForm.Handle;
      
      // SH.lpszProgressTitle = m_sTitle; -- not supported
      
      // Set the flags, add FOR_NOCONFIRMATION if necessary:
      SH.fFlags = this.flags;
      if (this.noConfirmation) 
      {
        SH.fFlags |= (ushort)foFileOperationFlag.FOF_NOCONFIRMATION;
      }

      // Build the to and from names from the arrays. Each should be a list of \0-terminated names
      // with a final \0 terminator at the very end.
      string fromFile = "";
      foreach (string file in from)
      {
        fromFile += file + "\0";
      }
      fromFile += "\0";
      SH.pFrom = fromFile;

      // If not deleting, we need another list, this time the To list:
      if (fileOperation != foFileOperation.FO_DELETE) 
      {
        if (to != null) 
        {
          string toFile = "";
          foreach (string file in to) 
          {
            toFile += file + "\0";
          }
          toFile += "\0";
          SH.pTo = toFile;
        }
      }

      // Indicate we're using multiple names, if
      // 1) we have more than one TO file or 
      // 2) we do NOT have a backslash at the end of the only TO file
      if ((to != null && to.Length > 0) || (to != null && to.Length == 1 && !to[0].EndsWith("\\"))) 
      {
        SH.fFlags |= (ushort)foFileOperationFlag.FOF_MULTIDESTFILES;
      }

      // Set the function code:
      SH.wFunc = (System.UInt32)fileOperation;
    }

    // Execute an operation set in SH. Throw a FileOperationFailedException with the provided error message
    // if there was an error executing the operation.
    private void Execute(ref SHFILEOPSTRUCT SH, string errorMessage)
    {
      // Was there an error code?
      if (SHFileOperation(ref SH) != 0) 
      {
        // Throw an Aborted operation if any part of the operation as cancelled
        if (SH.fAnyOperationsAborted != 0) 
        {
          throw new FileOperationAbortedException();
        }
        else 
        {
          // There must have been another error...
          throw new FileOperationFailedException(errorMessage);
        }
      } 
      else 
      {
        // Throw an Aborted operation if any part of the operation as cancelled
        if (SH.fAnyOperationsAborted != 0) 
        {
          throw new FileOperationAbortedException();
        }
      }
    }

    // Single file copy operation:
    public void Copy(string fromFile, string toFile)
    {
      string[] from = new string[1];
      string[] to = new string[1];

      from[0] = fromFile;
      to[0] = toFile;

      Copy(from, to);
    }

    // Copy multiple files to a directory:
    public void Copy(string[] from, string toDir)
    {
      string[] to = new string[1];

      if (!toDir.EndsWith(@"\")) 
      {
        toDir += @"\";
      }
      to[0] = toDir;

      Copy(from, to);
    }

    // Multiple file copy operation:
    public void Copy(string[] from, string[] to)
    {
      SHFILEOPSTRUCT SH = new SHFILEOPSTRUCT();

      Prepare(ref SH, foFileOperation.FO_COPY, from, to);
      Execute(ref SH, "Copy operation error");
    }

    // Single file move operation:
    public void Move(string fromFile, string toFile)
    {
      string[] from = new string[1];
      string[] to = new string[1];

      from[0] = fromFile;
      to[0] = toFile;

      Move(from, to);
    }

    // Move multiple files to a directory:
    public void Move(string[] from, string toDir)
    {
      string[] to = new string[1];

      if (!toDir.EndsWith(@"\")) 
      {
        toDir += @"\";
      }
      to[0] = toDir;

      Move(from, to);
    }

    //  Multiple file move operation:
    public void Move(string[] from, string[] to)
    {
      SHFILEOPSTRUCT SH = new SHFILEOPSTRUCT();

      Prepare(ref SH, foFileOperation.FO_MOVE, from, to);
      Execute(ref SH, "Move operation error");
    }

    // Single file delete operation:
    public void Delete(string file, bool recycle)
    {
      string[] from = new string[1];

      from[0] = file;
      Delete(from, recycle);
    }

    // Multiple file delete operation:
    public void Delete(string[] files, bool recycle)
    {
      SHFILEOPSTRUCT SH = new SHFILEOPSTRUCT();

      Prepare(ref SH, foFileOperation.FO_DELETE, files, null);
      if (recycle) 
      {
        SH.fFlags |= (ushort)foFileOperationFlag.FOF_ALLOWUNDO;
      }
      Execute(ref SH, "Delete operation error");
    }
  }
}
