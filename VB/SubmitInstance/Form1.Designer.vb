<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
		Me.txtLog = New System.Windows.Forms.TextBox()
		Me.pictureBox1 = New System.Windows.Forms.PictureBox()
		Me.groupBox1 = New System.Windows.Forms.GroupBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.txtFile = New System.Windows.Forms.TextBox()
		Me.txtPassword = New System.Windows.Forms.TextBox()
		Me.txtUsername = New System.Windows.Forms.TextBox()
		Me.label2 = New System.Windows.Forms.Label()
		Me.label1 = New System.Windows.Forms.Label()
		Me.chcTest = New System.Windows.Forms.CheckBox()
		Me.btnSubmit = New System.Windows.Forms.Button()
		Me.btnClose = New System.Windows.Forms.Button()
		CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.groupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'txtLog
		'
		Me.txtLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtLog.Location = New System.Drawing.Point(12, 12)
		Me.txtLog.Multiline = True
		Me.txtLog.Name = "txtLog"
		Me.txtLog.Size = New System.Drawing.Size(687, 209)
		Me.txtLog.TabIndex = 3
		'
		'pictureBox1
		'
		Me.pictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.pictureBox1.Image = CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image)
		Me.pictureBox1.Location = New System.Drawing.Point(12, 236)
		Me.pictureBox1.Name = "pictureBox1"
		Me.pictureBox1.Size = New System.Drawing.Size(151, 147)
		Me.pictureBox1.TabIndex = 21
		Me.pictureBox1.TabStop = False
		'
		'groupBox1
		'
		Me.groupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox1.Controls.Add(Me.Label3)
		Me.groupBox1.Controls.Add(Me.txtFile)
		Me.groupBox1.Controls.Add(Me.txtPassword)
		Me.groupBox1.Controls.Add(Me.txtUsername)
		Me.groupBox1.Controls.Add(Me.label2)
		Me.groupBox1.Controls.Add(Me.label1)
		Me.groupBox1.Location = New System.Drawing.Point(180, 227)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(520, 274)
		Me.groupBox1.TabIndex = 0
		Me.groupBox1.TabStop = False
		Me.groupBox1.Text = "CDR Login"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(21, 195)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(129, 25)
		Me.Label3.TabIndex = 5
		Me.Label3.Text = "Call Report File"
		'
		'txtFile
		'
		Me.txtFile.Location = New System.Drawing.Point(169, 195)
		Me.txtFile.Name = "txtFile"
		Me.txtFile.Size = New System.Drawing.Size(320, 31)
		Me.txtFile.TabIndex = 4
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
		Me.txtUsername.TabIndex = 0
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
		'chcTest
		'
		Me.chcTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.chcTest.AutoSize = True
		Me.chcTest.Location = New System.Drawing.Point(39, 534)
		Me.chcTest.Name = "chcTest"
		Me.chcTest.Size = New System.Drawing.Size(126, 29)
		Me.chcTest.TabIndex = 23
		Me.chcTest.Text = "Test Report"
		Me.chcTest.UseVisualStyleBackColor = True
		'
		'btnSubmit
		'
		Me.btnSubmit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnSubmit.Location = New System.Drawing.Point(201, 530)
		Me.btnSubmit.Name = "btnSubmit"
		Me.btnSubmit.Size = New System.Drawing.Size(205, 33)
		Me.btnSubmit.TabIndex = 1
		Me.btnSubmit.Text = "&Submit Call Report"
		Me.btnSubmit.UseVisualStyleBackColor = True
		'
		'btnClose
		'
		Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnClose.Location = New System.Drawing.Point(531, 530)
		Me.btnClose.Name = "btnClose"
		Me.btnClose.Size = New System.Drawing.Size(169, 33)
		Me.btnClose.TabIndex = 2
		Me.btnClose.Text = "&Close"
		Me.btnClose.UseVisualStyleBackColor = True
		'
		'Form1
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 25.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(743, 587)
		Me.Controls.Add(Me.btnClose)
		Me.Controls.Add(Me.btnSubmit)
		Me.Controls.Add(Me.chcTest)
		Me.Controls.Add(Me.groupBox1)
		Me.Controls.Add(Me.pictureBox1)
		Me.Controls.Add(Me.txtLog)
		Me.Name = "Form1"
		Me.Text = "Live CDR Call Report Submission"
		CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.groupBox1.ResumeLayout(False)
		Me.groupBox1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Private WithEvents txtLog As TextBox
	Private WithEvents pictureBox1 As PictureBox
	Private WithEvents groupBox1 As GroupBox
	Private WithEvents Label3 As Label
	Private WithEvents txtFile As TextBox
	Private WithEvents txtPassword As TextBox
	Private WithEvents txtUsername As TextBox
	Private WithEvents label2 As Label
	Private WithEvents label1 As Label
	Friend WithEvents chcTest As CheckBox
	Private WithEvents btnSubmit As Button
	Private WithEvents btnClose As Button
End Class
