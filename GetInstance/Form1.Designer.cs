namespace GetInstance
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.txtLog = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtPeriods = new System.Windows.Forms.TextBox();
			this.txtRssd = new System.Windows.Forms.TextBox();
			this.txtDate = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnGetCdr = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblFormat = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtLog
			// 
			this.txtLog.Location = new System.Drawing.Point(183, 6);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.Size = new System.Drawing.Size(549, 159);
			this.txtLog.TabIndex = 19;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 6);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(152, 146);
			this.pictureBox1.TabIndex = 18;
			this.pictureBox1.TabStop = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(65, 599);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 25);
			this.label5.TabIndex = 17;
			this.label5.Text = "&RSSD";
			// 
			// txtPeriods
			// 
			this.txtPeriods.Location = new System.Drawing.Point(376, 536);
			this.txtPeriods.Name = "txtPeriods";
			this.txtPeriods.Size = new System.Drawing.Size(73, 31);
			this.txtPeriods.TabIndex = 16;
			// 
			// txtRssd
			// 
			this.txtRssd.Location = new System.Drawing.Point(142, 593);
			this.txtRssd.Name = "txtRssd";
			this.txtRssd.Size = new System.Drawing.Size(137, 31);
			this.txtRssd.TabIndex = 14;
			// 
			// txtDate
			// 
			this.txtDate.Location = new System.Drawing.Point(65, 536);
			this.txtDate.Name = "txtDate";
			this.txtDate.Size = new System.Drawing.Size(246, 31);
			this.txtDate.TabIndex = 15;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(372, 508);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(204, 25);
			this.label4.TabIndex = 13;
			this.label4.Text = "Number of Prior Periods";
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(407, 426);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(169, 34);
			this.btnClose.TabIndex = 11;
			this.btnClose.Text = "&Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnGetCdr
			// 
			this.btnGetCdr.Location = new System.Drawing.Point(167, 426);
			this.btnGetCdr.Name = "btnGetCdr";
			this.btnGetCdr.Size = new System.Drawing.Size(157, 34);
			this.btnGetCdr.TabIndex = 10;
			this.btnGetCdr.Text = "&Get CDR Data";
			this.btnGetCdr.UseVisualStyleBackColor = true;
			this.btnGetCdr.Click += new System.EventHandler(this.btnGetCdr_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(65, 472);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(181, 25);
			this.label3.TabIndex = 12;
			this.label3.Text = "Prior Period End Date";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtPassword);
			this.groupBox1.Controls.Add(this.txtUsername);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(65, 189);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(557, 212);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "CDR Login";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(232, 137);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(246, 31);
			this.txtPassword.TabIndex = 4;
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(232, 89);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(246, 31);
			this.txtUsername.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(87, 143);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127, 25);
			this.label2.TabIndex = 2;
			this.label2.Text = "CDR &Password";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(87, 89);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(139, 25);
			this.label1.TabIndex = 1;
			this.label1.Text = "CDR &User Name";
			// 
			// lblFormat
			// 
			this.lblFormat.AutoSize = true;
			this.lblFormat.Location = new System.Drawing.Point(65, 501);
			this.lblFormat.Name = "lblFormat";
			this.lblFormat.Size = new System.Drawing.Size(76, 25);
			this.lblFormat.TabIndex = 12;
			this.lblFormat.Text = "(format)";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(777, 682);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtPeriods);
			this.Controls.Add(this.txtRssd);
			this.Controls.Add(this.txtDate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnGetCdr);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lblFormat);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.groupBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Shown += new System.EventHandler(this.Form1_Shown);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private GroupBox groupBox1;
		private Label label1;
		private TextBox txtPassword;
		private TextBox txtUsername;
		private Label label2;
		private Button btnGetCdr;
		private Button btnClose;
		private Label label3;
		private Label label4;
		private TextBox txtDate;
		private TextBox txtPeriods;
		private Label label5;
		private TextBox txtRssd;
		private PictureBox pictureBox1;
		private TextBox txtLog;
		private Label lblFormat;
	}
}