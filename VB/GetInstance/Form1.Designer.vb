<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
		Inherits System.Windows.Forms.Form
		Private components As System.ComponentModel.IContainer = Nothing


		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If

			MyBase.Dispose(disposing)
		End Sub

		Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
		Me.txtLog = New System.Windows.Forms.TextBox()
		Me.pictureBox1 = New System.Windows.Forms.PictureBox()
		Me.label5 = New System.Windows.Forms.Label()
		Me.txtPeriods = New System.Windows.Forms.TextBox()
		Me.txtRssd = New System.Windows.Forms.TextBox()
		Me.txtDate = New System.Windows.Forms.TextBox()
		Me.label4 = New System.Windows.Forms.Label()
		Me.btnClose = New System.Windows.Forms.Button()
		Me.btnGetCdr = New System.Windows.Forms.Button()
		Me.label3 = New System.Windows.Forms.Label()
		Me.groupBox1 = New System.Windows.Forms.GroupBox()
		Me.txtPassword = New System.Windows.Forms.TextBox()
		Me.txtUsername = New System.Windows.Forms.TextBox()
		Me.label2 = New System.Windows.Forms.Label()
		Me.label1 = New System.Windows.Forms.Label()
		Me.lblFormat = New System.Windows.Forms.Label()
		CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.groupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'txtLog
		'
		Me.txtLog.Location = New System.Drawing.Point(11, 23)
		Me.txtLog.Multiline = True
		Me.txtLog.Name = "txtLog"
		Me.txtLog.Size = New System.Drawing.Size(713, 159)
		Me.txtLog.TabIndex = 19
		'
		'pictureBox1
		'
		Me.pictureBox1.Image = CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image)
		Me.pictureBox1.Location = New System.Drawing.Point(9, 198)
		Me.pictureBox1.Name = "pictureBox1"
		Me.pictureBox1.Size = New System.Drawing.Size(151, 147)
		Me.pictureBox1.TabIndex = 18
		Me.pictureBox1.TabStop = False
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Location = New System.Drawing.Point(167, 608)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(56, 25)
		Me.label5.TabIndex = 17
		Me.label5.Text = "&RSSD"
		'
		'txtPeriods
		'
		Me.txtPeriods.Location = New System.Drawing.Point(479, 547)
		Me.txtPeriods.Name = "txtPeriods"
		Me.txtPeriods.Size = New System.Drawing.Size(73, 31)
		Me.txtPeriods.TabIndex = 16
		'
		'txtRssd
		'
		Me.txtRssd.Location = New System.Drawing.Point(244, 603)
		Me.txtRssd.Name = "txtRssd"
		Me.txtRssd.Size = New System.Drawing.Size(137, 31)
		Me.txtRssd.TabIndex = 14
		'
		'txtDate
		'
		Me.txtDate.Location = New System.Drawing.Point(167, 547)
		Me.txtDate.Name = "txtDate"
		Me.txtDate.Size = New System.Drawing.Size(245, 31)
		Me.txtDate.TabIndex = 15
		'
		'label4
		'
		Me.label4.AutoSize = True
		Me.label4.Location = New System.Drawing.Point(474, 518)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(204, 25)
		Me.label4.TabIndex = 13
		Me.label4.Text = "Number of Prior Periods"
		'
		'btnClose
		'
		Me.btnClose.Location = New System.Drawing.Point(407, 427)
		Me.btnClose.Name = "btnClose"
		Me.btnClose.Size = New System.Drawing.Size(169, 33)
		Me.btnClose.TabIndex = 11
		Me.btnClose.Text = "&Close"
		Me.btnClose.UseVisualStyleBackColor = True
		'
		'btnGetCdr
		'
		Me.btnGetCdr.Location = New System.Drawing.Point(167, 427)
		Me.btnGetCdr.Name = "btnGetCdr"
		Me.btnGetCdr.Size = New System.Drawing.Size(157, 33)
		Me.btnGetCdr.TabIndex = 10
		Me.btnGetCdr.Text = "&Get CDR Data"
		Me.btnGetCdr.UseVisualStyleBackColor = True
		'
		'label3
		'
		Me.label3.AutoSize = True
		Me.label3.Location = New System.Drawing.Point(167, 482)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(181, 25)
		Me.label3.TabIndex = 12
		Me.label3.Text = "Prior Period End Date"
		'
		'groupBox1
		'
		Me.groupBox1.Controls.Add(Me.txtPassword)
		Me.groupBox1.Controls.Add(Me.txtUsername)
		Me.groupBox1.Controls.Add(Me.label2)
		Me.groupBox1.Controls.Add(Me.label1)
		Me.groupBox1.Location = New System.Drawing.Point(167, 198)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(557, 212)
		Me.groupBox1.TabIndex = 9
		Me.groupBox1.TabStop = False
		Me.groupBox1.Text = "CDR Login"
		'
		'txtPassword
		'
		Me.txtPassword.Location = New System.Drawing.Point(231, 137)
		Me.txtPassword.Name = "txtPassword"
		Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
		Me.txtPassword.Size = New System.Drawing.Size(245, 31)
		Me.txtPassword.TabIndex = 4
		'
		'txtUsername
		'
		Me.txtUsername.Location = New System.Drawing.Point(231, 88)
		Me.txtUsername.Name = "txtUsername"
		Me.txtUsername.Size = New System.Drawing.Size(245, 31)
		Me.txtUsername.TabIndex = 3
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(87, 143)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(127, 25)
		Me.label2.TabIndex = 2
		Me.label2.Text = "CDR &Password"
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(87, 88)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(139, 25)
		Me.label1.TabIndex = 1
		Me.label1.Text = "CDR &User Name"
		'
		'lblFormat
		'
		Me.lblFormat.AutoSize = True
		Me.lblFormat.Location = New System.Drawing.Point(167, 512)
		Me.lblFormat.Name = "lblFormat"
		Me.lblFormat.Size = New System.Drawing.Size(76, 25)
		Me.lblFormat.TabIndex = 12
		Me.lblFormat.Text = "(format)"
		'
		'Form1
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 25.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(777, 682)
		Me.Controls.Add(Me.txtLog)
		Me.Controls.Add(Me.pictureBox1)
		Me.Controls.Add(Me.label5)
		Me.Controls.Add(Me.txtPeriods)
		Me.Controls.Add(Me.txtRssd)
		Me.Controls.Add(Me.txtDate)
		Me.Controls.Add(Me.label4)
		Me.Controls.Add(Me.btnGetCdr)
		Me.Controls.Add(Me.btnClose)
		Me.Controls.Add(Me.lblFormat)
		Me.Controls.Add(Me.label3)
		Me.Controls.Add(Me.groupBox1)
		Me.Name = "Form1"
		Me.Text = "Live CDR Update"
		CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.groupBox1.ResumeLayout(False)
		Me.groupBox1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Private groupBox1 As GroupBox
		Private label1 As Label
		Private txtPassword As TextBox
		Private txtUsername As TextBox
		Private label2 As Label
	Private WithEvents btnGetCdr As Button
	Private WithEvents btnClose As Button
	Private label3 As Label
		Private label4 As Label
		Private txtDate As TextBox
		Private txtPeriods As TextBox
		Private label5 As Label
		Private txtRssd As TextBox
		Private pictureBox1 As PictureBox
		Private txtLog As TextBox
		Private lblFormat As Label
End Class