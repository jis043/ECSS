<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DoubleTrackBarWithLabels
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.dtb = New TextBoxEmailAutocomplete.DoubleTrackBar()
        Me.lblMin = New System.Windows.Forms.Label()
        Me.lblMax = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'dtb
        '
        Me.dtb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtb.Cursor = System.Windows.Forms.Cursors.Hand
        Me.dtb.Location = New System.Drawing.Point(32, 3)
        Me.dtb.Max = 100
        Me.dtb.Min = 0
        Me.dtb.Name = "dtb"
        Me.dtb.SelectedMax = 100
        Me.dtb.SelectedMin = 0
        Me.dtb.Size = New System.Drawing.Size(150, 20)
        Me.dtb.TabIndex = 0
        Me.dtb.TrackBarColor = System.Drawing.Color.Blue
        Me.dtb.Value = 50
        Me.dtb.ValueVisible = False
        '
        'lblMin
        '
        Me.lblMin.AutoSize = True
        Me.lblMin.Location = New System.Drawing.Point(3, 7)
        Me.lblMin.Name = "lblMin"
        Me.lblMin.Size = New System.Drawing.Size(23, 13)
        Me.lblMin.TabIndex = 1
        Me.lblMin.Text = "min"
        '
        'lblMax
        '
        Me.lblMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMax.AutoSize = True
        Me.lblMax.Location = New System.Drawing.Point(188, 7)
        Me.lblMax.Name = "lblMax"
        Me.lblMax.Size = New System.Drawing.Size(26, 13)
        Me.lblMax.TabIndex = 2
        Me.lblMax.Text = "max"
        '
        'DoubleTrackBarWithLabels
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblMax)
        Me.Controls.Add(Me.lblMin)
        Me.Controls.Add(Me.dtb)
        Me.Name = "DoubleTrackBarWithLabels"
        Me.Size = New System.Drawing.Size(218, 26)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dtb As TextBoxEmailAutocomplete.DoubleTrackBar
    Friend WithEvents lblMin As Label
    Friend WithEvents lblMax As Label
End Class
