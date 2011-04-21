using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Remoting.Messaging;
using System.IO;

// TODO: use FileSystemWatcher to detect changes
// TODO: implement filter to find files/directories (e.g. Size > 1000000)

namespace MOBZystems.MOBZHunt
{
	/// <summary>
	/// Main form of the application
	/// </summary>
	public class FormHogHunt : Form
	{
		private delegate void RetrieveSizes(Folder folder, SystemImageList imageList);
		private delegate void ProcessResults();
    private delegate void EnableUIDelegate(bool enable);
		
		private AsyncResult asyncResult = null;
		private bool stopSignal =  false;

		// private Syncfusion.Windows.Forms.FolderBrowser folderBrowser;
    private FolderBrowserDialog folderBrowser;

		private FolderCompareType sortType = FolderCompareType.Name;
		private FolderCompareOrder sortOrder = FolderCompareOrder.Ascending;

		private ArrayList folderStack;

    private SystemImageList imageList = new SystemImageList(SystemInformation.SmallIconSize);

    private Panel panelTop;
		private ComboBox comboPath;
		private TreeView treeView;
		private ListView listView;
		private ColumnHeader columnHeaderName;
		private ColumnHeader columnHeaderSize;
		private ColumnHeader columnHeaderDateModified;
		private System.ComponentModel.IContainer components;
    private Button buttonOpen;
		private ColumnHeader columnHeaderBytes;
    private ImageList imageList1;
    private ContextMenuStrip contextMenuFile;
		private ToolStripMenuItem menuItem4;
    private ContextMenuStrip contextMenuFolder;
		private ToolStripMenuItem menuItem5;
		private ToolStripMenuItem menuPopupFolderOpen;
    private ToolStripMenuItem menuPopupFolderExplore;
		private ToolStripMenuItem menuPopupFolderDelete;
		private ToolStripMenuItem menuPopupFolderRecycle;
		private ToolStripMenuItem menuPopupFileDelete;
		private ToolStripMenuItem menuPopupFileRecycle;
    private ToolStripMenuItem menuPopupFolderMakeTop;
    private ToolStripMenuItem menuPopupFileOpen;
    private ToolStripMenuItem menuPopupFileExplore;
    private ToolStripMenuItem menuPopupFileSelectAll;
    private StatusStrip mainStatusStrip;
    private ToolStripStatusLabel toolStripStatusLabel;
    private ToolStripStatusLabel toolStripStatusLink;
    private ToolStrip mainToolStrip;
    private ToolStripButton toolStripButtonBrowse;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton toolStripButtonBack;
    private ToolStripButton toolStripButtonUp;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton toolStripButtonRefresh;
    private SplitContainer splitContainer1;
    private ToolStripSeparator menuItem1;
    private ToolStripSeparator menuItem2;
    private ToolStripSeparator menuItem7;
    private ToolStripSeparator menuItem3;
    private ToolStripSeparator menuItem8;
    private ToolStripSeparator toolStripSeparator3;

    // The root folder object
		private Folder rootFolder = null;
    
		public FormHogHunt()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

      this.treeView.ImageList = this.imageList.ImageList;
      this.listView.SmallImageList = this.imageList.ImageList;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHogHunt));
      System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Dir");
      System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Dir", new System.Windows.Forms.TreeNode[] {
            treeNode1});
      System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Program Files",
            "12.300 kB",
            "Folder"}, -1, System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
      this.panelTop = new System.Windows.Forms.Panel();
      this.buttonOpen = new System.Windows.Forms.Button();
      this.comboPath = new System.Windows.Forms.ComboBox();
      this.mainToolStrip = new System.Windows.Forms.ToolStrip();
      this.toolStripButtonBrowse = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButtonBack = new System.Windows.Forms.ToolStripButton();
      this.toolStripButtonUp = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.treeView = new System.Windows.Forms.TreeView();
      this.contextMenuFolder = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.menuPopupFolderRecycle = new System.Windows.Forms.ToolStripMenuItem();
      this.menuPopupFolderDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.menuPopupFolderOpen = new System.Windows.Forms.ToolStripMenuItem();
      this.menuPopupFolderExplore = new System.Windows.Forms.ToolStripMenuItem();
      this.menuPopupFolderMakeTop = new System.Windows.Forms.ToolStripMenuItem();
      this.menuItem5 = new System.Windows.Forms.ToolStripMenuItem();
      this.listView = new System.Windows.Forms.ListView();
      this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
      this.columnHeaderSize = new System.Windows.Forms.ColumnHeader();
      this.columnHeaderBytes = new System.Windows.Forms.ColumnHeader();
      this.columnHeaderDateModified = new System.Windows.Forms.ColumnHeader();
      this.contextMenuFile = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.menuPopupFileRecycle = new System.Windows.Forms.ToolStripMenuItem();
      this.menuPopupFileDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.menuPopupFileOpen = new System.Windows.Forms.ToolStripMenuItem();
      this.menuPopupFileExplore = new System.Windows.Forms.ToolStripMenuItem();
      this.menuPopupFileSelectAll = new System.Windows.Forms.ToolStripMenuItem();
      this.menuItem4 = new System.Windows.Forms.ToolStripMenuItem();
      this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLink = new System.Windows.Forms.ToolStripStatusLabel();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.menuItem8 = new System.Windows.Forms.ToolStripSeparator();
      this.menuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.menuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.menuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.menuItem7 = new System.Windows.Forms.ToolStripSeparator();
      this.panelTop.SuspendLayout();
      this.mainToolStrip.SuspendLayout();
      this.contextMenuFolder.SuspendLayout();
      this.contextMenuFile.SuspendLayout();
      this.mainStatusStrip.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panelTop
      // 
      this.panelTop.Controls.Add(this.buttonOpen);
      this.panelTop.Controls.Add(this.comboPath);
      this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelTop.Location = new System.Drawing.Point(0, 25);
      this.panelTop.Margin = new System.Windows.Forms.Padding(0);
      this.panelTop.Name = "panelTop";
      this.panelTop.Size = new System.Drawing.Size(925, 30);
      this.panelTop.TabIndex = 0;
      // 
      // buttonOpen
      // 
      this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.buttonOpen.Enabled = false;
      this.buttonOpen.Location = new System.Drawing.Point(866, 3);
      this.buttonOpen.Name = "buttonOpen";
      this.buttonOpen.Size = new System.Drawing.Size(56, 24);
      this.buttonOpen.TabIndex = 2;
      this.buttonOpen.Text = "Open";
      this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
      // 
      // comboPath
      // 
      this.comboPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.comboPath.Location = new System.Drawing.Point(3, 3);
      this.comboPath.Name = "comboPath";
      this.comboPath.Size = new System.Drawing.Size(857, 21);
      this.comboPath.TabIndex = 1;
      this.comboPath.TextChanged += new System.EventHandler(this.comboPath_TextChanged);
      // 
      // mainToolStrip
      // 
      this.mainToolStrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonBrowse,
            this.toolStripSeparator1,
            this.toolStripButtonBack,
            this.toolStripButtonUp,
            this.toolStripSeparator2,
            this.toolStripButtonRefresh});
      this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
      this.mainToolStrip.Name = "mainToolStrip";
      this.mainToolStrip.Size = new System.Drawing.Size(925, 25);
      this.mainToolStrip.TabIndex = 3;
      // 
      // toolStripButtonBrowse
      // 
      this.toolStripButtonBrowse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonBrowse.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBrowse.Image")));
      this.toolStripButtonBrowse.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonBrowse.Name = "toolStripButtonBrowse";
      this.toolStripButtonBrowse.Size = new System.Drawing.Size(23, 22);
      this.toolStripButtonBrowse.Text = "toolStripButton1";
      this.toolStripButtonBrowse.ToolTipText = "Browse for folder...";
      this.toolStripButtonBrowse.Click += new System.EventHandler(this.toolStripButtonBrowse_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripButtonBack
      // 
      this.toolStripButtonBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonBack.Enabled = false;
      this.toolStripButtonBack.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBack.Image")));
      this.toolStripButtonBack.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonBack.Name = "toolStripButtonBack";
      this.toolStripButtonBack.Size = new System.Drawing.Size(23, 22);
      this.toolStripButtonBack.Text = "toolStripButton2";
      this.toolStripButtonBack.ToolTipText = "Back";
      this.toolStripButtonBack.Click += new System.EventHandler(this.toolStripButtonBack_Click);
      // 
      // toolStripButtonUp
      // 
      this.toolStripButtonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonUp.Enabled = false;
      this.toolStripButtonUp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUp.Image")));
      this.toolStripButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonUp.Name = "toolStripButtonUp";
      this.toolStripButtonUp.Size = new System.Drawing.Size(23, 22);
      this.toolStripButtonUp.Text = "toolStripButton3";
      this.toolStripButtonUp.ToolTipText = "Go to parent folder";
      this.toolStripButtonUp.Click += new System.EventHandler(this.toolStripButtonUp_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
      // 
      // toolStripButtonRefresh
      // 
      this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripButtonRefresh.Enabled = false;
      this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
      this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
      this.toolStripButtonRefresh.Size = new System.Drawing.Size(23, 22);
      this.toolStripButtonRefresh.Text = "toolStripButton4";
      this.toolStripButtonRefresh.ToolTipText = "Refresh";
      this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
      this.imageList1.Images.SetKeyName(0, "");
      this.imageList1.Images.SetKeyName(1, "");
      this.imageList1.Images.SetKeyName(2, "");
      this.imageList1.Images.SetKeyName(3, "");
      this.imageList1.Images.SetKeyName(4, "");
      this.imageList1.Images.SetKeyName(5, "");
      this.imageList1.Images.SetKeyName(6, "");
      this.imageList1.Images.SetKeyName(7, "");
      this.imageList1.Images.SetKeyName(8, "");
      this.imageList1.Images.SetKeyName(9, "");
      // 
      // treeView
      // 
      this.treeView.ContextMenuStrip = this.contextMenuFolder;
      this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeView.Enabled = false;
      this.treeView.FullRowSelect = true;
      this.treeView.HideSelection = false;
      this.treeView.Location = new System.Drawing.Point(0, 0);
      this.treeView.Name = "treeView";
      treeNode1.Name = "";
      treeNode1.Text = "Dir";
      treeNode2.Name = "";
      treeNode2.Text = "Dir";
      this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
      this.treeView.Size = new System.Drawing.Size(308, 355);
      this.treeView.TabIndex = 0;
      this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
      this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
      // 
      // contextMenuFolder
      // 
      this.contextMenuFolder.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.contextMenuFolder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPopupFolderRecycle,
            this.menuPopupFolderDelete,
            this.menuItem1,
            this.menuPopupFolderOpen,
            this.menuPopupFolderExplore,
            this.menuItem2,
            this.menuPopupFolderMakeTop,
            this.menuItem7,
            this.menuItem5});
      this.contextMenuFolder.Name = "contextMenuFolder";
      this.contextMenuFolder.ShowImageMargin = false;
      this.contextMenuFolder.Size = new System.Drawing.Size(183, 154);
      this.contextMenuFolder.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuFolder_Opening);
      // 
      // menuPopupFolderRecycle
      // 
      this.menuPopupFolderRecycle.Name = "menuPopupFolderRecycle";
      this.menuPopupFolderRecycle.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.menuPopupFolderRecycle.Size = new System.Drawing.Size(182, 22);
      this.menuPopupFolderRecycle.Text = "&Send to Recycle Bin...";
      this.menuPopupFolderRecycle.ToolTipText = "Send this folder to the Recycle Bin";
      this.menuPopupFolderRecycle.Click += new System.EventHandler(this.menuPopupFolderRecycle_Click);
      // 
      // menuPopupFolderDelete
      // 
      this.menuPopupFolderDelete.Name = "menuPopupFolderDelete";
      this.menuPopupFolderDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
      this.menuPopupFolderDelete.Size = new System.Drawing.Size(182, 22);
      this.menuPopupFolderDelete.Text = "&Delete...";
      this.menuPopupFolderDelete.ToolTipText = "Delete this folder permanently";
      this.menuPopupFolderDelete.Click += new System.EventHandler(this.menuPopupFolderDelete_Click);
      // 
      // menuPopupFolderOpen
      // 
      this.menuPopupFolderOpen.Name = "menuPopupFolderOpen";
      this.menuPopupFolderOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
      this.menuPopupFolderOpen.Size = new System.Drawing.Size(182, 22);
      this.menuPopupFolderOpen.Text = "&Open";
      this.menuPopupFolderOpen.ToolTipText = "Open this folder";
      this.menuPopupFolderOpen.Click += new System.EventHandler(this.menuPopupFolderOpen_Click);
      // 
      // menuPopupFolderExplore
      // 
      this.menuPopupFolderExplore.Name = "menuPopupFolderExplore";
      this.menuPopupFolderExplore.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
      this.menuPopupFolderExplore.Size = new System.Drawing.Size(182, 22);
      this.menuPopupFolderExplore.Text = "&Explore";
      this.menuPopupFolderExplore.ToolTipText = "Explore this folder";
      this.menuPopupFolderExplore.Click += new System.EventHandler(this.menuPopupFolderExplore_Click);
      // 
      // menuPopupFolderMakeTop
      // 
      this.menuPopupFolderMakeTop.Name = "menuPopupFolderMakeTop";
      this.menuPopupFolderMakeTop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
      this.menuPopupFolderMakeTop.Size = new System.Drawing.Size(182, 22);
      this.menuPopupFolderMakeTop.Text = "Make this &top folder";
      this.menuPopupFolderMakeTop.ToolTipText = "Make this folder the top folder";
      this.menuPopupFolderMakeTop.Click += new System.EventHandler(this.menuPopupFolderMakeTop_Click);
      // 
      // menuItem5
      // 
      this.menuItem5.Name = "menuItem5";
      this.menuItem5.Size = new System.Drawing.Size(182, 22);
      this.menuItem5.Text = "Cancel";
      this.menuItem5.ToolTipText = "Close this menu";
      this.menuItem5.Visible = false;
      // 
      // listView
      // 
      this.listView.AllowColumnReorder = true;
      this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderSize,
            this.columnHeaderBytes,
            this.columnHeaderDateModified});
      this.listView.ContextMenuStrip = this.contextMenuFile;
      this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView.Enabled = false;
      this.listView.FullRowSelect = true;
      this.listView.HideSelection = false;
      this.listView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
      this.listView.Location = new System.Drawing.Point(0, 0);
      this.listView.Name = "listView";
      this.listView.Size = new System.Drawing.Size(613, 355);
      this.listView.TabIndex = 0;
      this.listView.UseCompatibleStateImageBehavior = false;
      this.listView.View = System.Windows.Forms.View.Details;
      this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
      this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
      // 
      // columnHeaderName
      // 
      this.columnHeaderName.Text = "Name";
      this.columnHeaderName.Width = 150;
      // 
      // columnHeaderSize
      // 
      this.columnHeaderSize.Text = "Size";
      this.columnHeaderSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.columnHeaderSize.Width = 84;
      // 
      // columnHeaderBytes
      // 
      this.columnHeaderBytes.Text = "Bytes";
      this.columnHeaderBytes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.columnHeaderBytes.Width = 100;
      // 
      // columnHeaderDateModified
      // 
      this.columnHeaderDateModified.Text = "Date Modified";
      this.columnHeaderDateModified.Width = 147;
      // 
      // contextMenuFile
      // 
      this.contextMenuFile.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.contextMenuFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPopupFileRecycle,
            this.menuPopupFileDelete,
            this.menuItem3,
            this.menuPopupFileOpen,
            this.menuPopupFileExplore,
            this.menuItem8,
            this.menuPopupFileSelectAll,
            this.toolStripSeparator3,
            this.menuItem4});
      this.contextMenuFile.Name = "contextMenuFile";
      this.contextMenuFile.ShowImageMargin = false;
      this.contextMenuFile.Size = new System.Drawing.Size(178, 176);
      this.contextMenuFile.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuFile_Opening);
      // 
      // menuPopupFileRecycle
      // 
      this.menuPopupFileRecycle.Name = "menuPopupFileRecycle";
      this.menuPopupFileRecycle.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.menuPopupFileRecycle.Size = new System.Drawing.Size(177, 22);
      this.menuPopupFileRecycle.Text = "&Send to Recycle Bin...";
      this.menuPopupFileRecycle.ToolTipText = "Send selected files and folders to the Recycle Bin";
      this.menuPopupFileRecycle.Click += new System.EventHandler(this.menuPopupFileRecycle_Click);
      // 
      // menuPopupFileDelete
      // 
      this.menuPopupFileDelete.Name = "menuPopupFileDelete";
      this.menuPopupFileDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
      this.menuPopupFileDelete.Size = new System.Drawing.Size(177, 22);
      this.menuPopupFileDelete.Text = "&Delete...";
      this.menuPopupFileDelete.ToolTipText = "Delete selected files and folders permanently";
      this.menuPopupFileDelete.Click += new System.EventHandler(this.menuPopupFileDelete_Click);
      // 
      // menuPopupFileOpen
      // 
      this.menuPopupFileOpen.Name = "menuPopupFileOpen";
      this.menuPopupFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.menuPopupFileOpen.Size = new System.Drawing.Size(177, 22);
      this.menuPopupFileOpen.Text = "&Open";
      this.menuPopupFileOpen.ToolTipText = "Open this folder";
      this.menuPopupFileOpen.Click += new System.EventHandler(this.menuPopupFileOpen_Click);
      // 
      // menuPopupFileExplore
      // 
      this.menuPopupFileExplore.Name = "menuPopupFileExplore";
      this.menuPopupFileExplore.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
      this.menuPopupFileExplore.Size = new System.Drawing.Size(177, 22);
      this.menuPopupFileExplore.Text = "&Explore";
      this.menuPopupFileExplore.ToolTipText = "Explore this folder";
      this.menuPopupFileExplore.Click += new System.EventHandler(this.menuItem8_Click);
      // 
      // menuPopupFileSelectAll
      // 
      this.menuPopupFileSelectAll.Name = "menuPopupFileSelectAll";
      this.menuPopupFileSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
      this.menuPopupFileSelectAll.Size = new System.Drawing.Size(177, 22);
      this.menuPopupFileSelectAll.Text = "Select &all";
      this.menuPopupFileSelectAll.ToolTipText = "Select all files and folders";
      this.menuPopupFileSelectAll.Click += new System.EventHandler(this.menuPopupFileSelectAll_Click);
      // 
      // menuItem4
      // 
      this.menuItem4.Name = "menuItem4";
      this.menuItem4.Size = new System.Drawing.Size(177, 22);
      this.menuItem4.Text = "Cancel";
      this.menuItem4.Visible = false;
      // 
      // mainStatusStrip
      // 
      this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripStatusLink});
      this.mainStatusStrip.Location = new System.Drawing.Point(0, 410);
      this.mainStatusStrip.Name = "mainStatusStrip";
      this.mainStatusStrip.Size = new System.Drawing.Size(925, 22);
      this.mainStatusStrip.TabIndex = 5;
      this.mainStatusStrip.Text = "statusStrip1";
      // 
      // toolStripStatusLabel
      // 
      this.toolStripStatusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolStripStatusLabel.Name = "toolStripStatusLabel";
      this.toolStripStatusLabel.Size = new System.Drawing.Size(755, 17);
      this.toolStripStatusLabel.Spring = true;
      this.toolStripStatusLabel.Text = "Ready";
      this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // toolStripStatusLink
      // 
      this.toolStripStatusLink.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.toolStripStatusLink.IsLink = true;
      this.toolStripStatusLink.Name = "toolStripStatusLink";
      this.toolStripStatusLink.Size = new System.Drawing.Size(155, 17);
      this.toolStripStatusLink.Text = "MOBZHunt v# by MOBZystems (@-bit)";
      this.toolStripStatusLink.ToolTipText = "http://www.mobzystems.com/tools/MOBZHunt.aspx";
      this.toolStripStatusLink.Click += new System.EventHandler(this.toolStripStatusLink_Click);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 55);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.treeView);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.listView);
      this.splitContainer1.Size = new System.Drawing.Size(925, 355);
      this.splitContainer1.SplitterDistance = 308;
      this.splitContainer1.TabIndex = 6;
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(174, 6);
      this.toolStripSeparator3.Visible = false;
      // 
      // menuItem8
      // 
      this.menuItem8.Name = "menuItem8";
      this.menuItem8.Size = new System.Drawing.Size(174, 6);
      // 
      // menuItem3
      // 
      this.menuItem3.Name = "menuItem3";
      this.menuItem3.Size = new System.Drawing.Size(174, 6);
      // 
      // menuItem1
      // 
      this.menuItem1.Name = "menuItem1";
      this.menuItem1.Size = new System.Drawing.Size(179, 6);
      // 
      // menuItem2
      // 
      this.menuItem2.Name = "menuItem2";
      this.menuItem2.Size = new System.Drawing.Size(179, 6);
      // 
      // menuItem7
      // 
      this.menuItem7.Name = "menuItem7";
      this.menuItem7.Size = new System.Drawing.Size(179, 6);
      this.menuItem7.Visible = false;
      // 
      // FormHogHunt
      // 
      this.AcceptButton = this.buttonOpen;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
      this.ClientSize = new System.Drawing.Size(925, 432);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.panelTop);
      this.Controls.Add(this.mainToolStrip);
      this.Controls.Add(this.mainStatusStrip);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FormHogHunt";
      this.Text = "MOBZHunt";
      this.Load += new System.EventHandler(this.FormHogHunt_Load);
      this.panelTop.ResumeLayout(false);
      this.mainToolStrip.ResumeLayout(false);
      this.mainToolStrip.PerformLayout();
      this.contextMenuFolder.ResumeLayout(false);
      this.contextMenuFile.ResumeLayout(false);
      this.mainStatusStrip.ResumeLayout(false);
      this.mainStatusStrip.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }
	  #endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
      try 
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new FormHogHunt());
      }
      catch (Exception ex)
      {
        MessageBox.Show(null, "An unexpected error occurred:\n\n" + ex.Message, "MOBZHunt", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
		}

    // Return true if we're currently retrieving files
    private bool IsBusy()
    {
      return this.asyncResult != null;
    }

		// Return a nicely formatted size
		private string FormatSize(long size)
		{
			if (size >= 1024 * 1024 * 1024)
				return (size / (1024.0 * 1024 * 1024)).ToString("N1") + " GB";
			else if (size >= 1024 * 1024)
				return (size / (1024.0 * 1024)).ToString("N1") + " MB";
			else if (size >= 1024)
				return (size / 1024.0).ToString("N0") + " kB";
			else if (size == 0)
        return "0";
      else
				return size.ToString("N0") + " bytes";
		}
		
		// Create the nodes in the tree for a folder, then create its nodes
		private void CreateNodes(Folder folder, TreeNode node)
		{
      // Store reference to folder in node
			node.Tag = folder;

      if (folder.ImageIndex < 0)
        folder.ImageIndex = this.imageList.AddIconForFile(folder.FullPath, false);

      // Set an image index
      node.ImageIndex = folder.ImageIndex;
      node.SelectedImageIndex = node.ImageIndex;

      //if (folder.Exception != null) 
      //{
      //  node.ImageIndex = -1;
      //  node.SelectedImageIndex = -1;
      //} 
      //else 
      //{
      //}
			
      // Walk the folder list
			foreach (Folder subFolder in folder.Folders) 
			{
				// Add subfolders under their folder name
				TreeNode subNode = node.Nodes.Add(subFolder.Name);

        if (subFolder.Exception != null)
          subNode.ForeColor = Color.Red;

        // Handle the subfolders of this folder
				CreateNodes(subFolder, subNode);
			}
		}
				 
		// Fill the tree with the information from the root folder
		private void FillTree()
		{
			// Clear the tree
			treeView.Nodes.Clear();

      // Add the root node under its full name
			TreeNode node = treeView.Nodes.Add(rootFolder.FullPath);
      if (rootFolder.Exception != null)
        node.ForeColor = Color.Red;

      // Create child nodes for each folder
			CreateNodes(rootFolder, node);
			
      // Select the first item in the tree if count > 0
			if (treeView.Nodes.Count > 0)
			{
				treeView.SelectedNode = treeView.Nodes[0];
				treeView.SelectedNode.Expand();
			}
		}

    // Catch events raised from root folder
    private void OnEnterFolder(object sender, CancelEventArgs e)
    {
      if (InvokeRequired)
      {
        Invoke(new CancelEventHandler(OnEnterFolder), new object[] { sender, e });
      }
      else
      {
        e.Cancel = false;

        if (stopSignal)
        {
          e.Cancel = true;
          toolStripStatusLabel.Text = "Canceling...";
        }
        else
          // Display folder name in status bar
          toolStripStatusLabel.Text = ((Folder)sender).FullPath;

        mainStatusStrip.Update();
      }
    }

    private void UpdateStatusBar(Folder folder)
    {
      toolStripStatusLabel.Text =
        (folder.Name == "" ? folder.FullPath : folder.Name) // use folder name if set, else full name (name of c:\ is ""!)
        + " contains " + FormatSize(folder.TotalFileSize)
        + " in " + folder.Folders.Count.ToString("N0") + " folder(s), "
        + folder.Files.Count.ToString("N0") + " file(s)"
        ;
    }

    // Update the file list based on the selected node in the tree
    private void UpdateFileList()
    {
			// Make sure we have a selected item:
			if (treeView.SelectedNode == null)
				return;
			
      try
      {
        this.listView.BeginUpdate();

        Cursor.Current = Cursors.WaitCursor;
        this.toolStripStatusLabel.Text = "Working...";
        this.mainStatusStrip.Update();

        listView.Items.Clear();
        
        // Populate the list of files with the currently selected folder
        Folder folder = GetSelectedFolderFromTree();

        if (folder.Exception != null)
        {
          toolStripStatusLabel.Text = "Error: " + folder.Exception.Message;
        } 
        else 
        {
          // Sort the contents of the folder based on the current criteria:
          folder.Sort(this.sortType, this.sortOrder);
  				
//          // Add a .. folder if this is not the root folder:
//          if (folder != rootFolder) 
//          {
//            ListViewItem item = listView.Items.Add("..", 0);
//            item.Tag = null;
//          }
          // Add the folders
          foreach (Folder subFolder in folder.Folders) 
          {
            ListViewItem item = listView.Items.Add(subFolder.Name);

            if (subFolder.Exception != null) 
            {
              // item.ImageIndex = 7;
              item.ForeColor = Color.Red;
              item.SubItems.Add("");
              item.SubItems.Add("");
              item.SubItems.Add(subFolder.Exception.Message);
              // item.SubItems[1].ForeColor = Color.Red;
            }
            else
            {
              if (subFolder.ImageIndex < 0)
                subFolder.ImageIndex = this.imageList.AddIconForFile(subFolder.FullPath, false);
              item.SubItems.Add(FormatSize(subFolder.TotalFileSize));
              item.SubItems.Add(subFolder.TotalFileSize.ToString("N0"));
              item.SubItems.Add(subFolder.DateModified.ToString());
            }
            item.ImageIndex = subFolder.ImageIndex; // 0;
            item.Tag = subFolder;
          }
          // Add the files:
          foreach (File file in folder.Files) 
          {
            ListViewItem item = listView.Items.Add(file.Name);
            if (file.Exception != null) 
            {
              // item.ImageIndex = 7;
              item.ForeColor = Color.Red;
              item.SubItems.Add("");
              item.SubItems.Add("");
              item.SubItems.Add(file.Exception.Message);
            }
            else
            {
              if (file.ImageIndex < 0)
                file.ImageIndex = this.imageList.AddIconForFile(Path.Combine(folder.FullPath, file.Name), false);
              item.ImageIndex = file.ImageIndex; // 6;
              item.SubItems.Add(FormatSize(file.Size));
              item.SubItems.Add(file.Size.ToString("N0"));
              item.SubItems.Add(file.DateModified.ToString());
            }
            item.Tag = file;
          }

          if (listView.Items.Count > 0) 
          {
            listView.Items[0].Selected = true;
          }

          // Enable the UP button if this is NOT the root folder
          toolStripButtonUp.Enabled = folder != rootFolder;

          // Display status information for this folder:
          UpdateStatusBar(folder);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error displaying folder contents", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        this.toolStripStatusLabel.Text = "Error retrieving files and folders";
      }
      finally
      {
        Cursor.Current = Cursors.Default;
        this.listView.EndUpdate();
      }
		}
		
		// Populate the list view when a new item is selected
		private void treeView_AfterSelect(object sender, TreeViewEventArgs e) 
		{
      try
      {
        UpdateFileList();

        // Push the current node on the folder stack:
        if (folderStack.Count == 0 || (TreeNode)folderStack[folderStack.Count - 1] != treeView.SelectedNode)
        {
          folderStack.Add(treeView.SelectedNode);
          toolStripButtonBack.Enabled = folderStack.Count > 0;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error displaying folder contents", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
		}

    // Form_Load
		private void FormHogHunt_Load(object sender, System.EventArgs e) 
		{
      try
      {
        // Clear the tree and list view for a clean start:
        treeView.Nodes.Clear();
        listView.Items.Clear();

        // Add special folder locations to combo box
				comboPath.Items.Add(Path.GetTempPath()); // TODO: this is a short file name!
				comboPath.Items.Add(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
        comboPath.Items.Add(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
        comboPath.Items.Add(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
				comboPath.Items.Add(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
				comboPath.Items.Add(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache));

        // See if we have a command line:
        string[] arguments = CommandLineParser.GetCommandLineArgs();
        if (arguments.Length == 2) 
        {
          comboPath.Text = arguments[1];
          OpenNewRootPath(arguments[1]);
        } 
        else 
        {
          // Select the first item in the list by default:
          comboPath.SelectedIndex = 0;
          if (arguments.Length > 2) 
          {
            MessageBox.Show(this, "Invalid command line arguments. Use MOBZHunt <path>.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error loading form", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }

      Version v = new Version(Application.ProductVersion);
      int bits = IntPtr.Size * 8;
      this.toolStripStatusLink.Text = this.toolStripStatusLink.Text.Replace("#", v.Major.ToString() + "." + v.Minor.ToString() + "." + v.Build.ToString()).Replace("@", bits.ToString());
		}

    private bool ComboContains(string item)
    {
      foreach (object o in comboPath.Items)
      {
        if (o.ToString().ToLower() == item.ToLower())
          return true;
      }
      return false;
    }

		// Process the results from the background thread.
		// Assumes the new root folder has been set!
		private void OnProcessResults()
		{
      if (rootFolder.Exception != null) 
      {
        Exception ex = rootFolder.Exception;

        MessageBox.Show(this, ex.Message, "Error processing folder", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        toolStripStatusLabel.Text = "Error: " + ex.Message;
      } 
      else 
      {
        Cursor.Current = Cursors.WaitCursor;
			
        toolStripStatusLabel.Text = "Displaying results...";
        this.mainStatusStrip.Update();
  			
        folderStack = new ArrayList();

        FillTree();

        // Add path to combo box if not present already
        if (!ComboContains(rootFolder.FullPath))
          comboPath.Items.Add(rootFolder.FullPath);

        Cursor.Current = Cursors.Default;
      }
		}

    // Enable/Disable the user interface while processing
    private void EnableUI(bool enable)
    {
      if (enable)
        buttonOpen.Text = "Open";
      else
        buttonOpen.Text = "Cancel";
      
      comboPath.Enabled = enable;
      toolStripButtonBrowse.Enabled = enable;
      toolStripButtonUp.Enabled = false; // enable;
      toolStripButtonBack.Enabled = false; // enable;
      toolStripButtonRefresh.Enabled = this.rootFolder != null && enable;
      treeView.Enabled = enable;
      listView.Enabled = enable;
    }
    		
		// This function is called when the background returns:
		private void OnSizesRetrieved(IAsyncResult asyncResult)
		{
			// Make sure we got our own asyncResult back as an argument:
			System.Diagnostics.Debug.Assert(this.asyncResult as IAsyncResult == asyncResult);

			// Get the delegate from the AsyncResult:
			RetrieveSizes retrieveSizes = (RetrieveSizes)this.asyncResult.AsyncDelegate;
			
			try
			{
				// EndInvoke to clean up. THIS MAY THROW AN EXCEPTION
				retrieveSizes.EndInvoke(asyncResult);

				if (!stopSignal) 
				{
					// The target of the BeginInvoke was the folder to use as a new root folder:
					Folder folder = (Folder)retrieveSizes.Target;
					// Set the new root folder object:
					rootFolder = folder;
					// Process the results:
          this.Invoke(new ProcessResults(this.OnProcessResults));
        } 
        else 
        {
					toolStripStatusLabel.Text = "Canceled";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Error processing folder", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				toolStripStatusLabel.Text = "Error: " + ex.Message;
			}
			finally
			{
				// Clean up reference to AsyncResult
				this.asyncResult = null;
				stopSignal = false;
        this.Invoke(new EnableUIDelegate(this.EnableUI), new object[]  { true });
			}
		}
		
    // Start the background thread to retrieve a new root path.
    // When the thread ends, it calls OnSizesRetrieved.
    private void OpenNewRootPath(string newRootPath)
    {
      try 
      {
				Folder newRoot;
				
        Cursor.Current = Cursors.WaitCursor;
					
        // Create a new root folder object:
        newRoot = new Folder(newRootPath);
        // Attach a handler to the EnterFolder event:
        newRoot.EnterFolder += new CancelEventHandler(OnEnterFolder);
				// Create a delegate to call newRoot.RetrieveSizes:
				RetrieveSizes retrieveSizes = new RetrieveSizes(newRoot.RetrieveSizes);
				// Create a callback delegate to call OnSizesRetrieved on completion:
				AsyncCallback callBack = new AsyncCallback(this.OnSizesRetrieved);
				// Invoke the delegate as a separate thread:
				this.asyncResult = (AsyncResult)retrieveSizes.BeginInvoke(newRoot, /* this.imageList */ null, callBack, null);
				
				if (this.asyncResult.CompletedSynchronously) {
					// The thread completed synchronously - pretend it called our callback
					OnSizesRetrieved(this.asyncResult);
				} else {
          EnableUI(false);					
					
					toolStripStatusLabel.Text = "Retrieving folders...";
				}
      }
      catch (Exception ex) 
      {
        MessageBox.Show(this, ex.Message, "Error reading folder", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
      finally
      {
        Cursor.Current = Cursors.Default;
      }
    }

    // The Open button was clicked. This may also be a Cancel!
    private void buttonOpen_Click(object sender, System.EventArgs e)
    {
      if (IsBusy()) 
      {
        // We're processing in the background!
        stopSignal = true;
      } 
      else 
      {
        // Open the path specified in comboPath.Text
        OpenNewRootPath(comboPath.Text);
      }
    }

    private TreeNode FindNodeWithFolder(Folder folder, TreeNode rootNode)
    {
      foreach (TreeNode childNode in rootNode.Nodes)
      {
        if (childNode.Tag == folder) 
        {
          return childNode;
        }
      }
      return null;
    }

    // Handle a doubleclick on a list item.
    private void listView_DoubleClick(object sender, System.EventArgs e)
    {
      try
      {
        // If this is a folder, enter that folder by selecting it in the tree
        if (listView.SelectedItems.Count == 1
          && listView.SelectedItems[0] != null
          && treeView.SelectedNode != null)
        {
          if (listView.SelectedItems[0].Text == "..")
          {
            treeView.SelectedNode = treeView.SelectedNode.Parent;
            return;
          }

          Folder folder = GetSelectedFolderFromList();

          if (folder != null)
          {
            // Find the folder name in the child nodes of the current node:
            TreeNode node = FindNodeWithFolder(folder, treeView.SelectedNode);

            if (node != null)
              treeView.SelectedNode = node;
          }
          else
          {
            File file = GetSelectedFileFromList();

            if (file != null)
            {
              Folder containingFolder = GetSelectedFolderFromTree();
              OpenFile(System.IO.Path.Combine(containingFolder.FullPath, file.Name));
            }
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error handling double-click", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }

    private void Browse()
    {
      this.folderBrowser = new FolderBrowserDialog();
      this.folderBrowser.Description = "Choose a folder to open";
      this.folderBrowser.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
      this.folderBrowser.ShowNewFolderButton = false;
      this.folderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
      if (this.folderBrowser.ShowDialog(this) == DialogResult.OK)
      {
        comboPath.Text = this.folderBrowser.SelectedPath;
        OpenNewRootPath(this.folderBrowser.SelectedPath);
      }
    }

    private void comboPath_TextChanged(object sender, System.EventArgs e)
    {
      buttonOpen.Enabled = comboPath.Text != "";
    }

    // Get the selected folder from the tree view or null:
    private Folder GetSelectedFolderFromTree()
    {
      // Ensure we have a valid folder node:
      if (treeView.SelectedNode == null || treeView.SelectedNode.Tag == null)
        return null;

      return treeView.SelectedNode.Tag as Folder;
    }

    // Get the single selected folder from the list view or null:
    private Folder GetSelectedFolderFromList()
    {
      // Ensure we have a valid folder node:
      if (listView.SelectedItems.Count != 1)
        return null;

      return listView.SelectedItems[0].Tag as Folder;
    }

    // Get the single selected file from the list view or null:
    private File GetSelectedFileFromList()
    {
      // Ensure we have a valid folder node:
      if (listView.SelectedItems.Count != 1)
        return null;

      return listView.SelectedItems[0].Tag as File;
    }

    // Set the sorting criteria for the file list when a column header is clicked.
    // When the sort type (name, size etc.) is changed, the order is re-set to ascending.
    // When a column is clicked again, the sort order is reversed
    private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      try
      {
        // Get the folder from the tag:  
        Folder folder = GetSelectedFolderFromTree();
        System.Diagnostics.Debug.Assert(folder != null, "No folder selected!");
        if (folder == null)
          return;

        // The new sort type:
        FolderCompareType newSortType;

        switch (e.Column)
        {
          default:
          case 0: // Name
            newSortType = FolderCompareType.Name;
            break;
          case 1: // Size, Bytes
          case 2:
            newSortType = FolderCompareType.Size;
            break;
          case 3: // Date Modified
            newSortType = FolderCompareType.DateModified;
            break;
        }

        // Same sort type? Reverse sort order
        if (this.sortType == newSortType)
        {
          if (this.sortOrder == FolderCompareOrder.Ascending)
            this.sortOrder = FolderCompareOrder.Descending;
          else
            this.sortOrder = FolderCompareOrder.Ascending;
        }
        else
        {
          // Use new sort type, ascending
          this.sortType = newSortType;
          this.sortOrder = FolderCompareOrder.Ascending;
        }

        UpdateFileList();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error sorting column", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }

    private void ShowAbout()
    {
/*
       MessageBox.Show(
        this,
        "MOBZHunt\nVersion "
        + System.Reflection.Assembly.GetCallingAssembly().GetName().Version.ToString()
        + "\n\n(C) 2003, Development Expertise B.V.\n\nhttp://www.dvxp.com/"
        , "About MOBZHunt"
        , MessageBoxButtons.OK
        , MessageBoxIcon.Information);
       */
      AboutForm form = new AboutForm();
      form.Text = "About MOBZHunt";
      form.labelProduct.Text = this.Text;
      form.labelCopyright.Text = "(C) 2003-2008, MOBZystems";
      form.ShowDialog(this);
    }

    // Update the folder context menu before it pops up:
    private void contextMenuFolder_Opening(object sender, CancelEventArgs e)
		{
      menuPopupFolderExplore.Enabled = treeView.SelectedNode != null;
      menuPopupFolderOpen.Enabled = treeView.SelectedNode != null;
      menuPopupFolderDelete.Enabled = treeView.SelectedNode != null;
      menuPopupFolderRecycle.Enabled = treeView.SelectedNode != null;
      menuPopupFolderMakeTop.Enabled = treeView.SelectedNode != null;
    }

    private void ExploreFolder(string folder)
    {
      try
      {
        System.Diagnostics.Process.Start(System.IO.Path.Combine(Environment.SystemDirectory, @"..\Explorer.exe"), "/e ," + folder);
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error exploring folder", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }

    // Handle "Explore <folder>"
		private void menuPopupFolderExplore_Click(object sender, System.EventArgs e)
		{
      Folder folder = GetSelectedFolderFromTree();

      if (folder == null)
        return;

      ExploreFolder(folder.FullPath);
		}

    private void OpenFolder(string folder)
    {
      try
      {
        System.Diagnostics.Process.Start(System.IO.Path.Combine(Environment.SystemDirectory, @"..\Explorer.exe"), "," + folder);
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error opening folder", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }

    // Handle "Open <folder>"
		private void menuPopupFolderOpen_Click(object sender, System.EventArgs e)
		{
      Folder folder = GetSelectedFolderFromTree();

      if (folder == null)
        return;

      OpenFolder(folder.FullPath);
		}

    private void RecalcSizesFromNodeUpwards(TreeNode node)
    {
      // Update folder sizes from the currently selected node upwards
      while (node != null)
      {
        ((Folder)node.Tag).RecalcSizes();
        node = node.Parent;
      }
    }

    private void DeleteFolder(bool recycle, string errorMessage)
    {
      Folder folder = GetSelectedFolderFromTree();

      if (folder == null)
        return;

      try 
      {
        FileOperation fo = new FileOperation();

        fo.ParentForm = this;
        fo.Delete(folder.FullPath, recycle);

        TreeNode parentNode = treeView.SelectedNode.Parent;

        // If there was no exception, remove the folder from the tree:
        treeView.Nodes.Remove(treeView.SelectedNode);

        // Also remove it from the parent folder:
        if (parentNode != null)
        {
          ((Folder)parentNode.Tag).Folders.Remove(folder);
          RecalcSizesFromNodeUpwards(parentNode);
        }
        // No nodes left?
        if (treeView.SelectedNode == null)
          listView.Items.Clear();
        else
          UpdateFileList();
      }
      catch (FileOperationAbortedException)
      {
        // Nothing - the user cancelled!
      }
      catch (FileOperationFailedException)
      {
        // Nothing - the user has already seen an alert box
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, errorMessage, MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }

    // Handle "Send <folder> to Recycle Bin"
		private void menuPopupFolderRecycle_Click(object sender, System.EventArgs e)
		{
      DeleteFolder(true, "Error sending folder to recycle bin");
		}

    // Handle "Delete <folder>"
		private void menuPopupFolderDelete_Click(object sender, System.EventArgs e)
		{
      DeleteFolder(false, "Error deleting folder");
		}

    private void DeleteFiles(bool recycle, string errorMessage)
    {
      Folder folder = GetSelectedFolderFromTree();

      if (folder == null)
        return;

      try 
      {
        string[] itemsToDelete = new String[listView.SelectedItems.Count];

        for (int i = 0; i < itemsToDelete.Length; i++)
        {
          if (listView.SelectedItems[i].Tag as Folder != null) 
          {
            itemsToDelete[i] = ((Folder)(listView.SelectedItems[i].Tag)).FullPath;
          } 
          else 
          {
            itemsToDelete[i] = System.IO.Path.Combine(folder.FullPath, ((File)(listView.SelectedItems[i].Tag)).Name);
          }
        }

        FileOperation fo = new FileOperation();

        fo.ParentForm = this;
        fo.Delete(itemsToDelete, recycle);

//        // Get the parent node and the corresponding folder:
//        TreeNode parentNode = treeView.SelectedNode.Parent;
//        Folder parentFolder = (Folder)parentfFolder.Tag;

        // If there was no exception, remove the folder from the tree:
        while (listView.SelectedItems.Count > 0)
        {
          if (listView.SelectedItems[0].Tag as Folder != null) 
          {
            Folder folderToDelete = (Folder)listView.SelectedItems[0].Tag;
            folder.Folders.Remove(folderToDelete);
            // Also remove the folder node from the tree view:
            TreeNode folderNode = FindNodeWithFolder(folderToDelete, treeView.SelectedNode);
            if (folderNode != null)
              treeView.SelectedNode.Nodes.Remove(folderNode);
          }
          else if (listView.SelectedItems[0].Tag as File != null) 
          {
            folder.Files.Remove((File)listView.SelectedItems[0].Tag);
          }
          listView.Items.Remove(listView.SelectedItems[0]);
        }
        RecalcSizesFromNodeUpwards(treeView.SelectedNode);
        UpdateStatusBar(folder);
      }
      catch (FileOperationAbortedException)
      {
        // Nothing - the user cancelled!
      }
      catch (FileOperationFailedException)
      {
        // Nothing - the user has already seen an alert box
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, errorMessage, MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }

    // Handle "Delete <file>"
		private void menuPopupFileDelete_Click(object sender, System.EventArgs e)
		{
      DeleteFiles(false, "Error deleting files and folders");
    }

    // Handle "Send <file> to Recycle Bin"
    private void menuPopupFileRecycle_Click(object sender, System.EventArgs e)
		{
      DeleteFiles(true, "Error sending files and folders to recycle bin");
		}

    // Make the selected item the one the right mouse button clicked on:
    private void treeView_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TreeNode newSelectedNode = treeView.GetNodeAt(new Point(e.X, e.Y));
        if (newSelectedNode != null)
          treeView.SelectedNode = newSelectedNode;
      }
    }

    private void contextMenuFile_Opening(object sender, CancelEventArgs e)
    {
      menuPopupFileDelete.Enabled = listView.SelectedItems.Count > 0;
      menuPopupFileRecycle.Enabled = listView.SelectedItems.Count > 0;

      menuPopupFileOpen.Enabled = listView.SelectedItems.Count == 1;
      menuPopupFileExplore.Enabled = GetSelectedFolderFromList() != null;

      menuPopupFileSelectAll.Enabled = listView.SelectedItems.Count != listView.Items.Count && listView.Items.Count > 0;
  }

    private void menuPopupFolderMakeTop_Click(object sender, System.EventArgs e)
    {
      Folder folder = GetSelectedFolderFromTree();

      if (folder == null)
         return;

      OpenNewRootPath(folder.FullPath);
    }

    private void OpenFile(string file)
    {
      try
      {
        System.Diagnostics.Process.Start(file);
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }

    private void menuPopupFileOpen_Click(object sender, System.EventArgs e)
    {
      Folder folder = GetSelectedFolderFromList();

      if (folder != null)
        OpenFolder(folder.FullPath);
      else 
      {
        File file = GetSelectedFileFromList();
        
        if (file != null) 
        {
          Folder containingFolder = GetSelectedFolderFromTree();
          OpenFile(System.IO.Path.Combine(containingFolder.FullPath, file.Name));
        }
      }
    }

    private void menuItem8_Click(object sender, System.EventArgs e)
    {
      Folder folder = GetSelectedFolderFromList();

      if (folder != null)
        ExploreFolder(folder.FullPath);
    }

    private void menuPopupFileSelectAll_Click(object sender, System.EventArgs e)
    {
      if (listView.SelectedItems.Count != listView.Items.Count)
      {
        foreach (ListViewItem listItem in listView.Items)
        {
          if (!listItem.Selected)
            listItem.Selected = true;
        }
      }
    }

    private void toolStripStatusLink_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.mobzystems.com/tools/MOBZHunt.aspx");
    }

    private void toolStripButtonBrowse_Click(object sender, EventArgs e)
    {
      Browse();
    }

    private void toolStripButtonBack_Click(object sender, EventArgs e)
    {
      // Go back
      if (folderStack != null && folderStack.Count > 1)
      {
        treeView.SelectedNode = (TreeNode)folderStack[folderStack.Count - 2];
        folderStack.RemoveAt(folderStack.Count - 1);
        folderStack.RemoveAt(folderStack.Count - 1);

        toolStripButtonBack.Enabled = folderStack.Count > 1;
      }
    }

    private void toolStripButtonUp_Click(object sender, EventArgs e)
    {
      // Go up
      if (treeView.SelectedNode != null && treeView.SelectedNode.Parent != null)
      {
        treeView.SelectedNode = treeView.SelectedNode.Parent;
      }
    }

    private void toolStripButtonRefresh_Click(object sender, EventArgs e)
    {
      if (this.rootFolder != null)
      {
        OpenNewRootPath(this.rootFolder.FullPath);
      }
    }
	}
}
