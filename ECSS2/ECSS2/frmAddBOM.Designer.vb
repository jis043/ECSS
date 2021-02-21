<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddBOM
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.radExisting = New System.Windows.Forms.RadioButton()
        Me.radNew = New System.Windows.Forms.RadioButton()
        Me.cboBOM = New System.Windows.Forms.ComboBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Add Select Part to BOM"
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(133, 86)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(150, 20)
        Me.txtQty.TabIndex = 11
        Me.txtQty.Text = "1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Qty:"
        '
        'radExisting
        '
        Me.radExisting.AutoSize = True
        Me.radExisting.Location = New System.Drawing.Point(12, 59)
        Me.radExisting.Name = "radExisting"
        Me.radExisting.Size = New System.Drawing.Size(56, 17)
        Me.radExisting.TabIndex = 9
        Me.radExisting.TabStop = True
        Me.radExisting.Text = "Add to"
        Me.radExisting.UseVisualStyleBackColor = True
        '
        'radNew
        '
        Me.radNew.AutoSize = True
        Me.radNew.Checked = True
        Me.radNew.Location = New System.Drawing.Point(12, 33)
        Me.radNew.Name = "radNew"
        Me.radNew.Size = New System.Drawing.Size(115, 17)
        Me.radNew.TabIndex = 8
        Me.radNew.TabStop = True
        Me.radNew.Text = "Create a new BOM"
        Me.radNew.UseVisualStyleBackColor = True
        '
        'cboBOM
        '
        Me.cboBOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBOM.FormattingEnabled = True
        Me.cboBOM.Location = New System.Drawing.Point(133, 58)
        Me.cboBOM.Name = "cboBOM"
        Me.cboBOM.Size = New System.Drawing.Size(150, 21)
        Me.cboBOM.TabIndex = 7
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(133, 32)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(150, 20)
        Me.txtName.TabIndex = 13
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(248, 123)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 14
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(167, 123)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 15
        Me.btnOK.Text = "Confirm"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'frmAddBOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(335, 158)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.radExisting)
        Me.Controls.Add(Me.radNew)
        Me.Controls.Add(Me.cboBOM)
        Me.Name = "frmAddBOM"
        Me.Text = "Add to BOM"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents txtQty As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents radExisting As RadioButton
    Friend WithEvents radNew As RadioButton
    Friend WithEvents cboBOM As ComboBox
    Friend WithEvents txtName As TextBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOK As Button
End Class
