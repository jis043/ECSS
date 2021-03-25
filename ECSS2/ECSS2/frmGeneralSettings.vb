Imports System.ComponentModel

Public Class frmGeneralSettings

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmGeneralSettings_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.chkMultiKeywords.Checked = GlobalSettings.MultiKeywords
            Me.chkOnTop.Checked = GlobalSettings.AlwaysOnTop
            Me.txtMax.Text = GlobalSettings.MaxDisplay
            Me.lblVersion.Text = GlobalSettings.GetCurrentVersion & " (Demo)"
            LoadUnit()

        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub LoadUnit()
        Dim Result = ECSSDBFunctions.SelectUnit
        If Result IsNot Nothing OrElse Result.Rows.Count > 0 Then

            Me.listUnit.Items.Clear()
            Me.listUnit.BeginUpdate()
            For Each oneRow As DataRow In Result.Rows

                Dim aitem As New ListViewItem(oneRow.Item("Parameter").ToString)
                aitem.SubItems.Add(oneRow.Item("Unit").ToString)
                Me.listUnit.Items.Add(aitem)

            Next
            Me.listUnit.EndUpdate()
            Miscelllaneous.PaintAlternatingBackColor(Me.listUnit, Color.White, Color.Honeydew)

        End If
    End Sub

    Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabGeneral.DrawItem
        Dim g As Graphics = e.Graphics
        Dim _TextBrush As Brush

        ' Get the item from the collection.
        Dim _TabPage As TabPage = TabGeneral.TabPages(e.Index)

        ' Get the real bounds for the tab rectangle.
        Dim _TabBounds As Rectangle = TabGeneral.GetTabRect(e.Index)

        If (e.State = DrawItemState.Selected) Then
            ' Draw a different background color, and don't paint a focus rectangle.
            _TextBrush = New SolidBrush(Color.Black)
            g.FillRectangle(Brushes.Gray, e.Bounds)
        Else
            _TextBrush = New System.Drawing.SolidBrush(e.ForeColor)
            e.DrawBackground()
        End If

        ' Use our own font.
        Dim _TabFont As New Font("Arial", 10.0, FontStyle.Bold, GraphicsUnit.Pixel)

        ' Draw string. Center the text.
        Dim _StringFlags As New StringFormat()
        _StringFlags.Alignment = StringAlignment.Center
        _StringFlags.LineAlignment = StringAlignment.Center
        g.DrawString(_TabPage.Text, _TabFont, _TextBrush, _TabBounds, New StringFormat(_StringFlags))
    End Sub

    Private Sub chkMultiKeywords_CheckedChanged(sender As Object, e As EventArgs) Handles chkMultiKeywords.CheckedChanged
        GlobalSettings.MultiKeywords = chkMultiKeywords.Checked
    End Sub
    Private Sub chkOnTop_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnTop.CheckedChanged
        GlobalSettings.AlwaysOnTop = chkOnTop.Checked
    End Sub
    Private Sub txtMax_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtMax.Validating
        If String.IsNullOrEmpty(Me.txtMax.Text.Trim) Then
            MessageBox.Show("Please input a number.")
            Me.txtMax.SelectAll()
            Me.txtMax.Focus()
        ElseIf IsNumeric(Me.txtMax.Text) = False Then
            MessageBox.Show("Please input a number.")
            Me.txtMax.SelectAll()
            Me.txtMax.Focus()
        Else
            GlobalSettings.MaxDisplay = CInt(Me.txtMax.Text)
        End If
    End Sub

    Private Sub frmGeneralSettings_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            ECSSDBFunctions.UpdateUserConfig()
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

End Class