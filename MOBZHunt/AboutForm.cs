using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace MOBZystems.MOBZHunt
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
    private System.Windows.Forms.PictureBox pictureBox1;
    internal System.Windows.Forms.Label labelProduct;
    internal System.Windows.Forms.Label labelVersion;
    internal System.Windows.Forms.Label labelCopyright;
    private System.Windows.Forms.Button buttonOK;
    internal System.Windows.Forms.LinkLabel linkLabel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutForm));
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.labelProduct = new System.Windows.Forms.Label();
      this.labelVersion = new System.Windows.Forms.Label();
      this.labelCopyright = new System.Windows.Forms.Label();
      this.buttonOK = new System.Windows.Forms.Button();
      this.linkLabel = new System.Windows.Forms.LinkLabel();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(82, 8);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(128, 71);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // labelProduct
      // 
      this.labelProduct.Location = new System.Drawing.Point(10, 88);
      this.labelProduct.Name = "labelProduct";
      this.labelProduct.Size = new System.Drawing.Size(272, 16);
      this.labelProduct.TabIndex = 1;
      this.labelProduct.Text = "(product)";
      this.labelProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelVersion
      // 
      this.labelVersion.Location = new System.Drawing.Point(10, 112);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new System.Drawing.Size(272, 16);
      this.labelVersion.TabIndex = 2;
      this.labelVersion.Text = "(version)";
      this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // labelCopyright
      // 
      this.labelCopyright.Location = new System.Drawing.Point(10, 136);
      this.labelCopyright.Name = "labelCopyright";
      this.labelCopyright.Size = new System.Drawing.Size(272, 16);
      this.labelCopyright.TabIndex = 3;
      this.labelCopyright.Text = "(copyright)";
      this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // buttonOK
      // 
      this.buttonOK.BackColor = System.Drawing.SystemColors.Control;
      this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.buttonOK.Location = new System.Drawing.Point(110, 192);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(72, 24);
      this.buttonOK.TabIndex = 0;
      this.buttonOK.Text = "OK";
      // 
      // linkLabel
      // 
      this.linkLabel.Location = new System.Drawing.Point(10, 160);
      this.linkLabel.Name = "linkLabel";
      this.linkLabel.Size = new System.Drawing.Size(272, 16);
      this.linkLabel.TabIndex = 4;
      this.linkLabel.TabStop = true;
      this.linkLabel.Text = "http://www.dvxp.com/";
      this.linkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
      // 
      // AboutForm
      // 
      this.AcceptButton = this.buttonOK;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(292, 221);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.linkLabel,
                                                                  this.buttonOK,
                                                                  this.labelCopyright,
                                                                  this.labelVersion,
                                                                  this.labelProduct,
                                                                  this.pictureBox1});
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AboutForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "AboutForm";
      this.Load += new System.EventHandler(this.AboutForm_Load);
      this.ResumeLayout(false);

    }
		#endregion

    private void AboutForm_Load(object sender, System.EventArgs e)
    {
      // Set defaults:
      System.Version version = Assembly.GetExecutingAssembly().GetName().Version;
      DateTime compileDateTime = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
      labelVersion.Text = "v" + version.ToString() + " (" + compileDateTime.ToString() + ")";
    }

    private void linkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start(linkLabel.Text);
    }
	}
}
