using System;

namespace MOBZystems.MOBZHunt
{
	public enum FolderCompareType
	{
		Name,
		Size,
		DateModified,
		Type
	}
	
	public enum FolderCompareOrder
	{
		Ascending,
		Descending
	}
	
	/// <summary>
	/// Class to use for comparing folders
	/// </summary>
	public class FolderComparer: System.Collections.IComparer
	{
		private FolderCompareType folderCompareType;
		private FolderCompareOrder folderCompareOrder;
		
		public FolderComparer(FolderCompareType fct, FolderCompareOrder fco)
		{
			this.folderCompareType = fct;
			this.folderCompareOrder = fco;
		}
		
		public int Compare(object one, object other)
		{
			Folder f1;
			Folder f2; 
			int retCode;
			
			if (this.folderCompareOrder == FolderCompareOrder.Ascending) {
				f1 = (Folder)one;
				f2 = (Folder)other;
			} else {
				f1 = (Folder)other;
				f2 = (Folder)one;
			}
			
			switch (this.folderCompareType) {
			case FolderCompareType.Name:
				return String.Compare(f1.Name, f2.Name, true);
			case FolderCompareType.Size:
				if (f1.TotalFileSize == f2.TotalFileSize)
					return String.Compare(f1.Name, f2.Name, true);
				else if (f1.TotalFileSize < f2.TotalFileSize)
					return -1;
				else
					return 1;
			case FolderCompareType.DateModified:
				retCode = DateTime.Compare(f1.DateModified, f2.DateModified);
				if (retCode == 0)
					retCode = String.Compare(f1.Name, f2.Name, true);
				return retCode;
			default:
				System.Diagnostics.Debug.Assert(false, "Unknown folder sort type");
				return 0;
			}
		}
	}

	/// <summary>
	/// Class to use for comparing files
	/// </summary>
	public class FileComparer: System.Collections.IComparer
	{
		private FolderCompareType folderCompareType;
		private FolderCompareOrder folderCompareOrder;
		
		public FileComparer(FolderCompareType fct, FolderCompareOrder fco)
		{
			this.folderCompareType = fct;
			this.folderCompareOrder = fco;
		}
		
		public int Compare(object one, object other)
		{
			File f1;
			File f2; 
			
			if (this.folderCompareOrder == FolderCompareOrder.Ascending) 
			{
				f1 = (File)one;
				f2 = (File)other;
			} 
			else 
			{
				f1 = (File)other;
				f2 = (File)one;
			}
			
			switch (this.folderCompareType) 
			{
				case FolderCompareType.Name:
					return String.Compare(f1.Name, f2.Name, true);
				case FolderCompareType.Size:
					if (f1.Size == f2.Size)
						return 0;
					else if (f1.Size < f2.Size)
						return -1;
					else
						return 1;
				case FolderCompareType.DateModified:
					return DateTime.Compare(f1.DateModified, f2.DateModified);
				default:
					System.Diagnostics.Debug.Assert(false, "Unknown file sort type");
					return 0;
			}
		}
	}
}
