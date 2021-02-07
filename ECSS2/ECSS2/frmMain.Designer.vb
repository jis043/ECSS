<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.palSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnClean = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnBOM = New System.Windows.Forms.Button()
        Me.splPart = New System.Windows.Forms.SplitContainer()
        Me.grpFilter = New System.Windows.Forms.GroupBox()
        Me.btnFilter = New System.Windows.Forms.Button()
        Me.lblMaterial = New System.Windows.Forms.Label()
        Me.clbMaterial = New System.Windows.Forms.CheckedListBox()
        Me.lblManufacturer = New System.Windows.Forms.Label()
        Me.clbManufacturer = New System.Windows.Forms.CheckedListBox()
        Me.lblPartType = New System.Windows.Forms.Label()
        Me.clbPartType = New System.Windows.Forms.CheckedListBox()
        Me.lstPart = New System.Windows.Forms.ListView()
        Me.colPartID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColManufacturer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColCerti1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColCerti2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColMaterial = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColHighlight = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColDescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.palDetail = New System.Windows.Forms.Panel()
        Me.palAddBOM = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.radExisting = New System.Windows.Forms.RadioButton()
        Me.radNew = New System.Windows.Forms.RadioButton()
        Me.cboBOM = New System.Windows.Forms.ComboBox()
        Me.btnAddBOM = New System.Windows.Forms.Button()
        Me.lstDetail = New System.Windows.Forms.ListView()
        Me.colVar = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColValue = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.palBOM = New System.Windows.Forms.Panel()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnExcel = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.dgvBOM = New System.Windows.Forms.DataGridView()
        Me.colBOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColBOMPartID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColManu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDesp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNote = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TreeBOM = New System.Windows.Forms.TreeView()
        Me.palSearch.SuspendLayout()
        CType(Me.splPart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splPart.Panel1.SuspendLayout()
        Me.splPart.Panel2.SuspendLayout()
        Me.splPart.SuspendLayout()
        Me.grpFilter.SuspendLayout()
        Me.palDetail.SuspendLayout()
        Me.palAddBOM.SuspendLayout()
        Me.palBOM.SuspendLayout()
        CType(Me.dgvBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'palSearch
        '
        Me.palSearch.Controls.Add(Me.txtSearch)
        Me.palSearch.Controls.Add(Me.btnClean)
        Me.palSearch.Controls.Add(Me.btnSearch)
        Me.palSearch.Controls.Add(Me.Label1)
        Me.palSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.palSearch.Location = New System.Drawing.Point(0, 0)
        Me.palSearch.Name = "palSearch"
        Me.palSearch.Size = New System.Drawing.Size(1015, 69)
        Me.palSearch.TabIndex = 0
        '
        'txtSearch
        '
        Me.txtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Location = New System.Drawing.Point(182, 28)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(427, 20)
        Me.txtSearch.TabIndex = 1
        '
        'btnClean
        '
        Me.btnClean.Location = New System.Drawing.Point(698, 28)
        Me.btnClean.Name = "btnClean"
        Me.btnClean.Size = New System.Drawing.Size(77, 20)
        Me.btnClean.TabIndex = 3
        Me.btnClean.Text = "Clean"
        Me.btnClean.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(615, 28)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(77, 20)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Engineering Component Search:"
        '
        'btnBOM
        '
        Me.btnBOM.Location = New System.Drawing.Point(5, 106)
        Me.btnBOM.Name = "btnBOM"
        Me.btnBOM.Size = New System.Drawing.Size(78, 20)
        Me.btnBOM.TabIndex = 4
        Me.btnBOM.Text = "View BOM"
        Me.btnBOM.UseVisualStyleBackColor = True
        '
        'splPart
        '
        Me.splPart.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splPart.IsSplitterFixed = True
        Me.splPart.Location = New System.Drawing.Point(0, 69)
        Me.splPart.Name = "splPart"
        '
        'splPart.Panel1
        '
        Me.splPart.Panel1.Controls.Add(Me.grpFilter)
        '
        'splPart.Panel2
        '
        Me.splPart.Panel2.Controls.Add(Me.lstPart)
        Me.splPart.Panel2.Controls.Add(Me.palDetail)
        Me.splPart.Size = New System.Drawing.Size(1015, 590)
        Me.splPart.SplitterDistance = 102
        Me.splPart.SplitterWidth = 2
        Me.splPart.TabIndex = 1
        '
        'grpFilter
        '
        Me.grpFilter.Controls.Add(Me.btnFilter)
        Me.grpFilter.Controls.Add(Me.lblMaterial)
        Me.grpFilter.Controls.Add(Me.clbMaterial)
        Me.grpFilter.Controls.Add(Me.lblManufacturer)
        Me.grpFilter.Controls.Add(Me.clbManufacturer)
        Me.grpFilter.Controls.Add(Me.lblPartType)
        Me.grpFilter.Controls.Add(Me.clbPartType)
        Me.grpFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpFilter.Location = New System.Drawing.Point(0, 0)
        Me.grpFilter.Name = "grpFilter"
        Me.grpFilter.Size = New System.Drawing.Size(102, 590)
        Me.grpFilter.TabIndex = 0
        Me.grpFilter.TabStop = False
        Me.grpFilter.Text = "Filters"
        '
        'btnFilter
        '
        Me.btnFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnFilter.Location = New System.Drawing.Point(7, 556)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(87, 23)
        Me.btnFilter.TabIndex = 4
        Me.btnFilter.Text = "Apply Filter"
        Me.btnFilter.UseVisualStyleBackColor = True
        '
        'lblMaterial
        '
        Me.lblMaterial.AutoSize = True
        Me.lblMaterial.Location = New System.Drawing.Point(10, 294)
        Me.lblMaterial.Name = "lblMaterial"
        Me.lblMaterial.Size = New System.Drawing.Size(44, 13)
        Me.lblMaterial.TabIndex = 11
        Me.lblMaterial.Text = "Material"
        '
        'clbMaterial
        '
        Me.clbMaterial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbMaterial.FormattingEnabled = True
        Me.clbMaterial.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbMaterial.Location = New System.Drawing.Point(5, 312)
        Me.clbMaterial.Name = "clbMaterial"
        Me.clbMaterial.Size = New System.Drawing.Size(93, 107)
        Me.clbMaterial.TabIndex = 10
        '
        'lblManufacturer
        '
        Me.lblManufacturer.AutoSize = True
        Me.lblManufacturer.Location = New System.Drawing.Point(10, 155)
        Me.lblManufacturer.Name = "lblManufacturer"
        Me.lblManufacturer.Size = New System.Drawing.Size(70, 13)
        Me.lblManufacturer.TabIndex = 9
        Me.lblManufacturer.Text = "Manufacturer"
        '
        'clbManufacturer
        '
        Me.clbManufacturer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbManufacturer.FormattingEnabled = True
        Me.clbManufacturer.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbManufacturer.Location = New System.Drawing.Point(7, 174)
        Me.clbManufacturer.Name = "clbManufacturer"
        Me.clbManufacturer.Size = New System.Drawing.Size(93, 107)
        Me.clbManufacturer.TabIndex = 8
        '
        'lblPartType
        '
        Me.lblPartType.AutoSize = True
        Me.lblPartType.Location = New System.Drawing.Point(10, 16)
        Me.lblPartType.Name = "lblPartType"
        Me.lblPartType.Size = New System.Drawing.Size(53, 13)
        Me.lblPartType.TabIndex = 7
        Me.lblPartType.Text = "Part Type"
        '
        'clbPartType
        '
        Me.clbPartType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbPartType.FormattingEnabled = True
        Me.clbPartType.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbPartType.Location = New System.Drawing.Point(5, 36)
        Me.clbPartType.Name = "clbPartType"
        Me.clbPartType.Size = New System.Drawing.Size(93, 107)
        Me.clbPartType.TabIndex = 6
        '
        'lstPart
        '
        Me.lstPart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstPart.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colPartID, Me.ColManufacturer, Me.ColCerti1, Me.ColCerti2, Me.ColMaterial, Me.ColHighlight, Me.ColDescription, Me.ColType})
        Me.lstPart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstPart.FullRowSelect = True
        Me.lstPart.GridLines = True
        Me.lstPart.HideSelection = False
        Me.lstPart.Location = New System.Drawing.Point(0, 0)
        Me.lstPart.MultiSelect = False
        Me.lstPart.Name = "lstPart"
        Me.lstPart.Size = New System.Drawing.Size(722, 590)
        Me.lstPart.TabIndex = 1
        Me.lstPart.UseCompatibleStateImageBehavior = False
        Me.lstPart.View = System.Windows.Forms.View.Details
        '
        'colPartID
        '
        Me.colPartID.Name = "colPartID"
        Me.colPartID.Text = "Part ID"
        Me.colPartID.Width = 100
        '
        'ColManufacturer
        '
        Me.ColManufacturer.Name = "ColManufacturer"
        Me.ColManufacturer.Text = "Manufacturer"
        Me.ColManufacturer.Width = 100
        '
        'ColCerti1
        '
        Me.ColCerti1.Name = "ColCerti1"
        Me.ColCerti1.Text = "Certificates 1"
        Me.ColCerti1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColCerti1.Width = 75
        '
        'ColCerti2
        '
        Me.ColCerti2.Name = "ColCerti2"
        Me.ColCerti2.Text = "Certificates 2"
        Me.ColCerti2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColCerti2.Width = 75
        '
        'ColMaterial
        '
        Me.ColMaterial.Name = "ColMaterial"
        Me.ColMaterial.Text = "Material"
        Me.ColMaterial.Width = 70
        '
        'ColHighlight
        '
        Me.ColHighlight.Name = "ColHighlight"
        Me.ColHighlight.Text = "Highlights"
        Me.ColHighlight.Width = 110
        '
        'ColDescription
        '
        Me.ColDescription.Name = "ColDescription"
        Me.ColDescription.Text = "Description"
        Me.ColDescription.Width = 170
        '
        'ColType
        '
        Me.ColType.Name = "ColType"
        Me.ColType.Text = "Type"
        Me.ColType.Width = 0
        '
        'palDetail
        '
        Me.palDetail.Controls.Add(Me.palAddBOM)
        Me.palDetail.Controls.Add(Me.lstDetail)
        Me.palDetail.Dock = System.Windows.Forms.DockStyle.Right
        Me.palDetail.Location = New System.Drawing.Point(722, 0)
        Me.palDetail.Name = "palDetail"
        Me.palDetail.Size = New System.Drawing.Size(189, 590)
        Me.palDetail.TabIndex = 0
        '
        'palAddBOM
        '
        Me.palAddBOM.Controls.Add(Me.btnBOM)
        Me.palAddBOM.Controls.Add(Me.Label3)
        Me.palAddBOM.Controls.Add(Me.txtQty)
        Me.palAddBOM.Controls.Add(Me.Label2)
        Me.palAddBOM.Controls.Add(Me.radExisting)
        Me.palAddBOM.Controls.Add(Me.radNew)
        Me.palAddBOM.Controls.Add(Me.cboBOM)
        Me.palAddBOM.Controls.Add(Me.btnAddBOM)
        Me.palAddBOM.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.palAddBOM.Location = New System.Drawing.Point(0, 454)
        Me.palAddBOM.Name = "palAddBOM"
        Me.palAddBOM.Size = New System.Drawing.Size(189, 136)
        Me.palAddBOM.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Add Select Part to BOM"
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(63, 81)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(116, 20)
        Me.txtQty.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Qty:"
        '
        'radExisting
        '
        Me.radExisting.AutoSize = True
        Me.radExisting.Location = New System.Drawing.Point(5, 56)
        Me.radExisting.Name = "radExisting"
        Me.radExisting.Size = New System.Drawing.Size(56, 17)
        Me.radExisting.TabIndex = 3
        Me.radExisting.TabStop = True
        Me.radExisting.Text = "Add to"
        Me.radExisting.UseVisualStyleBackColor = True
        '
        'radNew
        '
        Me.radNew.AutoSize = True
        Me.radNew.Checked = True
        Me.radNew.Location = New System.Drawing.Point(5, 34)
        Me.radNew.Name = "radNew"
        Me.radNew.Size = New System.Drawing.Size(115, 17)
        Me.radNew.TabIndex = 2
        Me.radNew.TabStop = True
        Me.radNew.Text = "Create a new BOM"
        Me.radNew.UseVisualStyleBackColor = True
        '
        'cboBOM
        '
        Me.cboBOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBOM.FormattingEnabled = True
        Me.cboBOM.Location = New System.Drawing.Point(63, 55)
        Me.cboBOM.Name = "cboBOM"
        Me.cboBOM.Size = New System.Drawing.Size(116, 21)
        Me.cboBOM.TabIndex = 1
        '
        'btnAddBOM
        '
        Me.btnAddBOM.Location = New System.Drawing.Point(88, 106)
        Me.btnAddBOM.Name = "btnAddBOM"
        Me.btnAddBOM.Size = New System.Drawing.Size(90, 20)
        Me.btnAddBOM.TabIndex = 0
        Me.btnAddBOM.Text = "Add to BOM"
        Me.btnAddBOM.UseVisualStyleBackColor = True
        '
        'lstDetail
        '
        Me.lstDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstDetail.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colVar, Me.ColValue})
        Me.lstDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstDetail.GridLines = True
        Me.lstDetail.HideSelection = False
        Me.lstDetail.Location = New System.Drawing.Point(0, 0)
        Me.lstDetail.Name = "lstDetail"
        Me.lstDetail.Size = New System.Drawing.Size(189, 590)
        Me.lstDetail.TabIndex = 0
        Me.lstDetail.UseCompatibleStateImageBehavior = False
        Me.lstDetail.View = System.Windows.Forms.View.Details
        '
        'colVar
        '
        Me.colVar.Name = "colVar"
        Me.colVar.Text = "Specification"
        Me.colVar.Width = 100
        '
        'ColValue
        '
        Me.ColValue.Name = "ColValue"
        Me.ColValue.Text = "Value"
        Me.ColValue.Width = 120
        '
        'palBOM
        '
        Me.palBOM.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.palBOM.Controls.Add(Me.btnUpdate)
        Me.palBOM.Controls.Add(Me.btnExcel)
        Me.palBOM.Controls.Add(Me.btnBack)
        Me.palBOM.Controls.Add(Me.dgvBOM)
        Me.palBOM.Controls.Add(Me.TreeBOM)
        Me.palBOM.Location = New System.Drawing.Point(0, 70)
        Me.palBOM.Name = "palBOM"
        Me.palBOM.Size = New System.Drawing.Size(1015, 589)
        Me.palBOM.TabIndex = 2
        Me.palBOM.Visible = False
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(711, 559)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(90, 20)
        Me.btnUpdate.TabIndex = 4
        Me.btnUpdate.Text = "Update BOM"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnExcel
        '
        Me.btnExcel.Location = New System.Drawing.Point(901, 559)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(104, 20)
        Me.btnExcel.TabIndex = 3
        Me.btnExcel.Text = "Export to EXCEL"
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(806, 559)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(90, 20)
        Me.btnBack.TabIndex = 2
        Me.btnBack.Text = "Back to Search"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'dgvBOM
        '
        Me.dgvBOM.AllowUserToAddRows = False
        Me.dgvBOM.AllowUserToDeleteRows = False
        Me.dgvBOM.AllowUserToResizeRows = False
        Me.dgvBOM.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvBOM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBOM.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colBOM, Me.ColBOMPartID, Me.colQty, Me.ColManu, Me.ColDesp, Me.ColNote})
        Me.dgvBOM.Location = New System.Drawing.Point(210, 0)
        Me.dgvBOM.Name = "dgvBOM"
        Me.dgvBOM.RowTemplate.Height = 25
        Me.dgvBOM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBOM.Size = New System.Drawing.Size(802, 444)
        Me.dgvBOM.TabIndex = 1
        '
        'colBOM
        '
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ControlLight
        Me.colBOM.DefaultCellStyle = DataGridViewCellStyle7
        Me.colBOM.HeaderText = "BOM Name"
        Me.colBOM.Name = "colBOM"
        Me.colBOM.ReadOnly = True
        '
        'ColBOMPartID
        '
        Me.ColBOMPartID.HeaderText = "Part ID"
        Me.ColBOMPartID.Name = "ColBOMPartID"
        Me.ColBOMPartID.Width = 120
        '
        'colQty
        '
        Me.colQty.HeaderText = "Qty"
        Me.colQty.Name = "colQty"
        Me.colQty.Width = 50
        '
        'ColManu
        '
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ColManu.DefaultCellStyle = DataGridViewCellStyle8
        Me.ColManu.HeaderText = "Manufacturer"
        Me.ColManu.Name = "ColManu"
        Me.ColManu.ReadOnly = True
        Me.ColManu.Width = 120
        '
        'ColDesp
        '
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ColDesp.DefaultCellStyle = DataGridViewCellStyle9
        Me.ColDesp.HeaderText = "Description"
        Me.ColDesp.Name = "ColDesp"
        Me.ColDesp.ReadOnly = True
        Me.ColDesp.Width = 180
        '
        'ColNote
        '
        Me.ColNote.HeaderText = "Note"
        Me.ColNote.Name = "ColNote"
        Me.ColNote.Width = 150
        '
        'TreeBOM
        '
        Me.TreeBOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TreeBOM.Dock = System.Windows.Forms.DockStyle.Left
        Me.TreeBOM.Location = New System.Drawing.Point(0, 0)
        Me.TreeBOM.Name = "TreeBOM"
        Me.TreeBOM.Size = New System.Drawing.Size(209, 589)
        Me.TreeBOM.TabIndex = 0
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1015, 660)
        Me.Controls.Add(Me.palSearch)
        Me.Controls.Add(Me.splPart)
        Me.Controls.Add(Me.palBOM)
        Me.HelpButton = True
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.Text = "Form1"
        Me.palSearch.ResumeLayout(False)
        Me.palSearch.PerformLayout()
        Me.splPart.Panel1.ResumeLayout(False)
        Me.splPart.Panel2.ResumeLayout(False)
        CType(Me.splPart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splPart.ResumeLayout(False)
        Me.grpFilter.ResumeLayout(False)
        Me.grpFilter.PerformLayout()
        Me.palDetail.ResumeLayout(False)
        Me.palAddBOM.ResumeLayout(False)
        Me.palAddBOM.PerformLayout()
        Me.palBOM.ResumeLayout(False)
        CType(Me.dgvBOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents palSearch As Panel
    Friend WithEvents splPart As SplitContainer
    Friend WithEvents lstPart As ListView
    Friend WithEvents palDetail As Panel
    Friend WithEvents grpFilter As GroupBox
    Friend WithEvents btnBOM As Button
    Friend WithEvents btnClean As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents palBOM As Panel
    Friend WithEvents colPartID As ColumnHeader
    Friend WithEvents ColManufacturer As ColumnHeader
    Friend WithEvents ColCerti1 As ColumnHeader
    Friend WithEvents ColCerti2 As ColumnHeader
    Friend WithEvents ColMaterial As ColumnHeader
    Friend WithEvents ColHighlight As ColumnHeader
    Friend WithEvents ColDescription As ColumnHeader
    Friend WithEvents ColType As ColumnHeader
    Friend WithEvents clbPartType As CheckedListBox
    Friend WithEvents palAddBOM As Panel
    Friend WithEvents lstDetail As ListView
    Friend WithEvents colVar As ColumnHeader
    Friend WithEvents ColValue As ColumnHeader
    Friend WithEvents radExisting As RadioButton
    Friend WithEvents radNew As RadioButton
    Friend WithEvents cboBOM As ComboBox
    Friend WithEvents btnAddBOM As Button
    Friend WithEvents txtQty As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnExcel As Button
    Friend WithEvents btnBack As Button
    Friend WithEvents dgvBOM As DataGridView
    Friend WithEvents TreeBOM As TreeView
    Friend WithEvents btnUpdate As Button
    Friend WithEvents lblMaterial As Label
    Friend WithEvents clbMaterial As CheckedListBox
    Friend WithEvents lblManufacturer As Label
    Friend WithEvents clbManufacturer As CheckedListBox
    Friend WithEvents lblPartType As Label
    Friend WithEvents btnFilter As Button
    Friend WithEvents colBOM As DataGridViewTextBoxColumn
    Friend WithEvents ColBOMPartID As DataGridViewTextBoxColumn
    Friend WithEvents colQty As DataGridViewTextBoxColumn
    Friend WithEvents ColManu As DataGridViewTextBoxColumn
    Friend WithEvents ColDesp As DataGridViewTextBoxColumn
    Friend WithEvents ColNote As DataGridViewTextBoxColumn
End Class
