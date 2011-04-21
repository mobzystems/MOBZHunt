using System;
using System.Collections;
using System.IO;
using System.ComponentModel;

namespace MOBZystems.MOBZHunt
{
	/// <summary>
	/// Summary description for Folder.
	/// </summary>
	public class Folder
	{
    private ArrayList files;
    private ArrayList folders;
    
    private string fullPath;
    private string name;
    private DateTime dateModified;
    private Exception exception;

    private long totalFileSize;

    private int imageIndex;

    // Delegate for EnterFolder event handler:
		// public delegate bool EnterFolderEventHandler(object sender, EventArgs e);
		
		/// <summary>
		/// EnterFolder event. Raised when entering a new folder
		/// during RetrieveSizes()
		/// </summary>
		public event CancelEventHandler EnterFolder;
		
		// Raise the EnterFolder event
		protected bool OnEnterFolder(Folder folder, CancelEventArgs e)
		{
      if (this.EnterFolder != null)
      {
        this.EnterFolder(folder, e);
        return e.Cancel;
      }
      else
      {
        return false;
      }
		}

		/// <summary>
		/// TotalSizeKnown event. Raised when total file size of this folder is known
		/// </summary>
		public event EventHandler TotalSizeKnown;
		
		// Raise the TotalSizeKnown event
		protected void OnTotalSizeKnown(Folder folder, EventArgs e)
		{
			if (this.TotalSizeKnown != null)
				this.TotalSizeKnown(folder, e);
		}
		
    /// <summary>
    /// Property FullPath (string). The full path of the folder
    /// </summary>
    public string FullPath
    {
      get
      {
        return this.fullPath;
      }
      set
      {
        this.fullPath = value;
        this.name = Path.GetFileName(value);
      }
    }
    
    /// <summary>
    /// Property Name (string). Set through FullPath
    /// </summary>
    public string Name
    {
      get
      {
        return this.name;
      }
    }
    
    /// <summary>
    /// DateTime of last modification
    /// </summary>
    public DateTime DateModified
		{
			get
			{
				return this.dateModified;
			}
			set
			{
				this.dateModified = value;
			}
		}
		
    /// <summary>
    /// Property Exception (Exception)
    /// </summary>
    public Exception Exception
    {
      get
      {
        return this.exception;
      }
      set
      {
        this.exception = value;
      }
    }
    
    /// <summary>
    /// Property TotalFileSize (long). The total size of all files and folders in this directory
    /// </summary>
    public long TotalFileSize
    {
      get
      {
        return this.totalFileSize;
      }
    }

    /// Property ImageIndex (int)
    /// The system image index of the file
    public int ImageIndex
    {
      get
      {
        return this.imageIndex;
      }
      set
      {
        this.imageIndex = value;
      }
    }

    /// <summary>
    /// Fill the information for every file and folder in the folder.
    /// Notifies the folderNotify folder upon entering a new directory
    /// </summary>
    public void RetrieveSizes(Folder folderNotify, SystemImageList imageList)
    {
      this.files = new ArrayList();
      this.folders = new ArrayList();
      this.totalFileSize = 0;
      
      if (folderNotify != null) 
      {
        if (folderNotify.OnEnterFolder(this, new CancelEventArgs()))
          return;
      }
			
      DirectoryInfo dirInfo = new DirectoryInfo(this.fullPath);
      
      // First, retrieve the file list for this directory:

      try
      {
        FileInfo[] fileList = dirInfo.GetFiles();
        foreach (FileInfo fileInfo in fileList) 
        {
          File file = new File();
          try 
          {
            file.Name = fileInfo.Name;
            file.Size = fileInfo.Length;
            file.DateModified = fileInfo.LastWriteTime;
            // Add to total file size:
            this.totalFileSize += file.Size;
            if (imageList == null)
              file.ImageIndex = -1;
            else
              // Add the icon of the file to the image list:
              file.ImageIndex = imageList.AddIconForFile(fileInfo.FullName, false);
          }
          catch (Exception ex)
          {
            file.Exception = ex;
          }
          finally 
          {
            files.Add(file);
          }
        }
        // Sort the array:
        files.Sort(new FileComparer(FolderCompareType.Name, FolderCompareOrder.Ascending));
      }
      catch (Exception ex)
      {
        this.exception = ex;
      }

      if (this.exception == null) 
      {
        try 
        {
          // Retrieve the folder list:
          DirectoryInfo[] folderList = dirInfo.GetDirectories();
          foreach (DirectoryInfo dir in folderList) 
          {
            Folder folder = new Folder(dir.FullName);
            try
            {
              folder.DateModified = dir.LastWriteTime;
              if (imageList == null)
                folder.ImageIndex = -1;
              else
                folder.ImageIndex = imageList.AddIconForFile(folder.FullPath, false);
            }
            catch (Exception ex)
            {
              folder.Exception = ex;
            }
            finally
            {
              folders.Add(folder);
            }

            // Retrieve the information IN the folder
            folder.RetrieveSizes(folderNotify, imageList);
            // Add to total file size:
            this.totalFileSize += folder.TotalFileSize;
          }
          folders.Sort(new FolderComparer(FolderCompareType.Name, FolderCompareOrder.Ascending));
        }
        catch (Exception ex)
        {
          this.exception = ex;
        }
      }

			// Raise the TotalSizeKnown event
			if (folderNotify != null)
				folderNotify.OnTotalSizeKnown(this, new EventArgs());
		}
    
    public void RecalcSizes()
    {
      if (this.exception == null) 
      {
        this.totalFileSize = 0;
        foreach (File file in files)
          if (file.Exception == null)
            this.totalFileSize += file.Size;

        foreach (Folder folder in folders)
          if (folder.Exception == null)
            this.totalFileSize += folder.TotalFileSize;
      }
    }

    /// <summary>
    /// Property Folders
    /// </summary>
    public ArrayList Folders
		{
			get {
				return this.folders;
			}
		}

		/// <summary>
		/// Property Files
		/// </summary>
		public ArrayList Files
		{
			get {
				return this.files;
			}
		}
		
		// Constructor with full path name
		public Folder(string path)
		{
		  this.FullPath = path;
		}
		
		public void Sort(FolderCompareType sortType, FolderCompareOrder sortOrder)
		{
			files.Sort(new FileComparer(sortType, sortOrder));
			folders.Sort(new FolderComparer(sortType, sortOrder));
		}
	}
}
