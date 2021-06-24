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
    Public Shared Function ListToString(ByVal l As List(Of Single)) As String
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

    Public Shared Function NumberToGoodString(ByVal i As Integer) As String
        If i = 0 Then Return "N/A"
        If i = Integer.MinValue OrElse i = Integer.MaxValue Then Return "null"
        Return i.ToString
    End Function

    Public Shared Function NumberToGoodString(ByVal i As Double) As String
        If i = 0 Then Return "N/A"
        If i = Double.MinValue OrElse i = Double.MaxValue Then Return "null"
        Return i.ToString
    End Function

    Public Shared Function MM2INCH(ByVal m As Integer) As String
        Return String.Format("{0:F1}", m / 25.4)
    End Function

    Public Shared Function HttpPost(URL As String, Parameters As Specialized.NameValueCollection) As String
        Dim webClient As New Net.WebClient()
        Try
            EnableTLS1p2ForLegacySystems()
            Dim responseBytes As Byte() = webClient.UploadValues(URL, "POST", Parameters)
            Dim ResultAuthTicket As String = System.Text.Encoding.UTF8.GetString(responseBytes)
            Return ResultAuthTicket

        Catch ex As Exception
            Return "Error"
        Finally
            webClient.Dispose()
        End Try

    End Function

    Public Shared Sub EnableTLS1p2ForLegacySystems()
        If Environment.OSVersion.Platform = PlatformID.Win32NT AndAlso Environment.OSVersion.Version.Major < 10 Then
            '3072 is the TLS1.2 setting
            Net.ServicePointManager.SecurityProtocol = CType(3072, Net.SecurityProtocolType)
        End If
    End Sub
End Class
