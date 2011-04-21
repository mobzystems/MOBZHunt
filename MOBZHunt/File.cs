using System;

namespace MOBZystems.MOBZHunt
{
	/// <summary>
	/// Representation of a disk file
	/// </summary>
	public class File
	{
	  private long size;
	  private string name;
	  private DateTime dateModified;
    private Exception exception;

    private int imageIndex;

	  /// <summary>
	  /// Property Name (string). The name of the file
	  /// </summary>
	  public string Name
	  {
	    get
	    {
	      return this.name;
	    }
	    set
	    {
	      this.name = value;
	    }
	  }
	  
		/// <summary>
		/// Property Size (long). The size of the file
		/// </summary>
		public long Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
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
    /// Constructor
    /// </summary>
		public File()
		{
		  this.name = "";
		  this.size = 0;
		}
	}
}
