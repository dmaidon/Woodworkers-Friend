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

    ' Context menu for RtbLog
    Friend WithEvents CmsLog As ContextMenuStrip
    Friend WithEvents CmsLogSelectAll As ToolStripMenuItem
    Friend WithEvents CmsLogCopy As ToolStripMenuItem
    Friend WithEvents CmsLogCopyAll As ToolStripMenuItem
    Friend WithEvents CmsLogSeparator1 As ToolStripSeparator
    Friend WithEvents CmsLogFind As ToolStripMenuItem
    Friend WithEvents CmsLogSeparator2 As ToolStripSeparator
    Friend WithEvents CmsLogClear As ToolStripMenuItem
    Friend WithEvents CmsLogRefresh As ToolStripMenuItem
    Friend WithEvents CmsLogSeparator3 As ToolStripSeparator
    Friend WithEvents CmsLogSaveAs As ToolStripMenuItem

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        CmsLog = New ContextMenuStrip(components)
        CmsLogSelectAll = New ToolStripMenuItem()
        CmsLogCopy = New ToolStripMenuItem()
        CmsLogCopyAll = New ToolStripMenuItem()
        CmsLogSeparator1 = New ToolStripSeparator()
        CmsLogFind = New ToolStripMenuItem()
        CmsLogSeparator2 = New ToolStripSeparator()
        CmsLogClear = New ToolStripMenuItem()
        CmsLogRefresh = New ToolStripMenuItem()
        CmsLogSeparator3 = New ToolStripSeparator()
        CmsLogSaveAs = New ToolStripMenuItem()
        Ss1 = New StatusStrip()
        TsslVersion = New ToolStripStatusLabel()
        TsslCpy = New ToolStripStatusLabel()
        TsslError = New ToolStripStatusLabel()
        TsslClock = New ToolStripStatusLabel()
        Ss2 = New StatusStrip()
        Ss3 = New StatusStrip()
        TsslToggleTheme = New ToolStripDropDownButton()
        TsslMemoriam = New ToolStripStatusLabel()
        TsslToggleDoorExploded = New ToolStripStatusLabel()
        TsslScale = New ToolStripStatusLabel()
        Tc = New TabControl()
        TpDrawers = New TabPage()
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
        BtnDrawDrawerImage = New Button()
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
        TxtDrawerProjectName = New TextBox()
        TpDoors = New TabPage()
        GbSetScale = New GroupBox()
        RbImperial = New RadioButton()
        RbMetric = New RadioButton()
        ScDoors = New SplitContainer()
        LblTotalMaterialArea = New Label()
        LblDoorWidth = New Label()
        LblDoorHeight = New Label()
        BtnDeleteDoorProject = New Button()
        BtnLoadDoorProject = New Button()
        GroupBox12 = New GroupBox()
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
        RtbDoorResults = New RichTextBox()
        BtnDrawDoorImage = New Button()
        BtnPrintDoorResults = New Button()
        BtnExportDoorResults = New Button()
        Label50 = New Label()
        Label49 = New Label()
        TpBoardfeet = New TabPage()
        PnlBoardFeet = New Panel()
        BtnLoadBoardFeetHistory = New Button()
        BtnSaveBoardFeetHistory = New Button()
        LblBfProjectName = New Label()
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
        bfCol7 = New DataGridViewTextBoxColumn()
        TpCalcs = New TabPage()
        TcCalculattions = New TabControl()
        TpEpoxy = New TabPage()
        GbxAreaCalculator = New GroupBox()
        RbAreaBoth = New RadioButton()
        RbAreaTopcoat = New RadioButton()
        RbAreaPour = New RadioButton()
        DgvAreaCalc = New DataGridView()
        PnlStoneCoatTopCoat = New Panel()
        RbTcWaste20 = New RadioButton()
        RbTcWaste15 = New RadioButton()
        RbTcWaste10 = New RadioButton()
        RbTcWaste0 = New RadioButton()
        LblTcWastePct = New Label()
        LblTopCoatWaterMult = New Label()
        LblTCTotalMixture = New Label()
        LblPartB = New Label()
        LblPartA = New Label()
        LblTcMultiplier = New Label()
        TxtTotalArea = New TextBox()
        LblTotalArea = New Label()
        Label53 = New Label()
        PnlEpoxyPours = New Panel()
        LblEpoxyMilliliters = New Label()
        Label54 = New Label()
        TxtEpoxyArea = New TextBox()
        LblEpoxyArea = New Label()
        TxtEpoxyDiameter = New TextBox()
        Label52 = New Label()
        CmbEpoxyCost = New ComboBox()
        LblEpoxyCost = New Label()
        LblEpoxyLiters = New Label()
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
        TpConversions = New TabPage()
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
        Panel2 = New Panel()
        RtbFraction2Mm = New RichTextBox()
        RtbFraction2Decimal = New RichTextBox()
        Label10 = New Label()
        Label8 = New Label()
        TpCalculators = New TabPage()
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
        Panel3 = New Panel()
        Label51 = New Label()
        LblPolygonPieceAngle = New Label()
        LblPolygonSideAngle = New Label()
        TxtPolygonSides = New TextBox()
        Label11 = New Label()
        PbPolygon = New PictureBox()
        TpDrawings = New TabPage()
        PbOutputDrawing = New PictureBox()
        TpJoinery = New TabPage()
        ScJoinery = New SplitContainer()
        GbxMortiseTenonInput = New GroupBox()
        Label59 = New Label()
        TxtJointStockThickness = New TextBox()
        Label57 = New Label()
        TxtJointStockWidth = New TextBox()
        Label58 = New Label()
        RbTenonStandard = New RadioButton()
        RbTenonHaunched = New RadioButton()
        RbTenonThrough = New RadioButton()
        GbxDado = New GroupBox()
        TxtDadoStockThickness = New TextBox()
        LblDadoStockThickness = New Label()
        TxtDadoShelfThickness = New TextBox()
        LblDadoShelfThickness = New Label()
        GbxDovetails = New GroupBox()
        TxtDovetailSpacing = New TextBox()
        ChkDovetailHardwood = New CheckBox()
        LblDovetailThickness = New Label()
        TxtDovetailWidth = New TextBox()
        LblDovetailSpacing = New Label()
        TxtDovetailThickness = New TextBox()
        LblDovetailWidth = New Label()
        GbxBoxJoint = New GroupBox()
        TxtBoxJointThickness = New TextBox()
        LblBoxJointThickness = New Label()
        TxtBoxJointWidth = New TextBox()
        LblBoxJointWidth = New Label()
        BtnCalculateJoinery = New Button()
        GbxMortiseTenonResults = New GroupBox()
        LblShoulderOffset = New Label()
        LblMortiseWidth = New Label()
        LblMortiseDepth = New Label()
        LblTenonWidth = New Label()
        LblTenonThickness = New Label()
        LblTenonLength = New Label()
        PbJointDiagram = New PictureBox()
        GbxDovetailResults = New GroupBox()
        LblDovetailCount = New Label()
        LblDovetailAngle = New Label()
        LblDovetailPinWidth = New Label()
        LblDovetailTailWidth = New Label()
        GbxDadoResults = New GroupBox()
        LblDadoDepth = New Label()
        LblDadoWidth = New Label()
        GbxBoxJointResults = New GroupBox()
        LblBoxJointPinWidth = New Label()
        LblBoxJointCount = New Label()
        TpWoodMovement = New TabPage()
        TcWoodMovement = New TabControl()
        TpWmWoodMovement = New TabPage()
        GbxWoodMovementResults = New GroupBox()
        LblMovementResult = New Label()
        LblMovementDirection = New Label()
        LblMovementFraction = New Label()
        GbxPanelGaps = New GroupBox()
        LblPanelGapMin = New Label()
        LblPanelGapMax = New Label()
        BtnCalculateMovement = New Button()
        GbxGrainDirection = New GroupBox()
        RbTangential = New RadioButton()
        RbRadial = New RadioButton()
        GbxWoodProperties = New GroupBox()
        LblWoodDensity = New Label()
        LblWoodType = New Label()
        GbxWoodMovementInput = New GroupBox()
        LblWoodSpecies = New Label()
        CmbWoodSpecies = New ComboBox()
        LblMovementWidth = New Label()
        TxtMovementWidth = New TextBox()
        LblInitialHumidity = New Label()
        TxtInitialHumidity = New TextBox()
        LblFinalHumidity = New Label()
        TxtFinalHumidity = New TextBox()
        LblHumidityPreset = New Label()
        CmbHumidityPreset = New ComboBox()
        TpWmShelfSag = New TabPage()
        GbShelfSupportType = New GroupBox()
        LblPinWidthUnits = New Label()
        TxtPinWidth = New TextBox()
        LblPinWidth = New Label()
        RbSupportPin = New RadioButton()
        LblBracketWidthUnits = New Label()
        TxtshelfBracketWidth = New TextBox()
        LblShelfBracketWidth = New Label()
        LblSupportTypeInfo = New Label()
        LblDadoDepthUnit = New Label()
        TxtDadoDepth1 = New TextBox()
        LblDadoDepth1 = New Label()
        RbSupportDado = New RadioButton()
        RbSupportBracket = New RadioButton()
        GbxShelfSagInput = New GroupBox()
        GbxStiffener = New GroupBox()
        TxtStiffenerThickness = New TextBox()
        TxtStiffenerheight = New TextBox()
        CmbStiffenerMaterial = New ComboBox()
        LblStiffenerMaterial = New Label()
        LblStiffenerThickness = New Label()
        LblStiffenerHeight = New Label()
        ChkBackStiffener = New CheckBox()
        ChkFrontStiffener = New CheckBox()
        TxtShelfLoad = New TextBox()
        LblShelfLoad = New Label()
        TxtShelfWidth = New TextBox()
        LblShelfWidth = New Label()
        TxtShelfThickness = New TextBox()
        LblShelfThickness = New Label()
        TxtShelfSpan = New TextBox()
        LblShelfSpan = New Label()
        CmbShelfMaterial = New ComboBox()
        LblShelfMaterial = New Label()
        BtnCalculateShelf = New Button()
        GbxShelfSagResults = New GroupBox()
        PbShelfDiagram = New PictureBox()
        LblShelfRecommendations = New Label()
        LblShelfWarning = New Label()
        LblShelfMaterialInfo = New Label()
        LblShelfMaxSpan = New Label()
        LblShelfSafetyStatus = New Label()
        LblShelfMaxLoad = New Label()
        LblShelfSafeLoad = New Label()
        LblShelfSagFraction = New Label()
        LblShelfSagInches = New Label()
        TpCutList = New TabPage()
        ScCutList = New SplitContainer()
        GbxCutListInput = New GroupBox()
        DgvCutList = New DataGridView()
        PnlCutListButtons = New Panel()
        BtnAddCutRow = New Button()
        BtnDeleteCutRow = New Button()
        PnlCutListOptions = New Panel()
        LblStockBoard = New Label()
        CmbStockBoard = New ComboBox()
        LblKerf = New Label()
        TxtKerf = New TextBox()
        BtnOptimize = New Button()
        BtnExportCutList = New Button()
        GbCutListResults = New GroupBox()
        LblBoardsNeeded = New Label()
        LblTotalCost = New Label()
        LblWastePercent = New Label()
        LblAvgEfficiency = New Label()
        GbCuttingDiagram = New GroupBox()
        PbCuttingDiagram = New PictureBox()
        PnlDiagramNav = New Panel()
        BtnPrevPattern = New Button()
        LblPatternInfo = New Label()
        BtnNextPattern = New Button()
        TpReferences = New TabPage()
        TcReferences = New TabControl()
        TpWoodProperties = New TabPage()
        PnlWoodDetails = New Panel()
        BtnAddWoodSpecies = New Button()
        BtnPrintWoodData = New Button()
        BtnExportWoodData = New Button()
        BtnCompareWoods = New Button()
        RtbWoodDetails = New RichTextBox()
        LblWoodDetailsHeader = New Label()
        PnlWoodProperties = New Panel()
        LblWoodSearch = New Label()
        BtnWoodClearSearch = New Button()
        TxtWoodSearch = New TextBox()
        RbWoodSoftwoods = New RadioButton()
        RbWoodHardwoods = New RadioButton()
        RbWoodAll = New RadioButton()
        LblWoodPropertiesReference = New Label()
        DgvWoodProperties = New DataGridView()
        TpJoineryReference = New TabPage()
        Panel11 = New Panel()
        Label67 = New Label()
        TxtJoineryHistory = New TextBox()
        Label66 = New Label()
        Label65 = New Label()
        Label64 = New Label()
        Label63 = New Label()
        TxtJoineryReinforcement = New TextBox()
        TxtJoineryStrengthChar = New TextBox()
        TxtJoineryTools = New TextBox()
        TxtJoineryUses = New TextBox()
        TxtJoineryDescription = New TextBox()
        Label62 = New Label()
        Panel1 = New Panel()
        LblJoineryGlue = New Label()
        LblJoineryStrength = New Label()
        LblJoineryDifficulty = New Label()
        LblJoineryCategory = New Label()
        LblJoineryName = New Label()
        LblSummary = New Label()
        GbxJoineryFilter = New GroupBox()
        LblJoineryCount = New Label()
        RbJoineryBeginner = New RadioButton()
        RbJoineryEdge = New RadioButton()
        RbJoineryBox = New RadioButton()
        RbJoineryFrame = New RadioButton()
        RbJoineryAll = New RadioButton()
        DgvJoineryTypes = New DataGridView()
        TpHardwareStandards = New TabPage()
        GbxHardwareFilter = New GroupBox()
        Label73 = New Label()
        RbHardwareFasteners = New RadioButton()
        RbHardwareSlides = New RadioButton()
        LblHardwareCount = New Label()
        RbHardwareHinges = New RadioButton()
        RbHardwareAll = New RadioButton()
        RbHardwareShelf = New RadioButton()
        ScHardwareMain = New SplitContainer()
        DgvHardware = New DataGridView()
        Label72 = New Label()
        Label71 = New Label()
        Label70 = New Label()
        Label69 = New Label()
        Label68 = New Label()
        PnlHardwareSummaryInfo = New Panel()
        LblHardwareWeight = New Label()
        LblHardwareDimensions = New Label()
        LblhardwareBrand = New Label()
        LblHardwareCategory = New Label()
        LblHardwareType = New Label()
        TxtHardwarePartNumber = New TextBox()
        TxtHardwareInstallation = New TextBox()
        TxtHardwareMounting = New TextBox()
        TxtHardwareUses = New TextBox()
        TxtHardwareDescription = New TextBox()
        TpHelp = New TabPage()
        TpAbout = New TabPage()
        GbxAbout = New GroupBox()
        TxtAppAbout = New TextBox()
        LbLogFiles = New ListBox()
        LblClickLoadLogFile = New Label()
        RtbLog = New RichTextBox()
        Label55 = New Label()
        Label56 = New Label()
        Label60 = New Label()
        Label61 = New Label()
        ColCutLabel = New DataGridViewTextBoxColumn()
        ColCutLength = New DataGridViewTextBoxColumn()
        ColCutWidth = New DataGridViewTextBoxColumn()
        ColCutQuantity = New DataGridViewTextBoxColumn()
        TmrRotation = New Timer(components)
        TmrDoorCalculationDelay = New Timer(components)
        TmrClock = New Timer(components)
        BtnManageCosts = New Button()
        LblManageCosts = New Label()
        CmsLog.SuspendLayout()
        Ss1.SuspendLayout()
        Ss3.SuspendLayout()
        Tc.SuspendLayout()
        TpDrawers.SuspendLayout()
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
        TpDoors.SuspendLayout()
        GbSetScale.SuspendLayout()
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
        TpBoardfeet.SuspendLayout()
        PnlBoardFeet.SuspendLayout()
        CType(DgvBoardfeet, ComponentModel.ISupportInitialize).BeginInit()
        TpCalcs.SuspendLayout()
        TcCalculattions.SuspendLayout()
        TpEpoxy.SuspendLayout()
        GbxAreaCalculator.SuspendLayout()
        CType(DgvAreaCalc, ComponentModel.ISupportInitialize).BeginInit()
        PnlStoneCoatTopCoat.SuspendLayout()
        PnlEpoxyPours.SuspendLayout()
        TpConversions.SuspendLayout()
        Panel4.SuspendLayout()
        GroupBox4.SuspendLayout()
        GroupBox3.SuspendLayout()
        GroupBox2.SuspendLayout()
        GroupBox1.SuspendLayout()
        Panel2.SuspendLayout()
        TpCalculators.SuspendLayout()
        Panel5.SuspendLayout()
        Panel3.SuspendLayout()
        CType(PbPolygon, ComponentModel.ISupportInitialize).BeginInit()
        TpDrawings.SuspendLayout()
        CType(PbOutputDrawing, ComponentModel.ISupportInitialize).BeginInit()
        TpJoinery.SuspendLayout()
        CType(ScJoinery, ComponentModel.ISupportInitialize).BeginInit()
        ScJoinery.Panel1.SuspendLayout()
        ScJoinery.Panel2.SuspendLayout()
        ScJoinery.SuspendLayout()
        GbxMortiseTenonInput.SuspendLayout()
        GbxDado.SuspendLayout()
        GbxDovetails.SuspendLayout()
        GbxBoxJoint.SuspendLayout()
        GbxMortiseTenonResults.SuspendLayout()
        CType(PbJointDiagram, ComponentModel.ISupportInitialize).BeginInit()
        GbxDovetailResults.SuspendLayout()
        GbxDadoResults.SuspendLayout()
        GbxBoxJointResults.SuspendLayout()
        TpWoodMovement.SuspendLayout()
        TcWoodMovement.SuspendLayout()
        TpWmWoodMovement.SuspendLayout()
        GbxWoodMovementResults.SuspendLayout()
        GbxPanelGaps.SuspendLayout()
        GbxGrainDirection.SuspendLayout()
        GbxWoodProperties.SuspendLayout()
        GbxWoodMovementInput.SuspendLayout()
        TpWmShelfSag.SuspendLayout()
        GbShelfSupportType.SuspendLayout()
        GbxShelfSagInput.SuspendLayout()
        GbxStiffener.SuspendLayout()
        GbxShelfSagResults.SuspendLayout()
        CType(PbShelfDiagram, ComponentModel.ISupportInitialize).BeginInit()
        TpCutList.SuspendLayout()
        CType(ScCutList, ComponentModel.ISupportInitialize).BeginInit()
        ScCutList.Panel1.SuspendLayout()
        ScCutList.Panel2.SuspendLayout()
        ScCutList.SuspendLayout()
        GbxCutListInput.SuspendLayout()
        CType(DgvCutList, ComponentModel.ISupportInitialize).BeginInit()
        PnlCutListButtons.SuspendLayout()
        PnlCutListOptions.SuspendLayout()
        GbCutListResults.SuspendLayout()
        GbCuttingDiagram.SuspendLayout()
        CType(PbCuttingDiagram, ComponentModel.ISupportInitialize).BeginInit()
        PnlDiagramNav.SuspendLayout()
        TpReferences.SuspendLayout()
        TcReferences.SuspendLayout()
        TpWoodProperties.SuspendLayout()
        PnlWoodDetails.SuspendLayout()
        PnlWoodProperties.SuspendLayout()
        CType(DgvWoodProperties, ComponentModel.ISupportInitialize).BeginInit()
        TpJoineryReference.SuspendLayout()
        Panel11.SuspendLayout()
        Panel1.SuspendLayout()
        GbxJoineryFilter.SuspendLayout()
        CType(DgvJoineryTypes, ComponentModel.ISupportInitialize).BeginInit()
        TpHardwareStandards.SuspendLayout()
        GbxHardwareFilter.SuspendLayout()
        CType(ScHardwareMain, ComponentModel.ISupportInitialize).BeginInit()
        ScHardwareMain.Panel1.SuspendLayout()
        ScHardwareMain.Panel2.SuspendLayout()
        ScHardwareMain.SuspendLayout()
        CType(DgvHardware, ComponentModel.ISupportInitialize).BeginInit()
        PnlHardwareSummaryInfo.SuspendLayout()
        TpAbout.SuspendLayout()
        GbxAbout.SuspendLayout()
        SuspendLayout()
        ' 
        ' CmsLog
        ' 
        CmsLog.ImageScalingSize = New Size(24, 24)
        CmsLog.Items.AddRange(New ToolStripItem() {CmsLogSelectAll, CmsLogCopy, CmsLogCopyAll, CmsLogSeparator1, CmsLogFind, CmsLogSeparator2, CmsLogClear, CmsLogRefresh, CmsLogSeparator3, CmsLogSaveAs})
        CmsLog.Name = "CmsLog"
        CmsLog.Size = New Size(219, 246)
        ' 
        ' CmsLogSelectAll
        ' 
        CmsLogSelectAll.Name = "CmsLogSelectAll"
        CmsLogSelectAll.ShortcutKeys = Keys.Control Or Keys.A
        CmsLogSelectAll.Size = New Size(218, 32)
        CmsLogSelectAll.Text = "Select &All"
        ' 
        ' CmsLogCopy
        ' 
        CmsLogCopy.Name = "CmsLogCopy"
        CmsLogCopy.ShortcutKeys = Keys.Control Or Keys.C
        CmsLogCopy.Size = New Size(218, 32)
        CmsLogCopy.Text = "&Copy"
        ' 
        ' CmsLogCopyAll
        ' 
        CmsLogCopyAll.Name = "CmsLogCopyAll"
        CmsLogCopyAll.Size = New Size(218, 32)
        CmsLogCopyAll.Text = "Copy A&ll"
        ' 
        ' CmsLogSeparator1
        ' 
        CmsLogSeparator1.Name = "CmsLogSeparator1"
        CmsLogSeparator1.Size = New Size(215, 6)
        ' 
        ' CmsLogFind
        ' 
        CmsLogFind.Name = "CmsLogFind"
        CmsLogFind.ShortcutKeys = Keys.Control Or Keys.F
        CmsLogFind.Size = New Size(218, 32)
        CmsLogFind.Text = "&Find..."
        ' 
        ' CmsLogSeparator2
        ' 
        CmsLogSeparator2.Name = "CmsLogSeparator2"
        CmsLogSeparator2.Size = New Size(215, 6)
        ' 
        ' CmsLogClear
        ' 
        CmsLogClear.Name = "CmsLogClear"
        CmsLogClear.Size = New Size(218, 32)
        CmsLogClear.Text = "C&lear"
        ' 
        ' CmsLogRefresh
        ' 
        CmsLogRefresh.Name = "CmsLogRefresh"
        CmsLogRefresh.ShortcutKeys = Keys.F5
        CmsLogRefresh.Size = New Size(218, 32)
        CmsLogRefresh.Text = "&Refresh"
        ' 
        ' CmsLogSeparator3
        ' 
        CmsLogSeparator3.Name = "CmsLogSeparator3"
        CmsLogSeparator3.Size = New Size(215, 6)
        ' 
        ' CmsLogSaveAs
        ' 
        CmsLogSaveAs.Name = "CmsLogSaveAs"
        CmsLogSaveAs.Size = New Size(218, 32)
        CmsLogSaveAs.Text = "&Save As..."
        ' 
        ' Ss1
        ' 
        Ss1.GripMargin = New Padding(0)
        Ss1.ImageScalingSize = New Size(24, 24)
        Ss1.Items.AddRange(New ToolStripItem() {TsslVersion, TsslCpy, TsslError, TsslClock})
        Ss1.Location = New Point(0, 926)
        Ss1.Name = "Ss1"
        Ss1.Padding = New Padding(1, 0, 13, 0)
        Ss1.Size = New Size(1178, 32)
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
        TsslCpy.Font = New Font("Microsoft Sans Serif", 8.28F, FontStyle.Italic)
        TsslCpy.ForeColor = Color.Brown
        TsslCpy.Name = "TsslCpy"
        TsslCpy.Size = New Size(1103, 25)
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
        Ss2.Location = New Point(0, 905)
        Ss2.Name = "Ss2"
        Ss2.Padding = New Padding(1, 0, 13, 0)
        Ss2.Size = New Size(1178, 21)
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
        Ss3.Items.AddRange(New ToolStripItem() {TsslToggleTheme, TsslMemoriam, TsslToggleDoorExploded, TsslScale})
        Ss3.Location = New Point(0, 872)
        Ss3.Name = "Ss3"
        Ss3.Padding = New Padding(1, 0, 13, 0)
        Ss3.Size = New Size(1178, 33)
        Ss3.SizingGrip = False
        Ss3.TabIndex = 2
        Ss3.Text = "StatusStrip3"
        ' 
        ' TsslToggleTheme
        ' 
        TsslToggleTheme.Name = "TsslToggleTheme"
        TsslToggleTheme.Size = New Size(18, 30)
        ' 
        ' TsslMemoriam
        ' 
        TsslMemoriam.Name = "TsslMemoriam"
        TsslMemoriam.Size = New Size(1070, 26)
        TsslMemoriam.Spring = True
        ' 
        ' TsslToggleDoorExploded
        ' 
        TsslToggleDoorExploded.AutoToolTip = True
        TsslToggleDoorExploded.BackColor = Color.MintCream
        TsslToggleDoorExploded.BorderSides = ToolStripStatusLabelBorderSides.Left Or ToolStripStatusLabelBorderSides.Top Or ToolStripStatusLabelBorderSides.Right Or ToolStripStatusLabelBorderSides.Bottom
        TsslToggleDoorExploded.BorderStyle = Border3DStyle.Raised
        TsslToggleDoorExploded.Enabled = False
        TsslToggleDoorExploded.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Bold)
        TsslToggleDoorExploded.IsLink = True
        TsslToggleDoorExploded.Name = "TsslToggleDoorExploded"
        TsslToggleDoorExploded.Size = New Size(190, 26)
        TsslToggleDoorExploded.Text = "Toggle Door Exploded"
        TsslToggleDoorExploded.Visible = False
        ' 
        ' TsslScale
        ' 
        TsslScale.Font = New Font("Microsoft Sans Serif", 8.28F, FontStyle.Bold)
        TsslScale.ForeColor = Color.ForestGreen
        TsslScale.Name = "TsslScale"
        TsslScale.Size = New Size(76, 26)
        TsslScale.Text = "Imperial"
        ' 
        ' Tc
        ' 
        Tc.Controls.Add(TpDrawers)
        Tc.Controls.Add(TpDoors)
        Tc.Controls.Add(TpBoardfeet)
        Tc.Controls.Add(TpCalcs)
        Tc.Controls.Add(TpDrawings)
        Tc.Controls.Add(TpJoinery)
        Tc.Controls.Add(TpWoodMovement)
        Tc.Controls.Add(TpCutList)
        Tc.Controls.Add(TpReferences)
        Tc.Controls.Add(TpHelp)
        Tc.Controls.Add(TpAbout)
        Tc.Dock = DockStyle.Top
        Tc.Font = New Font("Georgia", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Tc.Location = New Point(0, 0)
        Tc.Name = "Tc"
        Tc.SelectedIndex = 0
        Tc.Size = New Size(1178, 854)
        Tc.TabIndex = 3
        ' 
        ' TpDrawers
        ' 
        TpDrawers.BackColor = Color.Gainsboro
        TpDrawers.BorderStyle = BorderStyle.Fixed3D
        TpDrawers.Controls.Add(SplitContainer1)
        TpDrawers.Location = New Point(4, 27)
        TpDrawers.Name = "TpDrawers"
        TpDrawers.Padding = New Padding(3)
        TpDrawers.Size = New Size(1170, 823)
        TpDrawers.TabIndex = 1
        TpDrawers.Text = "Drawers"
        ' 
        ' SplitContainer1
        ' 
        SplitContainer1.BorderStyle = BorderStyle.Fixed3D
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
        SplitContainer1.Panel1.Font = New Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        ' 
        ' SplitContainer1.Panel2
        ' 
        SplitContainer1.Panel2.Controls.Add(BtnDrawDrawerImage)
        SplitContainer1.Panel2.Controls.Add(PnlResults)
        SplitContainer1.Panel2.Controls.Add(GroupBox11)
        SplitContainer1.Panel2.Controls.Add(Label47)
        SplitContainer1.Panel2.Controls.Add(Label33)
        SplitContainer1.Panel2.Controls.Add(BtnSaveProject)
        SplitContainer1.Panel2.Controls.Add(Label34)
        SplitContainer1.Panel2.Controls.Add(TxtDrawerProjectName)
        SplitContainer1.Size = New Size(1160, 813)
        SplitContainer1.SplitterDistance = 524
        SplitContainer1.SplitterWidth = 8
        SplitContainer1.TabIndex = 0
        ' 
        ' LblAverageHeightResults
        ' 
        LblAverageHeightResults.AutoSize = True
        LblAverageHeightResults.Location = New Point(17, 707)
        LblAverageHeightResults.Name = "LblAverageHeightResults"
        LblAverageHeightResults.Size = New Size(129, 21)
        LblAverageHeightResults.TabIndex = 15
        LblAverageHeightResults.Text = "Average Height"
        ' 
        ' LblTotalMaterialResults
        ' 
        LblTotalMaterialResults.AutoSize = True
        LblTotalMaterialResults.Location = New Point(17, 732)
        LblTotalMaterialResults.Name = "LblTotalMaterialResults"
        LblTotalMaterialResults.Size = New Size(116, 21)
        LblTotalMaterialResults.TabIndex = 14
        LblTotalMaterialResults.Text = "Total Material"
        ' 
        ' LblHeightRatioResults
        ' 
        LblHeightRatioResults.AutoSize = True
        LblHeightRatioResults.Location = New Point(17, 682)
        LblHeightRatioResults.Name = "LblHeightRatioResults"
        LblHeightRatioResults.Size = New Size(106, 21)
        LblHeightRatioResults.TabIndex = 13
        LblHeightRatioResults.Text = "Height Ratio"
        ' 
        ' LbltotalDrawerHeightResults
        ' 
        LbltotalDrawerHeightResults.AutoSize = True
        LbltotalDrawerHeightResults.Location = New Point(17, 757)
        LbltotalDrawerHeightResults.Name = "LbltotalDrawerHeightResults"
        LbltotalDrawerHeightResults.Size = New Size(163, 21)
        LbltotalDrawerHeightResults.TabIndex = 12
        LbltotalDrawerHeightResults.Text = "Total Drawer Height"
        ' 
        ' LblTotalHeightResults
        ' 
        LblTotalHeightResults.AutoSize = True
        LblTotalHeightResults.Location = New Point(17, 782)
        LblTotalHeightResults.Name = "LblTotalHeightResults"
        LblTotalHeightResults.Size = New Size(111, 21)
        LblTotalHeightResults.TabIndex = 11
        LblTotalHeightResults.Text = "Total Heights"
        ' 
        ' LblStatus
        ' 
        LblStatus.Location = New Point(62, 657)
        LblStatus.Name = "LblStatus"
        LblStatus.Size = New Size(392, 18)
        LblStatus.TabIndex = 10
        LblStatus.Text = "Status"
        LblStatus.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' GroupBox9
        ' 
        GroupBox9.BackColor = Color.WhiteSmoke
        GroupBox9.Controls.Add(DgvDrawerHeights)
        GroupBox9.Location = New Point(14, 473)
        GroupBox9.Name = "GroupBox9"
        GroupBox9.Size = New Size(478, 171)
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
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 7F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
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
        DgvDrawerHeights.Size = New Size(472, 143)
        DgvDrawerHeights.TabIndex = 0
        ' 
        ' GroupBox8
        ' 
        GroupBox8.BackColor = Color.WhiteSmoke
        GroupBox8.Controls.Add(Button7)
        GroupBox8.Controls.Add(Button6)
        GroupBox8.Controls.Add(BtnCalculateDrawers)
        GroupBox8.Location = New Point(14, 409)
        GroupBox8.Name = "GroupBox8"
        GroupBox8.Size = New Size(481, 64)
        GroupBox8.TabIndex = 5
        GroupBox8.TabStop = False
        GroupBox8.Text = "Actions"
        ' 
        ' Button7
        ' 
        Button7.BackColor = Color.DarkSalmon
        Button7.FlatAppearance.BorderColor = Color.FromArgb(CByte(192), CByte(64), CByte(0))
        Button7.Location = New Point(341, 17)
        Button7.Name = "Button7"
        Button7.Size = New Size(112, 29)
        Button7.TabIndex = 2
        Button7.Text = "Clear Cache"
        Button7.UseVisualStyleBackColor = False
        ' 
        ' Button6
        ' 
        Button6.BackColor = Color.DarkSalmon
        Button6.FlatAppearance.BorderColor = Color.FromArgb(CByte(192), CByte(64), CByte(0))
        Button6.Location = New Point(184, 17)
        Button6.Name = "Button6"
        Button6.Size = New Size(112, 29)
        Button6.TabIndex = 1
        Button6.Text = "Clear"
        Button6.UseVisualStyleBackColor = False
        ' 
        ' BtnCalculateDrawers
        ' 
        BtnCalculateDrawers.BackColor = Color.DarkSalmon
        BtnCalculateDrawers.FlatAppearance.BorderColor = Color.FromArgb(CByte(192), CByte(64), CByte(0))
        BtnCalculateDrawers.Location = New Point(27, 17)
        BtnCalculateDrawers.Name = "BtnCalculateDrawers"
        BtnCalculateDrawers.Size = New Size(112, 29)
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
        GroupBox7.Location = New Point(14, 215)
        GroupBox7.Name = "GroupBox7"
        GroupBox7.Size = New Size(481, 194)
        GroupBox7.TabIndex = 4
        GroupBox7.TabStop = False
        GroupBox7.Text = "Quick Presets"
        ' 
        ' BtnUniformPreset
        ' 
        BtnUniformPreset.BackColor = Color.LightCyan
        BtnUniformPreset.FlatAppearance.BorderColor = Color.Blue
        BtnUniformPreset.Font = New Font("Microsoft Sans Serif", 7.25F)
        BtnUniformPreset.Location = New Point(241, 153)
        BtnUniformPreset.Name = "BtnUniformPreset"
        BtnUniformPreset.Size = New Size(196, 29)
        BtnUniformPreset.TabIndex = 9
        BtnUniformPreset.Tag = "2"
        BtnUniformPreset.Text = "Uniform"
        BtnUniformPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnCustomRatioPreset
        ' 
        BtnCustomRatioPreset.BackColor = Color.LightCyan
        BtnCustomRatioPreset.FlatAppearance.BorderColor = Color.Blue
        BtnCustomRatioPreset.Font = New Font("Microsoft Sans Serif", 7.25F)
        BtnCustomRatioPreset.Location = New Point(241, 122)
        BtnCustomRatioPreset.Name = "BtnCustomRatioPreset"
        BtnCustomRatioPreset.Size = New Size(196, 29)
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
        BtnExponentialProgressionPreset.Location = New Point(41, 153)
        BtnExponentialProgressionPreset.Name = "BtnExponentialProgressionPreset"
        BtnExponentialProgressionPreset.Size = New Size(196, 29)
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
        BtnLogarithmicProgressionPreset.Location = New Point(42, 122)
        BtnLogarithmicProgressionPreset.Name = "BtnLogarithmicProgressionPreset"
        BtnLogarithmicProgressionPreset.Size = New Size(196, 29)
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
        BtnReverseArithmeticPreset.Location = New Point(243, 87)
        BtnReverseArithmeticPreset.Name = "BtnReverseArithmeticPreset"
        BtnReverseArithmeticPreset.Size = New Size(196, 29)
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
        BtnGoldenRatioPreset.Location = New Point(42, 87)
        BtnGoldenRatioPreset.Name = "BtnGoldenRatioPreset"
        BtnGoldenRatioPreset.Size = New Size(196, 29)
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
        BtnCustomCabinetPreset.Location = New Point(243, 54)
        BtnCustomCabinetPreset.Name = "BtnCustomCabinetPreset"
        BtnCustomCabinetPreset.Size = New Size(196, 29)
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
        BtnBathroomVanityPreset.Location = New Point(243, 21)
        BtnBathroomVanityPreset.Name = "BtnBathroomVanityPreset"
        BtnBathroomVanityPreset.Size = New Size(196, 29)
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
        BtnOfficeDeskPreset.Location = New Point(42, 54)
        BtnOfficeDeskPreset.Name = "BtnOfficeDeskPreset"
        BtnOfficeDeskPreset.Size = New Size(196, 29)
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
        BtnKitchenStandardPreset.Location = New Point(42, 21)
        BtnKitchenStandardPreset.Name = "BtnKitchenStandardPreset"
        BtnKitchenStandardPreset.Size = New Size(196, 29)
        BtnKitchenStandardPreset.TabIndex = 0
        BtnKitchenStandardPreset.Tag = "0"
        BtnKitchenStandardPreset.Text = "Kitchen Standard"
        BtnKitchenStandardPreset.UseVisualStyleBackColor = False
        ' 
        ' Label32
        ' 
        Label32.AutoSize = True
        Label32.Font = New Font("Georgia", 16F, FontStyle.Bold)
        Label32.ForeColor = Color.Maroon
        Label32.Location = New Point(81, 11)
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
        GroupBox6.Location = New Point(244, 53)
        GroupBox6.Name = "GroupBox6"
        GroupBox6.Size = New Size(251, 157)
        GroupBox6.TabIndex = 2
        GroupBox6.TabStop = False
        GroupBox6.Text = "Method Specific Parameters"
        ' 
        ' LblCustomRatioInput
        ' 
        LblCustomRatioInput.AutoSize = True
        LblCustomRatioInput.Location = New Point(50, 126)
        LblCustomRatioInput.Name = "LblCustomRatioInput"
        LblCustomRatioInput.Size = New Size(112, 21)
        LblCustomRatioInput.TabIndex = 8
        LblCustomRatioInput.Text = "Custom Ratio"
        ' 
        ' TxtCustomRatioInput
        ' 
        TxtCustomRatioInput.Location = New Point(180, 115)
        TxtCustomRatioInput.Multiline = True
        TxtCustomRatioInput.Name = "TxtCustomRatioInput"
        TxtCustomRatioInput.Size = New Size(65, 40)
        TxtCustomRatioInput.TabIndex = 7
        ' 
        ' TxtArithmeticIncrement
        ' 
        TxtArithmeticIncrement.Location = New Point(180, 83)
        TxtArithmeticIncrement.Name = "TxtArithmeticIncrement"
        TxtArithmeticIncrement.Size = New Size(65, 29)
        TxtArithmeticIncrement.TabIndex = 6
        ' 
        ' TxtMultiplier
        ' 
        TxtMultiplier.Location = New Point(180, 51)
        TxtMultiplier.Name = "TxtMultiplier"
        TxtMultiplier.Size = New Size(65, 29)
        TxtMultiplier.TabIndex = 5
        ' 
        ' TxtFirstDrawerHeight
        ' 
        TxtFirstDrawerHeight.Location = New Point(180, 20)
        TxtFirstDrawerHeight.Name = "TxtFirstDrawerHeight"
        TxtFirstDrawerHeight.Size = New Size(65, 29)
        TxtFirstDrawerHeight.TabIndex = 4
        ' 
        ' Label31
        ' 
        Label31.AutoSize = True
        Label31.Location = New Point(5, 87)
        Label31.Name = "Label31"
        Label31.Size = New Size(173, 21)
        Label31.TabIndex = 2
        Label31.Text = "Arithmetic Increment"
        Label31.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label30
        ' 
        Label30.AutoSize = True
        Label30.Location = New Point(92, 55)
        Label30.Name = "Label30"
        Label30.Size = New Size(86, 21)
        Label30.TabIndex = 1
        Label30.Text = "Multiplier"
        Label30.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label29
        ' 
        Label29.AutoSize = True
        Label29.Location = New Point(31, 23)
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
        GroupBox5.Location = New Point(14, 53)
        GroupBox5.Name = "GroupBox5"
        GroupBox5.Size = New Size(224, 157)
        GroupBox5.TabIndex = 1
        GroupBox5.TabStop = False
        GroupBox5.Text = "Basic Parameters"
        ' 
        ' TxtDrawerWidth
        ' 
        TxtDrawerWidth.Location = New Point(146, 78)
        TxtDrawerWidth.Name = "TxtDrawerWidth"
        TxtDrawerWidth.Size = New Size(65, 29)
        TxtDrawerWidth.TabIndex = 5
        ' 
        ' TxtDrawerSpacing
        ' 
        TxtDrawerSpacing.Location = New Point(146, 49)
        TxtDrawerSpacing.Name = "TxtDrawerSpacing"
        TxtDrawerSpacing.Size = New Size(65, 29)
        TxtDrawerSpacing.TabIndex = 4
        ' 
        ' TxtDrawerCount
        ' 
        TxtDrawerCount.Location = New Point(146, 20)
        TxtDrawerCount.Name = "TxtDrawerCount"
        TxtDrawerCount.Size = New Size(65, 29)
        TxtDrawerCount.TabIndex = 3
        ' 
        ' Label28
        ' 
        Label28.AutoSize = True
        Label28.Location = New Point(24, 81)
        Label28.Name = "Label28"
        Label28.Size = New Size(116, 21)
        Label28.TabIndex = 2
        Label28.Text = "Drawer Width"
        Label28.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label27
        ' 
        Label27.AutoSize = True
        Label27.Location = New Point(10, 52)
        Label27.Name = "Label27"
        Label27.Size = New Size(130, 21)
        Label27.TabIndex = 1
        Label27.Text = "Drawer Spacing"
        Label27.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label26
        ' 
        Label26.AutoSize = True
        Label26.Location = New Point(55, 23)
        Label26.Name = "Label26"
        Label26.Size = New Size(85, 21)
        Label26.TabIndex = 0
        Label26.Text = "# Drawers"
        Label26.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' BtnDrawDrawerImage
        ' 
        BtnDrawDrawerImage.Enabled = False
        BtnDrawDrawerImage.Location = New Point(168, 439)
        BtnDrawDrawerImage.Name = "BtnDrawDrawerImage"
        BtnDrawDrawerImage.Size = New Size(211, 34)
        BtnDrawDrawerImage.TabIndex = 19
        BtnDrawDrawerImage.Text = "Drawer Shop Drawings"
        BtnDrawDrawerImage.UseVisualStyleBackColor = True
        ' 
        ' PnlResults
        ' 
        PnlResults.BorderStyle = BorderStyle.Fixed3D
        PnlResults.Controls.Add(RtbResults)
        PnlResults.Dock = DockStyle.Bottom
        PnlResults.Location = New Point(0, 541)
        PnlResults.Name = "PnlResults"
        PnlResults.Size = New Size(624, 268)
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
        RtbResults.Size = New Size(620, 264)
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
        GroupBox11.Font = New Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        GroupBox11.Location = New Point(12, 51)
        GroupBox11.Name = "GroupBox11"
        GroupBox11.Size = New Size(523, 315)
        GroupBox11.TabIndex = 17
        GroupBox11.TabStop = False
        GroupBox11.Text = "Calculation Methods"
        ' 
        ' RbArithmetic
        ' 
        RbArithmetic.AutoSize = True
        RbArithmetic.Font = New Font("Segoe UI", 7.5F, FontStyle.Bold)
        RbArithmetic.Location = New Point(25, 109)
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
        RbFibonacci.Location = New Point(25, 81)
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
        RbGeometric.Location = New Point(25, 52)
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
        RbHambridge.Location = New Point(25, 24)
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
        RbGoldenRatio.Location = New Point(25, 279)
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
        RbReverseArithmetic.Location = New Point(25, 250)
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
        RbUniform.Location = New Point(25, 222)
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
        RbCustomRatio.Location = New Point(25, 194)
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
        RbExponential.Location = New Point(25, 165)
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
        RbLogarithmic.Location = New Point(25, 137)
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
        Label47.Font = New Font("Georgia", 16F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label47.ForeColor = Color.Maroon
        Label47.Location = New Point(210, 500)
        Label47.Name = "Label47"
        Label47.Size = New Size(139, 38)
        Label47.TabIndex = 16
        Label47.Text = "Results"
        ' 
        ' Label33
        ' 
        Label33.AutoSize = True
        Label33.Font = New Font("Georgia", 16F, FontStyle.Bold)
        Label33.ForeColor = Color.Maroon
        Label33.Location = New Point(96, 13)
        Label33.Name = "Label33"
        Label33.Size = New Size(355, 38)
        Label33.TabIndex = 4
        Label33.Text = "Drawer Calculations"
        ' 
        ' BtnSaveProject
        ' 
        BtnSaveProject.Location = New Point(42, 399)
        BtnSaveProject.Name = "BtnSaveProject"
        BtnSaveProject.Size = New Size(112, 29)
        BtnSaveProject.TabIndex = 7
        BtnSaveProject.Text = "Save Project"
        BtnSaveProject.UseVisualStyleBackColor = True
        ' 
        ' Label34
        ' 
        Label34.AutoSize = True
        Label34.Font = New Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label34.Location = New Point(237, 405)
        Label34.Name = "Label34"
        Label34.Size = New Size(114, 21)
        Label34.TabIndex = 9
        Label34.Text = "Project Name"
        ' 
        ' TxtDrawerProjectName
        ' 
        TxtDrawerProjectName.Location = New Point(355, 401)
        TxtDrawerProjectName.Name = "TxtDrawerProjectName"
        TxtDrawerProjectName.Size = New Size(150, 26)
        TxtDrawerProjectName.TabIndex = 8
        ' 
        ' TpDoors
        ' 
        TpDoors.BackColor = Color.Gainsboro
        TpDoors.BorderStyle = BorderStyle.Fixed3D
        TpDoors.Controls.Add(GbSetScale)
        TpDoors.Controls.Add(ScDoors)
        TpDoors.Location = New Point(4, 27)
        TpDoors.Name = "TpDoors"
        TpDoors.Size = New Size(1170, 823)
        TpDoors.TabIndex = 4
        TpDoors.Text = "Doors"
        ' 
        ' GbSetScale
        ' 
        GbSetScale.BackColor = Color.WhiteSmoke
        GbSetScale.Controls.Add(RbImperial)
        GbSetScale.Controls.Add(RbMetric)
        GbSetScale.Location = New Point(396, 739)
        GbSetScale.Name = "GbSetScale"
        GbSetScale.Size = New Size(281, 71)
        GbSetScale.TabIndex = 23
        GbSetScale.TabStop = False
        GbSetScale.Text = "Set Scale"
        ' 
        ' RbImperial
        ' 
        RbImperial.AutoSize = True
        RbImperial.Checked = True
        RbImperial.Location = New Point(39, 25)
        RbImperial.Name = "RbImperial"
        RbImperial.Size = New Size(107, 22)
        RbImperial.TabIndex = 1
        RbImperial.TabStop = True
        RbImperial.Text = "Imperial"
        RbImperial.UseVisualStyleBackColor = True
        ' 
        ' RbMetric
        ' 
        RbMetric.AutoSize = True
        RbMetric.Location = New Point(162, 25)
        RbMetric.Name = "RbMetric"
        RbMetric.Size = New Size(87, 22)
        RbMetric.TabIndex = 2
        RbMetric.Text = "Metric"
        RbMetric.UseVisualStyleBackColor = True
        ' 
        ' ScDoors
        ' 
        ScDoors.BorderStyle = BorderStyle.Fixed3D
        ScDoors.Dock = DockStyle.Top
        ScDoors.Location = New Point(0, 0)
        ScDoors.Name = "ScDoors"
        ' 
        ' ScDoors.Panel1
        ' 
        ScDoors.Panel1.Controls.Add(LblTotalMaterialArea)
        ScDoors.Panel1.Controls.Add(LblDoorWidth)
        ScDoors.Panel1.Controls.Add(LblDoorHeight)
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
        ScDoors.Panel2.Controls.Add(RtbDoorResults)
        ScDoors.Panel2.Controls.Add(BtnDrawDoorImage)
        ScDoors.Panel2.Controls.Add(BtnPrintDoorResults)
        ScDoors.Panel2.Controls.Add(BtnExportDoorResults)
        ScDoors.Panel2.Controls.Add(Label50)
        ScDoors.Panel2.Controls.Add(Label49)
        ScDoors.Size = New Size(1166, 734)
        ScDoors.SplitterDistance = 554
        ScDoors.SplitterWidth = 8
        ScDoors.TabIndex = 22
        ' 
        ' LblTotalMaterialArea
        ' 
        LblTotalMaterialArea.AutoSize = True
        LblTotalMaterialArea.Location = New Point(31, 687)
        LblTotalMaterialArea.Name = "LblTotalMaterialArea"
        LblTotalMaterialArea.Size = New Size(171, 18)
        LblTotalMaterialArea.TabIndex = 31
        LblTotalMaterialArea.Text = "Total Material Area"
        ' 
        ' LblDoorWidth
        ' 
        LblDoorWidth.AutoSize = True
        LblDoorWidth.Location = New Point(360, 643)
        LblDoorWidth.Name = "LblDoorWidth"
        LblDoorWidth.Size = New Size(105, 18)
        LblDoorWidth.TabIndex = 30
        LblDoorWidth.Text = "Door Width"
        ' 
        ' LblDoorHeight
        ' 
        LblDoorHeight.AutoSize = True
        LblDoorHeight.Location = New Point(53, 639)
        LblDoorHeight.Name = "LblDoorHeight"
        LblDoorHeight.Size = New Size(108, 18)
        LblDoorHeight.TabIndex = 29
        LblDoorHeight.Text = "Door Height"
        ' 
        ' BtnDeleteDoorProject
        ' 
        BtnDeleteDoorProject.Location = New Point(372, 387)
        BtnDeleteDoorProject.Name = "BtnDeleteDoorProject"
        BtnDeleteDoorProject.Size = New Size(126, 29)
        BtnDeleteDoorProject.TabIndex = 28
        BtnDeleteDoorProject.Text = "Delete Project"
        BtnDeleteDoorProject.UseVisualStyleBackColor = True
        ' 
        ' BtnLoadDoorProject
        ' 
        BtnLoadDoorProject.Location = New Point(372, 352)
        BtnLoadDoorProject.Name = "BtnLoadDoorProject"
        BtnLoadDoorProject.Size = New Size(126, 29)
        BtnLoadDoorProject.TabIndex = 27
        BtnLoadDoorProject.Text = "Load Project"
        BtnLoadDoorProject.UseVisualStyleBackColor = True
        ' 
        ' GroupBox12
        ' 
        GroupBox12.BackColor = Color.WhiteSmoke
        GroupBox12.Controls.Add(BtnOfficeDoorPreset)
        GroupBox12.Controls.Add(BtnBathroomDoorPreset)
        GroupBox12.Controls.Add(BtnKitchenDoorPreset)
        GroupBox12.Location = New Point(27, 417)
        GroupBox12.Name = "GroupBox12"
        GroupBox12.Size = New Size(300, 88)
        GroupBox12.TabIndex = 23
        GroupBox12.TabStop = False
        GroupBox12.Text = "Door Presets"
        ' 
        ' BtnOfficeDoorPreset
        ' 
        BtnOfficeDoorPreset.BackColor = Color.Linen
        BtnOfficeDoorPreset.FlatAppearance.BorderColor = Color.FromArgb(CByte(255), CByte(128), CByte(0))
        BtnOfficeDoorPreset.Location = New Point(164, 18)
        BtnOfficeDoorPreset.Name = "BtnOfficeDoorPreset"
        BtnOfficeDoorPreset.Size = New Size(112, 29)
        BtnOfficeDoorPreset.TabIndex = 2
        BtnOfficeDoorPreset.Text = "Office"
        BtnOfficeDoorPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnBathroomDoorPreset
        ' 
        BtnBathroomDoorPreset.BackColor = Color.Linen
        BtnBathroomDoorPreset.FlatAppearance.BorderColor = Color.FromArgb(CByte(255), CByte(128), CByte(0))
        BtnBathroomDoorPreset.Location = New Point(25, 53)
        BtnBathroomDoorPreset.Name = "BtnBathroomDoorPreset"
        BtnBathroomDoorPreset.Size = New Size(112, 29)
        BtnBathroomDoorPreset.TabIndex = 1
        BtnBathroomDoorPreset.Text = "Bathroom"
        BtnBathroomDoorPreset.UseVisualStyleBackColor = False
        ' 
        ' BtnKitchenDoorPreset
        ' 
        BtnKitchenDoorPreset.BackColor = Color.Linen
        BtnKitchenDoorPreset.FlatAppearance.BorderColor = Color.FromArgb(CByte(255), CByte(128), CByte(0))
        BtnKitchenDoorPreset.Location = New Point(25, 18)
        BtnKitchenDoorPreset.Name = "BtnKitchenDoorPreset"
        BtnKitchenDoorPreset.Size = New Size(112, 29)
        BtnKitchenDoorPreset.TabIndex = 0
        BtnKitchenDoorPreset.Text = "Kitchen"
        BtnKitchenDoorPreset.UseVisualStyleBackColor = False
        ' 
        ' Label48
        ' 
        Label48.AutoSize = True
        Label48.Font = New Font("Georgia", 16F, FontStyle.Bold)
        Label48.ForeColor = Color.Maroon
        Label48.Location = New Point(126, 15)
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
        Panel6.Location = New Point(26, 67)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(487, 65)
        Panel6.TabIndex = 16
        ' 
        ' TxtCabinetOpeningHeight
        ' 
        TxtCabinetOpeningHeight.Location = New Point(336, 27)
        TxtCabinetOpeningHeight.Name = "TxtCabinetOpeningHeight"
        TxtCabinetOpeningHeight.Size = New Size(117, 26)
        TxtCabinetOpeningHeight.TabIndex = 6
        ' 
        ' TxtCabinetOpeningWidth
        ' 
        TxtCabinetOpeningWidth.Location = New Point(87, 27)
        TxtCabinetOpeningWidth.Name = "TxtCabinetOpeningWidth"
        TxtCabinetOpeningWidth.Size = New Size(117, 26)
        TxtCabinetOpeningWidth.TabIndex = 5
        ' 
        ' Label35
        ' 
        Label35.AutoSize = True
        Label35.Location = New Point(13, 5)
        Label35.Name = "Label35"
        Label35.Size = New Size(146, 18)
        Label35.TabIndex = 4
        Label35.Text = "Cabinet Opening"
        ' 
        ' Label36
        ' 
        Label36.AutoSize = True
        Label36.Location = New Point(274, 30)
        Label36.Name = "Label36"
        Label36.Size = New Size(64, 18)
        Label36.TabIndex = 1
        Label36.Text = "Height"
        Label36.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label37
        ' 
        Label37.AutoSize = True
        Label37.Location = New Point(29, 30)
        Label37.Name = "Label37"
        Label37.Size = New Size(61, 18)
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
        GroupBox10.Location = New Point(26, 529)
        GroupBox10.Name = "GroupBox10"
        GroupBox10.Size = New Size(487, 92)
        GroupBox10.TabIndex = 15
        GroupBox10.TabStop = False
        GroupBox10.Text = "Rails - Stiles - Panels"
        ' 
        ' LblPanelWidth
        ' 
        LblPanelWidth.AutoSize = True
        LblPanelWidth.Font = New Font("Georgia", 8F)
        LblPanelWidth.Location = New Point(248, 63)
        LblPanelWidth.Name = "LblPanelWidth"
        LblPanelWidth.Size = New Size(51, 18)
        LblPanelWidth.TabIndex = 4
        LblPanelWidth.Text = "Width"
        ' 
        ' LblPanelHeight
        ' 
        LblPanelHeight.AutoSize = True
        LblPanelHeight.Font = New Font("Georgia", 8F)
        LblPanelHeight.Location = New Point(248, 38)
        LblPanelHeight.Name = "LblPanelHeight"
        LblPanelHeight.Size = New Size(55, 18)
        LblPanelHeight.TabIndex = 3
        LblPanelHeight.Text = "Height"
        ' 
        ' Label46
        ' 
        Label46.AutoSize = True
        Label46.Font = New Font("Segoe UI", 7F, FontStyle.Bold)
        Label46.Location = New Point(248, 12)
        Label46.Name = "Label46"
        Label46.Size = New Size(77, 19)
        Label46.TabIndex = 2
        Label46.Text = "Panel Size"
        ' 
        ' LblStileLength
        ' 
        LblStileLength.AutoSize = True
        LblStileLength.Font = New Font("Georgia", 8F)
        LblStileLength.Location = New Point(13, 52)
        LblStileLength.Name = "LblStileLength"
        LblStileLength.Size = New Size(46, 18)
        LblStileLength.TabIndex = 1
        LblStileLength.Tag = "{0} mm"
        LblStileLength.Text = "Stiles"
        LblStileLength.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblRailLength
        ' 
        LblRailLength.AutoSize = True
        LblRailLength.Font = New Font("Georgia", 8F)
        LblRailLength.Location = New Point(13, 23)
        LblRailLength.Name = "LblRailLength"
        LblRailLength.Size = New Size(42, 18)
        LblRailLength.TabIndex = 0
        LblRailLength.Tag = "{0} mm"
        LblRailLength.Text = "Rails"
        LblRailLength.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' BtnCalculateDoors
        ' 
        BtnCalculateDoors.Font = New Font("Segoe UI", 7F, FontStyle.Bold)
        BtnCalculateDoors.Location = New Point(358, 486)
        BtnCalculateDoors.Name = "BtnCalculateDoors"
        BtnCalculateDoors.Size = New Size(154, 29)
        BtnCalculateDoors.TabIndex = 21
        BtnCalculateDoors.Text = "Calculate Doors"
        BtnCalculateDoors.UseVisualStyleBackColor = True
        ' 
        ' BtnSaveDoorProject
        ' 
        BtnSaveDoorProject.Enabled = False
        BtnSaveDoorProject.FlatStyle = FlatStyle.System
        BtnSaveDoorProject.Location = New Point(372, 318)
        BtnSaveDoorProject.Name = "BtnSaveDoorProject"
        BtnSaveDoorProject.Size = New Size(126, 29)
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
        Panel8.Location = New Point(26, 136)
        Panel8.Name = "Panel8"
        Panel8.Size = New Size(223, 275)
        Panel8.TabIndex = 19
        ' 
        ' TxtDoorOverlay
        ' 
        TxtDoorOverlay.Location = New Point(114, 120)
        TxtDoorOverlay.Name = "TxtDoorOverlay"
        TxtDoorOverlay.Size = New Size(78, 26)
        TxtDoorOverlay.TabIndex = 15
        ' 
        ' TxtGapSize
        ' 
        TxtGapSize.Location = New Point(114, 224)
        TxtGapSize.Name = "TxtGapSize"
        TxtGapSize.Size = New Size(81, 26)
        TxtGapSize.TabIndex = 14
        ' 
        ' TxtRailWidth
        ' 
        TxtRailWidth.Location = New Point(114, 94)
        TxtRailWidth.Name = "TxtRailWidth"
        TxtRailWidth.Size = New Size(78, 26)
        TxtRailWidth.TabIndex = 13
        ' 
        ' TxtStileWidth
        ' 
        TxtStileWidth.Location = New Point(114, 68)
        TxtStileWidth.Name = "TxtStileWidth"
        TxtStileWidth.Size = New Size(78, 26)
        TxtStileWidth.TabIndex = 12
        ' 
        ' Label41
        ' 
        Label41.AutoSize = True
        Label41.Location = New Point(16, 98)
        Label41.Name = "Label41"
        Label41.Size = New Size(99, 18)
        Label41.TabIndex = 11
        Label41.Text = "Rail Width"
        Label41.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label42
        ' 
        Label42.Font = New Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label42.ImageAlign = ContentAlignment.MiddleRight
        Label42.Location = New Point(8, 216)
        Label42.Name = "Label42"
        Label42.Size = New Size(98, 43)
        Label42.TabIndex = 9
        Label42.Text = "Gap between doors"
        ' 
        ' Label43
        ' 
        Label43.AutoSize = True
        Label43.Location = New Point(20, 7)
        Label43.Name = "Label43"
        Label43.Size = New Size(118, 18)
        Label43.TabIndex = 0
        Label43.Text = "Door Options"
        ' 
        ' Panel9
        ' 
        Panel9.AutoSize = True
        Panel9.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Panel9.Controls.Add(RbInset)
        Panel9.Controls.Add(RbOverlay)
        Panel9.Location = New Point(23, 30)
        Panel9.Name = "Panel9"
        Panel9.Size = New Size(182, 30)
        Panel9.TabIndex = 4
        ' 
        ' RbInset
        ' 
        RbInset.AutoSize = True
        RbInset.Location = New Point(103, 4)
        RbInset.Name = "RbInset"
        RbInset.Size = New Size(76, 22)
        RbInset.TabIndex = 1
        RbInset.Tag = "1"
        RbInset.Text = "Inset"
        RbInset.UseVisualStyleBackColor = True
        ' 
        ' RbOverlay
        ' 
        RbOverlay.AutoSize = True
        RbOverlay.Location = New Point(6, 5)
        RbOverlay.Name = "RbOverlay"
        RbOverlay.Size = New Size(97, 22)
        RbOverlay.TabIndex = 0
        RbOverlay.Tag = "0"
        RbOverlay.Text = "Overlay"
        RbOverlay.UseVisualStyleBackColor = True
        ' 
        ' Label44
        ' 
        Label44.AutoSize = True
        Label44.Location = New Point(13, 72)
        Label44.Name = "Label44"
        Label44.Size = New Size(103, 18)
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
        Panel10.Location = New Point(17, 168)
        Panel10.Name = "Panel10"
        Panel10.Size = New Size(186, 32)
        Panel10.TabIndex = 5
        ' 
        ' Rb2Door
        ' 
        Rb2Door.AutoSize = True
        Rb2Door.Font = New Font("Segoe UI", 7.5F)
        Rb2Door.Location = New Point(97, 5)
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
        Rb1Door.Location = New Point(4, 5)
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
        Label45.Location = New Point(34, 124)
        Label45.Name = "Label45"
        Label45.Size = New Size(72, 18)
        Label45.TabIndex = 3
        Label45.Text = "Overlay"
        Label45.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' TxtDoorProjectName
        ' 
        TxtDoorProjectName.AutoCompleteMode = AutoCompleteMode.Suggest
        TxtDoorProjectName.AutoCompleteSource = AutoCompleteSource.RecentlyUsedList
        TxtDoorProjectName.Location = New Point(270, 288)
        TxtDoorProjectName.MaxLength = 40
        TxtDoorProjectName.Name = "TxtDoorProjectName"
        TxtDoorProjectName.Size = New Size(228, 26)
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
        Panel7.Location = New Point(275, 164)
        Panel7.Name = "Panel7"
        Panel7.Size = New Size(223, 108)
        Panel7.TabIndex = 20
        ' 
        ' TxtPanelExpansionGap
        ' 
        TxtPanelExpansionGap.Location = New Point(139, 69)
        TxtPanelExpansionGap.Name = "TxtPanelExpansionGap"
        TxtPanelExpansionGap.Size = New Size(69, 26)
        TxtPanelExpansionGap.TabIndex = 4
        ' 
        ' TxtPanelGrooveDepth
        ' 
        TxtPanelGrooveDepth.Location = New Point(139, 30)
        TxtPanelGrooveDepth.Name = "TxtPanelGrooveDepth"
        TxtPanelGrooveDepth.Size = New Size(69, 26)
        TxtPanelGrooveDepth.TabIndex = 3
        ' 
        ' Label38
        ' 
        Label38.Font = New Font("Segoe UI", 7.5F)
        Label38.ImageAlign = ContentAlignment.MiddleRight
        Label38.Location = New Point(16, 61)
        Label38.Name = "Label38"
        Label38.Size = New Size(107, 42)
        Label38.TabIndex = 2
        Label38.Text = "Panel Expansion Gap"
        ' 
        ' Label39
        ' 
        Label39.AutoSize = True
        Label39.Font = New Font("Segoe UI", 7.5F)
        Label39.ImageAlign = ContentAlignment.MiddleRight
        Label39.Location = New Point(16, 33)
        Label39.Name = "Label39"
        Label39.Size = New Size(102, 20)
        Label39.TabIndex = 1
        Label39.Text = "Groove Depth"
        ' 
        ' Label40
        ' 
        Label40.AutoSize = True
        Label40.Location = New Point(14, 7)
        Label40.Name = "Label40"
        Label40.Size = New Size(125, 18)
        Label40.TabIndex = 0
        Label40.Text = "Panel Options"
        ' 
        ' RtbDoorResults
        ' 
        RtbDoorResults.Location = New Point(23, 205)
        RtbDoorResults.Name = "RtbDoorResults"
        RtbDoorResults.Size = New Size(555, 310)
        RtbDoorResults.TabIndex = 0
        RtbDoorResults.Text = ""
        ' 
        ' BtnDrawDoorImage
        ' 
        BtnDrawDoorImage.Enabled = False
        BtnDrawDoorImage.Location = New Point(204, 601)
        BtnDrawDoorImage.Name = "BtnDrawDoorImage"
        BtnDrawDoorImage.Size = New Size(190, 34)
        BtnDrawDoorImage.TabIndex = 28
        BtnDrawDoorImage.Text = "Draw Door Image"
        BtnDrawDoorImage.UseVisualStyleBackColor = True
        ' 
        ' BtnPrintDoorResults
        ' 
        BtnPrintDoorResults.Location = New Point(89, 548)
        BtnPrintDoorResults.Name = "BtnPrintDoorResults"
        BtnPrintDoorResults.Size = New Size(191, 29)
        BtnPrintDoorResults.TabIndex = 27
        BtnPrintDoorResults.Text = "Print Door Results"
        BtnPrintDoorResults.UseVisualStyleBackColor = True
        ' 
        ' BtnExportDoorResults
        ' 
        BtnExportDoorResults.Location = New Point(320, 548)
        BtnExportDoorResults.Name = "BtnExportDoorResults"
        BtnExportDoorResults.Size = New Size(191, 29)
        BtnExportDoorResults.TabIndex = 26
        BtnExportDoorResults.Text = "Export Door Results"
        BtnExportDoorResults.UseVisualStyleBackColor = True
        ' 
        ' Label50
        ' 
        Label50.AutoSize = True
        Label50.Font = New Font("Georgia", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label50.ForeColor = Color.Maroon
        Label50.Location = New Point(248, 164)
        Label50.Name = "Label50"
        Label50.Size = New Size(105, 29)
        Label50.TabIndex = 25
        Label50.Text = "Results"
        ' 
        ' Label49
        ' 
        Label49.AutoSize = True
        Label49.Font = New Font("Georgia", 16F, FontStyle.Bold)
        Label49.ForeColor = Color.Maroon
        Label49.Location = New Point(144, 15)
        Label49.Name = "Label49"
        Label49.Size = New Size(313, 38)
        Label49.TabIndex = 23
        Label49.Text = "Door Calculations"
        ' 
        ' TpBoardfeet
        ' 
        TpBoardfeet.BackColor = Color.Gainsboro
        TpBoardfeet.BorderStyle = BorderStyle.Fixed3D
        TpBoardfeet.Controls.Add(PnlBoardFeet)
        TpBoardfeet.Location = New Point(4, 27)
        TpBoardfeet.Name = "TpBoardfeet"
        TpBoardfeet.Padding = New Padding(3)
        TpBoardfeet.Size = New Size(1170, 823)
        TpBoardfeet.TabIndex = 0
        TpBoardfeet.Text = "Boardfeet"
        ' 
        ' PnlBoardFeet
        ' 
        PnlBoardFeet.BackColor = Color.WhiteSmoke
        PnlBoardFeet.BorderStyle = BorderStyle.Fixed3D
        PnlBoardFeet.Controls.Add(BtnLoadBoardFeetHistory)
        PnlBoardFeet.Controls.Add(BtnSaveBoardFeetHistory)
        PnlBoardFeet.Controls.Add(LblBfProjectName)
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
        PnlBoardFeet.Dock = DockStyle.Top
        PnlBoardFeet.Location = New Point(3, 3)
        PnlBoardFeet.Name = "PnlBoardFeet"
        PnlBoardFeet.Size = New Size(1160, 567)
        PnlBoardFeet.TabIndex = 0
        ' 
        ' BtnLoadBoardFeetHistory
        ' 
        BtnLoadBoardFeetHistory.BackColor = Color.MistyRose
        BtnLoadBoardFeetHistory.Location = New Point(619, 504)
        BtnLoadBoardFeetHistory.Name = "BtnLoadBoardFeetHistory"
        BtnLoadBoardFeetHistory.Size = New Size(190, 34)
        BtnLoadBoardFeetHistory.TabIndex = 20
        BtnLoadBoardFeetHistory.Text = "Load from History"
        BtnLoadBoardFeetHistory.UseVisualStyleBackColor = False
        ' 
        ' BtnSaveBoardFeetHistory
        ' 
        BtnSaveBoardFeetHistory.BackColor = Color.MistyRose
        BtnSaveBoardFeetHistory.Location = New Point(347, 504)
        BtnSaveBoardFeetHistory.Name = "BtnSaveBoardFeetHistory"
        BtnSaveBoardFeetHistory.Size = New Size(190, 34)
        BtnSaveBoardFeetHistory.TabIndex = 19
        BtnSaveBoardFeetHistory.Text = "Save to History"
        BtnSaveBoardFeetHistory.UseVisualStyleBackColor = False
        ' 
        ' LblBfProjectName
        ' 
        LblBfProjectName.AutoSize = True
        LblBfProjectName.Location = New Point(378, 453)
        LblBfProjectName.Name = "LblBfProjectName"
        LblBfProjectName.Size = New Size(129, 18)
        LblBfProjectName.TabIndex = 18
        LblBfProjectName.Text = "Project Name: "
        ' 
        ' BtnPrtBfProject
        ' 
        BtnPrtBfProject.BackColor = Color.MistyRose
        BtnPrtBfProject.Location = New Point(891, 504)
        BtnPrtBfProject.Name = "BtnPrtBfProject"
        BtnPrtBfProject.Size = New Size(190, 34)
        BtnPrtBfProject.TabIndex = 16
        BtnPrtBfProject.Text = "Print Project"
        BtnPrtBfProject.UseVisualStyleBackColor = False
        ' 
        ' TxtBfProjectName
        ' 
        TxtBfProjectName.Location = New Point(507, 449)
        TxtBfProjectName.Name = "TxtBfProjectName"
        TxtBfProjectName.Size = New Size(222, 26)
        TxtBfProjectName.TabIndex = 15
        ' 
        ' BtnSaveBfProject
        ' 
        BtnSaveBfProject.BackColor = Color.MistyRose
        BtnSaveBfProject.Location = New Point(75, 504)
        BtnSaveBfProject.Name = "BtnSaveBfProject"
        BtnSaveBfProject.Size = New Size(190, 34)
        BtnSaveBfProject.TabIndex = 14
        BtnSaveBfProject.Text = "Save Project"
        BtnSaveBfProject.UseVisualStyleBackColor = False
        ' 
        ' LblBoardFeetCost20
        ' 
        LblBoardFeetCost20.BorderStyle = BorderStyle.FixedSingle
        LblBoardFeetCost20.Location = New Point(851, 396)
        LblBoardFeetCost20.Name = "LblBoardFeetCost20"
        LblBoardFeetCost20.Size = New Size(175, 28)
        LblBoardFeetCost20.TabIndex = 13
        LblBoardFeetCost20.Text = "$0.00"
        LblBoardFeetCost20.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblTotalBoardFeet20
        ' 
        LblTotalBoardFeet20.BorderStyle = BorderStyle.FixedSingle
        LblTotalBoardFeet20.Font = New Font("Segoe UI", 7.25F)
        LblTotalBoardFeet20.Location = New Point(835, 357)
        LblTotalBoardFeet20.Name = "LblTotalBoardFeet20"
        LblTotalBoardFeet20.Size = New Size(206, 28)
        LblTotalBoardFeet20.TabIndex = 12
        LblTotalBoardFeet20.Text = "0.00"
        LblTotalBoardFeet20.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label12
        ' 
        Label12.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label12.Location = New Point(851, 321)
        Label12.Name = "Label12"
        Label12.Size = New Size(175, 27)
        Label12.TabIndex = 11
        Label12.Text = "Board Feet +20%"
        Label12.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblBoardFeetCost15
        ' 
        LblBoardFeetCost15.BorderStyle = BorderStyle.FixedSingle
        LblBoardFeetCost15.Location = New Point(611, 396)
        LblBoardFeetCost15.Name = "LblBoardFeetCost15"
        LblBoardFeetCost15.Size = New Size(175, 28)
        LblBoardFeetCost15.TabIndex = 10
        LblBoardFeetCost15.Text = "$0.00"
        LblBoardFeetCost15.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblTotalBoardFeet15
        ' 
        LblTotalBoardFeet15.BorderStyle = BorderStyle.FixedSingle
        LblTotalBoardFeet15.Font = New Font("Segoe UI", 7.25F)
        LblTotalBoardFeet15.Location = New Point(595, 357)
        LblTotalBoardFeet15.Name = "LblTotalBoardFeet15"
        LblTotalBoardFeet15.Size = New Size(206, 28)
        LblTotalBoardFeet15.TabIndex = 9
        LblTotalBoardFeet15.Text = "0.00"
        LblTotalBoardFeet15.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label9
        ' 
        Label9.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label9.Location = New Point(611, 321)
        Label9.Name = "Label9"
        Label9.Size = New Size(175, 27)
        Label9.TabIndex = 8
        Label9.Text = "Board Feet +15%"
        Label9.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblBoardFeetCost10
        ' 
        LblBoardFeetCost10.BorderStyle = BorderStyle.FixedSingle
        LblBoardFeetCost10.Location = New Point(371, 396)
        LblBoardFeetCost10.Name = "LblBoardFeetCost10"
        LblBoardFeetCost10.Size = New Size(175, 28)
        LblBoardFeetCost10.TabIndex = 7
        LblBoardFeetCost10.Text = "$0.00"
        LblBoardFeetCost10.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblTotalBoardFeet10
        ' 
        LblTotalBoardFeet10.BorderStyle = BorderStyle.FixedSingle
        LblTotalBoardFeet10.Font = New Font("Segoe UI", 7.25F)
        LblTotalBoardFeet10.Location = New Point(355, 357)
        LblTotalBoardFeet10.Name = "LblTotalBoardFeet10"
        LblTotalBoardFeet10.Size = New Size(206, 28)
        LblTotalBoardFeet10.TabIndex = 6
        LblTotalBoardFeet10.Text = "0.00"
        LblTotalBoardFeet10.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label6
        ' 
        Label6.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label6.Location = New Point(371, 321)
        Label6.Name = "Label6"
        Label6.Size = New Size(175, 27)
        Label6.TabIndex = 5
        Label6.Text = "Board Feet +10%"
        Label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblBoardFeetCost
        ' 
        LblBoardFeetCost.BorderStyle = BorderStyle.FixedSingle
        LblBoardFeetCost.Location = New Point(131, 396)
        LblBoardFeetCost.Name = "LblBoardFeetCost"
        LblBoardFeetCost.Size = New Size(175, 28)
        LblBoardFeetCost.TabIndex = 4
        LblBoardFeetCost.Text = "$0.00"
        LblBoardFeetCost.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LblTotalBoardFeet
        ' 
        LblTotalBoardFeet.BorderStyle = BorderStyle.FixedSingle
        LblTotalBoardFeet.Font = New Font("Segoe UI", 7.25F)
        LblTotalBoardFeet.Location = New Point(115, 357)
        LblTotalBoardFeet.Name = "LblTotalBoardFeet"
        LblTotalBoardFeet.Size = New Size(206, 28)
        LblTotalBoardFeet.TabIndex = 3
        LblTotalBoardFeet.Text = "0.00"
        LblTotalBoardFeet.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(0, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(100, 23)
        Label1.TabIndex = 17
        ' 
        ' lblCalculateBoardfeet
        ' 
        lblCalculateBoardfeet.AutoSize = True
        lblCalculateBoardfeet.Font = New Font("Georgia", 16F, FontStyle.Bold)
        lblCalculateBoardfeet.Location = New Point(425, 24)
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
        DgvBoardfeet.Columns.AddRange(New DataGridViewColumn() {bfCol0, bfCol1, bfCol2, bfCol3, bfCol4, bfCol5, bfCol6, bfCol7})
        DgvBoardfeet.Location = New Point(71, 63)
        DgvBoardfeet.Name = "DgvBoardfeet"
        DgvBoardfeet.RowHeadersWidth = 62
        DgvBoardfeet.Size = New Size(1014, 237)
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
        ' bfCol7
        ' 
        bfCol7.HeaderText = "Total Bm"
        bfCol7.MinimumWidth = 8
        bfCol7.Name = "bfCol7"
        ' 
        ' TpCalcs
        ' 
        TpCalcs.BorderStyle = BorderStyle.Fixed3D
        TpCalcs.Controls.Add(TcCalculattions)
        TpCalcs.Location = New Point(4, 27)
        TpCalcs.Name = "TpCalcs"
        TpCalcs.Size = New Size(1170, 823)
        TpCalcs.TabIndex = 6
        TpCalcs.Text = "Calculations"
        TpCalcs.UseVisualStyleBackColor = True
        ' 
        ' TcCalculattions
        ' 
        TcCalculattions.Alignment = TabAlignment.Right
        TcCalculattions.Controls.Add(TpEpoxy)
        TcCalculattions.Controls.Add(TpConversions)
        TcCalculattions.Controls.Add(TpCalculators)
        TcCalculattions.Dock = DockStyle.Fill
        TcCalculattions.Location = New Point(0, 0)
        TcCalculattions.Multiline = True
        TcCalculattions.Name = "TcCalculattions"
        TcCalculattions.SelectedIndex = 0
        TcCalculattions.Size = New Size(1166, 819)
        TcCalculattions.TabIndex = 0
        ' 
        ' TpEpoxy
        ' 
        TpEpoxy.BackColor = Color.Gainsboro
        TpEpoxy.BorderStyle = BorderStyle.Fixed3D
        TpEpoxy.Controls.Add(GbxAreaCalculator)
        TpEpoxy.Controls.Add(PnlStoneCoatTopCoat)
        TpEpoxy.Controls.Add(PnlEpoxyPours)
        TpEpoxy.Location = New Point(4, 4)
        TpEpoxy.Name = "TpEpoxy"
        TpEpoxy.Padding = New Padding(3)
        TpEpoxy.Size = New Size(1134, 811)
        TpEpoxy.TabIndex = 0
        TpEpoxy.Text = "Epoxy"
        ' 
        ' GbxAreaCalculator
        ' 
        GbxAreaCalculator.BackColor = Color.WhiteSmoke
        GbxAreaCalculator.Controls.Add(RbAreaBoth)
        GbxAreaCalculator.Controls.Add(RbAreaTopcoat)
        GbxAreaCalculator.Controls.Add(RbAreaPour)
        GbxAreaCalculator.Controls.Add(DgvAreaCalc)
        GbxAreaCalculator.Location = New Point(614, 9)
        GbxAreaCalculator.Name = "GbxAreaCalculator"
        GbxAreaCalculator.Size = New Size(415, 297)
        GbxAreaCalculator.TabIndex = 8
        GbxAreaCalculator.TabStop = False
        GbxAreaCalculator.Text = "Area Calculator"
        ' 
        ' RbAreaBoth
        ' 
        RbAreaBoth.AutoSize = True
        RbAreaBoth.Checked = True
        RbAreaBoth.Location = New Point(328, 262)
        RbAreaBoth.Name = "RbAreaBoth"
        RbAreaBoth.Size = New Size(72, 22)
        RbAreaBoth.TabIndex = 4
        RbAreaBoth.TabStop = True
        RbAreaBoth.Text = "Both"
        RbAreaBoth.UseVisualStyleBackColor = True
        ' 
        ' RbAreaTopcoat
        ' 
        RbAreaTopcoat.AutoSize = True
        RbAreaTopcoat.Location = New Point(186, 262)
        RbAreaTopcoat.Name = "RbAreaTopcoat"
        RbAreaTopcoat.Size = New Size(104, 22)
        RbAreaTopcoat.TabIndex = 3
        RbAreaTopcoat.Text = "TopCoat"
        RbAreaTopcoat.UseVisualStyleBackColor = True
        ' 
        ' RbAreaPour
        ' 
        RbAreaPour.AutoSize = True
        RbAreaPour.Location = New Point(75, 262)
        RbAreaPour.Name = "RbAreaPour"
        RbAreaPour.Size = New Size(73, 22)
        RbAreaPour.TabIndex = 2
        RbAreaPour.Text = "Pour"
        RbAreaPour.UseVisualStyleBackColor = True
        ' 
        ' DgvAreaCalc
        ' 
        DgvAreaCalc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DgvAreaCalc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvAreaCalc.Dock = DockStyle.Top
        DgvAreaCalc.Location = New Point(3, 22)
        DgvAreaCalc.Name = "DgvAreaCalc"
        DgvAreaCalc.RowHeadersVisible = False
        DgvAreaCalc.RowHeadersWidth = 62
        DgvAreaCalc.Size = New Size(409, 234)
        DgvAreaCalc.TabIndex = 1
        ' 
        ' PnlStoneCoatTopCoat
        ' 
        PnlStoneCoatTopCoat.BackColor = Color.WhiteSmoke
        PnlStoneCoatTopCoat.BorderStyle = BorderStyle.FixedSingle
        PnlStoneCoatTopCoat.Controls.Add(RbTcWaste20)
        PnlStoneCoatTopCoat.Controls.Add(RbTcWaste15)
        PnlStoneCoatTopCoat.Controls.Add(RbTcWaste10)
        PnlStoneCoatTopCoat.Controls.Add(RbTcWaste0)
        PnlStoneCoatTopCoat.Controls.Add(LblTcWastePct)
        PnlStoneCoatTopCoat.Controls.Add(LblTopCoatWaterMult)
        PnlStoneCoatTopCoat.Controls.Add(LblTCTotalMixture)
        PnlStoneCoatTopCoat.Controls.Add(LblPartB)
        PnlStoneCoatTopCoat.Controls.Add(LblPartA)
        PnlStoneCoatTopCoat.Controls.Add(LblTcMultiplier)
        PnlStoneCoatTopCoat.Controls.Add(TxtTotalArea)
        PnlStoneCoatTopCoat.Controls.Add(LblTotalArea)
        PnlStoneCoatTopCoat.Controls.Add(Label53)
        PnlStoneCoatTopCoat.Location = New Point(269, 9)
        PnlStoneCoatTopCoat.Name = "PnlStoneCoatTopCoat"
        PnlStoneCoatTopCoat.Size = New Size(334, 382)
        PnlStoneCoatTopCoat.TabIndex = 7
        ' 
        ' RbTcWaste20
        ' 
        RbTcWaste20.AutoSize = True
        RbTcWaste20.Location = New Point(227, 336)
        RbTcWaste20.Name = "RbTcWaste20"
        RbTcWaste20.Size = New Size(53, 22)
        RbTcWaste20.TabIndex = 15
        RbTcWaste20.Text = "20"
        RbTcWaste20.UseVisualStyleBackColor = True
        ' 
        ' RbTcWaste15
        ' 
        RbTcWaste15.AutoSize = True
        RbTcWaste15.Location = New Point(166, 336)
        RbTcWaste15.Name = "RbTcWaste15"
        RbTcWaste15.Size = New Size(52, 22)
        RbTcWaste15.TabIndex = 13
        RbTcWaste15.Text = "15"
        RbTcWaste15.UseVisualStyleBackColor = True
        ' 
        ' RbTcWaste10
        ' 
        RbTcWaste10.AutoSize = True
        RbTcWaste10.Location = New Point(105, 336)
        RbTcWaste10.Name = "RbTcWaste10"
        RbTcWaste10.Size = New Size(52, 22)
        RbTcWaste10.TabIndex = 14
        RbTcWaste10.Text = "10"
        RbTcWaste10.UseVisualStyleBackColor = True
        ' 
        ' RbTcWaste0
        ' 
        RbTcWaste0.AutoSize = True
        RbTcWaste0.Checked = True
        RbTcWaste0.Location = New Point(53, 336)
        RbTcWaste0.Name = "RbTcWaste0"
        RbTcWaste0.Size = New Size(43, 22)
        RbTcWaste0.TabIndex = 12
        RbTcWaste0.TabStop = True
        RbTcWaste0.Text = "0"
        RbTcWaste0.UseVisualStyleBackColor = True
        ' 
        ' LblTcWastePct
        ' 
        LblTcWastePct.AutoSize = True
        LblTcWastePct.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblTcWastePct.ForeColor = Color.Maroon
        LblTcWastePct.Location = New Point(84, 305)
        LblTcWastePct.Name = "LblTcWastePct"
        LblTcWastePct.Size = New Size(164, 25)
        LblTcWastePct.TabIndex = 16
        LblTcWastePct.Text = "Waste Percentage"
        ' 
        ' LblTopCoatWaterMult
        ' 
        LblTopCoatWaterMult.Location = New Point(17, 100)
        LblTopCoatWaterMult.Name = "LblTopCoatWaterMult"
        LblTopCoatWaterMult.Size = New Size(299, 66)
        LblTopCoatWaterMult.TabIndex = 11
        LblTopCoatWaterMult.Tag = "Room Temp. Water {0} oz"
        LblTopCoatWaterMult.Text = "Room Temp. Water"
        ' 
        ' LblTCTotalMixture
        ' 
        LblTCTotalMixture.Location = New Point(17, 223)
        LblTCTotalMixture.Name = "LblTCTotalMixture"
        LblTCTotalMixture.Size = New Size(299, 66)
        LblTCTotalMixture.TabIndex = 10
        LblTCTotalMixture.Tag = "Total Mixture: {0}  Matte: {1:N0} oz ({2:N0} ml){0}  Gloss: {3:N0} oz ({4:N0} ml)"
        LblTCTotalMixture.Text = "Total Mixture"
        ' 
        ' LblPartB
        ' 
        LblPartB.AutoSize = True
        LblPartB.Location = New Point(17, 198)
        LblPartB.Name = "LblPartB"
        LblPartB.Size = New Size(59, 18)
        LblPartB.TabIndex = 8
        LblPartB.Tag = "Part B: Matte: {0} ({1} ml) | Gloss: {2} {{3} ml)"
        LblPartB.Text = "Part B"
        ' 
        ' LblPartA
        ' 
        LblPartA.AutoSize = True
        LblPartA.Location = New Point(17, 173)
        LblPartA.Name = "LblPartA"
        LblPartA.Size = New Size(60, 18)
        LblPartA.TabIndex = 7
        LblPartA.Tag = "Part A: Matte: {0} ({1} ml) | Gloss: {2} {{3} ml)"
        LblPartA.Text = "Part A"
        ' 
        ' LblTcMultiplier
        ' 
        LblTcMultiplier.AutoSize = True
        LblTcMultiplier.Location = New Point(17, 75)
        LblTcMultiplier.Name = "LblTcMultiplier"
        LblTcMultiplier.Size = New Size(120, 18)
        LblTcMultiplier.TabIndex = 5
        LblTcMultiplier.Tag = "Top Coat Multiplier: {0}"
        LblTcMultiplier.Text = "TC Multiplier"
        ' 
        ' TxtTotalArea
        ' 
        TxtTotalArea.Location = New Point(176, 46)
        TxtTotalArea.Name = "TxtTotalArea"
        TxtTotalArea.Size = New Size(93, 26)
        TxtTotalArea.TabIndex = 3
        ' 
        ' LblTotalArea
        ' 
        LblTotalArea.AutoSize = True
        LblTotalArea.Location = New Point(14, 50)
        LblTotalArea.Name = "LblTotalArea"
        LblTotalArea.Size = New Size(154, 18)
        LblTotalArea.TabIndex = 2
        LblTotalArea.Text = "Total Area (SqFt)"
        ' 
        ' Label53
        ' 
        Label53.AutoSize = True
        Label53.Font = New Font("Georgia", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label53.ForeColor = Color.Maroon
        Label53.Location = New Point(17, 7)
        Label53.Name = "Label53"
        Label53.Size = New Size(299, 21)
        Label53.TabIndex = 0
        Label53.Text = "Stone Coat Top Coat Calculator"
        ' 
        ' PnlEpoxyPours
        ' 
        PnlEpoxyPours.BackColor = Color.WhiteSmoke
        PnlEpoxyPours.BorderStyle = BorderStyle.Fixed3D
        PnlEpoxyPours.Controls.Add(LblEpoxyMilliliters)
        PnlEpoxyPours.Controls.Add(Label54)
        PnlEpoxyPours.Controls.Add(TxtEpoxyArea)
        PnlEpoxyPours.Controls.Add(LblEpoxyArea)
        PnlEpoxyPours.Controls.Add(TxtEpoxyDiameter)
        PnlEpoxyPours.Controls.Add(Label52)
        PnlEpoxyPours.Controls.Add(CmbEpoxyCost)
        PnlEpoxyPours.Controls.Add(LblEpoxyCost)
        PnlEpoxyPours.Controls.Add(LblEpoxyLiters)
        PnlEpoxyPours.Controls.Add(RbEpoxyWaste20)
        PnlEpoxyPours.Controls.Add(RbEpoxyWaste15)
        PnlEpoxyPours.Controls.Add(RbEpoxyWaste10)
        PnlEpoxyPours.Controls.Add(RbEpoxyWaste0)
        PnlEpoxyPours.Controls.Add(Label7)
        PnlEpoxyPours.Controls.Add(TxtEpoxyDepth)
        PnlEpoxyPours.Controls.Add(TxtEpoxyWidth)
        PnlEpoxyPours.Controls.Add(TxtEpoxyLength)
        PnlEpoxyPours.Controls.Add(LblEpoxyPints)
        PnlEpoxyPours.Controls.Add(LblEpoxyQuarts)
        PnlEpoxyPours.Controls.Add(LblEpoxyGallons)
        PnlEpoxyPours.Controls.Add(LblEpoxyOunces)
        PnlEpoxyPours.Controls.Add(Label5)
        PnlEpoxyPours.Controls.Add(Label4)
        PnlEpoxyPours.Controls.Add(Label3)
        PnlEpoxyPours.Controls.Add(Label2)
        PnlEpoxyPours.Font = New Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        PnlEpoxyPours.Location = New Point(8, 9)
        PnlEpoxyPours.Name = "PnlEpoxyPours"
        PnlEpoxyPours.Size = New Size(250, 532)
        PnlEpoxyPours.TabIndex = 6
        ' 
        ' LblEpoxyMilliliters
        ' 
        LblEpoxyMilliliters.AutoSize = True
        LblEpoxyMilliliters.Location = New Point(27, 384)
        LblEpoxyMilliliters.Name = "LblEpoxyMilliliters"
        LblEpoxyMilliliters.Size = New Size(152, 21)
        LblEpoxyMilliliters.TabIndex = 20
        LblEpoxyMilliliters.Tag = "Milliliters: {0:N0} ml"
        LblEpoxyMilliliters.Text = "Milliliters required"
        LblEpoxyMilliliters.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label54
        ' 
        Label54.AutoSize = True
        Label54.Location = New Point(147, 157)
        Label54.Name = "Label54"
        Label54.Size = New Size(43, 21)
        Label54.TabIndex = 19
        Label54.Text = "SqFt"
        ' 
        ' TxtEpoxyArea
        ' 
        TxtEpoxyArea.Location = New Point(68, 153)
        TxtEpoxyArea.Name = "TxtEpoxyArea"
        TxtEpoxyArea.Size = New Size(79, 29)
        TxtEpoxyArea.TabIndex = 18
        ' 
        ' LblEpoxyArea
        ' 
        LblEpoxyArea.AutoSize = True
        LblEpoxyArea.Location = New Point(15, 157)
        LblEpoxyArea.Name = "LblEpoxyArea"
        LblEpoxyArea.Size = New Size(53, 21)
        LblEpoxyArea.TabIndex = 17
        LblEpoxyArea.Text = "Area: "
        ' 
        ' TxtEpoxyDiameter
        ' 
        TxtEpoxyDiameter.Location = New Point(102, 83)
        TxtEpoxyDiameter.Name = "TxtEpoxyDiameter"
        TxtEpoxyDiameter.Size = New Size(47, 29)
        TxtEpoxyDiameter.TabIndex = 16
        TxtEpoxyDiameter.Tag = "2"
        ' 
        ' Label52
        ' 
        Label52.AutoSize = True
        Label52.Location = New Point(22, 87)
        Label52.Name = "Label52"
        Label52.Size = New Size(81, 21)
        Label52.TabIndex = 15
        Label52.Text = "Diameter"
        ' 
        ' CmbEpoxyCost
        ' 
        CmbEpoxyCost.Font = New Font("Segoe UI", 7F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        CmbEpoxyCost.FormattingEnabled = True
        CmbEpoxyCost.Location = New Point(10, 195)
        CmbEpoxyCost.Name = "CmbEpoxyCost"
        CmbEpoxyCost.Size = New Size(226, 27)
        CmbEpoxyCost.Sorted = True
        CmbEpoxyCost.TabIndex = 14
        ' 
        ' LblEpoxyCost
        ' 
        LblEpoxyCost.AutoSize = True
        LblEpoxyCost.Location = New Point(55, 227)
        LblEpoxyCost.Name = "LblEpoxyCost"
        LblEpoxyCost.Size = New Size(94, 21)
        LblEpoxyCost.TabIndex = 13
        LblEpoxyCost.Tag = "Cost: {0}"
        LblEpoxyCost.Text = "Epoxy Cost"
        LblEpoxyCost.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblEpoxyLiters
        ' 
        LblEpoxyLiters.AutoSize = True
        LblEpoxyLiters.Location = New Point(29, 416)
        LblEpoxyLiters.Name = "LblEpoxyLiters"
        LblEpoxyLiters.Size = New Size(120, 21)
        LblEpoxyLiters.TabIndex = 12
        LblEpoxyLiters.Tag = "Liters: {0:N2} L"
        LblEpoxyLiters.Text = "Liters required"
        LblEpoxyLiters.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' RbEpoxyWaste20
        ' 
        RbEpoxyWaste20.AutoSize = True
        RbEpoxyWaste20.Location = New Point(184, 489)
        RbEpoxyWaste20.Name = "RbEpoxyWaste20"
        RbEpoxyWaste20.Size = New Size(53, 25)
        RbEpoxyWaste20.TabIndex = 5
        RbEpoxyWaste20.Text = "20"
        RbEpoxyWaste20.UseVisualStyleBackColor = True
        ' 
        ' RbEpoxyWaste15
        ' 
        RbEpoxyWaste15.AutoSize = True
        RbEpoxyWaste15.Location = New Point(123, 489)
        RbEpoxyWaste15.Name = "RbEpoxyWaste15"
        RbEpoxyWaste15.Size = New Size(53, 25)
        RbEpoxyWaste15.TabIndex = 3
        RbEpoxyWaste15.Text = "15"
        RbEpoxyWaste15.UseVisualStyleBackColor = True
        ' 
        ' RbEpoxyWaste10
        ' 
        RbEpoxyWaste10.AutoSize = True
        RbEpoxyWaste10.Location = New Point(62, 489)
        RbEpoxyWaste10.Name = "RbEpoxyWaste10"
        RbEpoxyWaste10.Size = New Size(53, 25)
        RbEpoxyWaste10.TabIndex = 4
        RbEpoxyWaste10.Text = "10"
        RbEpoxyWaste10.UseVisualStyleBackColor = True
        ' 
        ' RbEpoxyWaste0
        ' 
        RbEpoxyWaste0.AutoSize = True
        RbEpoxyWaste0.Checked = True
        RbEpoxyWaste0.Location = New Point(10, 489)
        RbEpoxyWaste0.Name = "RbEpoxyWaste0"
        RbEpoxyWaste0.Size = New Size(44, 25)
        RbEpoxyWaste0.TabIndex = 2
        RbEpoxyWaste0.TabStop = True
        RbEpoxyWaste0.Text = "0"
        RbEpoxyWaste0.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label7.ForeColor = Color.Maroon
        Label7.Location = New Point(41, 458)
        Label7.Name = "Label7"
        Label7.Size = New Size(164, 25)
        Label7.TabIndex = 11
        Label7.Text = "Waste Percentage"
        ' 
        ' TxtEpoxyDepth
        ' 
        TxtEpoxyDepth.Location = New Point(102, 116)
        TxtEpoxyDepth.Name = "TxtEpoxyDepth"
        TxtEpoxyDepth.Size = New Size(69, 29)
        TxtEpoxyDepth.TabIndex = 2
        TxtEpoxyDepth.Tag = "3"
        ' 
        ' TxtEpoxyWidth
        ' 
        TxtEpoxyWidth.Location = New Point(189, 51)
        TxtEpoxyWidth.Name = "TxtEpoxyWidth"
        TxtEpoxyWidth.Size = New Size(47, 29)
        TxtEpoxyWidth.TabIndex = 1
        TxtEpoxyWidth.Tag = "1"
        ' 
        ' TxtEpoxyLength
        ' 
        TxtEpoxyLength.Location = New Point(73, 51)
        TxtEpoxyLength.Name = "TxtEpoxyLength"
        TxtEpoxyLength.Size = New Size(47, 29)
        TxtEpoxyLength.TabIndex = 0
        TxtEpoxyLength.Tag = "0"
        ' 
        ' LblEpoxyPints
        ' 
        LblEpoxyPints.AutoSize = True
        LblEpoxyPints.Location = New Point(32, 352)
        LblEpoxyPints.Name = "LblEpoxyPints"
        LblEpoxyPints.Size = New Size(117, 21)
        LblEpoxyPints.TabIndex = 7
        LblEpoxyPints.Tag = "Pints: {0:N2} pts"
        LblEpoxyPints.Text = "Pints required"
        LblEpoxyPints.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblEpoxyQuarts
        ' 
        LblEpoxyQuarts.AutoSize = True
        LblEpoxyQuarts.Location = New Point(20, 320)
        LblEpoxyQuarts.Name = "LblEpoxyQuarts"
        LblEpoxyQuarts.Size = New Size(129, 21)
        LblEpoxyQuarts.TabIndex = 6
        LblEpoxyQuarts.Tag = "Quarts: {0:N2} qts"
        LblEpoxyQuarts.Text = "Quarts required"
        LblEpoxyQuarts.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblEpoxyGallons
        ' 
        LblEpoxyGallons.AutoSize = True
        LblEpoxyGallons.Location = New Point(13, 288)
        LblEpoxyGallons.Name = "LblEpoxyGallons"
        LblEpoxyGallons.Size = New Size(136, 21)
        LblEpoxyGallons.TabIndex = 5
        LblEpoxyGallons.Tag = "Gallons: {0:N2} gals"
        LblEpoxyGallons.Text = "Gallons required"
        LblEpoxyGallons.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' LblEpoxyOunces
        ' 
        LblEpoxyOunces.AutoSize = True
        LblEpoxyOunces.Location = New Point(14, 256)
        LblEpoxyOunces.Name = "LblEpoxyOunces"
        LblEpoxyOunces.Size = New Size(135, 21)
        LblEpoxyOunces.TabIndex = 4
        LblEpoxyOunces.Tag = "Ounces: {0:N2} oz"
        LblEpoxyOunces.Text = "Ounces required"
        LblEpoxyOunces.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(37, 119)
        Label5.Name = "Label5"
        Label5.Size = New Size(57, 21)
        Label5.TabIndex = 3
        Label5.Text = "Depth"
        Label5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(130, 55)
        Label4.Name = "Label4"
        Label4.Size = New Size(57, 21)
        Label4.TabIndex = 2
        Label4.Text = "Width"
        Label4.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(10, 55)
        Label3.Name = "Label3"
        Label3.Size = New Size(63, 21)
        Label3.TabIndex = 1
        Label3.Text = "Length"
        Label3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Georgia", 14F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = Color.Maroon
        Label2.Location = New Point(26, 11)
        Label2.Name = "Label2"
        Label2.Size = New Size(195, 32)
        Label2.TabIndex = 0
        Label2.Text = "Epoxy Pours"
        ' 
        ' TpConversions
        ' 
        TpConversions.BackColor = Color.Gainsboro
        TpConversions.BorderStyle = BorderStyle.Fixed3D
        TpConversions.Controls.Add(Panel4)
        TpConversions.Controls.Add(Panel2)
        TpConversions.Location = New Point(4, 4)
        TpConversions.Name = "TpConversions"
        TpConversions.Padding = New Padding(3)
        TpConversions.Size = New Size(1128, 811)
        TpConversions.TabIndex = 1
        TpConversions.Text = "Conversions"
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
        Panel4.Location = New Point(655, 175)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(300, 456)
        Panel4.TabIndex = 5
        ' 
        ' GroupBox4
        ' 
        GroupBox4.BackColor = Color.Silver
        GroupBox4.Controls.Add(LblFraction2Decimal)
        GroupBox4.Controls.Add(TxtFraction2Decimal)
        GroupBox4.Controls.Add(Label16)
        GroupBox4.Font = New Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        GroupBox4.Location = New Point(20, 346)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(257, 99)
        GroupBox4.TabIndex = 4
        GroupBox4.TabStop = False
        GroupBox4.Tag = "Decimal: {0}"
        GroupBox4.Text = "Fraction to Decimal"
        ' 
        ' LblFraction2Decimal
        ' 
        LblFraction2Decimal.AutoSize = True
        LblFraction2Decimal.Location = New Point(20, 57)
        LblFraction2Decimal.Name = "LblFraction2Decimal"
        LblFraction2Decimal.Size = New Size(73, 21)
        LblFraction2Decimal.TabIndex = 2
        LblFraction2Decimal.Tag = "Decimal: {0:N3} "
        LblFraction2Decimal.Text = "Decimal"
        ' 
        ' TxtFraction2Decimal
        ' 
        TxtFraction2Decimal.Location = New Point(98, 22)
        TxtFraction2Decimal.Name = "TxtFraction2Decimal"
        TxtFraction2Decimal.Size = New Size(81, 29)
        TxtFraction2Decimal.TabIndex = 1
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.Location = New Point(20, 26)
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
        GroupBox3.Font = New Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        GroupBox3.Location = New Point(20, 242)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(257, 99)
        GroupBox3.TabIndex = 3
        GroupBox3.TabStop = False
        GroupBox3.Text = "Decimal to Fraction"
        ' 
        ' LblDecimal2Fraction
        ' 
        LblDecimal2Fraction.AutoSize = True
        LblDecimal2Fraction.Location = New Point(20, 57)
        LblDecimal2Fraction.Name = "LblDecimal2Fraction"
        LblDecimal2Fraction.Size = New Size(72, 21)
        LblDecimal2Fraction.TabIndex = 2
        LblDecimal2Fraction.Tag = "Fraction: {0}"
        LblDecimal2Fraction.Text = "Fraction"
        ' 
        ' TxtDecimal2Fraction
        ' 
        TxtDecimal2Fraction.Location = New Point(98, 22)
        TxtDecimal2Fraction.Name = "TxtDecimal2Fraction"
        TxtDecimal2Fraction.Size = New Size(81, 29)
        TxtDecimal2Fraction.TabIndex = 1
        ' 
        ' Label19
        ' 
        Label19.AutoSize = True
        Label19.Location = New Point(20, 26)
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
        GroupBox2.Font = New Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        GroupBox2.Location = New Point(20, 138)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(257, 99)
        GroupBox2.TabIndex = 2
        GroupBox2.TabStop = False
        GroupBox2.Text = "MM to Inches"
        ' 
        ' LblMM2Inches
        ' 
        LblMM2Inches.AutoSize = True
        LblMM2Inches.Location = New Point(20, 57)
        LblMM2Inches.Name = "LblMM2Inches"
        LblMM2Inches.Size = New Size(59, 21)
        LblMM2Inches.TabIndex = 2
        LblMM2Inches.Tag = "Inches: {0:N2} in"
        LblMM2Inches.Text = "Inches"
        ' 
        ' TxtMm2Inches
        ' 
        TxtMm2Inches.Location = New Point(90, 22)
        TxtMm2Inches.Name = "TxtMm2Inches"
        TxtMm2Inches.Size = New Size(81, 29)
        TxtMm2Inches.TabIndex = 1
        ' 
        ' Label17
        ' 
        Label17.AutoSize = True
        Label17.Location = New Point(20, 26)
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
        GroupBox1.Font = New Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        GroupBox1.Location = New Point(20, 35)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(257, 99)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        GroupBox1.Text = "Inches to MM"
        ' 
        ' LblInches2MM
        ' 
        LblInches2MM.AutoSize = True
        LblInches2MM.Location = New Point(20, 57)
        LblInches2MM.Name = "LblInches2MM"
        LblInches2MM.Size = New Size(97, 21)
        LblInches2MM.TabIndex = 2
        LblInches2MM.Tag = "Millimeters: {0:N2} mm"
        LblInches2MM.Text = "Millimeters"
        ' 
        ' TxtInches2Mm
        ' 
        TxtInches2Mm.Location = New Point(90, 22)
        TxtInches2Mm.Name = "TxtInches2Mm"
        TxtInches2Mm.Size = New Size(81, 29)
        TxtInches2Mm.TabIndex = 1
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.Location = New Point(20, 26)
        Label14.Name = "Label14"
        Label14.Size = New Size(59, 21)
        Label14.TabIndex = 0
        Label14.Text = "Inches"
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.Font = New Font("Georgia", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label13.ForeColor = Color.Maroon
        Label13.Location = New Point(87, 9)
        Label13.Name = "Label13"
        Label13.Size = New Size(122, 24)
        Label13.TabIndex = 0
        Label13.Text = "Converters"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.WhiteSmoke
        Panel2.BorderStyle = BorderStyle.Fixed3D
        Panel2.Controls.Add(RtbFraction2Mm)
        Panel2.Controls.Add(RtbFraction2Decimal)
        Panel2.Controls.Add(Label10)
        Panel2.Controls.Add(Label8)
        Panel2.Location = New Point(175, 211)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(465, 385)
        Panel2.TabIndex = 4
        ' 
        ' RtbFraction2Mm
        ' 
        RtbFraction2Mm.BackColor = Color.White
        RtbFraction2Mm.DetectUrls = False
        RtbFraction2Mm.Location = New Point(239, 47)
        RtbFraction2Mm.Name = "RtbFraction2Mm"
        RtbFraction2Mm.ReadOnly = True
        RtbFraction2Mm.ScrollBars = RichTextBoxScrollBars.Vertical
        RtbFraction2Mm.ShowSelectionMargin = True
        RtbFraction2Mm.Size = New Size(209, 321)
        RtbFraction2Mm.TabIndex = 3
        RtbFraction2Mm.TabStop = False
        RtbFraction2Mm.Text = ""
        ' 
        ' RtbFraction2Decimal
        ' 
        RtbFraction2Decimal.BackColor = Color.White
        RtbFraction2Decimal.DetectUrls = False
        RtbFraction2Decimal.Location = New Point(12, 47)
        RtbFraction2Decimal.Name = "RtbFraction2Decimal"
        RtbFraction2Decimal.ReadOnly = True
        RtbFraction2Decimal.ScrollBars = RichTextBoxScrollBars.Vertical
        RtbFraction2Decimal.ShowSelectionMargin = True
        RtbFraction2Decimal.Size = New Size(209, 321)
        RtbFraction2Decimal.TabIndex = 2
        RtbFraction2Decimal.TabStop = False
        RtbFraction2Decimal.Text = ""
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Segoe UI", 8F, FontStyle.Bold)
        Label10.ForeColor = Color.Maroon
        Label10.Location = New Point(286, 18)
        Label10.Name = "Label10"
        Label10.Size = New Size(126, 21)
        Label10.TabIndex = 1
        Label10.Text = "Fraction to mm"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 8F, FontStyle.Bold)
        Label8.ForeColor = Color.Maroon
        Label8.Location = New Point(43, 18)
        Label8.Name = "Label8"
        Label8.Size = New Size(159, 21)
        Label8.TabIndex = 0
        Label8.Text = "Fraction to Decimal"
        ' 
        ' TpCalculators
        ' 
        TpCalculators.BackColor = Color.Silver
        TpCalculators.BorderStyle = BorderStyle.Fixed3D
        TpCalculators.Controls.Add(Panel5)
        TpCalculators.Controls.Add(Panel3)
        TpCalculators.Location = New Point(4, 4)
        TpCalculators.Name = "TpCalculators"
        TpCalculators.Size = New Size(1128, 811)
        TpCalculators.TabIndex = 2
        TpCalculators.Text = "Calculators"
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
        Panel5.Font = New Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Panel5.Location = New Point(185, 249)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(356, 355)
        Panel5.TabIndex = 6
        ' 
        ' LblTippingForce
        ' 
        LblTippingForce.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblTippingForce.ForeColor = Color.DodgerBlue
        LblTippingForce.Location = New Point(15, 272)
        LblTippingForce.Name = "LblTippingForce"
        LblTippingForce.Size = New Size(323, 33)
        LblTippingForce.TabIndex = 12
        LblTippingForce.Tag = "Tipping force: {0:N2} lbs / {1:N2} kgf"
        LblTippingForce.Text = "Tipping force required"
        LblTippingForce.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TxtTtTableBaseWeight
        ' 
        TxtTtTableBaseWeight.Location = New Point(106, 230)
        TxtTtTableBaseWeight.Name = "TxtTtTableBaseWeight"
        TxtTtTableBaseWeight.Size = New Size(75, 29)
        TxtTtTableBaseWeight.TabIndex = 11
        ' 
        ' TxtTtTableBaselength
        ' 
        TxtTtTableBaselength.Location = New Point(106, 191)
        TxtTtTableBaselength.Name = "TxtTtTableBaselength"
        TxtTtTableBaselength.Size = New Size(75, 29)
        TxtTtTableBaselength.TabIndex = 10
        ' 
        ' TxtTtTableTopWeight
        ' 
        TxtTtTableTopWeight.Location = New Point(106, 121)
        TxtTtTableTopWeight.Name = "TxtTtTableTopWeight"
        TxtTtTableTopWeight.Size = New Size(75, 29)
        TxtTtTableTopWeight.TabIndex = 9
        ' 
        ' TxtTtTableTopLength
        ' 
        TxtTtTableTopLength.Location = New Point(106, 89)
        TxtTtTableTopLength.Name = "TxtTtTableTopLength"
        TxtTtTableTopLength.Size = New Size(75, 29)
        TxtTtTableTopLength.TabIndex = 8
        ' 
        ' Label25
        ' 
        Label25.AutoSize = True
        Label25.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label25.ForeColor = Color.OrangeRed
        Label25.Location = New Point(86, 162)
        Label25.Name = "Label25"
        Label25.Size = New Size(102, 25)
        Label25.TabIndex = 7
        Label25.Text = "Table Base"
        ' 
        ' Label24
        ' 
        Label24.AutoSize = True
        Label24.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        Label24.ForeColor = Color.OrangeRed
        Label24.Location = New Point(97, 62)
        Label24.Name = "Label24"
        Label24.Size = New Size(93, 25)
        Label24.TabIndex = 6
        Label24.Text = "Table Top"
        ' 
        ' Label22
        ' 
        Label22.AutoSize = True
        Label22.Location = New Point(31, 233)
        Label22.Name = "Label22"
        Label22.Size = New Size(66, 21)
        Label22.TabIndex = 5
        Label22.Text = "Weight"
        Label22.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label23
        ' 
        Label23.AutoSize = True
        Label23.Location = New Point(34, 195)
        Label23.Name = "Label23"
        Label23.Size = New Size(63, 21)
        Label23.TabIndex = 4
        Label23.Text = "Length"
        Label23.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.Location = New Point(31, 124)
        Label21.Name = "Label21"
        Label21.Size = New Size(66, 21)
        Label21.TabIndex = 3
        Label21.Text = "Weight"
        Label21.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label20
        ' 
        Label20.AutoSize = True
        Label20.Location = New Point(34, 93)
        Label20.Name = "Label20"
        Label20.Size = New Size(63, 21)
        Label20.TabIndex = 2
        Label20.Text = "Length"
        Label20.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label18
        ' 
        Label18.AutoSize = True
        Label18.Location = New Point(29, 313)
        Label18.Name = "Label18"
        Label18.Size = New Size(294, 21)
        Label18.TabIndex = 1
        Label18.Text = "Calculate force required to tip a table"
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.Font = New Font("Georgia", 16F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label15.ForeColor = Color.Maroon
        Label15.Location = New Point(41, 20)
        Label15.Name = "Label15"
        Label15.Size = New Size(271, 38)
        Label15.TabIndex = 0
        Label15.Text = "Table Tip Force"
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.WhiteSmoke
        Panel3.BorderStyle = BorderStyle.Fixed3D
        Panel3.Controls.Add(Label51)
        Panel3.Controls.Add(LblPolygonPieceAngle)
        Panel3.Controls.Add(LblPolygonSideAngle)
        Panel3.Controls.Add(TxtPolygonSides)
        Panel3.Controls.Add(Label11)
        Panel3.Controls.Add(PbPolygon)
        Panel3.Font = New Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Panel3.Location = New Point(553, 207)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(302, 397)
        Panel3.TabIndex = 5
        ' 
        ' Label51
        ' 
        Label51.AutoSize = True
        Label51.Font = New Font("Georgia", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label51.ForeColor = Color.Maroon
        Label51.Location = New Point(87, 9)
        Label51.Name = "Label51"
        Label51.Size = New Size(124, 29)
        Label51.TabIndex = 5
        Label51.Text = "Polygons"
        ' 
        ' LblPolygonPieceAngle
        ' 
        LblPolygonPieceAngle.AutoSize = True
        LblPolygonPieceAngle.Location = New Point(24, 113)
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
        LblPolygonSideAngle.Location = New Point(24, 84)
        LblPolygonSideAngle.Name = "LblPolygonSideAngle"
        LblPolygonSideAngle.Size = New Size(130, 21)
        LblPolygonSideAngle.TabIndex = 3
        LblPolygonSideAngle.Tag = "Angle each side: {0:N2}°"
        LblPolygonSideAngle.Text = "Angle each side"
        LblPolygonSideAngle.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TxtPolygonSides
        ' 
        TxtPolygonSides.Location = New Point(146, 45)
        TxtPolygonSides.MaxLength = 5
        TxtPolygonSides.Name = "TxtPolygonSides"
        TxtPolygonSides.Size = New Size(49, 29)
        TxtPolygonSides.TabIndex = 0
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(24, 49)
        Label11.Name = "Label11"
        Label11.Size = New Size(115, 21)
        Label11.TabIndex = 1
        Label11.Text = "Number sides"
        ' 
        ' PbPolygon
        ' 
        PbPolygon.BackColor = Color.LightGray
        PbPolygon.BorderStyle = BorderStyle.Fixed3D
        PbPolygon.Location = New Point(21, 151)
        PbPolygon.Name = "PbPolygon"
        PbPolygon.Size = New Size(257, 236)
        PbPolygon.TabIndex = 0
        PbPolygon.TabStop = False
        ' 
        ' TpDrawings
        ' 
        TpDrawings.Controls.Add(PbOutputDrawing)
        TpDrawings.Location = New Point(4, 27)
        TpDrawings.Name = "TpDrawings"
        TpDrawings.Size = New Size(1170, 823)
        TpDrawings.TabIndex = 5
        TpDrawings.Text = "Drawings"
        TpDrawings.UseVisualStyleBackColor = True
        ' 
        ' PbOutputDrawing
        ' 
        PbOutputDrawing.BackColor = Color.Silver
        PbOutputDrawing.BorderStyle = BorderStyle.Fixed3D
        PbOutputDrawing.Dock = DockStyle.Fill
        PbOutputDrawing.Location = New Point(0, 0)
        PbOutputDrawing.Name = "PbOutputDrawing"
        PbOutputDrawing.Size = New Size(1170, 823)
        PbOutputDrawing.TabIndex = 0
        PbOutputDrawing.TabStop = False
        ' 
        ' TpJoinery
        ' 
        TpJoinery.BackColor = Color.Gainsboro
        TpJoinery.BorderStyle = BorderStyle.Fixed3D
        TpJoinery.Controls.Add(ScJoinery)
        TpJoinery.Location = New Point(4, 27)
        TpJoinery.Name = "TpJoinery"
        TpJoinery.Size = New Size(1170, 823)
        TpJoinery.TabIndex = 8
        TpJoinery.Text = "Joinery"
        ' 
        ' ScJoinery
        ' 
        ScJoinery.BorderStyle = BorderStyle.Fixed3D
        ScJoinery.Dock = DockStyle.Fill
        ScJoinery.Location = New Point(0, 0)
        ScJoinery.Name = "ScJoinery"
        ' 
        ' ScJoinery.Panel1
        ' 
        ScJoinery.Panel1.AutoScroll = True
        ScJoinery.Panel1.AutoScrollMargin = New Size(3, 3)
        ScJoinery.Panel1.AutoScrollMinSize = New Size(4, 4)
        ScJoinery.Panel1.Controls.Add(GbxMortiseTenonInput)
        ScJoinery.Panel1.Controls.Add(GbxDado)
        ScJoinery.Panel1.Controls.Add(GbxDovetails)
        ScJoinery.Panel1.Controls.Add(GbxBoxJoint)
        ScJoinery.Panel1.Controls.Add(BtnCalculateJoinery)
        ' 
        ' ScJoinery.Panel2
        ' 
        ScJoinery.Panel2.AutoScroll = True
        ScJoinery.Panel2.AutoScrollMargin = New Size(3, 3)
        ScJoinery.Panel2.AutoScrollMinSize = New Size(4, 4)
        ScJoinery.Panel2.Controls.Add(GbxMortiseTenonResults)
        ScJoinery.Panel2.Controls.Add(PbJointDiagram)
        ScJoinery.Panel2.Controls.Add(GbxDovetailResults)
        ScJoinery.Panel2.Controls.Add(GbxDadoResults)
        ScJoinery.Panel2.Controls.Add(GbxBoxJointResults)
        ScJoinery.Size = New Size(1166, 819)
        ScJoinery.SplitterDistance = 485
        ScJoinery.SplitterWidth = 8
        ScJoinery.TabIndex = 11
        ' 
        ' GbxMortiseTenonInput
        ' 
        GbxMortiseTenonInput.BackColor = Color.DarkGray
        GbxMortiseTenonInput.Controls.Add(Label59)
        GbxMortiseTenonInput.Controls.Add(TxtJointStockThickness)
        GbxMortiseTenonInput.Controls.Add(Label57)
        GbxMortiseTenonInput.Controls.Add(TxtJointStockWidth)
        GbxMortiseTenonInput.Controls.Add(Label58)
        GbxMortiseTenonInput.Controls.Add(RbTenonStandard)
        GbxMortiseTenonInput.Controls.Add(RbTenonHaunched)
        GbxMortiseTenonInput.Controls.Add(RbTenonThrough)
        GbxMortiseTenonInput.Font = New Font("Georgia", 10F, FontStyle.Bold)
        GbxMortiseTenonInput.Location = New Point(45, 19)
        GbxMortiseTenonInput.Name = "GbxMortiseTenonInput"
        GbxMortiseTenonInput.Size = New Size(316, 236)
        GbxMortiseTenonInput.TabIndex = 0
        GbxMortiseTenonInput.TabStop = False
        GbxMortiseTenonInput.Text = "Mortise && Tenon Input"
        ' 
        ' Label59
        ' 
        Label59.AutoSize = True
        Label59.Font = New Font("Georgia", 9F, FontStyle.Bold)
        Label59.Location = New Point(15, 30)
        Label59.Name = "Label59"
        Label59.Size = New Size(166, 21)
        Label59.TabIndex = 0
        Label59.Text = "Stock Thickness:"
        ' 
        ' TxtJointStockThickness
        ' 
        TxtJointStockThickness.Location = New Point(184, 25)
        TxtJointStockThickness.MaxLength = 7
        TxtJointStockThickness.Name = "TxtJointStockThickness"
        TxtJointStockThickness.Size = New Size(100, 30)
        TxtJointStockThickness.TabIndex = 0
        TxtJointStockThickness.Text = "0.75"
        TxtJointStockThickness.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label57
        ' 
        Label57.AutoSize = True
        Label57.Font = New Font("Georgia", 9F, FontStyle.Bold)
        Label57.Location = New Point(50, 65)
        Label57.Name = "Label57"
        Label57.Size = New Size(131, 21)
        Label57.TabIndex = 2
        Label57.Text = "Stock Width:"
        ' 
        ' TxtJointStockWidth
        ' 
        TxtJointStockWidth.Location = New Point(184, 62)
        TxtJointStockWidth.MaxLength = 7
        TxtJointStockWidth.Name = "TxtJointStockWidth"
        TxtJointStockWidth.Size = New Size(100, 30)
        TxtJointStockWidth.TabIndex = 1
        TxtJointStockWidth.Text = "3.5"
        TxtJointStockWidth.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label58
        ' 
        Label58.AutoSize = True
        Label58.Font = New Font("Georgia", 9F, FontStyle.Bold)
        Label58.Location = New Point(15, 105)
        Label58.Name = "Label58"
        Label58.Size = New Size(125, 21)
        Label58.TabIndex = 2
        Label58.Text = "Tenon Type:"
        ' 
        ' RbTenonStandard
        ' 
        RbTenonStandard.AutoSize = True
        RbTenonStandard.Checked = True
        RbTenonStandard.Font = New Font("Georgia", 9F, FontStyle.Bold)
        RbTenonStandard.Location = New Point(30, 130)
        RbTenonStandard.Name = "RbTenonStandard"
        RbTenonStandard.Size = New Size(122, 25)
        RbTenonStandard.TabIndex = 2
        RbTenonStandard.TabStop = True
        RbTenonStandard.Text = "Standard"
        RbTenonStandard.UseVisualStyleBackColor = True
        ' 
        ' RbTenonHaunched
        ' 
        RbTenonHaunched.AutoSize = True
        RbTenonHaunched.Font = New Font("Georgia", 9F, FontStyle.Bold)
        RbTenonHaunched.Location = New Point(30, 160)
        RbTenonHaunched.Name = "RbTenonHaunched"
        RbTenonHaunched.Size = New Size(133, 25)
        RbTenonHaunched.TabIndex = 3
        RbTenonHaunched.Text = "Haunched"
        RbTenonHaunched.UseVisualStyleBackColor = True
        ' 
        ' RbTenonThrough
        ' 
        RbTenonThrough.AutoSize = True
        RbTenonThrough.Font = New Font("Georgia", 9F, FontStyle.Bold)
        RbTenonThrough.Location = New Point(30, 190)
        RbTenonThrough.Name = "RbTenonThrough"
        RbTenonThrough.Size = New Size(116, 25)
        RbTenonThrough.TabIndex = 4
        RbTenonThrough.Text = "Through"
        RbTenonThrough.UseVisualStyleBackColor = True
        ' 
        ' GbxDado
        ' 
        GbxDado.BackColor = Color.Silver
        GbxDado.Controls.Add(TxtDadoStockThickness)
        GbxDado.Controls.Add(LblDadoStockThickness)
        GbxDado.Controls.Add(TxtDadoShelfThickness)
        GbxDado.Controls.Add(LblDadoShelfThickness)
        GbxDado.Font = New Font("Georgia", 9F, FontStyle.Bold)
        GbxDado.Location = New Point(45, 626)
        GbxDado.Name = "GbxDado"
        GbxDado.Size = New Size(316, 106)
        GbxDado.TabIndex = 0
        GbxDado.TabStop = False
        GbxDado.Text = "Dado"
        ' 
        ' TxtDadoStockThickness
        ' 
        TxtDadoStockThickness.Location = New Point(194, 57)
        TxtDadoStockThickness.Name = "TxtDadoStockThickness"
        TxtDadoStockThickness.Size = New Size(100, 28)
        TxtDadoStockThickness.TabIndex = 1
        ' 
        ' LblDadoStockThickness
        ' 
        LblDadoStockThickness.AutoSize = True
        LblDadoStockThickness.Location = New Point(23, 61)
        LblDadoStockThickness.Name = "LblDadoStockThickness"
        LblDadoStockThickness.Size = New Size(168, 21)
        LblDadoStockThickness.TabIndex = 0
        LblDadoStockThickness.Text = "Shelf Thickness: "
        ' 
        ' TxtDadoShelfThickness
        ' 
        TxtDadoShelfThickness.Location = New Point(194, 22)
        TxtDadoShelfThickness.Name = "TxtDadoShelfThickness"
        TxtDadoShelfThickness.Size = New Size(100, 28)
        TxtDadoShelfThickness.TabIndex = 0
        ' 
        ' LblDadoShelfThickness
        ' 
        LblDadoShelfThickness.AutoSize = True
        LblDadoShelfThickness.Location = New Point(23, 26)
        LblDadoShelfThickness.Name = "LblDadoShelfThickness"
        LblDadoShelfThickness.Size = New Size(171, 21)
        LblDadoShelfThickness.TabIndex = 0
        LblDadoShelfThickness.Text = "Stock Thickness: "
        ' 
        ' GbxDovetails
        ' 
        GbxDovetails.BackColor = Color.Silver
        GbxDovetails.Controls.Add(TxtDovetailSpacing)
        GbxDovetails.Controls.Add(ChkDovetailHardwood)
        GbxDovetails.Controls.Add(LblDovetailThickness)
        GbxDovetails.Controls.Add(TxtDovetailWidth)
        GbxDovetails.Controls.Add(LblDovetailSpacing)
        GbxDovetails.Controls.Add(TxtDovetailThickness)
        GbxDovetails.Controls.Add(LblDovetailWidth)
        GbxDovetails.Font = New Font("Georgia", 9F, FontStyle.Bold)
        GbxDovetails.Location = New Point(45, 273)
        GbxDovetails.Name = "GbxDovetails"
        GbxDovetails.Size = New Size(316, 217)
        GbxDovetails.TabIndex = 2
        GbxDovetails.TabStop = False
        GbxDovetails.Text = "Dovetails"
        ' 
        ' TxtDovetailSpacing
        ' 
        TxtDovetailSpacing.Location = New Point(185, 104)
        TxtDovetailSpacing.Name = "TxtDovetailSpacing"
        TxtDovetailSpacing.Size = New Size(106, 28)
        TxtDovetailSpacing.TabIndex = 2
        ' 
        ' ChkDovetailHardwood
        ' 
        ChkDovetailHardwood.Location = New Point(13, 144)
        ChkDovetailHardwood.Name = "ChkDovetailHardwood"
        ChkDovetailHardwood.Size = New Size(248, 57)
        ChkDovetailHardwood.TabIndex = 3
        ChkDovetailHardwood.Text = "Hardwood (1:8 angle)" & vbCrLf & "(Softwood 1:7 if off)"
        ' 
        ' LblDovetailThickness
        ' 
        LblDovetailThickness.AutoSize = True
        LblDovetailThickness.Location = New Point(13, 38)
        LblDovetailThickness.Name = "LblDovetailThickness"
        LblDovetailThickness.Size = New Size(172, 21)
        LblDovetailThickness.TabIndex = 0
        LblDovetailThickness.Text = "Board Thickness:"
        ' 
        ' TxtDovetailWidth
        ' 
        TxtDovetailWidth.Location = New Point(185, 68)
        TxtDovetailWidth.Name = "TxtDovetailWidth"
        TxtDovetailWidth.Size = New Size(106, 28)
        TxtDovetailWidth.TabIndex = 1
        ' 
        ' LblDovetailSpacing
        ' 
        LblDovetailSpacing.AutoSize = True
        LblDovetailSpacing.Location = New Point(57, 108)
        LblDovetailSpacing.Name = "LblDovetailSpacing"
        LblDovetailSpacing.Size = New Size(128, 21)
        LblDovetailSpacing.TabIndex = 0
        LblDovetailSpacing.Text = "Pin Spacing:"
        ' 
        ' TxtDovetailThickness
        ' 
        TxtDovetailThickness.Location = New Point(185, 34)
        TxtDovetailThickness.Name = "TxtDovetailThickness"
        TxtDovetailThickness.Size = New Size(106, 28)
        TxtDovetailThickness.TabIndex = 0
        TxtDovetailThickness.Text = "0.75"
        ' 
        ' LblDovetailWidth
        ' 
        LblDovetailWidth.AutoSize = True
        LblDovetailWidth.Location = New Point(43, 72)
        LblDovetailWidth.Name = "LblDovetailWidth"
        LblDovetailWidth.Size = New Size(142, 21)
        LblDovetailWidth.TabIndex = 0
        LblDovetailWidth.Text = "Board Width: "
        ' 
        ' GbxBoxJoint
        ' 
        GbxBoxJoint.BackColor = Color.Silver
        GbxBoxJoint.Controls.Add(TxtBoxJointThickness)
        GbxBoxJoint.Controls.Add(LblBoxJointThickness)
        GbxBoxJoint.Controls.Add(TxtBoxJointWidth)
        GbxBoxJoint.Controls.Add(LblBoxJointWidth)
        GbxBoxJoint.Font = New Font("Georgia", 9F, FontStyle.Bold)
        GbxBoxJoint.Location = New Point(45, 508)
        GbxBoxJoint.Name = "GbxBoxJoint"
        GbxBoxJoint.Size = New Size(316, 100)
        GbxBoxJoint.TabIndex = 0
        GbxBoxJoint.TabStop = False
        GbxBoxJoint.Text = "Box Joint"
        ' 
        ' TxtBoxJointThickness
        ' 
        TxtBoxJointThickness.Location = New Point(182, 64)
        TxtBoxJointThickness.Name = "TxtBoxJointThickness"
        TxtBoxJointThickness.Size = New Size(110, 28)
        TxtBoxJointThickness.TabIndex = 1
        ' 
        ' LblBoxJointThickness
        ' 
        LblBoxJointThickness.AutoSize = True
        LblBoxJointThickness.Location = New Point(40, 68)
        LblBoxJointThickness.Name = "LblBoxJointThickness"
        LblBoxJointThickness.Size = New Size(142, 21)
        LblBoxJointThickness.TabIndex = 0
        LblBoxJointThickness.Text = "Board Width: "
        ' 
        ' TxtBoxJointWidth
        ' 
        TxtBoxJointWidth.Location = New Point(182, 29)
        TxtBoxJointWidth.Name = "TxtBoxJointWidth"
        TxtBoxJointWidth.Size = New Size(110, 28)
        TxtBoxJointWidth.TabIndex = 0
        ' 
        ' LblBoxJointWidth
        ' 
        LblBoxJointWidth.AutoSize = True
        LblBoxJointWidth.Location = New Point(11, 33)
        LblBoxJointWidth.Name = "LblBoxJointWidth"
        LblBoxJointWidth.Size = New Size(171, 21)
        LblBoxJointWidth.TabIndex = 0
        LblBoxJointWidth.Text = "Stock Thickness: "
        ' 
        ' BtnCalculateJoinery
        ' 
        BtnCalculateJoinery.BackColor = Color.MistyRose
        BtnCalculateJoinery.Font = New Font("Georgia", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnCalculateJoinery.Location = New Point(68, 754)
        BtnCalculateJoinery.Name = "BtnCalculateJoinery"
        BtnCalculateJoinery.Size = New Size(265, 40)
        BtnCalculateJoinery.TabIndex = 0
        BtnCalculateJoinery.Text = "Calculate Joinery"
        BtnCalculateJoinery.UseVisualStyleBackColor = False
        ' 
        ' GbxMortiseTenonResults
        ' 
        GbxMortiseTenonResults.BackColor = Color.WhiteSmoke
        GbxMortiseTenonResults.Controls.Add(LblShoulderOffset)
        GbxMortiseTenonResults.Controls.Add(LblMortiseWidth)
        GbxMortiseTenonResults.Controls.Add(LblMortiseDepth)
        GbxMortiseTenonResults.Controls.Add(LblTenonWidth)
        GbxMortiseTenonResults.Controls.Add(LblTenonThickness)
        GbxMortiseTenonResults.Controls.Add(LblTenonLength)
        GbxMortiseTenonResults.Font = New Font("Georgia", 10F, FontStyle.Bold)
        GbxMortiseTenonResults.Location = New Point(34, 11)
        GbxMortiseTenonResults.Name = "GbxMortiseTenonResults"
        GbxMortiseTenonResults.Size = New Size(420, 256)
        GbxMortiseTenonResults.TabIndex = 1
        GbxMortiseTenonResults.TabStop = False
        GbxMortiseTenonResults.Text = "Mortise && Tenon Results"
        ' 
        ' LblShoulderOffset
        ' 
        LblShoulderOffset.AutoSize = True
        LblShoulderOffset.Font = New Font("Georgia", 9F, FontStyle.Bold)
        LblShoulderOffset.ForeColor = Color.DarkBlue
        LblShoulderOffset.Location = New Point(15, 215)
        LblShoulderOffset.Name = "LblShoulderOffset"
        LblShoulderOffset.Size = New Size(182, 21)
        LblShoulderOffset.TabIndex = 5
        LblShoulderOffset.Text = "Shoulder Offset: --"
        ' 
        ' LblMortiseWidth
        ' 
        LblMortiseWidth.AutoSize = True
        LblMortiseWidth.Font = New Font("Georgia", 9F, FontStyle.Bold)
        LblMortiseWidth.ForeColor = Color.DarkBlue
        LblMortiseWidth.Location = New Point(15, 178)
        LblMortiseWidth.Name = "LblMortiseWidth"
        LblMortiseWidth.Size = New Size(169, 21)
        LblMortiseWidth.TabIndex = 4
        LblMortiseWidth.Text = "Mortise Width: --"
        ' 
        ' LblMortiseDepth
        ' 
        LblMortiseDepth.AutoSize = True
        LblMortiseDepth.Font = New Font("Georgia", 9F, FontStyle.Bold)
        LblMortiseDepth.ForeColor = Color.DarkBlue
        LblMortiseDepth.Location = New Point(15, 141)
        LblMortiseDepth.Name = "LblMortiseDepth"
        LblMortiseDepth.Size = New Size(168, 21)
        LblMortiseDepth.TabIndex = 3
        LblMortiseDepth.Text = "Mortise Depth: --"
        ' 
        ' LblTenonWidth
        ' 
        LblTenonWidth.AutoSize = True
        LblTenonWidth.Font = New Font("Georgia", 9F, FontStyle.Bold)
        LblTenonWidth.ForeColor = Color.DarkBlue
        LblTenonWidth.Location = New Point(15, 104)
        LblTenonWidth.Name = "LblTenonWidth"
        LblTenonWidth.Size = New Size(158, 21)
        LblTenonWidth.TabIndex = 2
        LblTenonWidth.Text = "Tenon Width: --"
        ' 
        ' LblTenonThickness
        ' 
        LblTenonThickness.AutoSize = True
        LblTenonThickness.Font = New Font("Georgia", 9F, FontStyle.Bold)
        LblTenonThickness.ForeColor = Color.DarkBlue
        LblTenonThickness.Location = New Point(15, 30)
        LblTenonThickness.Name = "LblTenonThickness"
        LblTenonThickness.Size = New Size(193, 21)
        LblTenonThickness.TabIndex = 0
        LblTenonThickness.Text = "Tenon Thickness: --"
        ' 
        ' LblTenonLength
        ' 
        LblTenonLength.AutoSize = True
        LblTenonLength.Font = New Font("Georgia", 9F, FontStyle.Bold)
        LblTenonLength.ForeColor = Color.DarkBlue
        LblTenonLength.Location = New Point(15, 67)
        LblTenonLength.Name = "LblTenonLength"
        LblTenonLength.Size = New Size(165, 21)
        LblTenonLength.TabIndex = 1
        LblTenonLength.Text = "Tenon Length: --"
        ' 
        ' PbJointDiagram
        ' 
        PbJointDiagram.BorderStyle = BorderStyle.Fixed3D
        PbJointDiagram.Location = New Point(49, 734)
        PbJointDiagram.Name = "PbJointDiagram"
        PbJointDiagram.Size = New Size(500, 350)
        PbJointDiagram.TabIndex = 10
        PbJointDiagram.TabStop = False
        ' 
        ' GbxDovetailResults
        ' 
        GbxDovetailResults.BackColor = Color.WhiteSmoke
        GbxDovetailResults.Controls.Add(LblDovetailCount)
        GbxDovetailResults.Controls.Add(LblDovetailAngle)
        GbxDovetailResults.Controls.Add(LblDovetailPinWidth)
        GbxDovetailResults.Controls.Add(LblDovetailTailWidth)
        GbxDovetailResults.Font = New Font("Georgia", 10F, FontStyle.Bold)
        GbxDovetailResults.Location = New Point(34, 276)
        GbxDovetailResults.Name = "GbxDovetailResults"
        GbxDovetailResults.Size = New Size(420, 198)
        GbxDovetailResults.TabIndex = 0
        GbxDovetailResults.TabStop = False
        GbxDovetailResults.Text = "Dovetail Results"
        ' 
        ' LblDovetailCount
        ' 
        LblDovetailCount.AutoSize = True
        LblDovetailCount.Font = New Font("Georgia", 9F, FontStyle.Bold)
        LblDovetailCount.Location = New Point(18, 77)
        LblDovetailCount.Name = "LblDovetailCount"
        LblDovetailCount.Size = New Size(133, 21)
        LblDovetailCount.TabIndex = 0
        LblDovetailCount.Text = "Tail Count: --"
        ' 
        ' LblDovetailAngle
        ' 
        LblDovetailAngle.AutoSize = True
        LblDovetailAngle.Font = New Font("Georgia", 9F, FontStyle.Bold)
        LblDovetailAngle.Location = New Point(18, 38)
        LblDovetailAngle.Name = "LblDovetailAngle"
        LblDovetailAngle.Size = New Size(89, 21)
        LblDovetailAngle.TabIndex = 0
        LblDovetailAngle.Text = "Angle: --"
        ' 
        ' LblDovetailPinWidth
        ' 
        LblDovetailPinWidth.AutoSize = True
        LblDovetailPinWidth.Font = New Font("Georgia", 9F, FontStyle.Bold)
        LblDovetailPinWidth.Location = New Point(18, 153)
        LblDovetailPinWidth.Name = "LblDovetailPinWidth"
        LblDovetailPinWidth.Size = New Size(131, 21)
        LblDovetailPinWidth.TabIndex = 0
        LblDovetailPinWidth.Text = "Pin Width: --"
        ' 
        ' LblDovetailTailWidth
        ' 
        LblDovetailTailWidth.AutoSize = True
        LblDovetailTailWidth.Font = New Font("Georgia", 9F, FontStyle.Bold)
        LblDovetailTailWidth.Location = New Point(18, 116)
        LblDovetailTailWidth.Name = "LblDovetailTailWidth"
        LblDovetailTailWidth.Size = New Size(134, 21)
        LblDovetailTailWidth.TabIndex = 0
        LblDovetailTailWidth.Text = "Tail Width: --"
        ' 
        ' GbxDadoResults
        ' 
        GbxDadoResults.BackColor = Color.WhiteSmoke
        GbxDadoResults.Controls.Add(LblDadoDepth)
        GbxDadoResults.Controls.Add(LblDadoWidth)
        GbxDadoResults.Location = New Point(34, 616)
        GbxDadoResults.Name = "GbxDadoResults"
        GbxDadoResults.Size = New Size(420, 100)
        GbxDadoResults.TabIndex = 0
        GbxDadoResults.TabStop = False
        GbxDadoResults.Text = "Dado Results"
        ' 
        ' LblDadoDepth
        ' 
        LblDadoDepth.Location = New Point(17, 58)
        LblDadoDepth.Name = "LblDadoDepth"
        LblDadoDepth.Size = New Size(100, 23)
        LblDadoDepth.TabIndex = 0
        LblDadoDepth.Text = "Width: --"
        ' 
        ' LblDadoWidth
        ' 
        LblDadoWidth.Location = New Point(17, 25)
        LblDadoWidth.Name = "LblDadoWidth"
        LblDadoWidth.Size = New Size(100, 23)
        LblDadoWidth.TabIndex = 0
        LblDadoWidth.Text = "Depth: --"
        ' 
        ' GbxBoxJointResults
        ' 
        GbxBoxJointResults.BackColor = Color.WhiteSmoke
        GbxBoxJointResults.Controls.Add(LblBoxJointPinWidth)
        GbxBoxJointResults.Controls.Add(LblBoxJointCount)
        GbxBoxJointResults.Location = New Point(34, 480)
        GbxBoxJointResults.Name = "GbxBoxJointResults"
        GbxBoxJointResults.Size = New Size(420, 100)
        GbxBoxJointResults.TabIndex = 0
        GbxBoxJointResults.TabStop = False
        GbxBoxJointResults.Text = "Box Joint Results"
        ' 
        ' LblBoxJointPinWidth
        ' 
        LblBoxJointPinWidth.AutoSize = True
        LblBoxJointPinWidth.Location = New Point(17, 29)
        LblBoxJointPinWidth.Name = "LblBoxJointPinWidth"
        LblBoxJointPinWidth.Size = New Size(117, 18)
        LblBoxJointPinWidth.TabIndex = 0
        LblBoxJointPinWidth.Text = "Pin Width: --"
        ' 
        ' LblBoxJointCount
        ' 
        LblBoxJointCount.AutoSize = True
        LblBoxJointCount.Location = New Point(17, 60)
        LblBoxJointCount.Name = "LblBoxJointCount"
        LblBoxJointCount.Size = New Size(114, 18)
        LblBoxJointCount.TabIndex = 0
        LblBoxJointCount.Text = "Pin Count: --"
        ' 
        ' TpWoodMovement
        ' 
        TpWoodMovement.BackColor = Color.Gainsboro
        TpWoodMovement.BorderStyle = BorderStyle.Fixed3D
        TpWoodMovement.Controls.Add(TcWoodMovement)
        TpWoodMovement.Location = New Point(4, 27)
        TpWoodMovement.Name = "TpWoodMovement"
        TpWoodMovement.Size = New Size(1170, 823)
        TpWoodMovement.TabIndex = 9
        TpWoodMovement.Text = "Wood Movement"
        ' 
        ' TcWoodMovement
        ' 
        TcWoodMovement.Alignment = TabAlignment.Right
        TcWoodMovement.Controls.Add(TpWmWoodMovement)
        TcWoodMovement.Controls.Add(TpWmShelfSag)
        TcWoodMovement.Dock = DockStyle.Fill
        TcWoodMovement.Location = New Point(0, 0)
        TcWoodMovement.Multiline = True
        TcWoodMovement.Name = "TcWoodMovement"
        TcWoodMovement.SelectedIndex = 0
        TcWoodMovement.Size = New Size(1166, 819)
        TcWoodMovement.TabIndex = 1
        ' 
        ' TpWmWoodMovement
        ' 
        TpWmWoodMovement.BackColor = Color.Gainsboro
        TpWmWoodMovement.BorderStyle = BorderStyle.Fixed3D
        TpWmWoodMovement.Controls.Add(GbxWoodMovementResults)
        TpWmWoodMovement.Controls.Add(GbxPanelGaps)
        TpWmWoodMovement.Controls.Add(BtnCalculateMovement)
        TpWmWoodMovement.Controls.Add(GbxGrainDirection)
        TpWmWoodMovement.Controls.Add(GbxWoodProperties)
        TpWmWoodMovement.Controls.Add(GbxWoodMovementInput)
        TpWmWoodMovement.Location = New Point(4, 4)
        TpWmWoodMovement.Name = "TpWmWoodMovement"
        TpWmWoodMovement.Padding = New Padding(3)
        TpWmWoodMovement.Size = New Size(1134, 811)
        TpWmWoodMovement.TabIndex = 0
        TpWmWoodMovement.Text = "Wood Movement"
        ' 
        ' GbxWoodMovementResults
        ' 
        GbxWoodMovementResults.BackColor = Color.WhiteSmoke
        GbxWoodMovementResults.Controls.Add(LblMovementResult)
        GbxWoodMovementResults.Controls.Add(LblMovementDirection)
        GbxWoodMovementResults.Controls.Add(LblMovementFraction)
        GbxWoodMovementResults.Location = New Point(542, 17)
        GbxWoodMovementResults.Name = "GbxWoodMovementResults"
        GbxWoodMovementResults.Size = New Size(434, 150)
        GbxWoodMovementResults.TabIndex = 0
        GbxWoodMovementResults.TabStop = False
        GbxWoodMovementResults.Text = "Movement Results"
        ' 
        ' LblMovementResult
        ' 
        LblMovementResult.AutoSize = True
        LblMovementResult.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        LblMovementResult.Location = New Point(15, 30)
        LblMovementResult.Name = "LblMovementResult"
        LblMovementResult.Size = New Size(141, 28)
        LblMovementResult.TabIndex = 0
        LblMovementResult.Text = "Movement: --"
        ' 
        ' LblMovementDirection
        ' 
        LblMovementDirection.AutoSize = True
        LblMovementDirection.Location = New Point(15, 73)
        LblMovementDirection.Name = "LblMovementDirection"
        LblMovementDirection.Size = New Size(110, 18)
        LblMovementDirection.TabIndex = 1
        LblMovementDirection.Text = "Direction: --"
        ' 
        ' LblMovementFraction
        ' 
        LblMovementFraction.AutoSize = True
        LblMovementFraction.Location = New Point(15, 106)
        LblMovementFraction.Name = "LblMovementFraction"
        LblMovementFraction.Size = New Size(128, 18)
        LblMovementFraction.TabIndex = 2
        LblMovementFraction.Text = "As Fraction: --"
        ' 
        ' GbxPanelGaps
        ' 
        GbxPanelGaps.BackColor = Color.WhiteSmoke
        GbxPanelGaps.Controls.Add(LblPanelGapMin)
        GbxPanelGaps.Controls.Add(LblPanelGapMax)
        GbxPanelGaps.Location = New Point(542, 177)
        GbxPanelGaps.Name = "GbxPanelGaps"
        GbxPanelGaps.Size = New Size(434, 100)
        GbxPanelGaps.TabIndex = 1
        GbxPanelGaps.TabStop = False
        GbxPanelGaps.Text = "Recommended Panel Gaps"
        ' 
        ' LblPanelGapMin
        ' 
        LblPanelGapMin.AutoSize = True
        LblPanelGapMin.Location = New Point(15, 30)
        LblPanelGapMin.Name = "LblPanelGapMin"
        LblPanelGapMin.Size = New Size(151, 18)
        LblPanelGapMin.TabIndex = 0
        LblPanelGapMin.Text = "Minimum Gap: --"
        ' 
        ' LblPanelGapMax
        ' 
        LblPanelGapMax.AutoSize = True
        LblPanelGapMax.Location = New Point(15, 60)
        LblPanelGapMax.Name = "LblPanelGapMax"
        LblPanelGapMax.Size = New Size(154, 18)
        LblPanelGapMax.TabIndex = 1
        LblPanelGapMax.Text = "Maximum Gap: --"
        ' 
        ' BtnCalculateMovement
        ' 
        BtnCalculateMovement.BackColor = Color.MistyRose
        BtnCalculateMovement.Location = New Point(794, 514)
        BtnCalculateMovement.Name = "BtnCalculateMovement"
        BtnCalculateMovement.Size = New Size(208, 40)
        BtnCalculateMovement.TabIndex = 11
        BtnCalculateMovement.Text = "Calculate Movement"
        BtnCalculateMovement.UseVisualStyleBackColor = False
        ' 
        ' GbxGrainDirection
        ' 
        GbxGrainDirection.BackColor = Color.Silver
        GbxGrainDirection.Controls.Add(RbTangential)
        GbxGrainDirection.Controls.Add(RbRadial)
        GbxGrainDirection.Location = New Point(6, 337)
        GbxGrainDirection.Name = "GbxGrainDirection"
        GbxGrainDirection.Size = New Size(425, 100)
        GbxGrainDirection.TabIndex = 10
        GbxGrainDirection.TabStop = False
        GbxGrainDirection.Text = "Grain Direction"
        ' 
        ' RbTangential
        ' 
        RbTangential.AutoSize = True
        RbTangential.Checked = True
        RbTangential.Location = New Point(15, 25)
        RbTangential.Name = "RbTangential"
        RbTangential.Size = New Size(226, 22)
        RbTangential.TabIndex = 0
        RbTangential.TabStop = True
        RbTangential.Text = "Tangential (Flat Sawn)"
        ' 
        ' RbRadial
        ' 
        RbRadial.AutoSize = True
        RbRadial.Location = New Point(15, 58)
        RbRadial.Name = "RbRadial"
        RbRadial.Size = New Size(222, 22)
        RbRadial.TabIndex = 1
        RbRadial.Text = "Radial (Quarter Sawn)"
        ' 
        ' GbxWoodProperties
        ' 
        GbxWoodProperties.BackColor = Color.WhiteSmoke
        GbxWoodProperties.Controls.Add(LblWoodDensity)
        GbxWoodProperties.Controls.Add(LblWoodType)
        GbxWoodProperties.Location = New Point(542, 287)
        GbxWoodProperties.Name = "GbxWoodProperties"
        GbxWoodProperties.Size = New Size(434, 100)
        GbxWoodProperties.TabIndex = 2
        GbxWoodProperties.TabStop = False
        GbxWoodProperties.Text = "Wood Properties"
        ' 
        ' LblWoodDensity
        ' 
        LblWoodDensity.AutoSize = True
        LblWoodDensity.Location = New Point(15, 30)
        LblWoodDensity.Name = "LblWoodDensity"
        LblWoodDensity.Size = New Size(94, 18)
        LblWoodDensity.TabIndex = 0
        LblWoodDensity.Text = "Density: --"
        ' 
        ' LblWoodType
        ' 
        LblWoodType.AutoSize = True
        LblWoodType.Location = New Point(15, 60)
        LblWoodType.Name = "LblWoodType"
        LblWoodType.Size = New Size(73, 18)
        LblWoodType.TabIndex = 1
        LblWoodType.Text = "Type: --"
        ' 
        ' GbxWoodMovementInput
        ' 
        GbxWoodMovementInput.BackColor = Color.Silver
        GbxWoodMovementInput.Controls.Add(LblWoodSpecies)
        GbxWoodMovementInput.Controls.Add(CmbWoodSpecies)
        GbxWoodMovementInput.Controls.Add(LblMovementWidth)
        GbxWoodMovementInput.Controls.Add(TxtMovementWidth)
        GbxWoodMovementInput.Controls.Add(LblInitialHumidity)
        GbxWoodMovementInput.Controls.Add(TxtInitialHumidity)
        GbxWoodMovementInput.Controls.Add(LblFinalHumidity)
        GbxWoodMovementInput.Controls.Add(TxtFinalHumidity)
        GbxWoodMovementInput.Controls.Add(LblHumidityPreset)
        GbxWoodMovementInput.Controls.Add(CmbHumidityPreset)
        GbxWoodMovementInput.Location = New Point(6, 17)
        GbxWoodMovementInput.Name = "GbxWoodMovementInput"
        GbxWoodMovementInput.Size = New Size(425, 314)
        GbxWoodMovementInput.TabIndex = 0
        GbxWoodMovementInput.TabStop = False
        GbxWoodMovementInput.Text = "Wood Movement Input"
        ' 
        ' LblWoodSpecies
        ' 
        LblWoodSpecies.Location = New Point(15, 30)
        LblWoodSpecies.Name = "LblWoodSpecies"
        LblWoodSpecies.Size = New Size(143, 40)
        LblWoodSpecies.TabIndex = 0
        LblWoodSpecies.Text = "Wood Species:"
        ' 
        ' CmbWoodSpecies
        ' 
        CmbWoodSpecies.DropDownStyle = ComboBoxStyle.DropDownList
        CmbWoodSpecies.Location = New Point(158, 37)
        CmbWoodSpecies.Name = "CmbWoodSpecies"
        CmbWoodSpecies.Size = New Size(200, 26)
        CmbWoodSpecies.Sorted = True
        CmbWoodSpecies.TabIndex = 1
        ' 
        ' LblMovementWidth
        ' 
        LblMovementWidth.Location = New Point(15, 78)
        LblMovementWidth.Name = "LblMovementWidth"
        LblMovementWidth.Size = New Size(120, 50)
        LblMovementWidth.TabIndex = 2
        LblMovementWidth.Text = "Board Width (in):"
        ' 
        ' TxtMovementWidth
        ' 
        TxtMovementWidth.Location = New Point(158, 90)
        TxtMovementWidth.Name = "TxtMovementWidth"
        TxtMovementWidth.Size = New Size(100, 26)
        TxtMovementWidth.TabIndex = 3
        TxtMovementWidth.Text = "12"
        ' 
        ' LblInitialHumidity
        ' 
        LblInitialHumidity.Location = New Point(15, 136)
        LblInitialHumidity.Name = "LblInitialHumidity"
        LblInitialHumidity.Size = New Size(130, 43)
        LblInitialHumidity.TabIndex = 4
        LblInitialHumidity.Text = "Initial Humidity (%):"
        ' 
        ' TxtInitialHumidity
        ' 
        TxtInitialHumidity.Location = New Point(158, 144)
        TxtInitialHumidity.Name = "TxtInitialHumidity"
        TxtInitialHumidity.Size = New Size(100, 26)
        TxtInitialHumidity.TabIndex = 5
        TxtInitialHumidity.Text = "12"
        ' 
        ' LblFinalHumidity
        ' 
        LblFinalHumidity.Location = New Point(15, 187)
        LblFinalHumidity.Name = "LblFinalHumidity"
        LblFinalHumidity.Size = New Size(130, 47)
        LblFinalHumidity.TabIndex = 6
        LblFinalHumidity.Text = "Final Humidity (%):"
        ' 
        ' TxtFinalHumidity
        ' 
        TxtFinalHumidity.Location = New Point(158, 197)
        TxtFinalHumidity.Name = "TxtFinalHumidity"
        TxtFinalHumidity.Size = New Size(100, 26)
        TxtFinalHumidity.TabIndex = 7
        TxtFinalHumidity.Text = "8"
        ' 
        ' LblHumidityPreset
        ' 
        LblHumidityPreset.Location = New Point(15, 243)
        LblHumidityPreset.Name = "LblHumidityPreset"
        LblHumidityPreset.Size = New Size(130, 44)
        LblHumidityPreset.TabIndex = 8
        LblHumidityPreset.Text = "Humidity Preset:"
        ' 
        ' CmbHumidityPreset
        ' 
        CmbHumidityPreset.DropDownStyle = ComboBoxStyle.DropDownList
        CmbHumidityPreset.Location = New Point(158, 252)
        CmbHumidityPreset.Name = "CmbHumidityPreset"
        CmbHumidityPreset.Size = New Size(200, 26)
        CmbHumidityPreset.TabIndex = 9
        ' 
        ' TpWmShelfSag
        ' 
        TpWmShelfSag.BackColor = Color.Gainsboro
        TpWmShelfSag.BorderStyle = BorderStyle.Fixed3D
        TpWmShelfSag.Controls.Add(GbShelfSupportType)
        TpWmShelfSag.Controls.Add(GbxShelfSagInput)
        TpWmShelfSag.Controls.Add(BtnCalculateShelf)
        TpWmShelfSag.Controls.Add(GbxShelfSagResults)
        TpWmShelfSag.Location = New Point(4, 4)
        TpWmShelfSag.Name = "TpWmShelfSag"
        TpWmShelfSag.Padding = New Padding(3)
        TpWmShelfSag.Size = New Size(1128, 811)
        TpWmShelfSag.TabIndex = 1
        TpWmShelfSag.Text = "Shelf Sag"
        ' 
        ' GbShelfSupportType
        ' 
        GbShelfSupportType.BackColor = Color.Silver
        GbShelfSupportType.Controls.Add(LblPinWidthUnits)
        GbShelfSupportType.Controls.Add(TxtPinWidth)
        GbShelfSupportType.Controls.Add(LblPinWidth)
        GbShelfSupportType.Controls.Add(RbSupportPin)
        GbShelfSupportType.Controls.Add(LblBracketWidthUnits)
        GbShelfSupportType.Controls.Add(TxtshelfBracketWidth)
        GbShelfSupportType.Controls.Add(LblShelfBracketWidth)
        GbShelfSupportType.Controls.Add(LblSupportTypeInfo)
        GbShelfSupportType.Controls.Add(LblDadoDepthUnit)
        GbShelfSupportType.Controls.Add(TxtDadoDepth1)
        GbShelfSupportType.Controls.Add(LblDadoDepth1)
        GbShelfSupportType.Controls.Add(RbSupportDado)
        GbShelfSupportType.Controls.Add(RbSupportBracket)
        GbShelfSupportType.Location = New Point(23, 355)
        GbShelfSupportType.Name = "GbShelfSupportType"
        GbShelfSupportType.Size = New Size(425, 248)
        GbShelfSupportType.TabIndex = 13
        GbShelfSupportType.TabStop = False
        GbShelfSupportType.Text = "Shelf Support Type"
        ' 
        ' LblPinWidthUnits
        ' 
        LblPinWidthUnits.AutoSize = True
        LblPinWidthUnits.Location = New Point(219, 127)
        LblPinWidthUnits.Name = "LblPinWidthUnits"
        LblPinWidthUnits.Size = New Size(62, 18)
        LblPinWidthUnits.TabIndex = 12
        LblPinWidthUnits.Text = "inches"
        ' 
        ' TxtPinWidth
        ' 
        TxtPinWidth.Location = New Point(126, 123)
        TxtPinWidth.Name = "TxtPinWidth"
        TxtPinWidth.Size = New Size(93, 26)
        TxtPinWidth.TabIndex = 11
        ' 
        ' LblPinWidth
        ' 
        LblPinWidth.AutoSize = True
        LblPinWidth.Location = New Point(23, 127)
        LblPinWidth.Name = "LblPinWidth"
        LblPinWidth.Size = New Size(103, 18)
        LblPinWidth.TabIndex = 10
        LblPinWidth.Text = "Pin Width: "
        ' 
        ' RbSupportPin
        ' 
        RbSupportPin.AutoSize = True
        RbSupportPin.Location = New Point(234, 24)
        RbSupportPin.Name = "RbSupportPin"
        RbSupportPin.Size = New Size(61, 22)
        RbSupportPin.TabIndex = 9
        RbSupportPin.TabStop = True
        RbSupportPin.Text = "Pin"
        RbSupportPin.UseVisualStyleBackColor = True
        ' 
        ' LblBracketWidthUnits
        ' 
        LblBracketWidthUnits.AutoSize = True
        LblBracketWidthUnits.Location = New Point(247, 96)
        LblBracketWidthUnits.Name = "LblBracketWidthUnits"
        LblBracketWidthUnits.Size = New Size(62, 18)
        LblBracketWidthUnits.TabIndex = 8
        LblBracketWidthUnits.Text = "inches"
        ' 
        ' TxtshelfBracketWidth
        ' 
        TxtshelfBracketWidth.Location = New Point(154, 92)
        TxtshelfBracketWidth.Name = "TxtshelfBracketWidth"
        TxtshelfBracketWidth.Size = New Size(93, 26)
        TxtshelfBracketWidth.TabIndex = 7
        ' 
        ' LblShelfBracketWidth
        ' 
        LblShelfBracketWidth.AutoSize = True
        LblShelfBracketWidth.Location = New Point(15, 96)
        LblShelfBracketWidth.Name = "LblShelfBracketWidth"
        LblShelfBracketWidth.Size = New Size(139, 18)
        LblShelfBracketWidth.TabIndex = 6
        LblShelfBracketWidth.Text = "Bracket Width: "
        ' 
        ' LblSupportTypeInfo
        ' 
        LblSupportTypeInfo.Location = New Point(15, 163)
        LblSupportTypeInfo.Name = "LblSupportTypeInfo"
        LblSupportTypeInfo.Size = New Size(392, 57)
        LblSupportTypeInfo.TabIndex = 5
        LblSupportTypeInfo.Text = "Explanation:"
        ' 
        ' LblDadoDepthUnit
        ' 
        LblDadoDepthUnit.AutoSize = True
        LblDadoDepthUnit.Location = New Point(219, 60)
        LblDadoDepthUnit.Name = "LblDadoDepthUnit"
        LblDadoDepthUnit.Size = New Size(62, 18)
        LblDadoDepthUnit.TabIndex = 4
        LblDadoDepthUnit.Text = "inches"
        ' 
        ' TxtDadoDepth1
        ' 
        TxtDadoDepth1.Location = New Point(126, 56)
        TxtDadoDepth1.Name = "TxtDadoDepth1"
        TxtDadoDepth1.Size = New Size(93, 26)
        TxtDadoDepth1.TabIndex = 3
        ' 
        ' LblDadoDepth1
        ' 
        LblDadoDepth1.AutoSize = True
        LblDadoDepth1.Location = New Point(15, 60)
        LblDadoDepth1.Name = "LblDadoDepth1"
        LblDadoDepth1.Size = New Size(111, 18)
        LblDadoDepth1.TabIndex = 2
        LblDadoDepth1.Text = "Dado Depth:"
        ' 
        ' RbSupportDado
        ' 
        RbSupportDado.AutoSize = True
        RbSupportDado.Location = New Point(135, 24)
        RbSupportDado.Name = "RbSupportDado"
        RbSupportDado.Size = New Size(76, 22)
        RbSupportDado.TabIndex = 1
        RbSupportDado.TabStop = True
        RbSupportDado.Text = "Dado"
        RbSupportDado.UseVisualStyleBackColor = True
        ' 
        ' RbSupportBracket
        ' 
        RbSupportBracket.AutoSize = True
        RbSupportBracket.Location = New Point(15, 24)
        RbSupportBracket.Name = "RbSupportBracket"
        RbSupportBracket.Size = New Size(97, 22)
        RbSupportBracket.TabIndex = 0
        RbSupportBracket.TabStop = True
        RbSupportBracket.Text = "Bracket"
        RbSupportBracket.UseVisualStyleBackColor = True
        ' 
        ' GbxShelfSagInput
        ' 
        GbxShelfSagInput.BackColor = Color.Silver
        GbxShelfSagInput.Controls.Add(GbxStiffener)
        GbxShelfSagInput.Controls.Add(TxtShelfLoad)
        GbxShelfSagInput.Controls.Add(LblShelfLoad)
        GbxShelfSagInput.Controls.Add(TxtShelfWidth)
        GbxShelfSagInput.Controls.Add(LblShelfWidth)
        GbxShelfSagInput.Controls.Add(TxtShelfThickness)
        GbxShelfSagInput.Controls.Add(LblShelfThickness)
        GbxShelfSagInput.Controls.Add(TxtShelfSpan)
        GbxShelfSagInput.Controls.Add(LblShelfSpan)
        GbxShelfSagInput.Controls.Add(CmbShelfMaterial)
        GbxShelfSagInput.Controls.Add(LblShelfMaterial)
        GbxShelfSagInput.Location = New Point(23, 10)
        GbxShelfSagInput.Name = "GbxShelfSagInput"
        GbxShelfSagInput.Size = New Size(425, 345)
        GbxShelfSagInput.TabIndex = 12
        GbxShelfSagInput.TabStop = False
        GbxShelfSagInput.Text = "Shelf Sag Input"
        ' 
        ' GbxStiffener
        ' 
        GbxStiffener.Controls.Add(TxtStiffenerThickness)
        GbxStiffener.Controls.Add(TxtStiffenerheight)
        GbxStiffener.Controls.Add(CmbStiffenerMaterial)
        GbxStiffener.Controls.Add(LblStiffenerMaterial)
        GbxStiffener.Controls.Add(LblStiffenerThickness)
        GbxStiffener.Controls.Add(LblStiffenerHeight)
        GbxStiffener.Controls.Add(ChkBackStiffener)
        GbxStiffener.Controls.Add(ChkFrontStiffener)
        GbxStiffener.Dock = DockStyle.Bottom
        GbxStiffener.Location = New Point(3, 137)
        GbxStiffener.Name = "GbxStiffener"
        GbxStiffener.Size = New Size(419, 205)
        GbxStiffener.TabIndex = 13
        GbxStiffener.TabStop = False
        GbxStiffener.Text = "Edge Stiffeners"
        ' 
        ' TxtStiffenerThickness
        ' 
        TxtStiffenerThickness.Location = New Point(194, 134)
        TxtStiffenerThickness.Name = "TxtStiffenerThickness"
        TxtStiffenerThickness.Size = New Size(150, 26)
        TxtStiffenerThickness.TabIndex = 7
        ' 
        ' TxtStiffenerheight
        ' 
        TxtStiffenerheight.Location = New Point(194, 101)
        TxtStiffenerheight.Name = "TxtStiffenerheight"
        TxtStiffenerheight.Size = New Size(150, 26)
        TxtStiffenerheight.TabIndex = 6
        ' 
        ' CmbStiffenerMaterial
        ' 
        CmbStiffenerMaterial.FormattingEnabled = True
        CmbStiffenerMaterial.Location = New Point(104, 167)
        CmbStiffenerMaterial.Name = "CmbStiffenerMaterial"
        CmbStiffenerMaterial.Size = New Size(251, 26)
        CmbStiffenerMaterial.TabIndex = 5
        ' 
        ' LblStiffenerMaterial
        ' 
        LblStiffenerMaterial.AutoSize = True
        LblStiffenerMaterial.Location = New Point(15, 171)
        LblStiffenerMaterial.Name = "LblStiffenerMaterial"
        LblStiffenerMaterial.Size = New Size(89, 18)
        LblStiffenerMaterial.TabIndex = 4
        LblStiffenerMaterial.Text = "Material: "
        ' 
        ' LblStiffenerThickness
        ' 
        LblStiffenerThickness.AutoSize = True
        LblStiffenerThickness.Location = New Point(16, 138)
        LblStiffenerThickness.Name = "LblStiffenerThickness"
        LblStiffenerThickness.Size = New Size(178, 18)
        LblStiffenerThickness.TabIndex = 3
        LblStiffenerThickness.Text = "Stiffener Thickness: "
        ' 
        ' LblStiffenerHeight
        ' 
        LblStiffenerHeight.AutoSize = True
        LblStiffenerHeight.Location = New Point(44, 105)
        LblStiffenerHeight.Name = "LblStiffenerHeight"
        LblStiffenerHeight.Size = New Size(150, 18)
        LblStiffenerHeight.TabIndex = 2
        LblStiffenerHeight.Text = "Stiffener Height: "
        ' 
        ' ChkBackStiffener
        ' 
        ChkBackStiffener.AutoSize = True
        ChkBackStiffener.Location = New Point(16, 68)
        ChkBackStiffener.Name = "ChkBackStiffener"
        ChkBackStiffener.Size = New Size(194, 22)
        ChkBackStiffener.TabIndex = 1
        ChkBackStiffener.Text = "Back Edge Stiffener"
        ChkBackStiffener.UseVisualStyleBackColor = True
        ' 
        ' ChkFrontStiffener
        ' 
        ChkFrontStiffener.AutoSize = True
        ChkFrontStiffener.Location = New Point(16, 31)
        ChkFrontStiffener.Name = "ChkFrontStiffener"
        ChkFrontStiffener.Size = New Size(201, 22)
        ChkFrontStiffener.TabIndex = 0
        ChkFrontStiffener.Text = "Front Edge Stiffener"
        ChkFrontStiffener.UseVisualStyleBackColor = True
        ' 
        ' TxtShelfLoad
        ' 
        TxtShelfLoad.Location = New Point(310, 63)
        TxtShelfLoad.Name = "TxtShelfLoad"
        TxtShelfLoad.Size = New Size(85, 26)
        TxtShelfLoad.TabIndex = 9
        ' 
        ' LblShelfLoad
        ' 
        LblShelfLoad.AutoSize = True
        LblShelfLoad.Location = New Point(250, 67)
        LblShelfLoad.Name = "LblShelfLoad"
        LblShelfLoad.Size = New Size(60, 18)
        LblShelfLoad.TabIndex = 8
        LblShelfLoad.Text = "Load: "
        ' 
        ' TxtShelfWidth
        ' 
        TxtShelfWidth.Location = New Point(310, 98)
        TxtShelfWidth.Name = "TxtShelfWidth"
        TxtShelfWidth.Size = New Size(85, 26)
        TxtShelfWidth.TabIndex = 7
        ' 
        ' LblShelfWidth
        ' 
        LblShelfWidth.AutoSize = True
        LblShelfWidth.Location = New Point(239, 102)
        LblShelfWidth.Name = "LblShelfWidth"
        LblShelfWidth.Size = New Size(71, 18)
        LblShelfWidth.TabIndex = 6
        LblShelfWidth.Text = "Width: "
        ' 
        ' TxtShelfThickness
        ' 
        TxtShelfThickness.Location = New Point(121, 98)
        TxtShelfThickness.Name = "TxtShelfThickness"
        TxtShelfThickness.Size = New Size(85, 26)
        TxtShelfThickness.TabIndex = 5
        ' 
        ' LblShelfThickness
        ' 
        LblShelfThickness.AutoSize = True
        LblShelfThickness.Location = New Point(15, 102)
        LblShelfThickness.Name = "LblShelfThickness"
        LblShelfThickness.Size = New Size(102, 18)
        LblShelfThickness.TabIndex = 4
        LblShelfThickness.Text = "Thickness: "
        ' 
        ' TxtShelfSpan
        ' 
        TxtShelfSpan.Location = New Point(121, 63)
        TxtShelfSpan.Name = "TxtShelfSpan"
        TxtShelfSpan.Size = New Size(85, 26)
        TxtShelfSpan.TabIndex = 3
        ' 
        ' LblShelfSpan
        ' 
        LblShelfSpan.AutoSize = True
        LblShelfSpan.Location = New Point(15, 67)
        LblShelfSpan.Name = "LblShelfSpan"
        LblShelfSpan.Size = New Size(106, 18)
        LblShelfSpan.TabIndex = 2
        LblShelfSpan.Text = "Shelf Span: "
        ' 
        ' CmbShelfMaterial
        ' 
        CmbShelfMaterial.FormattingEnabled = True
        CmbShelfMaterial.Location = New Point(151, 28)
        CmbShelfMaterial.Name = "CmbShelfMaterial"
        CmbShelfMaterial.Size = New Size(244, 26)
        CmbShelfMaterial.TabIndex = 1
        ' 
        ' LblShelfMaterial
        ' 
        LblShelfMaterial.AutoSize = True
        LblShelfMaterial.Location = New Point(16, 32)
        LblShelfMaterial.Name = "LblShelfMaterial"
        LblShelfMaterial.Size = New Size(135, 18)
        LblShelfMaterial.TabIndex = 0
        LblShelfMaterial.Text = "Shelf Material: "
        ' 
        ' BtnCalculateShelf
        ' 
        BtnCalculateShelf.BackColor = Color.MistyRose
        BtnCalculateShelf.Location = New Point(131, 620)
        BtnCalculateShelf.Name = "BtnCalculateShelf"
        BtnCalculateShelf.Size = New Size(208, 40)
        BtnCalculateShelf.TabIndex = 12
        BtnCalculateShelf.Text = "Calculate Shelf Sag"
        BtnCalculateShelf.UseVisualStyleBackColor = False
        ' 
        ' GbxShelfSagResults
        ' 
        GbxShelfSagResults.BackColor = Color.WhiteSmoke
        GbxShelfSagResults.Controls.Add(PbShelfDiagram)
        GbxShelfSagResults.Controls.Add(LblShelfRecommendations)
        GbxShelfSagResults.Controls.Add(LblShelfWarning)
        GbxShelfSagResults.Controls.Add(LblShelfMaterialInfo)
        GbxShelfSagResults.Controls.Add(LblShelfMaxSpan)
        GbxShelfSagResults.Controls.Add(LblShelfSafetyStatus)
        GbxShelfSagResults.Controls.Add(LblShelfMaxLoad)
        GbxShelfSagResults.Controls.Add(LblShelfSafeLoad)
        GbxShelfSagResults.Controls.Add(LblShelfSagFraction)
        GbxShelfSagResults.Controls.Add(LblShelfSagInches)
        GbxShelfSagResults.Location = New Point(638, 10)
        GbxShelfSagResults.Name = "GbxShelfSagResults"
        GbxShelfSagResults.Size = New Size(434, 551)
        GbxShelfSagResults.TabIndex = 3
        GbxShelfSagResults.TabStop = False
        GbxShelfSagResults.Text = "Shelf Sag Results"
        ' 
        ' PbShelfDiagram
        ' 
        PbShelfDiagram.BorderStyle = BorderStyle.FixedSingle
        PbShelfDiagram.Location = New Point(11, 300)
        PbShelfDiagram.Name = "PbShelfDiagram"
        PbShelfDiagram.Size = New Size(413, 227)
        PbShelfDiagram.TabIndex = 10
        PbShelfDiagram.TabStop = False
        ' 
        ' LblShelfRecommendations
        ' 
        LblShelfRecommendations.Location = New Point(13, 235)
        LblShelfRecommendations.Name = "LblShelfRecommendations"
        LblShelfRecommendations.Size = New Size(401, 48)
        LblShelfRecommendations.TabIndex = 9
        LblShelfRecommendations.Text = "Reccomendations:"
        ' 
        ' LblShelfWarning
        ' 
        LblShelfWarning.Location = New Point(13, 177)
        LblShelfWarning.Name = "LblShelfWarning"
        LblShelfWarning.Size = New Size(401, 48)
        LblShelfWarning.TabIndex = 8
        LblShelfWarning.Text = "Warning: "
        ' 
        ' LblShelfMaterialInfo
        ' 
        LblShelfMaterialInfo.AutoSize = True
        LblShelfMaterialInfo.Location = New Point(13, 208)
        LblShelfMaterialInfo.Name = "LblShelfMaterialInfo"
        LblShelfMaterialInfo.Size = New Size(128, 18)
        LblShelfMaterialInfo.TabIndex = 7
        LblShelfMaterialInfo.Text = "Material Info: "
        ' 
        ' LblShelfMaxSpan
        ' 
        LblShelfMaxSpan.AutoSize = True
        LblShelfMaxSpan.Location = New Point(13, 153)
        LblShelfMaxSpan.Name = "LblShelfMaxSpan"
        LblShelfMaxSpan.Size = New Size(99, 18)
        LblShelfMaxSpan.TabIndex = 6
        LblShelfMaxSpan.Text = "Max Span: "
        ' 
        ' LblShelfSafetyStatus
        ' 
        LblShelfSafetyStatus.AutoSize = True
        LblShelfSafetyStatus.Location = New Point(13, 127)
        LblShelfSafetyStatus.Name = "LblShelfSafetyStatus"
        LblShelfSafetyStatus.Size = New Size(122, 18)
        LblShelfSafetyStatus.TabIndex = 5
        LblShelfSafetyStatus.Text = "Safety Status:"
        ' 
        ' LblShelfMaxLoad
        ' 
        LblShelfMaxLoad.AutoSize = True
        LblShelfMaxLoad.Location = New Point(13, 101)
        LblShelfMaxLoad.Name = "LblShelfMaxLoad"
        LblShelfMaxLoad.Size = New Size(95, 18)
        LblShelfMaxLoad.TabIndex = 4
        LblShelfMaxLoad.Text = "Max Load:"
        ' 
        ' LblShelfSafeLoad
        ' 
        LblShelfSafeLoad.AutoSize = True
        LblShelfSafeLoad.Location = New Point(13, 75)
        LblShelfSafeLoad.Name = "LblShelfSafeLoad"
        LblShelfSafeLoad.Size = New Size(99, 18)
        LblShelfSafeLoad.TabIndex = 3
        LblShelfSafeLoad.Text = "Safe Load: "
        ' 
        ' LblShelfSagFraction
        ' 
        LblShelfSagFraction.AutoSize = True
        LblShelfSagFraction.Location = New Point(13, 50)
        LblShelfSagFraction.Name = "LblShelfSagFraction"
        LblShelfSagFraction.Size = New Size(123, 18)
        LblShelfSagFraction.TabIndex = 2
        LblShelfSagFraction.Text = "Sag Fraction: "
        ' 
        ' LblShelfSagInches
        ' 
        LblShelfSagInches.AutoSize = True
        LblShelfSagInches.Location = New Point(13, 26)
        LblShelfSagInches.Name = "LblShelfSagInches"
        LblShelfSagInches.Size = New Size(107, 18)
        LblShelfSagInches.TabIndex = 0
        LblShelfSagInches.Tag = "Expected sag: {0:N2} in. ({1:N0} mm)"
        LblShelfSagInches.Text = "Sag Inches: "
        ' 
        ' TpCutList
        ' 
        TpCutList.BackColor = Color.Gainsboro
        TpCutList.BorderStyle = BorderStyle.Fixed3D
        TpCutList.Controls.Add(ScCutList)
        TpCutList.Location = New Point(4, 27)
        TpCutList.Name = "TpCutList"
        TpCutList.Size = New Size(1170, 823)
        TpCutList.TabIndex = 10
        TpCutList.Text = "Cut List"
        ' 
        ' ScCutList
        ' 
        ScCutList.BorderStyle = BorderStyle.Fixed3D
        ScCutList.Dock = DockStyle.Fill
        ScCutList.Location = New Point(0, 0)
        ScCutList.Name = "ScCutList"
        ' 
        ' ScCutList.Panel1
        ' 
        ScCutList.Panel1.Controls.Add(GbxCutListInput)
        ScCutList.Panel1.Controls.Add(PnlCutListOptions)
        ' 
        ' ScCutList.Panel2
        ' 
        ScCutList.Panel2.Controls.Add(GbCutListResults)
        ScCutList.Panel2.Controls.Add(GbCuttingDiagram)
        ScCutList.Size = New Size(1166, 819)
        ScCutList.SplitterDistance = 494
        ScCutList.SplitterWidth = 8
        ScCutList.TabIndex = 0
        ' 
        ' GbxCutListInput
        ' 
        GbxCutListInput.BackColor = Color.Silver
        GbxCutListInput.Controls.Add(DgvCutList)
        GbxCutListInput.Controls.Add(PnlCutListButtons)
        GbxCutListInput.Location = New Point(10, 10)
        GbxCutListInput.Name = "GbxCutListInput"
        GbxCutListInput.Size = New Size(454, 350)
        GbxCutListInput.TabIndex = 0
        GbxCutListInput.TabStop = False
        GbxCutListInput.Text = "Cut List Input"
        ' 
        ' DgvCutList
        ' 
        DgvCutList.ColumnHeadersHeight = 34
        DgvCutList.Dock = DockStyle.Fill
        DgvCutList.Location = New Point(3, 22)
        DgvCutList.Name = "DgvCutList"
        DgvCutList.RowHeadersVisible = False
        DgvCutList.RowHeadersWidth = 30
        DgvCutList.Size = New Size(448, 325)
        DgvCutList.TabIndex = 0
        ' 
        ' PnlCutListButtons
        ' 
        PnlCutListButtons.Controls.Add(BtnAddCutRow)
        PnlCutListButtons.Controls.Add(BtnDeleteCutRow)
        PnlCutListButtons.Location = New Point(10, 310)
        PnlCutListButtons.Name = "PnlCutListButtons"
        PnlCutListButtons.Size = New Size(400, 35)
        PnlCutListButtons.TabIndex = 1
        ' 
        ' BtnAddCutRow
        ' 
        BtnAddCutRow.Location = New Point(5, 5)
        BtnAddCutRow.Name = "BtnAddCutRow"
        BtnAddCutRow.Size = New Size(100, 28)
        BtnAddCutRow.TabIndex = 0
        BtnAddCutRow.Text = "Add Row"
        BtnAddCutRow.UseVisualStyleBackColor = True
        ' 
        ' BtnDeleteCutRow
        ' 
        BtnDeleteCutRow.Location = New Point(115, 5)
        BtnDeleteCutRow.Name = "BtnDeleteCutRow"
        BtnDeleteCutRow.Size = New Size(100, 28)
        BtnDeleteCutRow.TabIndex = 1
        BtnDeleteCutRow.Text = "Delete Row"
        BtnDeleteCutRow.UseVisualStyleBackColor = True
        ' 
        ' PnlCutListOptions
        ' 
        PnlCutListOptions.BackColor = Color.Silver
        PnlCutListOptions.BorderStyle = BorderStyle.FixedSingle
        PnlCutListOptions.Controls.Add(LblStockBoard)
        PnlCutListOptions.Controls.Add(CmbStockBoard)
        PnlCutListOptions.Controls.Add(LblKerf)
        PnlCutListOptions.Controls.Add(TxtKerf)
        PnlCutListOptions.Controls.Add(BtnOptimize)
        PnlCutListOptions.Controls.Add(BtnExportCutList)
        PnlCutListOptions.Location = New Point(10, 370)
        PnlCutListOptions.Name = "PnlCutListOptions"
        PnlCutListOptions.Size = New Size(420, 132)
        PnlCutListOptions.TabIndex = 1
        ' 
        ' LblStockBoard
        ' 
        LblStockBoard.AutoSize = True
        LblStockBoard.Location = New Point(5, 15)
        LblStockBoard.Name = "LblStockBoard"
        LblStockBoard.Size = New Size(60, 18)
        LblStockBoard.TabIndex = 0
        LblStockBoard.Text = "Stock:"
        ' 
        ' CmbStockBoard
        ' 
        CmbStockBoard.DropDownStyle = ComboBoxStyle.DropDownList
        CmbStockBoard.Location = New Point(65, 11)
        CmbStockBoard.Name = "CmbStockBoard"
        CmbStockBoard.Size = New Size(150, 26)
        CmbStockBoard.TabIndex = 1
        ' 
        ' LblKerf
        ' 
        LblKerf.AutoSize = True
        LblKerf.Location = New Point(15, 52)
        LblKerf.Name = "LblKerf"
        LblKerf.Size = New Size(49, 18)
        LblKerf.TabIndex = 2
        LblKerf.Text = "Kerf:"
        ' 
        ' TxtKerf
        ' 
        TxtKerf.Location = New Point(64, 48)
        TxtKerf.Name = "TxtKerf"
        TxtKerf.Size = New Size(60, 26)
        TxtKerf.TabIndex = 3
        TxtKerf.Text = "0.125"
        ' 
        ' BtnOptimize
        ' 
        BtnOptimize.Location = New Point(22, 92)
        BtnOptimize.Name = "BtnOptimize"
        BtnOptimize.Size = New Size(155, 28)
        BtnOptimize.TabIndex = 4
        BtnOptimize.Text = "Optimize"
        BtnOptimize.UseVisualStyleBackColor = True
        ' 
        ' BtnExportCutList
        ' 
        BtnExportCutList.Location = New Point(244, 92)
        BtnExportCutList.Name = "BtnExportCutList"
        BtnExportCutList.Size = New Size(155, 28)
        BtnExportCutList.TabIndex = 5
        BtnExportCutList.Text = "Export"
        BtnExportCutList.UseVisualStyleBackColor = True
        ' 
        ' GbCutListResults
        ' 
        GbCutListResults.BackColor = Color.WhiteSmoke
        GbCutListResults.Controls.Add(LblBoardsNeeded)
        GbCutListResults.Controls.Add(LblTotalCost)
        GbCutListResults.Controls.Add(LblWastePercent)
        GbCutListResults.Controls.Add(LblAvgEfficiency)
        GbCutListResults.Location = New Point(10, 10)
        GbCutListResults.Name = "GbCutListResults"
        GbCutListResults.Size = New Size(350, 150)
        GbCutListResults.TabIndex = 0
        GbCutListResults.TabStop = False
        GbCutListResults.Text = "Optimization Results"
        ' 
        ' LblBoardsNeeded
        ' 
        LblBoardsNeeded.AutoSize = True
        LblBoardsNeeded.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        LblBoardsNeeded.Location = New Point(15, 30)
        LblBoardsNeeded.Name = "LblBoardsNeeded"
        LblBoardsNeeded.Size = New Size(183, 28)
        LblBoardsNeeded.TabIndex = 0
        LblBoardsNeeded.Text = "Boards Needed: --"
        ' 
        ' LblTotalCost
        ' 
        LblTotalCost.AutoSize = True
        LblTotalCost.Location = New Point(15, 60)
        LblTotalCost.Name = "LblTotalCost"
        LblTotalCost.Size = New Size(117, 18)
        LblTotalCost.TabIndex = 1
        LblTotalCost.Text = "Total Cost: --"
        ' 
        ' LblWastePercent
        ' 
        LblWastePercent.AutoSize = True
        LblWastePercent.Location = New Point(15, 90)
        LblWastePercent.Name = "LblWastePercent"
        LblWastePercent.Size = New Size(103, 18)
        LblWastePercent.TabIndex = 2
        LblWastePercent.Text = "Waste %: --"
        ' 
        ' LblAvgEfficiency
        ' 
        LblAvgEfficiency.AutoSize = True
        LblAvgEfficiency.Location = New Point(15, 120)
        LblAvgEfficiency.Name = "LblAvgEfficiency"
        LblAvgEfficiency.Size = New Size(114, 18)
        LblAvgEfficiency.TabIndex = 3
        LblAvgEfficiency.Text = "Efficiency: --"
        ' 
        ' GbCuttingDiagram
        ' 
        GbCuttingDiagram.BackColor = Color.WhiteSmoke
        GbCuttingDiagram.Controls.Add(PbCuttingDiagram)
        GbCuttingDiagram.Controls.Add(PnlDiagramNav)
        GbCuttingDiagram.Location = New Point(10, 170)
        GbCuttingDiagram.Name = "GbCuttingDiagram"
        GbCuttingDiagram.Size = New Size(626, 494)
        GbCuttingDiagram.TabIndex = 1
        GbCuttingDiagram.TabStop = False
        GbCuttingDiagram.Text = "Cutting Diagram"
        ' 
        ' PbCuttingDiagram
        ' 
        PbCuttingDiagram.BorderStyle = BorderStyle.FixedSingle
        PbCuttingDiagram.Location = New Point(13, 25)
        PbCuttingDiagram.Name = "PbCuttingDiagram"
        PbCuttingDiagram.Size = New Size(600, 400)
        PbCuttingDiagram.TabIndex = 0
        PbCuttingDiagram.TabStop = False
        ' 
        ' PnlDiagramNav
        ' 
        PnlDiagramNav.Controls.Add(BtnPrevPattern)
        PnlDiagramNav.Controls.Add(LblPatternInfo)
        PnlDiagramNav.Controls.Add(BtnNextPattern)
        PnlDiagramNav.Location = New Point(73, 444)
        PnlDiagramNav.Name = "PnlDiagramNav"
        PnlDiagramNav.Size = New Size(480, 35)
        PnlDiagramNav.TabIndex = 1
        ' 
        ' BtnPrevPattern
        ' 
        BtnPrevPattern.Location = New Point(5, 5)
        BtnPrevPattern.Name = "BtnPrevPattern"
        BtnPrevPattern.Size = New Size(80, 28)
        BtnPrevPattern.TabIndex = 0
        BtnPrevPattern.Text = "< Prev"
        BtnPrevPattern.UseVisualStyleBackColor = True
        ' 
        ' LblPatternInfo
        ' 
        LblPatternInfo.Location = New Point(100, 10)
        LblPatternInfo.Name = "LblPatternInfo"
        LblPatternInfo.Size = New Size(280, 23)
        LblPatternInfo.TabIndex = 1
        LblPatternInfo.Text = "Board 0 of 0"
        LblPatternInfo.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' BtnNextPattern
        ' 
        BtnNextPattern.Location = New Point(395, 5)
        BtnNextPattern.Name = "BtnNextPattern"
        BtnNextPattern.Size = New Size(80, 28)
        BtnNextPattern.TabIndex = 2
        BtnNextPattern.Text = "Next >"
        BtnNextPattern.UseVisualStyleBackColor = True
        ' 
        ' TpReferences
        ' 
        TpReferences.BackColor = Color.Gainsboro
        TpReferences.BorderStyle = BorderStyle.Fixed3D
        TpReferences.Controls.Add(TcReferences)
        TpReferences.Location = New Point(4, 27)
        TpReferences.Name = "TpReferences"
        TpReferences.Size = New Size(1170, 823)
        TpReferences.TabIndex = 12
        TpReferences.Text = "References"
        ' 
        ' TcReferences
        ' 
        TcReferences.Alignment = TabAlignment.Right
        TcReferences.Controls.Add(TpWoodProperties)
        TcReferences.Controls.Add(TpJoineryReference)
        TcReferences.Controls.Add(TpHardwareStandards)
        TcReferences.Dock = DockStyle.Fill
        TcReferences.Location = New Point(0, 0)
        TcReferences.Multiline = True
        TcReferences.Name = "TcReferences"
        TcReferences.SelectedIndex = 0
        TcReferences.Size = New Size(1166, 819)
        TcReferences.TabIndex = 0
        ' 
        ' TpWoodProperties
        ' 
        TpWoodProperties.Controls.Add(PnlWoodDetails)
        TpWoodProperties.Controls.Add(PnlWoodProperties)
        TpWoodProperties.Controls.Add(DgvWoodProperties)
        TpWoodProperties.Location = New Point(4, 4)
        TpWoodProperties.Name = "TpWoodProperties"
        TpWoodProperties.Padding = New Padding(3)
        TpWoodProperties.Size = New Size(1134, 811)
        TpWoodProperties.TabIndex = 0
        TpWoodProperties.Text = "Wood Properties"
        TpWoodProperties.UseVisualStyleBackColor = True
        ' 
        ' PnlWoodDetails
        ' 
        PnlWoodDetails.BackColor = Color.LightGray
        PnlWoodDetails.BorderStyle = BorderStyle.Fixed3D
        PnlWoodDetails.Controls.Add(BtnAddWoodSpecies)
        PnlWoodDetails.Controls.Add(BtnPrintWoodData)
        PnlWoodDetails.Controls.Add(BtnExportWoodData)
        PnlWoodDetails.Controls.Add(BtnCompareWoods)
        PnlWoodDetails.Controls.Add(RtbWoodDetails)
        PnlWoodDetails.Controls.Add(LblWoodDetailsHeader)
        PnlWoodDetails.Dock = DockStyle.Bottom
        PnlWoodDetails.Location = New Point(3, 429)
        PnlWoodDetails.Name = "PnlWoodDetails"
        PnlWoodDetails.Size = New Size(1128, 379)
        PnlWoodDetails.TabIndex = 2
        ' 
        ' BtnAddWoodSpecies
        ' 
        BtnAddWoodSpecies.BackColor = Color.MistyRose
        BtnAddWoodSpecies.Location = New Point(858, 323)
        BtnAddWoodSpecies.Name = "BtnAddWoodSpecies"
        BtnAddWoodSpecies.Size = New Size(198, 34)
        BtnAddWoodSpecies.TabIndex = 5
        BtnAddWoodSpecies.Text = "Add Species"
        BtnAddWoodSpecies.UseVisualStyleBackColor = False
        ' 
        ' BtnPrintWoodData
        ' 
        BtnPrintWoodData.BackColor = Color.MistyRose
        BtnPrintWoodData.Location = New Point(595, 323)
        BtnPrintWoodData.Name = "BtnPrintWoodData"
        BtnPrintWoodData.Size = New Size(198, 34)
        BtnPrintWoodData.TabIndex = 4
        BtnPrintWoodData.Text = "Print"
        BtnPrintWoodData.UseVisualStyleBackColor = False
        ' 
        ' BtnExportWoodData
        ' 
        BtnExportWoodData.BackColor = Color.MistyRose
        BtnExportWoodData.Location = New Point(332, 323)
        BtnExportWoodData.Name = "BtnExportWoodData"
        BtnExportWoodData.Size = New Size(198, 34)
        BtnExportWoodData.TabIndex = 3
        BtnExportWoodData.Text = "Export to CSV"
        BtnExportWoodData.UseVisualStyleBackColor = False
        ' 
        ' BtnCompareWoods
        ' 
        BtnCompareWoods.BackColor = Color.MistyRose
        BtnCompareWoods.Location = New Point(69, 323)
        BtnCompareWoods.Name = "BtnCompareWoods"
        BtnCompareWoods.Size = New Size(198, 34)
        BtnCompareWoods.TabIndex = 2
        BtnCompareWoods.Text = "Compare Selected"
        BtnCompareWoods.UseVisualStyleBackColor = False
        ' 
        ' RtbWoodDetails
        ' 
        RtbWoodDetails.Location = New Point(24, 60)
        RtbWoodDetails.Name = "RtbWoodDetails"
        RtbWoodDetails.Size = New Size(1058, 251)
        RtbWoodDetails.TabIndex = 1
        RtbWoodDetails.Text = ""
        ' 
        ' LblWoodDetailsHeader
        ' 
        LblWoodDetailsHeader.AutoSize = True
        LblWoodDetailsHeader.Font = New Font("Georgia", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblWoodDetailsHeader.Location = New Point(16, 10)
        LblWoodDetailsHeader.Name = "LblWoodDetailsHeader"
        LblWoodDetailsHeader.Size = New Size(130, 24)
        LblWoodDetailsHeader.TabIndex = 0
        LblWoodDetailsHeader.Text = "Details for: "
        ' 
        ' PnlWoodProperties
        ' 
        PnlWoodProperties.BackColor = Color.LightGray
        PnlWoodProperties.BorderStyle = BorderStyle.Fixed3D
        PnlWoodProperties.Controls.Add(LblWoodSearch)
        PnlWoodProperties.Controls.Add(BtnWoodClearSearch)
        PnlWoodProperties.Controls.Add(TxtWoodSearch)
        PnlWoodProperties.Controls.Add(RbWoodSoftwoods)
        PnlWoodProperties.Controls.Add(RbWoodHardwoods)
        PnlWoodProperties.Controls.Add(RbWoodAll)
        PnlWoodProperties.Controls.Add(LblWoodPropertiesReference)
        PnlWoodProperties.Dock = DockStyle.Top
        PnlWoodProperties.Location = New Point(3, 3)
        PnlWoodProperties.Name = "PnlWoodProperties"
        PnlWoodProperties.Size = New Size(1128, 112)
        PnlWoodProperties.TabIndex = 1
        ' 
        ' LblWoodSearch
        ' 
        LblWoodSearch.AutoSize = True
        LblWoodSearch.Location = New Point(604, 59)
        LblWoodSearch.Name = "LblWoodSearch"
        LblWoodSearch.Size = New Size(75, 18)
        LblWoodSearch.TabIndex = 6
        LblWoodSearch.Text = "Search: "
        ' 
        ' BtnWoodClearSearch
        ' 
        BtnWoodClearSearch.BackColor = Color.MistyRose
        BtnWoodClearSearch.Location = New Point(952, 51)
        BtnWoodClearSearch.Name = "BtnWoodClearSearch"
        BtnWoodClearSearch.Size = New Size(130, 34)
        BtnWoodClearSearch.TabIndex = 5
        BtnWoodClearSearch.Text = "Clear Search"
        BtnWoodClearSearch.UseVisualStyleBackColor = False
        ' 
        ' TxtWoodSearch
        ' 
        TxtWoodSearch.Location = New Point(679, 55)
        TxtWoodSearch.Name = "TxtWoodSearch"
        TxtWoodSearch.Size = New Size(216, 26)
        TxtWoodSearch.TabIndex = 4
        ' 
        ' RbWoodSoftwoods
        ' 
        RbWoodSoftwoods.AutoSize = True
        RbWoodSoftwoods.Location = New Point(305, 57)
        RbWoodSoftwoods.Name = "RbWoodSoftwoods"
        RbWoodSoftwoods.Size = New Size(118, 22)
        RbWoodSoftwoods.TabIndex = 3
        RbWoodSoftwoods.TabStop = True
        RbWoodSoftwoods.Text = "Softwoods"
        RbWoodSoftwoods.UseVisualStyleBackColor = True
        ' 
        ' RbWoodHardwoods
        ' 
        RbWoodHardwoods.AutoSize = True
        RbWoodHardwoods.Location = New Point(126, 57)
        RbWoodHardwoods.Name = "RbWoodHardwoods"
        RbWoodHardwoods.Size = New Size(128, 22)
        RbWoodHardwoods.TabIndex = 2
        RbWoodHardwoods.TabStop = True
        RbWoodHardwoods.Text = "Hardwoods"
        RbWoodHardwoods.UseVisualStyleBackColor = True
        ' 
        ' RbWoodAll
        ' 
        RbWoodAll.AutoSize = True
        RbWoodAll.Location = New Point(18, 57)
        RbWoodAll.Name = "RbWoodAll"
        RbWoodAll.Size = New Size(57, 22)
        RbWoodAll.TabIndex = 1
        RbWoodAll.TabStop = True
        RbWoodAll.Text = "All"
        RbWoodAll.UseVisualStyleBackColor = True
        ' 
        ' LblWoodPropertiesReference
        ' 
        LblWoodPropertiesReference.AutoSize = True
        LblWoodPropertiesReference.Font = New Font("Georgia", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblWoodPropertiesReference.Location = New Point(17, 9)
        LblWoodPropertiesReference.Name = "LblWoodPropertiesReference"
        LblWoodPropertiesReference.Size = New Size(354, 29)
        LblWoodPropertiesReference.TabIndex = 0
        LblWoodPropertiesReference.Text = "Wood Properties Reference"
        ' 
        ' DgvWoodProperties
        ' 
        DgvWoodProperties.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvWoodProperties.Location = New Point(4, 114)
        DgvWoodProperties.Name = "DgvWoodProperties"
        DgvWoodProperties.RowHeadersWidth = 62
        DgvWoodProperties.Size = New Size(1122, 310)
        DgvWoodProperties.TabIndex = 0
        ' 
        ' TpJoineryReference
        ' 
        TpJoineryReference.BackColor = Color.Gainsboro
        TpJoineryReference.Controls.Add(Panel11)
        TpJoineryReference.Controls.Add(Panel1)
        TpJoineryReference.Controls.Add(GbxJoineryFilter)
        TpJoineryReference.Controls.Add(DgvJoineryTypes)
        TpJoineryReference.Location = New Point(4, 4)
        TpJoineryReference.Name = "TpJoineryReference"
        TpJoineryReference.Padding = New Padding(3)
        TpJoineryReference.Size = New Size(1128, 811)
        TpJoineryReference.TabIndex = 1
        TpJoineryReference.Text = "Joinery Types"
        ' 
        ' Panel11
        ' 
        Panel11.BackColor = Color.Silver
        Panel11.BorderStyle = BorderStyle.FixedSingle
        Panel11.Controls.Add(Label67)
        Panel11.Controls.Add(TxtJoineryHistory)
        Panel11.Controls.Add(Label66)
        Panel11.Controls.Add(Label65)
        Panel11.Controls.Add(Label64)
        Panel11.Controls.Add(Label63)
        Panel11.Controls.Add(TxtJoineryReinforcement)
        Panel11.Controls.Add(TxtJoineryStrengthChar)
        Panel11.Controls.Add(TxtJoineryTools)
        Panel11.Controls.Add(TxtJoineryUses)
        Panel11.Controls.Add(TxtJoineryDescription)
        Panel11.Controls.Add(Label62)
        Panel11.Location = New Point(509, 324)
        Panel11.Name = "Panel11"
        Panel11.Size = New Size(609, 481)
        Panel11.TabIndex = 3
        ' 
        ' Label67
        ' 
        Label67.AutoSize = True
        Label67.Font = New Font("Georgia", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label67.Location = New Point(20, 404)
        Label67.Name = "Label67"
        Label67.Size = New Size(181, 18)
        Label67.TabIndex = 12
        Label67.Text = "**Historical Notes:**"
        ' 
        ' TxtJoineryHistory
        ' 
        TxtJoineryHistory.Location = New Point(20, 430)
        TxtJoineryHistory.Multiline = True
        TxtJoineryHistory.Name = "TxtJoineryHistory"
        TxtJoineryHistory.ReadOnly = True
        TxtJoineryHistory.ScrollBars = ScrollBars.Both
        TxtJoineryHistory.Size = New Size(570, 46)
        TxtJoineryHistory.TabIndex = 11
        ' 
        ' Label66
        ' 
        Label66.AutoSize = True
        Label66.Font = New Font("Georgia", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label66.Location = New Point(20, 322)
        Label66.Name = "Label66"
        Label66.Size = New Size(239, 18)
        Label66.TabIndex = 10
        Label66.Text = "**Reinforcement Options:**"
        ' 
        ' Label65
        ' 
        Label65.AutoSize = True
        Label65.Font = New Font("Georgia", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label65.Location = New Point(20, 242)
        Label65.Name = "Label65"
        Label65.Size = New Size(249, 18)
        Label65.TabIndex = 9
        Label65.Text = "**Strength Characteristics:**"
        ' 
        ' Label64
        ' 
        Label64.AutoSize = True
        Label64.Font = New Font("Georgia", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label64.Location = New Point(20, 162)
        Label64.Name = "Label64"
        Label64.Size = New Size(172, 18)
        Label64.TabIndex = 8
        Label64.Text = "**Required Tools:**"
        ' 
        ' Label63
        ' 
        Label63.AutoSize = True
        Label63.Font = New Font("Georgia", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label63.Location = New Point(20, 82)
        Label63.Name = "Label63"
        Label63.Size = New Size(149, 18)
        Label63.TabIndex = 7
        Label63.Text = "**Typical uses:**"
        ' 
        ' TxtJoineryReinforcement
        ' 
        TxtJoineryReinforcement.Location = New Point(20, 348)
        TxtJoineryReinforcement.Multiline = True
        TxtJoineryReinforcement.Name = "TxtJoineryReinforcement"
        TxtJoineryReinforcement.ReadOnly = True
        TxtJoineryReinforcement.ScrollBars = ScrollBars.Both
        TxtJoineryReinforcement.Size = New Size(570, 46)
        TxtJoineryReinforcement.TabIndex = 6
        ' 
        ' TxtJoineryStrengthChar
        ' 
        TxtJoineryStrengthChar.Location = New Point(20, 268)
        TxtJoineryStrengthChar.Multiline = True
        TxtJoineryStrengthChar.Name = "TxtJoineryStrengthChar"
        TxtJoineryStrengthChar.ReadOnly = True
        TxtJoineryStrengthChar.ScrollBars = ScrollBars.Both
        TxtJoineryStrengthChar.Size = New Size(570, 46)
        TxtJoineryStrengthChar.TabIndex = 5
        ' 
        ' TxtJoineryTools
        ' 
        TxtJoineryTools.Location = New Point(20, 188)
        TxtJoineryTools.Multiline = True
        TxtJoineryTools.Name = "TxtJoineryTools"
        TxtJoineryTools.ReadOnly = True
        TxtJoineryTools.ScrollBars = ScrollBars.Both
        TxtJoineryTools.Size = New Size(570, 46)
        TxtJoineryTools.TabIndex = 4
        ' 
        ' TxtJoineryUses
        ' 
        TxtJoineryUses.Location = New Point(20, 108)
        TxtJoineryUses.Multiline = True
        TxtJoineryUses.Name = "TxtJoineryUses"
        TxtJoineryUses.ReadOnly = True
        TxtJoineryUses.ScrollBars = ScrollBars.Both
        TxtJoineryUses.Size = New Size(570, 46)
        TxtJoineryUses.TabIndex = 3
        ' 
        ' TxtJoineryDescription
        ' 
        TxtJoineryDescription.Location = New Point(20, 28)
        TxtJoineryDescription.Multiline = True
        TxtJoineryDescription.Name = "TxtJoineryDescription"
        TxtJoineryDescription.ReadOnly = True
        TxtJoineryDescription.ScrollBars = ScrollBars.Both
        TxtJoineryDescription.Size = New Size(570, 46)
        TxtJoineryDescription.TabIndex = 2
        ' 
        ' Label62
        ' 
        Label62.AutoSize = True
        Label62.Font = New Font("Georgia", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label62.Location = New Point(20, 2)
        Label62.Name = "Label62"
        Label62.Size = New Size(143, 18)
        Label62.TabIndex = 1
        Label62.Text = "**Description:**"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Silver
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(LblJoineryGlue)
        Panel1.Controls.Add(LblJoineryStrength)
        Panel1.Controls.Add(LblJoineryDifficulty)
        Panel1.Controls.Add(LblJoineryCategory)
        Panel1.Controls.Add(LblJoineryName)
        Panel1.Controls.Add(LblSummary)
        Panel1.Location = New Point(509, 102)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(609, 687)
        Panel1.TabIndex = 2
        ' 
        ' LblJoineryGlue
        ' 
        LblJoineryGlue.AutoSize = True
        LblJoineryGlue.Location = New Point(25, 183)
        LblJoineryGlue.Name = "LblJoineryGlue"
        LblJoineryGlue.Size = New Size(137, 18)
        LblJoineryGlue.TabIndex = 5
        LblJoineryGlue.Text = "Glue Required: "
        ' 
        ' LblJoineryStrength
        ' 
        LblJoineryStrength.AutoSize = True
        LblJoineryStrength.Location = New Point(25, 148)
        LblJoineryStrength.Name = "LblJoineryStrength"
        LblJoineryStrength.Size = New Size(90, 18)
        LblJoineryStrength.TabIndex = 4
        LblJoineryStrength.Text = "Strength: "
        ' 
        ' LblJoineryDifficulty
        ' 
        LblJoineryDifficulty.AutoSize = True
        LblJoineryDifficulty.Location = New Point(25, 113)
        LblJoineryDifficulty.Name = "LblJoineryDifficulty"
        LblJoineryDifficulty.Size = New Size(96, 18)
        LblJoineryDifficulty.TabIndex = 3
        LblJoineryDifficulty.Text = "Difficulty: "
        ' 
        ' LblJoineryCategory
        ' 
        LblJoineryCategory.AutoSize = True
        LblJoineryCategory.Location = New Point(25, 78)
        LblJoineryCategory.Name = "LblJoineryCategory"
        LblJoineryCategory.Size = New Size(91, 18)
        LblJoineryCategory.TabIndex = 2
        LblJoineryCategory.Text = "Category: "
        ' 
        ' LblJoineryName
        ' 
        LblJoineryName.AutoSize = True
        LblJoineryName.Location = New Point(25, 43)
        LblJoineryName.Name = "LblJoineryName"
        LblJoineryName.Size = New Size(66, 18)
        LblJoineryName.TabIndex = 1
        LblJoineryName.Text = "Name: "
        ' 
        ' LblSummary
        ' 
        LblSummary.AutoSize = True
        LblSummary.Font = New Font("Georgia", 10F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblSummary.Location = New Point(18, 7)
        LblSummary.Name = "LblSummary"
        LblSummary.Size = New Size(157, 24)
        LblSummary.TabIndex = 0
        LblSummary.Text = "**Summary:**"
        ' 
        ' GbxJoineryFilter
        ' 
        GbxJoineryFilter.BackColor = Color.Silver
        GbxJoineryFilter.Controls.Add(LblJoineryCount)
        GbxJoineryFilter.Controls.Add(RbJoineryBeginner)
        GbxJoineryFilter.Controls.Add(RbJoineryEdge)
        GbxJoineryFilter.Controls.Add(RbJoineryBox)
        GbxJoineryFilter.Controls.Add(RbJoineryFrame)
        GbxJoineryFilter.Controls.Add(RbJoineryAll)
        GbxJoineryFilter.Location = New Point(509, 6)
        GbxJoineryFilter.Name = "GbxJoineryFilter"
        GbxJoineryFilter.Size = New Size(609, 96)
        GbxJoineryFilter.TabIndex = 1
        GbxJoineryFilter.TabStop = False
        GbxJoineryFilter.Text = "Joinery Filter"
        ' 
        ' LblJoineryCount
        ' 
        LblJoineryCount.AutoSize = True
        LblJoineryCount.Location = New Point(18, 65)
        LblJoineryCount.Name = "LblJoineryCount"
        LblJoineryCount.Size = New Size(128, 18)
        LblJoineryCount.TabIndex = 5
        LblJoineryCount.Text = "0 joinery types"
        ' 
        ' RbJoineryBeginner
        ' 
        RbJoineryBeginner.AutoSize = True
        RbJoineryBeginner.Location = New Point(408, 27)
        RbJoineryBeginner.Name = "RbJoineryBeginner"
        RbJoineryBeginner.Size = New Size(182, 22)
        RbJoineryBeginner.TabIndex = 4
        RbJoineryBeginner.TabStop = True
        RbJoineryBeginner.Text = "Beginner Friendly"
        RbJoineryBeginner.UseVisualStyleBackColor = True
        ' 
        ' RbJoineryEdge
        ' 
        RbJoineryEdge.AutoSize = True
        RbJoineryEdge.Location = New Point(321, 27)
        RbJoineryEdge.Name = "RbJoineryEdge"
        RbJoineryEdge.Size = New Size(73, 22)
        RbJoineryEdge.TabIndex = 3
        RbJoineryEdge.TabStop = True
        RbJoineryEdge.Text = "Edge"
        RbJoineryEdge.UseVisualStyleBackColor = True
        ' 
        ' RbJoineryBox
        ' 
        RbJoineryBox.AutoSize = True
        RbJoineryBox.Location = New Point(243, 27)
        RbJoineryBox.Name = "RbJoineryBox"
        RbJoineryBox.Size = New Size(64, 22)
        RbJoineryBox.TabIndex = 2
        RbJoineryBox.TabStop = True
        RbJoineryBox.Text = "Box"
        RbJoineryBox.UseVisualStyleBackColor = True
        ' 
        ' RbJoineryFrame
        ' 
        RbJoineryFrame.AutoSize = True
        RbJoineryFrame.Location = New Point(142, 27)
        RbJoineryFrame.Name = "RbJoineryFrame"
        RbJoineryFrame.Size = New Size(87, 22)
        RbJoineryFrame.TabIndex = 1
        RbJoineryFrame.TabStop = True
        RbJoineryFrame.Text = "Frame"
        RbJoineryFrame.UseVisualStyleBackColor = True
        ' 
        ' RbJoineryAll
        ' 
        RbJoineryAll.AutoSize = True
        RbJoineryAll.Location = New Point(18, 27)
        RbJoineryAll.Name = "RbJoineryAll"
        RbJoineryAll.Size = New Size(110, 22)
        RbJoineryAll.TabIndex = 0
        RbJoineryAll.TabStop = True
        RbJoineryAll.Text = "All Types"
        RbJoineryAll.UseVisualStyleBackColor = True
        ' 
        ' DgvJoineryTypes
        ' 
        DgvJoineryTypes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvJoineryTypes.Dock = DockStyle.Left
        DgvJoineryTypes.Location = New Point(3, 3)
        DgvJoineryTypes.Name = "DgvJoineryTypes"
        DgvJoineryTypes.RowHeadersWidth = 62
        DgvJoineryTypes.Size = New Size(500, 805)
        DgvJoineryTypes.TabIndex = 0
        ' 
        ' TpHardwareStandards
        ' 
        TpHardwareStandards.BorderStyle = BorderStyle.Fixed3D
        TpHardwareStandards.Controls.Add(GbxHardwareFilter)
        TpHardwareStandards.Controls.Add(ScHardwareMain)
        TpHardwareStandards.Location = New Point(4, 4)
        TpHardwareStandards.Name = "TpHardwareStandards"
        TpHardwareStandards.Size = New Size(1128, 811)
        TpHardwareStandards.TabIndex = 2
        TpHardwareStandards.Text = "Hardware Standards"
        TpHardwareStandards.UseVisualStyleBackColor = True
        ' 
        ' GbxHardwareFilter
        ' 
        GbxHardwareFilter.BackColor = Color.DarkGray
        GbxHardwareFilter.Controls.Add(Label73)
        GbxHardwareFilter.Controls.Add(RbHardwareFasteners)
        GbxHardwareFilter.Controls.Add(RbHardwareSlides)
        GbxHardwareFilter.Controls.Add(LblHardwareCount)
        GbxHardwareFilter.Controls.Add(RbHardwareHinges)
        GbxHardwareFilter.Controls.Add(RbHardwareAll)
        GbxHardwareFilter.Controls.Add(RbHardwareShelf)
        GbxHardwareFilter.Dock = DockStyle.Top
        GbxHardwareFilter.Location = New Point(0, 0)
        GbxHardwareFilter.Name = "GbxHardwareFilter"
        GbxHardwareFilter.Size = New Size(1124, 88)
        GbxHardwareFilter.TabIndex = 1
        GbxHardwareFilter.TabStop = False
        GbxHardwareFilter.Text = "Filter by Category"
        ' 
        ' Label73
        ' 
        Label73.AutoSize = True
        Label73.Location = New Point(27, 28)
        Label73.Name = "Label73"
        Label73.Size = New Size(65, 18)
        Label73.TabIndex = 6
        Label73.Text = "Filter: "
        ' 
        ' RbHardwareFasteners
        ' 
        RbHardwareFasteners.AutoSize = True
        RbHardwareFasteners.Location = New Point(619, 26)
        RbHardwareFasteners.Name = "RbHardwareFasteners"
        RbHardwareFasteners.Size = New Size(114, 22)
        RbHardwareFasteners.TabIndex = 4
        RbHardwareFasteners.TabStop = True
        RbHardwareFasteners.Text = "Fasteners"
        RbHardwareFasteners.UseVisualStyleBackColor = True
        ' 
        ' RbHardwareSlides
        ' 
        RbHardwareSlides.AutoSize = True
        RbHardwareSlides.Location = New Point(341, 26)
        RbHardwareSlides.Name = "RbHardwareSlides"
        RbHardwareSlides.Size = New Size(83, 22)
        RbHardwareSlides.TabIndex = 2
        RbHardwareSlides.TabStop = True
        RbHardwareSlides.Text = "Slides"
        RbHardwareSlides.UseVisualStyleBackColor = True
        ' 
        ' LblHardwareCount
        ' 
        LblHardwareCount.AutoSize = True
        LblHardwareCount.Location = New Point(27, 59)
        LblHardwareCount.Name = "LblHardwareCount"
        LblHardwareCount.Size = New Size(68, 18)
        LblHardwareCount.TabIndex = 5
        LblHardwareCount.Text = "Count: "
        ' 
        ' RbHardwareHinges
        ' 
        RbHardwareHinges.AutoSize = True
        RbHardwareHinges.Location = New Point(227, 26)
        RbHardwareHinges.Name = "RbHardwareHinges"
        RbHardwareHinges.Size = New Size(90, 22)
        RbHardwareHinges.TabIndex = 1
        RbHardwareHinges.TabStop = True
        RbHardwareHinges.Text = "Hinges"
        RbHardwareHinges.UseVisualStyleBackColor = True
        ' 
        ' RbHardwareAll
        ' 
        RbHardwareAll.AutoSize = True
        RbHardwareAll.Location = New Point(98, 26)
        RbHardwareAll.Name = "RbHardwareAll"
        RbHardwareAll.Size = New Size(105, 22)
        RbHardwareAll.TabIndex = 0
        RbHardwareAll.TabStop = True
        RbHardwareAll.Text = "All types"
        RbHardwareAll.UseVisualStyleBackColor = True
        ' 
        ' RbHardwareShelf
        ' 
        RbHardwareShelf.AutoSize = True
        RbHardwareShelf.Location = New Point(448, 26)
        RbHardwareShelf.Name = "RbHardwareShelf"
        RbHardwareShelf.Size = New Size(147, 22)
        RbHardwareShelf.TabIndex = 3
        RbHardwareShelf.TabStop = True
        RbHardwareShelf.Text = "Shelf Support"
        RbHardwareShelf.UseVisualStyleBackColor = True
        ' 
        ' ScHardwareMain
        ' 
        ScHardwareMain.BorderStyle = BorderStyle.Fixed3D
        ScHardwareMain.Dock = DockStyle.Bottom
        ScHardwareMain.Location = New Point(0, 94)
        ScHardwareMain.Name = "ScHardwareMain"
        ' 
        ' ScHardwareMain.Panel1
        ' 
        ScHardwareMain.Panel1.Controls.Add(DgvHardware)
        ' 
        ' ScHardwareMain.Panel2
        ' 
        ScHardwareMain.Panel2.BackColor = Color.Gainsboro
        ScHardwareMain.Panel2.Controls.Add(Label72)
        ScHardwareMain.Panel2.Controls.Add(Label71)
        ScHardwareMain.Panel2.Controls.Add(Label70)
        ScHardwareMain.Panel2.Controls.Add(Label69)
        ScHardwareMain.Panel2.Controls.Add(Label68)
        ScHardwareMain.Panel2.Controls.Add(PnlHardwareSummaryInfo)
        ScHardwareMain.Panel2.Controls.Add(TxtHardwarePartNumber)
        ScHardwareMain.Panel2.Controls.Add(TxtHardwareInstallation)
        ScHardwareMain.Panel2.Controls.Add(TxtHardwareMounting)
        ScHardwareMain.Panel2.Controls.Add(TxtHardwareUses)
        ScHardwareMain.Panel2.Controls.Add(TxtHardwareDescription)
        ScHardwareMain.Size = New Size(1124, 713)
        ScHardwareMain.SplitterDistance = 501
        ScHardwareMain.SplitterWidth = 6
        ScHardwareMain.TabIndex = 0
        ' 
        ' DgvHardware
        ' 
        DgvHardware.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvHardware.Dock = DockStyle.Fill
        DgvHardware.Location = New Point(0, 0)
        DgvHardware.Name = "DgvHardware"
        DgvHardware.RowHeadersWidth = 62
        DgvHardware.Size = New Size(497, 709)
        DgvHardware.TabIndex = 0
        ' 
        ' Label72
        ' 
        Label72.AutoSize = True
        Label72.Location = New Point(23, 627)
        Label72.Name = "Label72"
        Label72.Size = New Size(153, 18)
        Label72.TabIndex = 23
        Label72.Text = "**Part Number:**"
        ' 
        ' Label71
        ' 
        Label71.AutoSize = True
        Label71.Location = New Point(23, 553)
        Label71.Name = "Label71"
        Label71.Size = New Size(187, 18)
        Label71.TabIndex = 22
        Label71.Text = "**Installtion Notes:**"
        ' 
        ' Label70
        ' 
        Label70.AutoSize = True
        Label70.Location = New Point(23, 478)
        Label70.Name = "Label70"
        Label70.Size = New Size(246, 18)
        Label70.TabIndex = 21
        Label70.Text = "**Mounting Requirements:**"
        ' 
        ' Label69
        ' 
        Label69.AutoSize = True
        Label69.Location = New Point(23, 403)
        Label69.Name = "Label69"
        Label69.Size = New Size(151, 18)
        Label69.TabIndex = 20
        Label69.Text = "**Typical Uses:**"
        ' 
        ' Label68
        ' 
        Label68.AutoSize = True
        Label68.Location = New Point(23, 319)
        Label68.Name = "Label68"
        Label68.Size = New Size(143, 18)
        Label68.TabIndex = 19
        Label68.Text = "**Description:**"
        ' 
        ' PnlHardwareSummaryInfo
        ' 
        PnlHardwareSummaryInfo.BackColor = Color.Silver
        PnlHardwareSummaryInfo.Controls.Add(LblHardwareWeight)
        PnlHardwareSummaryInfo.Controls.Add(LblHardwareDimensions)
        PnlHardwareSummaryInfo.Controls.Add(LblhardwareBrand)
        PnlHardwareSummaryInfo.Controls.Add(LblHardwareCategory)
        PnlHardwareSummaryInfo.Controls.Add(LblHardwareType)
        PnlHardwareSummaryInfo.Dock = DockStyle.Top
        PnlHardwareSummaryInfo.Location = New Point(0, 0)
        PnlHardwareSummaryInfo.Name = "PnlHardwareSummaryInfo"
        PnlHardwareSummaryInfo.Size = New Size(613, 315)
        PnlHardwareSummaryInfo.TabIndex = 18
        ' 
        ' LblHardwareWeight
        ' 
        LblHardwareWeight.BackColor = Color.Silver
        LblHardwareWeight.Font = New Font("Georgia", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblHardwareWeight.Location = New Point(17, 257)
        LblHardwareWeight.Name = "LblHardwareWeight"
        LblHardwareWeight.Size = New Size(570, 47)
        LblHardwareWeight.TabIndex = 17
        LblHardwareWeight.Text = "Weight Capacity: "
        ' 
        ' LblHardwareDimensions
        ' 
        LblHardwareDimensions.BackColor = Color.Silver
        LblHardwareDimensions.Font = New Font("Georgia", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblHardwareDimensions.Location = New Point(17, 194)
        LblHardwareDimensions.Name = "LblHardwareDimensions"
        LblHardwareDimensions.Size = New Size(570, 47)
        LblHardwareDimensions.TabIndex = 16
        LblHardwareDimensions.Text = "Dimensions: "
        ' 
        ' LblhardwareBrand
        ' 
        LblhardwareBrand.BackColor = Color.Silver
        LblhardwareBrand.Font = New Font("Georgia", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblhardwareBrand.Location = New Point(17, 131)
        LblhardwareBrand.Name = "LblhardwareBrand"
        LblhardwareBrand.Size = New Size(570, 47)
        LblhardwareBrand.TabIndex = 15
        LblhardwareBrand.Text = "Brand: "
        ' 
        ' LblHardwareCategory
        ' 
        LblHardwareCategory.BackColor = Color.Silver
        LblHardwareCategory.Font = New Font("Georgia", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblHardwareCategory.Location = New Point(17, 68)
        LblHardwareCategory.Name = "LblHardwareCategory"
        LblHardwareCategory.Size = New Size(570, 47)
        LblHardwareCategory.TabIndex = 14
        LblHardwareCategory.Text = "Category: "
        ' 
        ' LblHardwareType
        ' 
        LblHardwareType.BackColor = Color.Silver
        LblHardwareType.Font = New Font("Georgia", 8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LblHardwareType.Location = New Point(17, 5)
        LblHardwareType.Name = "LblHardwareType"
        LblHardwareType.Size = New Size(570, 47)
        LblHardwareType.TabIndex = 13
        LblHardwareType.Text = "Type: "
        ' 
        ' TxtHardwarePartNumber
        ' 
        TxtHardwarePartNumber.Location = New Point(23, 652)
        TxtHardwarePartNumber.Multiline = True
        TxtHardwarePartNumber.Name = "TxtHardwarePartNumber"
        TxtHardwarePartNumber.ReadOnly = True
        TxtHardwarePartNumber.ScrollBars = ScrollBars.Both
        TxtHardwarePartNumber.Size = New Size(570, 46)
        TxtHardwarePartNumber.TabIndex = 12
        ' 
        ' TxtHardwareInstallation
        ' 
        TxtHardwareInstallation.Location = New Point(23, 577)
        TxtHardwareInstallation.Multiline = True
        TxtHardwareInstallation.Name = "TxtHardwareInstallation"
        TxtHardwareInstallation.ReadOnly = True
        TxtHardwareInstallation.ScrollBars = ScrollBars.Both
        TxtHardwareInstallation.Size = New Size(570, 46)
        TxtHardwareInstallation.TabIndex = 10
        ' 
        ' TxtHardwareMounting
        ' 
        TxtHardwareMounting.Location = New Point(23, 502)
        TxtHardwareMounting.Multiline = True
        TxtHardwareMounting.Name = "TxtHardwareMounting"
        TxtHardwareMounting.ReadOnly = True
        TxtHardwareMounting.ScrollBars = ScrollBars.Both
        TxtHardwareMounting.Size = New Size(570, 46)
        TxtHardwareMounting.TabIndex = 8
        ' 
        ' TxtHardwareUses
        ' 
        TxtHardwareUses.Location = New Point(23, 427)
        TxtHardwareUses.Multiline = True
        TxtHardwareUses.Name = "TxtHardwareUses"
        TxtHardwareUses.ReadOnly = True
        TxtHardwareUses.ScrollBars = ScrollBars.Both
        TxtHardwareUses.Size = New Size(570, 46)
        TxtHardwareUses.TabIndex = 6
        ' 
        ' TxtHardwareDescription
        ' 
        TxtHardwareDescription.Location = New Point(23, 345)
        TxtHardwareDescription.Multiline = True
        TxtHardwareDescription.Name = "TxtHardwareDescription"
        TxtHardwareDescription.ReadOnly = True
        TxtHardwareDescription.ScrollBars = ScrollBars.Both
        TxtHardwareDescription.Size = New Size(570, 46)
        TxtHardwareDescription.TabIndex = 4
        ' 
        ' TpHelp
        ' 
        TpHelp.BackColor = Color.LightGray
        TpHelp.BorderStyle = BorderStyle.Fixed3D
        TpHelp.Location = New Point(4, 27)
        TpHelp.Name = "TpHelp"
        TpHelp.Size = New Size(1170, 823)
        TpHelp.TabIndex = 7
        TpHelp.Text = "Help"
        ' 
        ' TpAbout
        ' 
        TpAbout.BackColor = Color.Gray
        TpAbout.BorderStyle = BorderStyle.Fixed3D
        TpAbout.Controls.Add(LblManageCosts)
        TpAbout.Controls.Add(BtnManageCosts)
        TpAbout.Controls.Add(GbxAbout)
        TpAbout.Controls.Add(LbLogFiles)
        TpAbout.Controls.Add(LblClickLoadLogFile)
        TpAbout.Controls.Add(RtbLog)
        TpAbout.Location = New Point(4, 27)
        TpAbout.Name = "TpAbout"
        TpAbout.Size = New Size(1170, 823)
        TpAbout.TabIndex = 11
        TpAbout.Text = "About"
        ' 
        ' GbxAbout
        ' 
        GbxAbout.Controls.Add(TxtAppAbout)
        GbxAbout.ForeColor = SystemColors.ButtonHighlight
        GbxAbout.Location = New Point(6, 3)
        GbxAbout.Name = "GbxAbout"
        GbxAbout.Size = New Size(655, 339)
        GbxAbout.TabIndex = 3
        GbxAbout.TabStop = False
        GbxAbout.Text = "Application Information"
        ' 
        ' TxtAppAbout
        ' 
        TxtAppAbout.BackColor = Color.WhiteSmoke
        TxtAppAbout.Dock = DockStyle.Fill
        TxtAppAbout.Font = New Font("Consolas", 9F)
        TxtAppAbout.ForeColor = Color.Black
        TxtAppAbout.Location = New Point(3, 22)
        TxtAppAbout.Multiline = True
        TxtAppAbout.Name = "TxtAppAbout"
        TxtAppAbout.ReadOnly = True
        TxtAppAbout.ScrollBars = ScrollBars.Vertical
        TxtAppAbout.Size = New Size(649, 314)
        TxtAppAbout.TabIndex = 0
        TxtAppAbout.WordWrap = False
        ' 
        ' LbLogFiles
        ' 
        LbLogFiles.FormattingEnabled = True
        LbLogFiles.Location = New Point(53, 479)
        LbLogFiles.Name = "LbLogFiles"
        LbLogFiles.Size = New Size(221, 274)
        LbLogFiles.TabIndex = 2
        ' 
        ' LblClickLoadLogFile
        ' 
        LblClickLoadLogFile.AutoSize = True
        LblClickLoadLogFile.ForeColor = SystemColors.ButtonFace
        LblClickLoadLogFile.Location = New Point(84, 447)
        LblClickLoadLogFile.Name = "LblClickLoadLogFile"
        LblClickLoadLogFile.Size = New Size(159, 18)
        LblClickLoadLogFile.TabIndex = 1
        LblClickLoadLogFile.Text = "Click to Load File:"
        ' 
        ' RtbLog
        ' 
        RtbLog.BackColor = SystemColors.Info
        RtbLog.ContextMenuStrip = CmsLog
        RtbLog.Location = New Point(317, 399)
        RtbLog.Name = "RtbLog"
        RtbLog.ReadOnly = True
        RtbLog.ShowSelectionMargin = True
        RtbLog.Size = New Size(832, 397)
        RtbLog.TabIndex = 0
        RtbLog.Text = ""
        ' 
        ' Label55
        ' 
        Label55.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Label55.AutoSize = True
        Label55.BackColor = Color.Silver
        Label55.Font = New Font("Georgia", 16F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label55.ForeColor = Color.Maroon
        Label55.Location = New Point(3, 0)
        Label55.Name = "Label55"
        Label55.Size = New Size(1160, 51)
        Label55.TabIndex = 0
        Label55.Text = "Joinery Calculator"
        Label55.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label56
        ' 
        Label56.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Label56.AutoSize = True
        Label56.BackColor = Color.Silver
        Label56.Font = New Font("Georgia", 12F, FontStyle.Bold)
        Label56.Location = New Point(3, 102)
        Label56.Name = "Label56"
        Label56.Size = New Size(285, 51)
        Label56.TabIndex = 2
        Label56.Text = "Input (Left)"
        Label56.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label60
        ' 
        Label60.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label60.AutoSize = True
        Label60.BackColor = Color.Silver
        Label60.Font = New Font("Georgia", 12F, FontStyle.Bold)
        Label60.ForeColor = Color.DarkBlue
        Label60.Location = New Point(352, 102)
        Label60.Name = "Label60"
        Label60.Size = New Size(370, 51)
        Label60.TabIndex = 11
        Label60.Text = "RESULTS & DIAGRAM (Right)"
        Label60.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label61
        ' 
        Label61.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        Label61.AutoSize = True
        Label61.BackColor = Color.Silver
        Label61.Font = New Font("Georgia", 12F, FontStyle.Bold)
        Label61.ForeColor = Color.DarkBlue
        Label61.Location = New Point(352, 204)
        Label61.Name = "Label61"
        Label61.Size = New Size(200, 51)
        Label61.TabIndex = 12
        Label61.Text = "Results Labels:"
        Label61.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ColCutLabel
        ' 
        ColCutLabel.MinimumWidth = 8
        ColCutLabel.Name = "ColCutLabel"
        ColCutLabel.Width = 150
        ' 
        ' ColCutLength
        ' 
        ColCutLength.MinimumWidth = 8
        ColCutLength.Name = "ColCutLength"
        ColCutLength.Width = 150
        ' 
        ' ColCutWidth
        ' 
        ColCutWidth.MinimumWidth = 8
        ColCutWidth.Name = "ColCutWidth"
        ColCutWidth.Width = 150
        ' 
        ' ColCutQuantity
        ' 
        ColCutQuantity.MinimumWidth = 8
        ColCutQuantity.Name = "ColCutQuantity"
        ColCutQuantity.Width = 150
        ' 
        ' TmrRotation
        ' 
        TmrRotation.Interval = 16
        ' 
        ' TmrDoorCalculationDelay
        ' 
        TmrDoorCalculationDelay.Interval = 500
        ' 
        ' TmrClock
        ' 
        TmrClock.Interval = 1000
        ' 
        ' BtnManageCosts
        ' 
        BtnManageCosts.BackColor = Color.MistyRose
        BtnManageCosts.Location = New Point(831, 52)
        BtnManageCosts.Name = "BtnManageCosts"
        BtnManageCosts.Size = New Size(158, 30)
        BtnManageCosts.TabIndex = 4
        BtnManageCosts.Text = "Manage Costs"
        BtnManageCosts.UseVisualStyleBackColor = False
        ' 
        ' LblManageCosts
        ' 
        LblManageCosts.AutoSize = True
        LblManageCosts.ForeColor = SystemColors.ButtonFace
        LblManageCosts.Location = New Point(801, 25)
        LblManageCosts.Name = "LblManageCosts"
        LblManageCosts.Size = New Size(219, 18)
        LblManageCosts.TabIndex = 5
        LblManageCosts.Text = "Epoxy && Board Foot Costs"
        ' 
        ' FrmMain
        ' 
        AutoScaleDimensions = New SizeF(9F, 18F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1178, 958)
        Controls.Add(Tc)
        Controls.Add(Ss3)
        Controls.Add(Ss2)
        Controls.Add(Ss1)
        Font = New Font("Georgia", 8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimumSize = New Size(1200, 900)
        Name = "FrmMain"
        SizeGripStyle = SizeGripStyle.Hide
        StartPosition = FormStartPosition.CenterScreen
        Text = "-"
        CmsLog.ResumeLayout(False)
        Ss1.ResumeLayout(False)
        Ss1.PerformLayout()
        Ss3.ResumeLayout(False)
        Ss3.PerformLayout()
        Tc.ResumeLayout(False)
        TpDrawers.ResumeLayout(False)
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
        TpDoors.ResumeLayout(False)
        GbSetScale.ResumeLayout(False)
        GbSetScale.PerformLayout()
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
        TpBoardfeet.ResumeLayout(False)
        PnlBoardFeet.ResumeLayout(False)
        PnlBoardFeet.PerformLayout()
        CType(DgvBoardfeet, ComponentModel.ISupportInitialize).EndInit()
        TpCalcs.ResumeLayout(False)
        TcCalculattions.ResumeLayout(False)
        TpEpoxy.ResumeLayout(False)
        GbxAreaCalculator.ResumeLayout(False)
        GbxAreaCalculator.PerformLayout()
        CType(DgvAreaCalc, ComponentModel.ISupportInitialize).EndInit()
        PnlStoneCoatTopCoat.ResumeLayout(False)
        PnlStoneCoatTopCoat.PerformLayout()
        PnlEpoxyPours.ResumeLayout(False)
        PnlEpoxyPours.PerformLayout()
        TpConversions.ResumeLayout(False)
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
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        TpCalculators.ResumeLayout(False)
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        CType(PbPolygon, ComponentModel.ISupportInitialize).EndInit()
        TpDrawings.ResumeLayout(False)
        CType(PbOutputDrawing, ComponentModel.ISupportInitialize).EndInit()
        TpJoinery.ResumeLayout(False)
        ScJoinery.Panel1.ResumeLayout(False)
        ScJoinery.Panel2.ResumeLayout(False)
        CType(ScJoinery, ComponentModel.ISupportInitialize).EndInit()
        ScJoinery.ResumeLayout(False)
        GbxMortiseTenonInput.ResumeLayout(False)
        GbxMortiseTenonInput.PerformLayout()
        GbxDado.ResumeLayout(False)
        GbxDado.PerformLayout()
        GbxDovetails.ResumeLayout(False)
        GbxDovetails.PerformLayout()
        GbxBoxJoint.ResumeLayout(False)
        GbxBoxJoint.PerformLayout()
        GbxMortiseTenonResults.ResumeLayout(False)
        GbxMortiseTenonResults.PerformLayout()
        CType(PbJointDiagram, ComponentModel.ISupportInitialize).EndInit()
        GbxDovetailResults.ResumeLayout(False)
        GbxDovetailResults.PerformLayout()
        GbxDadoResults.ResumeLayout(False)
        GbxBoxJointResults.ResumeLayout(False)
        GbxBoxJointResults.PerformLayout()
        TpWoodMovement.ResumeLayout(False)
        TcWoodMovement.ResumeLayout(False)
        TpWmWoodMovement.ResumeLayout(False)
        GbxWoodMovementResults.ResumeLayout(False)
        GbxWoodMovementResults.PerformLayout()
        GbxPanelGaps.ResumeLayout(False)
        GbxPanelGaps.PerformLayout()
        GbxGrainDirection.ResumeLayout(False)
        GbxGrainDirection.PerformLayout()
        GbxWoodProperties.ResumeLayout(False)
        GbxWoodProperties.PerformLayout()
        GbxWoodMovementInput.ResumeLayout(False)
        GbxWoodMovementInput.PerformLayout()
        TpWmShelfSag.ResumeLayout(False)
        GbShelfSupportType.ResumeLayout(False)
        GbShelfSupportType.PerformLayout()
        GbxShelfSagInput.ResumeLayout(False)
        GbxShelfSagInput.PerformLayout()
        GbxStiffener.ResumeLayout(False)
        GbxStiffener.PerformLayout()
        GbxShelfSagResults.ResumeLayout(False)
        GbxShelfSagResults.PerformLayout()
        CType(PbShelfDiagram, ComponentModel.ISupportInitialize).EndInit()
        TpCutList.ResumeLayout(False)
        ScCutList.Panel1.ResumeLayout(False)
        ScCutList.Panel2.ResumeLayout(False)
        CType(ScCutList, ComponentModel.ISupportInitialize).EndInit()
        ScCutList.ResumeLayout(False)
        GbxCutListInput.ResumeLayout(False)
        CType(DgvCutList, ComponentModel.ISupportInitialize).EndInit()
        PnlCutListButtons.ResumeLayout(False)
        PnlCutListOptions.ResumeLayout(False)
        PnlCutListOptions.PerformLayout()
        GbCutListResults.ResumeLayout(False)
        GbCutListResults.PerformLayout()
        GbCuttingDiagram.ResumeLayout(False)
        CType(PbCuttingDiagram, ComponentModel.ISupportInitialize).EndInit()
        PnlDiagramNav.ResumeLayout(False)
        TpReferences.ResumeLayout(False)
        TcReferences.ResumeLayout(False)
        TpWoodProperties.ResumeLayout(False)
        PnlWoodDetails.ResumeLayout(False)
        PnlWoodDetails.PerformLayout()
        PnlWoodProperties.ResumeLayout(False)
        PnlWoodProperties.PerformLayout()
        CType(DgvWoodProperties, ComponentModel.ISupportInitialize).EndInit()
        TpJoineryReference.ResumeLayout(False)
        Panel11.ResumeLayout(False)
        Panel11.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        GbxJoineryFilter.ResumeLayout(False)
        GbxJoineryFilter.PerformLayout()
        CType(DgvJoineryTypes, ComponentModel.ISupportInitialize).EndInit()
        TpHardwareStandards.ResumeLayout(False)
        GbxHardwareFilter.ResumeLayout(False)
        GbxHardwareFilter.PerformLayout()
        ScHardwareMain.Panel1.ResumeLayout(False)
        ScHardwareMain.Panel2.ResumeLayout(False)
        ScHardwareMain.Panel2.PerformLayout()
        CType(ScHardwareMain, ComponentModel.ISupportInitialize).EndInit()
        ScHardwareMain.ResumeLayout(False)
        CType(DgvHardware, ComponentModel.ISupportInitialize).EndInit()
        PnlHardwareSummaryInfo.ResumeLayout(False)
        TpAbout.ResumeLayout(False)
        TpAbout.PerformLayout()
        GbxAbout.ResumeLayout(False)
        GbxAbout.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    ' Status Strips
    Friend WithEvents Ss1 As StatusStrip
    Friend WithEvents Ss2 As StatusStrip
    Friend WithEvents Ss3 As StatusStrip
    Friend WithEvents TsslVersion As ToolStripStatusLabel
    Friend WithEvents TsslCpy As ToolStripStatusLabel
    Friend WithEvents TsslError As ToolStripStatusLabel
    Friend WithEvents TsslClock As ToolStripStatusLabel
    Friend WithEvents TsslToggleTheme As ToolStripDropDownButton
    Friend WithEvents TsslMemoriam As ToolStripStatusLabel
    Friend WithEvents TsslToggleDoorExploded As ToolStripStatusLabel
    Friend WithEvents TsslScale As ToolStripStatusLabel

    ' Main TabControl and Pages
    Friend WithEvents Tc As TabControl
    Friend WithEvents TpDrawers As TabPage
    Friend WithEvents TpDoors As TabPage
    Friend WithEvents TpBoardfeet As TabPage
    Friend WithEvents TpCalcs As TabPage
    Friend WithEvents TpDrawings As TabPage
    Friend WithEvents TpHelp As TabPage
    Friend WithEvents TpJoinery As TabPage
    Friend WithEvents TpWoodMovement As TabPage
    Friend WithEvents TpShelfSag As TabPage
    Friend WithEvents TpCutList As TabPage

    ' Drawers Tab Controls
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents LblAverageHeightResults As Label
    Friend WithEvents LblTotalMaterialResults As Label
    Friend WithEvents LblHeightRatioResults As Label
    Friend WithEvents LbltotalDrawerHeightResults As Label
    Friend WithEvents LblTotalHeightResults As Label
    Friend WithEvents LblStatus As Label
    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents DgvDrawerHeights As DataGridView
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents Button7 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents BtnCalculateDrawers As Button
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents BtnUniformPreset As Button
    Friend WithEvents BtnCustomRatioPreset As Button
    Friend WithEvents BtnExponentialProgressionPreset As Button
    Friend WithEvents BtnLogarithmicProgressionPreset As Button
    Friend WithEvents BtnReverseArithmeticPreset As Button
    Friend WithEvents BtnGoldenRatioPreset As Button
    Friend WithEvents BtnCustomCabinetPreset As Button
    Friend WithEvents BtnBathroomVanityPreset As Button
    Friend WithEvents BtnOfficeDeskPreset As Button
    Friend WithEvents BtnKitchenStandardPreset As Button
    Friend WithEvents Label32 As Label
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents LblCustomRatioInput As Label
    Friend WithEvents TxtCustomRatioInput As TextBox
    Friend WithEvents TxtArithmeticIncrement As TextBox
    Friend WithEvents TxtMultiplier As TextBox
    Friend WithEvents TxtFirstDrawerHeight As TextBox
    Friend WithEvents Label31 As Label
    Friend WithEvents Label30 As Label
    Friend WithEvents Label29 As Label
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents TxtDrawerWidth As TextBox
    Friend WithEvents TxtDrawerSpacing As TextBox
    Friend WithEvents TxtDrawerCount As TextBox
    Friend WithEvents Label28 As Label
    Friend WithEvents Label27 As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents BtnDrawDrawerImage As Button
    Friend WithEvents PnlResults As Panel
    Friend WithEvents RtbResults As RichTextBox
    Friend WithEvents GroupBox11 As GroupBox
    Friend WithEvents RbArithmetic As RadioButton
    Friend WithEvents RbFibonacci As RadioButton
    Friend WithEvents RbGeometric As RadioButton
    Friend WithEvents RbHambridge As RadioButton
    Friend WithEvents RbGoldenRatio As RadioButton
    Friend WithEvents RbReverseArithmetic As RadioButton
    Friend WithEvents RbUniform As RadioButton
    Friend WithEvents RbCustomRatio As RadioButton
    Friend WithEvents RbExponential As RadioButton
    Friend WithEvents RbLogarithmic As RadioButton
    Friend WithEvents Label47 As Label
    Friend WithEvents Label33 As Label
    Friend WithEvents BtnSaveProject As Button
    Friend WithEvents Label34 As Label
    Friend WithEvents TxtDrawerProjectName As TextBox

    ' Doors Tab Controls
    Friend WithEvents GbSetScale As GroupBox
    Friend WithEvents RbImperial As RadioButton
    Friend WithEvents RbMetric As RadioButton
    Friend WithEvents ScDoors As SplitContainer
    Friend WithEvents LblTotalMaterialArea As Label
    Friend WithEvents LblDoorWidth As Label
    Friend WithEvents LblDoorHeight As Label
    Friend WithEvents BtnDeleteDoorProject As Button
    Friend WithEvents BtnLoadDoorProject As Button
    Friend WithEvents GroupBox12 As GroupBox
    Friend WithEvents BtnOfficeDoorPreset As Button
    Friend WithEvents BtnBathroomDoorPreset As Button
    Friend WithEvents BtnKitchenDoorPreset As Button
    Friend WithEvents Label48 As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents TxtCabinetOpeningHeight As TextBox
    Friend WithEvents TxtCabinetOpeningWidth As TextBox
    Friend WithEvents Label35 As Label
    Friend WithEvents Label36 As Label
    Friend WithEvents Label37 As Label
    Friend WithEvents GroupBox10 As GroupBox
    Friend WithEvents LblPanelWidth As Label
    Friend WithEvents LblPanelHeight As Label
    Friend WithEvents Label46 As Label
    Friend WithEvents LblStileLength As Label
    Friend WithEvents LblRailLength As Label
    Friend WithEvents BtnCalculateDoors As Button
    Friend WithEvents BtnSaveDoorProject As Button
    Friend WithEvents Panel8 As Panel
    Friend WithEvents TxtDoorOverlay As TextBox
    Friend WithEvents TxtGapSize As TextBox
    Friend WithEvents TxtRailWidth As TextBox
    Friend WithEvents TxtStileWidth As TextBox
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
    Friend WithEvents TxtDoorProjectName As TextBox
    Friend WithEvents Panel7 As Panel
    Friend WithEvents TxtPanelExpansionGap As TextBox
    Friend WithEvents TxtPanelGrooveDepth As TextBox
    Friend WithEvents Label38 As Label
    Friend WithEvents Label39 As Label
    Friend WithEvents Label40 As Label
    Friend WithEvents BtnDrawDoorImage As Button
    Friend WithEvents BtnPrintDoorResults As Button
    Friend WithEvents BtnExportDoorResults As Button
    Friend WithEvents Label50 As Label
    Friend WithEvents RtbDoorResults As RichTextBox
    Friend WithEvents Label49 As Label

    ' Boardfeet Tab Controls
    Friend WithEvents PnlBoardFeet As Panel
    Friend WithEvents BtnPrtBfProject As Button
    Friend WithEvents TxtBfProjectName As TextBox
    Friend WithEvents BtnSaveBfProject As Button
    Friend WithEvents LblBoardFeetCost20 As Label
    Friend WithEvents LblTotalBoardFeet20 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents LblBoardFeetCost15 As Label
    Friend WithEvents LblTotalBoardFeet15 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents LblBoardFeetCost10 As Label
    Friend WithEvents LblTotalBoardFeet10 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents LblBoardFeetCost As Label
    Friend WithEvents LblTotalBoardFeet As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblCalculateBoardfeet As Label
    Friend WithEvents DgvBoardfeet As DataGridView
    Friend WithEvents bfCol0 As DataGridViewTextBoxColumn
    Friend WithEvents bfCol1 As DataGridViewTextBoxColumn
    Friend WithEvents bfCol2 As DataGridViewTextBoxColumn
    Friend WithEvents bfCol3 As DataGridViewTextBoxColumn
    Friend WithEvents bfCol4 As DataGridViewTextBoxColumn
    Friend WithEvents bfCol5 As DataGridViewTextBoxColumn
    Friend WithEvents bfCol6 As DataGridViewTextBoxColumn
    Friend WithEvents bfCol7 As DataGridViewTextBoxColumn

    ' Calculations Tab Controls
    Friend WithEvents TcCalculattions As TabControl
    Friend WithEvents TpEpoxy As TabPage
    Friend WithEvents TpConversions As TabPage
    Friend WithEvents TpCalculators As TabPage
    Friend WithEvents GbxAreaCalculator As GroupBox
    Friend WithEvents RbAreaBoth As RadioButton
    Friend WithEvents RbAreaTopcoat As RadioButton
    Friend WithEvents RbAreaPour As RadioButton
    Friend WithEvents DgvAreaCalc As DataGridView
    Friend WithEvents PnlStoneCoatTopCoat As Panel
    Friend WithEvents RbTcWaste20 As RadioButton
    Friend WithEvents RbTcWaste15 As RadioButton
    Friend WithEvents RbTcWaste10 As RadioButton
    Friend WithEvents RbTcWaste0 As RadioButton
    Friend WithEvents LblTcWastePct As Label
    Friend WithEvents LblTopCoatWaterMult As Label
    Friend WithEvents LblTCTotalMixture As Label
    Friend WithEvents LblPartB As Label
    Friend WithEvents LblPartA As Label
    Friend WithEvents LblTcMultiplier As Label
    Friend WithEvents TxtTotalArea As TextBox
    Friend WithEvents LblTotalArea As Label
    Friend WithEvents Label53 As Label
    Friend WithEvents PnlEpoxyPours As Panel
    Friend WithEvents LblEpoxyMilliliters As Label
    Friend WithEvents Label54 As Label
    Friend WithEvents TxtEpoxyArea As TextBox
    Friend WithEvents LblEpoxyArea As Label
    Friend WithEvents TxtEpoxyDiameter As TextBox
    Friend WithEvents Label52 As Label
    Friend WithEvents CmbEpoxyCost As ComboBox
    Friend WithEvents LblEpoxyCost As Label
    Friend WithEvents LblEpoxyLiters As Label
    Friend WithEvents RbEpoxyWaste20 As RadioButton
    Friend WithEvents RbEpoxyWaste15 As RadioButton
    Friend WithEvents RbEpoxyWaste10 As RadioButton
    Friend WithEvents RbEpoxyWaste0 As RadioButton
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtEpoxyDepth As TextBox
    Friend WithEvents TxtEpoxyWidth As TextBox
    Friend WithEvents TxtEpoxyLength As TextBox
    Friend WithEvents LblEpoxyPints As Label
    Friend WithEvents LblEpoxyQuarts As Label
    Friend WithEvents LblEpoxyGallons As Label
    Friend WithEvents LblEpoxyOunces As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label

    ' Conversions Tab Controls
    Friend WithEvents Panel4 As Panel
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents LblFraction2Decimal As Label
    Friend WithEvents TxtFraction2Decimal As TextBox
    Friend WithEvents Label16 As Label
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
    Friend WithEvents Panel2 As Panel
    Friend WithEvents RtbFraction2Mm As RichTextBox
    Friend WithEvents RtbFraction2Decimal As RichTextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label8 As Label

    ' Calculators Tab Controls
    Friend WithEvents Panel5 As Panel
    Friend WithEvents LblTippingForce As Label
    Friend WithEvents TxtTtTableBaseWeight As TextBox
    Friend WithEvents TxtTtTableBaselength As TextBox
    Friend WithEvents TxtTtTableTopWeight As TextBox
    Friend WithEvents TxtTtTableTopLength As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents Label23 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label51 As Label
    Friend WithEvents LblPolygonPieceAngle As Label
    Friend WithEvents LblPolygonSideAngle As Label
    Friend WithEvents TxtPolygonSides As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents PbPolygon As PictureBox

    ' Drawings Tab Controls
    Friend WithEvents PbOutputDrawing As PictureBox
    Friend WithEvents GbxMortiseTenonInput As GroupBox
    Friend WithEvents GbxMortiseTenonResults As GroupBox
    Friend WithEvents Label55 As Label
    Friend WithEvents Label56 As Label
    Friend WithEvents TxtJointStockThickness As TextBox
    Friend WithEvents Label59 As Label
    Friend WithEvents TxtJointStockWidth As TextBox
    Friend WithEvents Label57 As Label
    Friend WithEvents RbTenonThrough As RadioButton
    Friend WithEvents RbTenonHaunched As RadioButton
    Friend WithEvents RbTenonStandard As RadioButton
    Friend WithEvents Label58 As Label
    Friend WithEvents Label60 As Label
    Friend WithEvents Label61 As Label
    Friend WithEvents LblTenonThickness As Label
    Friend WithEvents LblTenonLength As Label
    Friend WithEvents LblTenonWidth As Label
    Friend WithEvents LblMortiseDepth As Label
    Friend WithEvents LblMortiseWidth As Label
    Friend WithEvents LblShoulderOffset As Label
    Friend WithEvents PbJointDiagram As PictureBox

    ' Timers and ToolTip
    Friend WithEvents tTip As ToolTip
    Friend WithEvents TmrRotation As Timer
    Friend WithEvents TmrDoorCalculationDelay As Timer
    Friend WithEvents TmrClock As Timer

    ' Joinery Tab - Additional Controls
    Friend WithEvents GbxDovetails As GroupBox
    Friend WithEvents LblDovetailThickness As Label
    Friend WithEvents TxtDovetailThickness As TextBox
    Friend WithEvents LblDovetailWidth As Label
    Friend WithEvents TxtDovetailWidth As TextBox
    Friend WithEvents LblDovetailSpacing As Label
    Friend WithEvents TxtDovetailSpacing As TextBox
    Friend WithEvents ChkDovetailHardwood As CheckBox
    Friend WithEvents GbxDovetailResults As GroupBox
    Friend WithEvents LblDovetailAngle As Label
    Friend WithEvents LblDovetailPinWidth As Label
    Friend WithEvents LblDovetailTailWidth As Label
    Friend WithEvents LblDovetailCount As Label
    Friend WithEvents GbxBoxJoint As GroupBox
    Friend WithEvents LblBoxJointThickness As Label
    Friend WithEvents TxtBoxJointThickness As TextBox
    Friend WithEvents LblBoxJointWidth As Label
    Friend WithEvents TxtBoxJointWidth As TextBox
    Friend WithEvents GbxBoxJointResults As GroupBox
    Friend WithEvents LblBoxJointPinWidth As Label
    Friend WithEvents LblBoxJointCount As Label
    Friend WithEvents GbxDado As GroupBox
    Friend WithEvents LblDadoStockThickness As Label
    Friend WithEvents TxtDadoStockThickness As TextBox
    Friend WithEvents LblDadoShelfThickness As Label
    Friend WithEvents TxtDadoShelfThickness As TextBox
    Friend WithEvents GbxDadoResults As GroupBox
    Friend WithEvents LblDadoDepth As Label
    Friend WithEvents LblDadoWidth As Label
    Friend WithEvents BtnCalculateJoinery As Button
    Friend WithEvents GbxWoodMovementInput As GroupBox
    Friend WithEvents LblWoodSpecies As Label
    Friend WithEvents CmbWoodSpecies As ComboBox
    Friend WithEvents LblMovementWidth As Label
    Friend WithEvents TxtMovementWidth As TextBox
    Friend WithEvents LblInitialHumidity As Label
    Friend WithEvents TxtInitialHumidity As TextBox
    Friend WithEvents LblFinalHumidity As Label
    Friend WithEvents TxtFinalHumidity As TextBox
    Friend WithEvents LblHumidityPreset As Label
    Friend WithEvents CmbHumidityPreset As ComboBox
    Friend WithEvents GbxGrainDirection As GroupBox
    Friend WithEvents RbTangential As RadioButton
    Friend WithEvents RbRadial As RadioButton
    Friend WithEvents BtnCalculateMovement As Button
    Friend WithEvents GbxWoodMovementResults As GroupBox
    Friend WithEvents LblMovementResult As Label
    Friend WithEvents LblMovementDirection As Label
    Friend WithEvents LblMovementFraction As Label
    Friend WithEvents GbxPanelGaps As GroupBox
    Friend WithEvents LblPanelGapMin As Label
    Friend WithEvents LblPanelGapMax As Label
    Friend WithEvents GbxWoodProperties As GroupBox
    Friend WithEvents LblWoodDensity As Label
    Friend WithEvents LblWoodType As Label
    Friend WithEvents ScCutList As SplitContainer
    Friend WithEvents GbxCutListInput As GroupBox
    Friend WithEvents DgvCutList As DataGridView
    Friend WithEvents ColCutLabel As DataGridViewTextBoxColumn
    Friend WithEvents ColCutLength As DataGridViewTextBoxColumn
    Friend WithEvents ColCutWidth As DataGridViewTextBoxColumn
    Friend WithEvents ColCutQuantity As DataGridViewTextBoxColumn
    Friend WithEvents PnlCutListButtons As Panel
    Friend WithEvents BtnAddCutRow As Button
    Friend WithEvents BtnDeleteCutRow As Button
    Friend WithEvents PnlCutListOptions As Panel
    Friend WithEvents LblStockBoard As Label
    Friend WithEvents CmbStockBoard As ComboBox
    Friend WithEvents LblKerf As Label
    Friend WithEvents TxtKerf As TextBox
    Friend WithEvents BtnOptimize As Button
    Friend WithEvents BtnExportCutList As Button
    Friend WithEvents GbCutListResults As GroupBox
    Friend WithEvents LblBoardsNeeded As Label
    Friend WithEvents LblTotalCost As Label
    Friend WithEvents LblWastePercent As Label
    Friend WithEvents LblAvgEfficiency As Label
    Friend WithEvents GbCuttingDiagram As GroupBox
    Friend WithEvents PbCuttingDiagram As PictureBox
    Friend WithEvents PnlDiagramNav As Panel
    Friend WithEvents BtnPrevPattern As Button
    Friend WithEvents BtnNextPattern As Button
    Friend WithEvents LblPatternInfo As Label
    Friend WithEvents ScJoinery As SplitContainer

    ' Shelf Sag Calculator (on Wood Movement Tab)
    Friend WithEvents GbxShelfSagInput As GroupBox
    Friend WithEvents LblShelfMaterial As Label
    Friend WithEvents CmbShelfMaterial As ComboBox
    Friend WithEvents LblShelfSpan As Label
    Friend WithEvents TxtShelfSpan As TextBox
    Friend WithEvents LblShelfThickness As Label
    Friend WithEvents TxtShelfThickness As TextBox
    Friend WithEvents LblShelfWidth As Label
    Friend WithEvents TxtShelfWidth As TextBox
    Friend WithEvents LblShelfLoad As Label
    Friend WithEvents TxtShelfLoad As TextBox
    Friend WithEvents BtnCalculateShelf As Button
    Friend WithEvents GbxShelfSagResults As GroupBox
    Friend WithEvents LblShelfSagInches As Label
    Friend WithEvents LblShelfSagFraction As Label
    Friend WithEvents LblShelfSafeLoad As Label
    Friend WithEvents LblShelfMaxLoad As Label
    Friend WithEvents LblShelfSafetyStatus As Label
    Friend WithEvents LblShelfMaxSpan As Label
    Friend WithEvents LblShelfMaterialInfo As Label
    Friend WithEvents LblShelfWarning As Label
    Friend WithEvents LblShelfRecommendations As Label
    Friend WithEvents PbShelfDiagram As PictureBox
    Friend WithEvents GbxStiffener As GroupBox
    Friend WithEvents LblStiffenerThickness As Label
    Friend WithEvents LblStiffenerHeight As Label
    Friend WithEvents ChkBackStiffener As CheckBox
    Friend WithEvents ChkFrontStiffener As CheckBox
    Friend WithEvents CmbStiffenerMaterial As ComboBox
    Friend WithEvents LblStiffenerMaterial As Label
    Friend WithEvents TxtStiffenerThickness As TextBox
    Friend WithEvents TxtStiffenerheight As TextBox
    Friend WithEvents TcWoodMovement As TabControl
    Friend WithEvents TpWmWoodMovement As TabPage
    Friend WithEvents TpWmShelfSag As TabPage
    Friend WithEvents TpAbout As TabPage
    Friend WithEvents RtbLog As RichTextBox
    Friend WithEvents LbLogFile As ListBox
    Friend WithEvents LbLogFiles As ListBox
    Friend WithEvents LblClickLoadLogFile As Label
    Friend WithEvents GbxAbout As GroupBox
    Friend WithEvents TxtAppAbout As TextBox
    Friend WithEvents GbShelfSupportType As GroupBox
    Friend WithEvents TxtDadoDepth1 As TextBox
    Friend WithEvents LblDadoDepth1 As Label
    Friend WithEvents RbSupportDado As RadioButton
    Friend WithEvents RbSupportBracket As RadioButton
    Friend WithEvents LblBracketWidthUnits As Label
    Friend WithEvents TxtshelfBracketWidth As TextBox
    Friend WithEvents LblShelfBracketWidth As Label
    Friend WithEvents LblSupportTypeInfo As Label
    Friend WithEvents LblDadoDepthUnit As Label
    Friend WithEvents RbSupportPin As RadioButton
    Friend WithEvents TxtPinWidth As TextBox
    Friend WithEvents LblPinWidth As Label
    Friend WithEvents LblPinWidthUnits As Label
    Friend WithEvents TpReferences As TabPage
    Friend WithEvents TcReferences As TabControl
    Friend WithEvents TpWoodProperties As TabPage
    Friend WithEvents TpJoineryReference As TabPage
    Friend WithEvents DgvWoodProperties As DataGridView
    Friend WithEvents PnlWoodProperties As Panel
    Friend WithEvents LblWoodSearch As Label
    Friend WithEvents BtnWoodClearSearch As Button
    Friend WithEvents TxtWoodSearch As TextBox
    Friend WithEvents RbWoodSoftwoods As RadioButton
    Friend WithEvents RbWoodHardwoods As RadioButton
    Friend WithEvents RbWoodAll As RadioButton
    Friend WithEvents LblWoodPropertiesReference As Label
    Friend WithEvents PnlWoodDetails As Panel
    Friend WithEvents RtbWoodDetails As RichTextBox
    Friend WithEvents LblWoodDetailsHeader As Label
    Friend WithEvents BtnPrintWoodData As Button
    Friend WithEvents BtnExportWoodData As Button
    Friend WithEvents BtnCompareWoods As Button
    Friend WithEvents BtnAddWoodSpecies As Button
    Friend WithEvents BtnLoadBoardFeetHistory As Button
    Friend WithEvents BtnSaveBoardFeetHistory As Button
    Friend WithEvents LblBfProjectName As Label
    Friend WithEvents DgvJoineryTypes As DataGridView
    Friend WithEvents GbxJoineryFilter As GroupBox
    Friend WithEvents RbJoineryBeginner As RadioButton
    Friend WithEvents RbJoineryEdge As RadioButton
    Friend WithEvents RbJoineryBox As RadioButton
    Friend WithEvents RbJoineryFrame As RadioButton
    Friend WithEvents RbJoineryAll As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents LblSummary As Label
    Friend WithEvents LblJoineryCount As Label
    Friend WithEvents LblJoineryGlue As Label
    Friend WithEvents LblJoineryStrength As Label
    Friend WithEvents LblJoineryDifficulty As Label
    Friend WithEvents LblJoineryCategory As Label
    Friend WithEvents LblJoineryName As Label
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Label62 As Label
    Friend WithEvents Label66 As Label
    Friend WithEvents Label65 As Label
    Friend WithEvents Label64 As Label
    Friend WithEvents Label63 As Label
    Friend WithEvents TxtJoineryReinforcement As TextBox
    Friend WithEvents TxtJoineryStrengthChar As TextBox
    Friend WithEvents TxtJoineryTools As TextBox
    Friend WithEvents TxtJoineryUses As TextBox
    Friend WithEvents TxtJoineryDescription As TextBox
    Friend WithEvents Label67 As Label
    Friend WithEvents TxtJoineryHistory As TextBox
    Friend WithEvents TpHardwareStandards As TabPage
    Friend WithEvents RbHardwareFasteners As RadioButton
    Friend WithEvents RbHardwareShelf As RadioButton
    Friend WithEvents RbHardwareSlides As RadioButton
    Friend WithEvents RbHardwareHinges As RadioButton
    Friend WithEvents RbHardwareAll As RadioButton
    Friend WithEvents ScHardwareMain As SplitContainer
    Friend WithEvents DgvHardware As DataGridView
    Friend WithEvents LblHardwareCount As Label
    Friend WithEvents TxtHardwarePartNumber As TextBox
    Friend WithEvents TxtHardwareInstallation As TextBox
    Friend WithEvents TxtHardwareMounting As TextBox
    Friend WithEvents TxtHardwareUses As TextBox
    Friend WithEvents TxtHardwareDescription As TextBox
    Friend WithEvents Label72 As Label
    Friend WithEvents Label71 As Label
    Friend WithEvents Label70 As Label
    Friend WithEvents Label69 As Label
    Friend WithEvents Label68 As Label
    Friend WithEvents PnlHardwareSummaryInfo As Panel
    Friend WithEvents LblHardwareWeight As Label
    Friend WithEvents LblHardwareDimensions As Label
    Friend WithEvents LblhardwareBrand As Label
    Friend WithEvents LblHardwareCategory As Label
    Friend WithEvents LblHardwareType As Label
    Friend WithEvents GbxHardwareFilter As GroupBox
    Friend WithEvents Label73 As Label
    Friend WithEvents LblManageCosts As Label
    Friend WithEvents BtnManageCosts As Button
End Class
