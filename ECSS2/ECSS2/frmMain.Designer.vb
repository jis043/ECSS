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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.palSearch = New System.Windows.Forms.Panel()
        Me.txtSearchSuggest = New CustomControls.TextBoxEmailAutocomplete()
        Me.btnSetting = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnClean = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.splPart = New System.Windows.Forms.SplitContainer()
        Me.palFilter = New System.Windows.Forms.Panel()
        Me.palGroup = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.clbGroup = New System.Windows.Forms.CheckedListBox()
        Me.palClass = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.clbClass = New System.Windows.Forms.CheckedListBox()
        Me.palInputPhase = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.clbInputPhase = New System.Windows.Forms.CheckedListBox()
        Me.palNormalV = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.clbNormalV = New System.Windows.Forms.CheckedListBox()
        Me.palOutputA = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.clbOutputA = New System.Windows.Forms.CheckedListBox()
        Me.palOutputV = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.clbOutputVol = New System.Windows.Forms.CheckedListBox()
        Me.palNEMA = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.clbNEMA = New System.Windows.Forms.CheckedListBox()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.palMount = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.clbMount = New System.Windows.Forms.CheckedListBox()
        Me.palDepth = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.clbDepth = New System.Windows.Forms.CheckedListBox()
        Me.palWidth = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.clbWidth = New System.Windows.Forms.CheckedListBox()
        Me.palHeight = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.clbHeight = New System.Windows.Forms.CheckedListBox()
        Me.palMaterial = New System.Windows.Forms.Panel()
        Me.lblMaterial = New System.Windows.Forms.Label()
        Me.clbMaterial = New System.Windows.Forms.CheckedListBox()
        Me.palCertificates = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.clbCertificates = New System.Windows.Forms.CheckedListBox()
        Me.palManufacturer = New System.Windows.Forms.Panel()
        Me.lblManufacturer = New System.Windows.Forms.Label()
        Me.clbManufacturer = New System.Windows.Forms.CheckedListBox()
        Me.palCate = New System.Windows.Forms.Panel()
        Me.lblPartType = New System.Windows.Forms.Label()
        Me.clbPartType = New System.Windows.Forms.CheckedListBox()
        Me.splDetail = New System.Windows.Forms.SplitContainer()
        Me.lstPart = New System.Windows.Forms.ListView()
        Me.colPartID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColManufacturer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColCerti1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColMaterial = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColHighlight = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColDescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.palDetail = New System.Windows.Forms.Panel()
        Me.palAddBOM = New System.Windows.Forms.Panel()
        Me.btnBOM = New System.Windows.Forms.Button()
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
        Me.txtBOMTitle = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnBottom = New System.Windows.Forms.Button()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.btnTop = New System.Windows.Forms.Button()
        Me.btnDelOnePart = New System.Windows.Forms.Button()
        Me.btnAddOnePart = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnExcel = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.dgvBOM = New System.Windows.Forms.DataGridView()
        Me.colIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColBOMPartID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColManu = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDesp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNote = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TreeBOM = New System.Windows.Forms.TreeView()
        Me.MyContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.palSearch.SuspendLayout()
        CType(Me.splPart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splPart.Panel1.SuspendLayout()
        Me.splPart.Panel2.SuspendLayout()
        Me.splPart.SuspendLayout()
        Me.palFilter.SuspendLayout()
        Me.palGroup.SuspendLayout()
        Me.palClass.SuspendLayout()
        Me.palInputPhase.SuspendLayout()
        Me.palNormalV.SuspendLayout()
        Me.palOutputA.SuspendLayout()
        Me.palOutputV.SuspendLayout()
        Me.palNEMA.SuspendLayout()
        Me.palMount.SuspendLayout()
        Me.palDepth.SuspendLayout()
        Me.palWidth.SuspendLayout()
        Me.palHeight.SuspendLayout()
        Me.palMaterial.SuspendLayout()
        Me.palCertificates.SuspendLayout()
        Me.palManufacturer.SuspendLayout()
        Me.palCate.SuspendLayout()
        CType(Me.splDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splDetail.Panel1.SuspendLayout()
        Me.splDetail.Panel2.SuspendLayout()
        Me.splDetail.SuspendLayout()
        Me.palDetail.SuspendLayout()
        Me.palAddBOM.SuspendLayout()
        Me.palBOM.SuspendLayout()
        CType(Me.dgvBOM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'palSearch
        '
        Me.palSearch.Controls.Add(Me.txtSearchSuggest)
        Me.palSearch.Controls.Add(Me.btnSetting)
        Me.palSearch.Controls.Add(Me.txtSearch)
        Me.palSearch.Controls.Add(Me.btnClean)
        Me.palSearch.Controls.Add(Me.btnSearch)
        Me.palSearch.Controls.Add(Me.Label1)
        Me.palSearch.Controls.Add(Me.splPart)
        Me.palSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.palSearch.Location = New System.Drawing.Point(0, 0)
        Me.palSearch.Name = "palSearch"
        Me.palSearch.Size = New System.Drawing.Size(1064, 681)
        Me.palSearch.TabIndex = 0
        '
        'txtSearchSuggest
        '
        Me.txtSearchSuggest.EmailAutocompleteSource = Nothing
        Me.txtSearchSuggest.HighlightColor = System.Drawing.SystemColors.ControlLight
        Me.txtSearchSuggest.Location = New System.Drawing.Point(176, 39)
        Me.txtSearchSuggest.MinimumSize = New System.Drawing.Size(103, 246)
        Me.txtSearchSuggest.Name = "txtSearchSuggest"
        Me.txtSearchSuggest.Size = New System.Drawing.Size(427, 246)
        Me.txtSearchSuggest.TabIndex = 5
        Me.txtSearchSuggest.TextColor = System.Drawing.SystemColors.WindowText
        '
        'btnSetting
        '
        Me.btnSetting.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSetting.BackgroundImage = CType(resources.GetObject("btnSetting.BackgroundImage"), System.Drawing.Image)
        Me.btnSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSetting.Location = New System.Drawing.Point(1032, 30)
        Me.btnSetting.Margin = New System.Windows.Forms.Padding(1)
        Me.btnSetting.Name = "btnSetting"
        Me.btnSetting.Size = New System.Drawing.Size(23, 23)
        Me.btnSetting.TabIndex = 4
        Me.btnSetting.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Location = New System.Drawing.Point(176, 9)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(427, 20)
        Me.txtSearch.TabIndex = 1
        '
        'btnClean
        '
        Me.btnClean.Location = New System.Drawing.Point(698, 9)
        Me.btnClean.Name = "btnClean"
        Me.btnClean.Size = New System.Drawing.Size(77, 23)
        Me.btnClean.TabIndex = 3
        Me.btnClean.Text = "Clean"
        Me.btnClean.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(615, 8)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(77, 23)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Engineering Component Search:"
        '
        'splPart
        '
        Me.splPart.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splPart.Location = New System.Drawing.Point(0, 69)
        Me.splPart.Name = "splPart"
        '
        'splPart.Panel1
        '
        Me.splPart.Panel1.Controls.Add(Me.palFilter)
        '
        'splPart.Panel2
        '
        Me.splPart.Panel2.Controls.Add(Me.splDetail)
        Me.splPart.Size = New System.Drawing.Size(1064, 611)
        Me.splPart.SplitterDistance = 130
        Me.splPart.SplitterWidth = 2
        Me.splPart.TabIndex = 1
        '
        'palFilter
        '
        Me.palFilter.AutoScroll = True
        Me.palFilter.Controls.Add(Me.palGroup)
        Me.palFilter.Controls.Add(Me.palClass)
        Me.palFilter.Controls.Add(Me.palInputPhase)
        Me.palFilter.Controls.Add(Me.palNormalV)
        Me.palFilter.Controls.Add(Me.palOutputA)
        Me.palFilter.Controls.Add(Me.palOutputV)
        Me.palFilter.Controls.Add(Me.palNEMA)
        Me.palFilter.Controls.Add(Me.lblLocation)
        Me.palFilter.Controls.Add(Me.palMount)
        Me.palFilter.Controls.Add(Me.palDepth)
        Me.palFilter.Controls.Add(Me.palWidth)
        Me.palFilter.Controls.Add(Me.palHeight)
        Me.palFilter.Controls.Add(Me.palMaterial)
        Me.palFilter.Controls.Add(Me.palCertificates)
        Me.palFilter.Controls.Add(Me.palManufacturer)
        Me.palFilter.Controls.Add(Me.palCate)
        Me.palFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.palFilter.Location = New System.Drawing.Point(0, 0)
        Me.palFilter.Name = "palFilter"
        Me.palFilter.Size = New System.Drawing.Size(130, 611)
        Me.palFilter.TabIndex = 1
        '
        'palGroup
        '
        Me.palGroup.Controls.Add(Me.Label16)
        Me.palGroup.Controls.Add(Me.clbGroup)
        Me.palGroup.Dock = System.Windows.Forms.DockStyle.Top
        Me.palGroup.Location = New System.Drawing.Point(0, 1797)
        Me.palGroup.Name = "palGroup"
        Me.palGroup.Size = New System.Drawing.Size(113, 128)
        Me.palGroup.TabIndex = 25
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(1, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(36, 13)
        Me.Label16.TabIndex = 11
        Me.Label16.Text = "Group"
        '
        'clbGroup
        '
        Me.clbGroup.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbGroup.FormattingEnabled = True
        Me.clbGroup.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbGroup.Location = New System.Drawing.Point(3, 16)
        Me.clbGroup.Name = "clbGroup"
        Me.clbGroup.Size = New System.Drawing.Size(107, 107)
        Me.clbGroup.TabIndex = 10
        '
        'palClass
        '
        Me.palClass.Controls.Add(Me.Label15)
        Me.palClass.Controls.Add(Me.clbClass)
        Me.palClass.Dock = System.Windows.Forms.DockStyle.Top
        Me.palClass.Location = New System.Drawing.Point(0, 1669)
        Me.palClass.Name = "palClass"
        Me.palClass.Size = New System.Drawing.Size(113, 128)
        Me.palClass.TabIndex = 24
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(1, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(32, 13)
        Me.Label15.TabIndex = 11
        Me.Label15.Text = "Class"
        '
        'clbClass
        '
        Me.clbClass.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbClass.FormattingEnabled = True
        Me.clbClass.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbClass.Location = New System.Drawing.Point(3, 16)
        Me.clbClass.Name = "clbClass"
        Me.clbClass.Size = New System.Drawing.Size(107, 107)
        Me.clbClass.TabIndex = 10
        '
        'palInputPhase
        '
        Me.palInputPhase.Controls.Add(Me.Label14)
        Me.palInputPhase.Controls.Add(Me.clbInputPhase)
        Me.palInputPhase.Dock = System.Windows.Forms.DockStyle.Top
        Me.palInputPhase.Location = New System.Drawing.Point(0, 1541)
        Me.palInputPhase.Name = "palInputPhase"
        Me.palInputPhase.Size = New System.Drawing.Size(113, 128)
        Me.palInputPhase.TabIndex = 23
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(3, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(64, 13)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "Input Phase"
        '
        'clbInputPhase
        '
        Me.clbInputPhase.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbInputPhase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbInputPhase.FormattingEnabled = True
        Me.clbInputPhase.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbInputPhase.Location = New System.Drawing.Point(3, 16)
        Me.clbInputPhase.Name = "clbInputPhase"
        Me.clbInputPhase.Size = New System.Drawing.Size(107, 107)
        Me.clbInputPhase.TabIndex = 10
        '
        'palNormalV
        '
        Me.palNormalV.Controls.Add(Me.Label13)
        Me.palNormalV.Controls.Add(Me.clbNormalV)
        Me.palNormalV.Dock = System.Windows.Forms.DockStyle.Top
        Me.palNormalV.Location = New System.Drawing.Point(0, 1413)
        Me.palNormalV.Name = "palNormalV"
        Me.palNormalV.Size = New System.Drawing.Size(113, 128)
        Me.palNormalV.TabIndex = 22
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(1, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(79, 13)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "Normal Voltage"
        '
        'clbNormalV
        '
        Me.clbNormalV.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbNormalV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbNormalV.FormattingEnabled = True
        Me.clbNormalV.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbNormalV.Location = New System.Drawing.Point(3, 16)
        Me.clbNormalV.Name = "clbNormalV"
        Me.clbNormalV.Size = New System.Drawing.Size(107, 107)
        Me.clbNormalV.TabIndex = 10
        '
        'palOutputA
        '
        Me.palOutputA.Controls.Add(Me.Label12)
        Me.palOutputA.Controls.Add(Me.clbOutputA)
        Me.palOutputA.Dock = System.Windows.Forms.DockStyle.Top
        Me.palOutputA.Location = New System.Drawing.Point(0, 1285)
        Me.palOutputA.Name = "palOutputA"
        Me.palOutputA.Size = New System.Drawing.Size(113, 128)
        Me.palOutputA.TabIndex = 21
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(1, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(76, 13)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Output Current"
        '
        'clbOutputA
        '
        Me.clbOutputA.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbOutputA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbOutputA.FormattingEnabled = True
        Me.clbOutputA.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbOutputA.Location = New System.Drawing.Point(3, 16)
        Me.clbOutputA.Name = "clbOutputA"
        Me.clbOutputA.Size = New System.Drawing.Size(107, 107)
        Me.clbOutputA.TabIndex = 10
        '
        'palOutputV
        '
        Me.palOutputV.Controls.Add(Me.Label11)
        Me.palOutputV.Controls.Add(Me.clbOutputVol)
        Me.palOutputV.Dock = System.Windows.Forms.DockStyle.Top
        Me.palOutputV.Location = New System.Drawing.Point(0, 1157)
        Me.palOutputV.Name = "palOutputV"
        Me.palOutputV.Size = New System.Drawing.Size(113, 128)
        Me.palOutputV.TabIndex = 20
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(1, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(78, 13)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "Output Voltage"
        '
        'clbOutputVol
        '
        Me.clbOutputVol.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbOutputVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbOutputVol.FormattingEnabled = True
        Me.clbOutputVol.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbOutputVol.Location = New System.Drawing.Point(3, 16)
        Me.clbOutputVol.Name = "clbOutputVol"
        Me.clbOutputVol.Size = New System.Drawing.Size(107, 107)
        Me.clbOutputVol.TabIndex = 10
        '
        'palNEMA
        '
        Me.palNEMA.Controls.Add(Me.Label10)
        Me.palNEMA.Controls.Add(Me.clbNEMA)
        Me.palNEMA.Dock = System.Windows.Forms.DockStyle.Top
        Me.palNEMA.Location = New System.Drawing.Point(0, 1029)
        Me.palNEMA.Name = "palNEMA"
        Me.palNEMA.Size = New System.Drawing.Size(113, 128)
        Me.palNEMA.TabIndex = 19
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(1, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(38, 13)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "NEMA"
        '
        'clbNEMA
        '
        Me.clbNEMA.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbNEMA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbNEMA.FormattingEnabled = True
        Me.clbNEMA.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbNEMA.Location = New System.Drawing.Point(3, 16)
        Me.clbNEMA.Name = "clbNEMA"
        Me.clbNEMA.Size = New System.Drawing.Size(107, 107)
        Me.clbNEMA.TabIndex = 10
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = True
        Me.lblLocation.Location = New System.Drawing.Point(35, 1900)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(35, 13)
        Me.lblLocation.TabIndex = 19
        Me.lblLocation.Text = "TEST"
        Me.lblLocation.Visible = False
        '
        'palMount
        '
        Me.palMount.Controls.Add(Me.Label9)
        Me.palMount.Controls.Add(Me.clbMount)
        Me.palMount.Dock = System.Windows.Forms.DockStyle.Top
        Me.palMount.Location = New System.Drawing.Point(0, 901)
        Me.palMount.Name = "palMount"
        Me.palMount.Size = New System.Drawing.Size(113, 128)
        Me.palMount.TabIndex = 18
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(1, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(37, 13)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Mount"
        '
        'clbMount
        '
        Me.clbMount.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbMount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbMount.FormattingEnabled = True
        Me.clbMount.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbMount.Location = New System.Drawing.Point(3, 16)
        Me.clbMount.Name = "clbMount"
        Me.clbMount.Size = New System.Drawing.Size(107, 107)
        Me.clbMount.TabIndex = 10
        '
        'palDepth
        '
        Me.palDepth.Controls.Add(Me.Label8)
        Me.palDepth.Controls.Add(Me.clbDepth)
        Me.palDepth.Dock = System.Windows.Forms.DockStyle.Top
        Me.palDepth.Location = New System.Drawing.Point(0, 773)
        Me.palDepth.Name = "palDepth"
        Me.palDepth.Size = New System.Drawing.Size(113, 128)
        Me.palDepth.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(36, 13)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Depth"
        '
        'clbDepth
        '
        Me.clbDepth.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbDepth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbDepth.FormattingEnabled = True
        Me.clbDepth.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbDepth.Location = New System.Drawing.Point(3, 16)
        Me.clbDepth.Name = "clbDepth"
        Me.clbDepth.Size = New System.Drawing.Size(107, 107)
        Me.clbDepth.TabIndex = 10
        '
        'palWidth
        '
        Me.palWidth.Controls.Add(Me.Label7)
        Me.palWidth.Controls.Add(Me.clbWidth)
        Me.palWidth.Dock = System.Windows.Forms.DockStyle.Top
        Me.palWidth.Location = New System.Drawing.Point(0, 645)
        Me.palWidth.Name = "palWidth"
        Me.palWidth.Size = New System.Drawing.Size(113, 128)
        Me.palWidth.TabIndex = 17
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(1, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Width"
        '
        'clbWidth
        '
        Me.clbWidth.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbWidth.FormattingEnabled = True
        Me.clbWidth.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbWidth.Location = New System.Drawing.Point(3, 16)
        Me.clbWidth.Name = "clbWidth"
        Me.clbWidth.Size = New System.Drawing.Size(107, 107)
        Me.clbWidth.TabIndex = 10
        '
        'palHeight
        '
        Me.palHeight.Controls.Add(Me.Label6)
        Me.palHeight.Controls.Add(Me.clbHeight)
        Me.palHeight.Dock = System.Windows.Forms.DockStyle.Top
        Me.palHeight.Location = New System.Drawing.Point(0, 517)
        Me.palHeight.Name = "palHeight"
        Me.palHeight.Size = New System.Drawing.Size(113, 128)
        Me.palHeight.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(1, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Height"
        '
        'clbHeight
        '
        Me.clbHeight.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbHeight.FormattingEnabled = True
        Me.clbHeight.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbHeight.Location = New System.Drawing.Point(3, 16)
        Me.clbHeight.Name = "clbHeight"
        Me.clbHeight.Size = New System.Drawing.Size(107, 107)
        Me.clbHeight.TabIndex = 10
        '
        'palMaterial
        '
        Me.palMaterial.Controls.Add(Me.lblMaterial)
        Me.palMaterial.Controls.Add(Me.clbMaterial)
        Me.palMaterial.Dock = System.Windows.Forms.DockStyle.Top
        Me.palMaterial.Location = New System.Drawing.Point(0, 389)
        Me.palMaterial.Name = "palMaterial"
        Me.palMaterial.Size = New System.Drawing.Size(113, 128)
        Me.palMaterial.TabIndex = 14
        '
        'lblMaterial
        '
        Me.lblMaterial.AutoSize = True
        Me.lblMaterial.Location = New System.Drawing.Point(1, 0)
        Me.lblMaterial.Name = "lblMaterial"
        Me.lblMaterial.Size = New System.Drawing.Size(44, 13)
        Me.lblMaterial.TabIndex = 11
        Me.lblMaterial.Text = "Material"
        '
        'clbMaterial
        '
        Me.clbMaterial.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbMaterial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbMaterial.FormattingEnabled = True
        Me.clbMaterial.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbMaterial.Location = New System.Drawing.Point(3, 16)
        Me.clbMaterial.Name = "clbMaterial"
        Me.clbMaterial.Size = New System.Drawing.Size(107, 107)
        Me.clbMaterial.TabIndex = 10
        '
        'palCertificates
        '
        Me.palCertificates.Controls.Add(Me.Label5)
        Me.palCertificates.Controls.Add(Me.clbCertificates)
        Me.palCertificates.Dock = System.Windows.Forms.DockStyle.Top
        Me.palCertificates.Location = New System.Drawing.Point(0, 261)
        Me.palCertificates.Name = "palCertificates"
        Me.palCertificates.Size = New System.Drawing.Size(113, 128)
        Me.palCertificates.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(1, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Certificates"
        '
        'clbCertificates
        '
        Me.clbCertificates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbCertificates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbCertificates.FormattingEnabled = True
        Me.clbCertificates.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbCertificates.Location = New System.Drawing.Point(3, 16)
        Me.clbCertificates.Name = "clbCertificates"
        Me.clbCertificates.Size = New System.Drawing.Size(107, 107)
        Me.clbCertificates.TabIndex = 10
        '
        'palManufacturer
        '
        Me.palManufacturer.Controls.Add(Me.lblManufacturer)
        Me.palManufacturer.Controls.Add(Me.clbManufacturer)
        Me.palManufacturer.Dock = System.Windows.Forms.DockStyle.Top
        Me.palManufacturer.Location = New System.Drawing.Point(0, 136)
        Me.palManufacturer.Name = "palManufacturer"
        Me.palManufacturer.Size = New System.Drawing.Size(113, 125)
        Me.palManufacturer.TabIndex = 13
        '
        'lblManufacturer
        '
        Me.lblManufacturer.AutoSize = True
        Me.lblManufacturer.Location = New System.Drawing.Point(0, 0)
        Me.lblManufacturer.Name = "lblManufacturer"
        Me.lblManufacturer.Size = New System.Drawing.Size(70, 13)
        Me.lblManufacturer.TabIndex = 9
        Me.lblManufacturer.Text = "Manufacturer"
        '
        'clbManufacturer
        '
        Me.clbManufacturer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbManufacturer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbManufacturer.FormattingEnabled = True
        Me.clbManufacturer.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbManufacturer.Location = New System.Drawing.Point(3, 16)
        Me.clbManufacturer.Name = "clbManufacturer"
        Me.clbManufacturer.Size = New System.Drawing.Size(107, 107)
        Me.clbManufacturer.TabIndex = 8
        '
        'palCate
        '
        Me.palCate.Controls.Add(Me.lblPartType)
        Me.palCate.Controls.Add(Me.clbPartType)
        Me.palCate.Dock = System.Windows.Forms.DockStyle.Top
        Me.palCate.Location = New System.Drawing.Point(0, 0)
        Me.palCate.Name = "palCate"
        Me.palCate.Size = New System.Drawing.Size(113, 136)
        Me.palCate.TabIndex = 12
        '
        'lblPartType
        '
        Me.lblPartType.AutoSize = True
        Me.lblPartType.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblPartType.Location = New System.Drawing.Point(0, 0)
        Me.lblPartType.Name = "lblPartType"
        Me.lblPartType.Size = New System.Drawing.Size(57, 13)
        Me.lblPartType.TabIndex = 7
        Me.lblPartType.Text = "Categories"
        '
        'clbPartType
        '
        Me.clbPartType.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.clbPartType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbPartType.FormattingEnabled = True
        Me.clbPartType.Items.AddRange(New Object() {"M20", "M25"})
        Me.clbPartType.Location = New System.Drawing.Point(3, 18)
        Me.clbPartType.Name = "clbPartType"
        Me.clbPartType.Size = New System.Drawing.Size(107, 107)
        Me.clbPartType.TabIndex = 6
        '
        'splDetail
        '
        Me.splDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splDetail.Location = New System.Drawing.Point(0, 0)
        Me.splDetail.Name = "splDetail"
        '
        'splDetail.Panel1
        '
        Me.splDetail.Panel1.Controls.Add(Me.lstPart)
        '
        'splDetail.Panel2
        '
        Me.splDetail.Panel2.Controls.Add(Me.palDetail)
        Me.splDetail.Size = New System.Drawing.Size(932, 611)
        Me.splDetail.SplitterDistance = 736
        Me.splDetail.TabIndex = 2
        '
        'lstPart
        '
        Me.lstPart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstPart.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colPartID, Me.ColManufacturer, Me.ColCerti1, Me.ColMaterial, Me.ColHighlight, Me.ColDescription, Me.ColType})
        Me.lstPart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstPart.FullRowSelect = True
        Me.lstPart.GridLines = True
        Me.lstPart.HideSelection = False
        Me.lstPart.Location = New System.Drawing.Point(0, 0)
        Me.lstPart.MultiSelect = False
        Me.lstPart.Name = "lstPart"
        Me.lstPart.Size = New System.Drawing.Size(736, 611)
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
        Me.ColCerti1.Text = "Certificates"
        Me.ColCerti1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColCerti1.Width = 130
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
        Me.palDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.palDetail.Location = New System.Drawing.Point(0, 0)
        Me.palDetail.Name = "palDetail"
        Me.palDetail.Size = New System.Drawing.Size(192, 611)
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
        Me.palAddBOM.Location = New System.Drawing.Point(0, 475)
        Me.palAddBOM.Name = "palAddBOM"
        Me.palAddBOM.Size = New System.Drawing.Size(192, 136)
        Me.palAddBOM.TabIndex = 1
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
        Me.lstDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstDetail.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colVar, Me.ColValue})
        Me.lstDetail.GridLines = True
        Me.lstDetail.HideSelection = False
        Me.lstDetail.Location = New System.Drawing.Point(0, 0)
        Me.lstDetail.Name = "lstDetail"
        Me.lstDetail.Size = New System.Drawing.Size(189, 469)
        Me.lstDetail.TabIndex = 0
        Me.lstDetail.UseCompatibleStateImageBehavior = False
        Me.lstDetail.View = System.Windows.Forms.View.Details
        '
        'colVar
        '
        Me.colVar.Name = "colVar"
        Me.colVar.Text = "Specification"
        Me.colVar.Width = 80
        '
        'ColValue
        '
        Me.ColValue.Name = "ColValue"
        Me.ColValue.Text = "Value"
        Me.ColValue.Width = 100
        '
        'palBOM
        '
        Me.palBOM.Controls.Add(Me.txtBOMTitle)
        Me.palBOM.Controls.Add(Me.Label4)
        Me.palBOM.Controls.Add(Me.btnBottom)
        Me.palBOM.Controls.Add(Me.btnDown)
        Me.palBOM.Controls.Add(Me.btnUp)
        Me.palBOM.Controls.Add(Me.btnTop)
        Me.palBOM.Controls.Add(Me.btnDelOnePart)
        Me.palBOM.Controls.Add(Me.btnAddOnePart)
        Me.palBOM.Controls.Add(Me.btnUpdate)
        Me.palBOM.Controls.Add(Me.btnExcel)
        Me.palBOM.Controls.Add(Me.btnBack)
        Me.palBOM.Controls.Add(Me.dgvBOM)
        Me.palBOM.Controls.Add(Me.TreeBOM)
        Me.palBOM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.palBOM.Location = New System.Drawing.Point(0, 0)
        Me.palBOM.Name = "palBOM"
        Me.palBOM.Size = New System.Drawing.Size(1064, 681)
        Me.palBOM.TabIndex = 2
        Me.palBOM.Visible = False
        '
        'txtBOMTitle
        '
        Me.txtBOMTitle.Location = New System.Drawing.Point(250, 13)
        Me.txtBOMTitle.Name = "txtBOMTitle"
        Me.txtBOMTitle.Size = New System.Drawing.Size(359, 20)
        Me.txtBOMTitle.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(179, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "BOM Name:"
        '
        'btnBottom
        '
        Me.btnBottom.Location = New System.Drawing.Point(980, 184)
        Me.btnBottom.Name = "btnBottom"
        Me.btnBottom.Size = New System.Drawing.Size(75, 23)
        Me.btnBottom.TabIndex = 10
        Me.btnBottom.Text = "Bottom"
        Me.btnBottom.UseVisualStyleBackColor = True
        '
        'btnDown
        '
        Me.btnDown.Location = New System.Drawing.Point(980, 155)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(75, 23)
        Me.btnDown.TabIndex = 9
        Me.btnDown.Text = "Down"
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.Location = New System.Drawing.Point(980, 126)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(75, 23)
        Me.btnUp.TabIndex = 8
        Me.btnUp.Text = "Up"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'btnTop
        '
        Me.btnTop.Location = New System.Drawing.Point(980, 97)
        Me.btnTop.Name = "btnTop"
        Me.btnTop.Size = New System.Drawing.Size(75, 23)
        Me.btnTop.TabIndex = 7
        Me.btnTop.Text = "Top"
        Me.btnTop.UseVisualStyleBackColor = True
        '
        'btnDelOnePart
        '
        Me.btnDelOnePart.Location = New System.Drawing.Point(980, 68)
        Me.btnDelOnePart.Name = "btnDelOnePart"
        Me.btnDelOnePart.Size = New System.Drawing.Size(75, 23)
        Me.btnDelOnePart.TabIndex = 6
        Me.btnDelOnePart.Text = "Delete"
        Me.btnDelOnePart.UseVisualStyleBackColor = True
        '
        'btnAddOnePart
        '
        Me.btnAddOnePart.Location = New System.Drawing.Point(980, 39)
        Me.btnAddOnePart.Name = "btnAddOnePart"
        Me.btnAddOnePart.Size = New System.Drawing.Size(75, 23)
        Me.btnAddOnePart.TabIndex = 5
        Me.btnAddOnePart.Text = "Add"
        Me.btnAddOnePart.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(759, 646)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(90, 23)
        Me.btnUpdate.TabIndex = 4
        Me.btnUpdate.Text = "Save Changes"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnExcel
        '
        Me.btnExcel.Location = New System.Drawing.Point(951, 646)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(104, 23)
        Me.btnExcel.TabIndex = 3
        Me.btnExcel.Text = "Export to EXCEL"
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(855, 646)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(90, 23)
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
        Me.dgvBOM.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colIndex, Me.ColBOMPartID, Me.colQty, Me.ColManu, Me.ColDesp, Me.ColNote})
        Me.dgvBOM.Location = New System.Drawing.Point(172, 39)
        Me.dgvBOM.Name = "dgvBOM"
        Me.dgvBOM.RowTemplate.Height = 25
        Me.dgvBOM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBOM.Size = New System.Drawing.Size(802, 601)
        Me.dgvBOM.TabIndex = 1
        '
        'colIndex
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.colIndex.DefaultCellStyle = DataGridViewCellStyle1
        Me.colIndex.HeaderText = "Num"
        Me.colIndex.Name = "colIndex"
        Me.colIndex.ReadOnly = True
        Me.colIndex.Width = 70
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
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        Me.ColManu.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColManu.HeaderText = "Manufacturer"
        Me.ColManu.Name = "ColManu"
        Me.ColManu.Width = 120
        '
        'ColDesp
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        Me.ColDesp.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColDesp.HeaderText = "Description"
        Me.ColDesp.Name = "ColDesp"
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
        Me.TreeBOM.Size = New System.Drawing.Size(170, 681)
        Me.TreeBOM.TabIndex = 0
        '
        'MyContextMenu
        '
        Me.MyContextMenu.Name = "MyContextMenu"
        Me.MyContextMenu.Size = New System.Drawing.Size(61, 4)
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1064, 681)
        Me.Controls.Add(Me.palSearch)
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
        Me.palFilter.ResumeLayout(False)
        Me.palFilter.PerformLayout()
        Me.palGroup.ResumeLayout(False)
        Me.palGroup.PerformLayout()
        Me.palClass.ResumeLayout(False)
        Me.palClass.PerformLayout()
        Me.palInputPhase.ResumeLayout(False)
        Me.palInputPhase.PerformLayout()
        Me.palNormalV.ResumeLayout(False)
        Me.palNormalV.PerformLayout()
        Me.palOutputA.ResumeLayout(False)
        Me.palOutputA.PerformLayout()
        Me.palOutputV.ResumeLayout(False)
        Me.palOutputV.PerformLayout()
        Me.palNEMA.ResumeLayout(False)
        Me.palNEMA.PerformLayout()
        Me.palMount.ResumeLayout(False)
        Me.palMount.PerformLayout()
        Me.palDepth.ResumeLayout(False)
        Me.palDepth.PerformLayout()
        Me.palWidth.ResumeLayout(False)
        Me.palWidth.PerformLayout()
        Me.palHeight.ResumeLayout(False)
        Me.palHeight.PerformLayout()
        Me.palMaterial.ResumeLayout(False)
        Me.palMaterial.PerformLayout()
        Me.palCertificates.ResumeLayout(False)
        Me.palCertificates.PerformLayout()
        Me.palManufacturer.ResumeLayout(False)
        Me.palManufacturer.PerformLayout()
        Me.palCate.ResumeLayout(False)
        Me.palCate.PerformLayout()
        Me.splDetail.Panel1.ResumeLayout(False)
        Me.splDetail.Panel2.ResumeLayout(False)
        CType(Me.splDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splDetail.ResumeLayout(False)
        Me.palDetail.ResumeLayout(False)
        Me.palAddBOM.ResumeLayout(False)
        Me.palAddBOM.PerformLayout()
        Me.palBOM.ResumeLayout(False)
        Me.palBOM.PerformLayout()
        CType(Me.dgvBOM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents palSearch As Panel
    Friend WithEvents splPart As SplitContainer
    Friend WithEvents lstPart As ListView
    Friend WithEvents palDetail As Panel
    Friend WithEvents btnBOM As Button
    Friend WithEvents btnClean As Button
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents palBOM As Panel
    Friend WithEvents colPartID As ColumnHeader
    Friend WithEvents ColManufacturer As ColumnHeader
    Friend WithEvents ColCerti As ColumnHeader
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
    Friend WithEvents splDetail As SplitContainer
    Friend WithEvents txtBOMTitle As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents btnBottom As Button
    Friend WithEvents btnDown As Button
    Friend WithEvents btnUp As Button
    Friend WithEvents btnTop As Button
    Friend WithEvents btnDelOnePart As Button
    Friend WithEvents btnAddOnePart As Button
    Friend WithEvents btnSetting As Button
    Friend WithEvents colIndex As DataGridViewTextBoxColumn
    Friend WithEvents ColBOMPartID As DataGridViewTextBoxColumn
    Friend WithEvents colQty As DataGridViewTextBoxColumn
    Friend WithEvents ColManu As DataGridViewTextBoxColumn
    Friend WithEvents ColDesp As DataGridViewTextBoxColumn
    Friend WithEvents ColNote As DataGridViewTextBoxColumn
    Friend WithEvents ColCerti1 As ColumnHeader
    Friend WithEvents txtSearchSuggest As CustomControls.TextBoxEmailAutocomplete
    Friend WithEvents MyContextMenu As ContextMenuStrip
    Friend WithEvents palHeight As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents clbHeight As CheckedListBox
    Friend WithEvents palCertificates As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents clbCertificates As CheckedListBox
    Friend WithEvents palMaterial As Panel
    Friend WithEvents palManufacturer As Panel
    Friend WithEvents palCate As Panel
    Friend WithEvents palFilter As Panel
    Friend WithEvents palNormalV As Panel
    Friend WithEvents Label13 As Label
    Friend WithEvents clbNormalV As CheckedListBox
    Friend WithEvents palOutputA As Panel
    Friend WithEvents Label12 As Label
    Friend WithEvents clbOutputA As CheckedListBox
    Friend WithEvents palOutputV As Panel
    Friend WithEvents Label11 As Label
    Friend WithEvents clbOutputVol As CheckedListBox
    Friend WithEvents palNEMA As Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents clbNEMA As CheckedListBox
    Friend WithEvents lblLocation As Label
    Friend WithEvents palMount As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents clbMount As CheckedListBox
    Friend WithEvents palDepth As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents clbDepth As CheckedListBox
    Friend WithEvents palWidth As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents clbWidth As CheckedListBox
    Friend WithEvents palGroup As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents clbGroup As CheckedListBox
    Friend WithEvents palClass As Panel
    Friend WithEvents Label15 As Label
    Friend WithEvents clbClass As CheckedListBox
    Friend WithEvents palInputPhase As Panel
    Friend WithEvents Label14 As Label
    Friend WithEvents clbInputPhase As CheckedListBox
End Class
