<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmKeygen
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
        Me.txtDev = New System.Windows.Forms.TextBox()
        Me.btnDev = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMachineID = New System.Windows.Forms.TextBox()
        Me.txtKey = New System.Windows.Forms.TextBox()
        Me.btnStandard = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtDev
        '
        Me.txtDev.Location = New System.Drawing.Point(12, 14)
        Me.txtDev.Name = "txtDev"
        Me.txtDev.Size = New System.Drawing.Size(321, 20)
        Me.txtDev.TabIndex = 0
        '
        'btnDev
        '
        Me.btnDev.Location = New System.Drawing.Point(339, 12)
        Me.btnDev.Name = "btnDev"
        Me.btnDev.Size = New System.Drawing.Size(158, 23)
        Me.btnDev.TabIndex = 1
        Me.btnDev.Text = "Gen Dev Key"
        Me.btnDev.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(-1, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(511, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "---------------------------------------------------------------------------------" &
    "--------------------------------------------------------------------------------" &
    "---------------------------------------"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMachineID
        '
        Me.txtMachineID.Location = New System.Drawing.Point(12, 81)
        Me.txtMachineID.Name = "txtMachineID"
        Me.txtMachineID.Size = New System.Drawing.Size(321, 20)
        Me.txtMachineID.TabIndex = 3
        '
        'txtKey
        '
        Me.txtKey.Location = New System.Drawing.Point(12, 107)
        Me.txtKey.Name = "txtKey"
        Me.txtKey.Size = New System.Drawing.Size(321, 20)
        Me.txtKey.TabIndex = 4
        '
        'btnStandard
        '
        Me.btnStandard.Location = New System.Drawing.Point(339, 105)
        Me.btnStandard.Name = "btnStandard"
        Me.btnStandard.Size = New System.Drawing.Size(158, 23)
        Me.btnStandard.TabIndex = 5
        Me.btnStandard.Text = "Gen Standard Key"
        Me.btnStandard.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(339, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Machine ID"
        '
        'frmKeygen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(509, 208)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnStandard)
        Me.Controls.Add(Me.txtKey)
        Me.Controls.Add(Me.txtMachineID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnDev)
        Me.Controls.Add(Me.txtDev)
        Me.Name = "frmKeygen"
        Me.Text = "Key Gen"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtDev As TextBox
    Friend WithEvents btnDev As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtMachineID As TextBox
    Friend WithEvents txtKey As TextBox
    Friend WithEvents btnStandard As Button
    Friend WithEvents Label2 As Label
End Class
