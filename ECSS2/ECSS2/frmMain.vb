﻿Imports System.ComponentModel
Imports System.Runtime.ExceptionServices

Public Class frmMain

    Private FIRSTTIME As Boolean = True
    Private ECSSParts As New ECSSParts
    Private ECSSSearch As New ECSSSearchCriteria
    Private BOMdic As New Dictionary(Of String, OneBOMList) 'key BOMID 
    ' Declare a Hashtable array in which to store the groups.
    Private groupTables() As Hashtable
    Private NeedSave As Boolean = False
    Private FullSizeHeight As Integer

    Private Const ColManufacturerWidthDefault As Integer = 100
    Private Const ColHighlightWidthDefault As Integer = 250
    Private Const SmallSizeHeight As Integer = 75

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    <System.STAThread()>
    Public Shared Sub Main()

        System.Windows.Forms.Application.EnableVisualStyles()
        System.Windows.Forms.Application.Run(New frmMain)
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ECSSDBFunctions.UserDB = New SQLiteDBFunctions(GlobalSettings.ECSSUSER_DB)
            ECSSDBFunctions.PartDB = New SQLiteDBFunctions(GlobalSettings.ECSS_DB)
            GlobalSettings.LoadUserConfig()

            Me.ECSSParts.ReadPartsFromDB()
            BOMHelper.LoadBOM(Me.BOMdic)
            Me.UpdateDetail(Nothing, True)
            Me.Updatefilters(Nothing)
            Me.SetSmallSize()
        Catch ex As Exception
            MessageBox.Show("Error: Loading main dialouge." & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.SetSearchSuggestList(False)
            Me.ResetAllFilters()
            If GlobalSettings.MultiKeywords Then
                Me.txtSearch.Visible = False
                Me.txtSearchMulti.Visible = True
            Else
                Me.txtSearch.Visible = True
                Me.txtSearchMulti.Visible = False
            End If
            Me.TopMost = GlobalSettings.AlwaysOnTop
            Me.lblError.Visible = False
        Catch ex As Exception
        Finally
            Me.SetTitle()
            Me.FIRSTTIME = False
        End Try
    End Sub


    Private Sub SetTitle()
        Me.Text = GlobalSettings.COMPANY & ": " & GlobalSettings.ECSS_TITLE & " -- ( " & GlobalSettings.GetDEMOVersion & " )"
    End Sub

    Private Sub SetSmallSize()
        Me.FullSizeHeight = Me.Height
        Me.Height = SmallSizeHeight
        Me.btnBOM.Visible = False
    End Sub

    Private Sub SetFullSize()
        Me.Height = Me.FullSizeHeight
        Me.btnBOM.Visible = True
    End Sub

    Private Sub SetSearchSuggestList(ByVal cleanOnly As Boolean)
        Try
            If Me.ECSSParts IsNot Nothing AndAlso Me.ECSSParts.Keywords IsNot Nothing Then
                Control.CheckForIllegalCrossThreadCalls = False
                Me.txtSearch.AutoCompleteCustomSource.Clear()
                If cleanOnly = False Then
                    Me.txtSearch.AutoCompleteCustomSource.AddRange(Me.ECSSParts.Keywords.ToArray)
                    Me.txtSearchMulti.EmailAutocompleteSource = Me.ECSSParts.Keywords.ToArray
                    Me.txtSearchMulti.BringToFront()
                    Me.splPart.SendToBack()
                End If
                Control.CheckForIllegalCrossThreadCalls = True
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub


    Private Sub LoadlstPart()
        If Me.ECSSParts Is Nothing OrElse Me.ECSSParts.PartDic Is Nothing Then Exit Sub
        Dim TempPartList As New List(Of ECSSParts.OnePart)
        Dim HighLight As String = ""
        Try
            Me.lstPart.Items.Clear()
            Me.lstPart.BeginUpdate()
            For Each kvp In Me.ECSSParts.PartDic
                If Me.ECSSParts.PartMatchesSearchCriteria(kvp.Value, Me.ECSSSearch) Then
                    TempPartList.Add(kvp.Value)
                End If
            Next

            Dim aPart As ECSSParts.OnePart = Nothing
            For i As Integer = 0 To TempPartList.Count - 1
                If i >= GlobalSettings.MaxDisplay Then Exit For
                aPart = TempPartList.Item(i)
                Dim anItem As ListViewItem = New ListViewItem(aPart.PartID)   'ID  'Name
                anItem.SubItems.Add(aPart.Manufacturer)
                anItem.SubItems.Add(Miscelllaneous.ListToString(aPart.Certificates))
                Select Case aPart.PartType
                    Case ECSSParts.PART_TYPE.TRANSFORMER
                        anItem.SubItems.Add("")
                    Case ECSSParts.PART_TYPE.POWER_SUPPLY
                        anItem.SubItems.Add("")
                    Case ECSSParts.PART_TYPE.ENCLOSURE
                        anItem.SubItems.Add(aPart.aEnclosure.Material)
                    Case ECSSParts.PART_TYPE.SERVIT_POST
                        anItem.SubItems.Add(aPart.aServitPost.Material)
                    Case ECSSParts.PART_TYPE.BREATHER_DRAIN
                        anItem.SubItems.Add(aPart.aBreatherDrain.Material)
                    Case ECSSParts.PART_TYPE.WINDOW_KIT
                        anItem.SubItems.Add(aPart.aWindowKit.Material)
                    Case ECSSParts.PART_TYPE.TEMP_SWITCH, ECSSParts.PART_TYPE.THEROMOSTAT
                        anItem.SubItems.Add("")
                    Case Else
                        anItem.SubItems.Add("")
                End Select
                anItem.SubItems.Add(aPart.HighLight)
                anItem.SubItems.Add(aPart.Description)
                anItem.SubItems.Add(ECSSParts.GetTypeName(aPart.PartType))
                anItem.Tag = aPart.PartID
                Me.lstPart.Items.Add(anItem)
            Next

            If TempPartList.Count > 0 Then
                groupTables = New Hashtable(2) {}
                groupTables(0) = CreateGroupsTable(1) 'Manufacture
                groupTables(1) = CreateGroupsTable(6) 'Part Type

                Dim PTlist = Me.GetPartTypeList(TempPartList)
                If PTlist IsNot Nothing Then
                    If PTlist.Count = 1 Then
                        Me.ColManufacturer.Width = 0
                        Me.ColHighlight.Width = ColHighlightWidthDefault + ColManufacturerWidthDefault
                        SetGroups(0, 1)
                    Else
                        Me.ColManufacturer.Width = ColManufacturerWidthDefault
                        Me.ColHighlight.Width = ColHighlightWidthDefault
                        SetGroups(1, 6)
                    End If
                End If

                If PTlist IsNot Nothing Then
                    If PTlist.Count = 1 Then
                        Me.ResetAllFilters()
                        Me.UpdateFiltersPerType(PTlist(0))
                    Else
                        Me.ResetAllFilters()
                    End If
                End If
                Me.FIRSTTIME = True
                Me.Updatefilters(TempPartList)
                Me.FIRSTTIME = False
            Else
                Me.ResetAllFilters()
                Me.FIRSTTIME = True
                Me.Updatefilters(Nothing)
                Me.FIRSTTIME = False
            End If

            Me.lstPart.EndUpdate()

            If TempPartList.Count > 0 Then
                Me.lblTotal.Text = "Total " & TempPartList.Count & " Parts"
                Miscelllaneous.PaintAlternatingBackColor(Me.lstPart, Color.White, Color.Honeydew)
            Else
                Me.lblTotal.Text = ""
                Me.lstPart.Refresh()
            End If

            If TempPartList.Count > GlobalSettings.MaxDisplay Then
                Me.lblError.Text = "There are " & TempPartList.Count & " parts in the result, only display the first " & GlobalSettings.MaxDisplay & " records!"
                Me.lblError.Visible = True
            Else
                Me.lblError.Visible = False
            End If
            Me.UpdateDetail(Nothing, True)

            If TempPartList.Count > 0 AndAlso Me.Height = SmallSizeHeight Then
                Me.SetFullSize()
            End If

        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        Finally
            Me.lblFilterDisplay.Text = Me.ECSSSearch.ToString.Trim
        End Try
    End Sub

    Private Function GetPartTypeList(ByVal PLst As List(Of ECSSParts.OnePart)) As List(Of ECSSParts.PART_TYPE)
        If PLst Is Nothing OrElse PLst.Count = 0 Then Return Nothing
        Dim tlst As New List(Of ECSSParts.PART_TYPE)
        For Each p In PLst
            If tlst.Contains(p.PartType) = False Then tlst.Add(p.PartType)
        Next
        Return tlst
    End Function

    Private Sub UpdateDetail(ByVal aPart As ECSSParts.OnePart, ByVal reset As Boolean)
        Try
            If reset Then
                Me.dgvDetail.Rows.Clear()
                Exit Sub
            End If

            Me.dgvDetail.Rows.Clear()
            If aPart Is Nothing Then
                Me.dgvDetail.Enabled = False
            Else
                Me.dgvDetail.Enabled = True

                Me.dgvDetail.Columns(0).DefaultCellStyle.Font = New Font(Me.dgvDetail.Font, FontStyle.Bold)
                Me.dgvDetail.Columns(2).DefaultCellStyle.Font = New Font(Me.dgvDetail.Font, FontStyle.Bold)
                Me.dgvDetail.Columns(4).DefaultCellStyle.Font = New Font(Me.dgvDetail.Font, FontStyle.Bold)

                Me.dgvDetail.Rows.Add("Part ID", aPart.PartID, "Type", ECSSParts.GetTypeName(aPart.PartType), "Manufacturer", aPart.Manufacturer)

                If aPart.PartType = ECSSParts.PART_TYPE.TRANSFORMER OrElse
                   aPart.PartType = ECSSParts.PART_TYPE.ENCLOSURE OrElse
                   aPart.PartType = ECSSParts.PART_TYPE.POWER_SUPPLY OrElse
                   aPart.PartType = ECSSParts.PART_TYPE.TEMP_SWITCH OrElse
                   aPart.PartType = ECSSParts.PART_TYPE.THEROMOSTAT OrElse
                   aPart.PartType = ECSSParts.PART_TYPE.PILOT_LIGHT OrElse
                   aPart.PartType = ECSSParts.PART_TYPE.HEATER Then
                    Me.dgvDetail.Rows.Add("Height", aPart.Height & " (mm)", "Width", aPart.Width & " (mm)", "Depth", aPart.Depth & " (mm)")
                End If

                Select Case aPart.PartType
                    Case ECSSParts.PART_TYPE.TRANSFORMER
                        Me.dgvDetail.Rows.Add("Power", aPart.aTransformer.Power & " (VA)", "Frequency", aPart.aTransformer.Frequency & " (Hz)", "Phase I", aPart.aTransformer.Phase1)
                        Me.dgvDetail.Rows.Add("Pri. Voltage", aPart.aTransformer.Pri_Voltage & " (V AC)", "Phase II", aPart.aTransformer.Phase2, "Sec. Voltage", aPart.aTransformer.Sec_Voltage & " (V AC)")
                    Case ECSSParts.PART_TYPE.POWER_SUPPLY
                        Me.dgvDetail.Rows.Add("Output Power", aPart.aPowerSupply.Output_Power & " (W)", "Output Current", aPart.aPowerSupply.Output_Current & " (A)", "Output Voltage", aPart.aPowerSupply.Output_Vol & " (V DC)")
                        Me.dgvDetail.Rows.Add("Input Phase", aPart.aPowerSupply.Input_Phase, "Operation Temp. Min", aPart.aPowerSupply.Opera_temp_min & " (°C)", "Operation Temp. Max", aPart.aPowerSupply.Opera_temp_max & " (°C)")
                        Me.dgvDetail.Rows.Add("Storage Temp. Min", aPart.aPowerSupply.Stor_temp_min & " (°C)", "Storage Temp. Max", aPart.aPowerSupply.Stor_temp_max & " (°C)", "Area Classification", aPart.aPowerSupply.Area_Class)
                        Me.dgvDetail.Rows.Add("Class", aPart.aPowerSupply.SupplyClass, "Division", aPart.aPowerSupply.Division, "Gas Group", aPart.aPowerSupply.Gas_Group)
                        Me.dgvDetail.Rows.Add("Temp. Code", aPart.aPowerSupply.Temp_Code, "", "", "", "")

                    Case ECSSParts.PART_TYPE.ENCLOSURE
                        Me.dgvDetail.Rows.Add("Color", aPart.aEnclosure.Color, "NEMA Type", Miscelllaneous.ListToString(aPart.aEnclosure.NEMA_Type), "Window", aPart.aEnclosure.Window)
                        Me.dgvDetail.Rows.Add("Mount Type", aPart.aEnclosure.MountType, "Mount ID", Miscelllaneous.ListToString(aPart.aEnclosure.MountID), "Mount Height", aPart.aEnclosure.MountHeight)
                        Me.dgvDetail.Rows.Add("Mount Width", aPart.aEnclosure.MountWidth, "PDF", aPart.PDF, "", "")
                        Dim cell As SpannedDataGridViewNet2.DataGridViewTextBoxCellEx = Me.dgvDetail.Rows(4).Cells(3)
                        cell.ColumnSpan = 3

                    Case ECSSParts.PART_TYPE.SERVIT_POST
                        Me.dgvDetail.Rows.Add("Type", aPart.aServitPost.Part_type, "Material", aPart.aServitPost.Material, "", "")
                        Me.dgvDetail.Rows.Add("Conductor Range Stranded", aPart.aServitPost.Range_Stranded & " (AWG)", "Conductor Range Solid", aPart.aServitPost.Range_Soild & " (AWG)", "", "")

                    Case ECSSParts.PART_TYPE.BREATHER_DRAIN
                        Me.dgvDetail.Rows.Add("Size", aPart.aBreatherDrain.Part_size, "Material", aPart.aBreatherDrain.Material, "NEMA Type", Miscelllaneous.ListToString(aPart.aBreatherDrain.NEMA_Type))

                    Case ECSSParts.PART_TYPE.WINDOW_KIT
                        Me.dgvDetail.Rows.Add("NEMA Type", Miscelllaneous.ListToString(aPart.aWindowKit.NEMA_Type), "Material", aPart.aWindowKit.Material, "Kit_Type", aPart.aWindowKit.Kit_Type)
                        Me.dgvDetail.Rows.Add("Color", aPart.aWindowKit.Color, "Weight", aPart.aWindowKit.Weight & " (kg)", "Overal Height", aPart.aWindowKit.OveralHeight & " (mm)")
                        Me.dgvDetail.Rows.Add("Overal Width", aPart.aWindowKit.OveralWidth & " (mm)", "View Height", aPart.aWindowKit.ViewHeight & " (mm)", "View Width", aPart.aWindowKit.ViewWidth & " (mm)")
                        Me.dgvDetail.Rows.Add("Cutout Height", aPart.aWindowKit.CutoutHeight & " (mm)", "Cutout Width", aPart.aWindowKit.CutoutWidth & " (mm)", "", "")

                    Case ECSSParts.PART_TYPE.THEROMOSTAT
                        Me.dgvDetail.Rows.Add("Monut", aPart.aTheromostat.Monut, "Rated_Vol_Max", aPart.aTheromostat.Rated_Vol_Max & " (V)", "Rated_Current", aPart.aTheromostat.Rated_Current & " (A)")
                        Me.dgvDetail.Rows.Add("Function", aPart.aTheromostat.TheroFunction, "Adjust Min", aPart.aTheromostat.AdjustMin, "Adjust Max", aPart.aTheromostat.AdjustMax)
                        Me.dgvDetail.Rows.Add("Opera Temp Min", aPart.aTheromostat.Opera_Temp_Min & " (°C)", "Opera Temp Max", aPart.aTheromostat.Opera_Temp_Max & " (°C)", "Weight", aPart.aTheromostat.Weight & " (kg)")
                        Me.dgvDetail.Rows.Add("Area Classification", aPart.aTheromostat.Area_Class, "Class", aPart.aTheromostat.Part_Class, "Gas Group", aPart.aTheromostat.Gas_Group)
                        Me.dgvDetail.Rows.Add("Temp. Code", aPart.aTheromostat.Temp_Code, "", "", "", "")
                    Case ECSSParts.PART_TYPE.TEMP_SWITCH
                        Me.dgvDetail.Rows.Add("Monut", aPart.aTempSwitch.Monut, "Rated_Vol_Max", aPart.aTempSwitch.Rated_Vol_Max & " (V)", "Rated_Current", aPart.aTempSwitch.Rated_Current & " (A)")
                        Me.dgvDetail.Rows.Add("Function", aPart.aTempSwitch.SwitchFunction, "Switch_Temp_ON", aPart.aTempSwitch.Switch_Temp_ON, "Switch_Temp_OFF", aPart.aTempSwitch.Switch_Temp_OFF)
                        Me.dgvDetail.Rows.Add("Opera Temp Min", aPart.aTempSwitch.Opera_Temp_Min & " (°C)", "Opera Temp Max", aPart.aTempSwitch.Opera_Temp_Max & " (°C)", "Weight", aPart.aTempSwitch.Weight & " (kg)")
                        Me.dgvDetail.Rows.Add("Area Classification", aPart.aTempSwitch.Area_Class, "Class", aPart.aTempSwitch.Part_Class, "Gas Group", aPart.aTempSwitch.Gas_Group)
                        Me.dgvDetail.Rows.Add("Temp. Code", aPart.aTempSwitch.Temp_Code, "Adjustable", aPart.aTempSwitch.Temp_Code, "", "")
                    Case ECSSParts.PART_TYPE.PILOT_LIGHT
                        Me.dgvDetail.Rows.Add("NEMA Type", Miscelllaneous.ListToString(aPart.aPilotLight.NEMA_TYPE), "Material", aPart.aPilotLight.Material, "Fingersafe Guard", aPart.aPilotLight.Fingersafe)
                        Me.dgvDetail.Rows.Add("Power Module", aPart.aPilotLight.Power_Module, "Lamp Test", aPart.aPilotLight.Lamp_Test, "Illumination", aPart.aPilotLight.Illumination)
                        Me.dgvDetail.Rows.Add("Contact Blocks Type", aPart.aPilotLight.Contact_Blocks_Type, "Voltage Type", aPart.aPilotLight.Voltage_Type, "Voltage", aPart.aPilotLight.Voltage & " (A)")
                        Me.dgvDetail.Rows.Add("Contacts", aPart.aPilotLight.Contacts, "Store Temp Min", aPart.aPilotLight.Stor_temp_min & " (°C)", "Store Temp Max", aPart.aPilotLight.Stor_temp_max & " (°C)")
                        Me.dgvDetail.Rows.Add("Opera Temp Min", aPart.aPilotLight.Opera_temp_min & " (°C)", "Opera Temp Max", aPart.aPilotLight.Opera_temp_max & " (°C)", "Weight", aPart.aPilotLight.Weight & " (kg)")
                        Me.dgvDetail.Rows.Add("Area Classification", aPart.aPilotLight.Area_Class, "Class", aPart.aPilotLight.Class, "Gas Groups", aPart.aPilotLight.Gas_Groups)
                        Me.dgvDetail.Rows.Add("Temp. Code", aPart.aPilotLight.Temp_Code, "Link", aPart.link, "Lens Color", aPart.aPilotLight.Lens_Color)
                    Case ECSSParts.PART_TYPE.HEATER
                        Me.dgvDetail.Rows.Add("Power", aPart.aHeater.Power & " (W)", "Rated Vol Max", aPart.aHeater.Rated_Vol_Max & " (V)", "Rated Vol Min", aPart.aHeater.Rated_Vol_MIN & " (V)")
                        Me.dgvDetail.Rows.Add("Frequency", Miscelllaneous.ListToString(aPart.aHeater.Frequency) & " (Hz)", "BuiltIn Thermostat", aPart.aHeater.BuiltIn_Thermostat, "BuiltIn Fan", aPart.aHeater.BuiltIn_Fan)
                        Me.dgvDetail.Rows.Add("Datasheet", aPart.aHeater.Datasheet, "Temp. Code", aPart.aHeater.Temp_Code, "Weight", aPart.aHeater.Weight & " (kg)")
                        Me.dgvDetail.Rows.Add("Area Classification", aPart.aHeater.Area_Class, "Class", aPart.aHeater.Class, "Gas Groups", aPart.aHeater.Gas_Groups)
                        Me.dgvDetail.Rows.Add("Link", aPart.link, "", "", "", "")
                        Dim cell As SpannedDataGridViewNet2.DataGridViewTextBoxCellEx = Me.dgvDetail.Rows(6).Cells(1)
                        cell.ColumnSpan = 3

                    Case ECSSParts.PART_TYPE.SELECTOR_SWITCH, ECSSParts.PART_TYPE.ESTOP, ECSSParts.PART_TYPE.PUSH_BUTTON
                        Me.dgvDetail.Rows.Add("NEMA Type", Miscelllaneous.ListToString(aPart.aNonIlluminate.NEMA_TYPE), "Material", aPart.aNonIlluminate.Material, "Fingersafe Guard", aPart.aNonIlluminate.Fingersafe)
                        Me.dgvDetail.Rows.Add("Operation Type", aPart.aNonIlluminate.OperatorType, "Functions", aPart.aNonIlluminate.Functions, "MushroomHead", aPart.aNonIlluminate.MushroomHead)
                        Me.dgvDetail.Rows.Add("Contact Blocks Type", aPart.aNonIlluminate.Contact_Blocks_Type, "Store Temp Min", aPart.aNonIlluminate.Stor_temp_min & " (°C)", "Store Temp Max", aPart.aNonIlluminate.Stor_temp_max & " (°C)")
                        Me.dgvDetail.Rows.Add("Contacts", aPart.aNonIlluminate.Contacts, "Opera Temp Min", aPart.aNonIlluminate.Opera_temp_min & " (°C)", "Opera Temp Max", aPart.aNonIlluminate.Opera_temp_max & " (°C)")
                        Me.dgvDetail.Rows.Add("Area Classification", aPart.aNonIlluminate.Area_Class, "Class", aPart.aNonIlluminate.Class, "Gas Groups", aPart.aNonIlluminate.Gas_Groups)
                        Me.dgvDetail.Rows.Add("Temp. Code", aPart.aNonIlluminate.Temp_Code, "Link", aPart.link, "Color", aPart.aNonIlluminate.Color)
                End Select

                'add button 

                Me.dgvDetail.Rows.Add("", "", "", "", "", "")
                Dim btnCell As New DataGridViewButtonCell
                btnCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                btnCell.Style.BackColor = dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(0).Style.BackColor
                btnCell.Style.Font = dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(0).Style.Font
                btnCell.Value = "Add to BOM"
                Me.dgvDetail.Rows(dgvDetail.RowCount - 1).Cells(5) = btnCell

                Dim PreviewHeight As Integer = Me.dgvDetail.Rows(0).Height * Me.dgvDetail.RowCount + 5 - dgvDetail.Height
                splDetail.SplitterDistance = splDetail.SplitterDistance - PreviewHeight
            End If

        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub


    Private Sub lstPart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstPart.SelectedIndexChanged
        Dim aPart As ECSSParts.OnePart = Nothing
        Try
            If Me.lstPart.SelectedItems IsNot Nothing AndAlso Me.lstPart.SelectedItems.Count > 0 Then
                Dim aItem As ListViewItem = Me.lstPart.SelectedItems(0)
                If aItem IsNot Nothing AndAlso aItem.Tag IsNot Nothing Then
                    If Me.ECSSParts.PartDic.ContainsKey(aItem.Tag.ToString) Then
                        aPart = Me.ECSSParts.PartDic.Item(aItem.Tag.ToString)
                        Me.UpdateDetail(aPart, False)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub txtSearchMulti_TextChanged(sender As Object, e As EventArgs) Handles txtSearchMulti.ValueChanged
        If Me.FIRSTTIME Then Exit Sub
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim input As String = Me.txtSearchMulti.Text
            If String.IsNullOrEmpty(input.Trim) = False AndAlso input.Contains(",") Then
                Dim iList = input.Split(",")
                Dim tempkey As New List(Of String)
                For i As Integer = 0 To iList.Count - 2
                    tempkey.Add(iList(i).Trim)
                Next
                If Me.ECSSSearch.keyword.SequenceEqual(tempkey) = False Then
                    Me.ECSSSearch.keyword.Clear()
                    Me.ECSSSearch.keyword.AddRange(tempkey)
                    Me.LoadlstPart()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If Me.FIRSTTIME Then Exit Sub
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim input As String = Me.txtSearch.Text
            Dim keyword As String = ""
            If String.IsNullOrEmpty(input.Trim) = False AndAlso input.Contains(" ") Then
                Dim strlst = input.Split(" ")
                If strlst(strlst.Length - 1) = "" Then
                    keyword = input.Trim
                Else
                    For i As Integer = 0 To strlst.Length - 2
                        keyword = keyword & strlst(i) & " "
                    Next
                    keyword = keyword.Trim
                End If
                If Me.ECSSSearch.keyword.Contains(keyword) = False Then
                    Me.ECSSSearch.keyword.Clear()
                    Me.ECSSSearch.keyword.Add(keyword.Trim)
                    Me.LoadlstPart()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub txtSearch_Validating(sender As Object, e As CancelEventArgs) Handles txtSearch.Validating, txtSearchMulti.TextValidating
        If Me.FIRSTTIME Then Exit Sub
        Try
            Dim input As String = ""
            If TypeOf sender Is TextBox Then
                input = DirectCast(sender, TextBox).Text
            ElseIf TypeOf sender Is CustomControls.TextBoxEmailAutocomplete Then
                input = DirectCast(sender, CustomControls.TextBoxEmailAutocomplete).Text
            End If

            If String.IsNullOrEmpty(input) = False Then
                If Me.ECSSSearch.keyword.Contains(input) = False Then
                    Me.ECSSSearch.keyword.Clear()
                    If input.Contains(",") Then
                        Dim iList = input.Split(",")
                        For Each Str As String In iList
                            If String.IsNullOrEmpty(Str.Trim) = False Then
                                Me.ECSSSearch.keyword.Add(Str.Trim)
                            End If
                        Next
                    Else
                        Me.ECSSSearch.keyword.Add(input.Trim)
                    End If
                    Me.LoadlstPart()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown, txtSearchMulti.TextKeyDown
        If Me.FIRSTTIME Then Exit Sub
        Try
            If e.KeyCode = Keys.Enter Then
                Dim input As String = ""
                If TypeOf sender Is TextBox Then
                    input = DirectCast(sender, TextBox).Text
                ElseIf TypeOf sender Is CustomControls.TextBoxEmailAutocomplete Then
                    input = DirectCast(sender, CustomControls.TextBoxEmailAutocomplete).Text
                End If
                If String.IsNullOrEmpty(input.Trim) = False Then
                    If Me.ECSSSearch.keyword.Contains(input) = False Then
                        Me.ECSSSearch.keyword.Clear()
                        If input.Contains(",") Then
                            Dim iList = input.Split(",")
                            For Each Str As String In iList
                                If String.IsNullOrEmpty(Str.Trim) = False Then
                                    Me.ECSSSearch.keyword.Add(Str.Trim)
                                End If
                            Next
                        Else
                            Me.ECSSSearch.keyword.Add(input.Trim)
                        End If
                        Me.LoadlstPart()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub


#Region "Listview Group"
    Private Sub SetGroups(ByVal nType As Integer, ByVal column As Integer)
        ' Remove the current groups.
        Me.lstPart.Groups.Clear()

        ' Retrieve the hash table corresponding to the column.
        Dim groups As Hashtable = CType(groupTables(nType), Hashtable)

        ' Copy the groups for the column to an array.
        Dim groupsArray(groups.Count - 1) As ListViewGroup
        groups.Values.CopyTo(groupsArray, 0)

        ' Sort the groups and add them to myListView.
        Array.Sort(groupsArray, New ListViewGroupSorter(Me.lstPart.Sorting))
        Me.lstPart.Groups.AddRange(groupsArray)

        ' Iterate through the items in myListView, assigning each 
        ' one to the appropriate group.
        Dim item As ListViewItem
        For Each item In Me.lstPart.Items
            ' Retrieve the subitem text corresponding to the column.
            Dim subItemText As String = item.SubItems(column).Text
            ' Assign the item to the matching group.
            item.Group = CType(groups(subItemText), ListViewGroup)
        Next item
    End Sub

    ' Creates a Hashtable object with one entry for each unique
    ' subitem value (or initial letter for the parent item)
    ' in the specified column.
    Private Function CreateGroupsTable(column As Integer) As Hashtable
        ' Create a Hashtable object.
        Dim groups As New Hashtable()

        ' Iterate through the items in myListView.
        Dim item As ListViewItem
        For Each item In Me.lstPart.Items
            ' Retrieve the text value for the column.
            Dim subItemText As String = item.SubItems(column).Text

            ' Use the initial letter instead if it is the first column.
            If column = 0 Then
                subItemText = subItemText.Substring(0, 1)
            End If

            ' If the groups table does not already contain a group
            ' for the subItemText value, add a new group using the 
            ' subItemText value for the group header and Hashtable key.
            If Not groups.Contains(subItemText) Then
                groups.Add(subItemText, New ListViewGroup(subItemText,
                    HorizontalAlignment.Left))
            End If
        Next item

        ' Return the Hashtable object.
        Return groups
    End Function 'CreateGroupsTable

    ' Sorts ListViewGroup objects by header value.
    Private Class ListViewGroupSorter
        Implements IComparer

        Private order As SortOrder

        ' Stores the sort order.
        Public Sub New(theOrder As SortOrder)
            order = theOrder
        End Sub

        ' Compares the groups by header value, using the saved sort
        ' order to return the correct value.
        Public Function Compare(x As Object, y As Object) As Integer _
            Implements IComparer.Compare
            Dim result As Integer = String.Compare(
                CType(x, ListViewGroup).Header,
                CType(y, ListViewGroup).Header)
            If order = SortOrder.Ascending Then
                Return result
            Else
                Return -result
            End If
        End Function 'Compare
    End Class

#End Region

    Private Sub UpdateTreeView()

        Me.TreeBOM.Nodes.Clear()
        If Me.BOMdic Is Nothing OrElse Me.BOMdic.Count = 0 Then Exit Sub
        Try
            Me.TreeBOM.BeginUpdate()
            For Each kvp In Me.BOMdic
                Me.TreeBOM.Nodes.Add(kvp.Value.BOMTitle)
                Me.TreeBOM.Nodes(Me.TreeBOM.Nodes.Count - 1).Tag = kvp.Key
            Next
            Me.TreeBOM.EndUpdate()
            Me.TreeBOM.SelectedNode = Me.TreeBOM.Nodes(0)
            Me.TreeBOM.Focus()
            UpdateBOMList()
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub UpdateBOMList()
        Me.dgvBOM.Rows.Clear()
        Try
            If Me.TreeBOM.SelectedNode Is Nothing Then Exit Sub
            Dim selectedBOM As String = Me.TreeBOM.SelectedNode.Tag.ToString
            If Me.BOMdic.ContainsKey(selectedBOM) Then
                Me.txtBOMTitle.Text = Me.BOMdic.Item(selectedBOM).BOMTitle
                For i As Integer = 0 To Me.BOMdic.Item(selectedBOM).BOMList.Count - 1
                    Dim aPart = Me.BOMdic.Item(selectedBOM).BOMList(i)
                    Me.dgvBOM.Rows.Add(i + 1, aPart.PartID, aPart.QTY, aPart.Manufacturer, aPart.Description, aPart.Note)
                    If Me.BOMdic.Item(selectedBOM).BOMList.Where(Function(p) p.PartID = aPart.PartID).Count > 1 Then
                        Me.dgvBOM.Rows.Item(Me.dgvBOM.RowCount - 1).ErrorText = "Duplicate Part"
                    ElseIf aPart.QTY <= 0 Then
                        Me.dgvBOM.Rows.Item(Me.dgvBOM.RowCount - 1).ErrorText = "Please input Qty number"
                    Else
                        Me.dgvBOM.Rows.Item(Me.dgvBOM.RowCount - 1).ErrorText = ""
                    End If
                Next
            End If
            Me.dgvBOM.Tag = selectedBOM
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub
    Private Sub TreeBOM_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) Handles TreeBOM.BeforeSelect
        If Me.FIRSTTIME Then Exit Sub
        If Me.NeedSave Then
            MessageBox.Show("Current BOM has been changed, please save it frist.")
        End If
        e.Cancel = Me.NeedSave
    End Sub
    Private Sub TreeBOM_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeBOM.AfterSelect
        If Me.FIRSTTIME Then Exit Sub
        Try
            UpdateBOMList()
            UpdateDGVbuttons()
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub btnBOM_Click(sender As Object, e As EventArgs) Handles btnBOM.Click
        If Me.FIRSTTIME Then Exit Sub
        Me.UpdateTreeView()
        Me.splPart.Visible = False
        Me.palSearch.Visible = False
        Me.palBOM.Visible = True
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If Me.FIRSTTIME Then Exit Sub
        Me.splPart.Visible = True
        Me.palSearch.Visible = True
        Me.palBOM.Visible = False
    End Sub

    Private Sub dgvBOM_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBOM.CellEndEdit
        If Me.FIRSTTIME Then Exit Sub
        Dim CurrentBOM As List(Of ECSSBOM) = Nothing
        Try
            If Me.dgvBOM.Tag IsNot Nothing Then
                If Me.BOMdic.ContainsKey(Me.dgvBOM.Tag.ToString) Then
                    CurrentBOM = Me.BOMdic.Item(Me.dgvBOM.Tag.ToString).BOMList
                End If
            End If

            If e.ColumnIndex = 1 Then ' Part ID
                If String.IsNullOrEmpty(dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value) = False Then
                    Dim partid As String = dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                    If CurrentBOM(e.RowIndex).PartID <> partid Then
                        If Me.ECSSParts.PartDic.ContainsKey(partid) Then
                            CurrentBOM(e.RowIndex).PartID = partid
                            dgvBOM.Item(3, e.RowIndex).Value = Me.ECSSParts.PartDic(partid).Manufacturer
                            CurrentBOM(e.RowIndex).Manufacturer = Me.ECSSParts.PartDic(partid).Manufacturer
                            dgvBOM.Item(4, e.RowIndex).Value = Me.ECSSParts.PartDic(partid).Description
                            CurrentBOM(e.RowIndex).Description = Me.ECSSParts.PartDic(partid).Description
                            dgvBOM.Rows(e.RowIndex).ErrorText = ""
                        Else
                            CurrentBOM(e.RowIndex).PartID = partid
                            dgvBOM.Item(3, e.RowIndex).Value = ""
                            CurrentBOM(e.RowIndex).Manufacturer = ""
                            dgvBOM.Item(4, e.RowIndex).Value = ""
                            CurrentBOM(e.RowIndex).Description = ""
                            dgvBOM.Rows(e.RowIndex).ErrorText = "Part ID does not exist!"
                        End If
                        Me.NeedSave = True
                    End If
                End If
            ElseIf e.ColumnIndex = 2 Then ' Qty
                If String.IsNullOrEmpty(dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value) = False Then
                    If IsNumeric(dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value) Then
                        CurrentBOM(e.RowIndex).QTY = CInt(dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value)
                        If CurrentBOM(e.RowIndex).QTY > 0 Then
                            dgvBOM.Rows(e.RowIndex).ErrorText = ""
                        Else
                            dgvBOM.Rows(e.RowIndex).ErrorText = "Please input Qty number."
                        End If
                    End If
                    Me.NeedSave = True
                Else
                    MessageBox.Show("Please input a valid number.")
                    dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value = CurrentBOM(e.RowIndex).QTY
                End If

            ElseIf e.ColumnIndex = 3 Then ' manufacture
                If String.IsNullOrEmpty(dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value) = False Then
                    CurrentBOM(e.RowIndex).Manufacturer = dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                    Me.NeedSave = True
                End If
            ElseIf e.ColumnIndex = 4 Then 'description
                If String.IsNullOrEmpty(dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value) = False Then
                    CurrentBOM(e.RowIndex).Description = dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                    Me.NeedSave = True
                End If
            ElseIf e.ColumnIndex = 5 Then 'Note
                If String.IsNullOrEmpty(dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value) = False Then
                    CurrentBOM(e.RowIndex).Note = dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value.ToString
                    Me.NeedSave = True
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If Me.FIRSTTIME Then Exit Sub
        Try
            For Each aRow As DataGridViewRow In Me.dgvBOM.Rows
                If String.IsNullOrEmpty(aRow.ErrorText) = False Then
                    If MessageBox.Show("There are errors in the BOM list, will you update the BOM anyway?", "BOM Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
                End If
            Next

            If Me.dgvBOM.Tag IsNot Nothing Then
                Dim BOMid As String = Me.dgvBOM.Tag.ToString
                If Me.BOMdic IsNot Nothing AndAlso Me.BOMdic.Count > 0 Then
                    For Each kvp In Me.BOMdic
                        If kvp.Key = BOMid Then
                            If ECSSDBFunctions.InsertOneBOM(kvp.Value, False) Then
                                MessageBox.Show("Update current BOM.")
                                Me.NeedSave = False
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        If Me.FIRSTTIME Then Exit Sub
        'Dim txtFile As System.IO.StreamWriter = Nothing
        Dim OutputFile As String = ""
        Dim pr As Diagnostics.Process = Nothing
        Try
            If Me.dgvBOM.Tag IsNot Nothing Then
                OutputFile = AppDomain.CurrentDomain.BaseDirectory & Me.dgvBOM.Tag.ToString & ".xlsx"

                Dim BOMs = Me.BOMdic.Item(Me.dgvBOM.Tag.ToString)
                Dim excel = New Microsoft.Office.Interop.Excel.Application
                Dim workbook = excel.Workbooks.Add()
                excel.Worksheets.Add()
                Dim slipSheet = DirectCast(excel.Worksheets(1), Microsoft.Office.Interop.Excel.Worksheet)
                slipSheet.Name = "BOM"
                excel.Visible = False

                Dim activeSheet = DirectCast(excel.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)
                DirectCast(activeSheet.Cells(1, 1), Microsoft.Office.Interop.Excel.Range).Value = "BOM Title"
                DirectCast(activeSheet.Cells(1, 1), Microsoft.Office.Interop.Excel.Range).Font.Bold = True
                DirectCast(activeSheet.Cells(1, 2), Microsoft.Office.Interop.Excel.Range).Value = BOMs.BOMTitle
                DirectCast(activeSheet.Cells(1, 2), Microsoft.Office.Interop.Excel.Range).Font.Bold = True

                DirectCast(activeSheet.Cells(2, 1), Microsoft.Office.Interop.Excel.Range).Value = "Num"
                DirectCast(activeSheet.Cells(2, 1), Microsoft.Office.Interop.Excel.Range).Font.Bold = True
                DirectCast(activeSheet.Cells(2, 2), Microsoft.Office.Interop.Excel.Range).Value = "Part ID"
                DirectCast(activeSheet.Cells(2, 2), Microsoft.Office.Interop.Excel.Range).Font.Bold = True
                DirectCast(activeSheet.Cells(2, 3), Microsoft.Office.Interop.Excel.Range).Value = "Qty"
                DirectCast(activeSheet.Cells(2, 3), Microsoft.Office.Interop.Excel.Range).Font.Bold = True
                DirectCast(activeSheet.Cells(2, 4), Microsoft.Office.Interop.Excel.Range).Value = "Manufacturer"
                DirectCast(activeSheet.Cells(2, 4), Microsoft.Office.Interop.Excel.Range).Font.Bold = True
                DirectCast(activeSheet.Cells(2, 5), Microsoft.Office.Interop.Excel.Range).Value = "Description"
                DirectCast(activeSheet.Cells(2, 5), Microsoft.Office.Interop.Excel.Range).Font.Bold = True
                DirectCast(activeSheet.Cells(2, 6), Microsoft.Office.Interop.Excel.Range).Value = "Note"
                DirectCast(activeSheet.Cells(2, 6), Microsoft.Office.Interop.Excel.Range).Font.Bold = True

                Dim row As Integer = 3

                If BOMs.BOMList IsNot Nothing Then
                    For i As Integer = 0 To BOMs.BOMList.Count - 1
                        Dim aPart = BOMs.BOMList(i)
                        DirectCast(activeSheet.Cells(row + i, 1), Microsoft.Office.Interop.Excel.Range).Value = i + 1
                        DirectCast(activeSheet.Cells(row + i, 2), Microsoft.Office.Interop.Excel.Range).Value = aPart.PartID
                        DirectCast(activeSheet.Cells(row + i, 3), Microsoft.Office.Interop.Excel.Range).Value = aPart.QTY
                        DirectCast(activeSheet.Cells(row + i, 4), Microsoft.Office.Interop.Excel.Range).Value = aPart.Manufacturer
                        DirectCast(activeSheet.Cells(row + i, 5), Microsoft.Office.Interop.Excel.Range).Value = aPart.Description
                        DirectCast(activeSheet.Cells(row + i, 6), Microsoft.Office.Interop.Excel.Range).Value = aPart.Note
                    Next
                End If


                DirectCast(activeSheet.Cells(1, 1), Microsoft.Office.Interop.Excel.Range).Activate()
                workbook.SaveAs(OutputFile)
                slipSheet = Nothing
                workbook.Close()
                workbook = Nothing
                excel.Quit()
                excel = Nothing

                If System.IO.File.Exists(OutputFile) Then
                    pr = New Diagnostics.Process
                    pr.StartInfo.WindowStyle = Diagnostics.ProcessWindowStyle.Normal
                    pr.StartInfo.FileName = OutputFile
                    pr.Start()
                End If


            End If

        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        Finally
            'If Not txtFile Is Nothing Then txtFile.Close()
            If Not pr Is Nothing Then pr.Dispose()
        End Try
    End Sub


    Private Sub clbPartType_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbPartType.ItemCheck, clbMaterial.ItemCheck, clbManufacturer.ItemCheck,
        clbCertificates.ItemCheck, clbMount.ItemCheck, clbNEMA.ItemCheck, clbOutputA.ItemCheck,
        clbOutputVol.ItemCheck, clbNormalV.ItemCheck, clbInputPhase.ItemCheck, clbClass.ItemCheck, clbGroup.ItemCheck, clbColor.ItemCheck,
        clbViewAreaH.ItemCheck, clbViewAreaW.ItemCheck, clbFunction.ItemCheck, clbRatedVolMax.ItemCheck, clbRatedCurrent.ItemCheck,
        clbSwitchTempON.ItemCheck, clbSwitchTempOFF.ItemCheck, clbType.ItemCheck, clbFunction.ItemCheck, clbRatedVolMax.ItemCheck,
    clbRatedCurrent.ItemCheck, clbSwitchTempON.ItemCheck, clbSwitchTempOFF.ItemCheck, clbType.ItemCheck, clbRatedVolMin.ItemCheck,
    clbBlockType.ItemCheck, clbContacts.ItemCheck, clbOperaTempMin.ItemCheck, clbOperaTempMax.ItemCheck, clbPower.ItemCheck,
    clbBuiltInThem.ItemCheck, clbBuiltInFan.ItemCheck, clbArea.ItemCheck, clbTempCode.ItemCheck, clbFingersafe.ItemCheck,
    clbPowerModuleType.ItemCheck, clbLampTest.ItemCheck, clbIlluminationOption.ItemCheck, clbVoltageType.ItemCheck, clbVoltage.ItemCheck,
    clbLenColor.ItemCheck
        If Me.FIRSTTIME Then Exit Sub
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim clb As CheckedListBox = DirectCast(sender, CheckedListBox)

            Select Case clb.Name
                Case clbPartType.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.PartType.Contains(ECSSParts.GetPartType(clbPartType.Items(e.Index).ToString)) = False Then Me.ECSSSearch.PartType.Add(ECSSParts.GetPartType(clbPartType.Items(e.Index).ToString))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim t = ECSSParts.GetPartType(clbPartType.Items(e.Index).ToString)
                        If Me.ECSSSearch.PartType.Contains(t) Then Me.ECSSSearch.PartType.Remove(t)
                    End If
                Case clbManufacturer.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Manufacturer.Contains(clbManufacturer.Items(e.Index).ToString) = False Then Me.ECSSSearch.Manufacturer.Add(clbManufacturer.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clbManufacturer.Items(e.Index).ToString
                        If Me.ECSSSearch.Manufacturer.Contains(m) Then Me.ECSSSearch.Manufacturer.Remove(m)
                    End If
                Case clbMaterial.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Material.Contains(clbMaterial.Items(e.Index).ToString) = False Then Me.ECSSSearch.Material.Add(clbMaterial.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clbMaterial.Items(e.Index).ToString
                        If Me.ECSSSearch.Material.Contains(m) Then Me.ECSSSearch.Material.Remove(m)
                    End If
                Case clbCertificates.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Certificates.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.Certificates.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.Certificates.Contains(m) Then Me.ECSSSearch.Certificates.Remove(m)
                    End If
                Case clbMount.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Mount.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.Mount.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.Mount.Contains(m) Then Me.ECSSSearch.Mount.Remove(m)
                    End If
                Case clbNEMA.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.NEMA.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.NEMA.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.NEMA.Contains(m) Then Me.ECSSSearch.NEMA.Remove(m)
                    End If
                Case clbOutputVol.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.outputV.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.outputV.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.outputV.Contains(m) Then Me.ECSSSearch.outputV.Remove(m)
                    End If
                Case clbOutputA.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.outputA.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.outputA.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.outputA.Contains(m) Then Me.ECSSSearch.outputA.Remove(m)
                    End If
                Case clbNormalV.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.NormalV.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.NormalV.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.NormalV.Contains(m) Then Me.ECSSSearch.NormalV.Remove(m)
                    End If
                Case clbInputPhase.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.InputPhase.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.InputPhase.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.InputPhase.Contains(m) Then Me.ECSSSearch.InputPhase.Remove(m)
                    End If
                Case clbClass.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Class.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.Class.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.Class.Contains(m) Then Me.ECSSSearch.Class.Remove(m)
                    End If
                Case clbGroup.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Group.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.Group.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.Group.Contains(m) Then Me.ECSSSearch.Group.Remove(m)
                    End If
                Case clbColor.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Color.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.Color.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.Color.Contains(m) Then Me.ECSSSearch.Color.Remove(m)
                    End If
                Case clbViewAreaH.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.ViewAreaH.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.ViewAreaH.Add(CInt(clb.Items(e.Index)))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m As Integer = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.ViewAreaH.Contains(m) Then Me.ECSSSearch.ViewAreaH.Remove(m)
                    End If
                Case clbViewAreaW.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.ViewAreaW.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.ViewAreaW.Add(CInt(clb.Items(e.Index)))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m As Integer = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.ViewAreaW.Contains(m) Then Me.ECSSSearch.ViewAreaW.Remove(m)
                    End If
                Case clbFunction.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Functions.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.Functions.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.Functions.Contains(m) Then Me.ECSSSearch.Functions.Remove(m)
                    End If
                Case clbRatedVolMax.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.RatedVol.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.RatedVol.Add(CInt(clb.Items(e.Index)))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m As Integer = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.RatedVol.Contains(m) Then Me.ECSSSearch.RatedVol.Remove(m)
                    End If
                Case clbRatedCurrent.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.RatedCurrent.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.RatedCurrent.Add(CInt(clb.Items(e.Index)))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m As Integer = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.RatedCurrent.Contains(m) Then Me.ECSSSearch.RatedCurrent.Remove(m)
                    End If
                Case clbSwitchTempOFF.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.TempSwitchOFF.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.TempSwitchOFF.Add(CInt(clb.Items(e.Index)))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m As Integer = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.TempSwitchOFF.Contains(m) Then Me.ECSSSearch.TempSwitchOFF.Remove(m)
                    End If
                Case clbSwitchTempON.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.TempSwitchON.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.TempSwitchON.Add(CInt(clb.Items(e.Index)))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m As Integer = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.TempSwitchON.Contains(m) Then Me.ECSSSearch.TempSwitchON.Remove(m)
                    End If
                Case clbType.Name
                    If Me.palCate.Tag IsNot Nothing Then
                        If DirectCast(Me.palCate.Tag, ECSSParts.PART_TYPE) = ECSSParts.PART_TYPE.WINDOW_KIT Then
                            If e.NewValue = CheckState.Checked Then
                                If Me.ECSSSearch.WindowType.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.WindowType.Add(clb.Items(e.Index).ToString)
                            ElseIf e.NewValue = CheckState.Unchecked Then
                                Dim m = clb.Items(e.Index).ToString
                                If Me.ECSSSearch.WindowType.Contains(m) Then Me.ECSSSearch.WindowType.Remove(m)
                            End If
                        Else
                            If e.NewValue = CheckState.Checked Then
                                If Me.ECSSSearch.Types.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.Types.Add(clb.Items(e.Index).ToString)
                            ElseIf e.NewValue = CheckState.Unchecked Then
                                Dim m = clb.Items(e.Index).ToString
                                If Me.ECSSSearch.Types.Contains(m) Then Me.ECSSSearch.Types.Remove(m)
                            End If
                        End If
                    End If
                Case clbFunction.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Functions.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.Functions.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.Functions.Contains(m) Then Me.ECSSSearch.Functions.Remove(m)
                    End If
                Case clbRatedVolMax.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.RatedVolMax.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.RatedVolMax.Add(CInt(clb.Items(e.Index)))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m As Integer = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.RatedVolMax.Contains(m) Then Me.ECSSSearch.RatedVolMax.Remove(m)
                    End If
                Case clbRatedVolMin.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.RatedVolMin.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.RatedVolMin.Add(CInt(clb.Items(e.Index)))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m As Integer = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.RatedVolMin.Contains(m) Then Me.ECSSSearch.RatedVolMin.Remove(m)
                    End If
                Case clbSwitchTempON.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.SwitchTempON.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.SwitchTempON.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.SwitchTempON.Contains(m) Then Me.ECSSSearch.SwitchTempON.Remove(m)
                    End If
                Case clbSwitchTempOFF.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.SwitchTempOFF.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.SwitchTempOFF.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.SwitchTempOFF.Contains(m) Then Me.ECSSSearch.SwitchTempOFF.Remove(m)
                    End If
                Case clbBlockType.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.BlockType.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.BlockType.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.BlockType.Contains(m) Then Me.ECSSSearch.BlockType.Remove(m)
                    End If
                Case clbContacts.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Contacts.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.Contacts.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.Contacts.Contains(m) Then Me.ECSSSearch.Contacts.Remove(m)
                    End If
                Case clbOperaTempMin.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.OperaTempMin.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.OperaTempMin.Add(CInt(clb.Items(e.Index)))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m As Integer = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.OperaTempMin.Contains(m) Then Me.ECSSSearch.OperaTempMin.Remove(m)
                    End If
                Case clbOperaTempMax.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.OperaTempMax.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.OperaTempMax.Add(CInt(clb.Items(e.Index)))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m As Integer = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.OperaTempMax.Contains(m) Then Me.ECSSSearch.OperaTempMax.Remove(m)
                    End If
                Case clbPower.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Power.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.Power.Add(CInt(clb.Items(e.Index)))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m As Integer = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.Power.Contains(m) Then Me.ECSSSearch.Power.Remove(m)
                    End If
                Case clbBuiltInThem.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.BuiltInThem.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.BuiltInThem.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.BuiltInThem.Contains(m) Then Me.ECSSSearch.BuiltInThem.Remove(m)
                    End If
                Case clbBuiltInFan.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.BuiltInFan.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.BuiltInFan.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.BuiltInFan.Contains(m) Then Me.ECSSSearch.BuiltInFan.Remove(m)
                    End If
                Case clbArea.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Area.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.Area.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.Area.Contains(m) Then Me.ECSSSearch.Area.Remove(m)
                    End If
                Case clbTempCode.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.TempCode.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.TempCode.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.TempCode.Contains(m) Then Me.ECSSSearch.TempCode.Remove(m)
                    End If
                Case clbFingersafe.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Fingersafe.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.Fingersafe.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.Fingersafe.Contains(m) Then Me.ECSSSearch.Fingersafe.Remove(m)
                    End If
                Case clbPowerModuleType.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.PowerModuleType.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.PowerModuleType.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.PowerModuleType.Contains(m) Then Me.ECSSSearch.PowerModuleType.Remove(m)
                    End If
                Case clbLampTest.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.LampTest.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.LampTest.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.LampTest.Contains(m) Then Me.ECSSSearch.LampTest.Remove(m)
                    End If
                Case clbIlluminationOption.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.IlluminationOption.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.IlluminationOption.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.IlluminationOption.Contains(m) Then Me.ECSSSearch.IlluminationOption.Remove(m)
                    End If
                Case clbVoltageType.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.VoltageType.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.VoltageType.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.VoltageType.Contains(m) Then Me.ECSSSearch.VoltageType.Remove(m)
                    End If
                Case clbVoltage.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Voltage.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.Voltage.Add(CInt(clb.Items(e.Index)))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m As Integer = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.Voltage.Contains(m) Then Me.ECSSSearch.Voltage.Remove(m)
                    End If
                Case clbLenColor.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.LensColor.Contains(clb.Items(e.Index).ToString) = False Then Me.ECSSSearch.LensColor.Add(clb.Items(e.Index).ToString)
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = clb.Items(e.Index).ToString
                        If Me.ECSSSearch.LensColor.Contains(m) Then Me.ECSSSearch.LensColor.Remove(m)
                    End If
            End Select

            Me.LoadlstPart()
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub btnClean_Click(sender As Object, e As EventArgs) Handles btnClean.Click
        If Me.FIRSTTIME Then Exit Sub
        Try
            Me.lstPart.Items.Clear()
            Me.ECSSSearch = New ECSSSearchCriteria
            Me.UnselectAllFilters()
            Me.UpdateDetail(Nothing, True)
            Me.ResetAllFilters()
            Me.SetSmallSize()
            Me.lblFilterDisplay.Text = ""
            Me.lblError.Visible = False
            Me.lblTotal.Text = ""
            Me.txtSearch.Text = ""
            Me.txtSearch.SelectAll()
            Me.txtSearch.Focus()
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Protected Overrides Function ProcessKeyPreview(ByRef m As System.Windows.Forms.Message) As Boolean
        If m.Msg = &H100 Then  'WM_KEYDOWN
            Dim key As Keys = m.WParam
            If key = Keys.F5 Then
                'DO stuff
                Me.btnClean_Click(Me.btnClean, Nothing)
                Return True
            End If
        End If

        Return MyBase.ProcessKeyPreview(m)
    End Function

#Region "BOM"
    Private Sub btnAddOnePart_Click(sender As Object, e As EventArgs) Handles btnAddOnePart.Click
        If Me.FIRSTTIME Then Exit Sub
        Dim selectedBOM As String = dgvBOM.Tag.ToString
        If Me.BOMdic.ContainsKey(selectedBOM) Then
            Dim aList = Me.BOMdic.Item(selectedBOM)
            Dim apart As New ECSSBOM
            apart.BOMID = aList.BOMID
            aList.BOMList.Add(apart)
            UpdateBOMList()
            UpdateDGVbuttons()
            Me.NeedSave = True
        End If
    End Sub

    Private Sub btnDelOnePart_Click(sender As Object, e As EventArgs) Handles btnDelOnePart.Click
        If Me.FIRSTTIME Then Exit Sub
        Dim selectedBOM As String = dgvBOM.Tag.ToString
        If dgvBOM.SelectedRows.Count = 0 Then Exit Sub
        If Me.BOMdic.ContainsKey(selectedBOM) Then
            Dim aList = Me.BOMdic.Item(selectedBOM)
            Dim index = Me.dgvBOM.SelectedRows(0).Index
            If index < aList.BOMList.Count Then
                aList.BOMList.RemoveAt(index)
            End If
            UpdateBOMList()
            UpdateDGVbuttons()
            Me.NeedSave = True
        End If
    End Sub

    Private Sub btnTop_Click(sender As Object, e As EventArgs) Handles btnTop.Click
        If Me.FIRSTTIME Then Exit Sub
        Dim selectedBOM As String = dgvBOM.Tag.ToString
        If dgvBOM.SelectedRows.Count = 0 Then Exit Sub
        If Me.BOMdic.ContainsKey(selectedBOM) Then
            Dim aList = Me.BOMdic.Item(selectedBOM)
            Dim index = Me.dgvBOM.SelectedRows(0).Index
            Dim apart = aList.BOMList(index)
            aList.BOMList.RemoveAt(index)
            aList.BOMList.Insert(0, apart)
            UpdateBOMList()
            dgvBOM.ClearSelection()
            dgvBOM.Rows(0).Selected = True
            Me.NeedSave = True
        End If
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        If Me.FIRSTTIME Then Exit Sub
        Dim selectedBOM As String = dgvBOM.Tag.ToString
        If dgvBOM.SelectedRows.Count = 0 Then Exit Sub
        If Me.BOMdic.ContainsKey(selectedBOM) Then
            Dim aList = Me.BOMdic.Item(selectedBOM)
            Dim index = Me.dgvBOM.SelectedRows(0).Index
            Dim apart = aList.BOMList(index)
            aList.BOMList.RemoveAt(index)
            aList.BOMList.Insert(index - 1, apart)
            UpdateBOMList()
            dgvBOM.ClearSelection()
            dgvBOM.Rows(index - 1).Selected = True
            Me.NeedSave = True
        End If
    End Sub
    Private Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click
        If Me.FIRSTTIME Then Exit Sub
        Dim selectedBOM As String = dgvBOM.Tag.ToString
        If dgvBOM.SelectedRows.Count = 0 Then Exit Sub
        If Me.BOMdic.ContainsKey(selectedBOM) Then
            Dim aList = Me.BOMdic.Item(selectedBOM)
            Dim index = Me.dgvBOM.SelectedRows(0).Index
            Dim apart = aList.BOMList(index)
            aList.BOMList.RemoveAt(index)
            aList.BOMList.Insert(index + 1, apart)
            UpdateBOMList()
            dgvBOM.ClearSelection()
            dgvBOM.Rows(index + 1).Selected = True
            Me.NeedSave = True
        End If
    End Sub

    Private Sub btnBottom_Click(sender As Object, e As EventArgs) Handles btnBottom.Click
        If Me.FIRSTTIME Then Exit Sub
        Dim selectedBOM As String = dgvBOM.Tag.ToString
        If dgvBOM.SelectedRows.Count = 0 Then Exit Sub
        If Me.BOMdic.ContainsKey(selectedBOM) Then
            Dim aList = Me.BOMdic.Item(selectedBOM)
            Dim index = Me.dgvBOM.SelectedRows(0).Index
            Dim apart = aList.BOMList(index)
            aList.BOMList.RemoveAt(index)
            aList.BOMList.Add(apart)
            UpdateBOMList()
            dgvBOM.ClearSelection()
            dgvBOM.Rows(dgvBOM.RowCount - 1).Selected = True
            Me.NeedSave = True
        End If
    End Sub

    Private Sub dgvBOM_SelectionChanged(sender As Object, e As EventArgs) Handles dgvBOM.SelectionChanged
        If Me.FIRSTTIME Then Exit Sub
        Me.UpdateDGVbuttons()
    End Sub

    Private Sub UpdateDGVbuttons()
        Me.btnAddOnePart.Enabled = Enabled
        If Me.dgvBOM.SelectedRows.Count = 0 Then
            Me.btnDown.Enabled = False
            Me.btnUp.Enabled = False
            Me.btnDown.Enabled = False
            Me.btnTop.Enabled = False
            Me.btnBottom.Enabled = False
        Else
            Me.btnDown.Enabled = True
            Me.btnUp.Enabled = (Me.dgvBOM.SelectedRows(0).Index > 0)
            Me.btnDown.Enabled = (Me.dgvBOM.SelectedRows(0).Index < (Me.dgvBOM.RowCount - 1))
            Me.btnTop.Enabled = (Me.dgvBOM.SelectedRows(0).Index > 0)
            Me.btnBottom.Enabled = (Me.dgvBOM.SelectedRows(0).Index < (Me.dgvBOM.RowCount - 1))
        End If
    End Sub

    Private Sub txtBOMTitle_Validating(sender As Object, e As CancelEventArgs) Handles txtBOMTitle.Validating
        If Me.FIRSTTIME Then Exit Sub
        Try
            Dim str As String = Me.txtBOMTitle.Text.Trim
            Dim selectedBOM As String = dgvBOM.Tag.ToString
            If dgvBOM.SelectedRows.Count = 0 Then Exit Sub
            If Me.BOMdic.ContainsKey(selectedBOM) Then
                Dim aList = Me.BOMdic.Item(selectedBOM)

                If String.IsNullOrEmpty(str) Then
                    MessageBox.Show("BOM Title cannot be empty!", "BOM Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.txtBOMTitle.Text = aList.BOMTitle
                    Me.txtBOMTitle.SelectAll()
                    Me.txtBOMTitle.Focus()
                    Exit Sub
                End If

                If Me.BOMdic.Where(Function(k) k.Value.BOMTitle.ToUpper = str.ToUpper).Count > 0 Then
                    MessageBox.Show("BOM Title already exists, please choose a different title!", "BOM Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.txtBOMTitle.Text = aList.BOMTitle
                    Me.txtBOMTitle.SelectAll()
                    Me.txtBOMTitle.Focus()
                    Exit Sub
                End If

                aList.BOMTitle = str
                Me.TreeBOM.SelectedNode.Text = str
                Me.NeedSave = True
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub
#End Region

    Private Sub frmMain_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Me.Height <> SmallSizeHeight Then
            Dim msg = MessageBox.Show("All unsaved BOM will be lost, do you still want to close the application?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            e.Cancel = (msg = DialogResult.No)
        End If
    End Sub

    Private Sub btnSetting_Click(sender As Object, e As EventArgs) Handles btnSetting.Click
        Try
            Dim frm As New frmGeneralSettings
            frm.ShowDialog(Me)
            frm.Dispose()

            If GlobalSettings.MultiKeywords Then
                Me.txtSearch.Visible = False
                Me.txtSearchMulti.Visible = True
            Else
                Me.txtSearch.Visible = True
                Me.txtSearchMulti.Visible = False
            End If

            Me.TopMost = GlobalSettings.AlwaysOnTop

        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

#Region "Context Menu"


    Private Sub lstProject_MouseUp(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles lstPart.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.SetContextMenu(e)
            Me.MyContextMenu.Show(lstPart, New Point(e.X, e.Y))
        End If
    End Sub


    Private Sub SetContextMenu(ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            Me.MyContextMenu.Items.Clear()
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim myItem As System.Windows.Forms.ToolStripMenuItem = Nothing
                Dim subItem As System.Windows.Forms.ToolStripMenuItem = Nothing
                Dim mnuSeparator As System.Windows.Forms.ToolStripSeparator = Nothing

                Me.AddOneMenuItem(myItem, "Add to BOM", Nothing, Color.Magenta)
                AddHandler myItem.Click, AddressOf Me.menuAddBOM_Click
                myItem.Enabled = True
                Me.MyContextMenu.Items.Add(myItem)
            End If


        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub AddOneMenuItem(ByRef myItem As System.Windows.Forms.ToolStripMenuItem, ByVal strText As String, ByVal theImage As System.Drawing.Image, ByVal tColor As Color, Optional ByVal tag As Object = Nothing)
        myItem = New System.Windows.Forms.ToolStripMenuItem
        myItem.Text = strText
        If Not theImage Is Nothing Then myItem.Image = theImage
        myItem.ImageTransparentColor = tColor
        If tag IsNot Nothing Then myItem.Tag = tag
    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDetail.CellClick

        If Me.dgvDetail.CurrentCell.GetType.Name = GetType(DataGridViewButtonCell).Name Then
            Me.menuAddBOM_Click(sender, Nothing)
        End If
    End Sub


    Private Sub menuAddBOM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim aBOMList As OneBOMList = Nothing
        Dim qty As Integer
        If Me.FIRSTTIME Then Exit Sub
        Try
            Dim frm As New frmAddBOM(Me.BOMdic)
            If frm.ShowDialog = DialogResult.OK Then
                aBOMList = frm.CurrentBOM
                qty = frm.QTY
                If Me.BOMdic.ContainsKey(aBOMList.BOMID) Then
                    If Me.lstPart.SelectedItems IsNot Nothing AndAlso Me.lstPart.SelectedItems.Count > 0 Then
                        Dim aItem As ListViewItem = Me.lstPart.SelectedItems(0)
                        If aItem IsNot Nothing AndAlso aItem.Tag IsNot Nothing Then
                            If Me.ECSSParts.PartDic.ContainsKey(aItem.Tag.ToString) Then
                                Dim aPart = Me.ECSSParts.PartDic.Item(aItem.Tag.ToString)
                                Dim aBOM = New ECSSBOM
                                aBOM.BOMID = aBOMList.BOMID
                                aBOM.QTY = qty
                                aBOM.PartID = aPart.PartID
                                aBOM.Manufacturer = aPart.Manufacturer
                                aBOM.Description = aPart.Description
                                aBOMList.BOMList.Add(aBOM)
                                If ECSSDBFunctions.InsertOneBOM(Me.BOMdic.Item(aBOM.BOMID), False) Then
                                    MessageBox.Show("Add one part into BOM.")
                                End If
                            End If
                        End If
                    End If
                End If
            Else

            End If
            frm.Dispose()
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub



#End Region


#Region "Filter"

    Private Sub UnselectAllFilters()
        Me.FIRSTTIME = True
        Try
            For Each ctrl As Control In Me.palFilter.Controls
                If TypeOf ctrl Is Panel Then
                    For Each c In ctrl.Controls
                        If TypeOf c Is CheckedListBox Then
                            Dim clb = DirectCast(c, CheckedListBox)
                            If clb IsNot Nothing AndAlso clb.Items.Count > 0 Then
                                For i As Integer = 0 To clb.Items.Count - 1
                                    clb.SetItemChecked(i, False)
                                Next
                            End If
                        End If
                    Next
                End If
            Next

            Me.Updatefilters(Nothing)
            Me.dtbDepth.Reset()
            Me.dtbHeight.Reset()
            Me.dtbWidth.Reset()
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        Finally
            Me.FIRSTTIME = False
        End Try
    End Sub
    Private Sub ResetAllFilters()
        Me.palCate.Tag = Nothing
        Me.palCate.Visible = True
        Me.palManufacturer.Visible = True
        Me.palCertificates.Visible = True

        Me.palMaterial.Visible = False
        Me.palHeight.Visible = False
        Me.palWidth.Visible = False
        Me.palDepth.Visible = False
        Me.palMount.Visible = False
        Me.palNEMA.Visible = False
        Me.palOutputV.Visible = False
        Me.palOutputA.Visible = False
        Me.palNormalV.Visible = False
        Me.palInputPhase.Visible = False
        Me.palClass.Visible = False
        Me.palGroup.Visible = False
        Me.palColor.Visible = False
        Me.palType.Visible = False
        Me.palSwitchTempOFF.Visible = False
        Me.palSwitchTempON.Visible = False
        Me.palRatedCurrent.Visible = False
        Me.palRatedVolMax.Visible = False
        Me.palFunction.Visible = False
        Me.palViewAreaW.Visible = False
        Me.palViewAreaH.Visible = False
        Me.palFunction.Visible = False
        Me.palRatedVolMax.Visible = False
        Me.palRatedCurrent.Visible = False
        Me.palSwitchTempON.Visible = False
        Me.palSwitchTempOFF.Visible = False
        Me.palType.Visible = False
        Me.palRatedVolMin.Visible = False
        Me.palBlockType.Visible = False
        Me.palContacts.Visible = False
        Me.palOperaTempMin.Visible = False
        Me.palOperaTempMax.Visible = False
        Me.palPower.Visible = False
        Me.palBuiltInThem.Visible = False
        Me.palBuiltInFan.Visible = False
        Me.palArea.Visible = False
        Me.palTempCode.Visible = False
        Me.palFingersafe.Visible = False
        Me.palPowerModuleType.Visible = False
        Me.palLampTest.Visible = False
        Me.palIlluminationOption.Visible = False
        Me.palVoltageType.Visible = False
        Me.palVoltage.Visible = False
        Me.palLenColor.Visible = False

    End Sub


    Private Sub Updatefilters(ByVal TempPartList As List(Of ECSSParts.OnePart))
        If Me.ECSSParts Is Nothing Then Exit Sub
        Try

            Me.lblFilterDepth.Text = "Depth (mm)"
            Me.lblFilterWidth.Text = "Width (mm)"
            Me.lblFilterHeight.Text = "Height (mm)"

            Dim Plist = Me.GetPartTypeList(TempPartList)

            'PART TYPE
            Dim ptlist = From aPart In Me.ECSSParts.PartDic Group By ptype = aPart.Value.PartType Into itemlst = Group, Count()
                         Order By ptype
            If ptlist IsNot Nothing AndAlso ptlist.Count > 0 Then
                Me.clbPartType.Items.Clear()
                For Each pt In ptlist
                    Me.clbPartType.Items.Add(ECSSParts.GetTypeName(pt.ptype))
                    If Me.ECSSSearch.PartType.Contains(pt.ptype) Then
                        Me.clbPartType.SetItemChecked(Me.clbPartType.Items.Count - 1, True)
                    End If
                Next
            End If

            'MANUFACTURER
            Dim mflist As List(Of String) = Nothing
            If TempPartList Is Nothing OrElse Plist.Count > 1 Then
                mflist = (From aPart In Me.ECSSParts.PartDic Group By mf = aPart.Value.Manufacturer Into itemlst = Group, Count()
                          Order By mf).Select(Function(p) p.mf.ToString).ToList
            Else
                Dim PartList = Me.ECSSParts.PartDic.Where(Function(p) p.Value.PartType = Plist(0)).Select(Function(p) p.Value).ToList
                mflist = (From aPart In PartList Group By mf = aPart.Manufacturer Into itemlst = Group, Count()
                          Order By mf).Select(Function(p) p.mf.ToString).ToList
            End If

            If mflist IsNot Nothing AndAlso mflist.Count > 0 Then
                Me.clbManufacturer.Items.Clear()
                For Each m In mflist
                    Me.clbManufacturer.Items.Add(m)
                    If Me.ECSSSearch.Manufacturer.Contains(m) Then
                        Me.clbManufacturer.SetItemChecked(Me.clbManufacturer.Items.Count - 1, True)
                    End If
                Next
            End If

            Dim crList As New List(Of String)

            If TempPartList Is Nothing OrElse Plist.Count > 1 Then
                For Each kvp In Me.ECSSParts.PartDic
                    Dim aPart = kvp.Value
                    'Certificate
                    For Each Str As String In aPart.Certificates
                        If crList.Contains(Str) = False AndAlso String.IsNullOrEmpty(Str.Trim) = False Then crList.Add(Str)
                    Next
                Next
            Else
                Dim PartList = Me.ECSSParts.PartDic.Where(Function(p) p.Value.PartType = Plist(0)).Select(Function(p) p.Value).ToList
                For Each aPart In PartList
                    'Certificate
                    For Each Str As String In aPart.Certificates
                        If crList.Contains(Str) = False AndAlso String.IsNullOrEmpty(Str.Trim) = False Then crList.Add(Str)
                    Next
                Next
            End If

            'Update the checkbox combo 
            Me.clbCertificates.Items.Clear()
            crList = crList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            Me.clbCertificates.Items.AddRange(crList.ToArray)
            Me.UpdateCheckList(Me.clbCertificates, Me.ECSSSearch.Certificates)
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub UpdateFiltersPerType(ByVal CurrentType As ECSSParts.PART_TYPE)

        Dim crList, mList, MountList, NElist, OVList, OAList, NVList, IPList, Clist, GList, tyList, FList As New List(Of String)
        Dim hlist, Wlist, Dlist, VHList, VWList, RVMList, RCList, TSOList, TSFList As New List(Of Integer)
        Dim PartList As List(Of ECSSParts.OnePart)
        If CurrentType = ECSSParts.PART_TYPE.NONE Then Exit Sub
        Try
            PartList = Me.ECSSParts.PartDic.Where(Function(p) p.Value.PartType = CurrentType).Select(Function(p) p.Value).ToList
            If PartList Is Nothing OrElse PartList.Count = 0 Then Exit Sub
            If PartList IsNot Nothing Then
                Dim ptlist = From aPart In PartList Group By ptype = aPart.PartType Into itemlst = Group, Count()
                             Order By ptype
                If ptlist Is Nothing OrElse ptlist.Count <> 1 Then Exit Sub

                Me.FIRSTTIME = True
                If Me.palCate.Tag Is Nothing OrElse DirectCast(Me.palCate.Tag, ECSSParts.PART_TYPE) <> CurrentType Then
                    Me.palCate.Tag = CurrentType
                    Select Case CurrentType
                        Case ECSSParts.PART_TYPE.BREATHER_DRAIN
                            mList = (From aPart In PartList Group By g = aPart.aBreatherDrain.Material Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g.ToString).ToList

                            Me.clbMaterial.Items.Clear()
                            mList = mList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            mList.Sort()
                            Me.clbMaterial.Items.AddRange(mList.ToArray)
                            Me.UpdateCheckList(Me.clbMaterial, Me.ECSSSearch.Material)

                        Case ECSSParts.PART_TYPE.SERVIT_POST
                            mList = (From aPart In PartList Group By g = aPart.aServitPost.Material Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g.ToString).ToList

                            Me.clbMaterial.Items.Clear()
                            mList = mList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            mList.Sort()
                            Me.clbMaterial.Items.AddRange(mList.ToArray)
                            Me.UpdateCheckList(Me.clbMaterial, Me.ECSSSearch.Material)

                        Case ECSSParts.PART_TYPE.POWER_SUPPLY
                            OVList = (From aPart In PartList Group By g = aPart.aPowerSupply.Output_Vol Into itemlst = Group, Count()
                                      Order By g).Select(Function(p) p.g.ToString).ToList

                            OAList = (From aPart In PartList Group By g = aPart.aPowerSupply.Output_Current Into itemlst = Group, Count()
                                      Order By g).Select(Function(p) p.g.ToString).ToList
                            NVList = (From aPart In PartList Group By g = aPart.aPowerSupply.Normal_Vol_Min_AC Into itemlst = Group, Count()
                                      Order By g).Select(Function(p) p.g.ToString).ToList

                            IPList = (From aPart In PartList Group By g = aPart.aPowerSupply.Input_Phase Into itemlst = Group, Count()
                                      Order By g).Select(Function(p) p.g.ToString).ToList
                            Clist = (From aPart In PartList Group By g = aPart.aPowerSupply.SupplyClass Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g.ToString).ToList
                            GList = (From aPart In PartList Group By g = aPart.aPowerSupply.Gas_Group Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g.ToString).ToList

                            Me.clbOutputVol.Items.Clear()
                            OVList = OVList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            OVList.Sort()
                            Me.clbOutputVol.Items.AddRange(OVList.ToArray)
                            Me.UpdateCheckList(Me.clbOutputVol, Me.ECSSSearch.outputV)

                            Me.clbOutputA.Items.Clear()
                            OAList = OAList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            OAList.Sort()
                            Me.clbOutputA.Items.AddRange(OAList.ToArray)
                            Me.UpdateCheckList(Me.clbOutputA, Me.ECSSSearch.outputA)

                            Me.clbNormalV.Items.Clear()
                            NVList = NVList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            NVList.Sort()
                            Me.clbNormalV.Items.AddRange(NVList.ToArray)
                            Me.UpdateCheckList(Me.clbNormalV, Me.ECSSSearch.NormalV)

                            Me.clbInputPhase.Items.Clear()
                            IPList = IPList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            IPList.Sort()
                            Me.clbInputPhase.Items.AddRange(IPList.ToArray)
                            Me.UpdateCheckList(Me.clbInputPhase, Me.ECSSSearch.InputPhase)

                            Me.clbClass.Items.Clear()
                            Clist = Clist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbClass.Items.AddRange(Clist.ToArray)
                            Me.UpdateCheckList(Me.clbClass, Me.ECSSSearch.Class)

                            Me.clbGroup.Items.Clear()
                            GList = GList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbGroup.Items.AddRange(GList.ToArray)
                            Me.UpdateCheckList(Me.clbGroup, Me.ECSSSearch.Group)

                            Me.palOutputV.Visible = True
                            Me.palOutputA.Visible = True
                            Me.palNormalV.Visible = True
                            Me.palInputPhase.Visible = True
                            Me.palClass.Visible = True
                            Me.palGroup.Visible = True

                        Case ECSSParts.PART_TYPE.ENCLOSURE
                            mList = (From aPart In PartList Group By g = aPart.aEnclosure.Material Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g).ToList

                            hlist = (From aPart In PartList Group By g = aPart.Height Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) CInt(p.g)).ToList
                            Wlist = (From aPart In PartList Group By g = aPart.Width Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) CInt(p.g)).ToList
                            Dlist = (From aPart In PartList Group By g = aPart.Depth Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) CInt(p.g)).ToList
                            MountList = (From aPart In PartList Group By g = aPart.aEnclosure.MountType Into itemlst = Group, Count()
                                         Order By g).Select(Function(p) p.g).ToList

                            For Each aPart In PartList
                                If aPart.aEnclosure IsNot Nothing AndAlso aPart.aEnclosure.NEMA_Type IsNot Nothing Then
                                    For Each str As String In aPart.aEnclosure.NEMA_Type
                                        If NElist.Contains(str) = Nothing AndAlso String.IsNullOrEmpty(str.Trim) = False Then NElist.Add(str)
                                    Next
                                End If
                            Next

                            Me.clbMaterial.Items.Clear()
                            mList = mList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbMaterial.Items.AddRange(mList.ToArray)
                            Me.UpdateCheckList(Me.clbMaterial, Me.ECSSSearch.Material)


                            hlist = hlist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.dtbHeight.Min = hlist.Min : Me.dtbHeight.SelectedMin = hlist.Min
                            Me.dtbHeight.Max = hlist.Max : Me.dtbHeight.SelectedMax = hlist.Max

                            Wlist = Wlist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.dtbWidth.Min = Wlist.Min : Me.dtbWidth.SelectedMin = Wlist.Min
                            Me.dtbWidth.Max = Wlist.Max : Me.dtbWidth.SelectedMax = Wlist.Max

                            Dlist = Dlist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.dtbDepth.Min = Dlist.Min : Me.dtbDepth.SelectedMin = Dlist.Min
                            Me.dtbDepth.Max = Dlist.Max : Me.dtbDepth.SelectedMax = Dlist.Max

                            Me.clbMount.Items.Clear()
                            MountList = MountList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbMount.Items.AddRange(MountList.ToArray)
                            Me.UpdateCheckList(Me.clbMount, Me.ECSSSearch.Mount)

                            Me.clbNEMA.Items.Clear()
                            NElist = NElist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbNEMA.Items.AddRange(NElist.ToArray)
                            Me.UpdateCheckList(Me.clbNEMA, Me.ECSSSearch.NEMA)

                            Me.palHeight.Visible = True
                            Me.palWidth.Visible = True
                            Me.palDepth.Visible = True
                            Me.palMount.Visible = True
                            Me.palNEMA.Visible = True

                        Case ECSSParts.PART_TYPE.WINDOW_KIT

                            mList = (From aPart In PartList Group By g = aPart.aWindowKit.Material Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g).ToList

                            tyList = (From aPart In PartList Group By g = aPart.aWindowKit.Kit_Type Into itemlst = Group, Count()
                                      Order By g).Select(Function(p) p.g).ToList

                            Clist = (From aPart In PartList Group By g = aPart.aWindowKit.Color Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g).ToList

                            VHList = (From aPart In PartList Group By g = aPart.aWindowKit.ViewHeight Into itemlst = Group, Count()
                                      Order By g).Select(Function(p) p.g).ToList

                            VWList = (From aPart In PartList Group By g = aPart.aWindowKit.ViewWidth Into itemlst = Group, Count()
                                      Order By g).Select(Function(p) p.g).ToList

                            For Each aPart In PartList
                                If aPart.aWindowKit IsNot Nothing AndAlso aPart.aWindowKit.NEMA_Type IsNot Nothing Then
                                    For Each str As String In aPart.aWindowKit.NEMA_Type
                                        If NElist.Contains(str) = Nothing AndAlso String.IsNullOrEmpty(str.Trim) = False Then NElist.Add(str)
                                    Next
                                End If
                            Next

                            Me.clbMaterial.Items.Clear()
                            mList = mList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbMaterial.Items.AddRange(mList.ToArray)
                            Me.palMaterial.Visible = True
                            Me.UpdateCheckList(Me.clbMaterial, Me.ECSSSearch.Material)

                            Me.clbNEMA.Items.Clear()
                            NElist = NElist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbNEMA.Items.AddRange(NElist.ToArray)
                            Me.palNEMA.Visible = True
                            Me.UpdateCheckList(Me.clbNEMA, Me.ECSSSearch.NEMA)

                            Me.clbType.Items.Clear()
                            tyList = tyList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbType.Items.AddRange(tyList.ToArray)
                            Me.palType.Visible = True
                            Me.UpdateCheckList(Me.clbType, Me.ECSSSearch.Types)

                            Me.clbColor.Items.Clear()
                            Clist = Clist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbColor.Items.AddRange(Clist.ToArray)
                            Me.palColor.Visible = True
                            Me.UpdateCheckList(Me.clbColor, Me.ECSSSearch.Color)

                            Me.clbViewAreaH.Items.Clear()
                            For Each i As Integer In VHList
                                Me.clbViewAreaH.Items.Add(i)
                            Next
                            Me.palViewAreaH.Visible = True
                            Me.UpdateCheckList(Me.clbViewAreaH, Me.ECSSSearch.ViewAreaH)

                            Me.clbViewAreaW.Items.Clear()
                            For Each i As Integer In VWList
                                Me.clbViewAreaW.Items.Add(i)
                            Next
                            Me.palViewAreaW.Visible = True
                            Me.UpdateCheckList(Me.clbViewAreaW, Me.ECSSSearch.ViewAreaW)

                        Case ECSSParts.PART_TYPE.THEROMOSTAT

                            RVMList = (From aPart In PartList Group By g = aPart.aTheromostat.Rated_Vol_Max Into itemlst = Group, Count()
                                       Order By g).Select(Function(p) p.g).ToList
                            RCList = (From aPart In PartList Group By g = aPart.aTheromostat.Rated_Current Into itemlst = Group, Count()
                                      Order By g).Select(Function(p) p.g).ToList
                            FList = (From aPart In PartList Group By g = aPart.aTheromostat.TheroFunction Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g).ToList
                            Clist = (From aPart In PartList Group By g = aPart.aTheromostat.Area_Class Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g).ToList

                            Me.clbRatedVolMax.Items.Clear()
                            For Each i As Integer In RVMList
                                Me.clbRatedVolMax.Items.Add(i)
                            Next
                            Me.palRatedVolMax.Visible = True
                            Me.UpdateCheckList(Me.clbRatedVolMax, Me.ECSSSearch.RatedVolMax)

                            Me.clbRatedCurrent.Items.Clear()
                            For Each i As Integer In RCList
                                Me.clbRatedCurrent.Items.Add(i)
                            Next
                            Me.palRatedCurrent.Visible = True
                            Me.UpdateCheckList(Me.clbRatedCurrent, Me.ECSSSearch.RatedCurrent)

                            Me.clbFunction.Items.Clear()
                            FList = FList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbFunction.Items.AddRange(FList.ToArray)
                            Me.palFunction.Visible = True
                            Me.UpdateCheckList(Me.clbFunction, Me.ECSSSearch.Functions)

                            Me.clbClass.Items.Clear()
                            Clist = Clist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbClass.Items.AddRange(Clist.ToArray)
                            Me.palClass.Visible = True
                            Me.UpdateCheckList(Me.clbClass, Me.ECSSSearch.Class)

                        Case ECSSParts.PART_TYPE.TEMP_SWITCH

                            RVMList = (From aPart In PartList Group By g = aPart.aTempSwitch.Rated_Vol_Max Into itemlst = Group, Count()
                                       Order By g).Select(Function(p) p.g).ToList
                            RCList = (From aPart In PartList Group By g = aPart.aTempSwitch.Rated_Current Into itemlst = Group, Count()
                                      Order By g).Select(Function(p) p.g).ToList
                            FList = (From aPart In PartList Group By g = aPart.aTempSwitch.SwitchFunction Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g).ToList
                            Clist = (From aPart In PartList Group By g = aPart.aTempSwitch.Area_Class Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g).ToList
                            TSOList = (From aPart In PartList Group By g = aPart.aTempSwitch.Switch_Temp_ON Into itemlst = Group, Count()
                                       Order By g).Select(Function(p) p.g).ToList
                            TSFList = (From aPart In PartList Group By g = aPart.aTempSwitch.Switch_Temp_OFF Into itemlst = Group, Count()
                                       Order By g).Select(Function(p) p.g).ToList

                            Me.clbRatedVolMax.Items.Clear()
                            For Each i As Integer In RVMList
                                Me.clbRatedVolMax.Items.Add(i)
                            Next
                            Me.palRatedVolMax.Visible = True
                            Me.UpdateCheckList(Me.clbRatedVolMax, Me.ECSSSearch.RatedVolMax)

                            Me.clbRatedCurrent.Items.Clear()
                            For Each i As Integer In RCList
                                Me.clbRatedCurrent.Items.Add(i)
                            Next
                            Me.palRatedCurrent.Visible = True
                            Me.UpdateCheckList(Me.clbRatedCurrent, Me.ECSSSearch.RatedCurrent)

                            Me.clbSwitchTempON.Items.Clear()
                            For Each i As Integer In TSOList
                                Me.clbSwitchTempON.Items.Add(i)
                            Next
                            Me.palSwitchTempON.Visible = True
                            Me.UpdateCheckList(Me.clbSwitchTempON, Me.ECSSSearch.SwitchTempON)

                            Me.clbSwitchTempOFF.Items.Clear()
                            For Each i As Integer In TSFList
                                Me.clbSwitchTempOFF.Items.Add(i)
                            Next
                            Me.palSwitchTempOFF.Visible = True
                            Me.UpdateCheckList(Me.clbSwitchTempOFF, Me.ECSSSearch.SwitchTempOFF)

                            Me.clbFunction.Items.Clear()
                            FList = FList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbFunction.Items.AddRange(FList.ToArray)
                            Me.palFunction.Visible = True
                            Me.UpdateCheckList(Me.clbFunction, Me.ECSSSearch.Functions)

                            Me.clbClass.Items.Clear()
                            Clist = Clist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbClass.Items.AddRange(Clist.ToArray)
                            Me.palClass.Visible = True
                            Me.UpdateCheckList(Me.clbClass, Me.ECSSSearch.Class)

                        Case ECSSParts.PART_TYPE.PILOT_LIGHT
                            For Each aPart In PartList
                                If aPart.aPilotLight IsNot Nothing AndAlso aPart.aPilotLight.NEMA_TYPE IsNot Nothing Then
                                    For Each str As String In aPart.aPilotLight.NEMA_TYPE
                                        If NElist.Contains(str) = Nothing AndAlso String.IsNullOrEmpty(str.Trim) = False Then NElist.Add(str)
                                    Next
                                End If
                            Next

                            Dim FSList = (From aPart In PartList Group By g = aPart.aPilotLight.Fingersafe Into itemlst = Group, Count()
                                          Order By g).Select(Function(p) p.g).ToList
                            Dim PMTList = (From aPart In PartList Group By g = aPart.aPilotLight.Power_Module Into itemlst = Group, Count()
                                           Order By g).Select(Function(p) p.g).ToList
                            Dim LTOlist = (From aPart In PartList Group By g = aPart.aPilotLight.Lamp_Test Into itemlst = Group, Count()
                                           Order By g).Select(Function(p) p.g).ToList
                            Dim IOList = (From aPart In PartList Group By g = aPart.aPilotLight.Illumination Into itemlst = Group, Count()
                                          Order By g).Select(Function(p) p.g).ToList
                            Dim VTList = (From aPart In PartList Group By g = aPart.aPilotLight.Voltage_Type Into itemlst = Group, Count()
                                          Order By g).Select(Function(p) p.g).ToList
                            Dim VList = (From aPart In PartList Group By g = aPart.aPilotLight.Voltage Into itemlst = Group, Count()
                                         Order By g).Select(Function(p) p.g).ToList
                            Dim LCList = (From aPart In PartList Group By g = aPart.aPilotLight.Lens_Color Into itemlst = Group, Count()
                                          Order By g).Select(Function(p) p.g).ToList
                            Dim CBTlIST = (From aPart In PartList Group By g = aPart.aPilotLight.Contact_Blocks_Type Into itemlst = Group, Count()
                                           Order By g).Select(Function(p) p.g).ToList
                            Clist = (From aPart In PartList Group By g = aPart.aPilotLight.Contacts Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g).ToList
                            Dim OTMList = (From aPart In PartList Group By g = aPart.aPilotLight.Opera_temp_min Into itemlst = Group, Count()
                                           Order By g).Select(Function(p) p.g).ToList
                            Dim OTXList = (From aPart In PartList Group By g = aPart.aPilotLight.Opera_temp_max Into itemlst = Group, Count()
                                           Order By g).Select(Function(p) p.g).ToList
                            Dim AList = (From aPart In PartList Group By g = aPart.aPilotLight.Area_Class Into itemlst = Group, Count()
                                         Order By g).Select(Function(p) p.g).ToList

                            Me.clbNEMA.Items.Clear()
                            NElist = NElist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbNEMA.Items.AddRange(NElist.ToArray)
                            Me.palNEMA.Visible = True
                            Me.UpdateCheckList(Me.clbNEMA, Me.ECSSSearch.NEMA)

                            Me.clbFingersafe.Items.Clear()
                            FSList = FSList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbFingersafe.Items.AddRange(FSList.ToArray)
                            Me.palFingersafe.Visible = True
                            Me.UpdateCheckList(Me.clbFingersafe, Me.ECSSSearch.Fingersafe)

                            Me.clbPowerModuleType.Items.Clear()
                            PMTList = PMTList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbPowerModuleType.Items.AddRange(PMTList.ToArray)
                            Me.palPowerModuleType.Visible = True
                            Me.UpdateCheckList(Me.clbPowerModuleType, Me.ECSSSearch.PowerModuleType)

                            Me.clbLampTest.Items.Clear()
                            LTOlist = LTOlist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbLampTest.Items.AddRange(LTOlist.ToArray)
                            Me.palLampTest.Visible = True
                            Me.UpdateCheckList(Me.clbLampTest, Me.ECSSSearch.LampTest)

                            Me.clbIlluminationOption.Items.Clear()
                            IOList = IOList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbIlluminationOption.Items.AddRange(IOList.ToArray)
                            Me.palIlluminationOption.Visible = True
                            Me.UpdateCheckList(Me.clbIlluminationOption, Me.ECSSSearch.IlluminationOption)

                            Me.clbVoltageType.Items.Clear()
                            VTList = VTList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbVoltageType.Items.AddRange(VTList.ToArray)
                            Me.palVoltageType.Visible = True
                            Me.UpdateCheckList(Me.clbVoltageType, Me.ECSSSearch.VoltageType)

                            Me.clbVoltage.Items.Clear()
                            VList = VList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbVoltage.Items.AddRange(VList.ToArray)
                            Me.palVoltage.Visible = True
                            Me.UpdateCheckList(Me.clbVoltage, Me.ECSSSearch.Voltage)

                            Me.clbLenColor.Items.Clear()
                            LCList = LCList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbLenColor.Items.AddRange(LCList.ToArray)
                            Me.palLenColor.Visible = True
                            Me.UpdateCheckList(Me.clbLenColor, Me.ECSSSearch.LensColor)

                            Me.clbBlockType.Items.Clear()
                            CBTlIST = CBTlIST.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbBlockType.Items.AddRange(CBTlIST.ToArray)
                            Me.palBlockType.Visible = True
                            Me.UpdateCheckList(Me.clbBlockType, Me.ECSSSearch.BlockType)

                            Me.clbContacts.Items.Clear()
                            Clist = Clist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbContacts.Items.AddRange(Clist.ToArray)
                            Me.palContacts.Visible = True
                            Me.UpdateCheckList(Me.clbContacts, Me.ECSSSearch.Contacts)

                            Me.clbOperaTempMin.Items.Clear()
                            For Each i As Integer In OTMList
                                Me.clbOperaTempMin.Items.Add(i)
                            Next
                            Me.palOperaTempMin.Visible = True
                            Me.UpdateCheckList(Me.clbOperaTempMin, Me.ECSSSearch.OperaTempMin)

                            Me.clbOperaTempMax.Items.Clear()
                            For Each i As Integer In OTXList
                                Me.clbOperaTempMax.Items.Add(i)
                            Next
                            Me.palOperaTempMax.Visible = True
                            Me.UpdateCheckList(Me.clbOperaTempMax, Me.ECSSSearch.OperaTempMax)

                            Me.clbArea.Items.Clear()
                            AList = AList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbArea.Items.AddRange(AList.ToArray)
                            Me.palArea.Visible = True
                            Me.UpdateCheckList(Me.clbArea, Me.ECSSSearch.Area)

                        Case ECSSParts.PART_TYPE.SELECTOR_SWITCH, ECSSParts.PART_TYPE.PUSH_BUTTON, ECSSParts.PART_TYPE.ESTOP
                            For Each aPart In PartList
                                If aPart.aNonIlluminate IsNot Nothing AndAlso aPart.aNonIlluminate.NEMA_TYPE IsNot Nothing Then
                                    For Each str As String In aPart.aNonIlluminate.NEMA_TYPE
                                        If NElist.Contains(str) = Nothing AndAlso String.IsNullOrEmpty(str.Trim) = False Then NElist.Add(str)
                                    Next
                                End If
                            Next

                            Dim FSList = (From aPart In PartList Group By g = aPart.aNonIlluminate.Fingersafe Into itemlst = Group, Count()
                                          Order By g).Select(Function(p) p.g).ToList
                            Dim TList = (From aPart In PartList Group By g = aPart.aNonIlluminate.OperatorType Into itemlst = Group, Count()
                                         Order By g).Select(Function(p) p.g).ToList

                            Dim CoList = (From aPart In PartList Group By g = aPart.aNonIlluminate.Color Into itemlst = Group, Count()
                                          Order By g).Select(Function(p) p.g).ToList
                            Dim CBTList = (From aPart In PartList Group By g = aPart.aNonIlluminate.Contact_Blocks_Type Into itemlst = Group, Count()
                                           Order By g).Select(Function(p) p.g).ToList
                            Clist = (From aPart In PartList Group By g = aPart.aNonIlluminate.Contacts Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g).ToList
                            Dim OTMList = (From aPart In PartList Group By g = aPart.aNonIlluminate.Opera_temp_min Into itemlst = Group, Count()
                                           Order By g).Select(Function(p) p.g).ToList
                            Dim OTXList = (From aPart In PartList Group By g = aPart.aNonIlluminate.Opera_temp_max Into itemlst = Group, Count()
                                           Order By g).Select(Function(p) p.g).ToList
                            Dim AList = (From aPart In PartList Group By g = aPart.aNonIlluminate.Area_Class Into itemlst = Group, Count()
                                         Order By g).Select(Function(p) p.g).ToList
                            Dim ClList = (From aPart In PartList Group By g = aPart.aNonIlluminate.Class Into itemlst = Group, Count()
                                          Order By g).Select(Function(p) p.g).ToList

                            Me.clbNEMA.Items.Clear()
                            NElist = NElist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbNEMA.Items.AddRange(NElist.ToArray)
                            Me.palNEMA.Visible = True
                            Me.UpdateCheckList(Me.clbNEMA, Me.ECSSSearch.NEMA)

                            Me.clbFingersafe.Items.Clear()
                            FSList = FSList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbFingersafe.Items.AddRange(FSList.ToArray)
                            Me.palFingersafe.Visible = True
                            Me.UpdateCheckList(Me.clbFingersafe, Me.ECSSSearch.Fingersafe)

                            Me.clbType.Items.Clear()
                            TList = TList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbType.Items.AddRange(TList.ToArray)
                            Me.palType.Visible = True
                            Me.UpdateCheckList(Me.clbType, Me.ECSSSearch.Types)

                            Me.clbColor.Items.Clear()
                            CoList = CoList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbColor.Items.AddRange(CoList.ToArray)
                            Me.palColor.Visible = True
                            Me.UpdateCheckList(Me.clbColor, Me.ECSSSearch.Color)

                            Me.clbBlockType.Items.Clear()
                            CBTList = CBTList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbBlockType.Items.AddRange(CBTList.ToArray)
                            Me.palBlockType.Visible = True
                            Me.UpdateCheckList(Me.clbBlockType, Me.ECSSSearch.BlockType)

                            Me.clbContacts.Items.Clear()
                            Clist = Clist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbContacts.Items.AddRange(Clist.ToArray)
                            Me.palContacts.Visible = True
                            Me.UpdateCheckList(Me.clbContacts, Me.ECSSSearch.Contacts)

                            Me.clbOperaTempMin.Items.Clear()
                            For Each i As Integer In OTMList
                                Me.clbOperaTempMin.Items.Add(i)
                            Next
                            Me.palOperaTempMin.Visible = True
                            Me.UpdateCheckList(Me.clbOperaTempMin, Me.ECSSSearch.OperaTempMin)

                            Me.clbOperaTempMax.Items.Clear()
                            For Each i As Integer In OTXList
                                Me.clbOperaTempMax.Items.Add(i)
                            Next
                            Me.palOperaTempMax.Visible = True
                            Me.UpdateCheckList(Me.clbOperaTempMax, Me.ECSSSearch.OperaTempMax)

                            Me.clbArea.Items.Clear()
                            AList = AList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbArea.Items.AddRange(AList.ToArray)
                            Me.palArea.Visible = True
                            Me.UpdateCheckList(Me.clbArea, Me.ECSSSearch.Area)

                            Me.clbClass.Items.Clear()
                            ClList = ClList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbClass.Items.AddRange(ClList.ToArray)
                            Me.palClass.Visible = True
                            Me.UpdateCheckList(Me.clbClass, Me.ECSSSearch.Class)

                            If CurrentType = ECSSParts.PART_TYPE.PUSH_BUTTON Then
                                FList = (From aPart In PartList Group By g = aPart.aNonIlluminate.MushroomHead Into itemlst = Group, Count()
                                         Order By g).Select(Function(p) p.g).ToList

                                Me.clbFunction.Items.Clear()
                                FList = FList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                                Me.clbFunction.Items.AddRange(FList.ToArray)
                                Me.lblFilterFunction.Text = "Special Mushroom Head"
                                Me.palFunction.Visible = True
                                Me.UpdateCheckList(Me.clbFunction, Me.ECSSSearch.Functions)

                            ElseIf CurrentType = ECSSParts.PART_TYPE.SELECTOR_SWITCH OrElse CurrentType = ECSSParts.PART_TYPE.ESTOP Then
                                FList = (From aPart In PartList Group By g = aPart.aNonIlluminate.Functions Into itemlst = Group, Count()
                                         Order By g).Select(Function(p) p.g).ToList

                                Me.clbFunction.Items.Clear()
                                FList = FList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                                Me.clbFunction.Items.AddRange(FList.ToArray)
                                Me.lblFilterFunction.Text = "Functions"
                                Me.palFunction.Visible = True
                                Me.UpdateCheckList(Me.clbFunction, Me.ECSSSearch.Functions)
                            End If

                        Case ECSSParts.PART_TYPE.HEATER
                            Dim PList = (From aPart In PartList Group By g = aPart.aHeater.Power Into itemlst = Group, Count()
                                         Order By g).Select(Function(p) p.g).ToList
                            RVMList = (From aPart In PartList Group By g = aPart.aHeater.Rated_Vol_MIN Into itemlst = Group, Count()
                                       Order By g).Select(Function(p) p.g).ToList
                            Dim RVXList = (From aPart In PartList Group By g = aPart.aHeater.Rated_Vol_Max Into itemlst = Group, Count()
                                           Order By g).Select(Function(p) p.g).ToList
                            Dim AList = (From aPart In PartList Group By g = aPart.aHeater.Area_Class Into itemlst = Group, Count()
                                         Order By g).Select(Function(p) p.g).ToList
                            Dim ClList = (From aPart In PartList Group By g = aPart.aHeater.Class Into itemlst = Group, Count()
                                          Order By g).Select(Function(p) p.g).ToList
                            GList = (From aPart In PartList Group By g = aPart.aHeater.Gas_Groups Into itemlst = Group, Count()
                                     Order By g).Select(Function(p) p.g).ToList
                            Dim TCList = (From aPart In PartList Group By g = aPart.aHeater.Temp_Code Into itemlst = Group, Count()
                                          Order By g).Select(Function(p) p.g).ToList

                            Me.clbPower.Items.Clear()
                            For Each i As Integer In PList
                                Me.clbPower.Items.Add(i)
                            Next
                            Me.palPower.Visible = True
                            Me.UpdateCheckList(Me.clbPower, Me.ECSSSearch.Power)

                            Me.clbRatedVolMin.Items.Clear()
                            For Each i As Integer In RVMList
                                Me.clbRatedVolMin.Items.Add(i)
                            Next
                            Me.palRatedVolMin.Visible = True
                            Me.UpdateCheckList(Me.clbRatedVolMin, Me.ECSSSearch.RatedVolMin)

                            Me.clbRatedVolMax.Items.Clear()
                            For Each i As Integer In RVXList
                                Me.clbRatedVolMax.Items.Add(i)
                            Next
                            Me.palRatedVolMax.Visible = True
                            Me.UpdateCheckList(Me.clbRatedVolMax, Me.ECSSSearch.RatedVolMax)

                            Me.palBuiltInFan.Visible = True
                            Me.palBuiltInThem.Visible = True

                            Me.clbArea.Items.Clear()
                            AList = AList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbArea.Items.AddRange(AList.ToArray)
                            Me.palArea.Visible = True
                            Me.UpdateCheckList(Me.clbArea, Me.ECSSSearch.Area)

                            Me.clbClass.Items.Clear()
                            ClList = ClList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbClass.Items.AddRange(ClList.ToArray)
                            Me.palClass.Visible = True
                            Me.UpdateCheckList(Me.clbClass, Me.ECSSSearch.Class)

                            Me.clbGroup.Items.Clear()
                            GList = GList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbGroup.Items.AddRange(GList.ToArray)
                            Me.palGroup.Visible = True
                            Me.UpdateCheckList(Me.clbGroup, Me.ECSSSearch.Group)

                            Me.clbTempCode.Items.Clear()
                            TCList = TCList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
                            Me.clbTempCode.Items.AddRange(TCList.ToArray)
                            Me.palTempCode.Visible = True
                            Me.UpdateCheckList(Me.clbTempCode, Me.ECSSSearch.TempCode)
                    End Select
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        Finally
            Me.FIRSTTIME = False
        End Try
    End Sub

    Private Sub UpdateCheckList(ByVal clb As CheckedListBox, ByVal MList As List(Of String))
        If clb Is Nothing OrElse MList Is Nothing OrElse MList.Count = 0 Then Exit Sub
        For i As Integer = 0 To clb.Items.Count - 1
            If MList.Contains(clb.Items(i).ToString) Then clb.SetItemChecked(i, True)
        Next
    End Sub
    Private Sub UpdateCheckList(ByVal clb As CheckedListBox, ByVal MList As List(Of Integer))
        If clb Is Nothing OrElse MList Is Nothing OrElse MList.Count = 0 Then Exit Sub
        For i As Integer = 0 To clb.Items.Count - 1
            If MList.Contains(CInt(clb.Items(i))) Then clb.SetItemChecked(i, True)
        Next
    End Sub

    Private Sub dtbWidth_TrackBarMouseUp(sender As Object, e As MouseEventArgs) Handles dtbWidth.TrackBarMouseUp, dtbDepth.TrackBarMouseUp, dtbHeight.TrackBarMouseUp
        If Me.FIRSTTIME Then Exit Sub
        Try
            Dim dtb As DoubleTrackBarWithLabels = DirectCast(sender, DoubleTrackBarWithLabels)
            Select Case dtb.Name
                Case dtbWidth.Name
                    If Me.ECSSSearch.WidthMin <> dtb.SelectedMin OrElse Me.ECSSSearch.WidthMax <> dtb.SelectedMax Then
                        Me.ECSSSearch.WidthMin = dtb.SelectedMin
                        Me.ECSSSearch.WidthMax = dtb.SelectedMax
                        Me.LoadlstPart()
                    End If
                Case dtbHeight.Name
                    If Me.ECSSSearch.HeightMin <> dtb.SelectedMin OrElse Me.ECSSSearch.HeightMax <> dtb.SelectedMax Then
                        Me.ECSSSearch.HeightMin = dtb.SelectedMin
                        Me.ECSSSearch.HeightMax = dtb.SelectedMax
                        Me.LoadlstPart()
                    End If
                Case dtbDepth.Name
                    If Me.ECSSSearch.DepthMin <> dtb.SelectedMin OrElse Me.ECSSSearch.DepthMax <> dtb.SelectedMax Then
                        Me.ECSSSearch.DepthMin = dtb.SelectedMin
                        Me.ECSSSearch.DepthMax = dtb.SelectedMax
                        Me.LoadlstPart()
                    End If
            End Select

        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub


#End Region
End Class
