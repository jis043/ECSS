Imports System.ComponentModel
Imports System.Runtime.ExceptionServices

Public Class frmMain

    Private FIRSTTIME As Boolean = True
    Private ECSSParts As New ECSSParts
    Private ECSSSearch As New ECSSSearchCriteria
    Private BOMdic As New Dictionary(Of String, OneBOMList) 'key BOMID 
    ' Declare a Hashtable array in which to store the groups.
    Private groupTables() As Hashtable
    Private NeedSave As Boolean = False


    Private Const ColManufacturerWidthDefault As Integer = 100
    Private Const ColHighlightWidthDefault As Integer = 110

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

            Me.ECSSParts.ReadPartsFromDB()
            BOMHelper.LoadBOM(Me.BOMdic)
            Me.UpdateBOMcombolist()
            Me.UpdateDetail(Nothing, True)
            Me.Updatefilters(ECSSParts.PART_TYPE.NONE)
        Catch ex As Exception
            MessageBox.Show("Error: Loading main dialouge." & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.SetSearchSuggestList(False)
            Me.UpdateAllFilters(ECSSParts.PART_TYPE.NONE)
        Catch ex As Exception
        Finally
            Me.SetTitle()
            Me.FIRSTTIME = False
        End Try
    End Sub


    Private Sub SetTitle()
        Me.Text = GlobalSettings.COMPANY & ": " & GlobalSettings.ECSS_TITLE & " -- ( " & GlobalSettings.GetDEMOVersion & " )"
    End Sub

    Private Sub SetSearchSuggestList(ByVal cleanOnly As Boolean)
        Try
            If Me.ECSSParts IsNot Nothing AndAlso Me.ECSSParts.Keywords IsNot Nothing Then
                Control.CheckForIllegalCrossThreadCalls = False
                Me.txtSearch.AutoCompleteCustomSource.Clear()
                If cleanOnly = False Then
                    Me.txtSearch.AutoCompleteCustomSource.AddRange(Me.ECSSParts.Keywords.ToArray)
                    Me.txtSearchSuggest.EmailAutocompleteSource = Me.ECSSParts.Keywords.ToArray
                    Me.txtSearchSuggest.BringToFront()
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
        Try
            Me.lstPart.Items.Clear()
            Me.lstPart.BeginUpdate()
            For Each kvp In Me.ECSSParts.PartDic
                If Me.ECSSParts.PartMatchesSearchCriteria(kvp.Value, Me.ECSSSearch) Then
                    TempPartList.Add(kvp.Value)
                End If
            Next

            For Each aPart In TempPartList
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
                End Select
                anItem.SubItems.Add(aPart.PartType.ToString)
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
                        Me.UpdateAllFilters(PTlist(0))
                    Else
                        Me.UpdateAllFilters(ECSSParts.PART_TYPE.NONE)
                    End If
                End If
            End If

            Me.lstPart.EndUpdate()
            If TempPartList.Count > 0 Then
                Miscelllaneous.PaintAlternatingBackColor(Me.lstPart, Color.White, Color.Honeydew)
            Else
                Me.lstPart.Refresh()
            End If


        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        Finally

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
                Me.lstDetail.Items.Clear()
                'Me.palAddBOM.Enabled = False
                Exit Sub
            Else
                'Me.palAddBOM.Enabled = True
            End If

            Me.lstDetail.Items.Clear()
            If aPart Is Nothing Then
                Me.lstDetail.Enabled = False
                Me.palAddBOM.Visible = False
            Else
                Me.lstDetail.BeginUpdate()
                Me.lstDetail.Enabled = True
                Me.palAddBOM.Visible = True
                If aPart.PartType = ECSSParts.PART_TYPE.TRANSFORMER OrElse
                    aPart.PartType = ECSSParts.PART_TYPE.ENCLOSURE OrElse
                    aPart.PartType = ECSSParts.PART_TYPE.POWER_SUPPLY Then

                    Me.lstDetail.Items.Add(New ListViewItem(New String() {"Height", aPart.Height & " (mm)"}))
                    Me.lstDetail.Items.Add(New ListViewItem(New String() {"Width", aPart.Width & " (mm)"}))
                    Me.lstDetail.Items.Add(New ListViewItem(New String() {"Depth", aPart.Depth & " (mm)"}))
                    Me.lstDetail.Items.Add(New ListViewItem(New String() {"", ""}))
                End If

                Select Case aPart.PartType
                    Case ECSSParts.PART_TYPE.TRANSFORMER
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Power", aPart.aTransformer.Power & " (VA)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Frequency", aPart.aTransformer.Frequency & " (Hz)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Phase I", aPart.aTransformer.Phase1}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Pri. Voltage", aPart.aTransformer.Pri_Voltage & " (V AC)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Phase II", aPart.aTransformer.Phase2}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Sec. Voltage", aPart.aTransformer.Sec_Voltage & " (V AC)"}))
                    Case ECSSParts.PART_TYPE.POWER_SUPPLY
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Output Power", aPart.aPowerSupply.Output_Power & " (W)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Output Current", aPart.aPowerSupply.Output_Current & " (A)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Output Voltage", aPart.aPowerSupply.Output_Vol & " (V DC)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Input Phase", aPart.aPowerSupply.Input_Phase}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Operation Temp. Min", aPart.aPowerSupply.Opera_temp_min & " (°C)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Operation Temp. Max", aPart.aPowerSupply.Opera_temp_max & " (°C)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Storage Temp. Min", aPart.aPowerSupply.Stor_temp_min & " (°C)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Storage Temp. Max", aPart.aPowerSupply.Stor_temp_max & " (°C)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Area Classification", aPart.aPowerSupply.Area_Class}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Class", aPart.aPowerSupply.SupplyClass}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Division", aPart.aPowerSupply.Division}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Gas Group", aPart.aPowerSupply.Gas_Group}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Temp. Code", aPart.aPowerSupply.Temp_Code}))
                    Case ECSSParts.PART_TYPE.ENCLOSURE
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Color", aPart.aEnclosure.Color}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"NEMA Type", Miscelllaneous.ListToString(aPart.aEnclosure.NEMA_Type)}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Mount Type", aPart.aEnclosure.MountType}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Mount ID", Miscelllaneous.ListToString(aPart.aEnclosure.MountID)}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Mount Height", aPart.aEnclosure.MountHeight}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Mount Width", aPart.aEnclosure.MountWidth}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Window", aPart.aEnclosure.Window}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"PDF", aPart.PDF}))
                    Case ECSSParts.PART_TYPE.SERVIT_POST
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Type", aPart.aServitPost.Part_type}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Conductor Range Stranded", aPart.aServitPost.Range_Stranded & " (AWG)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Conductor Range Solid", aPart.aServitPost.Range_Soild & " (AWG)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Material", aPart.aServitPost.Material}))
                    Case ECSSParts.PART_TYPE.BREATHER_DRAIN
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Size", aPart.aBreatherDrain.Part_size}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"NEMA Type", Miscelllaneous.ListToString(aPart.aBreatherDrain.NEMA_Type)}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Material", aPart.aBreatherDrain.Material}))
                End Select

                Me.lstDetail.EndUpdate()
                Miscelllaneous.PaintAlternatingBackColor(Me.lstDetail, Color.White, Color.Honeydew)
                Me.txtQty.Text = ""
                Me.btnAddBOM.Enabled = False
                If Me.BOMdic Is Nothing OrElse Me.BOMdic.Count > 0 Then
                    Me.UpdateBOMcombolist()
                    Me.radExisting.Checked = True
                Else
                    Me.radNew.Checked = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub UpdateBOMcombolist()
        Me.cboBOM.Items.Clear()
        If Me.BOMdic Is Nothing OrElse Me.BOMdic.Count > 0 Then
            For Each kvp In Me.BOMdic
                Me.cboBOM.Items.Add(kvp.Value.BOMTitle)
            Next
            Me.cboBOM.SelectedIndex = 0
        End If
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

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If Me.FIRSTTIME Then Exit Sub
        Try
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
                If Me.ECSSSearch.keyword.ToUpper <> keyword.ToUpper Then
                    Me.ECSSSearch.keyword = keyword
                    Me.LoadlstPart()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub


    Private Sub txtSearch_Validating(sender As Object, e As CancelEventArgs) Handles txtSearch.Validating
        If Me.FIRSTTIME Then Exit Sub
        Try
            Dim input As String = Me.txtSearch.Text
            If String.IsNullOrEmpty(input.Trim) = False Then
                If Me.ECSSSearch.keyword.ToUpper <> input.Trim.ToUpper Then
                    Me.ECSSSearch.keyword = input
                    Me.LoadlstPart()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If Me.FIRSTTIME Then Exit Sub
        Try
            If e.KeyCode = Keys.Enter Then
                Dim input As String = Me.txtSearch.Text
                If String.IsNullOrEmpty(input.Trim) = False Then
                    If Me.ECSSSearch.keyword.ToUpper <> input.Trim.ToUpper Then
                        Me.ECSSSearch.keyword = input
                        Me.LoadlstPart()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        If Me.FIRSTTIME Then Exit Sub
        Try
            If String.IsNullOrEmpty(Me.txtQty.Text.Trim) = False Then
                If IsNumeric(Me.txtQty.Text.Trim) Then
                    Me.btnAddBOM.Enabled = True
                Else
                    MessageBox.Show("Please input a valid number. ", "Qty Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub btnAddBOM_Click(sender As Object, e As EventArgs) Handles btnAddBOM.Click
        If Me.FIRSTTIME Then Exit Sub
        Dim aPart As ECSSParts.OnePart = Nothing
        Dim aBOM As ECSSBOM = Nothing
        Dim BOMID As String = ""
        Dim BOMTitle As String = ""
        Try
            If Me.lstPart.SelectedItems IsNot Nothing AndAlso Me.lstPart.SelectedItems.Count > 0 Then
                Dim aItem As ListViewItem = Me.lstPart.SelectedItems(0)
                If aItem IsNot Nothing AndAlso aItem.Tag IsNot Nothing Then
                    If Me.ECSSParts.PartDic.ContainsKey(aItem.Tag.ToString) Then
                        aPart = Me.ECSSParts.PartDic.Item(aItem.Tag.ToString)
                        If radExisting.Checked Then
                            BOMTitle = cboBOM.SelectedItem.ToString
                        ElseIf radNew.Checked Then
                            BOMTitle = "BOM" & Date.Now.ToString("yyyyMMddHHmmss")
                        End If
                        BOMID = Me.BOMdic.ElementAt(Me.cboBOM.SelectedIndex).Key
                        aBOM = New ECSSBOM
                        aBOM.BOMID = BOMID
                        aBOM.QTY = CInt(Me.txtQty.Text)
                        aBOM.PartID = aPart.PartID
                        aBOM.Manufacturer = aPart.Manufacturer
                        aBOM.Description = aPart.Description
                        If Me.BOMdic.ContainsKey(aBOM.BOMID) Then
                            Me.BOMdic.Item(aBOM.BOMID).BOMList.Add(aBOM)
                            If ECSSDBFunctions.InsertOneBOM(Me.BOMdic.Item(aBOM.BOMID), False) Then
                                MessageBox.Show("Add one part into BOM.")
                            End If
                        Else
                            Dim aList As New OneBOMList
                            aList.BOMID = BOMID
                            aList.BOMTitle = BOMTitle
                            aList.BOMList = New List(Of ECSSBOM)({aBOM})
                            Me.BOMdic.Add(BOMID, aList)
                            If ECSSDBFunctions.InsertOneBOM(aList, radNew.Checked) Then
                                MessageBox.Show("Add one part into BOM.")
                            End If
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
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
                    CurrentBOM(e.RowIndex).Manufacturer = CInt(dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value)
                    Me.NeedSave = True
                End If
            ElseIf e.ColumnIndex = 4 Then 'description
                If String.IsNullOrEmpty(dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value) = False Then
                    CurrentBOM(e.RowIndex).Description = CInt(dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value)
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
        clbCertificates.ItemCheck, clbHeight.ItemCheck, clbWidth.ItemCheck, clbDepth.ItemCheck, clbMount.ItemCheck, clbNEMA.ItemCheck, clbOutputA.ItemCheck,
        clbOutputVol.ItemCheck, clbNormalV.ItemCheck, clbInputPhase.ItemCheck, clbClass.ItemCheck, clbGroup.ItemCheck
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
                Case clbHeight.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Height.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.Height.Add(clb.Items(e.Index))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.Height.Contains(m) Then Me.ECSSSearch.Height.Remove(m)
                    End If
                Case clbWidth.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Width.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.Width.Add(clb.Items(e.Index))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.Width.Contains(m) Then Me.ECSSSearch.Width.Remove(m)
                    End If
                Case clbDepth.Name
                    If e.NewValue = CheckState.Checked Then
                        If Me.ECSSSearch.Depth.Contains(CInt(clb.Items(e.Index))) = False Then Me.ECSSSearch.Depth.Add(clb.Items(e.Index))
                    ElseIf e.NewValue = CheckState.Unchecked Then
                        Dim m = CInt(clb.Items(e.Index))
                        If Me.ECSSSearch.Depth.Contains(m) Then Me.ECSSSearch.Depth.Remove(m)
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
            Me.UpdateAllFilters(ECSSParts.PART_TYPE.NONE)
            Me.txtSearch.Text = ""
            Me.txtSearch.SelectAll()
            Me.txtSearch.Focus()
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

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

        Dim msg = MessageBox.Show("All unsaved BOM will be lost, do you still want to close the application?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        e.Cancel = (msg = DialogResult.No)

    End Sub

    Private Sub btnSetting_Click(sender As Object, e As EventArgs) Handles btnSetting.Click
        Try
            Dim frm As New frmGeneralSettings
            frm.ShowDialog(Me)
            frm.Dispose()
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

        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        Finally
            Me.FIRSTTIME = False
        End Try
    End Sub
    Private Sub UpdateAllFilters(ByVal PType As ECSSParts.PART_TYPE)
        Me.palCate.Visible = True
        Me.palManufacturer.Visible = True
        Me.palCertificates.Visible = True
        Me.palMaterial.Visible = True

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

        If PType = ECSSParts.PART_TYPE.ENCLOSURE Then
            Me.palHeight.Visible = True
            Me.palWidth.Visible = True
            Me.palDepth.Visible = True
            Me.palMount.Visible = True
            Me.palNEMA.Visible = True
        ElseIf PType = ECSSParts.PART_TYPE.POWER_SUPPLY Then
            Me.palOutputV.Visible = True
            Me.palOutputA.Visible = True
            Me.palNormalV.Visible = True
            Me.palInputPhase.Visible = True
            Me.palClass.Visible = True
            Me.palGroup.Visible = True
        End If

    End Sub


    Private Sub Updatefilters(ByVal partType As ECSSParts.PART_TYPE)
        If Me.ECSSParts Is Nothing Then Exit Sub
        Try
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
            Dim mflist = From aPart In Me.ECSSParts.PartDic Group By mf = aPart.Value.Manufacturer Into itemlst = Group, Count()
                         Order By mf
            If mflist IsNot Nothing AndAlso mflist.Count > 0 Then
                Me.clbManufacturer.Items.Clear()
                For Each m In mflist
                    Me.clbManufacturer.Items.Add(m.mf)
                    If Me.ECSSSearch.Manufacturer.Contains(m.mf) Then
                        Me.clbManufacturer.SetItemChecked(Me.clbManufacturer.Items.Count - 1, True)
                    End If
                Next
            End If

            Dim crList, mList, hList, Wlist, DList, MountList, NElist, OVList, OAList, NVList, IPList, Clist, GList As New List(Of String)

            For Each kvp In Me.ECSSParts.PartDic
                Dim aPart = kvp.Value

                'Certificate
                For Each Str As String In aPart.Certificates
                    If crList.Contains(Str) = False AndAlso String.IsNullOrEmpty(Str.Trim) = False Then crList.Add(Str)
                Next

                'Material 
                If aPart.aBreatherDrain IsNot Nothing AndAlso mList.Contains(aPart.aBreatherDrain.Material) = False Then
                    mList.Add(aPart.aBreatherDrain.Material)
                End If
                If aPart.aServitPost IsNot Nothing AndAlso mList.Contains(aPart.aServitPost.Material) = False Then
                    mList.Add(aPart.aServitPost.Material)
                End If
                If aPart.aEnclosure IsNot Nothing AndAlso mList.Contains(aPart.aEnclosure.Material) = False Then
                    mList.Add(aPart.aEnclosure.Material)
                End If

                If aPart.PartType = ECSSParts.PART_TYPE.ENCLOSURE Then
                    If hList.Contains(aPart.Height.ToString) = False Then
                        hList.Add(aPart.Height.ToString)
                    End If
                    If Wlist.Contains(aPart.Width.ToString) = False Then
                        Wlist.Add(aPart.Width.ToString)
                    End If
                    If DList.Contains(aPart.Depth.ToString) = False Then
                        DList.Add(aPart.Depth.ToString)
                    End If
                End If

                If aPart.aEnclosure IsNot Nothing AndAlso MountList.Contains(aPart.aEnclosure.MountType) = False Then
                    MountList.Add(aPart.aEnclosure.MountType)
                End If


                If aPart.aEnclosure IsNot Nothing AndAlso aPart.aEnclosure.NEMA_Type IsNot Nothing Then
                    For Each str As String In aPart.aEnclosure.NEMA_Type
                        If NElist.Contains(str) = Nothing AndAlso String.IsNullOrEmpty(str.Trim) = False Then NElist.Add(str)
                    Next
                End If


                If aPart.aPowerSupply IsNot Nothing AndAlso OVList.Contains(aPart.aPowerSupply.Output_Vol.ToString) = False Then
                    OVList.Add(aPart.aPowerSupply.Output_Vol.ToString)
                End If

                If aPart.aPowerSupply IsNot Nothing AndAlso OAList.Contains(aPart.aPowerSupply.Output_Current.ToString) = False Then
                    OAList.Add(aPart.aPowerSupply.Output_Current.ToString)
                End If

                If aPart.aPowerSupply IsNot Nothing AndAlso NVList.Contains(aPart.aPowerSupply.Normal_Vol.ToString) = False Then
                    NVList.Add(aPart.aPowerSupply.Normal_Vol.ToString)
                End If

                If aPart.aPowerSupply IsNot Nothing AndAlso IPList.Contains(aPart.aPowerSupply.Input_Phase.ToString) = False Then
                    IPList.Add(aPart.aPowerSupply.Input_Phase.ToString)
                End If

                If aPart.aPowerSupply IsNot Nothing AndAlso Clist.Contains(aPart.aPowerSupply.SupplyClass) = False Then
                    Clist.Add(aPart.aPowerSupply.SupplyClass)
                End If

                If aPart.aPowerSupply IsNot Nothing AndAlso GList.Contains(aPart.aPowerSupply.Gas_Group) = False Then
                    GList.Add(aPart.aPowerSupply.Gas_Group)
                End If
            Next

            'Update the checkbox combo 
            Me.clbCertificates.Items.Clear()
            crList = crList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            Me.clbCertificates.Items.AddRange(crList.ToArray)

            Me.clbMaterial.Items.Clear()
            mList = mList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            Me.clbMaterial.Items.AddRange(mList.ToArray)

            Me.clbHeight.Items.Clear()
            hList = hList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            hList.Sort()
            Me.clbHeight.Items.AddRange(hList.ToArray)

            Me.clbWidth.Items.Clear()
            Wlist = Wlist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            Wlist.Sort()
            Me.clbWidth.Items.AddRange(Wlist.ToArray)

            Me.clbDepth.Items.Clear()
            DList = DList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            DList.Sort()
            Me.clbDepth.Items.AddRange(DList.ToArray)

            Me.clbMount.Items.Clear()
            MountList = MountList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            Me.clbMount.Items.AddRange(MountList.ToArray)

            Me.clbNEMA.Items.Clear()
            NElist = NElist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            Me.clbNEMA.Items.AddRange(NElist.ToArray)

            Me.clbOutputVol.Items.Clear()
            OVList = OVList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            OVList.Sort()
            Me.clbOutputVol.Items.AddRange(OVList.ToArray)

            Me.clbOutputA.Items.Clear()
            OAList = OAList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            OAList.Sort()
            Me.clbOutputA.Items.AddRange(OAList.ToArray)

            Me.clbNormalV.Items.Clear()
            NVList = NVList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            NVList.Sort()
            Me.clbNormalV.Items.AddRange(NVList.ToArray)

            Me.clbInputPhase.Items.Clear()
            IPList = IPList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            IPList.Sort()
            Me.clbInputPhase.Items.AddRange(IPList.ToArray)

            Me.clbClass.Items.Clear()
            Clist = Clist.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            Me.clbClass.Items.AddRange(Clist.ToArray)

            Me.clbGroup.Items.Clear()
            GList = GList.Where(Function(s) String.IsNullOrWhiteSpace(s) = False).Distinct().ToList()
            Me.clbGroup.Items.AddRange(GList.ToArray)


        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub UpdateFilters(ByVal PartList As List(Of ECSSParts.OnePart))
        Try


            If PartList IsNot Nothing Then
                'PART TYPE
                Dim ptlist = From aPart In PartList Group By ptype = aPart.PartType Into itemlst = Group, Count()
                             Order By ptype
                If ptlist IsNot Nothing AndAlso ptlist.Count > 0 Then
                    Me.clbPartType.Items.Clear()
                    For Each pt In ptlist
                        Me.clbPartType.Items.Add(ECSSParts.GetTypeName(pt.ptype))
                        If Me.ECSSSearch.PartType.Contains(pt.ptype) Then
                            Me.clbPartType.SetItemChecked(Me.clbPartType.Items.Count - 1, True)
                        End If
                    Next
                    Me.lblPartType.Visible = True
                    Me.clbPartType.Visible = True
                End If

                'MANUFACTURER
                Dim mflist = From aPart In PartList Group By mf = aPart.Manufacturer Into itemlst = Group, Count()
                             Order By mf
                If mflist IsNot Nothing AndAlso mflist.Count > 0 Then
                    Me.clbManufacturer.Items.Clear()
                    For Each m In mflist
                        Me.clbManufacturer.Items.Add(m.mf)
                        If Me.ECSSSearch.Manufacturer.Contains(m.mf) Then
                            Me.clbManufacturer.SetItemChecked(Me.clbManufacturer.Items.Count - 1, True)
                        End If
                    Next
                    Me.lblManufacturer.Visible = True
                    Me.clbManufacturer.Visible = True
                End If

                'MATERIAL
                Dim mList As New List(Of String)
                For Each apart In PartList
                    If apart.aBreatherDrain IsNot Nothing AndAlso mList.Contains(apart.aBreatherDrain.Material) = False Then
                        mList.Add(apart.aBreatherDrain.Material)
                    End If
                    If apart.aServitPost IsNot Nothing AndAlso mList.Contains(apart.aServitPost.Material) = False Then
                        mList.Add(apart.aServitPost.Material)
                    End If
                Next
                If mList IsNot Nothing AndAlso mList.Count > 0 Then
                    Me.clbMaterial.Items.Clear()
                    For Each m In mList
                        Me.clbMaterial.Items.Add(m)
                        If Me.ECSSSearch.Material.Contains(m) Then
                            Me.clbMaterial.SetItemChecked(Me.clbMaterial.Items.Count - 1, True)
                        End If
                    Next
                    Me.lblMaterial.Visible = True
                    Me.clbMaterial.Visible = True
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub


#End Region
End Class
