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

    Public Shared Function ListToString(ByVal l As List(Of String)) As String
        If l Is Nothing OrElse l.Count = 0 Then Return ""
        Dim str As String = ""
        For Each s In l
            str = str & s & ", "
        Next
        str = str.Trim
        Return str.Substring(0, str.Length - 1)
    End Function
    Public Shared Function ListToString(ByVal l As List(Of Integer)) As String
        If l Is Nothing OrElse l.Count = 0 Then Return ""
        Dim str As String = ""
        For Each s In l
            str = str & s & ", "
        Next
        str = str.Trim
        Return str.Substring(0, str.Length - 1)
    End Function

    Public Shared Function ListToString(ByVal l As List(Of ECSSParts.PART_TYPE)) As String
        If l Is Nothing OrElse l.Count = 0 Then Return ""
        Dim str As String = ""
        For Each s In l
            str = str & ECSSParts.GetTypeName(s) & ", "
        Next
        str = str.Trim
        Return str.Substring(0, str.Length - 1)
    End Function
End Class
