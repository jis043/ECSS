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
        WINDOW_KIT = 6
        THEROMOSTAT = 7
        TEMP_SWITCH = 8
        PILOT_LIGHT = 9
        SELECTOR_SWITCH = 10
        PUSH_BUTTON = 11
        ESTOP = 12
        HEATER = 13
    End Enum
    Public Class OnePart
        Public PartID As String = ""
        Public PartType As PART_TYPE = PART_TYPE.NONE
        Public Manufacturer As String = ""
        Public Certificates As New List(Of String)
        Public Height As Double
        Public Width As Double
        Public Depth As Double
        Public Keywords As New List(Of String)
        Public link As String = ""
        Public Description As String = ""
        Public PDF As String = ""
        Public aTransformer As OneTransformer = Nothing
        Public aPowerSupply As OnePowerSupply = Nothing
        Public aEnclosure As OneEnclosure = Nothing
        Public aServitPost As OneServitPost = Nothing
        Public aBreatherDrain As OneBreatherDrain = Nothing
        Public aWindowKit As OneWindowKit = Nothing
        Public aTempSwitch As OneTempSwitch = Nothing
        Public aTheromostat As OneTheromostat = Nothing
        Public aPilotLight As OnePilotLight = Nothing
        Public aHeater As OneHeater = Nothing
        Public aNonIlluminate As OneNonIlluminate = Nothing


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
            Public Normal_Vol_Min_AC As Integer
            Public Normal_Vol_Max_AC As Integer
            Public Normal_Vol_Min_DC As Integer
            Public Normal_Vol_Max_DC As Integer
            Public Input_Phase As String = ""
            Public Opera_temp_min As Integer
            Public Opera_temp_max As Integer
            Public Stor_temp_min As Integer
            Public Stor_temp_max As Integer
            Public Area_Class As String = ""
            Public SupplyClass As String = ""
            Public Division As String = ""
            Public Gas_Group As String = ""
            Public Temp_Code As String = ""
            Public Weight As Double
        End Class

        Public Class OneEnclosure
            Public Color As String = ""
            Public NEMA_Type As New List(Of String)
            Public Material As String = ""
            Public MountType As String = ""
            Public MountID As New List(Of String)
            Public MountHeight As Double
            Public MountWidth As Double
            Public Window As String = ""
            Public IP As String = ""
        End Class

        Public Class OneServitPost
            Public Part_type As String = ""
            Public Range_Stranded As String = ""
            Public Range_Soild As String = ""
            Public Material As String = ""
        End Class

        Public Class OneBreatherDrain
            Public Part_size As String = ""
            Public NEMA_Type As New List(Of String)
            Public Material As String = ""
        End Class

        Public Class OneTempSwitch
            Public Monut As String = ""
            Public Rated_Vol_Max As Integer
            Public Rated_Current As Single
            Public SwitchFunction As String = ""
            Public Adjustable As String = ""
            Public Switch_Temp_ON As Integer
            Public Switch_Temp_OFF As Integer
            Public Opera_Temp_Min As Integer
            Public Opera_Temp_Max As Integer
            Public Weight As Double
            Public Area_Class As String = ""
            Public Part_Class As String = ""
            Public Gas_Group As String = ""
            Public Temp_Code As String = ""
        End Class

        Public Class OneTheromostat
            Public Monut As String = ""
            Public Rated_Vol_Max As Integer
            Public Rated_Current As Single
            Public TheroFunction As String = ""
            Public AdjustMin As Integer
            Public AdjustMax As Integer
            Public Opera_Temp_Min As Integer
            Public Opera_Temp_Max As Integer
            Public Weight As Double
            Public Area_Class As String = ""
            Public Part_Class As String = ""
            Public Gas_Group As String = ""
            Public Temp_Code As String = ""
        End Class

        Public Class OneWindowKit
            Public NEMA_Type As New List(Of String)
            Public Material As String = ""
            Public Kit_Type As String = ""
            Public Color As String = ""
            Public Weight As Double
            Public OveralHeight As Integer
            Public OveralWidth As Integer
            Public ViewHeight As Integer
            Public ViewWidth As Integer
            Public CutoutHeight As Integer
            Public CutoutWidth As Integer
        End Class

        Public Class OnePilotLight
            Public NEMA_TYPE As New List(Of String)
            Public Material As String = ""
            Public Fingersafe As String = ""
            Public Power_Module As String = ""
            Public Lamp_Test As String = ""
            Public Illumination As String = ""
            Public Voltage_Type As String = ""
            Public Voltage As String = ""
            Public Lens_Color As String = ""
            Public Contact_Blocks_Type As String = ""
            Public Contacts As String = ""
            Public Opera_temp_min As Integer
            Public Opera_temp_max As Integer
            Public Stor_temp_min As Integer
            Public Stor_temp_max As Integer
            Public Weight As Double
            Public Area_Class As String = ""
            Public [Class] As String = ""
            Public Gas_Groups As String = ""
            Public Temp_Code As String = ""
        End Class

        Public Class OneHeater
            Public Power As Integer
            Public Rated_Vol_MIN As Integer
            Public Rated_Vol_Max As Integer
            Public Frequency As New List(Of Integer)
            Public Weight As Double
            Public BuiltIn_Thermostat As String = ""
            Public BuiltIn_Fan As String = ""
            Public Area_Class As String = ""
            Public [Class] As String = ""
            Public Gas_Groups As String = ""
            Public Temp_Code As String = ""
            Public Datasheet As String = ""
        End Class

        Public Class OneNonIlluminate
            Public NEMA_TYPE As New List(Of String)
            Public Material As String = ""
            Public Fingersafe As String = ""
            Public OperatorType As String = ""
            Public Functions As String = ""
            Public MushroomHead As String = ""
            Public Color As String = ""
            Public Contact_Blocks_Type As String = ""
            Public Contacts As String = ""
            Public Opera_temp_min As Integer
            Public Opera_temp_max As Integer
            Public Stor_temp_min As Integer
            Public Stor_temp_max As Integer
            Public Area_Class As String = ""
            Public [Class] As String = ""
            Public Gas_Groups As String = ""
            Public Temp_Code As String = ""
        End Class

        Public Function HighLight() As String
            Dim HL As String = ""
            Select Case Me.PartType
                Case PART_TYPE.NONE
                    HL = ""
                Case PART_TYPE.TRANSFORMER
                    HL = ECSSParts.GetTypeName(Me.PartType)
                Case PART_TYPE.POWER_SUPPLY
                    HL = "Output Voltage: " & Me.aPowerSupply.Output_Power & "; Output Current: " & Me.aPowerSupply.Output_Current & "; Norminal Voltage:" &
                        Me.aPowerSupply.Normal_Vol_Max_AC & " - " & Me.aPowerSupply.Normal_Vol_Min_AC & "; Input Phase:" & Me.aPowerSupply.Input_Phase &
                        "; Operation Temp:" & Me.aPowerSupply.Opera_temp_max & " - " & Me.aPowerSupply.Opera_temp_min & " (°C) ;Area: " & Me.aPowerSupply.Area_Class
                Case PART_TYPE.ENCLOSURE
                    HL = "NEMA:" & Miscelllaneous.ListToString(Me.aEnclosure.NEMA_Type) & "; Heiht: " & Me.Height & "; Width: " & Me.Width &
                        ";Depth: " & Me.Depth & ";Mounting Plate Part No: " & Miscelllaneous.ListToString(Me.aEnclosure.MountID)
                Case PART_TYPE.SERVIT_POST
                    HL = ECSSParts.GetTypeName(Me.PartType)
                Case PART_TYPE.BREATHER_DRAIN
                    HL = ECSSParts.GetTypeName(Me.PartType)
                Case PART_TYPE.TEMP_SWITCH
                    HL = "Rated Voltage Max:" & Me.aTempSwitch.Rated_Vol_Max & ";Rated Current: " & Me.aTempSwitch.Rated_Current &
                        "; Function: " & Me.aTempSwitch.SwitchFunction & "; Switch On Temperature: " & Me.aTempSwitch.Switch_Temp_ON &
                        " (°C) ; Switch Off Temperature: " & Me.aTempSwitch.Switch_Temp_OFF & " (°C) ;Area: " & Me.aTempSwitch.Area_Class &
                        ";Class: " & Me.aTempSwitch.Part_Class
                Case PART_TYPE.THEROMOSTAT
                    HL = "Rated Voltage Max: " & Me.aTheromostat.Rated_Vol_Max & ";Rated Current: " & Me.aTheromostat.Rated_Current &
                        "; Function: " & Me.aTheromostat.TheroFunction & ";Adjustment Range Min: " & Me.aTheromostat.AdjustMin &
                        ";Adjustment Range Max: " & Me.aTheromostat.AdjustMax & ";Area: " & Me.aTheromostat.Area_Class
                Case PART_TYPE.WINDOW_KIT
                    HL = "NEMA:" & Miscelllaneous.ListToString(Me.aWindowKit.NEMA_Type) & ";Type: " & Me.aWindowKit.Kit_Type &
                        ";Color: " & Me.aWindowKit.Color & ";Height Overal: " & Me.aWindowKit.OveralHeight & ";Width Overal: " & Me.aWindowKit.OveralWidth &
                        "; Height View Area: " & Me.aWindowKit.ViewHeight & "; Width View Area:" & Me.aWindowKit.ViewWidth
                Case PART_TYPE.PILOT_LIGHT
                    HL = "NEMA: " & Miscelllaneous.ListToString(Me.aPilotLight.NEMA_TYPE) & ";Power Module Type: " & Me.aPilotLight.Power_Module &
                        ";Lamp Test Options: " & Me.aPilotLight.Lamp_Test & ";Illumination Options: " & Me.aPilotLight.Illumination &
                        ";Voltage Type: " & Me.aPilotLight.Voltage_Type & ";Voltage: " & Me.aPilotLight.Voltage & ";Lens Color: " & Me.aPilotLight.Lens_Color &
                        ";Contact Blocks Type: " & Me.aPilotLight.Contact_Blocks_Type & ";Contacts: " & Me.aPilotLight.Contacts &
                        ";OPERATION TEMP. MIN: " & Me.aPilotLight.Opera_temp_min & " (°C) ;OPERATION TEMP. MAX: " & Me.aPilotLight.Opera_temp_max & " (°C)"
                Case PART_TYPE.PUSH_BUTTON, PART_TYPE.SELECTOR_SWITCH, PART_TYPE.ESTOP
                    HL = "NEMA: " & Miscelllaneous.ListToString(Me.aNonIlluminate.NEMA_TYPE) & ";Type: " & Me.aNonIlluminate.OperatorType & ";Special Mushroom Head: " & Me.aNonIlluminate.MushroomHead &
                        "; Functions: " & Me.aNonIlluminate.Functions & "; Color: " & Me.aNonIlluminate.Color & "; Contact Blocks Type: " & Me.aNonIlluminate.Contact_Blocks_Type &
                        "; Contacts: " & Me.aNonIlluminate.Contacts & "; Operation Temp Min: " & Me.aNonIlluminate.Opera_temp_min & " (°C) ; Operation Temp Max: " & Me.aNonIlluminate.Opera_temp_max &
                        " (°C) ;Class: " & Me.aNonIlluminate.Class
                Case PART_TYPE.HEATER
                    HL = "Power: " & Me.aHeater.Power & " (W);Rated Voltage Min: " & Me.aHeater.Rated_Vol_MIN &
                        " (V);Rated Voltage Max: " & Me.aHeater.Rated_Vol_Max & " (V);Built-in Thermostat: " & Me.aHeater.BuiltIn_Thermostat &
                        ";Built-in Fan: " & Me.aHeater.BuiltIn_Fan & ";Area Class: " & Me.aHeater.Area_Class &
                        ";Class: " & Me.aHeater.Class & "; Gas Groups: " & Me.aHeater.Gas_Groups & ";Temp.Code: " & Me.aHeater.Temp_Code
                Case Else
                    Return ""
            End Select

            Return HL
            End Function

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
            Case PART_TYPE.TEMP_SWITCH
                Return "Temperature Switch"
            Case PART_TYPE.THEROMOSTAT
                Return "Theromostat"
            Case PART_TYPE.WINDOW_KIT
                Return "Window Kit"
            Case PART_TYPE.PILOT_LIGHT
                Return "Pilot Light"
            Case PART_TYPE.SELECTOR_SWITCH
                Return "Selector Switch"
            Case PART_TYPE.PUSH_BUTTON
                Return "Push Button"
            Case PART_TYPE.ESTOP
                Return "E-Stop"
            Case PART_TYPE.HEATER
                Return "Heater"
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
            Case "Window Kit"
                Return PART_TYPE.WINDOW_KIT
            Case "Theromostat"
                Return PART_TYPE.THEROMOSTAT
            Case "Temperature Switch"
                Return PART_TYPE.TEMP_SWITCH
            Case "Pilot Light"
                Return PART_TYPE.PILOT_LIGHT
            Case "Selector Switch"
                Return PART_TYPE.SELECTOR_SWITCH
            Case "Push Button"
                Return PART_TYPE.PUSH_BUTTON
            Case "E-Stop"
                Return PART_TYPE.ESTOP
            Case "Heater"
                Return PART_TYPE.HEATER
            Case Else
                Return PART_TYPE.NONE
        End Select
    End Function



    Public Function ReadPartsFromDB() As Boolean
        If Me.PartDic Is Nothing Then Me.PartDic = New Dictionary(Of String, OnePart)
        Try
            Me.PartDic.Clear()
            Dim Result As DataTable = ECSSDBFunctions.SelectEnclosure
            If Result IsNot Nothing OrElse Result.Rows.Count > 0 Then
                For Each oneRow As DataRow In Result.Rows
                    Dim apart = AddOneEnclosure(oneRow)
                    If apart IsNot Nothing AndAlso Me.PartDic.ContainsKey(apart.PartID) = False Then Me.PartDic.Add(apart.PartID, apart)
                Next
            End If


            If Authorization.IsDevelopement Then  ' developer only
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

                Result = ECSSDBFunctions.SelectWindowKit
                If Result IsNot Nothing OrElse Result.Rows.Count > 0 Then
                    For Each oneRow As DataRow In Result.Rows
                        Dim apart = AddOneWindowKit(oneRow)
                        If apart IsNot Nothing AndAlso Me.PartDic.ContainsKey(apart.PartID) = False Then Me.PartDic.Add(apart.PartID, apart)
                    Next
                End If
            End If

            If Authorization.IsAuthorized Then
                Result = ECSSDBFunctions.SelectTransformer
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

                Result = ECSSDBFunctions.SelectTempSwitch
                If Result IsNot Nothing OrElse Result.Rows.Count > 0 Then
                    For Each oneRow As DataRow In Result.Rows
                        Dim apart = AddOneTempSwitch(oneRow)
                        If apart IsNot Nothing AndAlso Me.PartDic.ContainsKey(apart.PartID) = False Then Me.PartDic.Add(apart.PartID, apart)
                    Next
                End If

                Result = ECSSDBFunctions.SelectTheromostat
                If Result IsNot Nothing OrElse Result.Rows.Count > 0 Then
                    For Each oneRow As DataRow In Result.Rows
                        Dim apart = AddOneTheromostat(oneRow)
                        If apart IsNot Nothing AndAlso Me.PartDic.ContainsKey(apart.PartID) = False Then Me.PartDic.Add(apart.PartID, apart)
                    Next
                End If

                Result = ECSSDBFunctions.SelectPilotLight
                If Result IsNot Nothing OrElse Result.Rows.Count > 0 Then
                    For Each oneRow As DataRow In Result.Rows
                        Dim apart = AddOnePilotLight(oneRow)
                        If apart IsNot Nothing AndAlso Me.PartDic.ContainsKey(apart.PartID) = False Then Me.PartDic.Add(apart.PartID, apart)
                    Next
                End If

                Result = ECSSDBFunctions.SelectHeater
                If Result IsNot Nothing OrElse Result.Rows.Count > 0 Then
                    For Each oneRow As DataRow In Result.Rows
                        Dim apart = AddOneHeater(oneRow)
                        If apart IsNot Nothing AndAlso Me.PartDic.ContainsKey(apart.PartID) = False Then Me.PartDic.Add(apart.PartID, apart)
                    Next
                End If

                Result = ECSSDBFunctions.SelectNonIlluminate
                If Result IsNot Nothing OrElse Result.Rows.Count > 0 Then
                    For Each oneRow As DataRow In Result.Rows
                        Dim apart = AddOneNonIlluminate(oneRow)
                        If apart IsNot Nothing AndAlso Me.PartDic.ContainsKey(apart.PartID) = False Then Me.PartDic.Add(apart.PartID, apart)
                    Next
                End If
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
                If kvp.Value.Certificates IsNot Nothing AndAlso kvp.Value.Certificates.Count > 0 Then
                    For Each cert In kvp.Value.Certificates
                        If Me.Keywords.Contains(cert) = False Then Me.Keywords.Add(cert)
                    Next
                End If
                If String.IsNullOrEmpty(kvp.Value.PartID) = False Then
                    If kvp.Value.PartID.Contains("|") Then
                        Dim tList = kvp.Value.PartID.Split("|")
                        For Each s As String In tList
                            If Me.Keywords.Contains(s.Trim) = False Then Me.Keywords.Add(s.Trim)
                        Next
                    Else
                        If Me.Keywords.Contains(kvp.Value.PartID.Trim) = False Then Me.Keywords.Add(kvp.Value.PartID.Trim)
                    End If
                End If
                If String.IsNullOrEmpty(kvp.Value.Manufacturer) = False Then
                    If Me.Keywords.Contains(kvp.Value.Manufacturer.Trim) = False Then Me.Keywords.Add(kvp.Value.Manufacturer.Trim)
                End If
            Next
        End If
        Me.Keywords.Sort()
        Return True
    End Function

    Public Function AddOneTransformer(ByVal oneRow As DataRow) As OnePart
        Dim aPart As New OnePart
        Try
            aPart.PartType = PART_TYPE.TRANSFORMER
            aPart.aTransformer = New OnePart.OneTransformer
            aPart.PartID = oneRow("PartID").ToString
            aPart.Manufacturer = oneRow("Manufacturer").ToString
            If String.IsNullOrEmpty(oneRow("Certificates1")) = False Then aPart.Certificates.Add(oneRow("Certificates1").ToString)
            If String.IsNullOrEmpty(oneRow("Certificates2")) = False Then aPart.Certificates.Add(oneRow("Certificates2").ToString)
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
            If oneRow("Certificates") IsNot Nothing Then
                aPart.Certificates.AddRange(oneRow("Certificates").ToString.Split("|"))
            End If
            aPart.Height = CDbl(oneRow("Height"))
            aPart.Width = CDbl(oneRow("Width"))
            aPart.Depth = CDbl(oneRow("Depth"))
            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.link = oneRow("Link").ToString

            aPart.aPowerSupply.Output_Power = CInt(oneRow("Output_Power"))
            aPart.aPowerSupply.Output_Current = CSng(oneRow("Output_Current"))
            aPart.aPowerSupply.Output_Vol = CInt(oneRow("Output_Vol"))
            aPart.aPowerSupply.Normal_Vol_Min_AC = CInt(oneRow("Normal_Vol_Min_AC"))
            aPart.aPowerSupply.Normal_Vol_Max_AC = CInt(oneRow("Normal_Vol_Max_AC"))
            aPart.aPowerSupply.Normal_Vol_Min_DC = CInt(oneRow("Normal_Vol_Min_DC"))
            aPart.aPowerSupply.Normal_Vol_Max_DC = CInt(oneRow("Normal_Vol_Max_DC"))
            aPart.aPowerSupply.Opera_temp_min = CInt(oneRow("Opera_temp_min"))
            aPart.aPowerSupply.Opera_temp_max = CInt(oneRow("Opera_temp_max"))
            aPart.aPowerSupply.Stor_temp_min = CInt(oneRow("Stor_temp_min"))
            aPart.aPowerSupply.Stor_temp_max = CInt(oneRow("Stor_temp_max"))
            aPart.aPowerSupply.Input_Phase = oneRow("Input_Phase").ToString
            aPart.aPowerSupply.Area_Class = oneRow("Area_Class").ToString
            aPart.aPowerSupply.SupplyClass = oneRow("Class").ToString
            aPart.aPowerSupply.Division = oneRow("Division").ToString
            aPart.aPowerSupply.Gas_Group = oneRow("Gas_Group").ToString
            aPart.aPowerSupply.Temp_Code = oneRow("Temp_Code").ToString
            aPart.aPowerSupply.Weight = CDbl(oneRow("Weight"))

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
            If oneRow("Certificates") IsNot Nothing Then
                aPart.Certificates.AddRange(oneRow("Certificates").ToString.Split("|"))
            End If
            aPart.Height = CDbl(oneRow("Height"))
            aPart.Width = CDbl(oneRow("Width"))
            aPart.Depth = CDbl(oneRow("Depth"))
            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.link = oneRow("Link").ToString
            aPart.PDF = oneRow("PDF").ToString

            aPart.aEnclosure.Color = oneRow("Color").ToString
            If oneRow("NEMA_Type") IsNot Nothing Then
                aPart.aEnclosure.NEMA_Type.AddRange(oneRow("NEMA_Type").ToString.Split("|"))
            End If
            aPart.aEnclosure.Material = oneRow("Material").ToString
            aPart.aEnclosure.MountType = oneRow("Mount").ToString
            If oneRow("MountID") IsNot Nothing Then
                aPart.aEnclosure.MountID.AddRange(oneRow("MountID").ToString.Split("|"))
            End If
            aPart.aEnclosure.MountHeight = CDbl(oneRow("MountHeight"))
            aPart.aEnclosure.MountWidth = CDbl(oneRow("MountWidth"))
            aPart.aEnclosure.Window = oneRow("Window").ToString
            aPart.aEnclosure.ip = oneRow("IP").ToString
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
            If String.IsNullOrEmpty(oneRow("Certificates1")) = False Then aPart.Certificates.Add(oneRow("Certificates1").ToString)
            If String.IsNullOrEmpty(oneRow("Certificates2")) = False Then aPart.Certificates.Add(oneRow("Certificates2").ToString)

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
            If String.IsNullOrEmpty(oneRow("Certificates1")) = False Then aPart.Certificates.Add(oneRow("Certificates1").ToString)
            If String.IsNullOrEmpty(oneRow("Certificates2")) = False Then aPart.Certificates.Add(oneRow("Certificates2").ToString)

            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.link = oneRow("Link").ToString

            aPart.aBreatherDrain.Part_size = oneRow("Part_size").ToString
            If oneRow("NEMA_Type") IsNot Nothing Then
                aPart.aBreatherDrain.NEMA_Type.AddRange(oneRow("NEMA_Type").ToString.Split("|"))
            End If
            aPart.aBreatherDrain.Material = oneRow("Material").ToString

        Catch ex As Exception
            MessageBox.Show("Error: Add Servit Post.")
        End Try
        Return aPart
    End Function

    Public Function AddOneWindowKit(ByVal oneRow As DataRow) As OnePart
        Dim aPart As New OnePart
        Try
            aPart.PartType = PART_TYPE.WINDOW_KIT
            aPart.aWindowKit = New OnePart.OneWindowKit
            aPart.PartID = oneRow("PartID").ToString
            aPart.Manufacturer = oneRow("Manufacturer").ToString
            If oneRow("Certificates") IsNot Nothing Then
                aPart.Certificates.AddRange(oneRow("Certificates").ToString.Split("|"))
            End If

            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.link = oneRow("Link").ToString


            aPart.aWindowKit.Color = oneRow("Color").ToString
            If oneRow("NEMA_Type") IsNot Nothing Then
                aPart.aWindowKit.NEMA_Type.AddRange(oneRow("NEMA_Type").ToString.Split("|"))
            End If
            aPart.aWindowKit.Material = oneRow("Material").ToString
            aPart.aWindowKit.Kit_Type = oneRow("Kit_Type").ToString
            aPart.aWindowKit.OveralHeight = CInt(oneRow("OveralHeight"))
            aPart.aWindowKit.OveralWidth = CInt(oneRow("OveralWidth"))
            aPart.aWindowKit.ViewHeight = CInt(oneRow("ViewHeight"))
            aPart.aWindowKit.ViewWidth = CInt(oneRow("ViewWidth"))
            aPart.aWindowKit.CutoutHeight = CInt(oneRow("CutoutHeight"))
            aPart.aWindowKit.CutoutWidth = CInt(oneRow("CutoutWidth"))
            aPart.aWindowKit.Weight = CDbl(oneRow("Weight"))
        Catch ex As Exception
            MessageBox.Show("Error: Add Enclosure.")
        End Try
        Return aPart
    End Function

    Public Function AddOneTheromostat(ByVal oneRow As DataRow) As OnePart
        Dim aPart As New OnePart
        Try
            aPart.PartType = PART_TYPE.THEROMOSTAT
            aPart.aTheromostat = New OnePart.OneTheromostat
            aPart.PartID = oneRow("PartID").ToString
            aPart.Manufacturer = oneRow("Manufacturer").ToString
            If oneRow("Certificates") IsNot Nothing Then
                aPart.Certificates.AddRange(oneRow("Certificates").ToString.Split("|"))
            End If
            aPart.Height = CDbl(oneRow("Height"))
            aPart.Width = CDbl(oneRow("Width"))
            aPart.Depth = CDbl(oneRow("Depth"))
            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.link = oneRow("Link").ToString

            aPart.aTheromostat.Monut = oneRow("Monut").ToString
            aPart.aTheromostat.Rated_Vol_Max = CInt(oneRow("Rated_Vol_Max"))
            aPart.aTheromostat.Rated_Current = CSng(oneRow("Rated_Current"))
            aPart.aTheromostat.TheroFunction = oneRow("Function").ToString
            aPart.aTheromostat.AdjustMin = CInt(oneRow("AdjustMin"))
            aPart.aTheromostat.AdjustMax = CInt(oneRow("AdjustMax"))
            aPart.aTheromostat.Opera_Temp_Min = CInt(oneRow("Opera_Temp_Min"))
            aPart.aTheromostat.Opera_Temp_Max = CInt(oneRow("Opera_Temp_Max"))
            aPart.aTheromostat.Weight = CDbl(oneRow("Weight"))
            aPart.aTheromostat.Area_Class = oneRow("Area_Class").ToString
            aPart.aTheromostat.Part_Class = oneRow("Class").ToString
            aPart.aTheromostat.Gas_Group = oneRow("Gas_Group").ToString
            aPart.aTheromostat.Temp_Code = oneRow("Temp_Code").ToString

        Catch ex As Exception
            MessageBox.Show("Error: Add aTheromostat.")
        End Try
        Return aPart
    End Function

    Public Function AddOneTempSwitch(ByVal oneRow As DataRow) As OnePart
        Dim aPart As New OnePart
        Try
            aPart.PartType = PART_TYPE.TEMP_SWITCH
            aPart.aTempSwitch = New OnePart.OneTempSwitch
            aPart.PartID = oneRow("PartID").ToString
            aPart.Manufacturer = oneRow("Manufacturer").ToString
            If oneRow("Certificates") IsNot Nothing Then
                aPart.Certificates.AddRange(oneRow("Certificates").ToString.Split("|"))
            End If
            aPart.Height = CDbl(oneRow("Height"))
            aPart.Width = CDbl(oneRow("Width"))
            aPart.Depth = CDbl(oneRow("Depth"))
            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.link = oneRow("Link").ToString


            aPart.aTempSwitch.Monut = oneRow("Monut").ToString
            aPart.aTempSwitch.Rated_Vol_Max = CInt(oneRow("Rated_Vol_Max"))
            aPart.aTempSwitch.Rated_Current = CSng(oneRow("Rated_Current"))
            aPart.aTempSwitch.SwitchFunction = oneRow("Function").ToString
            aPart.aTempSwitch.Switch_Temp_ON = CInt(oneRow("Switch_Temp_ON"))
            aPart.aTempSwitch.Switch_Temp_OFF = CInt(oneRow("Switch_Temp_OFF"))
            aPart.aTempSwitch.Opera_Temp_Min = CInt(oneRow("Opera_Temp_Min"))
            aPart.aTempSwitch.Opera_Temp_Max = CInt(oneRow("Opera_Temp_Max"))
            aPart.aTempSwitch.Weight = CDbl(oneRow("Weight"))
            aPart.aTempSwitch.Adjustable = oneRow("Adjustable").ToString
            aPart.aTempSwitch.Area_Class = oneRow("Area_Class").ToString
            aPart.aTempSwitch.Part_Class = oneRow("Class").ToString
            aPart.aTempSwitch.Gas_Group = oneRow("Gas_Group").ToString
            aPart.aTempSwitch.Temp_Code = oneRow("Temp_Code").ToString

        Catch ex As Exception
            MessageBox.Show("Error: Add aTheromostat.")
        End Try
        Return aPart
    End Function

    Public Function AddOnePilotLight(ByVal oneRow As DataRow) As OnePart
        Dim aPart As New OnePart
        Try
            aPart.PartType = PART_TYPE.PILOT_LIGHT
            aPart.aPilotLight = New OnePart.OnePilotLight
            aPart.PartID = oneRow("PartID").ToString
            aPart.Manufacturer = oneRow("Manufacturer").ToString
            If oneRow("Certificates") IsNot Nothing Then
                aPart.Certificates.AddRange(oneRow("Certificates").ToString.Split("|"))
            End If
            aPart.Height = CDbl(oneRow("Height"))
            aPart.Width = CDbl(oneRow("Width"))
            aPart.Depth = CDbl(oneRow("Depth"))
            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.link = oneRow("Link").ToString

            If oneRow("NEMA_Type") IsNot Nothing Then
                aPart.aPilotLight.NEMA_TYPE.AddRange(oneRow("NEMA_Type").ToString.Split("|"))
            End If
            aPart.aPilotLight.Material = oneRow("Material").ToString
            aPart.aPilotLight.Fingersafe = oneRow("Fingersafe").ToString
            aPart.aPilotLight.Power_Module = oneRow("Power_Module").ToString
            aPart.aPilotLight.Lamp_Test = oneRow("Lamp_Test").ToString
            aPart.aPilotLight.Illumination = oneRow("Illumination").ToString
            aPart.aPilotLight.Voltage_Type = oneRow("Voltage_Type").ToString
            aPart.aPilotLight.Voltage = oneRow("Voltage").ToString
            aPart.aPilotLight.Lens_Color = oneRow("Lens_Color").ToString
            aPart.aPilotLight.Contact_Blocks_Type = oneRow("Contact_Blocks_Type").ToString
            aPart.aPilotLight.Contacts = oneRow("Contacts").ToString
            aPart.aPilotLight.Stor_temp_min = CInt(oneRow("Stor_temp_min"))
            aPart.aPilotLight.Stor_temp_max = CInt(oneRow("Stor_temp_max"))
            aPart.aPilotLight.Opera_temp_min = CInt(oneRow("Opera_Temp_Min"))
            aPart.aPilotLight.Opera_temp_max = CInt(oneRow("Opera_Temp_Max"))
            aPart.aPilotLight.Weight = CDbl(oneRow("Weight"))
            aPart.aPilotLight.Area_Class = oneRow("Area_Class").ToString
            aPart.aPilotLight.Class = oneRow("Class").ToString
            aPart.aPilotLight.Gas_Groups = oneRow("Gas_Groups").ToString
            aPart.aPilotLight.Temp_Code = oneRow("Temp_Code").ToString

        Catch ex As Exception
            MessageBox.Show("Error: Add OnePilotLight.")
        End Try
        Return aPart
    End Function

    Public Function AddOneHeater(ByVal oneRow As DataRow) As OnePart
        Dim aPart As New OnePart
        Try
            aPart.PartType = PART_TYPE.HEATER
            aPart.aHeater = New OnePart.OneHeater
            aPart.PartID = oneRow("PartID").ToString
            aPart.Manufacturer = oneRow("Manufacturer").ToString
            If oneRow("Certificates") IsNot Nothing Then
                aPart.Certificates.AddRange(oneRow("Certificates").ToString.Split("|"))
            End If
            aPart.Height = CDbl(oneRow("Height"))
            aPart.Width = CDbl(oneRow("Width"))
            aPart.Depth = CDbl(oneRow("Depth"))
            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.link = oneRow("Link").ToString

            aPart.aHeater.Power = CInt(oneRow("Power"))
            aPart.aHeater.Rated_Vol_Max = CInt(oneRow("Rated_Vol_Max"))
            aPart.aHeater.Rated_Vol_MIN = CInt(oneRow("Rated_Vol_MIN"))
            If oneRow("Frequency") IsNot Nothing Then
                aPart.aHeater.Frequency.AddRange(oneRow("Frequency").ToString.Split("|").ToList.ConvertAll(Function(str) Int32.Parse(str)))
            End If
            aPart.aHeater.Weight = CInt(oneRow("Weight"))
            aPart.aHeater.BuiltIn_Thermostat = oneRow("BuiltIn_Thermostat").ToString
            aPart.aHeater.BuiltIn_Fan = oneRow("BuiltIn_Fan").ToString
            aPart.aHeater.Area_Class = oneRow("Area_Class").ToString
            aPart.aHeater.Class = oneRow("Class").ToString
            aPart.aHeater.Gas_Groups = oneRow("Gas_Groups").ToString
            aPart.aHeater.Temp_Code = oneRow("Temp_Code").ToString
            aPart.aHeater.Datasheet = oneRow("Datasheet").ToString
        Catch ex As Exception
            MessageBox.Show("Error: Add OneHeater.")
        End Try
        Return aPart
    End Function

    Public Function AddOneNonIlluminate(ByVal oneRow As DataRow) As OnePart
        Dim aPart As New OnePart
        Try
            aPart.aNonIlluminate = New OnePart.OneNonIlluminate
            aPart.PartID = oneRow("PartID").ToString
            aPart.PartType = DirectCast(CInt(oneRow("PartType")), ECSSParts.PART_TYPE)
            aPart.Manufacturer = oneRow("Manufacturer").ToString
            If oneRow("Certificates") IsNot Nothing Then
                aPart.Certificates.AddRange(oneRow("Certificates").ToString.Split("|"))
            End If
            'aPart.Height = CDbl(oneRow("Height"))
            'aPart.Width = CDbl(oneRow("Width"))
            'aPart.Depth = CDbl(oneRow("Depth"))
            aPart.Keywords = New List(Of String)
            aPart.Keywords.AddRange(Me.GenKeyWords(oneRow("Keywords").ToString))
            aPart.Description = oneRow("Description").ToString
            aPart.link = oneRow("Link").ToString

            If oneRow("NEMA_Type") IsNot Nothing Then
                aPart.aNonIlluminate.NEMA_TYPE.AddRange(oneRow("NEMA_Type").ToString.Split("|"))
            End If
            aPart.aNonIlluminate.Material = oneRow("Material").ToString
            aPart.aNonIlluminate.Fingersafe = oneRow("Fingersafe").ToString

            aPart.aNonIlluminate.OperatorType = oneRow("OperatorType").ToString
            aPart.aNonIlluminate.Functions = oneRow("Functions").ToString
            aPart.aNonIlluminate.MushroomHead = oneRow("MushroomHead").ToString
            aPart.aNonIlluminate.Color = oneRow("Color").ToString.Trim
            aPart.aNonIlluminate.Contact_Blocks_Type = oneRow("Contact_Blocks_Type").ToString.Trim
            aPart.aNonIlluminate.Contacts = oneRow("Contacts").ToString.Trim
            aPart.aNonIlluminate.Opera_temp_min = CInt(oneRow("Opera_temp_min"))
            aPart.aNonIlluminate.Opera_temp_max = CInt(oneRow("Opera_temp_max"))
            aPart.aNonIlluminate.Stor_temp_min = CInt(oneRow("Stor_temp_min"))
            aPart.aNonIlluminate.Stor_temp_max = CInt(oneRow("Stor_temp_max"))
            aPart.aNonIlluminate.Area_Class = oneRow("Area_Class").ToString
            aPart.aNonIlluminate.[Class] = oneRow("Class").ToString
            aPart.aNonIlluminate.Gas_Groups = oneRow("Gas_Groups").ToString
            aPart.aNonIlluminate.Temp_Code = oneRow("Temp_Code").ToString
        Catch ex As Exception
            MessageBox.Show("Error: Add OneNonIlluminate.")
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
        If searchCondition.IsEmpty Then Return False
        Dim keywordMatch As Boolean = False
        If searchCondition.keyword IsNot Nothing AndAlso searchCondition.keyword.Count > 0 Then
            For Each k As String In searchCondition.keyword
                keywordMatch = PartMatchSearchWords(aPart, k.Trim)
                If keywordMatch = False Then Exit For
            Next
            If keywordMatch = False Then keywordMatch = PartMatchFilterMaterial(aPart, searchCondition.keyword)
            If keywordMatch = False Then Return False
            End If

        If Not PartMatchFilterPartType(aPart, searchCondition.PartType) Then Return False
        If Not PartMatchFilterManufacturer(aPart, searchCondition.Manufacturer) Then Return False
        If Not PartMatchFilterMaterial(aPart, searchCondition.Material) Then Return False
        If Not PartMatchFilterCertificate(aPart, searchCondition.Certificates) Then Return False
        If Not PartMatchFilterDimension(aPart, searchCondition) Then Return False

        If Not PartMatchFilterMount(aPart, searchCondition.Mount) Then Return False
        If Not PartMatchFilterNEMA(aPart, searchCondition.NEMA) Then Return False

        If Not PartMatchFilterOTV(aPart, searchCondition.outputV) Then Return False
        If Not PartMatchFilterOTA(aPart, searchCondition.outputA) Then Return False
        If Not PartMatchFilterNorV(aPart, searchCondition) Then Return False
        If Not PartMatchFilterInputP(aPart, searchCondition.InputPhase) Then Return False
        If Not PartMatchFilterClass(aPart, searchCondition.Class) Then Return False
        If Not PartMatchFilterGroup(aPart, searchCondition.Group) Then Return False

        If Not PartMatchFilterWindowType(aPart, searchCondition.WindowType) Then Return False
        If Not PartMatchFilterColor(aPart, searchCondition.Color) Then Return False
        If Not PartMatchFilterViewAreaH(aPart, searchCondition.ViewAreaH) Then Return False
        If Not PartMatchFilterViewAreaW(aPart, searchCondition.ViewAreaW) Then Return False

        If Not PartMatchFilterRatedVol(aPart, searchCondition.RatedVol) Then Return False
        If Not PartMatchFilterRatedCurrent(aPart, searchCondition.RatedCurrent) Then Return False
        If Not PartMatchFilterFunctions(aPart, searchCondition.Functions) Then Return False
        If Not PartMatchFilterArea(aPart, searchCondition.Area) Then Return False
        If Not PartMatchFilterTempSwitchON(aPart, searchCondition.TempSwitchON) Then Return False
        If Not PartMatchFilterTempSwitchOFF(aPart, searchCondition.TempSwitchOFF) Then Return False

        If Not PartMatchFilterRatedVol(aPart, searchCondition) Then Return False
        If Not PartMatchFilterFeatureTypes(aPart, searchCondition.Types) Then Return False
        If Not PartMatchFilterBlockType(aPart, searchCondition.BlockType) Then Return False
        If Not PartMatchFilterContacts(aPart, searchCondition.Contacts) Then Return False

        If Not PartMatchFilterOperaTemp(aPart, searchCondition) Then Return False
        If Not PartMatchFilterPower(aPart, searchCondition.Power) Then Return False
        If Not PartMatchFilterBuiltInThem(aPart, searchCondition.BuiltInThem) Then Return False
        If Not PartMatchFilterBuiltInFan(aPart, searchCondition.BuiltInFan) Then Return False
        If Not PartMatchFilterBuiltInArea(aPart, searchCondition.Area) Then Return False
        If Not PartMatchFilterBuiltInTempCode(aPart, searchCondition.TempCode) Then Return False
        If Not PartMatchFilterFingersafe(aPart, searchCondition.Fingersafe) Then Return False
        If Not PartMatchFilterPowerModuleType(aPart, searchCondition.PowerModuleType) Then Return False
        If Not PartMatchFilterLampTest(aPart, searchCondition.LampTest) Then Return False
        If Not PartMatchFilterIlluminationOption(aPart, searchCondition.IlluminationOption) Then Return False
        If Not PartMatchFilterVoltageType(aPart, searchCondition.VoltageType) Then Return False
        If Not PartMatchFilterVoltage(aPart, searchCondition.Voltage) Then Return False
        If Not PartMatchFilterLenColor(aPart, searchCondition.LensColor) Then Return False
        If Not PartMatchFilterVoltageType(aPart, searchCondition.VoltageType) Then Return False

        Return True
    End Function

    Public Function PartMatchSearchWords(ByVal aPart As OnePart, ByVal keyword As String) As Boolean
        If String.IsNullOrEmpty(keyword) Then Return True
        If aPart Is Nothing Then Return False
        Dim wordMatches As Boolean = False
        If aPart.PartID.ToUpper.Contains(keyword.ToUpper) Then wordMatches = True
        If aPart.Manufacturer.ToUpper.Contains(keyword.ToUpper) Then wordMatches = True
        If aPart.Certificates IsNot Nothing AndAlso aPart.Certificates.Count > 0 Then
            For Each cert In aPart.Certificates
                If cert.ToUpper.Contains(keyword.ToUpper) Then wordMatches = True
            Next
        End If
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
        If aPart.aEnclosure IsNot Nothing AndAlso MList.Contains(aPart.aEnclosure.Material) Then filterMatches = True
        If aPart.aWindowKit IsNot Nothing AndAlso MList.Contains(aPart.aWindowKit.Material) Then filterMatches = True
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Material) Then filterMatches = True
        If aPart.aNonIlluminate IsNot Nothing AndAlso MList.Contains(aPart.aNonIlluminate.Material) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterCertificate(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        For Each Str As String In aPart.Certificates
            If MList.Contains(Str) Then filterMatches = True : Exit For
        Next
        Return filterMatches
    End Function

    Public Function PartMatchFilterDimension(ByVal aPart As OnePart, ByVal searchCondition As ECSSSearchCriteria) As Boolean
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = True
        If searchCondition.HeightMin > 0 OrElse searchCondition.HeightMax > 0 Then
            If aPart.Height > 0 AndAlso (aPart.Height > searchCondition.HeightMax OrElse aPart.Height < searchCondition.HeightMin) Then filterMatches = False : Return filterMatches
        End If
        If searchCondition.WidthMax > 0 OrElse searchCondition.WidthMin > 0 Then
            If aPart.Width > 0 AndAlso (aPart.Width > searchCondition.WidthMax OrElse aPart.Width < searchCondition.WidthMin) Then filterMatches = False : Return filterMatches
        End If
        If searchCondition.DepthMax > 0 OrElse searchCondition.DepthMin > 0 Then
            If aPart.Depth > 0 AndAlso (aPart.Depth > searchCondition.DepthMax OrElse aPart.Depth < searchCondition.DepthMin) Then filterMatches = False : Return filterMatches
        End If
        Return filterMatches
    End Function

    Public Function PartMatchFilterMount(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aEnclosure IsNot Nothing AndAlso MList.Contains(aPart.aEnclosure.MountType) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterNEMA(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aEnclosure IsNot Nothing Then
            For Each Str As String In aPart.aEnclosure.NEMA_Type
                If MList.Contains(Str) Then filterMatches = True : Exit For
            Next
        End If
        If aPart.aBreatherDrain IsNot Nothing Then
            For Each Str As String In aPart.aBreatherDrain.NEMA_Type
                If MList.Contains(Str) Then filterMatches = True : Exit For
            Next
        End If
        If aPart.aWindowKit IsNot Nothing Then
            For Each Str As String In aPart.aWindowKit.NEMA_Type
                If MList.Contains(Str) Then filterMatches = True : Exit For
            Next
        End If
        If aPart.aPilotLight IsNot Nothing Then
            For Each Str As String In aPart.aPilotLight.NEMA_TYPE
                If MList.Contains(Str) Then filterMatches = True : Exit For
            Next
        End If
        If aPart.aNonIlluminate IsNot Nothing Then
            For Each Str As String In aPart.aNonIlluminate.NEMA_TYPE
                If MList.Contains(Str) Then filterMatches = True : Exit For
            Next
        End If
        Return filterMatches
    End Function

    Public Function PartMatchFilterOTV(ByVal aPart As OnePart, ByVal MList As List(Of Integer)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPowerSupply IsNot Nothing AndAlso MList.Contains(aPart.aPowerSupply.Output_Vol) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterOTA(ByVal aPart As OnePart, ByVal MList As List(Of Single)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPowerSupply IsNot Nothing AndAlso MList.Contains(CSng(aPart.aPowerSupply.Output_Current)) Then filterMatches = True
        Return filterMatches
    End Function
    Public Function PartMatchFilterNorV(ByVal aPart As OnePart, ByVal searchCondition As ECSSSearchCriteria) As Boolean
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If searchCondition.NormalVMax = 0 AndAlso searchCondition.NormalVMin = 0 Then Return True

        If aPart.aPowerSupply IsNot Nothing Then
            If searchCondition.NormalVMin = 0 AndAlso aPart.aPowerSupply.Normal_Vol_Max_AC >= searchCondition.NormalVMax Then Return True
            If searchCondition.NormalVMax = 0 AndAlso aPart.aPowerSupply.Normal_Vol_Min_AC <= searchCondition.NormalVMin Then Return True
            If aPart.aPowerSupply.Normal_Vol_Min_AC <= searchCondition.NormalVMin AndAlso aPart.aPowerSupply.Normal_Vol_Max_AC >= searchCondition.NormalVMax Then Return True
        End If

        Return filterMatches
    End Function

    Public Function PartMatchFilterInputP(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPowerSupply IsNot Nothing AndAlso MList.Contains(aPart.aPowerSupply.Input_Phase) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterClass(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPowerSupply IsNot Nothing AndAlso MList.Contains(aPart.aPowerSupply.SupplyClass) Then filterMatches = True
        If aPart.aHeater IsNot Nothing AndAlso MList.Contains(aPart.aHeater.Class) Then filterMatches = True
        If aPart.aNonIlluminate IsNot Nothing AndAlso MList.Contains(aPart.aNonIlluminate.Class) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterGroup(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPowerSupply IsNot Nothing AndAlso MList.Contains(aPart.aPowerSupply.Gas_Group) Then filterMatches = True
        If aPart.aHeater IsNot Nothing AndAlso MList.Contains(aPart.aPowerSupply.Gas_Group) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterWindowType(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aWindowKit IsNot Nothing AndAlso MList.Contains(aPart.aWindowKit.Kit_Type) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterColor(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aWindowKit IsNot Nothing AndAlso MList.Contains(aPart.aWindowKit.Color) Then filterMatches = True
        If aPart.aNonIlluminate IsNot Nothing AndAlso MList.Contains(aPart.aNonIlluminate.Color) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterViewAreaH(ByVal aPart As OnePart, ByVal MList As List(Of Integer)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aWindowKit IsNot Nothing AndAlso MList.Contains(aPart.aWindowKit.ViewHeight) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterViewAreaW(ByVal aPart As OnePart, ByVal MList As List(Of Integer)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aWindowKit IsNot Nothing AndAlso MList.Contains(aPart.aWindowKit.ViewWidth) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterRatedVol(ByVal aPart As OnePart, ByVal MList As List(Of Integer)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aTheromostat IsNot Nothing AndAlso MList.Contains(aPart.aTheromostat.Rated_Vol_Max) Then filterMatches = True
        If aPart.aTempSwitch IsNot Nothing AndAlso MList.Contains(aPart.aTempSwitch.Rated_Vol_Max) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterRatedCurrent(ByVal aPart As OnePart, ByVal MList As List(Of Single)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aTheromostat IsNot Nothing AndAlso MList.Contains(aPart.aTheromostat.Rated_Current) Then filterMatches = True
        If aPart.aTempSwitch IsNot Nothing AndAlso MList.Contains(aPart.aTempSwitch.Rated_Current) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterFunctions(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aTheromostat IsNot Nothing AndAlso MList.Contains(aPart.aTheromostat.TheroFunction) Then filterMatches = True
        If aPart.aTempSwitch IsNot Nothing AndAlso MList.Contains(aPart.aTempSwitch.SwitchFunction) Then filterMatches = True
        If aPart.aNonIlluminate IsNot Nothing Then
            If aPart.PartType = PART_TYPE.PUSH_BUTTON Then
                If MList.Contains(aPart.aNonIlluminate.MushroomHead) Then filterMatches = True
            Else
                If MList.Contains(aPart.aNonIlluminate.Functions) Then filterMatches = True
            End If
        End If
        Return filterMatches
    End Function

    Public Function PartMatchFilterArea(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aTheromostat IsNot Nothing AndAlso MList.Contains(aPart.aTheromostat.Area_Class) Then filterMatches = True
        If aPart.aTempSwitch IsNot Nothing AndAlso MList.Contains(aPart.aTempSwitch.Area_Class) Then filterMatches = True
        If aPart.aNonIlluminate IsNot Nothing AndAlso MList.Contains(aPart.aNonIlluminate.Area_Class) Then filterMatches = True
        If aPart.aHeater IsNot Nothing AndAlso MList.Contains(aPart.aHeater.Area_Class) Then filterMatches = True
        If aPart.aPowerSupply IsNot Nothing AndAlso MList.Contains(aPart.aPowerSupply.Area_Class) Then filterMatches = True
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Area_Class) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterTempSwitchON(ByVal aPart As OnePart, ByVal MList As List(Of Integer)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aTempSwitch IsNot Nothing AndAlso MList.Contains(aPart.aTempSwitch.Switch_Temp_ON) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterTempSwitchOFF(ByVal aPart As OnePart, ByVal MList As List(Of Integer)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aTempSwitch IsNot Nothing AndAlso MList.Contains(aPart.aTempSwitch.Switch_Temp_OFF) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterRatedVol(ByVal aPart As OnePart, ByVal searchCondition As ECSSSearchCriteria) As Boolean
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If searchCondition.RatedVolMax = 0 AndAlso searchCondition.RatedVolMin = 0 Then Return True

        If aPart.aHeater IsNot Nothing Then
            If searchCondition.RatedVolMin = 0 AndAlso aPart.aHeater.Rated_Vol_Max <= searchCondition.RatedVolMax Then Return True
            If searchCondition.RatedVolMax = 0 AndAlso aPart.aHeater.Rated_Vol_MIN >= searchCondition.RatedVolMin Then Return True
            If aPart.aHeater.Rated_Vol_MIN >= searchCondition.RatedVolMin AndAlso aPart.aHeater.Rated_Vol_Max <= searchCondition.RatedVolMax Then Return True
        End If

        Return filterMatches
    End Function

    Public Function PartMatchFilterRatedVolMin(ByVal aPart As OnePart, ByVal MList As List(Of Integer)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aHeater IsNot Nothing AndAlso MList.Contains(aPart.aHeater.Rated_Vol_MIN) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterFeatureTypes(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Voltage_Type) Then filterMatches = True
        If aPart.aNonIlluminate IsNot Nothing AndAlso MList.Contains(aPart.aNonIlluminate.OperatorType) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterBlockType(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Contact_Blocks_Type) Then filterMatches = True
        If aPart.aNonIlluminate IsNot Nothing AndAlso MList.Contains(aPart.aNonIlluminate.Contact_Blocks_Type) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterContacts(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Contacts) Then filterMatches = True
        If aPart.aNonIlluminate IsNot Nothing AndAlso MList.Contains(aPart.aNonIlluminate.Contacts) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterOperaTemp(ByVal aPart As OnePart, ByVal searchCondition As ECSSSearchCriteria) As Boolean
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If searchCondition.OperaTempMin = 0 AndAlso searchCondition.OperaTempMax = 0 Then Return True

        If aPart.aPilotLight IsNot Nothing Then
            If searchCondition.OperaTempMin = 0 AndAlso aPart.aPilotLight.Opera_temp_max >= searchCondition.OperaTempMax Then Return True
            If searchCondition.OperaTempMax = 0 AndAlso aPart.aPilotLight.Opera_temp_min <= searchCondition.OperaTempMin Then Return True
            If aPart.aPilotLight.Opera_temp_min <= searchCondition.OperaTempMin AndAlso aPart.aPilotLight.Opera_temp_max >= searchCondition.OperaTempMax Then Return True
        End If

        If aPart.aNonIlluminate IsNot Nothing Then
            If searchCondition.OperaTempMin = 0 AndAlso aPart.aNonIlluminate.Opera_temp_max >= searchCondition.OperaTempMax Then Return True
            If searchCondition.OperaTempMax = 0 AndAlso aPart.aNonIlluminate.Opera_temp_min <= searchCondition.OperaTempMin Then Return True
            If aPart.aNonIlluminate.Opera_temp_min <= searchCondition.OperaTempMin AndAlso aPart.aNonIlluminate.Opera_temp_max >= searchCondition.OperaTempMax Then Return True
        End If

        If aPart.aTempSwitch IsNot Nothing Then
            If searchCondition.OperaTempMin = 0 AndAlso aPart.aTempSwitch.Opera_Temp_Max >= searchCondition.OperaTempMax Then Return True
            If searchCondition.OperaTempMax = 0 AndAlso aPart.aTempSwitch.Opera_Temp_Min <= searchCondition.OperaTempMin Then Return True
            If aPart.aTempSwitch.Opera_Temp_Min <= searchCondition.OperaTempMin AndAlso aPart.aTempSwitch.Opera_Temp_Max >= searchCondition.OperaTempMax Then Return True
        End If

        If aPart.aPowerSupply IsNot Nothing Then
            If searchCondition.OperaTempMin = 0 AndAlso aPart.aPowerSupply.Opera_temp_max >= searchCondition.OperaTempMax Then Return True
            If searchCondition.OperaTempMax = 0 AndAlso aPart.aPowerSupply.Opera_temp_min <= searchCondition.OperaTempMin Then Return True
            If aPart.aPowerSupply.Opera_temp_min <= searchCondition.OperaTempMin AndAlso aPart.aPowerSupply.Opera_temp_max >= searchCondition.OperaTempMax Then Return True
        End If

        Return filterMatches
    End Function

    Public Function PartMatchFilterOperaTempMax(ByVal aPart As OnePart, ByVal MList As List(Of Integer)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Opera_temp_max) Then filterMatches = True
        If aPart.aNonIlluminate IsNot Nothing AndAlso MList.Contains(aPart.aNonIlluminate.Opera_temp_max) Then filterMatches = True
        If aPart.aTempSwitch IsNot Nothing AndAlso MList.Contains(aPart.aTempSwitch.Opera_Temp_Max) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterPower(ByVal aPart As OnePart, ByVal MList As List(Of Integer)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aHeater IsNot Nothing AndAlso MList.Contains(aPart.aHeater.Power) Then filterMatches = True
        Return filterMatches
    End Function
    Public Function PartMatchFilterBuiltInThem(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aHeater IsNot Nothing AndAlso MList.Contains(aPart.aHeater.BuiltIn_Thermostat) Then filterMatches = True
        Return filterMatches
    End Function
    Public Function PartMatchFilterBuiltInFan(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aHeater IsNot Nothing AndAlso MList.Contains(aPart.aHeater.BuiltIn_Fan) Then filterMatches = True
        Return filterMatches
    End Function
    Public Function PartMatchFilterBuiltInArea(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aHeater IsNot Nothing AndAlso MList.Contains(aPart.aHeater.Area_Class) Then filterMatches = True
        If aPart.aNonIlluminate IsNot Nothing AndAlso MList.Contains(aPart.aNonIlluminate.Area_Class) Then filterMatches = True
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Area_Class) Then filterMatches = True
        If aPart.aPowerSupply IsNot Nothing AndAlso MList.Contains(aPart.aPowerSupply.Area_Class) Then filterMatches = True
        If aPart.aTempSwitch IsNot Nothing AndAlso MList.Contains(aPart.aTempSwitch.Area_Class) Then filterMatches = True
        If aPart.aTheromostat IsNot Nothing AndAlso MList.Contains(aPart.aTheromostat.Area_Class) Then filterMatches = True
        Return filterMatches
    End Function
    Public Function PartMatchFilterBuiltInTempCode(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aHeater IsNot Nothing AndAlso MList.Contains(aPart.aHeater.Temp_Code) Then filterMatches = True
        If aPart.aNonIlluminate IsNot Nothing AndAlso MList.Contains(aPart.aNonIlluminate.Temp_Code) Then filterMatches = True
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Temp_Code) Then filterMatches = True
        If aPart.aPowerSupply IsNot Nothing AndAlso MList.Contains(aPart.aPowerSupply.Temp_Code) Then filterMatches = True
        If aPart.aTempSwitch IsNot Nothing AndAlso MList.Contains(aPart.aTempSwitch.Temp_Code) Then filterMatches = True
        If aPart.aTheromostat IsNot Nothing AndAlso MList.Contains(aPart.aTheromostat.Temp_Code) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterFingersafe(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aNonIlluminate IsNot Nothing AndAlso MList.Contains(aPart.aNonIlluminate.Fingersafe) Then filterMatches = True
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Fingersafe) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterPowerModuleType(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Power_Module) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterLampTest(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Lamp_Test) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterIlluminationOption(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Illumination) Then filterMatches = True
        Return filterMatches
    End Function
    Public Function PartMatchFilterVoltageType(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Voltage_Type) Then filterMatches = True
        Return filterMatches
    End Function
    Public Function PartMatchFilterVoltage(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Voltage) Then filterMatches = True
        Return filterMatches
    End Function

    Public Function PartMatchFilterLenColor(ByVal aPart As OnePart, ByVal MList As List(Of String)) As Boolean
        If MList Is Nothing Then Return True
        If MList.Count = 0 Then Return True
        If aPart Is Nothing Then Return False
        Dim filterMatches As Boolean = False
        If aPart.aPilotLight IsNot Nothing AndAlso MList.Contains(aPart.aPilotLight.Lens_Color) Then filterMatches = True
        Return filterMatches
    End Function
End Class

Public Class ECSSSearchCriteria
    Public keyword As New List(Of String)
    Public PartType As New List(Of ECSSParts.PART_TYPE)
    Public Manufacturer As New List(Of String)
    Public Material As New List(Of String)
    Public Certificates As New List(Of String)

    Public HeightMin, HeightMax As Integer
    Public WidthMin, WidthMax As Integer
    Public DepthMin, DepthMax As Integer
    Public Mount As New List(Of String)
    Public NEMA As New List(Of String)

    Public outputV As New List(Of Integer)
    Public outputA As New List(Of Single)
    Public NormalVMin As New Integer
    Public NormalVMax As New Integer
    Public InputPhase As New List(Of String)
    Public [Class] As New List(Of String)
    Public Group As New List(Of String)

    Public WindowType As New List(Of String)
    Public Color As New List(Of String)
    Public ViewAreaH As New List(Of Integer)
    Public ViewAreaW As New List(Of Integer)
    Public RatedVol As New List(Of Integer)
    Public RatedCurrent As New List(Of Single)
    Public [Functions] As New List(Of String)
    Public Area As New List(Of String)

    Public TempSwitchON As New List(Of Integer)
    Public TempSwitchOFF As New List(Of Integer)

    Public RatedVolMax As New Integer
    Public RatedVolMin As New Integer
    Public SwitchTempON As New List(Of String)
    Public SwitchTempOFF As New List(Of String)
    Public [Types] As New List(Of String)
    Public BlockType As New List(Of String)
    Public Contacts As New List(Of String)
    Public OperaTempMin As New Integer
    Public OperaTempMax As New Integer
    Public Power As New List(Of Integer)
    Public BuiltInThem As New List(Of String)
    Public BuiltInFan As New List(Of String)
    Public TempCode As New List(Of String)
    Public Fingersafe As New List(Of String)
    Public PowerModuleType As New List(Of String)
    Public LampTest As New List(Of String)
    Public IlluminationOption As New List(Of String)
    Public VoltageType As New List(Of String)
    Public Voltage As New List(Of String)
    Public LensColor As New List(Of String)

    Public Function IsEmpty() As Boolean
        If keyword.Count > 0 Then Return False
        If PartType.Count > 0 Then Return False
        If Manufacturer.Count > 0 Then Return False
        If Material.Count > 0 Then Return False
        If Certificates.Count > 0 Then Return False

        If HeightMin > 0 OrElse HeightMax > 0 Then Return False
        If WidthMin > 0 OrElse WidthMax > 0 Then Return False
        If DepthMin > 0 OrElse DepthMax > 0 Then Return False
        If Mount.Count > 0 Then Return False
        If NEMA.Count > 0 Then Return False

        If outputV.Count > 0 Then Return False
        If outputA.Count > 0 Then Return False
        If NormalVMax > 0 OrElse NormalVMin > 0 Then Return False
        If InputPhase.Count > 0 Then Return False
        If [Class].Count > 0 Then Return False
        If Group.Count > 0 Then Return False

        If WindowType.Count > 0 Then Return False
        If Color.Count > 0 Then Return False
        If ViewAreaH.Count > 0 Then Return False
        If ViewAreaW.Count > 0 Then Return False
        If RatedVol.Count > 0 Then Return False
        If RatedCurrent.Count > 0 Then Return False
        If Functions.Count > 0 Then Return False
        If Area.Count > 0 Then Return False
        If TempSwitchON.Count > 0 Then Return False
        If TempSwitchOFF.Count > 0 Then Return False
        If RatedVolMax > 0 OrElse RatedVolMin > 0 Then Return False
        If SwitchTempON.Count > 0 Then Return False
        If SwitchTempOFF.Count > 0 Then Return False
        If [Types].Count > 0 Then Return False
        If BlockType.Count > 0 Then Return False
        If Contacts.Count > 0 Then Return False
        If OperaTempMin > 0 OrElse OperaTempMax > 0 Then Return False
        If Power.Count > 0 Then Return False
        If BuiltInThem.Count > 0 Then Return False
        If BuiltInFan.Count > 0 Then Return False
        If TempCode.Count > 0 Then Return False
        If Fingersafe.Count > 0 Then Return False
        If PowerModuleType.Count > 0 Then Return False
        If LampTest.Count > 0 Then Return False
        If IlluminationOption.Count > 0 Then Return False
        If VoltageType.Count > 0 Then Return False
        If Voltage.Count > 0 Then Return False
        If LensColor.Count > 0 Then Return False

        Return True
    End Function

    Public Overrides Function ToString() As String
        Dim str As String = ""
        If PartType.Count > 0 Then str = str & " Categories: " & Miscelllaneous.ListToString(PartType) & "; " Else str = str & ""
        If Manufacturer.Count > 0 Then str = str & " Manufacturer: " & Miscelllaneous.ListToString(Manufacturer) & "; " Else str = str & ""
        If Material.Count > 0 Then str = str & " Material: " & Miscelllaneous.ListToString(Material) & "; " Else str = str & ""
        If Certificates.Count > 0 Then str = str & " Certificates: " & Miscelllaneous.ListToString(Certificates) & "; " Else str = str & ""

        If HeightMin > 0 OrElse HeightMax > 0 Then str = str & " Height: " & HeightMin & " - " & HeightMax & "; " Else str = str & ""
        If WidthMin > 0 OrElse WidthMax > 0 Then str = str & " Width: " & WidthMin & " - " & WidthMax & "; " Else str = str & ""
        If DepthMin > 0 OrElse DepthMax > 0 Then str = str & " Depth: " & DepthMin & " - " & DepthMax & "; " Else str = str & ""
        If Mount.Count > 0 Then str = str & " Mount: " & Miscelllaneous.ListToString(Mount) & "; " Else str = str & ""
        If NEMA.Count > 0 Then str = str & " NEMA Type: " & Miscelllaneous.ListToString(NEMA) & "; " Else str = str & ""

        If outputV.Count > 0 Then str = str & " Output Voltage: " & Miscelllaneous.ListToString(outputV) & "; " Else str = str & ""
        If outputA.Count > 0 Then str = str & " Output Current: " & Miscelllaneous.ListToString(outputA) & "; " Else str = str & ""
        If NormalVMin > 0 OrElse NormalVMax > 0 Then str = str & " Normal Voltage: " & NormalVMin & " - " & NormalVMax & "; " Else str = str & ""
        If InputPhase.Count > 0 Then str = str & " Input Phase: " & Miscelllaneous.ListToString(InputPhase) & "; " Else str = str & ""
        If [Class].Count > 0 Then str = str & " Class: " & Miscelllaneous.ListToString([Class]) & "; " Else str = str & ""
        If Group.Count > 0 Then str = str & " Group Phase: " & Miscelllaneous.ListToString(Group) & "; " Else str = str & ""

        If WindowType.Count > 0 Then str = str & " Window Type: " & Miscelllaneous.ListToString(WindowType) & "; " Else str = str & ""
        If Color.Count > 0 Then str = str & " Color: " & Miscelllaneous.ListToString(Color) & "; " Else str = str & ""
        If ViewAreaH.Count > 0 Then str = str & " View Area Height: " & Miscelllaneous.ListToString(ViewAreaH) & "; " Else str = str & ""
        If ViewAreaW.Count > 0 Then str = str & " View Area Width: " & Miscelllaneous.ListToString(ViewAreaW) & "; " Else str = str & ""
        If RatedVol.Count > 0 Then str = str & " Rated Voltage: " & Miscelllaneous.ListToString(RatedVol) & "; " Else str = str & ""
        If RatedCurrent.Count > 0 Then str = str & " Rated Current: " & Miscelllaneous.ListToString(RatedCurrent) & "; " Else str = str & ""
        If Functions.Count > 0 Then str = str & " Functions: " & Miscelllaneous.ListToString(Functions) & "; " Else str = str & ""
        If Area.Count > 0 Then str = str & " Area Class: " & Miscelllaneous.ListToString(Area) & "; " Else str = str & ""
        If TempSwitchON.Count > 0 Then str = str & " Temp Switch ON: " & Miscelllaneous.ListToString(TempSwitchON) & "; " Else str = str & ""
        If TempSwitchOFF.Count > 0 Then str = str & " Temp Switch OFF: " & Miscelllaneous.ListToString(TempSwitchOFF) & "; " Else str = str & ""
        If RatedVolMin > 0 OrElse RatedVolMax > 0 Then str = str & " Rated Voltage: " & RatedVolMin & " - " & RatedVolMax & "; " Else str = str & ""
        If SwitchTempON.Count > 0 Then str = str & " Switch Temp ON: " & Miscelllaneous.ListToString(SwitchTempON) & "; " Else str = str & ""
        If SwitchTempOFF.Count > 0 Then str = str & " Switch Temp OFF: " & Miscelllaneous.ListToString(SwitchTempOFF) & "; " Else str = str & ""
        If [Types].Count > 0 Then str = str & " Types: " & Miscelllaneous.ListToString([Types]) & "; " Else str = str & ""
        If BlockType.Count > 0 Then str = str & " Block Type: " & Miscelllaneous.ListToString(BlockType) & "; " Else str = str & ""
        If Contacts.Count > 0 Then str = str & " Contacts: " & Miscelllaneous.ListToString(Contacts) & "; " Else str = str & ""

        If OperaTempMin = 0 AndAlso Math.Abs(OperaTempMax) > 0 Then
            str = str & " Operation Temprture: > " & OperaTempMax & "; "
        ElseIf Math.Abs(OperaTempMin) > 0 AndAlso OperaTempMax = 0 Then
            str = str & " Operation Temprture: < " & OperaTempMin & "; "
        ElseIf Math.Abs(OperaTempMin) > 0 AndAlso Math.Abs(OperaTempMax) > 0 Then
            str = str & " Operation Temprture: " & OperaTempMin & " - " & OperaTempMax & "; "
        Else
            str = str & ""
        End If

        If Power.Count > 0 Then str = str & " Power: " & Miscelllaneous.ListToString(Power) & "; " Else str = str & ""
        If BuiltInThem.Count > 0 Then str = str & " Built-In Themometer: " & Miscelllaneous.ListToString(BuiltInThem) & "; " Else str = str & ""
        If BuiltInFan.Count > 0 Then str = str & " Built-In Fan: " & Miscelllaneous.ListToString(BuiltInFan) & "; " Else str = str & ""
        If TempCode.Count > 0 Then str = str & " Temp. Code: " & Miscelllaneous.ListToString(TempCode) & "; " Else str = str & ""
        If Fingersafe.Count > 0 Then str = str & " Fingersafe: " & Miscelllaneous.ListToString(Fingersafe) & "; " Else str = str & ""
        If PowerModuleType.Count > 0 Then str = str & " Power Module Type: " & Miscelllaneous.ListToString(PowerModuleType) & "; " Else str = str & ""
        If LampTest.Count > 0 Then str = str & " Lamp Test: " & Miscelllaneous.ListToString(LampTest) & "; " Else str = str & ""
        If IlluminationOption.Count > 0 Then str = str & " Illumination Option: " & Miscelllaneous.ListToString(IlluminationOption) & "; " Else str = str & ""
        If VoltageType.Count > 0 Then str = str & " Voltage Type: " & Miscelllaneous.ListToString(VoltageType) & "; " Else str = str & ""
        If Voltage.Count > 0 Then str = str & " Voltage: " & Miscelllaneous.ListToString(Voltage) & "; " Else str = str & ""
        If LensColor.Count > 0 Then str = str & " Lens Color: " & Miscelllaneous.ListToString(LensColor) & "; " Else str = str & ""

        Return str
    End Function

End Class


Public Class OneBOMList
    Public BOMID As String = ""
    Public BOMTitle As String = ""
    Public CreateTime As Date
    Public BOMList As New List(Of ECSSBOM)
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
    Public Shared Function LoadBOM(ByVal BOMdic As Dictionary(Of String, OneBOMList)) As Boolean
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
                        BOMdic.Item(aBOM.BOMID).BOMList.Add(aBOM)
                    Else
                        Dim alist As New OneBOMList
                        alist.BOMList = New List(Of ECSSBOM)({aBOM})
                        alist.BOMID = aBOM.BOMID
                        alist.BOMTitle = aRow.Item("BOMTITLE").ToString
                        alist.CreateTime = CDate(aRow.Item("CREATETIME"))
                        BOMdic.Add(aBOM.BOMID, alist)
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
