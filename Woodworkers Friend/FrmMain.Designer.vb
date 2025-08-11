<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Ss1 = New StatusStrip()
        TsslVersion = New ToolStripStatusLabel()
        TsslCpy = New ToolStripStatusLabel()
        TsslError = New ToolStripStatusLabel()
        TsslClock = New ToolStripStatusLabel()
        Ss2 = New StatusStrip()
        Ss3 = New StatusStrip()
        TsslToggleTheme = New ToolStripDropDownButton()
        TsslMemoriam = New ToolStripStatusLabel()
        TsslScale = New ToolStripStatusLabel()
        Tc = New TabControl()
        TpDrawersDoors = New TabPage()
        SplitContainer1 = New SplitContainer()
        LblAverageHeightResults = New Label()
        LblTotalMaterialResults = New Label()
        LblHeightRatioResults = New Label()
        LbltotalDrawerHeightResults = New Label()
        LblTotalHeightResults = New Label()
        LblStatus = New Label()
        GroupBox9 = New GroupBox()
        DgvDrawerHeights = New DataGridView()
        GroupBox8 = New GroupBox()
        Button7 = New Button()
        Button6 = New Button()
        BtnCalculateDrawers = New Button()
        GroupBox7 = New GroupBox()
        BtnUniformPreset = New Button()
        BtnCustomRatioPreset = New Button()
        BtnExponentialProgressionPreset = New Button()
        BtnLogarithmicProgressionPreset = New Button()
        BtnReverseArithmeticPreset = New Button()
        BtnGoldenRatioPreset = New Button()
        BtnCustomCabinetPreset = New Button()
        BtnBathroomVanityPreset = New Button()
        BtnOfficeDeskPreset = New Button()
        BtnKitchenStandardPreset = New Button()
        Label32 = New Label()
        GroupBox6 = New GroupBox()
        LblCustomRatioInput = New Label()
        TxtCustomRatioInput = New TextBox()
        TxtArithmeticIncrement = New TextBox()
        TxtMultiplier = New TextBox()
        TxtFirstDrawerHeight = New TextBox()
        Label31 = New Label()
        Label30 = New Label()
        Label29 = New Label()
        GroupBox5 = New GroupBox()
        TxtDrawerWidth = New TextBox()
        TxtDrawerSpacing = New TextBox()
        TxtDrawerCount = New TextBox()
        Label28 = New Label()
        Label27 = New Label()
        Label26 = New Label()
        PnlResults = New Panel()
        RtbResults = New RichTextBox()
        GroupBox11 = New GroupBox()
        RbArithmetic = New RadioButton()
        RbFibonacci = New RadioButton()
        RbGeometric = New RadioButton()
        RbHambridge = New RadioButton()
        RbGoldenRatio = New RadioButton()
        RbReverseArithmetic = New RadioButton()
        RbUniform = New RadioButton()
        RbCustomRatio = New RadioButton()
        RbExponential = New RadioButton()
        RbLogarithmic = New RadioButton()
        Label47 = New Label()
        Label33 = New Label()
        BtnSaveProject = New Button()
        Label34 = New Label()
        TxtProjectName = New TextBox()
        TpBoardfeet = New TabPage()
        PnlBoardFeet = New Panel()
        BtnPrtBfProject = New Button()
        TxtBfProjectName = New TextBox()
        BtnSaveBfProject = New Button()
        LblBoardFeetCost20 = New Label()
        LblTotalBoardFeet20 = New Label()
        Label12 = New Label()
        LblBoardFeetCost15 = New Label()
        LblTotalBoardFeet15 = New Label()
        Label9 = New Label()
        LblBoardFeetCost10 = New Label()
        LblTotalBoardFeet10 = New Label()
        Label6 = New Label()
        LblBoardFeetCost = New Label()
        LblTotalBoardFeet = New Label()
        Label1 = New Label()
        lblCalculateBoardfeet = New Label()
        DgvBoardfeet = New DataGridView()
        bfCol0 = New DataGridViewTextBoxColumn()
        bfCol1 = New DataGridViewTextBoxColumn()
        bfCol2 = New DataGridViewTextBoxColumn()
        bfCol3 = New DataGridViewTextBoxColumn()
        bfCol4 = New DataGridViewTextBoxColumn()
        bfCol5 = New DataGridViewTextBoxColumn()
        bfCol6 = New DataGridViewTextBoxColumn()
        TpCalculations = New TabPage()
        Panel5 = New Panel()
        LblTippingForce = New Label()
        TxtTtTableBaseWeight = New TextBox()
        TxtTtTableBaselength = New TextBox()
        TxtTtTableTopWeight = New TextBox()
        TxtTtTableTopLength = New TextBox()
        Label25 = New Label()
        Label24 = New Label()
        Label22 = New Label()
        Label23 = New Label()
        Label21 = New Label()
        Label20 = New Label()
        Label18 = New Label()
        Label15 = New Label()
        Panel4 = New Panel()
        GroupBox4 = New GroupBox()
        LblFraction2Decimal = New Label()
        TxtFraction2Decimal = New TextBox()
        Label16 = New Label()
        GroupBox3 = New GroupBox()
        LblDecimal2Fraction = New Label()
        TxtDecimal2Fraction = New TextBox()
        Label19 = New Label()
        GroupBox2 = New GroupBox()
        LblMM2Inches = New Label()
        TxtMm2Inches = New TextBox()
        Label17 = New Label()
        GroupBox1 = New GroupBox()
        LblInches2MM = New Label()
        TxtInches2Mm = New TextBox()
        Label14 = New Label()
        Label13 = New Label()
        Panel3 = New Panel()
        LblPolygonPieceAngle = New Label()
        LblPolygonSideAngle = New Label()
        TxtPolygonSides = New TextBox()
        Label11 = New Label()
        PbPolygon = New PictureBox()
        Panel2 = New Panel()
        RtbFraction2Mm = New RichTextBox()
        RtbFraction2Decimal = New RichTextBox()
        Label10 = New Label()
        Label8 = New Label()
        Panel1 = New Panel()
        RbEpoxyWaste20 = New RadioButton()
        RbEpoxyWaste15 = New RadioButton()
        RbEpoxyWaste10 = New RadioButton()
        RbEpoxyWaste0 = New RadioButton()
        Label7 = New Label()
        TxtEpoxyDepth = New TextBox()
        TxtEpoxyWidth = New TextBox()
        TxtEpoxyLength = New TextBox()
        LblEpoxyPints = New Label()
        LblEpoxyQuarts = New Label()
        LblEpoxyGallons = New Label()
        LblEpoxyOunces = New Label()
        Label5 = New Label()
        Label4 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        TpLogs = New TabPage()
        RbMetric = New RadioButton()
        RbImperial = New RadioButton()
        RtbLog = New RichTextBox()
        TpDoors = New TabPage()
        ScDoors = New SplitContainer()
        BtnDeleteDoorProject = New Button()
        BtnLoadDoorProject = New Button()
        GroupBox12 = New GroupBox()
        Button1 = New Button()
        BtnOfficeDoorPreset = New Button()
        BtnBathroomDoorPreset = New Button()
        BtnKitchenDoorPreset = New Button()
        Label48 = New Label()
        Panel6 = New Panel()
        TxtCabinetOpeningHeight = New TextBox()
        TxtCabinetOpeningWidth = New TextBox()
        Label35 = New Label()
        Label36 = New Label()
        Label37 = New Label()
        GroupBox10 = New GroupBox()
        LblPanelWidth = New Label()
        LblPanelHeight = New Label()
        Label46 = New Label()
        LblStileLength = New Label()
        LblRailLength = New Label()
        BtnCalculateDoors = New Button()
        BtnSaveDoorProject = New Button()
        Panel8 = New Panel()
        TxtDoorOverlay = New TextBox()
        TxtGapSize = New TextBox()
        TxtRailWidth = New TextBox()
        TxtStileWidth = New TextBox()
        Label41 = New Label()
        Label42 = New Label()
        Label43 = New Label()
        Panel9 = New Panel()
        RbInset = New RadioButton()
        RbOverlay = New RadioButton()
        Label44 = New Label()
        Panel10 = New Panel()
        Rb2Door = New RadioButton()
        Rb1Door = New RadioButton()
        Label45 = New Label()
        TxtDoorProjectName = New TextBox()
        Panel7 = New Panel()
        TxtPanelExpansionGap = New TextBox()
        TxtPanelGrooveDepth = New TextBox()
        Label38 = New Label()
        Label39 = New Label()
        Label40 = New Label()
        BtnPrintDoorResults = New Button()
        BtnExportDoorResults = New Button()
        Label50 = New Label()
        PnlDoorResults = New Panel()
        RtbDoorResults = New RichTextBox()
        Label49 = New Label()
        tTip = New ToolTip(components)
        TmrRotation = New Timer(components)
        TmrDoorCalculationDelay = New Timer(components)
        Ss1.SuspendLayout()
        Ss3.SuspendLayout()
        Tc.SuspendLayout()
        TpDrawersDoors.SuspendLayout()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        SplitContainer1.SuspendLayout()
        GroupBox9.SuspendLayout()
        CType(DgvDrawerHeights, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox8.SuspendLayout()
        GroupBox7.SuspendLayout()
        GroupBox6.SuspendLayout()
        GroupBox5.SuspendLayout()
        PnlResults.SuspendLayout()
        GroupBox11.SuspendLayout()
        TpBoardfeet.SuspendLayout()
        PnlBoardFeet.SuspendLayout()
        CType(DgvBoardfeet, ComponentModel.ISupportInitialize).BeginInit()
        TpCalculations.SuspendLayout()
        Panel5.SuspendLayout()
        Panel4.SuspendLayout()
        GroupBox4.SuspendLayout()
        GroupBox3.SuspendLayout()
        GroupBox2.SuspendLayout()
        GroupBox1.SuspendLayout()
        Panel3.SuspendLayout()
        CType(PbPolygon, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        TpLogs.SuspendLayout()
        TpDoors.SuspendLayout()
        CType(ScDoors, ComponentModel.ISupportInitialize).BeginInit()
        ScDoors.Panel1.SuspendLayout()
        ScDoors.Panel2.SuspendLayout()
        ScDoors.SuspendLayout()
        GroupBox12.SuspendLayout()
        Panel6.SuspendLayout()
        GroupBox10.SuspendLayout()
        Panel8.SuspendLayout()
        Panel9.SuspendLayout()
        Panel10.SuspendLayout()
        Panel7.SuspendLayout()
        PnlDoorResults.SuspendLayout()
        SuspendLayout()
        ' 
        ' Ss1
        ' 
        Ss1.GripMargin = New Padding(0)
        Ss1.ImageScalingSize = New Size(24, 24)
        Ss1.Items.AddRange(New ToolStripItem() {TsslVersion, TsslCpy, TsslError, TsslClock})
        Ss1.Location = New Point(0, 1086)
        Ss1.Name = "Ss1"
        Ss1.Padding = New Padding(1, 0, 13, 0)
        Ss1.Size = New Size(1084, 32)
        Ss1.SizingGrip = False
        Ss1.TabIndex = 0
        Ss1.Text = "StatusStrip1"
        ' 
        ' TsslVersion
        ' 
        TsslVersion.Name = "TsslVersion"
        TsslVersion.Size = New Size(21, 25)
        TsslVersion.Text = "v"
        ' 
        ' TsslCpy
        ' 
        TsslCpy.AutoSize = False
        TsslCpy.Font = New Font("Microsoft Sans Serif", 8.28F)
        TsslCpy.ForeColor = Color.Brown
        TsslCpy.Name = "TsslCpy"
        TsslCpy.Size = New Size(1009, 25)
        TsslCpy.Spring = True
        TsslCpy.Text = "cpy"
        ' 
        ' TsslError
        ' 
        TsslError.Name = "TsslError"
        TsslError.Size = New Size(22, 25)
        TsslError.Text = "0"
        ' 
        ' TsslClock
        ' 
        TsslClock.Name = "TsslClock"
        TsslClock.Size = New Size(18, 25)
        TsslClock.Text = "t"
        ' 
        ' Ss2
        ' 
        Ss2.AutoSize = False
        Ss2.Font = New Font("Microsoft Sans Serif", 8.28F)
        Ss2.GripMargin = New Padding(0)
        Ss2.ImageScalingSize = New Size(24, 24)
        Ss2.Location = New Point(0, 1061)
        Ss2.Name = "Ss2"
        Ss2.Padding = New Padding(1, 0, 13, 0)
        Ss2.Size = New Size(1084, 25)
        Ss2.SizingGrip = False
        Ss2.TabIndex = 1
        Ss2.Text = "StatusStrip2"
        ' 
        ' Ss3
        ' 
        Ss3.AutoSize = False
        Ss3.Font = New Font("Microsoft Sans Serif", 8.28F)
        Ss3.GripMargin = New Padding(0)
        Ss3.ImageScalingSize = New Size(24, 24)
        Ss3.Items.AddRange(New ToolStripItem() {TsslToggleTheme, TsslMemoriam, TsslScale})
        Ss3.Location = New Point(0, 1023)
        Ss3.Name = "Ss3"
        Ss3.Padding = New Padding(1, 0, 13, 0)
        Ss3.Size = New Size(1084, 38)
        Ss3.SizingGrip = False
        Ss3.TabIndex = 2
        Ss3.Text = "StatusStrip3"
        ' 
        ' TsslToggleTheme
        ' 
        TsslToggleTheme.Name = "TsslToggleTheme"
        TsslToggleTheme.Size = New Size(18, 35)
        ' 
        ' TsslMemoriam
        ' 
        TsslMemoriam.Name = "TsslMemoriam"
        TsslMemoriam.Size = New Size(976, 31)
        TsslMemoriam.Spring = True
        ' 
        ' TsslScale
        ' 
        TsslScale.Font = New Font("Microsoft Sans Serif", 8.28F, FontStyle.Bold)
        TsslScale.ForeColor = Color.ForestGreen
        TsslScale.Name = "TsslScale"
        TsslScale.Size = New Size(76, 31)
        TsslScale.Text = "Imperial"
        ' 
        ' Tc
        ' 
        Tc.Controls.Add(TpDrawersDoors)
        Tc.Controls.Add(TpBoardfeet)
        Tc.Controls.Add(TpCalculations)
        Tc.Controls.Add(TpLogs)
        Tc.Controls.Add(TpDoors)
        Tc.Dock = DockStyle.Top
        Tc.Location = New Point(0, 0)
        Tc.Name = "Tc"
        Tc.SelectedIndex = 0
        Tc.Size = New Size(1084, 996)
        Tc.TabIndex = 3
        ' 
        ' TpDrawersDoors
        ' 
        TpDrawersDoors.BackColor = Color.Gainsboro
        TpDrawersDoors.BorderStyle = BorderStyle.Fixed3D
        TpDrawersDoors.Controls.Add(SplitContainer1)
        TpDrawersDoors.Location = New Point(4, 30)
        TpDrawersDoors.Name = "TpDrawersDoors"
        TpDrawersDoors.Padding = New Padding(3)
        TpDrawersDoors.Size = New Size(1076, 962)
        TpDrawersDoors.TabIndex = 1
        TpDrawersDoors.Text = "Drawers/Doors"
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.Dock = DockStyle.Fill
        SplitContainer1.Location = New Point(3, 3)
        SplitContainer1.Name = "SplitContainer1"
        ' 
        ' SplitContainer1.Panel1
        ' 
        SplitContainer1.Panel1.BackColor = Color.Gainsboro
        SplitContainer1.Panel1.Controls.Add(LblAverageHeightResults)
        SplitContainer1.Panel1.Controls.Add(LblTotalMaterialResults)
        SplitContainer1.Panel1.Controls.Add(LblHeightRatioResults)
        SplitContainer1.Panel1.Controls.Add(LbltotalDrawerHeightResults)
        SplitContainer1.Panel1.Controls.Add(LblTotalHeightResults)
        SplitContainer1.Panel1.Controls.Add(LblStatus)
        SplitContainer1.Panel1.Controls.Add(GroupBox9)
        SplitContainer1.Panel1.Controls.Add(GroupBox8)
        SplitContainer1.Panel1.Controls.Add(GroupBox7)
        SplitContainer1.Panel1.Controls.Add(Label32)
        SplitContainer1.Panel1.Controls.Add(GroupBox6)
        SplitContainer1.Panel1.Controls.Add(GroupBox5)
        SplitContainer1.Panel1.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(PnlResults)
        SplitContainer1.Panel2.Controls.Add(GroupBox11)
        SplitContainer1.Panel2.Controls.Add(Label47)
        SplitContainer1.Panel2.Controls.Add(Label33)
        SplitContainer1.Panel2.Controls.Add(BtnSaveProject)
        SplitContainer1.Panel2.Controls.Add(Label34)
        SplitContainer1.Panel2.Controls.Add(TxtProjectName)
        SplitContainer1.Size = New Size(1066, 952)
        SplitContainer1.SplitterDistance = 516
        SplitContainer1.TabIndex = 0
        ' 
        ' LblAverageHeightResults
        ' 
        LblAverageHeightResults.AutoSize = True
        LblAverageHeightResults.Location = New Point(17, 825)
        LblAverageHeightResults.Name = "LblAverageHeightResults"
        LblAverageHeightResults.Size = New Size(129, 21)
        LblAverageHeightResults.TabIndex = 15
        LblAverageHeightResults.Text = "Average Height"
        ' 
        ' LblTotalMaterialResults
        ' 
        LblTotalMaterialResults.AutoSize = True
        LblTotalMaterialResults.Location = New Point(17, 854)
        LblTotalMaterialResults.Name = "LblTotalMaterialResults"
        LblTotalMaterialResults.Size = New Size(116, 21)
        LblTotalMaterialResults.TabIndex = 14
        LblTotalMaterialResults.Text = "Total Material"
        ' 
        ' LblHeightRatioResults
        ' 
        LblHeightRatioResults.AutoSize = True
        LblHeightRatioResults.Location = New Point(17, 796)
        LblHeightRatioResults.Name = "LblHeightRatioResults"
        LblHeightRatioResults.Size = New Size(106, 21)
        LblHeightRatioResults.TabIndex = 13
        LblHeightRatioResults.Text = "Height Ratio"
        ' 
        ' LbltotalDrawerHeightResults
        ' 
        LbltotalDrawerHeightResults.AutoSize = True
        LbltotalDrawerHeightResults.Location = New Point(17, 883)
        LbltotalDrawerHeightResults.Name = "LbltotalDrawerHeightResults"
        LbltotalDrawerHeightResults.Size = New Size(163, 21)
        LbltotalDrawerHeightResults.TabIndex = 12
        LbltotalDrawerHeightResults.Text = "Total Drawer Height"
        ' 
        ' LblTotalHeightResults
        ' 
        LblTotalHeightResults.AutoSize = True
        LblTotalHeightResults.Location = New Point(17, 912)
        LblTotalHeightResults.Name = "LblTotalHeightResults"
        LblTotalHeightResults.Size = New Size(111, 21)
        LblTotalHeightResults.TabIndex = 11
        LblTotalHeightResults.Text = "Total Heights"
        ' 
        ' LblStatus
        ' 
        LblStatus.Location = New Point(62, 766)
        LblStatus.Name = "LblStatus"
        LblStatus.Size = New Size(392, 21)
        LblStatus.TabIndex = 10
        LblStatus.Text = "Status"
        LblStatus.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' GroupBox9
        ' 
        GroupBox9.BackColor = Color.WhiteSmoke
        GroupBox9.Controls.Add(DgvDrawerHeights)
        GroupBox9.Location = New Point(14, 552)
        GroupBox9.Name = "GroupBox9"
        GroupBox9.Size = New Size(478, 199)
        GroupBox9.TabIndex = 6
        GroupBox9.TabStop = False
        GroupBox9.Text = "Individual Drawer Heights"
        ' 
        ' DgvDrawerHeights
        ' 
        DataGridViewCellStyle1.BackColor = Color.OldLace
        DgvDrawerHeights.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DgvDrawerHeights.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = SystemColors.Control
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 7.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle2.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        DgvDrawerHeights.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        DgvDrawerHeights.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvDrawerHeights.Dock = DockStyle.Fill
        DgvDrawerHeights.Location = New Point(3, 25)
        DgvDrawerHeights.Name = "DgvDrawerHeights"
        DgvDrawerHeights.RowHeadersWidth = 30
        DgvDrawerHeights.Size = New Size(472, 171)
        DgvDrawerHeights.TabIndex = 0
        ' 
        ' GroupBox8
        ' 
        GroupBox8.BackColor = Color.WhiteSmoke
        GroupBox8.Controls.Add(Button7)
        GroupBox8.Controls.Add(Button6)
        GroupBox8.Controls.Add(BtnCalculateDrawers)
        GroupBox8.Location = New Point(14, 477)
        GroupBox8.Name = "GroupBox8"
        GroupBox8.Size = New Size(481, 75)
        GroupBox8.TabIndex = 5
        GroupBox8.TabStop = False
        GroupBox8.Text = "Actions"
        ' 
        ' Button7
        ' 
        Button7.BackColor = Color.DarkSalmon
        Button7.FlatAppearance.BorderColor = Color.FromArgb(CByte(192), CByte(64), CByte(0))
        Button7.Location = New Point(341, 20)
        Button7.Name = "Button7"
        Button7.Size = New Size(112, 34)
        Button7.TabIndex = 2
        Button7.Text = "Clear Cache"
        Button7.UseVisualStyleBackColor = False
        ' 
        ' Button6
        ' 
        Button6.BackColor = Color.DarkSalmon
        Button6.FlatAppearance.BorderColor = Color.FromArgb(CByte(192), CByte(64), CByte(0))
        Button6.Location = New Point(184, 20)
        Button6.Name = "Button6"
        Button6.Size = New Size(112, 34)
        Button6.TabIndex = 1
        Button6.Text = "Clear"
        Button6.UseVisualStyleBackColor = False
        ' 
        ' BtnCalculateDrawers
        ' 
        BtnCalculateDrawers.BackColor = Color.DarkSalmon
        BtnCalculateDrawers.FlatAppearance.BorderColor = Color.FromArgb(CByte(192), CByte(64), CByte(0))
        BtnCalculateDrawers.Location = New Point(27, 20)
        BtnCalculateDrawers.Name = "BtnCalculateDrawers"
        BtnCalculateDrawers.Size = New Size(112, 34)
        BtnCalculateDrawers.TabIndex = 0
        BtnCalculateDrawers.Text = "Calculate"
        BtnCalculateDrawers.UseVisualStyleBackColor = False
        ' 
        ' GroupBox7
        ' 
        GroupBox7.BackColor = Color.WhiteSmoke
        GroupBox7.Controls.Add(BtnUniformPreset)
        GroupBox7.Controls.Add(BtnCustomRatioPreset)
        GroupBox7.Controls.Add(BtnExponentialProgressionPreset)
        GroupBox7.Controls.Add(BtnLogarithmicProgressionPreset)
        GroupBox7.Controls.Add(BtnReverseArithmeticPreset)
        GroupBox7.Controls.Add(BtnGoldenRatioPreset)
        GroupBox7.Controls.Add(BtnCustomCabinetPreset)
        GroupBox7.Controls.Add(BtnBathroomVanityPreset)
        GroupBox7.Controls.Add(BtnOfficeDeskPreset)
        GroupBox7.Controls.Add(BtnKitchenStandardPreset)
        GroupBox7.Location = New Point(14, 251)
        GroupBox7.Name = "GroupBox7"
        GroupBox7.Size = New Size(481, 226)
        GroupBox7.TabIndex = 4
        GroupBox7.TabStop = False
        GroupBox7.Text = "Quick Presets"
        ' 
        ' BtnUniformPreset
        ' 
        BtnUniformPreset.BackColor = Color.LightCyan
        BtnUniformPreset.FlatAppearance.BorderColor = Color.Blue
        BtnUniformPreset.Font = New Font("Microsoft Sans Serif", 7.25F)
        BtnUniformPreset.Location = New Point(241, 179)
        BtnUniformPreset.Name = "BtnUniformPreset"
        BtnUniformPreset.Size = New Size(196, 34)
        BtnUniformPreset.TabIndex = 9
        BtnUniformPreset.Tag = "2"
        BtnUniformPreset.Text = "Golden Ratio"
        BtnUniformPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnCustomRatioPreset
        ' 
        BtnCustomRatioPreset.BackColor = Color.LightCyan
        BtnCustomRatioPreset.FlatAppearance.BorderColor = Color.Blue
        BtnCustomRatioPreset.Font = New Font("Microsoft Sans Serif", 7.25F)
        BtnCustomRatioPreset.Location = New Point(241, 142)
        BtnCustomRatioPreset.Name = "BtnCustomRatioPreset"
        BtnCustomRatioPreset.Size = New Size(196, 34)
        BtnCustomRatioPreset.TabIndex = 8
        BtnCustomRatioPreset.Tag = "2"
        BtnCustomRatioPreset.Text = "Custom Ratio"
        BtnCustomRatioPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnExponentialProgressionPreset
        ' 
        BtnExponentialProgressionPreset.BackColor = Color.LightCyan
        BtnExponentialProgressionPreset.FlatAppearance.BorderColor = Color.Blue
        BtnExponentialProgressionPreset.Font = New Font("Microsoft Sans Serif", 7.25F)
        BtnExponentialProgressionPreset.Location = New Point(41, 179)
        BtnExponentialProgressionPreset.Name = "BtnExponentialProgressionPreset"
        BtnExponentialProgressionPreset.Size = New Size(196, 34)
        BtnExponentialProgressionPreset.TabIndex = 7
        BtnExponentialProgressionPreset.Tag = "2"
        BtnExponentialProgressionPreset.Text = "Exponential Progression"
        BtnExponentialProgressionPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnLogarithmicProgressionPreset
        ' 
        BtnLogarithmicProgressionPreset.BackColor = Color.LightCyan
        BtnLogarithmicProgressionPreset.FlatAppearance.BorderColor = Color.Blue
        BtnLogarithmicProgressionPreset.Font = New Font("Microsoft Sans Serif", 7.25F)
        BtnLogarithmicProgressionPreset.Location = New Point(42, 142)
        BtnLogarithmicProgressionPreset.Name = "BtnLogarithmicProgressionPreset"
        BtnLogarithmicProgressionPreset.Size = New Size(196, 34)
        BtnLogarithmicProgressionPreset.TabIndex = 6
        BtnLogarithmicProgressionPreset.Tag = "2"
        BtnLogarithmicProgressionPreset.Text = "Logarithmic Progression"
        BtnLogarithmicProgressionPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnReverseArithmeticPreset
        ' 
        BtnReverseArithmeticPreset.BackColor = Color.LightCyan
        BtnReverseArithmeticPreset.FlatAppearance.BorderColor = Color.Blue
        BtnReverseArithmeticPreset.Font = New Font("Microsoft Sans Serif", 7.25F)
        BtnReverseArithmeticPreset.Location = New Point(243, 102)
        BtnReverseArithmeticPreset.Name = "BtnReverseArithmeticPreset"
        BtnReverseArithmeticPreset.Size = New Size(196, 34)
        BtnReverseArithmeticPreset.TabIndex = 5
        BtnReverseArithmeticPreset.Tag = "2"
        BtnReverseArithmeticPreset.Text = "Reverse Arithmetic"
        BtnReverseArithmeticPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnGoldenRatioPreset
        ' 
        BtnGoldenRatioPreset.BackColor = Color.LightCyan
        BtnGoldenRatioPreset.FlatAppearance.BorderColor = Color.Blue
        BtnGoldenRatioPreset.Font = New Font("Microsoft Sans Serif", 7.25F)
        BtnGoldenRatioPreset.Location = New Point(42, 102)
        BtnGoldenRatioPreset.Name = "BtnGoldenRatioPreset"
        BtnGoldenRatioPreset.Size = New Size(196, 34)
        BtnGoldenRatioPreset.TabIndex = 4
        BtnGoldenRatioPreset.Tag = "2"
        BtnGoldenRatioPreset.Text = "Golden Ratio"
        BtnGoldenRatioPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnCustomCabinetPreset
        ' 
        BtnCustomCabinetPreset.BackColor = Color.LightCyan
        BtnCustomCabinetPreset.FlatAppearance.BorderColor = Color.Blue
        BtnCustomCabinetPreset.Font = New Font("Microsoft Sans Serif", 7.25F)
        BtnCustomCabinetPreset.Location = New Point(243, 63)
        BtnCustomCabinetPreset.Name = "BtnCustomCabinetPreset"
        BtnCustomCabinetPreset.Size = New Size(196, 34)
        BtnCustomCabinetPreset.TabIndex = 3
        BtnCustomCabinetPreset.Tag = "4"
        BtnCustomCabinetPreset.Text = "Custom Cabinet"
        BtnCustomCabinetPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnBathroomVanityPreset
        ' 
        BtnBathroomVanityPreset.BackColor = Color.LightCyan
        BtnBathroomVanityPreset.FlatAppearance.BorderColor = Color.Blue
        BtnBathroomVanityPreset.Font = New Font("Microsoft Sans Serif", 7.25F)
        BtnBathroomVanityPreset.Location = New Point(243, 24)
        BtnBathroomVanityPreset.Name = "BtnBathroomVanityPreset"
        BtnBathroomVanityPreset.Size = New Size(196, 34)
        BtnBathroomVanityPreset.TabIndex = 2
        BtnBathroomVanityPreset.Tag = "3"
        BtnBathroomVanityPreset.Text = "Bathroom Vanity"
        BtnBathroomVanityPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnOfficeDeskPreset
        ' 
        BtnOfficeDeskPreset.BackColor = Color.LightCyan
        BtnOfficeDeskPreset.FlatAppearance.BorderColor = Color.Blue
        BtnOfficeDeskPreset.Font = New Font("Microsoft Sans Serif", 7.25F)
        BtnOfficeDeskPreset.Location = New Point(42, 63)
        BtnOfficeDeskPreset.Name = "BtnOfficeDeskPreset"
        BtnOfficeDeskPreset.Size = New Size(196, 34)
        BtnOfficeDeskPreset.TabIndex = 1
        BtnOfficeDeskPreset.Tag = "1"
        BtnOfficeDeskPreset.Text = "Office Desk"
        BtnOfficeDeskPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnKitchenStandardPreset
        ' 
        BtnKitchenStandardPreset.BackColor = Color.LightCyan
        BtnKitchenStandardPreset.FlatAppearance.BorderColor = Color.Blue
        BtnKitchenStandardPreset.Font = New Font("Microsoft Sans Serif", 7.25F)
        BtnKitchenStandardPreset.Location = New Point(42, 24)
        BtnKitchenStandardPreset.Name = "BtnKitchenStandardPreset"
        BtnKitchenStandardPreset.Size = New Size(196, 34)
        BtnKitchenStandardPreset.TabIndex = 0
        BtnKitchenStandardPreset.Tag = "0"
        BtnKitchenStandardPreset.Text = "Kitchen Standard"
        BtnKitchenStandardPreset.UseVisualStyleBackColor = False
        ' 
        ' Label32
        ' 
        Label32.AutoSize = True
        Label32.Font = New Font("Georgia", 16.0F, FontStyle.Bold)
        Label32.ForeColor = Color.Maroon
        Label32.Location = New Point(81, 13)
        Label32.Name = "Label32"
        Label32.Size = New Size(355, 38)
        Label32.TabIndex = 3
        Label32.Text = "Drawer Calculations"
        ' 
        ' GroupBox6
        ' 
        GroupBox6.BackColor = Color.WhiteSmoke
        GroupBox6.Controls.Add(LblCustomRatioInput)
        GroupBox6.Controls.Add(TxtCustomRatioInput)
        GroupBox6.Controls.Add(TxtArithmeticIncrement)
        GroupBox6.Controls.Add(TxtMultiplier)
        GroupBox6.Controls.Add(TxtFirstDrawerHeight)
        GroupBox6.Controls.Add(Label31)
        GroupBox6.Controls.Add(Label30)
        GroupBox6.Controls.Add(Label29)
        GroupBox6.Location = New Point(244, 62)
        GroupBox6.Name = "GroupBox6"
        GroupBox6.Size = New Size(251, 183)
        GroupBox6.TabIndex = 2
        GroupBox6.TabStop = False
        GroupBox6.Text = "Method Specific Parameters"
        ' 
        ' LblCustomRatioInput
        ' 
        LblCustomRatioInput.AutoSize = True
        LblCustomRatioInput.Location = New Point(50, 147)
        LblCustomRatioInput.Name = "LblCustomRatioInput"
        LblCustomRatioInput.Size = New Size(112, 21)
        LblCustomRatioInput.TabIndex = 8
        LblCustomRatioInput.Text = "Custom Ratio"
        ' 
        ' TxtCustomRatioInput
        ' 
        TxtCustomRatioInput.Location = New Point(180, 134)
        TxtCustomRatioInput.Multiline = True
        TxtCustomRatioInput.Name = "TxtCustomRatioInput"
        TxtCustomRatioInput.Size = New Size(65, 46)
        TxtCustomRatioInput.TabIndex = 7
        tTip.SetToolTip(TxtCustomRatioInput, "Input can be entered in the following formats:" & vbCrLf & "1.0" & vbCrLf & "1.5" & vbCrLf & "2.0" & vbCrLf & "Comma separated:" & vbCrLf & "1.0,1.5, 2.0, 3.0" & vbCrLf & "Space separated:" & vbCrLf & "1.0 1.5 2.0 3.0" & vbCrLf & "Mixed format:" & vbCrLf & "1.0, 1.5" & vbCrLf & "2.0 3.0")
        ' 
        ' TxtArithmeticIncrement
        ' 
        TxtArithmeticIncrement.Location = New Point(180, 97)
        TxtArithmeticIncrement.Name = "TxtArithmeticIncrement"
        TxtArithmeticIncrement.Size = New Size(65, 29)
        TxtArithmeticIncrement.TabIndex = 6
        tTip.SetToolTip(TxtArithmeticIncrement, resources.GetString("TxtArithmeticIncrement.ToolTip"))
        ' 
        ' TxtMultiplier
        ' 
        TxtMultiplier.Location = New Point(180, 60)
        TxtMultiplier.Name = "TxtMultiplier"
        TxtMultiplier.Size = New Size(65, 29)
        TxtMultiplier.TabIndex = 5
        tTip.SetToolTip(TxtMultiplier, resources.GetString("TxtMultiplier.ToolTip"))
        ' 
        ' TxtFirstDrawerHeight
        ' 
        TxtFirstDrawerHeight.Location = New Point(180, 23)
        TxtFirstDrawerHeight.Name = "TxtFirstDrawerHeight"
        TxtFirstDrawerHeight.Size = New Size(65, 29)
        TxtFirstDrawerHeight.TabIndex = 4
        ' 
        ' Label31
        ' 
        Label31.AutoSize = True
        Label31.Location = New Point(5, 101)
        Label31.Name = "Label31"
        Label31.Size = New Size(173, 21)
        Label31.TabIndex = 2
        Label31.Text = "Arithmetic Increment"
        Label31.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label30
        ' 
        Label30.AutoSize = True
        Label30.Location = New Point(92, 64)
        Label30.Name = "Label30"
        Label30.Size = New Size(86, 21)
        Label30.TabIndex = 1
        Label30.Text = "Multiplier"
        Label30.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label29
        ' 
        Label29.AutoSize = True
        Label29.Location = New Point(31, 27)
        Label29.Name = "Label29"
        Label29.Size = New Size(147, 21)
        Label29.TabIndex = 0
        Label29.Text = "1st Drawer Height"
        Label29.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' GroupBox5
        ' 
        GroupBox5.BackColor = Color.WhiteSmoke
        GroupBox5.Controls.Add(TxtDrawerWidth)
        GroupBox5.Controls.Add(TxtDrawerSpacing)
        GroupBox5.Controls.Add(TxtDrawerCount)
        GroupBox5.Controls.Add(Label28)
        GroupBox5.Controls.Add(Label27)
        GroupBox5.Controls.Add(Label26)
        GroupBox5.Location = New Point(14, 62)
        GroupBox5.Name = "GroupBox5"
        GroupBox5.Size = New Size(224, 183)
        GroupBox5.TabIndex = 1
        GroupBox5.TabStop = False
        GroupBox5.Text = "Basic Parameters"
        ' 
        ' TxtDrawerWidth
        ' 
        TxtDrawerWidth.Location = New Point(146, 91)
        TxtDrawerWidth.Name = "TxtDrawerWidth"
        TxtDrawerWidth.Size = New Size(65, 29)
        TxtDrawerWidth.TabIndex = 5
        ' 
        ' TxtDrawerSpacing
        ' 
        TxtDrawerSpacing.Location = New Point(146, 57)
        TxtDrawerSpacing.Name = "TxtDrawerSpacing"
        TxtDrawerSpacing.Size = New Size(65, 29)
        TxtDrawerSpacing.TabIndex = 4
        ' 
        ' TxtDrawerCount
        ' 
        TxtDrawerCount.Location = New Point(146, 23)
        TxtDrawerCount.Name = "TxtDrawerCount"
        TxtDrawerCount.Size = New Size(65, 29)
        TxtDrawerCount.TabIndex = 3
        ' 
        ' Label28
        ' 
        Label28.AutoSize = True
        Label28.Location = New Point(24, 95)
        Label28.Name = "Label28"
        Label28.Size = New Size(116, 21)
        Label28.TabIndex = 2
        Label28.Text = "Drawer Width"
        Label28.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label27
        ' 
        Label27.AutoSize = True
        Label27.Location = New Point(10, 61)
        Label27.Name = "Label27"
        Label27.Size = New Size(130, 21)
        Label27.TabIndex = 1
        Label27.Text = "Drawer Spacing"
        Label27.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label26
        ' 
        Label26.AutoSize = True
        Label26.Location = New Point(55, 27)
        Label26.Name = "Label26"
        Label26.Size = New Size(85, 21)
        Label26.TabIndex = 0
        Label26.Text = "# Drawers"
        Label26.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' PnlResults
        ' 
        PnlResults.BorderStyle = BorderStyle.Fixed3D
        PnlResults.Controls.Add(RtbResults)
        PnlResults.Dock = DockStyle.Bottom
        PnlResults.Location = New Point(0, 640)
        PnlResults.Name = "PnlResults"
        PnlResults.Size = New Size(546, 312)
        PnlResults.TabIndex = 18
        ' 
        ' RtbResults
        ' 
        RtbResults.BackColor = Color.White
        RtbResults.Dock = DockStyle.Fill
        RtbResults.Location = New Point(0, 0)
        RtbResults.Name = "RtbResults"
        RtbResults.ReadOnly = True
        RtbResults.ShowSelectionMargin = True
        RtbResults.Size = New Size(542, 308)
        RtbResults.TabIndex = 5
        RtbResults.Text = ""
        ' 
        ' GroupBox11
        ' 
        GroupBox11.BackColor = Color.WhiteSmoke
        GroupBox11.Controls.Add(RbArithmetic)
        GroupBox11.Controls.Add(RbFibonacci)
        GroupBox11.Controls.Add(RbGeometric)
        GroupBox11.Controls.Add(RbHambridge)
        GroupBox11.Controls.Add(RbGoldenRatio)
        GroupBox11.Controls.Add(RbReverseArithmetic)
        GroupBox11.Controls.Add(RbUniform)
        GroupBox11.Controls.Add(RbCustomRatio)
        GroupBox11.Controls.Add(RbExponential)
        GroupBox11.Controls.Add(RbLogarithmic)
        GroupBox11.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        GroupBox11.Location = New Point(12, 60)
        GroupBox11.Name = "GroupBox11"
        GroupBox11.Size = New Size(523, 367)
        GroupBox11.TabIndex = 17
        GroupBox11.TabStop = False
        GroupBox11.Text = "Calculation Methods"
        ' 
        ' RbArithmetic
        ' 
        RbArithmetic.AutoSize = True
        RbArithmetic.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        RbArithmetic.Location = New Point(25, 127)
        RbArithmetic.Name = "RbArithmetic"
        RbArithmetic.Size = New Size(324, 24)
        RbArithmetic.TabIndex = 9
        RbArithmetic.Tag = "3"
        RbArithmetic.Text = "Arithmetic Progression (Fixed increment)"
        RbArithmetic.UseVisualStyleBackColor = True
        ' 
        ' RbFibonacci
        ' 
        RbFibonacci.AutoSize = True
        RbFibonacci.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        RbFibonacci.Location = New Point(25, 94)
        RbFibonacci.Name = "RbFibonacci"
        RbFibonacci.Size = New Size(318, 24)
        RbFibonacci.TabIndex = 8
        RbFibonacci.Tag = "2"
        RbFibonacci.Text = "Fibonacci Sequence (Natural Proportion)"
        RbFibonacci.UseVisualStyleBackColor = True
        ' 
        ' RbGeometric
        ' 
        RbGeometric.AutoSize = True
        RbGeometric.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        RbGeometric.Location = New Point(25, 61)
        RbGeometric.Name = "RbGeometric"
        RbGeometric.Size = New Size(319, 24)
        RbGeometric.TabIndex = 7
        RbGeometric.Tag = "1"
        RbGeometric.Text = "Geometric Progression (Fixed Multiplier)"
        RbGeometric.UseVisualStyleBackColor = True
        ' 
        ' RbHambridge
        ' 
        RbHambridge.AutoSize = True
        RbHambridge.Checked = True
        RbHambridge.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        RbHambridge.Location = New Point(25, 28)
        RbHambridge.Name = "RbHambridge"
        RbHambridge.Size = New Size(352, 24)
        RbHambridge.TabIndex = 6
        RbHambridge.TabStop = True
        RbHambridge.Tag = "0"
        RbHambridge.Text = "Hambridge Ratio (Mathematical Proportions)"
        RbHambridge.UseVisualStyleBackColor = True
        ' 
        ' RbGoldenRatio
        ' 
        RbGoldenRatio.AutoSize = True
        RbGoldenRatio.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        RbGoldenRatio.Location = New Point(25, 325)
        RbGoldenRatio.Name = "RbGoldenRatio"
        RbGoldenRatio.Size = New Size(368, 24)
        RbGoldenRatio.TabIndex = 5
        RbGoldenRatio.TabStop = True
        RbGoldenRatio.Tag = "9"
        RbGoldenRatio.Text = "Golden Ratio Cascade (Highly aesthetic layouts)"
        RbGoldenRatio.UseVisualStyleBackColor = True
        ' 
        ' RbReverseArithmetic
        ' 
        RbReverseArithmetic.AutoSize = True
        RbReverseArithmetic.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        RbReverseArithmetic.Location = New Point(25, 292)
        RbReverseArithmetic.Name = "RbReverseArithmetic"
        RbReverseArithmetic.Size = New Size(442, 24)
        RbReverseArithmetic.TabIndex = 4
        RbReverseArithmetic.TabStop = True
        RbReverseArithmetic.Tag = "8"
        RbReverseArithmetic.Text = "Reverse Arithmetic/Geometric (Larger drawers at the top)"
        RbReverseArithmetic.UseVisualStyleBackColor = True
        ' 
        ' RbUniform
        ' 
        RbUniform.AutoSize = True
        RbUniform.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        RbUniform.Location = New Point(25, 259)
        RbUniform.Name = "RbUniform"
        RbUniform.Size = New Size(300, 24)
        RbUniform.TabIndex = 3
        RbUniform.TabStop = True
        RbUniform.Tag = "7"
        RbUniform.Text = "Uniform (Equal Height, simple classic)"
        RbUniform.UseVisualStyleBackColor = True
        ' 
        ' RbCustomRatio
        ' 
        RbCustomRatio.AutoSize = True
        RbCustomRatio.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        RbCustomRatio.Location = New Point(25, 226)
        RbCustomRatio.Name = "RbCustomRatio"
        RbCustomRatio.Size = New Size(464, 24)
        RbCustomRatio.TabIndex = 2
        RbCustomRatio.TabStop = True
        RbCustomRatio.Tag = "6"
        RbCustomRatio.Text = "Custom Ratio Sequence (proportional to user-supplied ratios)"
        RbCustomRatio.UseVisualStyleBackColor = True
        ' 
        ' RbExponential
        ' 
        RbExponential.AutoSize = True
        RbExponential.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        RbExponential.Location = New Point(25, 193)
        RbExponential.Name = "RbExponential"
        RbExponential.Size = New Size(434, 24)
        RbExponential.TabIndex = 1
        RbExponential.TabStop = True
        RbExponential.Tag = "5"
        RbExponential.Text = "Exponential Progression ( height increases exponentially)"
        RbExponential.UseVisualStyleBackColor = True
        ' 
        ' RbLogarithmic
        ' 
        RbLogarithmic.AutoSize = True
        RbLogarithmic.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        RbLogarithmic.Location = New Point(25, 160)
        RbLogarithmic.Name = "RbLogarithmic"
        RbLogarithmic.Size = New Size(472, 24)
        RbLogarithmic.TabIndex = 0
        RbLogarithmic.TabStop = True
        RbLogarithmic.Tag = "4"
        RbLogarithmic.Text = "Logarithmic Progression ( height increase to logarithmic scale)"
        RbLogarithmic.UseVisualStyleBackColor = True
        ' 
        ' Label47
        ' 
        Label47.AutoSize = True
        Label47.Font = New Font("Georgia", 16.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label47.ForeColor = Color.Maroon
        Label47.Location = New Point(210, 583)
        Label47.Name = "Label47"
        Label47.Size = New Size(139, 38)
        Label47.TabIndex = 16
        Label47.Text = "Results"
        ' 
        ' Label33
        ' 
        Label33.AutoSize = True
        Label33.Font = New Font("Georgia", 16.0F, FontStyle.Bold)
        Label33.ForeColor = Color.Maroon
        Label33.Location = New Point(96, 15)
        Label33.Name = "Label33"
        Label33.Size = New Size(355, 38)
        Label33.TabIndex = 4
        Label33.Text = "Drawer Calculations"
        ' 
        ' BtnSaveProject
        ' 
        BtnSaveProject.Location = New Point(42, 465)
        BtnSaveProject.Name = "BtnSaveProject"
        BtnSaveProject.Size = New Size(112, 34)
        BtnSaveProject.TabIndex = 7
        BtnSaveProject.Text = "Save Project"
        BtnSaveProject.UseVisualStyleBackColor = True
        ' 
        ' Label34
        ' 
        Label34.AutoSize = True
        Label34.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label34.Location = New Point(237, 472)
        Label34.Name = "Label34"
        Label34.Size = New Size(114, 21)
        Label34.TabIndex = 9
        Label34.Text = "Project Name"
        ' 
        ' TxtProjectName
        ' 
        TxtProjectName.Location = New Point(355, 468)
        TxtProjectName.Name = "TxtProjectName"
        TxtProjectName.Size = New Size(150, 29)
        TxtProjectName.TabIndex = 8
        ' 
        ' TpBoardfeet
        ' 
        TpBoardfeet.BackColor = Color.Gainsboro
        TpBoardfeet.BorderStyle = BorderStyle.Fixed3D
        TpBoardfeet.Controls.Add(PnlBoardFeet)
        TpBoardfeet.Location = New Point(4, 34)
        TpBoardfeet.Name = "TpBoardfeet"
        TpBoardfeet.Padding = New Padding(3)
        TpBoardfeet.Size = New Size(1076, 958)
        TpBoardfeet.TabIndex = 0
        TpBoardfeet.Text = "Boardfeet"
        ' 
        ' PnlBoardFeet
        ' 
        PnlBoardFeet.BackColor = Color.WhiteSmoke
        PnlBoardFeet.BorderStyle = BorderStyle.Fixed3D
        PnlBoardFeet.Controls.Add(BtnPrtBfProject)
        PnlBoardFeet.Controls.Add(TxtBfProjectName)
        PnlBoardFeet.Controls.Add(BtnSaveBfProject)
        PnlBoardFeet.Controls.Add(LblBoardFeetCost20)
        PnlBoardFeet.Controls.Add(LblTotalBoardFeet20)
        PnlBoardFeet.Controls.Add(Label12)
        PnlBoardFeet.Controls.Add(LblBoardFeetCost15)
        PnlBoardFeet.Controls.Add(LblTotalBoardFeet15)
        PnlBoardFeet.Controls.Add(Label9)
        PnlBoardFeet.Controls.Add(LblBoardFeetCost10)
        PnlBoardFeet.Controls.Add(LblTotalBoardFeet10)
        PnlBoardFeet.Controls.Add(Label6)
        PnlBoardFeet.Controls.Add(LblBoardFeetCost)
        PnlBoardFeet.Controls.Add(LblTotalBoardFeet)
        PnlBoardFeet.Controls.Add(Label1)
        PnlBoardFeet.Controls.Add(lblCalculateBoardfeet)
        PnlBoardFeet.Controls.Add(DgvBoardfeet)
        PnlBoardFeet.Location = New Point(5, 5)
        PnlBoardFeet.Name = "PnlBoardFeet"
        PnlBoardFeet.Size = New Size(1038, 588)
        PnlBoardFeet.TabIndex = 0
        ' 
        ' BtnPrtBfProject
        ' 
        BtnPrtBfProject.Location = New Point(760, 522)
        BtnPrtBfProject.Name = "BtnPrtBfProject"
        BtnPrtBfProject.Size = New Size(144, 34)
        BtnPrtBfProject.TabIndex = 16
        BtnPrtBfProject.Text = "Print Project"
        BtnPrtBfProject.UseVisualStyleBackColor = True
        ' 
        ' TxtBfProjectName
        ' 
        TxtBfProjectName.Location = New Point(406, 525)
        TxtBfProjectName.Name = "TxtBfProjectName"
        TxtBfProjectName.Size = New Size(222, 29)
        TxtBfProjectName.TabIndex = 15
        tTip.SetToolTip(TxtBfProjectName, "Enter project name")
        ' 
        ' BtnSaveBfProject
        ' 
        BtnSaveBfProject.Location = New Point(130, 522)
        BtnSaveBfProject.Name = "BtnSaveBfProject"
        BtnSaveBfProject.Size = New Size(144, 34)
        BtnSaveBfProject.TabIndex = 14
        BtnSaveBfProject.Text = "Save Project"
        BtnSaveBfProject.UseVisualStyleBackColor = True
        ' 
        ' LblBoardFeetCost20
        ' 
        LblBoardFeetCost20.BorderStyle = BorderStyle.FixedSingle
        LblBoardFeetCost20.Location = New Point(767, 470)
        LblBoardFeetCost20.Name = "LblBoardFeetCost20"
        LblBoardFeetCost20.Size = New Size(175, 32)
        LblBoardFeetCost20.TabIndex = 13
        LblBoardFeetCost20.Text = "$0.00"
        LblBoardFeetCost20.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblTotalBoardFeet20
        ' 
        LblTotalBoardFeet20.BorderStyle = BorderStyle.FixedSingle
        LblTotalBoardFeet20.Location = New Point(767, 425)
        LblTotalBoardFeet20.Name = "LblTotalBoardFeet20"
        LblTotalBoardFeet20.Size = New Size(175, 32)
        LblTotalBoardFeet20.TabIndex = 12
        LblTotalBoardFeet20.Text = "0.00"
        LblTotalBoardFeet20.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label12
        ' 
        Label12.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Label12.Location = New Point(767, 375)
        Label12.Name = "Label12"
        Label12.Size = New Size(175, 32)
        Label12.TabIndex = 11
        Label12.Text = "Board Feet +20%"
        Label12.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblBoardFeetCost15
        ' 
        LblBoardFeetCost15.BorderStyle = BorderStyle.FixedSingle
        LblBoardFeetCost15.Location = New Point(522, 461)
        LblBoardFeetCost15.Name = "LblBoardFeetCost15"
        LblBoardFeetCost15.Size = New Size(175, 32)
        LblBoardFeetCost15.TabIndex = 10
        LblBoardFeetCost15.Text = "$0.00"
        LblBoardFeetCost15.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblTotalBoardFeet15
        ' 
        LblTotalBoardFeet15.BorderStyle = BorderStyle.FixedSingle
        LblTotalBoardFeet15.Location = New Point(522, 417)
        LblTotalBoardFeet15.Name = "LblTotalBoardFeet15"
        LblTotalBoardFeet15.Size = New Size(175, 32)
        LblTotalBoardFeet15.TabIndex = 9
        LblTotalBoardFeet15.Text = "0.00"
        LblTotalBoardFeet15.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label9
        ' 
        Label9.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Label9.Location = New Point(522, 375)
        Label9.Name = "Label9"
        Label9.Size = New Size(175, 32)
        Label9.TabIndex = 8
        Label9.Text = "Board Feet +15%"
        Label9.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblBoardFeetCost10
        ' 
        LblBoardFeetCost10.BorderStyle = BorderStyle.FixedSingle
        LblBoardFeetCost10.Location = New Point(307, 461)
        LblBoardFeetCost10.Name = "LblBoardFeetCost10"
        LblBoardFeetCost10.Size = New Size(175, 32)
        LblBoardFeetCost10.TabIndex = 7
        LblBoardFeetCost10.Text = "$0.00"
        LblBoardFeetCost10.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblTotalBoardFeet10
        ' 
        LblTotalBoardFeet10.BorderStyle = BorderStyle.FixedSingle
        LblTotalBoardFeet10.Location = New Point(307, 417)
        LblTotalBoardFeet10.Name = "LblTotalBoardFeet10"
        LblTotalBoardFeet10.Size = New Size(175, 32)
        LblTotalBoardFeet10.TabIndex = 6
        LblTotalBoardFeet10.Text = "0.00"
        LblTotalBoardFeet10.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label6
        ' 
        Label6.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Label6.Location = New Point(307, 375)
        Label6.Name = "Label6"
        Label6.Size = New Size(175, 32)
        Label6.TabIndex = 5
        Label6.Text = "Board Feet +10%"
        Label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblBoardFeetCost
        ' 
        LblBoardFeetCost.BorderStyle = BorderStyle.FixedSingle
        LblBoardFeetCost.Location = New Point(93, 461)
        LblBoardFeetCost.Name = "LblBoardFeetCost"
        LblBoardFeetCost.Size = New Size(175, 32)
        LblBoardFeetCost.TabIndex = 4
        LblBoardFeetCost.Text = "$0.00"
        LblBoardFeetCost.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblTotalBoardFeet
        ' 
        LblTotalBoardFeet.BorderStyle = BorderStyle.FixedSingle
        LblTotalBoardFeet.Location = New Point(93, 417)
        LblTotalBoardFeet.Name = "LblTotalBoardFeet"
        LblTotalBoardFeet.Size = New Size(175, 32)
        LblTotalBoardFeet.TabIndex = 3
        LblTotalBoardFeet.Text = "0.00"
        LblTotalBoardFeet.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Label1.Location = New Point(93, 375)
        Label1.Name = "Label1"
        Label1.Size = New Size(175, 32)
        Label1.TabIndex = 2
        Label1.Text = "Total Board Feet"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblCalculateBoardfeet
        ' 
        lblCalculateBoardfeet.AutoSize = True
        lblCalculateBoardfeet.Font = New Font("Georgia", 16.0F, FontStyle.Bold)
        lblCalculateBoardfeet.Location = New Point(364, 28)
        lblCalculateBoardfeet.Name = "lblCalculateBoardfeet"
        lblCalculateBoardfeet.Size = New Size(341, 38)
        lblCalculateBoardfeet.TabIndex = 1
        lblCalculateBoardfeet.Text = "Calculate Boardfeet"
        ' 
        ' DgvBoardfeet
        ' 
        DataGridViewCellStyle3.BackColor = Color.Beige
        DgvBoardfeet.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        DgvBoardfeet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DgvBoardfeet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvBoardfeet.Columns.AddRange(New DataGridViewColumn() {bfCol0, bfCol1, bfCol2, bfCol3, bfCol4, bfCol5, bfCol6})
        DgvBoardfeet.Location = New Point(10, 74)
        DgvBoardfeet.Name = "DgvBoardfeet"
        DgvBoardfeet.RowHeadersWidth = 62
        DgvBoardfeet.Size = New Size(1014, 276)
        DgvBoardfeet.TabIndex = 0
        ' 
        ' bfCol0
        ' 
        bfCol0.HeaderText = "Length"
        bfCol0.MinimumWidth = 8
        bfCol0.Name = "bfCol0"
        ' 
        ' bfCol1
        ' 
        bfCol1.HeaderText = "Width"
        bfCol1.MinimumWidth = 8
        bfCol1.Name = "bfCol1"
        ' 
        ' bfCol2
        ' 
        bfCol2.HeaderText = "Thickness"
        bfCol2.MinimumWidth = 8
        bfCol2.Name = "bfCol2"
        ' 
        ' bfCol3
        ' 
        bfCol3.HeaderText = "Amount"
        bfCol3.MinimumWidth = 8
        bfCol3.Name = "bfCol3"
        ' 
        ' bfCol4
        ' 
        bfCol4.HeaderText = "Wood Type"
        bfCol4.MinimumWidth = 8
        bfCol4.Name = "bfCol4"
        ' 
        ' bfCol5
        ' 
        bfCol5.HeaderText = "Cost/Bf"
        bfCol5.MinimumWidth = 8
        bfCol5.Name = "bfCol5"
        ' 
        ' bfCol6
        ' 
        bfCol6.HeaderText = "Total Bf"
        bfCol6.MinimumWidth = 8
        bfCol6.Name = "bfCol6"
        ' 
        ' TpCalculations
        ' 
        TpCalculations.BackColor = Color.Gainsboro
        TpCalculations.BorderStyle = BorderStyle.Fixed3D
        TpCalculations.Controls.Add(Panel5)
        TpCalculations.Controls.Add(Panel4)
        TpCalculations.Controls.Add(Panel3)
        TpCalculations.Controls.Add(Panel2)
        TpCalculations.Controls.Add(Panel1)
        TpCalculations.Location = New Point(4, 34)
        TpCalculations.Name = "TpCalculations"
        TpCalculations.Size = New Size(1076, 958)
        TpCalculations.TabIndex = 2
        TpCalculations.Text = "Calculations"
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = Color.WhiteSmoke
        Panel5.BorderStyle = BorderStyle.Fixed3D
        Panel5.Controls.Add(LblTippingForce)
        Panel5.Controls.Add(TxtTtTableBaseWeight)
        Panel5.Controls.Add(TxtTtTableBaselength)
        Panel5.Controls.Add(TxtTtTableTopWeight)
        Panel5.Controls.Add(TxtTtTableTopLength)
        Panel5.Controls.Add(Label25)
        Panel5.Controls.Add(Label24)
        Panel5.Controls.Add(Label22)
        Panel5.Controls.Add(Label23)
        Panel5.Controls.Add(Label21)
        Panel5.Controls.Add(Label20)
        Panel5.Controls.Add(Label18)
        Panel5.Controls.Add(Label15)
        Panel5.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Panel5.Location = New Point(356, 485)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(391, 413)
        Panel5.TabIndex = 4
        ' 
        ' LblTippingForce
        ' 
        LblTippingForce.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblTippingForce.ForeColor = Color.DodgerBlue
        LblTippingForce.Location = New Point(5, 317)
        LblTippingForce.Name = "LblTippingForce"
        LblTippingForce.Size = New Size(377, 38)
        LblTippingForce.TabIndex = 12
        LblTippingForce.Tag = "Tipping force required: {0:N2} lbs"
        LblTippingForce.Text = "Tipping force required"
        LblTippingForce.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TxtTtTableBaseWeight
        ' 
        TxtTtTableBaseWeight.Location = New Point(106, 268)
        TxtTtTableBaseWeight.Name = "TxtTtTableBaseWeight"
        TxtTtTableBaseWeight.Size = New Size(75, 29)
        TxtTtTableBaseWeight.TabIndex = 11
        ' 
        ' TxtTtTableBaselength
        ' 
        TxtTtTableBaselength.Location = New Point(106, 223)
        TxtTtTableBaselength.Name = "TxtTtTableBaselength"
        TxtTtTableBaselength.Size = New Size(75, 29)
        TxtTtTableBaselength.TabIndex = 10
        ' 
        ' TxtTtTableTopWeight
        ' 
        TxtTtTableTopWeight.Location = New Point(106, 141)
        TxtTtTableTopWeight.Name = "TxtTtTableTopWeight"
        TxtTtTableTopWeight.Size = New Size(75, 29)
        TxtTtTableTopWeight.TabIndex = 9
        ' 
        ' TxtTtTableTopLength
        ' 
        TxtTtTableTopLength.Location = New Point(106, 104)
        TxtTtTableTopLength.Name = "TxtTtTableTopLength"
        TxtTtTableTopLength.Size = New Size(75, 29)
        TxtTtTableTopLength.TabIndex = 8
        ' 
        ' Label25
        ' 
        Label25.AutoSize = True
        Label25.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Label25.ForeColor = Color.OrangeRed
        Label25.Location = New Point(86, 189)
        Label25.Name = "Label25"
        Label25.Size = New Size(102, 25)
        Label25.TabIndex = 7
        Label25.Text = "Table Base"
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        Label24.ForeColor = Color.OrangeRed
        Label24.Location = New Point(97, 72)
        Label24.Name = "Label24"
        Label24.Size = New Size(93, 25)
        Label24.TabIndex = 6
        Label24.Text = "Table Top"
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.Location = New Point(31, 272)
        Label22.Name = "Label22"
        Label22.Size = New Size(66, 21)
        Label22.TabIndex = 5
        Label22.Text = "Weight"
        Label22.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label23
        ' 
        Label23.AutoSize = True
        Label23.Location = New Point(34, 227)
        Label23.Name = "Label23"
        Label23.Size = New Size(63, 21)
        Label23.TabIndex = 4
        Label23.Text = "Length"
        Label23.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Location = New Point(31, 145)
        Label21.Name = "Label21"
        Label21.Size = New Size(66, 21)
        Label21.TabIndex = 3
        Label21.Text = "Weight"
        Label21.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(34, 108)
        Label20.Name = "Label20"
        Label20.Size = New Size(63, 21)
        Label20.TabIndex = 2
        Label20.Text = "Length"
        Label20.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(46, 365)
        Label18.Name = "Label18"
        Label18.Size = New Size(294, 21)
        Label18.TabIndex = 1
        Label18.Text = "Calculate force required to tip a table"
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Georgia", 16.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label15.ForeColor = Color.Maroon
        Label15.Location = New Point(58, 23)
        Label15.Name = "Label15"
        Label15.Size = New Size(271, 38)
        Label15.TabIndex = 0
        Label15.Text = "Table Tip Force"
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.WhiteSmoke
        Panel4.BorderStyle = BorderStyle.Fixed3D
        Panel4.Controls.Add(GroupBox4)
        Panel4.Controls.Add(GroupBox3)
        Panel4.Controls.Add(GroupBox2)
        Panel4.Controls.Add(GroupBox1)
        Panel4.Controls.Add(Label13)
        Panel4.Location = New Point(762, 19)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(300, 568)
        Panel4.TabIndex = 3
        ' 
        ' GroupBox4
        ' 
        GroupBox4.BackColor = Color.Silver
        GroupBox4.Controls.Add(LblFraction2Decimal)
        GroupBox4.Controls.Add(TxtFraction2Decimal)
        GroupBox4.Controls.Add(Label16)
        GroupBox4.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        GroupBox4.Location = New Point(20, 435)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(257, 115)
        GroupBox4.TabIndex = 4
        GroupBox4.TabStop = False
        GroupBox4.Tag = "Decimal: {0}"
        GroupBox4.Text = "Fraction to Decimal"
        ' 
        ' LblFraction2Decimal
        ' 
        LblFraction2Decimal.AutoSize = True
        LblFraction2Decimal.Location = New Point(20, 67)
        LblFraction2Decimal.Name = "LblFraction2Decimal"
        LblFraction2Decimal.Size = New Size(73, 21)
        LblFraction2Decimal.TabIndex = 2
        LblFraction2Decimal.Tag = "Decimal: {0:N3} "
        LblFraction2Decimal.Text = "Decimal"
        ' 
        ' TxtFraction2Decimal
        ' 
        TxtFraction2Decimal.Location = New Point(98, 26)
        TxtFraction2Decimal.Name = "TxtFraction2Decimal"
        TxtFraction2Decimal.Size = New Size(81, 29)
        TxtFraction2Decimal.TabIndex = 1
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(20, 30)
        Label16.Name = "Label16"
        Label16.Size = New Size(72, 21)
        Label16.TabIndex = 0
        Label16.Text = "Fraction"
        ' 
        ' GroupBox3
        ' 
        GroupBox3.BackColor = Color.Silver
        GroupBox3.Controls.Add(LblDecimal2Fraction)
        GroupBox3.Controls.Add(TxtDecimal2Fraction)
        GroupBox3.Controls.Add(Label19)
        GroupBox3.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        GroupBox3.Location = New Point(20, 314)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(257, 115)
        GroupBox3.TabIndex = 3
        GroupBox3.TabStop = False
        GroupBox3.Text = "Decimal to Fraction"
        ' 
        ' LblDecimal2Fraction
        ' 
        LblDecimal2Fraction.AutoSize = True
        LblDecimal2Fraction.Location = New Point(20, 67)
        LblDecimal2Fraction.Name = "LblDecimal2Fraction"
        LblDecimal2Fraction.Size = New Size(72, 21)
        LblDecimal2Fraction.TabIndex = 2
        LblDecimal2Fraction.Tag = "Fraction: {0}"
        LblDecimal2Fraction.Text = "Fraction"
        ' 
        ' TxtDecimal2Fraction
        ' 
        TxtDecimal2Fraction.Location = New Point(98, 26)
        TxtDecimal2Fraction.Name = "TxtDecimal2Fraction"
        TxtDecimal2Fraction.Size = New Size(81, 29)
        TxtDecimal2Fraction.TabIndex = 1
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(20, 30)
        Label19.Name = "Label19"
        Label19.Size = New Size(73, 21)
        Label19.TabIndex = 0
        Label19.Text = "Decimal"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.BackColor = Color.Silver
        GroupBox2.Controls.Add(LblMM2Inches)
        GroupBox2.Controls.Add(TxtMm2Inches)
        GroupBox2.Controls.Add(Label17)
        GroupBox2.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        GroupBox2.Location = New Point(20, 193)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(257, 115)
        GroupBox2.TabIndex = 2
        GroupBox2.TabStop = False
        GroupBox2.Text = "MM to Inches"
        ' 
        ' LblMM2Inches
        ' 
        LblMM2Inches.AutoSize = True
        LblMM2Inches.Location = New Point(20, 67)
        LblMM2Inches.Name = "LblMM2Inches"
        LblMM2Inches.Size = New Size(59, 21)
        LblMM2Inches.TabIndex = 2
        LblMM2Inches.Tag = "Inches: {0:N2} in"
        LblMM2Inches.Text = "Inches"
        ' 
        ' TxtMm2Inches
        ' 
        TxtMm2Inches.Location = New Point(90, 26)
        TxtMm2Inches.Name = "TxtMm2Inches"
        TxtMm2Inches.Size = New Size(81, 29)
        TxtMm2Inches.TabIndex = 1
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(20, 30)
        Label17.Name = "Label17"
        Label17.Size = New Size(59, 21)
        Label17.TabIndex = 0
        Label17.Text = "Inches"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.BackColor = Color.Silver
        GroupBox1.Controls.Add(LblInches2MM)
        GroupBox1.Controls.Add(TxtInches2Mm)
        GroupBox1.Controls.Add(Label14)
        GroupBox1.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        GroupBox1.Location = New Point(20, 72)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(257, 115)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        GroupBox1.Text = "Inches to MM"
        ' 
        ' LblInches2MM
        ' 
        LblInches2MM.AutoSize = True
        LblInches2MM.Location = New Point(20, 67)
        LblInches2MM.Name = "LblInches2MM"
        LblInches2MM.Size = New Size(97, 21)
        LblInches2MM.TabIndex = 2
        LblInches2MM.Tag = "Millimeters: {0:N2} mm"
        LblInches2MM.Text = "Millimeters"
        ' 
        ' TxtInches2Mm
        ' 
        TxtInches2Mm.Location = New Point(90, 26)
        TxtInches2Mm.Name = "TxtInches2Mm"
        TxtInches2Mm.Size = New Size(81, 29)
        TxtInches2Mm.TabIndex = 1
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(20, 30)
        Label14.Name = "Label14"
        Label14.Size = New Size(59, 21)
        Label14.TabIndex = 0
        Label14.Text = "Inches"
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Georgia", 16.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label13.ForeColor = Color.Maroon
        Label13.Location = New Point(49, 24)
        Label13.Name = "Label13"
        Label13.Size = New Size(199, 38)
        Label13.TabIndex = 0
        Label13.Text = "Converters"
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.WhiteSmoke
        Panel3.BorderStyle = BorderStyle.Fixed3D
        Panel3.Controls.Add(LblPolygonPieceAngle)
        Panel3.Controls.Add(LblPolygonSideAngle)
        Panel3.Controls.Add(TxtPolygonSides)
        Panel3.Controls.Add(Label11)
        Panel3.Controls.Add(PbPolygon)
        Panel3.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Panel3.Location = New Point(26, 477)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(302, 462)
        Panel3.TabIndex = 2
        ' 
        ' LblPolygonPieceAngle
        ' 
        LblPolygonPieceAngle.AutoSize = True
        LblPolygonPieceAngle.Location = New Point(24, 100)
        LblPolygonPieceAngle.Name = "LblPolygonPieceAngle"
        LblPolygonPieceAngle.Size = New Size(168, 21)
        LblPolygonPieceAngle.TabIndex = 4
        LblPolygonPieceAngle.Tag = "Cut angle each piece: {0:N2}°"
        LblPolygonPieceAngle.Text = "Cut angle each piece"
        LblPolygonPieceAngle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LblPolygonSideAngle
        ' 
        LblPolygonSideAngle.AutoSize = True
        LblPolygonSideAngle.Location = New Point(24, 60)
        LblPolygonSideAngle.Name = "LblPolygonSideAngle"
        LblPolygonSideAngle.Size = New Size(130, 21)
        LblPolygonSideAngle.TabIndex = 3
        LblPolygonSideAngle.Tag = "Angle each side: {0:N2}°"
        LblPolygonSideAngle.Text = "Angle each side"
        LblPolygonSideAngle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TxtPolygonSides
        ' 
        TxtPolygonSides.Location = New Point(146, 15)
        TxtPolygonSides.MaxLength = 5
        TxtPolygonSides.Name = "TxtPolygonSides"
        TxtPolygonSides.Size = New Size(49, 29)
        TxtPolygonSides.TabIndex = 0
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(24, 19)
        Label11.Name = "Label11"
        Label11.Size = New Size(115, 21)
        Label11.TabIndex = 1
        Label11.Text = "Number sides"
        ' 
        ' PbPolygon
        ' 
        PbPolygon.BackColor = Color.LightGray
        PbPolygon.BorderStyle = BorderStyle.Fixed3D
        PbPolygon.Location = New Point(21, 144)
        PbPolygon.Name = "PbPolygon"
        PbPolygon.Size = New Size(257, 275)
        PbPolygon.TabIndex = 0
        PbPolygon.TabStop = False
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.WhiteSmoke
        Panel2.BorderStyle = BorderStyle.Fixed3D
        Panel2.Controls.Add(RtbFraction2Mm)
        Panel2.Controls.Add(RtbFraction2Decimal)
        Panel2.Controls.Add(Label10)
        Panel2.Controls.Add(Label8)
        Panel2.Location = New Point(282, 19)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(465, 449)
        Panel2.TabIndex = 1
        ' 
        ' RtbFraction2Mm
        ' 
        RtbFraction2Mm.BackColor = Color.White
        RtbFraction2Mm.DetectUrls = False
        RtbFraction2Mm.Location = New Point(239, 55)
        RtbFraction2Mm.Name = "RtbFraction2Mm"
        RtbFraction2Mm.ReadOnly = True
        RtbFraction2Mm.ScrollBars = RichTextBoxScrollBars.Vertical
        RtbFraction2Mm.ShowSelectionMargin = True
        RtbFraction2Mm.Size = New Size(209, 374)
        RtbFraction2Mm.TabIndex = 3
        RtbFraction2Mm.TabStop = False
        RtbFraction2Mm.Text = ""
        ' 
        ' RtbFraction2Decimal
        ' 
        RtbFraction2Decimal.BackColor = Color.White
        RtbFraction2Decimal.DetectUrls = False
        RtbFraction2Decimal.Location = New Point(12, 55)
        RtbFraction2Decimal.Name = "RtbFraction2Decimal"
        RtbFraction2Decimal.ReadOnly = True
        RtbFraction2Decimal.ScrollBars = RichTextBoxScrollBars.Vertical
        RtbFraction2Decimal.ShowSelectionMargin = True
        RtbFraction2Decimal.Size = New Size(209, 374)
        RtbFraction2Decimal.TabIndex = 2
        RtbFraction2Decimal.TabStop = False
        RtbFraction2Decimal.Text = ""
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold)
        Label10.ForeColor = Color.Maroon
        Label10.Location = New Point(286, 21)
        Label10.Name = "Label10"
        Label10.Size = New Size(126, 21)
        Label10.TabIndex = 1
        Label10.Text = "Fraction to mm"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold)
        Label8.ForeColor = Color.Maroon
        Label8.Location = New Point(43, 21)
        Label8.Name = "Label8"
        Label8.Size = New Size(159, 21)
        Label8.TabIndex = 0
        Label8.Text = "Fraction to Decimal"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.WhiteSmoke
        Panel1.BorderStyle = BorderStyle.Fixed3D
        Panel1.Controls.Add(RbEpoxyWaste20)
        Panel1.Controls.Add(RbEpoxyWaste15)
        Panel1.Controls.Add(RbEpoxyWaste10)
        Panel1.Controls.Add(RbEpoxyWaste0)
        Panel1.Controls.Add(Label7)
        Panel1.Controls.Add(TxtEpoxyDepth)
        Panel1.Controls.Add(TxtEpoxyWidth)
        Panel1.Controls.Add(TxtEpoxyLength)
        Panel1.Controls.Add(LblEpoxyPints)
        Panel1.Controls.Add(LblEpoxyQuarts)
        Panel1.Controls.Add(LblEpoxyGallons)
        Panel1.Controls.Add(LblEpoxyOunces)
        Panel1.Controls.Add(Label5)
        Panel1.Controls.Add(Label4)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(Label2)
        Panel1.Font = New Font("Segoe UI", 8.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Panel1.Location = New Point(26, 19)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(250, 449)
        Panel1.TabIndex = 0
        ' 
        ' RbEpoxyWaste20
        ' 
        RbEpoxyWaste20.AutoSize = True
        RbEpoxyWaste20.Location = New Point(140, 408)
        RbEpoxyWaste20.Name = "RbEpoxyWaste20"
        RbEpoxyWaste20.Size = New Size(67, 25)
        RbEpoxyWaste20.TabIndex = 5
        RbEpoxyWaste20.Text = "20%"
        RbEpoxyWaste20.UseVisualStyleBackColor = True
        ' 
        ' RbEpoxyWaste15
        ' 
        RbEpoxyWaste15.AutoSize = True
        RbEpoxyWaste15.Location = New Point(140, 375)
        RbEpoxyWaste15.Name = "RbEpoxyWaste15"
        RbEpoxyWaste15.Size = New Size(67, 25)
        RbEpoxyWaste15.TabIndex = 3
        RbEpoxyWaste15.Text = "15%"
        RbEpoxyWaste15.UseVisualStyleBackColor = True
        ' 
        ' RbEpoxyWaste10
        ' 
        RbEpoxyWaste10.AutoSize = True
        RbEpoxyWaste10.Location = New Point(40, 408)
        RbEpoxyWaste10.Name = "RbEpoxyWaste10"
        RbEpoxyWaste10.Size = New Size(67, 25)
        RbEpoxyWaste10.TabIndex = 4
        RbEpoxyWaste10.Text = "10%"
        RbEpoxyWaste10.UseVisualStyleBackColor = True
        ' 
        ' RbEpoxyWaste0
        ' 
        RbEpoxyWaste0.AutoSize = True
        RbEpoxyWaste0.Location = New Point(40, 375)
        RbEpoxyWaste0.Name = "RbEpoxyWaste0"
        RbEpoxyWaste0.Size = New Size(58, 25)
        RbEpoxyWaste0.TabIndex = 2
        RbEpoxyWaste0.Text = "0%"
        RbEpoxyWaste0.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.ForeColor = Color.Maroon
        Label7.Location = New Point(41, 338)
        Label7.Name = "Label7"
        Label7.Size = New Size(164, 25)
        Label7.TabIndex = 11
        Label7.Text = "Waste Percentage"
        ' 
        ' TxtEpoxyDepth
        ' 
        TxtEpoxyDepth.Location = New Point(102, 140)
        TxtEpoxyDepth.Name = "TxtEpoxyDepth"
        TxtEpoxyDepth.Size = New Size(69, 29)
        TxtEpoxyDepth.TabIndex = 2
        ' 
        ' TxtEpoxyWidth
        ' 
        TxtEpoxyWidth.Location = New Point(102, 100)
        TxtEpoxyWidth.Name = "TxtEpoxyWidth"
        TxtEpoxyWidth.Size = New Size(69, 29)
        TxtEpoxyWidth.TabIndex = 1
        ' 
        ' TxtEpoxyLength
        ' 
        TxtEpoxyLength.Location = New Point(102, 60)
        TxtEpoxyLength.Name = "TxtEpoxyLength"
        TxtEpoxyLength.Size = New Size(69, 29)
        TxtEpoxyLength.TabIndex = 0
        ' 
        ' LblEpoxyPints
        ' 
        LblEpoxyPints.AutoSize = True
        LblEpoxyPints.Location = New Point(23, 297)
        LblEpoxyPints.Name = "LblEpoxyPints"
        LblEpoxyPints.Size = New Size(117, 21)
        LblEpoxyPints.TabIndex = 7
        LblEpoxyPints.Tag = "Pints: {0:N2} pts"
        LblEpoxyPints.Text = "Pints required"
        LblEpoxyPints.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LblEpoxyQuarts
        ' 
        LblEpoxyQuarts.AutoSize = True
        LblEpoxyQuarts.Location = New Point(23, 260)
        LblEpoxyQuarts.Name = "LblEpoxyQuarts"
        LblEpoxyQuarts.Size = New Size(129, 21)
        LblEpoxyQuarts.TabIndex = 6
        LblEpoxyQuarts.Tag = "Quarts {0:N2} qts"
        LblEpoxyQuarts.Text = "Quarts required"
        LblEpoxyQuarts.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LblEpoxyGallons
        ' 
        LblEpoxyGallons.AutoSize = True
        LblEpoxyGallons.Location = New Point(23, 223)
        LblEpoxyGallons.Name = "LblEpoxyGallons"
        LblEpoxyGallons.Size = New Size(136, 21)
        LblEpoxyGallons.TabIndex = 5
        LblEpoxyGallons.Tag = "Gallons: {0:N2} gal"
        LblEpoxyGallons.Text = "Gallons required"
        LblEpoxyGallons.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' LblEpoxyOunces
        ' 
        LblEpoxyOunces.AutoSize = True
        LblEpoxyOunces.Location = New Point(23, 186)
        LblEpoxyOunces.Name = "LblEpoxyOunces"
        LblEpoxyOunces.Size = New Size(135, 21)
        LblEpoxyOunces.TabIndex = 4
        LblEpoxyOunces.Tag = "Ounces: {0:N2} oz"
        LblEpoxyOunces.Text = "Ounces required"
        LblEpoxyOunces.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(37, 144)
        Label5.Name = "Label5"
        Label5.Size = New Size(57, 21)
        Label5.TabIndex = 3
        Label5.Text = "Depth"
        Label5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(37, 104)
        Label4.Name = "Label4"
        Label4.Size = New Size(57, 21)
        Label4.TabIndex = 2
        Label4.Text = "Width"
        Label4.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(31, 64)
        Label3.Name = "Label3"
        Label3.Size = New Size(63, 21)
        Label3.TabIndex = 1
        Label3.Text = "Length"
        Label3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Georgia", 14.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Maroon
        Label2.Location = New Point(26, 13)
        Label2.Name = "Label2"
        Label2.Size = New Size(195, 32)
        Label2.TabIndex = 0
        Label2.Text = "Epoxy Pours"
        ' 
        ' TpLogs
        ' 
        TpLogs.BackColor = Color.Gainsboro
        TpLogs.BorderStyle = BorderStyle.Fixed3D
        TpLogs.Controls.Add(RbMetric)
        TpLogs.Controls.Add(RbImperial)
        TpLogs.Controls.Add(RtbLog)
        TpLogs.Location = New Point(4, 34)
        TpLogs.Name = "TpLogs"
        TpLogs.Size = New Size(1076, 958)
        TpLogs.TabIndex = 3
        TpLogs.Text = "Logs"
        ' 
        ' RbMetric
        ' 
        RbMetric.AutoSize = True
        RbMetric.Location = New Point(276, 75)
        RbMetric.Name = "RbMetric"
        RbMetric.Size = New Size(79, 25)
        RbMetric.TabIndex = 2
        RbMetric.TabStop = True
        RbMetric.Text = "Metric"
        RbMetric.UseVisualStyleBackColor = True
        ' 
        ' RbImperial
        ' 
        RbImperial.AutoSize = True
        RbImperial.Location = New Point(94, 81)
        RbImperial.Name = "RbImperial"
        RbImperial.Size = New Size(92, 25)
        RbImperial.TabIndex = 1
        RbImperial.TabStop = True
        RbImperial.Text = "Imperial"
        RbImperial.UseVisualStyleBackColor = True
        ' 
        ' RtbLog
        ' 
        RtbLog.Location = New Point(151, 239)
        RtbLog.Name = "RtbLog"
        RtbLog.ReadOnly = True
        RtbLog.ShowSelectionMargin = True
        RtbLog.Size = New Size(590, 583)
        RtbLog.TabIndex = 0
        RtbLog.Text = ""
        ' 
        ' TpDoors
        ' 
        TpDoors.BackColor = Color.Gainsboro
        TpDoors.BorderStyle = BorderStyle.Fixed3D
        TpDoors.Controls.Add(ScDoors)
        TpDoors.Location = New Point(4, 30)
        TpDoors.Name = "TpDoors"
        TpDoors.Size = New Size(1076, 962)
        TpDoors.TabIndex = 4
        TpDoors.Text = "Doors"
        ' 
        ' ScDoors
        ' 
        ScDoors.Dock = DockStyle.Top
        ScDoors.Location = New Point(0, 0)
        ScDoors.Name = "ScDoors"
        ' 
        ' ScDoors.Panel1
        ' 
        ScDoors.Panel1.Controls.Add(BtnDeleteDoorProject)
        ScDoors.Panel1.Controls.Add(BtnLoadDoorProject)
        ScDoors.Panel1.Controls.Add(GroupBox12)
        ScDoors.Panel1.Controls.Add(Label48)
        ScDoors.Panel1.Controls.Add(Panel6)
        ScDoors.Panel1.Controls.Add(GroupBox10)
        ScDoors.Panel1.Controls.Add(BtnCalculateDoors)
        ScDoors.Panel1.Controls.Add(BtnSaveDoorProject)
        ScDoors.Panel1.Controls.Add(Panel8)
        ScDoors.Panel1.Controls.Add(TxtDoorProjectName)
        ScDoors.Panel1.Controls.Add(Panel7)
        ' 
        ' ScDoors.Panel2
        ' 
        ScDoors.Panel2.Controls.Add(BtnPrintDoorResults)
        ScDoors.Panel2.Controls.Add(BtnExportDoorResults)
        ScDoors.Panel2.Controls.Add(Label50)
        ScDoors.Panel2.Controls.Add(PnlDoorResults)
        ScDoors.Panel2.Controls.Add(Label49)
        ScDoors.Size = New Size(1072, 709)
        ScDoors.SplitterDistance = 538
        ScDoors.TabIndex = 22
        ' 
        ' BtnDeleteDoorProject
        ' 
        BtnDeleteDoorProject.Location = New Point(372, 451)
        BtnDeleteDoorProject.Name = "BtnDeleteDoorProject"
        BtnDeleteDoorProject.Size = New Size(126, 34)
        BtnDeleteDoorProject.TabIndex = 28
        BtnDeleteDoorProject.Text = "Delete Project"
        BtnDeleteDoorProject.UseVisualStyleBackColor = True
        ' 
        ' BtnLoadDoorProject
        ' 
        BtnLoadDoorProject.Location = New Point(372, 411)
        BtnLoadDoorProject.Name = "BtnLoadDoorProject"
        BtnLoadDoorProject.Size = New Size(126, 34)
        BtnLoadDoorProject.TabIndex = 27
        BtnLoadDoorProject.Text = "Load Project"
        BtnLoadDoorProject.UseVisualStyleBackColor = True
        ' 
        ' GroupBox12
        ' 
        GroupBox12.BackColor = Color.WhiteSmoke
        GroupBox12.Controls.Add(Button1)
        GroupBox12.Controls.Add(BtnOfficeDoorPreset)
        GroupBox12.Controls.Add(BtnBathroomDoorPreset)
        GroupBox12.Controls.Add(BtnKitchenDoorPreset)
        GroupBox12.Location = New Point(27, 456)
        GroupBox12.Name = "GroupBox12"
        GroupBox12.Size = New Size(300, 103)
        GroupBox12.TabIndex = 23
        GroupBox12.TabStop = False
        GroupBox12.Text = "Door Presets"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(3, 25)
        Button1.Name = "Button1"
        Button1.Size = New Size(112, 34)
        Button1.TabIndex = 3
        Button1.Text = "Button1"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' BtnOfficeDoorPreset
        ' 
        BtnOfficeDoorPreset.BackColor = Color.Linen
        BtnOfficeDoorPreset.FlatAppearance.BorderColor = Color.FromArgb(CByte(255), CByte(128), CByte(0))
        BtnOfficeDoorPreset.Location = New Point(164, 21)
        BtnOfficeDoorPreset.Name = "BtnOfficeDoorPreset"
        BtnOfficeDoorPreset.Size = New Size(112, 34)
        BtnOfficeDoorPreset.TabIndex = 2
        BtnOfficeDoorPreset.Text = "Office"
        BtnOfficeDoorPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnBathroomDoorPreset
        ' 
        BtnBathroomDoorPreset.BackColor = Color.Linen
        BtnBathroomDoorPreset.FlatAppearance.BorderColor = Color.FromArgb(CByte(255), CByte(128), CByte(0))
        BtnBathroomDoorPreset.Location = New Point(25, 62)
        BtnBathroomDoorPreset.Name = "BtnBathroomDoorPreset"
        BtnBathroomDoorPreset.Size = New Size(112, 34)
        BtnBathroomDoorPreset.TabIndex = 1
        BtnBathroomDoorPreset.Text = "Bathroom"
        BtnBathroomDoorPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnKitchenDoorPreset
        ' 
        BtnKitchenDoorPreset.BackColor = Color.Linen
        BtnKitchenDoorPreset.FlatAppearance.BorderColor = Color.FromArgb(CByte(255), CByte(128), CByte(0))
        BtnKitchenDoorPreset.Location = New Point(25, 21)
        BtnKitchenDoorPreset.Name = "BtnKitchenDoorPreset"
        BtnKitchenDoorPreset.Size = New Size(112, 34)
        BtnKitchenDoorPreset.TabIndex = 0
        BtnKitchenDoorPreset.Text = "Kitchen"
        BtnKitchenDoorPreset.UseVisualStyleBackColor = False
        ' 
        ' Label48
        ' 
        Label48.AutoSize = True
        Label48.Font = New Font("Georgia", 16.0F, FontStyle.Bold)
        Label48.ForeColor = Color.Maroon
        Label48.Location = New Point(126, 18)
        Label48.Name = "Label48"
        Label48.Size = New Size(313, 38)
        Label48.TabIndex = 22
        Label48.Text = "Door Calculations"
        ' 
        ' Panel6
        ' 
        Panel6.BackColor = Color.WhiteSmoke
        Panel6.BorderStyle = BorderStyle.FixedSingle
        Panel6.Controls.Add(TxtCabinetOpeningHeight)
        Panel6.Controls.Add(TxtCabinetOpeningWidth)
        Panel6.Controls.Add(Label35)
        Panel6.Controls.Add(Label36)
        Panel6.Controls.Add(Label37)
        Panel6.Location = New Point(26, 78)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(487, 75)
        Panel6.TabIndex = 16
        ' 
        ' TxtCabinetOpeningHeight
        ' 
        TxtCabinetOpeningHeight.Location = New Point(336, 31)
        TxtCabinetOpeningHeight.Name = "TxtCabinetOpeningHeight"
        TxtCabinetOpeningHeight.Size = New Size(117, 29)
        TxtCabinetOpeningHeight.TabIndex = 6
        ' 
        ' TxtCabinetOpeningWidth
        ' 
        TxtCabinetOpeningWidth.Location = New Point(87, 31)
        TxtCabinetOpeningWidth.Name = "TxtCabinetOpeningWidth"
        TxtCabinetOpeningWidth.Size = New Size(117, 29)
        TxtCabinetOpeningWidth.TabIndex = 5
        ' 
        ' Label35
        ' 
        Label35.AutoSize = True
        Label35.Location = New Point(13, 6)
        Label35.Name = "Label35"
        Label35.Size = New Size(127, 21)
        Label35.TabIndex = 4
        Label35.Text = "Cabinet Opening"
        ' 
        ' Label36
        ' 
        Label36.AutoSize = True
        Label36.Location = New Point(274, 35)
        Label36.Name = "Label36"
        Label36.Size = New Size(56, 21)
        Label36.TabIndex = 1
        Label36.Text = "Height"
        Label36.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label37
        ' 
        Label37.AutoSize = True
        Label37.Location = New Point(29, 35)
        Label37.Name = "Label37"
        Label37.Size = New Size(52, 21)
        Label37.TabIndex = 0
        Label37.Text = "Width"
        Label37.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' GroupBox10
        ' 
        GroupBox10.BackColor = Color.WhiteSmoke
        GroupBox10.Controls.Add(LblPanelWidth)
        GroupBox10.Controls.Add(LblPanelHeight)
        GroupBox10.Controls.Add(Label46)
        GroupBox10.Controls.Add(LblStileLength)
        GroupBox10.Controls.Add(LblRailLength)
        GroupBox10.Location = New Point(26, 587)
        GroupBox10.Name = "GroupBox10"
        GroupBox10.Size = New Size(487, 107)
        GroupBox10.TabIndex = 15
        GroupBox10.TabStop = False
        GroupBox10.Text = "Rails - Stiles - Panels"
        ' 
        ' LblPanelWidth
        ' 
        LblPanelWidth.AutoSize = True
        LblPanelWidth.Location = New Point(248, 74)
        LblPanelWidth.Name = "LblPanelWidth"
        LblPanelWidth.Size = New Size(52, 21)
        LblPanelWidth.TabIndex = 4
        LblPanelWidth.Text = "Width"
        ' 
        ' LblPanelHeight
        ' 
        LblPanelHeight.AutoSize = True
        LblPanelHeight.Location = New Point(248, 44)
        LblPanelHeight.Name = "LblPanelHeight"
        LblPanelHeight.Size = New Size(56, 21)
        LblPanelHeight.TabIndex = 3
        LblPanelHeight.Text = "Height"
        ' 
        ' Label46
        ' 
        Label46.AutoSize = True
        Label46.Font = New Font("Segoe UI", 7.0F, FontStyle.Bold)
        Label46.Location = New Point(248, 14)
        Label46.Name = "Label46"
        Label46.Size = New Size(77, 19)
        Label46.TabIndex = 2
        Label46.Text = "Panel Size"
        ' 
        ' LblStileLength
        ' 
        LblStileLength.AutoSize = True
        LblStileLength.Font = New Font("Segoe UI", 7.0F)
        LblStileLength.Location = New Point(13, 61)
        LblStileLength.Name = "LblStileLength"
        LblStileLength.Size = New Size(40, 19)
        LblStileLength.TabIndex = 1
        LblStileLength.Tag = "{0} mm"
        LblStileLength.Text = "Stiles"
        LblStileLength.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblRailLength
        ' 
        LblRailLength.AutoSize = True
        LblRailLength.Font = New Font("Segoe UI", 7.0F)
        LblRailLength.Location = New Point(13, 27)
        LblRailLength.Name = "LblRailLength"
        LblRailLength.Size = New Size(36, 19)
        LblRailLength.TabIndex = 0
        LblRailLength.Tag = "{0} mm"
        LblRailLength.Text = "Rails"
        LblRailLength.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' BtnCalculateDoors
        ' 
        BtnCalculateDoors.Font = New Font("Segoe UI", 7.0F, FontStyle.Bold)
        BtnCalculateDoors.Location = New Point(358, 537)
        BtnCalculateDoors.Name = "BtnCalculateDoors"
        BtnCalculateDoors.Size = New Size(154, 34)
        BtnCalculateDoors.TabIndex = 21
        BtnCalculateDoors.Text = "Calculate Doors"
        BtnCalculateDoors.UseVisualStyleBackColor = True
        ' 
        ' BtnSaveDoorProject
        ' 
        BtnSaveDoorProject.Enabled = False
        BtnSaveDoorProject.FlatStyle = FlatStyle.System
        BtnSaveDoorProject.Location = New Point(372, 371)
        BtnSaveDoorProject.Name = "BtnSaveDoorProject"
        BtnSaveDoorProject.Size = New Size(126, 34)
        BtnSaveDoorProject.TabIndex = 18
        BtnSaveDoorProject.Text = "Save Project"
        BtnSaveDoorProject.UseVisualStyleBackColor = True
        ' 
        ' Panel8
        ' 
        Panel8.BackColor = Color.WhiteSmoke
        Panel8.BorderStyle = BorderStyle.FixedSingle
        Panel8.Controls.Add(TxtDoorOverlay)
        Panel8.Controls.Add(TxtGapSize)
        Panel8.Controls.Add(TxtRailWidth)
        Panel8.Controls.Add(TxtStileWidth)
        Panel8.Controls.Add(Label41)
        Panel8.Controls.Add(Label42)
        Panel8.Controls.Add(Label43)
        Panel8.Controls.Add(Panel9)
        Panel8.Controls.Add(Label44)
        Panel8.Controls.Add(Panel10)
        Panel8.Controls.Add(Label45)
        Panel8.Location = New Point(26, 159)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(223, 270)
        Panel8.TabIndex = 19
        ' 
        ' TxtDoorOverlay
        ' 
        TxtDoorOverlay.Location = New Point(114, 130)
        TxtDoorOverlay.Name = "TxtDoorOverlay"
        TxtDoorOverlay.Size = New Size(78, 29)
        TxtDoorOverlay.TabIndex = 15
        ' 
        ' TxtGapSize
        ' 
        TxtGapSize.Location = New Point(114, 226)
        TxtGapSize.Name = "TxtGapSize"
        TxtGapSize.Size = New Size(81, 29)
        TxtGapSize.TabIndex = 14
        ' 
        ' TxtRailWidth
        ' 
        TxtRailWidth.Location = New Point(114, 99)
        TxtRailWidth.Name = "TxtRailWidth"
        TxtRailWidth.Size = New Size(78, 29)
        TxtRailWidth.TabIndex = 13
        ' 
        ' TxtStileWidth
        ' 
        TxtStileWidth.Location = New Point(114, 69)
        TxtStileWidth.Name = "TxtStileWidth"
        TxtStileWidth.Size = New Size(78, 29)
        TxtStileWidth.TabIndex = 12
        ' 
        ' Label41
        ' 
        Label41.AutoSize = True
        Label41.Location = New Point(16, 103)
        Label41.Name = "Label41"
        Label41.Size = New Size(82, 21)
        Label41.TabIndex = 11
        Label41.Text = "Rail Width"
        Label41.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label42
        ' 
        Label42.Font = New Font("Segoe UI", 7.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label42.ImageAlign = ContentAlignment.MiddleRight
        Label42.Location = New Point(8, 224)
        Label42.Name = "Label42"
        Label42.Size = New Size(98, 38)
        Label42.TabIndex = 9
        Label42.Text = "Gap between doors"
        ' 
        ' Label43
        ' 
        Label43.AutoSize = True
        Label43.Location = New Point(20, 8)
        Label43.Name = "Label43"
        Label43.Size = New Size(104, 21)
        Label43.TabIndex = 0
        Label43.Text = "Door Options"
        ' 
        ' Panel9
        ' 
        Panel9.AutoSize = True
        Panel9.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Panel9.Controls.Add(RbInset)
        Panel9.Controls.Add(RbOverlay)
        Panel9.Location = New Point(32, 31)
        Panel9.Name = "Panel9"
        Panel9.Size = New Size(160, 34)
        Panel9.TabIndex = 4
        ' 
        ' RbInset
        ' 
        RbInset.AutoSize = True
        RbInset.Location = New Point(89, 5)
        RbInset.Name = "RbInset"
        RbInset.Size = New Size(68, 25)
        RbInset.TabIndex = 1
        RbInset.Tag = "1"
        RbInset.Text = "Inset"
        RbInset.UseVisualStyleBackColor = True
        ' 
        ' RbOverlay
        ' 
        RbOverlay.AutoSize = True
        RbOverlay.Location = New Point(8, 6)
        RbOverlay.Name = "RbOverlay"
        RbOverlay.Size = New Size(89, 25)
        RbOverlay.TabIndex = 0
        RbOverlay.Tag = "0"
        RbOverlay.Text = "Overlay"
        RbOverlay.UseVisualStyleBackColor = True
        ' 
        ' Label44
        ' 
        Label44.AutoSize = True
        Label44.Location = New Point(13, 73)
        Label44.Name = "Label44"
        Label44.Size = New Size(85, 21)
        Label44.TabIndex = 2
        Label44.Text = "Stile Width"
        Label44.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Panel10
        ' 
        Panel10.AutoSize = True
        Panel10.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Panel10.Controls.Add(Rb2Door)
        Panel10.Controls.Add(Rb1Door)
        Panel10.Location = New Point(20, 176)
        Panel10.Name = "Panel10"
        Panel10.Size = New Size(186, 33)
        Panel10.TabIndex = 5
        ' 
        ' Rb2Door
        ' 
        Rb2Door.AutoSize = True
        Rb2Door.Font = New Font("Segoe UI", 7.5F)
        Rb2Door.Location = New Point(97, 6)
        Rb2Door.Name = "Rb2Door"
        Rb2Door.Size = New Size(86, 24)
        Rb2Door.TabIndex = 1
        Rb2Door.Tag = "1"
        Rb2Door.Text = "2 Doors"
        Rb2Door.UseVisualStyleBackColor = True
        ' 
        ' Rb1Door
        ' 
        Rb1Door.AutoSize = True
        Rb1Door.Font = New Font("Segoe UI", 7.5F)
        Rb1Door.Location = New Point(4, 6)
        Rb1Door.Name = "Rb1Door"
        Rb1Door.Size = New Size(80, 24)
        Rb1Door.TabIndex = 0
        Rb1Door.Tag = "0"
        Rb1Door.Text = "1 Door"
        Rb1Door.UseVisualStyleBackColor = True
        ' 
        ' Label45
        ' 
        Label45.AutoSize = True
        Label45.Location = New Point(34, 134)
        Label45.Name = "Label45"
        Label45.Size = New Size(64, 21)
        Label45.TabIndex = 3
        Label45.Text = "Overlay"
        Label45.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TxtDoorProjectName
        ' 
        TxtDoorProjectName.AutoCompleteMode = AutoCompleteMode.Suggest
        TxtDoorProjectName.AutoCompleteSource = AutoCompleteSource.RecentlyUsedList
        TxtDoorProjectName.Location = New Point(270, 336)
        TxtDoorProjectName.MaxLength = 40
        TxtDoorProjectName.Name = "TxtDoorProjectName"
        TxtDoorProjectName.Size = New Size(228, 29)
        TxtDoorProjectName.TabIndex = 17
        ' 
        ' Panel7
        ' 
        Panel7.BackColor = Color.WhiteSmoke
        Panel7.BorderStyle = BorderStyle.FixedSingle
        Panel7.Controls.Add(TxtPanelExpansionGap)
        Panel7.Controls.Add(TxtPanelGrooveDepth)
        Panel7.Controls.Add(Label38)
        Panel7.Controls.Add(Label39)
        Panel7.Controls.Add(Label40)
        Panel7.Location = New Point(275, 191)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(223, 126)
        Panel7.TabIndex = 20
        ' 
        ' TxtPanelExpansionGap
        ' 
        TxtPanelExpansionGap.Location = New Point(139, 81)
        TxtPanelExpansionGap.Name = "TxtPanelExpansionGap"
        TxtPanelExpansionGap.Size = New Size(69, 29)
        TxtPanelExpansionGap.TabIndex = 4
        ' 
        ' TxtPanelGrooveDepth
        ' 
        TxtPanelGrooveDepth.Location = New Point(139, 35)
        TxtPanelGrooveDepth.Name = "TxtPanelGrooveDepth"
        TxtPanelGrooveDepth.Size = New Size(69, 29)
        TxtPanelGrooveDepth.TabIndex = 3
        ' 
        ' Label38
        ' 
        Label38.Font = New Font("Segoe UI", 7.5F)
        Label38.ImageAlign = ContentAlignment.MiddleRight
        Label38.Location = New Point(16, 71)
        Label38.Name = "Label38"
        Label38.Size = New Size(107, 49)
        Label38.TabIndex = 2
        Label38.Text = "Panel Expansion Gap"
        ' 
        ' Label39
        ' 
        Label39.AutoSize = True
        Label39.Font = New Font("Segoe UI", 7.5F)
        Label39.ImageAlign = ContentAlignment.MiddleRight
        Label39.Location = New Point(16, 39)
        Label39.Name = "Label39"
        Label39.Size = New Size(102, 20)
        Label39.TabIndex = 1
        Label39.Text = "Groove Depth"
        ' 
        ' Label40
        ' 
        Label40.AutoSize = True
        Label40.Location = New Point(14, 8)
        Label40.Name = "Label40"
        Label40.Size = New Size(106, 21)
        Label40.TabIndex = 0
        Label40.Text = "Panel Options"
        ' 
        ' BtnPrintDoorResults
        ' 
        BtnPrintDoorResults.Location = New Point(54, 639)
        BtnPrintDoorResults.Name = "BtnPrintDoorResults"
        BtnPrintDoorResults.Size = New Size(191, 34)
        BtnPrintDoorResults.TabIndex = 27
        BtnPrintDoorResults.Text = "Print Door Results"
        BtnPrintDoorResults.UseVisualStyleBackColor = True
        ' 
        ' BtnExportDoorResults
        ' 
        BtnExportDoorResults.Location = New Point(285, 639)
        BtnExportDoorResults.Name = "BtnExportDoorResults"
        BtnExportDoorResults.Size = New Size(191, 34)
        BtnExportDoorResults.TabIndex = 26
        BtnExportDoorResults.Text = "Export Door Results"
        BtnExportDoorResults.UseVisualStyleBackColor = True
        ' 
        ' Label50
        ' 
        Label50.AutoSize = True
        Label50.Font = New Font("Georgia", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label50.ForeColor = Color.Maroon
        Label50.Location = New Point(213, 191)
        Label50.Name = "Label50"
        Label50.Size = New Size(105, 29)
        Label50.TabIndex = 25
        Label50.Text = "Results"
        ' 
        ' PnlDoorResults
        ' 
        PnlDoorResults.BackColor = Color.WhiteSmoke
        PnlDoorResults.BorderStyle = BorderStyle.Fixed3D
        PnlDoorResults.Controls.Add(RtbDoorResults)
        PnlDoorResults.Location = New Point(0, 238)
        PnlDoorResults.Name = "PnlDoorResults"
        PnlDoorResults.Size = New Size(530, 381)
        PnlDoorResults.TabIndex = 24
        ' 
        ' RtbDoorResults
        ' 
        RtbDoorResults.Dock = DockStyle.Fill
        RtbDoorResults.Location = New Point(0, 0)
        RtbDoorResults.Name = "RtbDoorResults"
        RtbDoorResults.Size = New Size(526, 377)
        RtbDoorResults.TabIndex = 0
        RtbDoorResults.Text = ""
        ' 
        ' Label49
        ' 
        Label49.AutoSize = True
        Label49.Font = New Font("Georgia", 16.0F, FontStyle.Bold)
        Label49.ForeColor = Color.Maroon
        Label49.Location = New Point(122, 18)
        Label49.Name = "Label49"
        Label49.Size = New Size(313, 38)
        Label49.TabIndex = 23
        Label49.Text = "Door Calculations"
        ' 
        ' tTip
        ' 
        tTip.IsBalloon = True
        tTip.ShowAlways = True
        tTip.ToolTipIcon = ToolTipIcon.Info
        tTip.ToolTipTitle = "Woodworkers Friend"
        ' 
        ' TmrRotation
        ' 
        TmrRotation.Enabled = True
        TmrRotation.Interval = 30
        ' 
        ' TmrDoorCalculationDelay
        ' 
        TmrDoorCalculationDelay.Interval = 500
        ' 
        ' FrmMain
        ' 
        AutoScaleDimensions = New SizeF(9.0F, 21.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1084, 1118)
        Controls.Add(Tc)
        Controls.Add(Ss3)
        Controls.Add(Ss2)
        Controls.Add(Ss1)
        Font = New Font("Segoe UI", 8.0F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.Fixed3D
        MaximizeBox = False
        Name = "FrmMain"
        SizeGripStyle = SizeGripStyle.Hide
        Text = "Form1"
        Ss1.ResumeLayout(False)
        Ss1.PerformLayout()
        Ss3.ResumeLayout(False)
        Ss3.PerformLayout()
        Tc.ResumeLayout(False)
        TpDrawersDoors.ResumeLayout(False)
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel1.PerformLayout()
        SplitContainer1.Panel2.ResumeLayout(False)
        SplitContainer1.Panel2.PerformLayout()
        CType(SplitContainer1, ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        GroupBox9.ResumeLayout(False)
        CType(DgvDrawerHeights, ComponentModel.ISupportInitialize).EndInit()
        GroupBox8.ResumeLayout(False)
        GroupBox7.ResumeLayout(False)
        GroupBox6.ResumeLayout(False)
        GroupBox6.PerformLayout()
        GroupBox5.ResumeLayout(False)
        GroupBox5.PerformLayout()
        PnlResults.ResumeLayout(False)
        GroupBox11.ResumeLayout(False)
        GroupBox11.PerformLayout()
        TpBoardfeet.ResumeLayout(False)
        PnlBoardFeet.ResumeLayout(False)
        PnlBoardFeet.PerformLayout()
        CType(DgvBoardfeet, ComponentModel.ISupportInitialize).EndInit()
        TpCalculations.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        CType(PbPolygon, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        TpLogs.ResumeLayout(False)
        TpLogs.PerformLayout()
        TpDoors.ResumeLayout(False)
        ScDoors.Panel1.ResumeLayout(False)
        ScDoors.Panel1.PerformLayout()
        ScDoors.Panel2.ResumeLayout(False)
        ScDoors.Panel2.PerformLayout()
        CType(ScDoors, ComponentModel.ISupportInitialize).EndInit()
        ScDoors.ResumeLayout(False)
        GroupBox12.ResumeLayout(False)
        Panel6.ResumeLayout(False)
        Panel6.PerformLayout()
        GroupBox10.ResumeLayout(False)
        GroupBox10.PerformLayout()
        Panel8.ResumeLayout(False)
        Panel8.PerformLayout()
        Panel9.ResumeLayout(False)
        Panel9.PerformLayout()
        Panel10.ResumeLayout(False)
        Panel10.PerformLayout()
        Panel7.ResumeLayout(False)
        Panel7.PerformLayout()
        PnlDoorResults.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Ss1 As StatusStrip
    Friend WithEvents Ss2 As StatusStrip
    Friend WithEvents Ss3 As StatusStrip
    Friend WithEvents Tc As TabControl
    Friend WithEvents TpBoardfeet As TabPage
    Friend WithEvents TpDrawersDoors As TabPage
    Friend WithEvents TsslVersion As ToolStripStatusLabel
    Friend WithEvents TsslCpy As ToolStripStatusLabel
    Friend WithEvents TsslError As ToolStripStatusLabel
    Friend WithEvents TsslClock As ToolStripStatusLabel
    Friend WithEvents TsslToggleTheme As ToolStripDropDownButton
    Friend WithEvents PnlBoardFeet As Panel
    Friend WithEvents TpCalculations As TabPage
    Friend WithEvents DgvBoardfeet As DataGridView
    Friend WithEvents lblCalculateBoardfeet As Label
    Friend WithEvents bfCol0 As DataGridViewTextBoxColumn
    Friend WithEvents bfCol1 As DataGridViewTextBoxColumn
    Friend WithEvents bfCol2 As DataGridViewTextBoxColumn
    Friend WithEvents bfCol3 As DataGridViewTextBoxColumn
    Friend WithEvents bfCol4 As DataGridViewTextBoxColumn
    Friend WithEvents bfCol5 As DataGridViewTextBoxColumn
    Friend WithEvents bfCol6 As DataGridViewTextBoxColumn
    Friend WithEvents LblBoardFeetCost As Label
    Friend WithEvents LblTotalBoardFeet As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents LblBoardFeetCost20 As Label
    Friend WithEvents LblTotalBoardFeet20 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents LblBoardFeetCost15 As Label
    Friend WithEvents LblTotalBoardFeet15 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents LblBoardFeetCost10 As Label
    Friend WithEvents LblTotalBoardFeet10 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents BtnPrtBfProject As Button
    Friend WithEvents TxtBfProjectName As TextBox
    Friend WithEvents tTip As ToolTip
    Friend WithEvents BtnSaveBfProject As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents LblEpoxyPints As Label
    Friend WithEvents LblEpoxyQuarts As Label
    Friend WithEvents LblEpoxyGallons As Label
    Friend WithEvents LblEpoxyOunces As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtEpoxyDepth As TextBox
    Friend WithEvents TxtEpoxyWidth As TextBox
    Friend WithEvents TxtEpoxyLength As TextBox
    Friend WithEvents TsslMemoriam As ToolStripStatusLabel
    Friend WithEvents TsslScale As ToolStripStatusLabel
    Friend WithEvents RbEpoxyWaste20 As RadioButton
    Friend WithEvents RbEpoxyWaste15 As RadioButton
    Friend WithEvents RbEpoxyWaste10 As RadioButton
    Friend WithEvents RbEpoxyWaste0 As RadioButton
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents RtbFraction2Mm As RichTextBox
    Friend WithEvents RtbFraction2Decimal As RichTextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents LblPolygonPieceAngle As Label
    Friend WithEvents LblPolygonSideAngle As Label
    Friend WithEvents TxtPolygonSides As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents PbPolygon As PictureBox
    Friend WithEvents TmrRotation As Timer
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents LblDecimal2Fraction As Label
    Friend WithEvents TxtDecimal2Fraction As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents LblMM2Inches As Label
    Friend WithEvents TxtMm2Inches As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LblInches2MM As Label
    Friend WithEvents TxtInches2Mm As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents LblFraction2Decimal As Label
    Friend WithEvents TxtFraction2Decimal As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label24 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents LblTippingForce As Label
    Friend WithEvents TxtTtTableBaseWeight As TextBox
    Friend WithEvents TxtTtTableBaselength As TextBox
    Friend WithEvents TxtTtTableTopWeight As TextBox
    Friend WithEvents TxtTtTableTopLength As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents TxtMultiplier As TextBox
    Friend WithEvents TxtFirstDrawerHeight As TextBox
    Friend WithEvents Label31 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents TxtDrawerWidth As TextBox
    Friend WithEvents TxtDrawerSpacing As TextBox
    Friend WithEvents TxtDrawerCount As TextBox
    Friend WithEvents Label28 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents TxtArithmeticIncrement As TextBox
    Friend WithEvents Label32 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents BtnCustomCabinetPreset As Button
    Friend WithEvents BtnBathroomVanityPreset As Button
    Friend WithEvents BtnOfficeDeskPreset As Button
    Friend WithEvents BtnKitchenStandardPreset As Button
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents Button7 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents BtnCalculateDrawers As Button
    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents DgvDrawerHeights As DataGridView
    Friend WithEvents BtnSaveProject As Button
    Friend WithEvents TxtProjectName As TextBox
    Friend WithEvents Label34 As Label
    Friend WithEvents TpLogs As TabPage
    Friend WithEvents RtbLog As RichTextBox
    Friend WithEvents LblStatus As Label
    Friend WithEvents LbltotalDrawerHeightResults As Label
    Friend WithEvents LblTotalHeightResults As Label
    Friend WithEvents LblHeightRatioResults As Label
    Friend WithEvents LblAverageHeightResults As Label
    Friend WithEvents LblTotalMaterialResults As Label
    Friend WithEvents RtbResults As RichTextBox
    Friend WithEvents RbMetric As RadioButton
    Friend WithEvents RbImperial As RadioButton
    Friend WithEvents Label47 As Label
    Friend WithEvents TpDoors As TabPage
    Friend WithEvents BtnSaveDoorProject As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label38 As Label
    Friend WithEvents Label39 As Label
    Friend WithEvents Label40 As Label
    Friend WithEvents TxtDoorProjectName As TextBox
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label41 As Label
    Friend WithEvents Label42 As Label
    Friend WithEvents Label43 As Label
    Friend WithEvents Panel9 As Panel
    Friend WithEvents RbInset As RadioButton
    Friend WithEvents RbOverlay As RadioButton
    Friend WithEvents Label44 As Label
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Rb2Door As RadioButton
    Friend WithEvents Rb1Door As RadioButton
    Friend WithEvents Label45 As Label
    Friend WithEvents BtnCalculateDoors As Button
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Label35 As Label
    Friend WithEvents Label36 As Label
    Friend WithEvents Label37 As Label
    Friend WithEvents GroupBox10 As GroupBox
    Friend WithEvents LblPanelWidth As Label
    Friend WithEvents LblPanelHeight As Label
    Friend WithEvents Label46 As Label
    Friend WithEvents LblStileLength As Label
    Friend WithEvents LblRailLength As Label
    Friend WithEvents GroupBox11 As GroupBox
    Friend WithEvents RbGoldenRatio As RadioButton
    Friend WithEvents RbReverseArithmetic As RadioButton
    Friend WithEvents RbUniform As RadioButton
    Friend WithEvents RbCustomRatio As RadioButton
    Friend WithEvents RbExponential As RadioButton
    Friend WithEvents RbLogarithmic As RadioButton
    Friend WithEvents RbArithmetic As RadioButton
    Friend WithEvents RbFibonacci As RadioButton
    Friend WithEvents RbGeometric As RadioButton
    Friend WithEvents RbHambridge As RadioButton
    Friend WithEvents BtnGoldenRatioPreset As Button
    Friend WithEvents PnlResults As Panel
    Friend WithEvents BtnReverseArithmeticPreset As Button
    Friend WithEvents BtnUniformPreset As Button
    Friend WithEvents BtnCustomRatioPreset As Button
    Friend WithEvents BtnExponentialProgressionPreset As Button
    Friend WithEvents BtnLogarithmicProgressionPreset As Button
    Friend WithEvents LblCustomRatioInput As Label
    Friend WithEvents TxtCustomRatioInput As TextBox
    Friend WithEvents ScDoors As SplitContainer
    Friend WithEvents Label48 As Label
    Friend WithEvents Label49 As Label
    Friend WithEvents RtbDoorResults As RichTextBox
    Friend WithEvents TxtCabinetOpeningHeight As TextBox
    Friend WithEvents TxtCabinetOpeningWidth As TextBox
    Friend WithEvents TxtGapSize As TextBox
    Friend WithEvents TxtRailWidth As TextBox
    Friend WithEvents TxtStileWidth As TextBox
    Friend WithEvents TxtPanelGrooveDepth As TextBox
    Friend WithEvents TxtPanelExpansionGap As TextBox
    Friend WithEvents TxtDoorOverlay As TextBox
    Friend WithEvents Label50 As Label
    Friend WithEvents PnlDoorResults As Panel
    Friend WithEvents GroupBox12 As GroupBox
    Friend WithEvents BtnOfficeDoorPreset As Button
    Friend WithEvents BtnBathroomDoorPreset As Button
    Friend WithEvents BtnKitchenDoorPreset As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents BtnExportDoorResults As Button
    Friend WithEvents BtnLoadDoorProject As Button
    Friend WithEvents BtnDeleteDoorProject As Button
    Friend WithEvents TmrDoorCalculationDelay As Timer
    Friend WithEvents BtnPrintDoorResults As Button

End Class
