Public Class ECSSParts

    Public PartDic As New Dictionary(Of String, OnePart) 'key =  part id
    Public Keywords As New List(Of String)

    Public Enum PART_TYPE
        NONE = 0
        TRANSFORMER = 1
        POWER_SUPPLY = 2
        ENCLOSURE = 3
        SERVIT_POST = 4
        BREATHER_DRAIN = 5
    End Enum
    Public Class OnePart
        Public PartID As String = ""
        Public PartType As PART_TYPE = PART_TYPE.NONE
        Public Manufacturer As String = ""
        Public Certificates1 As String = ""
        Public Certificates2 As String = ""
        Public Height As Double
        Public Width As Double
        Public Depth As Double
        Public Keywords As New List(Of String)
        Public link As String = ""
        Public Description As String = ""
        Public aTransformer As OneTransformer = Nothing
        Public aPowerSupply As OnePowerSupply = Nothing
        Public aEnclosure As OneEnclosure = Nothing
        Public aServitPost As OneServitPost = Nothing
        Public aBreatherDrain As OneBreatherDrain = Nothing

        Public Class OneTransformer
            Public Power As Integer
            Public Frequency As Integer
            Public Phase1 As Integer
            Public Pri_Voltage As Integer
            Public Phase2 As Integer
            Public Sec_Voltage As Integer
        End Class

        Public Class OnePowerSupply
            Public Output_Power As Integer
            Public Output_Current As Double
            Public Output_Vol As Integer
            Public Input_Vol As String = ""
            Public Opera_temp_min As Integer
            Public Opera_temp_max As Integer
            Public Stor_temp_min As Integer
            Public Stor_temp_max As Integer
            Public Area_Class As String = ""
            Public SupplyClass As String = ""
            Public Division As String = ""
            Public Gas_Group As String = ""
            Public Temp_Code As String = ""
        End Class

        Public Class OneEnclosure
            Public Style As String = ""
            Public NEMA_Type As String = ""
            Public Finish As String = ""
        End Class

        Public Class OneServitPost
            Public Part_type As String = ""
            Public Range_Stranded As String = ""
            Public Range_Soild As String = ""
            Public Material As String = ""
        End Class

        Public Class OneBreatherDrain
            Public Part_size As String = ""
            Public NEMA_Type As String = ""
            Public Material As String = ""
        End Class

    End Class

    Public Shared Function GetTypeName(ByVal t As PART_TYPE) As String
        Select Case t
            Case PART_TYPE.NONE
                Return "None"
            Case PART_TYPE.TRANSFORMER
                Return "Transformer"
            Case PART_TYPE.POWER_SUPPLY
                Return "Power Supply"
            Case PART_TYPE.ENCLOSURE
                Return "Enclosure"
            Case PART_TYPE.SERVIT_POST
                Return "Servit Post"
            Case PART_TYPE.BREATHER_DRAIN
                Return "Breather Drain"
            Case Else
                Return ""
        End Select
    End Function

    Public Shared Function GetPartType(ByVal s As String) As PART_TYPE
        Select Case s
            Case "None"
                Return PART_TYPE.NONE
            Case "Transformer"
                Return PART_TYPE.TRANSFORMER
            Case "Power Supply"
                Return PART_TYPE.POWER_SUPPLY
            Case "Enclosure"
                Return PART_TYPE.ENCLOSURE
            Case "Servit Post"
                Return PART_TYPE.SERVIT_POST
            Case "Breather Drain"
                Return PART_TYPE.BREATHER_DRAIN
            Case Else
                Return PART_TYPE.NONE
        End Select
    End Function

    Public Function ReadPartsFromDB() As Boolean
        If Me.PartDic Is Nothing Then Me.PartDic = New Dictionary(Of String, OnePart)
        Try
            Me.PartDic.Clear()
            Dim Result As DataTable = ECSSDBFunctions.SelectTransformer
            If Result IsNot Nothing OrElse Result.Rows.Count > 0 Then
                For Each oneRow As DataRow In Result.Rows
                    Dim apart = AddOneTransformer(oneRow)
                    If apart IsNot Nothing AndAlso Me.PartDic.ContainsKey(apart.PartID) = False Then Me.PartDic.Add(apart.PartID, apart)
                Next
            End If

            Result = ECSSDBFunctions.SelectPowerSupply
            If Result IsNot Nothing OrElse Result.Rows.Count > 0 Then
                For Each oneRow As DataRow In Result.Rows
                    Dim apart = AddOnePowerSupply(oneRow)
                    If apart IsNot Nothing AndAlso Me.PartDic.ContainsKey(apart.PartID) = False Then Me.PartDic.Add(apart.PartID, apart)
                Next
            End If

            Result = ECSSDBFunctions.SelectEnclosure
            If Result IsNot Nothing OrElse Result.Rows.Count > 0 Then
                For Each oneRow As DataRow In Result.Rows
                    Dim apart = AddOneEnclosure(oneRow)
                    If apart IsNot Nothing AndAlso Me.PartDic.ContainsKey(apart.PartID) = False Then Me.PartDic.Add(apart.PartID, apart)
                Next
            End If

            Result = ECSSDBFunctions.SelectServitPost
            If Result IsNot Nothing OrElse Result.Rows.Count > 0 Then
                For Each oneRow As DataRow In Result.Rows
                    Dim apart = AddOneServitPost(oneRow)
                    If apart IsNot Nothing AndAlso Me.PartDic.ContainsKey(apart.PartID) = False Then Me.PartDic.Add(apart.PartID, apart)
                Next
            End If

            Result = ECSSDBFunctions.SelectBreatherDrain
            If Result IsNot Nothing OrElse Result.Rows.Count > 0 Then
                For Each oneRow As DataRow In Result.Rows
                    Dim apart = AddOneBreatherDrain(oneRow)
                    If apart IsNot Nothing AndAlso Me.PartDic.ContainsKey(apart.PartID) = False Then Me.PartDic.Add(apart.PartID, apart)
                Next
            End If

            Return Me.GenerateKeywords
        Catch ex As Exception
            Debug.Print(ex.ToString)
            Return False
        End Try
    End Function

    Public Function GenerateKeywords() As Boolean
        Me.Keywords.Clear()
        If Me.PartDic IsNot Nothing Then
            For Each kvp In Me.PartDic
                If kvp.Value.Keywords IsNot Nothing AndAlso kvp.Value.Keywords.Count > 0 Then
                    For Each Str As String In kvp.Value.Keywords
                        If Me.Keywords.Contains(Str.Trim) = False Then Me.Keywords.Add(Str.Trim)
                    Next
                End If
                If String.IsNullOrEmpty(kvp.Value.Certificates1) = False Then
                    If Me.Keywords.Contains(kvp.Value.Certificates1.Trim) = False Then Me.Keywords.Add(kvp.Value.Certificates1.Trim)
                End If
                If String.IsNullOrEmpty(kvp.Value.Certificates2) = False Then
                    If Me.Keywords.Contains(kvp.Value.Certificates2.Trim) = False Then Me.Keywords.Add(kvp.Value.Certificates2.Trim)
                End If
                If String.IsNullOrEmpty(kvp.Value.PartID) = False Then
                    If Me.Keywords.Contains(kvp.Value.PartID.Trim) = False Then Me.Keywords.Add(kvp.Value.PartID.Trim)
                End If
                If String.IsNullOrEmpty(kvp.Value.Manufacturer) = False Then
                    If Me.Keywords.Contains(kvp.Value.Manufacturer.Trim) = False Then Me.Keywords.Add(kvp.Value.Manufacturer.Trim)
                End If
            Next
        End If
        Return True
    End Function


    Public Function AddOneTransformer(ByVal oneRow As DataRow) As OnePart
        Dim aPart As New OnePart
        Try
            aPart.PartType = PART_TYPE.TRANSFORMER
            aPart.aTransformer = New OnePart.OneTransformer
            aPart.PartID = oneRow("PartID").ToString
            aPart.Manufacturer = oneRow("Manufacturer").ToString
            aPart.Certificates1 = oneRow("Certificates1").ToString
            aPart.Certificates2 = oneRow("Certificates2").ToString
            aPart.Height = CDbl(oneRow("Height"))
            aPart.Width = CDbl(oneRow("Width"))
            aPart.Depth = CDbl(oneRow("Depth"))
            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.aTransformer.Power = CInt(oneRow("Power"))
            aPart.aTransformer.Frequency = CInt(oneRow("Frequency"))
            aPart.aTransformer.Phase1 = CInt(oneRow("Phase1"))
            aPart.aTransformer.Pri_Voltage = CInt(oneRow("Pri_Voltage"))
            aPart.aTransformer.Phase2 = CInt(oneRow("Phase2"))
            aPart.aTransformer.Sec_Voltage = CInt(oneRow("Sec_Voltage"))
        Catch ex As Exception
            MessageBox.Show("Error: Add Transformer.")
        End Try
        Return aPart
    End Function

    Public Function AddOnePowerSupply(ByVal oneRow As DataRow) As OnePart
        Dim aPart As New OnePart
        Try
            aPart.PartType = PART_TYPE.POWER_SUPPLY
            aPart.aPowerSupply = New OnePart.OnePowerSupply
            aPart.PartID = oneRow("PartID").ToString
            aPart.Manufacturer = oneRow("Manufacturer").ToString
            aPart.Certificates1 = oneRow("Certificates1").ToString
            aPart.Certificates2 = oneRow("Certificates2").ToString
            aPart.Height = CDbl(oneRow("Height"))
            aPart.Width = CDbl(oneRow("Width"))
            aPart.Depth = CDbl(oneRow("Depth"))
            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.link = oneRow("Link").ToString

            aPart.aPowerSupply.Output_Power = CInt(oneRow("Output_Power"))
            aPart.aPowerSupply.Output_Current = CInt(oneRow("Output_Current"))
            aPart.aPowerSupply.Output_Vol = CInt(oneRow("Output_Vol"))
            aPart.aPowerSupply.Opera_temp_min = CInt(oneRow("Opera_temp_min"))
            aPart.aPowerSupply.Opera_temp_max = CInt(oneRow("Opera_temp_max"))
            aPart.aPowerSupply.Stor_temp_min = CInt(oneRow("Stor_temp_min"))
            aPart.aPowerSupply.Stor_temp_max = CInt(oneRow("Stor_temp_max"))
            aPart.aPowerSupply.Area_Class = oneRow("Area_Class").ToString
            aPart.aPowerSupply.SupplyClass = oneRow("Class").ToString
            aPart.aPowerSupply.Division = oneRow("Division").ToString
            aPart.aPowerSupply.Gas_Group = oneRow("Gas_Group").ToString
            aPart.aPowerSupply.Temp_Code = oneRow("Temp_Code").ToString

        Catch ex As Exception
            MessageBox.Show("Error: Add Power Supply.")
        End Try
        Return aPart
    End Function

    Public Function AddOneEnclosure(ByVal oneRow As DataRow) As OnePart
        Dim aPart As New OnePart
        Try
            aPart.PartType = PART_TYPE.ENCLOSURE
            aPart.aEnclosure = New OnePart.OneEnclosure
            aPart.PartID = oneRow("PartID").ToString
            aPart.Manufacturer = oneRow("Manufacturer").ToString
            aPart.Certificates1 = oneRow("Certificates1").ToString
            aPart.Certificates2 = oneRow("Certificates2").ToString
            aPart.Height = CDbl(oneRow("Height"))
            aPart.Width = CDbl(oneRow("Width"))
            aPart.Depth = CDbl(oneRow("Depth"))
            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.link = oneRow("Link").ToString

            aPart.aEnclosure.Style = oneRow("Style").ToString
            aPart.aEnclosure.NEMA_Type = oneRow("NEMA_Type").ToString
            aPart.aEnclosure.Finish = oneRow("Finish").ToString
        Catch ex As Exception
            MessageBox.Show("Error: Add Enclosure.")
        End Try
        Return aPart
    End Function

    Public Function AddOneServitPost(ByVal oneRow As DataRow) As OnePart
        Dim aPart As New OnePart
        Try
            aPart.PartType = PART_TYPE.SERVIT_POST
            aPart.aServitPost = New OnePart.OneServitPost
            aPart.PartID = oneRow("PartID").ToString
            aPart.Manufacturer = oneRow("Manufacturer").ToString
            aPart.Certificates1 = oneRow("Certificates1").ToString
            aPart.Certificates2 = oneRow("Certificates2").ToString

            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.link = oneRow("Link").ToString

            aPart.aServitPost.Part_type = oneRow("Part_type").ToString
            aPart.aServitPost.Range_Stranded = oneRow("Range_Stranded").ToString
            aPart.aServitPost.Range_Soild = oneRow("Range_Soild").ToString
            aPart.aServitPost.Material = oneRow("Material").ToString
        Catch ex As Exception
            MessageBox.Show("Error: Add Servit Post.")
        End Try
        Return aPart
    End Function

    Public Function AddOneBreatherDrain(ByVal oneRow As DataRow) As OnePart
        Dim aPart As New OnePart
        Try
            aPart.PartType = PART_TYPE.BREATHER_DRAIN
            aPart.aBreatherDrain = New OnePart.OneBreatherDrain
            aPart.PartID = oneRow("PartID").ToString
            aPart.Manufacturer = oneRow("Manufacturer").ToString
            aPart.Certificates1 = oneRow("Certificates1").ToString
            aPart.Certificates2 = oneRow("Certificates2").ToString

            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.link = oneRow("Link").ToString

            aPart.aBreatherDrain.Part_size = oneRow("Part_size").ToString
            aPart.aBreatherDrain.NEMA_Type = oneRow("NEMA_Type").ToString
            aPart.aBreatherDrain.Material = oneRow("Material").ToString

        Catch ex As Exception
            MessageBox.Show("Error: Add Servit Post.")
        End Try
        Return aPart
    End Function

    Private Function GenKeyWords(ByVal str As String) As List(Of String)
        Dim keys As New List(Of String)
        If String.IsNullOrEmpty(str) = False Then
            keys.AddRange(str.Split(","))
        End If
        For Each str In keys
            str = str.Trim
        Next
        Return keys
    End Function


    Public Function PartMatchesSearchCriteria(ByVal aPart As OnePart, ByVal searchCondition As ECSSSearchCriteria) As Boolean
        If aPart Is Nothing OrElse searchCondition Is Nothing Then Return False
        If Not PartMatchSearchWords(aPart, searchCondition.keyword.Trim) Then Return False
        If Not PartMatchFilterPartType(aPart, searchCondition.PartType) Then Return False
        If Not PartMatchFilterManufacturer(aPart, searchCondition.Manufacturer) Then Return False
        If Not PartMatchFilterMaterial(aPart, searchCondition.Material) Then Return False
        Return True
    End Function

    Public Function PartMatchSearchWords(ByVal aPart As OnePart, ByVal keyword As String) As Boolean
        If String.IsNullOrEmpty(keyword) Then Return True
        If aPart Is Nothing Then Return False
        Dim wordMatches As Boolean = False
        If aPart.PartID.ToUpper.Contains(keyword.ToUpper) Then wordMatches = True
        If aPart.Manufacturer.ToUpper.Contains(keyword.ToUpper) Then wordMatches = True
        If aPart.Certificates1.ToUpper.Contains(keyword.ToUpper) Then wordMatches = True
        If aPart.Certificates2.ToUpper.Contains(keyword.ToUpper) Then wordMatches = True
        If aPart.Description.ToUpper.Contains(keyword.ToUpper) Then wordMatches = True
        If aPart.link.ToUpper.Contains(keyword.ToUpper) Then wordMatches = True
        If aPart.Keywords IsNot Nothing Then
            For Each Str As String In aPart.Keywords
                If Str.ToUpper.Contains(keyword.ToUpper) Then wordMatches = True : Exit For
            Next
        End If
        Return wordMatches
    End Function

    Public Function PartMatchFilterPartType(ByVal aPart As OnePart, ByVal PType As List(Of PART_TYPE)) As Boolean
        If PType Is Nothing Then Return True
        If PType.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If PType.Contains(aPart.PartType) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterManufacturer(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If MList.Contains(aPart.Manufacturer) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterMaterial(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aBreatherDrain IsNot Nothing AndAlso MList.Contains(aPart.aBreatherDrain.Material) Then filterMatches = True
        If aPart.aServitPost IsNot Nothing AndAlso MList.Contains(aPart.aServitPost.Material) Then filterMatches = True
        Return filterMatches
    End Function

End Class

Public Class ECSSSearchCriteria
    Public keyword As String = ""
    Public PartType As New List(Of ECSSParts.PART_TYPE)
    Public Manufacturer As New List(Of String)
    Public Material As New List(Of String)
End Class


Public Class ECSSBOM
    Public BOMID As String = ""
    Public QTY As Integer = 0
    Public PartID As String = ""
    Public Manufacturer As String = ""
    Public Description As String = ""
    Public Note As String = ""
End Class

Public Class BOMHelper
    Public Shared Function LoadBOM(ByVal BOMdic As Dictionary(Of String, List(Of ECSSBOM))) As Boolean
        Try
            BOMdic.Clear()
            Dim result = ECSSDBFunctions.SelectBOM
            If result IsNot Nothing AndAlso result.Rows.Count > 0 Then
                For Each aRow As DataRow In result.Rows
                    Dim aBOM As New ECSSBOM
                    aBOM.BOMID = aRow.Item("BOMNAME").ToString
                    aBOM.QTY = CInt(aRow.Item("PART_QTY"))
                    aBOM.PartID = aRow.Item("PARTID").ToString
                    aBOM.Manufacturer = aRow.Item("Manufacturer").ToString
                    aBOM.Description = aRow.Item("Description").ToString
                    aBOM.Note = aRow.Item("NOTE").ToString
                    If BOMdic.ContainsKey(aBOM.BOMID) Then
                        BOMdic.Item(aBOM.BOMID).Add(aBOM)
                    Else
                        BOMdic.Add(aBOM.BOMID, New List(Of ECSSBOM)({aBOM}))
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Public Shared Function EnableFilter(ByVal search As ECSSSearchCriteria) As Boolean
    '    If search IsNot Nothing Then
    '        If search.PartType IsNot Nothing AndAlso search.PartType.Count > 0 Then Return True
    '        If search.Manufacturer IsNot Nothing AndAlso search.Manufacturer.Count > 0 Then Return True
    '        If search.Material IsNot Nothing AndAlso search.Material.Count > 0 Then Return True
    '        If search.PartType.Count = 0 AndAlso search.Manufacturer.Count = 0 AndAlso search.Material.Count = 0 Then Return False
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function
End Class
