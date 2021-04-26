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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkOnTop = New System.Windows.Forms.CheckBox()
        Me.txtMax = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkMultiKeywords = New System.Windows.Forms.CheckBox()
        Me.tabUnit = New System.Windows.Forms.TabPage()
        Me.listUnit = New System.Windows.Forms.ListView()
        Me.colVar = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colUnit = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tabAbout = New System.Windows.Forms.TabPage()
        Me.palAbout = New System.Windows.Forms.Panel()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblAbout = New System.Windows.Forms.Label()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.TabGeneral.SuspendLayout()
        Me.tabSettings.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.tabUnit.SuspendLayout()
        Me.tabAbout.SuspendLayout()
        Me.palAbout.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabGeneral
        '
        Me.TabGeneral.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.TabGeneral.Controls.Add(Me.tabSettings)
        Me.TabGeneral.Controls.Add(Me.tabUnit)
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
        Me.tabSettings.Controls.Add(Me.GroupBox1)
        Me.tabSettings.Location = New System.Drawing.Point(104, 4)
        Me.tabSettings.Name = "tabSettings"
        Me.tabSettings.Size = New System.Drawing.Size(335, 304)
        Me.tabSettings.TabIndex = 2
        Me.tabSettings.Text = "Settings"
        Me.tabSettings.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnApply)
        Me.GroupBox1.Controls.Add(Me.chkOnTop)
        Me.GroupBox1.Controls.Add(Me.txtMax)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.chkMultiKeywords)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(335, 304)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "General Settings"
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
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(252, 273)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 4
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = True
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
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.tabUnit.ResumeLayout(False)
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
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents txtMax As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents chkMultiKeywords As CheckBox
    Friend WithEvents chkOnTop As CheckBox
    Friend WithEvents lblVersion As Label
    Friend WithEvents btnApply As Button
End Class
