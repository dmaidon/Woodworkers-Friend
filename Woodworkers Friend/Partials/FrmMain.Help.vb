' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation of comprehensive help system with navigation,
'          color-coded sections, and detailed instructions for all features
' ============================================================================

Partial Public Class FrmMain

#Region "Help System"

    Private _helpInitialized As Boolean = False

    ''' <summary>
    ''' Initializes the help system when the help tab is entered
    ''' </summary>
    Private Sub TpHelp_Enter(sender As Object, e As EventArgs) Handles TpHelp.Enter
        If Not _helpInitialized Then
            InitializeHelpSystem()
            _helpInitialized = True
        End If
    End Sub

    ''' <summary>
    ''' Initializes the complete help system with navigation and content
    ''' </summary>
    Private Sub InitializeHelpSystem()
        Try
            ' Clear existing controls
            TpHelp.Controls.Clear()

            ' Create main split container
            Dim mainSplit As New SplitContainer With {
                .Dock = DockStyle.Fill,
                .SplitterDistance = 200,
                .SplitterWidth = 4,
                .BackColor = Color.LightGray
            }

            ' Create navigation panel (left side)
            CreateHelpNavigation(mainSplit.Panel1)

            ' Create content panel (right side)
            CreateHelpContent(mainSplit.Panel2)

            ' Add split container to help tab
            TpHelp.Controls.Add(mainSplit)
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "InitializeHelpSystem", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Creates the help navigation tree
    ''' </summary>
    Private Sub CreateHelpNavigation(panel As SplitterPanel)
        Dim treeView As New TreeView With {
            .Dock = DockStyle.Fill,
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .BackColor = Color.FromArgb(240, 240, 240),
            .Name = "tvHelpNav"
        }

        ' Add main categories
        Dim nodeGettingStarted = treeView.Nodes.Add("getting_started", "?? Getting Started")
        nodeGettingStarted.Nodes.Add("overview", "Application Overview")
        nodeGettingStarted.Nodes.Add("interface", "Understanding the Interface")
        nodeGettingStarted.Nodes.Add("quick_start", "Quick Start Guide")

        Dim nodeCalculators = treeView.Nodes.Add("calculators", "?? Calculators")
        nodeCalculators.Nodes.Add("drawers", "Drawer Calculator")
        nodeCalculators.Nodes.Add("doors", "Door Calculator")
        nodeCalculators.Nodes.Add("boardfeet", "Board Feet Calculator")
        nodeCalculators.Nodes.Add("epoxy", "Epoxy Pour Calculator")
        nodeCalculators.Nodes.Add("polygon", "Polygon Calculator")

        Dim nodeConversions = treeView.Nodes.Add("conversions", "?? Conversions")
        nodeConversions.Nodes.Add("units", "Unit Conversions")
        nodeConversions.Nodes.Add("fractions", "Fraction to Decimal")
        nodeConversions.Nodes.Add("table_tip", "Table Tipping Force")

        Dim nodeFeatures = treeView.Nodes.Add("features", "? Features")
        nodeFeatures.Nodes.Add("export", "Exporting Results")
        nodeFeatures.Nodes.Add("presets", "Using Presets")
        nodeFeatures.Nodes.Add("validation", "Input Validation")
        nodeFeatures.Nodes.Add("themes", "Dark/Light Themes")

        Dim nodeTips = treeView.Nodes.Add("tips", "?? Tips & Tricks")
        nodeTips.Nodes.Add("shortcuts", "Keyboard Shortcuts")
        nodeTips.Nodes.Add("best_practices", "Best Practices")
        nodeTips.Nodes.Add("troubleshooting", "Troubleshooting")

        Dim nodeAbout = treeView.Nodes.Add("about", "?? About")
        nodeAbout.Nodes.Add("version", "Version Information")
        nodeAbout.Nodes.Add("updates", "Recent Updates")
        nodeAbout.Nodes.Add("credits", "Credits")

        ' Expand all top-level nodes
        For Each node As TreeNode In treeView.Nodes
            node.Expand()
        Next

        ' Handle node selection
        AddHandler treeView.AfterSelect, AddressOf HelpNav_AfterSelect

        panel.Controls.Add(treeView)
    End Sub

    ''' <summary>
    ''' Creates the help content display area
    ''' </summary>
    Private Sub CreateHelpContent(panel As SplitterPanel)
        Dim rtbHelp As New RichTextBox With {
            .Dock = DockStyle.Fill,
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .BackColor = Color.White,
            .ReadOnly = True,
            .Name = "rtbHelpContent",
            .BorderStyle = BorderStyle.None,
            .Padding = New Padding(10)
        }

        panel.Controls.Add(rtbHelp)

        ' Show default content
        ShowHelpContent("getting_started", rtbHelp)
    End Sub

    ''' <summary>
    ''' Handles navigation tree selection
    ''' </summary>
    Private Sub HelpNav_AfterSelect(sender As Object, e As TreeViewEventArgs)
        Dim mainSplit = CType(TpHelp.Controls(0), SplitContainer)
        Dim rtbHelp = CType(mainSplit.Panel2.Controls("rtbHelpContent"), RichTextBox)
        ShowHelpContent(e.Node.Name, rtbHelp)
    End Sub

    ''' <summary>
    ''' Displays help content based on selected topic
    ''' </summary>
    Private Sub ShowHelpContent(topic As String, rtb As RichTextBox)
        rtb.Clear()

        ' Map topic names to markdown file names
        Dim topicFileName As String = MapTopicToFileName(topic)

        ' Try to load from embedded markdown resource first
        Dim markdownContent As String = HelpContentManager.LoadHelpTopic(topicFileName)

        ' Format and display the markdown content
        MarkdownFormatter.FormatMarkdown(rtb, markdownContent)
    End Sub

    ''' <summary>
    ''' Maps topic names to markdown file names
    ''' </summary>
    Private Function MapTopicToFileName(topic As String) As String
        Select Case topic.ToLower()
            Case "getting_started", "overview"
                Return "GettingStarted"
            Case "drawers"
                Return "DrawerCalculator"
            Case "wood_movement"
                Return "WoodMovement"
            Case "shelf_sag"
                Return "ShelfSag"
            Case Else
                ' Default to GettingStarted for unknown topics
                Return "GettingStarted"
        End Select
    End Function

#Region "Help Content Methods"

    ''' <summary>
    ''' Shows getting started help
    ''' </summary>
    Private Sub ShowGettingStartedHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Getting Started with Woodworker's Friend", Color.DarkBlue)
        AddHelpSection(rtb, "Welcome!", "Woodworker's Friend is your comprehensive woodworking calculator and planning tool. This application helps you calculate dimensions, materials, and perform conversions for various woodworking projects.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "What Can You Do?", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Calculate drawer heights using various mathematical progressions")
        AddHelpBullet(rtb, "Design cabinet doors with precise rail and stile dimensions")
        AddHelpBullet(rtb, "Calculate board feet for material estimation")
        AddHelpBullet(rtb, "Determine epoxy pour volumes for river tables and projects")
        AddHelpBullet(rtb, "Calculate polygon dimensions and angles")
        AddHelpBullet(rtb, "Convert between imperial and metric units")
        AddHelpBullet(rtb, "Convert fractions to decimals and vice versa")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Navigation", Color.DarkOliveGreen)
        AddHelpText(rtb, "Use the tabs at the top of the window to access different calculators and tools. Each tab contains related functionality with easy-to-understand input fields.")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? Tip: Hover your mouse over input fields to see helpful tooltips with valid ranges and examples!")
    End Sub

    ''' <summary>
    ''' Shows interface help
    ''' </summary>
    Private Sub ShowInterfaceHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "Understanding the Interface", Color.DarkBlue)

        AddHelpSubtitle(rtb, "Main Window Layout", Color.DarkOliveGreen)
        AddHelpText(rtb, "The application uses a tabbed interface with the following main sections:")
        AddHelpNewLine(rtb)

        AddHelpBullet(rtb, "Drawers Tab - Calculate drawer heights and spacing")
        AddHelpBullet(rtb, "Doors Tab - Design cabinet doors with precise measurements")
        AddHelpBullet(rtb, "Board Feet Tab - Calculate lumber requirements")
        AddHelpBullet(rtb, "Calculations Tab - Unit conversions and misc calculations")
        AddHelpBullet(rtb, "Epoxy Tab - Calculate epoxy pour volumes")
        AddHelpBullet(rtb, "Conversions Tab - Quick unit conversions")
        AddHelpBullet(rtb, "Calculators Tab - Polygon and geometric calculations")
        AddHelpBullet(rtb, "Drawings Tab - Visual representations of calculations")
        AddHelpBullet(rtb, "Help Tab - This help system")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Status Bar", Color.DarkOliveGreen)
        AddHelpText(rtb, "The status bar at the bottom shows:")
        AddHelpBullet(rtb, "Application version")
        AddHelpBullet(rtb, "Copyright information")
        AddHelpBullet(rtb, "Current theme (click to toggle Dark/Light)")
        AddHelpBullet(rtb, "Current scale setting")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Color Coding", Color.DarkOliveGreen)
        AddHelpColorBox(rtb, "Input Fields", Color.White, "Standard input areas")
        AddHelpColorBox(rtb, "Results", Color.LightYellow, "Calculated results display here")
        AddHelpColorBox(rtb, "Errors", Color.LightPink, "Invalid inputs are highlighted")
        AddHelpColorBox(rtb, "Success", Color.LightGreen, "Successful operations")
    End Sub

    ''' <summary>
    ''' Shows quick start guide
    ''' </summary>
    Private Sub ShowQuickStartHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "? Quick Start Guide", Color.DarkBlue)

        AddHelpSubtitle(rtb, "Your First Calculation in 3 Steps", Color.DarkGreen)

        AddHelpStep(rtb, 1, "Choose Your Tool", "Click on the tab for the calculator you need (e.g., Drawers, Doors, Epoxy)")
        AddHelpStep(rtb, 2, "Enter Your Measurements", "Fill in the required dimensions in the input fields. Valid ranges are shown in tooltips.")
        AddHelpStep(rtb, 3, "View Results", "Click Calculate to see your results. Results include measurements, materials needed, and visual representations where applicable.")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Example: Drawer Calculator", Color.DarkOliveGreen)
        AddHelpNumbered(rtb, 1, "Navigate to the Drawers tab")
        AddHelpNumbered(rtb, 2, "Enter the number of drawers (e.g., 5)")
        AddHelpNumbered(rtb, 3, "Enter drawer width (e.g., 24 inches)")
        AddHelpNumbered(rtb, 4, "Enter drawer spacing (e.g., 0.125 inches)")
        AddHelpNumbered(rtb, 5, "Choose a calculation method (e.g., Geometric)")
        AddHelpNumbered(rtb, 6, "Click 'Calculate' to see drawer heights")

        AddHelpNewLine(rtb)
        AddHelpWarning(rtb, "?? Always verify calculations match your project requirements before cutting materials!")
    End Sub

    ''' <summary>
    ''' Shows drawer calculator help
    ''' </summary>
    Private Sub ShowDrawerCalculatorHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Drawer Calculator", Color.DarkBlue)

        AddHelpSection(rtb, "Purpose", "Calculate optimal drawer heights for cabinets using various mathematical progressions. Perfect for creating aesthetically pleasing and functional drawer configurations.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Calculation Methods", Color.DarkOliveGreen)

        AddHelpMethodBox(rtb, "Geometric", "Each drawer is proportionally taller than the previous one", Color.FromArgb(230, 230, 250))
        AddHelpMethodBox(rtb, "Arithmetic", "Each drawer increases by a fixed amount", Color.FromArgb(240, 255, 240))
        AddHelpMethodBox(rtb, "Golden Ratio", "Uses the golden ratio (1.618) for pleasing proportions", Color.FromArgb(255, 250, 205))
        AddHelpMethodBox(rtb, "Fibonacci", "Based on the Fibonacci sequence", Color.FromArgb(255, 228, 225))
        AddHelpMethodBox(rtb, "Uniform", "All drawers the same height", Color.FromArgb(245, 245, 245))
        AddHelpMethodBox(rtb, "Custom Ratio", "Define your own progression ratio", Color.FromArgb(230, 250, 250))

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Required Inputs", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Number of Drawers (1-20)")
        AddHelpBullet(rtb, "Drawer Width (6-48 inches)")
        AddHelpBullet(rtb, "Drawer Spacing (0-2 inches)")
        AddHelpBullet(rtb, "First Drawer Height (method-specific)")
        AddHelpBullet(rtb, "Multiplier or Increment (method-specific)")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Presets", Color.DarkOliveGreen)
        AddHelpText(rtb, "Quick-start presets are available for common scenarios:")
        AddHelpBullet(rtb, "Kitchen Standard - Typical kitchen base cabinet")
        AddHelpBullet(rtb, "Office Desk - Standard desk drawer configuration")
        AddHelpBullet(rtb, "Bathroom Vanity - Bathroom cabinet dimensions")
        AddHelpBullet(rtb, "Custom Cabinet - Your saved configurations")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? Tip: Use the 'Draw' button to see a visual representation of your drawer configuration!")
    End Sub

    ''' <summary>
    ''' Shows door calculator help
    ''' </summary>
    Private Sub ShowDoorCalculatorHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Door Calculator", Color.DarkBlue)

        AddHelpSection(rtb, "Purpose", "Calculate precise dimensions for cabinet doors including rails, stiles, and panels. Supports both inset and overlay door configurations.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Door Types", Color.DarkOliveGreen)
        AddHelpMethodBox(rtb, "Inset Doors", "Door fits inside the cabinet opening", Color.FromArgb(230, 230, 250))
        AddHelpMethodBox(rtb, "Overlay Doors", "Door overlaps the cabinet face frame", Color.FromArgb(240, 255, 240))

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Required Inputs", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Cabinet Opening Height (6-120 inches)")
        AddHelpBullet(rtb, "Cabinet Opening Width (6-60 inches)")
        AddHelpBullet(rtb, "Stile Width (0.5-6 inches)")
        AddHelpBullet(rtb, "Rail Width (0.5-6 inches)")
        AddHelpBullet(rtb, "Panel Groove Depth (typically 0.25-0.5 inches)")
        AddHelpBullet(rtb, "Gap Size (for inset doors)")
        AddHelpBullet(rtb, "Overlay Amount (for overlay doors)")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Calculated Results", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Exact door width and height")
        AddHelpBullet(rtb, "Rail lengths (top and bottom)")
        AddHelpBullet(rtb, "Stile lengths (left and right)")
        AddHelpBullet(rtb, "Panel dimensions")
        AddHelpBullet(rtb, "Material requirements")

        AddHelpNewLine(rtb)
        AddHelpWarning(rtb, "?? Remember to account for wood movement when sizing panels!")
    End Sub

    ''' <summary>
    ''' Shows board feet help
    ''' </summary>
    Private Sub ShowBoardFeetHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Board Feet Calculator", Color.DarkBlue)

        AddHelpSection(rtb, "What is a Board Foot?", "A board foot is a unit of measurement for lumber. One board foot equals 144 cubic inches (1"" × 12"" × 12"").", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Formula", Color.DarkOliveGreen)
        AddHelpFormula(rtb, "Board Feet = (Thickness × Width × Length) ÷ 144")
        AddHelpText(rtb, "Where all dimensions are in inches")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Using the Calculator", Color.DarkOliveGreen)
        AddHelpNumbered(rtb, 1, "Enter board thickness in inches (e.g., 0.75 for 3/4"")")
        AddHelpNumbered(rtb, 2, "Enter board width in inches")
        AddHelpNumbered(rtb, 3, "Enter board length in inches")
        AddHelpNumbered(rtb, 4, "Enter quantity needed")
        AddHelpNumbered(rtb, 5, "Add waste percentage (typically 10-20%)")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Multiple Boards", Color.DarkOliveGreen)
        AddHelpText(rtb, "Use the grid to calculate total board feet for multiple board sizes. The calculator automatically sums all entries and applies waste percentage.")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? Tip: Always add 10-20% waste factor for cuts, mistakes, and matching grain!")
    End Sub

    ''' <summary>
    ''' Shows epoxy pour help
    ''' </summary>
    Private Sub ShowEpoxyHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Epoxy Pour Calculator", Color.DarkBlue)

        AddHelpSection(rtb, "Purpose", "Calculate the exact amount of epoxy resin needed for river tables, bar tops, and other epoxy projects.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Calculation Methods", Color.DarkOliveGreen)
        AddHelpMethodBox(rtb, "Rectangular Pour", "For straight-sided projects (Length × Width × Depth)", Color.FromArgb(230, 230, 250))
        AddHelpMethodBox(rtb, "Circular Pour", "For round projects (? × Radius² × Depth)", Color.FromArgb(240, 255, 240))
        AddHelpMethodBox(rtb, "Custom Area", "Enter your own calculated area", Color.FromArgb(255, 250, 205))

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Required Inputs", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Length (inches) - for rectangular pours")
        AddHelpBullet(rtb, "Width (inches) - for rectangular pours")
        AddHelpBullet(rtb, "Diameter (inches) - for circular pours")
        AddHelpBullet(rtb, "Depth (1/16"" to 6"" typical)")
        AddHelpBullet(rtb, "Waste percentage (10-20% recommended)")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Results Provided", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Total ounces needed")
        AddHelpBullet(rtb, "Gallons, quarts, and pints")
        AddHelpBullet(rtb, "Milliliters and liters")
        AddHelpBullet(rtb, "Estimated cost (if epoxy price is set)")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Area Calculator Grid", Color.DarkOliveGreen)
        AddHelpText(rtb, "Use the grid to calculate total area for complex shapes:")
        AddHelpNumbered(rtb, 1, "Enter length and width for each section")
        AddHelpNumbered(rtb, 2, "Area is automatically calculated")
        AddHelpNumbered(rtb, 3, "Total area is summed for all rows")
        AddHelpNumbered(rtb, 4, "Choose to apply to Epoxy Pour, Top Coat, or Both")

        AddHelpNewLine(rtb)
        AddHelpWarning(rtb, "?? Important: Always mix resin in small batches to avoid excessive heat buildup!")
        AddHelpNote(rtb, "?? Tip: For deep pours, consider multiple thin layers instead of one thick pour!")
    End Sub

    ''' <summary>
    ''' Shows polygon calculator help
    ''' </summary>
    Private Sub ShowPolygonHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "? Polygon Calculator", Color.DarkBlue)

        AddHelpSection(rtb, "Purpose", "Calculate dimensions and angles for regular polygons. Useful for hexagonal tables, octagonal windows, and decorative inlays.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Calculations", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Side length from radius")
        AddHelpBullet(rtb, "Interior angles")
        AddHelpBullet(rtb, "Exterior angles")
        AddHelpBullet(rtb, "Total perimeter")
        AddHelpBullet(rtb, "Area")
        AddHelpBullet(rtb, "Apothem (distance from center to mid-side)")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Inputs", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Number of sides (3-25)")
        AddHelpBullet(rtb, "Radius or side length")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Common Polygons", Color.DarkOliveGreen)
        AddHelpText(rtb, "Triangle (3), Square (4), Pentagon (5), Hexagon (6), Octagon (8)")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? The visual display rotates to show your polygon from all angles!")
    End Sub

    ''' <summary>
    ''' Shows unit conversions help
    ''' </summary>
    Private Sub ShowUnitConversionsHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Unit Conversions", Color.DarkBlue)

        AddHelpSection(rtb, "Available Conversions", "Quick conversion between imperial and metric units commonly used in woodworking.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Length Conversions", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Inches to Millimeters (1"" = 25.4mm)")
        AddHelpBullet(rtb, "Millimeters to Inches")
        AddHelpBullet(rtb, "Feet to Meters")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Fraction Conversions", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Decimal to Fraction (e.g., 0.375 = 3/8)")
        AddHelpBullet(rtb, "Fraction to Decimal (e.g., 3/4 = 0.75)")
        AddHelpBullet(rtb, "Mixed fractions supported (e.g., 1 1/2 = 1.5)")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Quick Reference Tables", Color.DarkOliveGreen)
        AddHelpText(rtb, "The Calculations tab includes comprehensive conversion tables for common fractions (1/64"" through 1"") in both decimal and metric.")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? Tip: Conversion constants are centralized and can be found in UnitConversionConstants module!")
    End Sub

    ''' <summary>
    ''' Shows fractions help
    ''' </summary>
    Private Sub ShowFractionsHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Fraction Conversions", Color.DarkBlue)

        AddHelpSection(rtb, "Working with Fractions", "Woodworking often requires converting between fractional and decimal measurements.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Fraction to Decimal", Color.DarkOliveGreen)
        AddHelpText(rtb, "Enter fractions in any of these formats:")
        AddHelpBullet(rtb, "Simple: 3/4")
        AddHelpBullet(rtb, "Mixed: 1 1/2")
        AddHelpBullet(rtb, "Already decimal: 0.75")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Decimal to Fraction", Color.DarkOliveGreen)
        AddHelpText(rtb, "Converts decimal values to the nearest 1/64"" fraction. The calculator automatically reduces fractions (e.g., 32/64 becomes 1/2).")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Common Conversions", Color.DarkOliveGreen)
        AddHelpText(rtb, "Quick reference for common woodworking measurements:")
        AddHelpColorBox(rtb, "1/4"" = 0.25", Color.LightYellow, "Quarter inch")
        AddHelpColorBox(rtb, "3/8"" = 0.375", Color.LightYellow, "Common dado width")
        AddHelpColorBox(rtb, "1/2"" = 0.5", Color.LightYellow, "Half inch")
        AddHelpColorBox(rtb, "5/8"" = 0.625", Color.LightYellow, "Common drawer bottom")
        AddHelpColorBox(rtb, "3/4"" = 0.75", Color.LightYellow, "Standard plywood thickness")
    End Sub

    ''' <summary>
    ''' Shows table tipping force help
    ''' </summary>
    Private Sub ShowTableTipHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Table Tipping Force Calculator", Color.DarkBlue)

        AddHelpSection(rtb, "Purpose", "Calculate the force required to tip over a table. Important for safety in furniture design, especially with children.", Color.DarkRed)

        AddHelpSubtitle(rtb, "How It Works", Color.DarkOliveGreen)
        AddHelpText(rtb, "The calculator determines the force needed at the edge of the table top to cause tipping, based on:")
        AddHelpBullet(rtb, "Table top weight and length")
        AddHelpBullet(rtb, "Base weight and length")
        AddHelpBullet(rtb, "Lever arm principles")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Required Inputs", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Table Top Length (inches or mm)")
        AddHelpBullet(rtb, "Table Top Weight (lbs or kg)")
        AddHelpBullet(rtb, "Base Length (inches or mm)")
        AddHelpBullet(rtb, "Base Weight (lbs or kg)")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Safety Guidelines", Color.DarkRed)
        AddHelpWarning(rtb, "?? IMPORTANT: Tipping force should be AT LEAST 50 lbs (23 kg) for tables used around children!")
        AddHelpWarning(rtb, "?? Consider: A heavy table is safer than a light one with the same proportions")
        AddHelpWarning(rtb, "?? Wider bases significantly increase tipping resistance")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Design Tips", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Increase base width/length")
        AddHelpBullet(rtb, "Add weight to the base")
        AddHelpBullet(rtb, "Reduce table top overhang")
        AddHelpBullet(rtb, "Consider attaching to wall for very tall pieces")
    End Sub

    ''' <summary>
    ''' Shows export functionality help
    ''' </summary>
    Private Sub ShowExportHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Exporting Results", Color.DarkBlue)

        AddHelpSection(rtb, "Export Capabilities", "Save and share your calculations in multiple formats for documentation, client quotes, and project planning.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Available Export Formats", Color.DarkOliveGreen)

        AddHelpMethodBox(rtb, "CSV (Comma-Separated Values)", "Opens in Excel, Google Sheets, or any spreadsheet application. Best for data analysis and manipulation.", Color.FromArgb(230, 230, 250))

        AddHelpMethodBox(rtb, "Text (Plain Text)", "Simple format readable in any text editor. Good for printing or embedding in documentation.", Color.FromArgb(240, 255, 240))

        AddHelpMethodBox(rtb, "HTML (Web Page)", "Formatted web page with styling. Perfect for emailing to clients or posting online.", Color.FromArgb(255, 250, 205))

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "How to Export", Color.DarkOliveGreen)
        AddHelpNumbered(rtb, 1, "Complete your calculations")
        AddHelpNumbered(rtb, 2, "Right-click on results area")
        AddHelpNumbered(rtb, 3, "Choose 'Export Results'")
        AddHelpNumbered(rtb, 4, "Select format (CSV, Text, or HTML)")
        AddHelpNumbered(rtb, 5, "Choose save location")
        AddHelpNumbered(rtb, 6, "Click Save")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "What Gets Exported", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "All input parameters")
        AddHelpBullet(rtb, "All calculated results")
        AddHelpBullet(rtb, "Timestamp of calculation")
        AddHelpBullet(rtb, "Application version")
        AddHelpBullet(rtb, "Formatted for easy reading")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? Tip: CSV exports can be imported back into Excel for further calculations or charts!")
        AddHelpNote(rtb, "?? Tip: HTML exports look professional when emailed to clients!")
    End Sub

    ''' <summary>
    ''' Shows presets help
    ''' </summary>
    Private Sub ShowPresetsHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "? Using Presets", Color.DarkBlue)

        AddHelpSection(rtb, "What are Presets?", "Presets are pre-configured settings for common woodworking scenarios. They save time and ensure you start with industry-standard dimensions.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Available Presets", Color.DarkOliveGreen)

        AddHelpMethodBox(rtb, "Kitchen Standard", "Typical kitchen base cabinet: 5 drawers with graduated heights", Color.FromArgb(255, 250, 205))

        AddHelpMethodBox(rtb, "Office Desk", "Standard office desk drawer configuration", Color.FromArgb(230, 230, 250))

        AddHelpMethodBox(rtb, "Bathroom Vanity", "Typical bathroom vanity dimensions", Color.FromArgb(240, 255, 240))

        AddHelpMethodBox(rtb, "Custom Cabinet", "Your saved custom configurations", Color.FromArgb(255, 240, 245))

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Using Presets", Color.DarkOliveGreen)
        AddHelpNumbered(rtb, 1, "Navigate to the calculator (e.g., Drawers)")
        AddHelpNumbered(rtb, 2, "Click on a preset button")
        AddHelpNumbered(rtb, 3, "All fields are populated automatically")
        AddHelpNumbered(rtb, 4, "Modify values as needed for your project")
        AddHelpNumbered(rtb, 5, "Calculate to see results")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Creating Custom Presets", Color.DarkOliveGreen)
        AddHelpNumbered(rtb, 1, "Configure calculator with your desired settings")
        AddHelpNumbered(rtb, 2, "Click 'Save as Preset'")
        AddHelpNumbered(rtb, 3, "Give it a descriptive name")
        AddHelpNumbered(rtb, 4, "Your preset is now available in the preset list")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? Tip: Save presets for your frequently-built pieces to speed up future projects!")
    End Sub

    ''' <summary>
    ''' Shows validation help
    ''' </summary>
    Private Sub ShowValidationHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "? Input Validation", Color.DarkBlue)

        AddHelpSection(rtb, "Smart Input Validation", "Woodworker's Friend includes intelligent validation to prevent errors and guide you to successful calculations.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Validation Features", Color.DarkOliveGreen)

        AddHelpBullet(rtb, "Real-time feedback as you type")
        AddHelpBullet(rtb, "Clear error messages explaining what's wrong")
        AddHelpBullet(rtb, "Suggested valid ranges for each field")
        AddHelpBullet(rtb, "Automatic fixing of common mistakes")
        AddHelpBullet(rtb, "Prevention of impossible calculations")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Visual Feedback", Color.DarkOliveGreen)
        AddHelpColorBox(rtb, "White Background", Color.White, "Valid input, ready to calculate")
        AddHelpColorBox(rtb, "Light Pink Background", Color.LightPink, "Invalid input, needs correction")
        AddHelpColorBox(rtb, "Yellow Background", Color.LightYellow, "Warning - value is unusual but acceptable")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Common Validation Rules", Color.DarkOliveGreen)
        AddHelpText(rtb, "Drawer Calculator:")
        AddHelpBullet(rtb, "Drawer count: 1-20 drawers")
        AddHelpBullet(rtb, "Width: 6-48 inches")
        AddHelpBullet(rtb, "Spacing: 0-2 inches")

        AddHelpNewLine(rtb)
        AddHelpText(rtb, "Door Calculator:")
        AddHelpBullet(rtb, "Height: 6-120 inches")
        AddHelpBullet(rtb, "Width: 6-60 inches")
        AddHelpBullet(rtb, "Stile/Rail: 0.5-6 inches")

        AddHelpNewLine(rtb)
        AddHelpText(rtb, "Epoxy Calculator:")
        AddHelpBullet(rtb, "Depth: 1/16 inch to 6 inches")
        AddHelpBullet(rtb, "Dimensions: 0.1-1000 inches")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? Tip: Hover over any input field to see its valid range and examples!")
        AddHelpWarning(rtb, "?? Validation prevents mistakes, but always double-check critical measurements!")
    End Sub

    ''' <summary>
    ''' Shows themes help
    ''' </summary>
    Private Sub ShowThemesHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Dark & Light Themes", Color.DarkBlue)

        AddHelpSection(rtb, "Visual Themes", "Choose between Light and Dark themes to match your preference and working environment.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Switching Themes", Color.DarkOliveGreen)
        AddHelpText(rtb, "Click the theme toggle in the status bar at the bottom of the window.")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Light Theme", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Traditional light background")
        AddHelpBullet(rtb, "Black text on white")
        AddHelpBullet(rtb, "Better for bright environments")
        AddHelpBullet(rtb, "Less eye strain in daylight")
        AddHelpBullet(rtb, "Better for printing screenshots")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Dark Theme", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Dark background")
        AddHelpBullet(rtb, "White text on dark gray")
        AddHelpBullet(rtb, "Reduces eye strain in dim lighting")
        AddHelpBullet(rtb, "Modern appearance")
        AddHelpBullet(rtb, "Reduces screen brightness")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? Your theme preference is remembered between sessions!")
        AddHelpNote(rtb, "?? Both themes maintain the same functionality - choose what's comfortable for you!")
    End Sub

    ''' <summary>
    ''' Shows keyboard shortcuts help
    ''' </summary>
    Private Sub ShowShortcutsHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Keyboard Shortcuts", Color.DarkBlue)

        AddHelpSection(rtb, "Work Faster", "Use keyboard shortcuts to navigate and perform calculations more efficiently.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Navigation Shortcuts", Color.DarkOliveGreen)
        AddShortcut(rtb, "Ctrl+Tab", "Next tab")
        AddShortcut(rtb, "Ctrl+Shift+Tab", "Previous tab")
        AddShortcut(rtb, "Tab", "Next field")
        AddShortcut(rtb, "Shift+Tab", "Previous field")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Action Shortcuts", Color.DarkOliveGreen)
        AddShortcut(rtb, "Enter", "Calculate (when in input field)")
        AddShortcut(rtb, "Ctrl+C", "Copy results")
        AddShortcut(rtb, "Ctrl+V", "Paste values")
        AddShortcut(rtb, "Ctrl+A", "Select all (in text fields)")
        AddShortcut(rtb, "Escape", "Clear current field")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Future Shortcuts (Coming Soon)", Color.DarkOliveGreen)
        AddShortcut(rtb, "Ctrl+Z", "Undo last action")
        AddShortcut(rtb, "Ctrl+Y", "Redo action")
        AddShortcut(rtb, "Ctrl+S", "Save project")
        AddShortcut(rtb, "Ctrl+E", "Export results")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? Tip: Text fields auto-select all text when you click them for quick replacement!")
    End Sub

    ''' <summary>
    ''' Shows best practices help
    ''' </summary>
    Private Sub ShowBestPracticesHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "? Best Practices", Color.DarkBlue)

        AddHelpSection(rtb, "Tips for Success", "Follow these best practices to get the most accurate and useful results from Woodworker's Friend.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Measurement Best Practices", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Always measure twice, calculate once")
        AddHelpBullet(rtb, "Use consistent units throughout a project")
        AddHelpBullet(rtb, "Account for wood movement in panel calculations")
        AddHelpBullet(rtb, "Add waste factor to material estimates (10-20%)")
        AddHelpBullet(rtb, "Consider grain direction in your calculations")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Calculator Usage", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Use presets as starting points")
        AddHelpBullet(rtb, "Export results before starting a new calculation")
        AddHelpBullet(rtb, "Verify results make sense for your project")
        AddHelpBullet(rtb, "Save custom presets for repeat projects")
        AddHelpBullet(rtb, "Check units before calculating")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Safety First", Color.DarkRed)
        AddHelpWarning(rtb, "?? Always verify structural calculations with local building codes")
        AddHelpWarning(rtb, "?? Test joinery on scrap wood before final pieces")
        AddHelpWarning(rtb, "?? Use appropriate safety equipment when working")
        AddHelpWarning(rtb, "?? Confirm table tipping force meets safety standards")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Material Selection", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Choose wood species appropriate for the application")
        AddHelpBullet(rtb, "Consider wood movement when sizing panels")
        AddHelpBullet(rtb, "Account for plywood actual vs. nominal thickness")
        AddHelpBullet(rtb, "Plan cuts to minimize waste")
        AddHelpBullet(rtb, "Consider grain patterns in visible surfaces")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? The calculator is a tool - your experience and judgment are irreplaceable!")
    End Sub

    ''' <summary>
    ''' Shows troubleshooting help
    ''' </summary>
    Private Sub ShowTroubleshootingHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Troubleshooting", Color.DarkBlue)

        AddHelpSection(rtb, "Common Issues & Solutions", "Having problems? Check these common issues and their solutions.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Calculation Issues", Color.DarkOliveGreen)

        AddHelpProblemSolution(rtb,
            "Problem: ""Invalid Input"" error",
            "• Check that all required fields are filled in" & vbCrLf &
            "• Verify values are within valid ranges (hover for tooltip)" & vbCrLf &
            "• Ensure you're using numbers, not text" & vbCrLf &
            "• Check for extra spaces or special characters")

        AddHelpProblemSolution(rtb,
            "Problem: Results seem incorrect",
            "• Verify you're using consistent units (all inches or all mm)" & vbCrLf &
            "• Double-check input values for typos" & vbCrLf &
            "• Ensure correct calculation method is selected" & vbCrLf &
            "• Try a preset to verify calculator is working")

        AddHelpProblemSolution(rtb,
            "Problem: Can't see all results",
            "• Scroll down in the results panel" & vbCrLf &
            "• Resize the window or splitter" & vbCrLf &
            "• Export results to view in external program" & vbCrLf &
            "• Check if results panel is collapsed")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Display Issues", Color.DarkOliveGreen)

        AddHelpProblemSolution(rtb,
            "Problem: Text is too small/large",
            "• Use Windows display scaling settings" & vbCrLf &
            "• Adjust Windows text size" & vbCrLf &
            "• Try different theme (Light/Dark)" & vbCrLf &
            "• Maximize window for more space")

        AddHelpProblemSolution(rtb,
            "Problem: Colors look wrong",
            "• Toggle between Light and Dark themes" & vbCrLf &
            "• Check Windows high contrast settings" & vbCrLf &
            "• Verify graphics drivers are updated" & vbCrLf &
            "• Restart application")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Performance Issues", Color.DarkOliveGreen)

        AddHelpProblemSolution(rtb,
            "Problem: Application runs slowly",
            "• Close unused tabs" & vbCrLf &
            "• Clear old results before new calculations" & vbCrLf &
            "• Restart application if running for extended period" & vbCrLf &
            "• Check system resources (RAM, CPU)")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Getting Help", Color.DarkOliveGreen)
        AddHelpText(rtb, "If problems persist:")
        AddHelpBullet(rtb, "Check error log file in application folder")
        AddHelpBullet(rtb, "Note exact error messages")
        AddHelpBullet(rtb, "Try restarting the application")
        AddHelpBullet(rtb, "Contact support with screenshots and error logs")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? Error logs are automatically created in the Logs folder for debugging!")
    End Sub

    ''' <summary>
    ''' Shows version information
    ''' </summary>
    Private Sub ShowVersionHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Version Information", Color.DarkBlue)

        Dim assemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version

        AddHelpSection(rtb, $"Woodworker's Friend v{Version}", $"Assembly Version: {assemblyVersion}", Color.DarkGreen)

        AddHelpSubtitle(rtb, "System Information", Color.DarkOliveGreen)
        AddHelpBullet(rtb, $"Application: {AppName}")
        AddHelpBullet(rtb, $"Version: {Version}")
        AddHelpBullet(rtb, $"Build: {assemblyVersion}")
        AddHelpBullet(rtb, $".NET Framework: {Environment.Version}")
        AddHelpBullet(rtb, $"Operating System: {Environment.OSVersion}")
        AddHelpBullet(rtb, $"64-bit: {Environment.Is64BitOperatingSystem}")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Features in This Version", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "? Drawer height calculator with 10 calculation methods")
        AddHelpBullet(rtb, "? Cabinet door calculator for inset and overlay")
        AddHelpBullet(rtb, "? Board feet calculator with grid support")
        AddHelpBullet(rtb, "? Epoxy pour volume calculator")
        AddHelpBullet(rtb, "? Polygon calculator with visual display")
        AddHelpBullet(rtb, "? Unit conversion tools")
        AddHelpBullet(rtb, "? Table tipping force calculator")
        AddHelpBullet(rtb, "? Dark/Light theme support")
        AddHelpBullet(rtb, "? Export to CSV, Text, HTML")
        AddHelpBullet(rtb, "? Preset configurations")
        AddHelpBullet(rtb, "? Comprehensive input validation")
        AddHelpBullet(rtb, "? Error logging and handling")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Technical Details", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Language: Visual Basic .NET")
        AddHelpBullet(rtb, "UI Framework: Windows Forms")
        AddHelpBullet(rtb, "Architecture: Modular with utility libraries")
        AddHelpBullet(rtb, "Pattern: Partial classes for organization")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? Check the 'Recent Updates' section to see what's new in this version!")
    End Sub

    ''' <summary>
    ''' Shows recent updates
    ''' </summary>
    Private Sub ShowUpdatesHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Recent Updates", Color.DarkBlue)

        AddHelpSection(rtb, "What's New", "Recent enhancements and improvements to Woodworker's Friend.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Latest Update - " & DateTime.Now.ToString("MMMM yyyy"), Color.DarkOliveGreen)

        AddHelpUpdateCategory(rtb, "?? User Interface", Color.FromArgb(230, 230, 250))
        AddHelpBullet(rtb, "Added comprehensive Help system with navigation")
        AddHelpBullet(rtb, "Improved visual feedback for validation")
        AddHelpBullet(rtb, "Enhanced Dark theme support")
        AddHelpBullet(rtb, "Color-coded help sections")

        AddHelpNewLine(rtb)
        AddHelpUpdateCategory(rtb, "?? Core Improvements", Color.FromArgb(240, 255, 240))
        AddHelpBullet(rtb, "Centralized unit conversion constants")
        AddHelpBullet(rtb, "Standardized error handling and logging")
        AddHelpBullet(rtb, "Improved input validation service")
        AddHelpBullet(rtb, "Enhanced label formatting utilities")
        AddHelpBullet(rtb, "Added reentrancy guards")
        AddHelpBullet(rtb, "Calculation caching for performance")

        AddHelpNewLine(rtb)
        AddHelpUpdateCategory(rtb, "?? New Features", Color.FromArgb(255, 250, 205))
        AddHelpBullet(rtb, "Export functionality (CSV, Text, HTML)")
        AddHelpBullet(rtb, "Undo/Redo framework (infrastructure)")
        AddHelpBullet(rtb, "Dependency injection via ManagerFactory")
        AddHelpBullet(rtb, "Validation rules module")
        AddHelpBullet(rtb, "Enhanced fraction parsing")

        AddHelpNewLine(rtb)
        AddHelpUpdateCategory(rtb, "?? Bug Fixes", Color.FromArgb(255, 240, 240))
        AddHelpBullet(rtb, "Fixed FormatException in epoxy calculator")
        AddHelpBullet(rtb, "Corrected area calculation in TopCoat")
        AddHelpBullet(rtb, "Fixed reentrancy issues in area updates")
        AddHelpBullet(rtb, "Removed unnecessary null checks")

        AddHelpNewLine(rtb)
        AddHelpUpdateCategory(rtb, "? Performance", Color.FromArgb(250, 250, 230))
        AddHelpBullet(rtb, "Optimized polygon rendering with caching")
        AddHelpBullet(rtb, "Reduced redundant calculations")
        AddHelpBullet(rtb, "Improved startup time")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Coming Soon", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "?? Undo/Redo UI implementation")
        AddHelpBullet(rtb, "?? Project save/load functionality")
        AddHelpBullet(rtb, "?? PDF export")
        AddHelpBullet(rtb, "?? More calculation presets")
        AddHelpBullet(rtb, "?? Cloud sync (optional)")
        AddHelpBullet(rtb, "?? Cut list generator")
        AddHelpBullet(rtb, "?? Material cost calculator")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? Have suggestions? We love feedback! Check the Credits section for contact information.")
    End Sub

    ''' <summary>
    ''' Shows credits
    ''' </summary>
    Private Sub ShowCreditsHelp(rtb As RichTextBox)
        AddHelpTitle(rtb, "?? Credits & Acknowledgments", Color.DarkBlue)

        AddHelpSection(rtb, "Woodworker's Friend", "A comprehensive woodworking calculator and planning tool.", Color.DarkGreen)

        AddHelpSubtitle(rtb, "Development", Color.DarkOliveGreen)
        AddHelpText(rtb, "Developed with passion for woodworking and software craftsmanship.")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Technologies", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Visual Basic .NET")
        AddHelpBullet(rtb, "Windows Forms")
        AddHelpBullet(rtb, ".NET Framework / .NET Core")
        AddHelpBullet(rtb, "Visual Studio")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Special Thanks", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "Woodworking community for feedback and suggestions")
        AddHelpBullet(rtb, "Beta testers for finding bugs and usability issues")
        AddHelpBullet(rtb, "Contributors to open-source libraries")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "License", Color.DarkOliveGreen)
        AddHelpText(rtb, GetCopyrightNotice())
        AddHelpNewLine(rtb)
        AddHelpText(rtb, "This software is provided 'as-is' without any warranty. Always verify calculations and use appropriate safety measures in your woodworking projects.")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Contact & Support", Color.DarkOliveGreen)
        AddHelpBullet(rtb, "GitHub: https://github.com/dmaidon/Woodworkers-Friend")
        AddHelpBullet(rtb, "Report issues on GitHub Issues page")
        AddHelpBullet(rtb, "Suggest features via GitHub Discussions")

        AddHelpNewLine(rtb)
        AddHelpSubtitle(rtb, "Open Source", Color.DarkOliveGreen)
        AddHelpText(rtb, "This project is open source and welcomes contributions!")
        AddHelpBullet(rtb, "Fork the repository")
        AddHelpBullet(rtb, "Submit pull requests")
        AddHelpBullet(rtb, "Report bugs")
        AddHelpBullet(rtb, "Suggest enhancements")
        AddHelpBullet(rtb, "Improve documentation")

        AddHelpNewLine(rtb)
        AddHelpNote(rtb, "?? Thank you for using Woodworker's Friend! Happy woodworking! ????")
    End Sub

#End Region

#Region "Help Formatting Helper Methods"

    ''' <summary>
    ''' Adds a major title to help content
    ''' </summary>
    Private Sub AddHelpTitle(rtb As RichTextBox, text As String, color As Color)
        rtb.SelectionFont = New Font("Segoe UI", 16, FontStyle.Bold)
        rtb.SelectionColor = color
        rtb.AppendText(text & vbCrLf)
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
        rtb.SelectionColor = Color.Black
        rtb.AppendText(New String("?"c, 60) & vbCrLf & vbCrLf)
    End Sub

    ''' <summary>
    ''' Adds a section with title and description
    ''' </summary>
    Private Sub AddHelpSection(rtb As RichTextBox, title As String, description As String, color As Color)
        rtb.SelectionFont = New Font("Segoe UI", 12, FontStyle.Bold)
        rtb.SelectionColor = color
        rtb.AppendText(title & vbCrLf)
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
        rtb.SelectionColor = Color.Black
        rtb.AppendText(description & vbCrLf & vbCrLf)
    End Sub

    ''' <summary>
    ''' Adds a subtitle
    ''' </summary>
    Private Sub AddHelpSubtitle(rtb As RichTextBox, text As String, color As Color)
        rtb.SelectionFont = New Font("Segoe UI", 11, FontStyle.Bold)
        rtb.SelectionColor = color
        rtb.AppendText(text & vbCrLf)
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
        rtb.SelectionColor = Color.Black
    End Sub

    ''' <summary>
    ''' Adds normal text
    ''' </summary>
    Private Sub AddHelpText(rtb As RichTextBox, text As String)
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
        rtb.SelectionColor = Color.Black
        rtb.AppendText(text & vbCrLf)
    End Sub

    ''' <summary>
    ''' Adds a bullet point
    ''' </summary>
    Private Sub AddHelpBullet(rtb As RichTextBox, text As String)
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
        rtb.SelectionColor = Color.Black
        rtb.AppendText("  • " & text & vbCrLf)
    End Sub

    ''' <summary>
    ''' Adds a numbered item
    ''' </summary>
    Private Sub AddHelpNumbered(rtb As RichTextBox, number As Integer, text As String)
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Bold)
        rtb.SelectionColor = Color.DarkBlue
        rtb.AppendText($"  {number}. ")
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
        rtb.SelectionColor = Color.Black
        rtb.AppendText(text & vbCrLf)
    End Sub

    ''' <summary>
    ''' Adds a step in a process
    ''' </summary>
    Private Sub AddHelpStep(rtb As RichTextBox, stepNumber As Integer, title As String, description As String)
        rtb.SelectionFont = New Font("Segoe UI", 11, FontStyle.Bold)
        rtb.SelectionColor = Color.DarkGreen
        rtb.AppendText($"Step {stepNumber}: {title}" & vbCrLf)
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
        rtb.SelectionColor = Color.Black
        rtb.AppendText($"  {description}" & vbCrLf & vbCrLf)
    End Sub

    ''' <summary>
    ''' Adds a colored method/feature box
    ''' </summary>
    Private Sub AddHelpMethodBox(rtb As RichTextBox, title As String, description As String, bgColor As Color)
        Dim startPos = rtb.TextLength
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Bold)
        rtb.SelectionColor = Color.DarkBlue
        rtb.AppendText($"? {title}" & vbCrLf)
        rtb.SelectionFont = New Font("Segoe UI", 9, FontStyle.Regular)
        rtb.SelectionColor = Color.DimGray
        rtb.AppendText($"  {description}" & vbCrLf)
        rtb.SelectionBackColor = bgColor
        rtb.Select(startPos, rtb.TextLength - startPos)
        rtb.SelectionBackColor = bgColor
        rtb.SelectionLength = 0
        rtb.SelectionBackColor = Color.White
        rtb.AppendText(vbCrLf)
    End Sub

    ''' <summary>
    ''' Adds a color box with label
    ''' </summary>
    Private Sub AddHelpColorBox(rtb As RichTextBox, label As String, color As Color, description As String)
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Bold)
        rtb.SelectionBackColor = color
        rtb.SelectionColor = If(color.GetBrightness() > 0.5, Color.Black, Color.White)
        rtb.AppendText($"  {label}  ")
        rtb.SelectionBackColor = Color.White
        rtb.SelectionFont = New Font("Segoe UI", 9, FontStyle.Regular)
        rtb.SelectionColor = Color.Black
        rtb.AppendText($" - {description}" & vbCrLf)
    End Sub

    ''' <summary>
    ''' Adds a note box
    ''' </summary>
    Private Sub AddHelpNote(rtb As RichTextBox, text As String)
        Dim startPos = rtb.TextLength
        rtb.SelectionFont = New Font("Segoe UI", 9, FontStyle.Italic)
        rtb.SelectionColor = Color.DarkGreen
        rtb.AppendText(text & vbCrLf)
        rtb.Select(startPos, rtb.TextLength - startPos)
        rtb.SelectionBackColor = Color.FromArgb(240, 255, 240)
        rtb.SelectionLength = 0
        rtb.SelectionBackColor = Color.White
    End Sub

    ''' <summary>
    ''' Adds a warning box
    ''' </summary>
    Private Sub AddHelpWarning(rtb As RichTextBox, text As String)
        Dim startPos = rtb.TextLength
        rtb.SelectionFont = New Font("Segoe UI", 9, FontStyle.Bold)
        rtb.SelectionColor = Color.DarkRed
        rtb.AppendText(text & vbCrLf)
        rtb.Select(startPos, rtb.TextLength - startPos)
        rtb.SelectionBackColor = Color.FromArgb(255, 240, 240)
        rtb.SelectionLength = 0
        rtb.SelectionBackColor = Color.White
    End Sub

    ''' <summary>
    ''' Adds a formula box
    ''' </summary>
    Private Sub AddHelpFormula(rtb As RichTextBox, formula As String)
        Dim startPos = rtb.TextLength
        rtb.SelectionFont = New Font("Courier New", 10, FontStyle.Bold)
        rtb.SelectionColor = Color.DarkBlue
        rtb.AppendText($"  {formula}" & vbCrLf)
        rtb.Select(startPos, rtb.TextLength - startPos)
        rtb.SelectionBackColor = Color.FromArgb(245, 245, 250)
        rtb.SelectionLength = 0
        rtb.SelectionBackColor = Color.White
    End Sub

    ''' <summary>
    ''' Adds a keyboard shortcut
    ''' </summary>
    Private Sub AddShortcut(rtb As RichTextBox, keys As String, description As String)
        rtb.SelectionFont = New Font("Segoe UI", 9, FontStyle.Bold)
        rtb.SelectionColor = Color.White
        rtb.SelectionBackColor = Color.DarkBlue
        rtb.AppendText($" {keys} ")
        rtb.SelectionBackColor = Color.White
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
        rtb.SelectionColor = Color.Black
        rtb.AppendText($"  {description}" & vbCrLf)
    End Sub

    ''' <summary>
    ''' Adds a problem/solution box
    ''' </summary>
    Private Sub AddHelpProblemSolution(rtb As RichTextBox, problem As String, solution As String)
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Bold)
        rtb.SelectionColor = Color.DarkRed
        rtb.AppendText(problem & vbCrLf)
        rtb.SelectionFont = New Font("Segoe UI", 9, FontStyle.Regular)
        rtb.SelectionColor = Color.DarkGreen
        rtb.AppendText(solution & vbCrLf & vbCrLf)
    End Sub

    ''' <summary>
    ''' Adds an update category
    ''' </summary>
    Private Sub AddHelpUpdateCategory(rtb As RichTextBox, category As String, bgColor As Color)
        Dim startPos = rtb.TextLength
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Bold)
        rtb.SelectionColor = Color.DarkBlue
        rtb.AppendText(category & vbCrLf)
        rtb.Select(startPos, rtb.TextLength - startPos)
        rtb.SelectionBackColor = bgColor
        rtb.SelectionLength = 0
        rtb.SelectionBackColor = Color.White
    End Sub

    ''' <summary>
    ''' Adds a blank line
    ''' </summary>
    Private Sub AddHelpNewLine(rtb As RichTextBox)
        rtb.AppendText(vbCrLf)
    End Sub

#End Region

#End Region

End Class
