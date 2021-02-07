Imports System.ComponentModel
Imports System.Runtime.ExceptionServices

Public Class frmMain

    Private FIRSTTIME As Boolean = True
    Private ECSSParts As New ECSSParts
    Private ECSSSearch As New ECSSSearchCriteria
    Private BOMdic As New Dictionary(Of String, List(Of ECSSBOM))
    ' Declare a Hashtable array in which to store the groups.
    Private groupTables() As Hashtable

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
            Me.UpdateFilters(Nothing)
        Catch ex As Exception
            MessageBox.Show("Error: Loading main dialouge." & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.SetSearchSuggestList(False)
        Catch ex As Exception
        Finally
            Me.SetTitle()
            Me.FIRSTTIME = False
        End Try
    End Sub


    Private Sub SetTitle()
        Me.Text = GlobalSettings.ECSS_TITLE & " --- ( " & GlobalSettings.GetDEMOVersion & " )"
    End Sub

    Private Sub SetSearchSuggestList(ByVal cleanOnly As Boolean)
        Try
            If Me.ECSSParts IsNot Nothing AndAlso Me.ECSSParts.Keywords IsNot Nothing Then
                Control.CheckForIllegalCrossThreadCalls = False
                Me.txtSearch.AutoCompleteCustomSource.Clear()
                If cleanOnly = False Then
                    Me.txtSearch.AutoCompleteCustomSource.AddRange(Me.ECSSParts.Keywords.ToArray)
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
                anItem.SubItems.Add(aPart.Certificates1)
                anItem.SubItems.Add(aPart.Certificates2)
                Select Case aPart.PartType
                    Case ECSSParts.PART_TYPE.TRANSFORMER
                        anItem.SubItems.Add("")
                    Case ECSSParts.PART_TYPE.POWER_SUPPLY
                        anItem.SubItems.Add("")
                    Case ECSSParts.PART_TYPE.ENCLOSURE
                        anItem.SubItems.Add("")
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

            groupTables = New Hashtable(2) {}
            groupTables(0) = CreateGroupsTable(1) 'Manufacture
            groupTables(1) = CreateGroupsTable(7) 'Part Type

            Dim PTlist = Me.GetPartTypeList(TempPartList)
            If PTlist IsNot Nothing Then
                If PTlist.Count = 1 Then
                    Me.ColManufacturer.Width = 0
                    Me.ColHighlight.Width = ColHighlightWidthDefault + ColManufacturerWidthDefault
                    SetGroups(0, 1)
                Else
                    Me.ColManufacturer.Width = ColManufacturerWidthDefault
                    Me.ColHighlight.Width = ColHighlightWidthDefault
                    SetGroups(1, 7)
                End If
            End If

            Me.lstPart.EndUpdate()
            Miscelllaneous.PaintAlternatingBackColor(Me.lstPart, Color.White, Color.Honeydew)

            Me.UpdateFilters(TempPartList)
            'Me.btnFilter.Enabled = BOMHelper.EnableFilter(Me.ECSSSearch)
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        Finally

        End Try
    End Sub

    Private Sub UpdateFilters(ByVal PartList As List(Of ECSSParts.OnePart))
        Try
            Me.lblPartType.Visible = False
            Me.clbPartType.Visible = False
            Me.lblManufacturer.Visible = False
            Me.clbManufacturer.Visible = False
            Me.lblMaterial.Visible = False
            Me.clbMaterial.Visible = False

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
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Input Voltage", aPart.aPowerSupply.Input_Vol & " (V AC)"}))
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
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Style", aPart.aEnclosure.Style}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"NEMA Type", aPart.aEnclosure.NEMA_Type}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Finish", aPart.aEnclosure.Finish}))
                    Case ECSSParts.PART_TYPE.SERVIT_POST
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Type", aPart.aServitPost.Part_type}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Conductor Range Stranded", aPart.aServitPost.Range_Stranded & " (AWG)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Conductor Range Solid", aPart.aServitPost.Range_Soild & " (AWG)"}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Material", aPart.aServitPost.Material}))
                    Case ECSSParts.PART_TYPE.BREATHER_DRAIN
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"Size", aPart.aBreatherDrain.Part_size}))
                        Me.lstDetail.Items.Add(New ListViewItem(New String() {"NEMA Type", aPart.aBreatherDrain.NEMA_Type}))
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
                Me.cboBOM.Items.Add(kvp.Key)
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
                Me.TreeBOM.Nodes.Add(kvp.Key)
            Next
            Me.TreeBOM.EndUpdate()
            Me.TreeBOM.SelectedNode = Me.TreeBOM.Nodes(0)
            UpdateBOMList()
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub UpdateBOMList()
        Me.dgvBOM.Rows.Clear()
        Try
            If Me.TreeBOM.SelectedNode Is Nothing Then Exit Sub
            Dim selectedBOM As String = Me.TreeBOM.SelectedNode.Text
            If Me.BOMdic.ContainsKey(selectedBOM) Then
                For Each aPart In Me.BOMdic.Item(selectedBOM)
                    Me.dgvBOM.Rows.Add(aPart.BOMID, aPart.PartID, aPart.QTY, aPart.Manufacturer, aPart.Description, aPart.Note)
                    If Me.BOMdic.Item(selectedBOM).Where(Function(p) p.PartID = aPart.PartID).Count > 1 Then
                        Me.dgvBOM.Rows.Item(Me.dgvBOM.RowCount - 1).ErrorText = "Duplicate Part"
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

    Private Sub TreeBOM_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeBOM.AfterSelect
        If Me.FIRSTTIME Then Exit Sub
        Try
            UpdateBOMList()
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub btnBOM_Click(sender As Object, e As EventArgs) Handles btnBOM.Click
        If Me.FIRSTTIME Then Exit Sub
        Me.UpdateTreeView()
        Me.splPart.Visible = False
        Me.palBOM.Visible = True
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If Me.FIRSTTIME Then Exit Sub
        Me.splPart.Visible = True
        Me.palBOM.Visible = False
    End Sub

    Private Sub btnAddBOM_Click(sender As Object, e As EventArgs) Handles btnAddBOM.Click
        If Me.FIRSTTIME Then Exit Sub
        Dim aPart As ECSSParts.OnePart = Nothing
        Dim aBOM As ECSSBOM = Nothing
        Dim BOMID As String = ""
        Try
            If Me.lstPart.SelectedItems IsNot Nothing AndAlso Me.lstPart.SelectedItems.Count > 0 Then
                Dim aItem As ListViewItem = Me.lstPart.SelectedItems(0)
                If aItem IsNot Nothing AndAlso aItem.Tag IsNot Nothing Then
                    If Me.ECSSParts.PartDic.ContainsKey(aItem.Tag.ToString) Then
                        aPart = Me.ECSSParts.PartDic.Item(aItem.Tag.ToString)
                        If radExisting.Checked Then
                            BOMID = cboBOM.SelectedItem.ToString
                        ElseIf radNew.Checked Then
                            BOMID = "BOM" & Date.Now.ToString("yyMMddHHmmss")
                        End If
                        aBOM = New ECSSBOM
                        aBOM.BOMID = BOMID
                        aBOM.QTY = CInt(Me.txtQty.Text)
                        aBOM.PartID = aPart.PartID
                        aBOM.Manufacturer = aPart.Manufacturer
                        aBOM.Description = aPart.Description
                        If Me.BOMdic.ContainsKey(aBOM.BOMID) Then
                            Me.BOMdic.Item(aBOM.BOMID).Add(aBOM)
                            If ECSSDBFunctions.InsertOneBOM(New KeyValuePair(Of String, List(Of ECSSBOM))(aBOM.BOMID, Me.BOMdic.Item(aBOM.BOMID)), False) Then
                                MessageBox.Show("Add one part into BOM.")
                            End If
                        Else
                            Me.BOMdic.Add(aBOM.BOMID, New List(Of ECSSBOM)({aBOM}))
                            If ECSSDBFunctions.InsertOneBOM(New KeyValuePair(Of String, List(Of ECSSBOM))(aBOM.BOMID, New List(Of ECSSBOM)({aBOM})), radNew.Checked) Then
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
                    CurrentBOM = Me.BOMdic.Item(Me.dgvBOM.Tag.ToString)
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
                    End If
                End If
            ElseIf e.ColumnIndex = 2 Then ' Qty
                If String.IsNullOrEmpty(dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value) = False Then
                    CurrentBOM(e.RowIndex).QTY = CInt(dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value)
                    If CurrentBOM(e.RowIndex).QTY > 0 Then
                        dgvBOM.Rows(e.RowIndex).ErrorText = ""
                    Else
                        dgvBOM.Rows(e.RowIndex).ErrorText = "Please input Qty number."
                    End If
                End If
            ElseIf e.ColumnIndex = 5 Then 'Note
                If String.IsNullOrEmpty(dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value) = False Then
                    CurrentBOM(e.RowIndex).Note = dgvBOM.Item(e.ColumnIndex, e.RowIndex).Value.ToString
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
                            If ECSSDBFunctions.InsertOneBOM(kvp, False) Then
                                MessageBox.Show("Update current BOM.")
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
        Dim txtFile As System.IO.StreamWriter = Nothing
        Dim OutputFile As String = ""
        Dim pr As Diagnostics.Process = Nothing
        Try
            If Me.dgvBOM.Tag IsNot Nothing Then
                OutputFile = Me.dgvBOM.Tag.ToString & ".csv"
                If Me.BOMdic.ContainsKey(Me.dgvBOM.Tag.ToString) Then
                    Dim BOMs = Me.BOMdic.Item(Me.dgvBOM.Tag.ToString)
                    If System.IO.File.Exists(OutputFile) Then
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(OutputFile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
                    End If

                    txtFile = New System.IO.StreamWriter(OutputFile, False)
                    txtFile.WriteLine("Bill of Material: " & Me.dgvBOM.Tag.ToString)
                    txtFile.WriteLine("BOM Name, Part ID, Qty, Manufacturer, Description, Note")
                    txtFile.WriteLine("")
                    For Each p In BOMs
                        txtFile.WriteLine(p.BOMID & ", " & p.PartID & ", " & p.QTY & ", " & p.Manufacturer & ", """ & p.Description & """, """ & p.Note & """")
                    Next
                    txtFile.Close()

                    'OPEN CSV
                    '  pr = New Diagnostics.Process
                    'pr.StartInfo.WindowStyle = Diagnostics.ProcessWindowStyle.Normal
                    'pr.StartInfo.FileName = "Excel"
                    ' pr.StartInfo.FileName = AppContext.BaseDirectory & OutputFile
                    ' pr.Start()
                    MessageBox.Show("Excel file is generated.")
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        Finally
            If Not txtFile Is Nothing Then txtFile.Close()
            If Not pr Is Nothing Then pr.Dispose()
        End Try
    End Sub

    Private Sub clbPartType_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles clbPartType.ItemCheck, clbMaterial.ItemCheck, clbManufacturer.ItemCheck
        If Me.FIRSTTIME Then Exit Sub
        Try
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
            End Select

            'Me.btnFilter.Enabled = BOMHelper.EnableFilter(Me.ECSSSearch)
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        If Me.FIRSTTIME Then Exit Sub
        Try
            Me.LoadlstPart()
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub btnClean_Click(sender As Object, e As EventArgs) Handles btnClean.Click
        If Me.FIRSTTIME Then Exit Sub
        Try
            Me.lstPart.Items.Clear()
            Me.ECSSSearch = New ECSSSearchCriteria
            Me.UpdateDetail(Nothing, True)
            Me.UpdateFilters(Nothing)
            Me.txtSearch.Text = ""
            Me.txtSearch.SelectAll()
            Me.txtSearch.Focus()
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub
End Class
