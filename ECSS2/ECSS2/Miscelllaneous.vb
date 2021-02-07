Public Class Miscelllaneous
    Public Shared Sub PaintAlternatingBackColor(ByVal lv As ListView, ByVal color1 As Color, ByVal color2 As Color)
        Dim item As ListViewItem
        Dim subitem As ListViewItem.ListViewSubItem

        For Each item In lv.Items
            ' set the color for the Item -- the first column
            If (item.Index Mod 2) = 0 Then
                item.BackColor = color1
            Else
                item.BackColor = color2
            End If

            ' Assign same color to all subitems.
            For Each subitem In item.SubItems
                subitem.BackColor = item.BackColor
            Next
        Next
    End Sub
End Class
