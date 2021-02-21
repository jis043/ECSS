Public Class frmGeneralSettings

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmGeneralSettings_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

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


End Class