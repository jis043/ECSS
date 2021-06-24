<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGeneralSettings
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
        Me.TabGeneral = New System.Windows.Forms.TabControl()
        Me.tabSettings = New System.Windows.Forms.TabPage()
        Me.grpGeneral = New System.Windows.Forms.GroupBox()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.chkOnTop = New System.Windows.Forms.CheckBox()
        Me.txtMax = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkMultiKeywords = New System.Windows.Forms.CheckBox()
        Me.tabUnit = New System.Windows.Forms.TabPage()
        Me.listUnit = New System.Windows.Forms.ListView()
        Me.colVar = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colUnit = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tabAuth = New System.Windows.Forms.TabPage()
        Me.lblAuthLevel = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnAuth = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.txtAuthCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMachine = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tabAbout = New System.Windows.Forms.TabPage()
        Me.palAbout = New System.Windows.Forms.Panel()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblAbout = New System.Windows.Forms.Label()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.TabGeneral.SuspendLayout()
        Me.tabSettings.SuspendLayout()
        Me.grpGeneral.SuspendLayout()
        Me.tabUnit.SuspendLayout()
        Me.tabAuth.SuspendLayout()
        Me.tabAbout.SuspendLayout()
        Me.palAbout.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabGeneral
        '
        Me.TabGeneral.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.TabGeneral.Controls.Add(Me.tabSettings)
        Me.TabGeneral.Controls.Add(Me.tabUnit)
        Me.TabGeneral.Controls.Add(Me.tabAuth)
        Me.TabGeneral.Controls.Add(Me.tabAbout)
        Me.TabGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabGeneral.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.TabGeneral.ItemSize = New System.Drawing.Size(25, 100)
        Me.TabGeneral.Location = New System.Drawing.Point(0, 0)
        Me.TabGeneral.Multiline = True
        Me.TabGeneral.Name = "TabGeneral"
        Me.TabGeneral.SelectedIndex = 0
        Me.TabGeneral.Size = New System.Drawing.Size(443, 312)
        Me.TabGeneral.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabGeneral.TabIndex = 0
        '
        'tabSettings
        '
        Me.tabSettings.Controls.Add(Me.grpGeneral)
        Me.tabSettings.Location = New System.Drawing.Point(104, 4)
        Me.tabSettings.Name = "tabSettings"
        Me.tabSettings.Size = New System.Drawing.Size(335, 304)
        Me.tabSettings.TabIndex = 2
        Me.tabSettings.Text = "Settings"
        Me.tabSettings.UseVisualStyleBackColor = True
        '
        'grpGeneral
        '
        Me.grpGeneral.Controls.Add(Me.btnApply)
        Me.grpGeneral.Controls.Add(Me.chkOnTop)
        Me.grpGeneral.Controls.Add(Me.txtMax)
        Me.grpGeneral.Controls.Add(Me.Label1)
        Me.grpGeneral.Controls.Add(Me.chkMultiKeywords)
        Me.grpGeneral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpGeneral.Location = New System.Drawing.Point(0, 0)
        Me.grpGeneral.Name = "grpGeneral"
        Me.grpGeneral.Size = New System.Drawing.Size(335, 304)
        Me.grpGeneral.TabIndex = 1
        Me.grpGeneral.TabStop = False
        Me.grpGeneral.Text = "General Settings"
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(252, 273)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 4
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'chkOnTop
        '
        Me.chkOnTop.AutoSize = True
        Me.chkOnTop.Location = New System.Drawing.Point(16, 48)
        Me.chkOnTop.Name = "chkOnTop"
        Me.chkOnTop.Size = New System.Drawing.Size(98, 17)
        Me.chkOnTop.TabIndex = 3
        Me.chkOnTop.Text = "Always On Top"
        Me.chkOnTop.UseVisualStyleBackColor = True
        '
        'txtMax
        '
        Me.txtMax.Location = New System.Drawing.Point(165, 81)
        Me.txtMax.Name = "txtMax"
        Me.txtMax.Size = New System.Drawing.Size(100, 20)
        Me.txtMax.TabIndex = 2
        Me.txtMax.Text = "1000"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Max Number of Parts Display "
        '
        'chkMultiKeywords
        '
        Me.chkMultiKeywords.AutoSize = True
        Me.chkMultiKeywords.Location = New System.Drawing.Point(16, 25)
        Me.chkMultiKeywords.Name = "chkMultiKeywords"
        Me.chkMultiKeywords.Size = New System.Drawing.Size(184, 17)
        Me.chkMultiKeywords.TabIndex = 0
        Me.chkMultiKeywords.Text = "Enable Multiple Keywords Search"
        Me.chkMultiKeywords.UseVisualStyleBackColor = True
        '
        'tabUnit
        '
        Me.tabUnit.Controls.Add(Me.listUnit)
        Me.tabUnit.Location = New System.Drawing.Point(104, 4)
        Me.tabUnit.Name = "tabUnit"
        Me.tabUnit.Padding = New System.Windows.Forms.Padding(3)
        Me.tabUnit.Size = New System.Drawing.Size(335, 304)
        Me.tabUnit.TabIndex = 0
        Me.tabUnit.Text = "System Unit"
        Me.tabUnit.UseVisualStyleBackColor = True
        '
        'listUnit
        '
        Me.listUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.listUnit.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colVar, Me.colUnit})
        Me.listUnit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listUnit.FullRowSelect = True
        Me.listUnit.GridLines = True
        Me.listUnit.HideSelection = False
        Me.listUnit.Location = New System.Drawing.Point(3, 3)
        Me.listUnit.Name = "listUnit"
        Me.listUnit.Size = New System.Drawing.Size(329, 298)
        Me.listUnit.TabIndex = 0
        Me.listUnit.UseCompatibleStateImageBehavior = False
        Me.listUnit.View = System.Windows.Forms.View.Details
        '
        'colVar
        '
        Me.colVar.Text = "Parameter"
        Me.colVar.Width = 120
        '
        'colUnit
        '
        Me.colUnit.Text = "Unit"
        Me.colUnit.Width = 120
        '
        'tabAuth
        '
        Me.tabAuth.Controls.Add(Me.RichTextBox1)
        Me.tabAuth.Controls.Add(Me.lblAuthLevel)
        Me.tabAuth.Controls.Add(Me.Label4)
        Me.tabAuth.Controls.Add(Me.btnAuth)
        Me.tabAuth.Controls.Add(Me.btnEdit)
        Me.tabAuth.Controls.Add(Me.txtAuthCode)
        Me.tabAuth.Controls.Add(Me.Label3)
        Me.tabAuth.Controls.Add(Me.txtMachine)
        Me.tabAuth.Controls.Add(Me.Label2)
        Me.tabAuth.Location = New System.Drawing.Point(104, 4)
        Me.tabAuth.Name = "tabAuth"
        Me.tabAuth.Size = New System.Drawing.Size(335, 304)
        Me.tabAuth.TabIndex = 3
        Me.tabAuth.Text = "Authorization"
        Me.tabAuth.UseVisualStyleBackColor = True
        '
        'lblAuthLevel
        '
        Me.lblAuthLevel.AutoSize = True
        Me.lblAuthLevel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuthLevel.Location = New System.Drawing.Point(156, 133)
        Me.lblAuthLevel.Name = "lblAuthLevel"
        Me.lblAuthLevel.Size = New System.Drawing.Size(63, 20)
        Me.lblAuthLevel.TabIndex = 7
        Me.lblAuthLevel.Text = "Label5"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Authorization Level:"
        '
        'btnAuth
        '
        Me.btnAuth.Location = New System.Drawing.Point(187, 89)
        Me.btnAuth.Name = "btnAuth"
        Me.btnAuth.Size = New System.Drawing.Size(119, 23)
        Me.btnAuth.TabIndex = 5
        Me.btnAuth.Text = "Authorization"
        Me.btnAuth.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(106, 89)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnEdit.TabIndex = 4
        Me.btnEdit.Text = "Edit Code"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'txtAuthCode
        '
        Me.txtAuthCode.Location = New System.Drawing.Point(106, 63)
        Me.txtAuthCode.Name = "txtAuthCode"
        Me.txtAuthCode.ReadOnly = True
        Me.txtAuthCode.Size = New System.Drawing.Size(200, 20)
        Me.txtAuthCode.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Activation Code:"
        '
        'txtMachine
        '
        Me.txtMachine.Location = New System.Drawing.Point(106, 27)
        Me.txtMachine.Name = "txtMachine"
        Me.txtMachine.ReadOnly = True
        Me.txtMachine.Size = New System.Drawing.Size(200, 20)
        Me.txtMachine.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Machine ID:"
        '
        'tabAbout
        '
        Me.tabAbout.Controls.Add(Me.palAbout)
        Me.tabAbout.Location = New System.Drawing.Point(104, 4)
        Me.tabAbout.Name = "tabAbout"
        Me.tabAbout.Padding = New System.Windows.Forms.Padding(3)
        Me.tabAbout.Size = New System.Drawing.Size(335, 304)
        Me.tabAbout.TabIndex = 1
        Me.tabAbout.Text = "About"
        Me.tabAbout.UseVisualStyleBackColor = True
        '
        'palAbout
        '
        Me.palAbout.Controls.Add(Me.lblVersion)
        Me.palAbout.Controls.Add(Me.lblAbout)
        Me.palAbout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.palAbout.Location = New System.Drawing.Point(3, 3)
        Me.palAbout.Name = "palAbout"
        Me.palAbout.Size = New System.Drawing.Size(329, 298)
        Me.palAbout.TabIndex = 0
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(32, 98)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(39, 13)
        Me.lblVersion.TabIndex = 1
        Me.lblVersion.Text = "Label2"
        '
        'lblAbout
        '
        Me.lblAbout.Location = New System.Drawing.Point(32, 40)
        Me.lblAbout.Name = "lblAbout"
        Me.lblAbout.Size = New System.Drawing.Size(272, 210)
        Me.lblAbout.TabIndex = 0
        Me.lblAbout.Text = "Engineering Components Search Systems" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Copyright 2021 Prexeco. All rights Reser" &
    "ved." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Design and Developed by Sam S, Mark L, Randy L"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox1.Location = New System.Drawing.Point(18, 196)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(288, 66)
        Me.RichTextBox1.TabIndex = 8
        Me.RichTextBox1.Text = "I understand when I use this software, I CONSENT to allow Prexeco to anonymously " &
    "collect usage statistics in order to improve the software."
        '
        'frmGeneralSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 312)
        Me.Controls.Add(Me.TabGeneral)
        Me.Name = "frmGeneralSettings"
        Me.Text = "Settings"
        Me.TabGeneral.ResumeLayout(False)
        Me.tabSettings.ResumeLayout(False)
        Me.grpGeneral.ResumeLayout(False)
        Me.grpGeneral.PerformLayout()
        Me.tabUnit.ResumeLayout(False)
        Me.tabAuth.ResumeLayout(False)
        Me.tabAuth.PerformLayout()
        Me.tabAbout.ResumeLayout(False)
        Me.palAbout.ResumeLayout(False)
        Me.palAbout.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabGeneral As TabControl
    Friend WithEvents tabUnit As TabPage
    Friend WithEvents tabAbout As TabPage
    Friend WithEvents listUnit As ListView
    Friend WithEvents colVar As ColumnHeader
    Friend WithEvents colUnit As ColumnHeader
    Friend WithEvents palAbout As Panel
    Friend WithEvents lblAbout As Label
    Friend WithEvents tabSettings As TabPage
    Friend WithEvents grpGeneral As GroupBox
    Friend WithEvents txtMax As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents chkMultiKeywords As CheckBox
    Friend WithEvents chkOnTop As CheckBox
    Friend WithEvents lblVersion As Label
    Friend WithEvents btnApply As Button
    Friend WithEvents tabAuth As TabPage
    Friend WithEvents lblAuthLevel As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnAuth As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents txtAuthCode As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtMachine As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents RichTextBox1 As RichTextBox
End Class
