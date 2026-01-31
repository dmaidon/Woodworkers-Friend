' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation - Dado Stack Calculator implementation
'          Includes tooltips, error handling, context menu, and preset management
' ============================================================================

Imports System.Text

Partial Public Class FrmMain

#Region "Dado Stack Calculator"

    Private _dadoPresets As New Dictionary(Of String, DadoSetConfiguration)
    Private _currentDadoResult As DadoStackResult = Nothing
    Private _dadoContextMenu As ContextMenuStrip = Nothing
    Private _dadoErrorProvider As ErrorProvider = Nothing
    Private _dadoToolTip As ToolTip = Nothing

    ''' <summary>
    ''' Initialize the Dado Stack Calculator
    ''' </summary>
    Private Sub InitializeDadoStackCalculator()
        Try
            ' Initialize error provider
            _dadoErrorProvider = New ErrorProvider With {
                .BlinkStyle = ErrorBlinkStyle.NeverBlink
            }

            ' Initialize tooltips
            InitializeDadoTooltips()

            ' Initialize context menu
            InitializeDadoContextMenu()

            ' Configure NumericUpDowns
            ConfigureDadoNumericControls()

            ' Load default presets
            LoadDefaultDadoPresets()

            ' Wire up events
            AddHandler NudDesiredWidth.ValueChanged, AddressOf DadoStackInput_Changed
            AddHandler CboDadoUnits.SelectedIndexChanged, AddressOf DadoStackInput_Changed
            AddHandler NudKerfWidth.ValueChanged, AddressOf DadoStackInput_Changed
            AddHandler chklstAvailableChippers.ItemCheck, AddressOf ChipperList_ItemCheck
            AddHandler BtnDadoCalculate.Click, AddressOf BtnDadoCalculate_Click
            AddHandler BtnDadoReset.Click, AddressOf BtnDadoReset_Click
            AddHandler BtnCopyResults.Click, AddressOf BtnCopyResults_Click
            AddHandler BtnAddCustom.Click, AddressOf BtnAddCustom_Click
            AddHandler LstBladeStack.MouseDown, AddressOf LstBladeStack_MouseDown

            ' Set default values
            ResetDadoCalculator()
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "InitializeDadoStackCalculator", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Configure numeric controls with proper ranges and increments
    ''' </summary>
    Private Sub ConfigureDadoNumericControls()
        ' Desired Width
        NudDesiredWidth.DecimalPlaces = 4
        NudDesiredWidth.Increment = 0.0625D ' 1/16"
        NudDesiredWidth.Minimum = 0.125D
        NudDesiredWidth.Maximum = 2D
        NudDesiredWidth.Value = 0.5D

        ' Kerf Width
        NudKerfWidth.DecimalPlaces = 4
        NudKerfWidth.Increment = 0.001D
        NudKerfWidth.Minimum = 0.001D
        NudKerfWidth.Maximum = 0.25D
        NudKerfWidth.Value = 0.125D

        ' Units ComboBox
        CboDadoUnits.Items.Clear()
        CboDadoUnits.Items.AddRange({"Inches", "Millimeters"})
        CboDadoUnits.DropDownStyle = ComboBoxStyle.DropDownList
        CboDadoUnits.SelectedIndex = 0

        ' Set monospace font for results
        LstBladeStack.Font = New Font("Consolas", 9.0F, FontStyle.Regular)
        LstBladeStack.IntegralHeight = False
    End Sub

    ''' <summary>
    ''' Initialize tooltips for all controls
    ''' </summary>
    Private Sub InitializeDadoTooltips()
        _dadoToolTip = New ToolTip With {
            .AutoPopDelay = 5000,
            .InitialDelay = 500,
            .ReshowDelay = 100
        }

        _dadoToolTip.SetToolTip(NudDesiredWidth, "The target width you want to cut with the dado stack")
        _dadoToolTip.SetToolTip(CboDadoUnits, "Choose measurement units (Inches or Millimeters)")
        _dadoToolTip.SetToolTip(NudKerfWidth, "The thickness of each blade/chipper kerf (usually 0.125"")")
        _dadoToolTip.SetToolTip(chklstAvailableChippers, "Check the blades and chippers available in your dado set")
        _dadoToolTip.SetToolTip(BtnAddCustom, "Add a custom blade size not in the standard list")
        _dadoToolTip.SetToolTip(BtnDadoCalculate, "Calculate the optimal blade combination for your desired width")
        _dadoToolTip.SetToolTip(BtnDadoReset, "Reset all inputs to default values")
        _dadoToolTip.SetToolTip(BtnCopyResults, "Copy the blade combination to the clipboard")
        _dadoToolTip.SetToolTip(LstBladeStack, "Right-click for more options")
        _dadoToolTip.SetToolTip(TxtAlternatives, "Alternative blade combinations (if available)")
    End Sub

    ''' <summary>
    ''' Initialize context menu for results
    ''' </summary>
    Private Sub InitializeDadoContextMenu()
        _dadoContextMenu = New ContextMenuStrip()

        Dim copyItem As New ToolStripMenuItem("Copy Results")
        AddHandler copyItem.Click, AddressOf MenuCopyResults_Click

        Dim copyDetailedItem As New ToolStripMenuItem("Copy Detailed Results")
        AddHandler copyDetailedItem.Click, AddressOf MenuCopyDetailed_Click

        Dim savePresetItem As New ToolStripMenuItem("Save as Preset...")
        AddHandler savePresetItem.Click, AddressOf MenuSavePreset_Click

        Dim loadPresetItem As New ToolStripMenuItem("Load Preset...")
        AddHandler loadPresetItem.Click, AddressOf MenuLoadPreset_Click

        Dim separator As New ToolStripSeparator()

        Dim exportItem As New ToolStripMenuItem("Export Configuration...")
        AddHandler exportItem.Click, AddressOf MenuExportConfig_Click

        _dadoContextMenu.Items.AddRange({copyItem, copyDetailedItem, separator, savePresetItem, loadPresetItem, separator, exportItem})

        LstBladeStack.ContextMenuStrip = _dadoContextMenu
    End Sub

    ''' <summary>
    ''' Load default dado set presets
    ''' </summary>
    Private Sub LoadDefaultDadoPresets()
        ' Standard 8" dado set
        Dim standard As New DadoSetConfiguration With {
            .Name = "Standard 8"" Dado Set",
            .Blades = New List(Of DadoBlade) From {
                New DadoBlade("Outer Blade Left", 0.125),
                New DadoBlade("Outer Blade Right", 0.125),
                New DadoBlade("1/16"" Chipper", 0.0625),
                New DadoBlade("1/8"" Chipper", 0.125),
                New DadoBlade("3/16"" Chipper", 0.1875),
                New DadoBlade("1/4"" Chipper", 0.25)
            }
        }
        _dadoPresets("Standard") = standard

        ' Premium set with shims
        Dim premium As New DadoSetConfiguration With {
            .Name = "Premium 8"" Dado Set with Shims",
            .Blades = New List(Of DadoBlade) From {
                New DadoBlade("Outer Blade Left", 0.125),
                New DadoBlade("Outer Blade Right", 0.125),
                New DadoBlade("1/16"" Chipper", 0.0625),
                New DadoBlade("1/8"" Chipper", 0.125),
                New DadoBlade("3/16"" Chipper", 0.1875),
                New DadoBlade("1/4"" Chipper", 0.25),
                New DadoBlade("Shim 0.004""", 0.004),
                New DadoBlade("Shim 0.008""", 0.008)
            }
        }
        _dadoPresets("Premium") = premium
    End Sub

    ''' <summary>
    ''' Calculate the dado stack combination
    ''' </summary>
    Private Sub CalculateDadoStack()
        Try
            ' Clear previous errors
            _dadoErrorProvider.Clear()

            ' Validate inputs
            If Not ValidateDadoInputs() Then
                Return
            End If

            ' Get available blades from checked list
            Dim availableBlades As New List(Of DadoBlade)
            For i As Integer = 0 To chklstAvailableChippers.Items.Count - 1
                If chklstAvailableChippers.GetItemChecked(i) Then
                    Dim item As String = chklstAvailableChippers.Items(i).ToString()
                    Dim blade = ParseBladeFromString(item)
                    If blade IsNot Nothing Then
                        availableBlades.Add(blade)
                    End If
                End If
            Next

            If availableBlades.Count = 0 Then
                MessageBox.Show("Please select at least one blade or chipper from your dado set.",
                              "No Blades Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get target width
            Dim targetWidth = CDbl(NudDesiredWidth.Value)
            If CboDadoUnits.SelectedIndex = 1 Then ' Millimeters
                targetWidth /= 25.4 ' Convert to inches
            End If

            ' Calculate the best combination
            _currentDadoResult = DadoStackCalculator.CalculateBestStack(targetWidth, availableBlades)

            ' Display results
            DisplayDadoResults(_currentDadoResult)
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "CalculateDadoStack", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Validate dado inputs
    ''' </summary>
    Private Function ValidateDadoInputs() As Boolean
        Dim isValid As Boolean = True

        If NudDesiredWidth.Value <= 0 Then
            _dadoErrorProvider.SetError(NudDesiredWidth, "Width must be greater than zero")
            isValid = False
        End If

        If NudKerfWidth.Value <= 0 Then
            _dadoErrorProvider.SetError(NudKerfWidth, "Kerf width must be greater than zero")
            isValid = False
        End If

        Return isValid
    End Function

    ''' <summary>
    ''' Parse blade information from checklist item string
    ''' </summary>
    Private Function ParseBladeFromString(item As String) As DadoBlade
        Try
            ' Expected format: "Description (0.125")"
            Dim openParen = item.LastIndexOf("("c)
            Dim closeParen = item.LastIndexOf(")"c)

            If openParen > -1 AndAlso closeParen > openParen Then
                Dim name = item.Substring(0, openParen).Trim()
                Dim widthStr = item.Substring(openParen + 1, closeParen - openParen - 1).Replace(""""c, "").Trim()
                Dim width As Double

                If Double.TryParse(widthStr, width) Then
                    Return New DadoBlade(name, width)
                End If
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ParseBladeFromString")
        End Try

        Return Nothing
    End Function

    ''' <summary>
    ''' Display the calculated dado stack results
    ''' </summary>
    Private Sub DisplayDadoResults(result As DadoStackResult)
        If result Is Nothing Then
            LblResultsummary.Text = "No solution found"
            LstBladeStack.Items.Clear()
            TxtAlternatives.Text = ""
            Return
        End If

        ' Display summary
        Dim units As String = If(CboDadoUnits.SelectedIndex = 1, "mm", """")
        Dim displayWidth = If(CboDadoUnits.SelectedIndex = 1, result.TotalWidth * 25.4, result.TotalWidth)
        Dim displayError = If(CboDadoUnits.SelectedIndex = 1, result.ErrorAmount * 25.4, result.ErrorAmount)

        LblResultsummary.Text = $"Total Width: {displayWidth:N4}{units}  |  Error: {If(result.ErrorAmount >= 0, "+", "")}{displayError:N4}{units}"
        LblResultsummary.BackColor = If(Math.Abs(result.ErrorAmount) < 0.001, Color.LightGreen, Color.LightYellow)

        ' Display blade stack
        LstBladeStack.Items.Clear()
        LstBladeStack.Items.Add("DADO STACK ASSEMBLY")
        LstBladeStack.Items.Add("".PadRight(40, "="c))

        For i As Integer = 0 To result.Blades.Count - 1
            Dim blade = result.Blades(i)
            Dim bladeWidth = If(CboDadoUnits.SelectedIndex = 1, blade.Width * 25.4, blade.Width)
            LstBladeStack.Items.Add($"{i + 1,2}. {blade.Name,-25} ({bladeWidth:N4}{units})")
        Next

        LstBladeStack.Items.Add("".PadRight(40, "-"c))
        LstBladeStack.Items.Add($"TOTAL WIDTH: {displayWidth:N4}{units}")
        LstBladeStack.Items.Add($"ERROR:       {If(result.ErrorAmount >= 0, "+", "")}{displayError:N4}{units}")

        ' Display alternatives
        If result.Alternatives IsNot Nothing AndAlso result.Alternatives.Count > 0 Then
            Dim sb As New StringBuilder()
            sb.AppendLine("Alternative Combinations:")
            sb.AppendLine()

            For i As Integer = 0 To Math.Min(2, result.Alternatives.Count - 1)
                Dim alt = result.Alternatives(i)
                Dim altWidth = If(CboDadoUnits.SelectedIndex = 1, alt.TotalWidth * 25.4, alt.TotalWidth)
                Dim altError = If(CboDadoUnits.SelectedIndex = 1, alt.ErrorAmount * 25.4, alt.ErrorAmount)

                sb.AppendLine($"Option {i + 1}: {altWidth:N4}{units} (Error: {If(alt.ErrorAmount >= 0, "+", "")}{altError:N4}{units})")
                For Each blade In alt.Blades
                    Dim bladeWidth = If(CboDadoUnits.SelectedIndex = 1, blade.Width * 25.4, blade.Width)
                    sb.AppendLine($"  • {blade.Name} ({bladeWidth:N4}{units})")
                Next
                sb.AppendLine()
            Next

            TxtAlternatives.Text = sb.ToString()
        Else
            TxtAlternatives.Text = "No alternative combinations found within tolerance."
        End If
    End Sub

    ''' <summary>
    ''' Reset calculator to default values
    ''' </summary>
    Private Sub ResetDadoCalculator()
        NudDesiredWidth.Value = 0.5D
        CboDadoUnits.SelectedIndex = 0
        NudKerfWidth.Value = 0.125D

        ' Check standard blades
        For i As Integer = 0 To chklstAvailableChippers.Items.Count - 1
            chklstAvailableChippers.SetItemChecked(i, True)
        Next

        ' Clear results
        LblResultsummary.Text = "Total Width: 0.000"""
        LblResultsummary.BackColor = SystemColors.Control
        LstBladeStack.Items.Clear()
        TxtAlternatives.Text = ""
        _currentDadoResult = Nothing

        _dadoErrorProvider.Clear()
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub DadoStackInput_Changed(sender As Object, e As EventArgs)
        ' Auto-calculate on input change (optional)
        ' Uncomment if you want real-time calculation
        ' CalculateDadoStack()
    End Sub

    Private Sub ChipperList_ItemCheck(sender As Object, e As ItemCheckEventArgs)
        ' Ensure at least outer blades are always selected
        If e.Index = 0 AndAlso e.NewValue = CheckState.Unchecked Then
            MessageBox.Show("The outer blades must always be included in the dado stack.",
                          "Required Blades", MessageBoxButtons.OK, MessageBoxIcon.Information)
            e.NewValue = CheckState.Checked
        End If
    End Sub

    Private Sub BtnDadoCalculate_Click(sender As Object, e As EventArgs)
        CalculateDadoStack()
    End Sub

    Private Sub BtnDadoReset_Click(sender As Object, e As EventArgs)
        ResetDadoCalculator()
    End Sub

    Private Sub BtnCopyResults_Click(sender As Object, e As EventArgs)
        CopyResultsToClipboard()
    End Sub

    Private Sub BtnAddCustom_Click(sender As Object, e As EventArgs)
        AddCustomBlade()
    End Sub

    Private Sub LstBladeStack_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            LstBladeStack.SelectedIndex = LstBladeStack.IndexFromPoint(e.Location)
        End If
    End Sub

#End Region

#Region "Context Menu Handlers"

    Private Sub MenuCopyResults_Click(sender As Object, e As EventArgs)
        CopyResultsToClipboard()
    End Sub

    Private Sub MenuCopyDetailed_Click(sender As Object, e As EventArgs)
        CopyResultsToClipboard(detailed:=True)
    End Sub

    Private Sub MenuSavePreset_Click(sender As Object, e As EventArgs)
        SaveCurrentPreset()
    End Sub

    Private Sub MenuLoadPreset_Click(sender As Object, e As EventArgs)
        LoadPreset()
    End Sub

    Private Sub MenuExportConfig_Click(sender As Object, e As EventArgs)
        ExportConfiguration()
    End Sub

#End Region

#Region "Helper Methods"

    ''' <summary>
    ''' Copy results to clipboard
    ''' </summary>
    Private Sub CopyResultsToClipboard(Optional detailed As Boolean = False)
        Try
            If _currentDadoResult Is Nothing Then
                MessageBox.Show("No results to copy. Please calculate a dado stack first.",
                              "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim sb As New StringBuilder()
            sb.AppendLine("DADO STACK CALCULATOR RESULTS")
            sb.AppendLine("".PadRight(50, "="c))
            sb.AppendLine()
            sb.AppendLine($"Target Width: {NudDesiredWidth.Value:N4} {CboDadoUnits.Text}")
            sb.AppendLine($"Total Width:  {_currentDadoResult.TotalWidth:N4}""")
            sb.AppendLine($"Error:        {If(_currentDadoResult.ErrorAmount >= 0, "+", "")}{_currentDadoResult.ErrorAmount:N4}""")
            sb.AppendLine()
            sb.AppendLine("BLADE COMBINATION:")

            For i As Integer = 0 To _currentDadoResult.Blades.Count - 1
                Dim blade = _currentDadoResult.Blades(i)
                sb.AppendLine($"  {i + 1}. {blade.Name} ({blade.Width:N4}"")")
            Next

            If detailed AndAlso _currentDadoResult.Alternatives IsNot Nothing Then
                sb.AppendLine()
                sb.AppendLine("ALTERNATIVE COMBINATIONS:")
                For Each alt In _currentDadoResult.Alternatives
                    sb.AppendLine($"  Width: {alt.TotalWidth:N4}"" (Error: {alt.ErrorAmount:N4}"")")
                    For Each blade In alt.Blades
                        sb.AppendLine($"    • {blade.Name} ({blade.Width:N4}"")")
                    Next
                    sb.AppendLine()
                Next
            End If

            Clipboard.SetText(sb.ToString())
            MessageBox.Show("Results copied to clipboard!", "Success",
                          MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "CopyResultsToClipboard", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Add a custom blade size
    ''' </summary>
    Private Sub AddCustomBlade()
        Try
            Dim input As String = InputBox("Enter custom blade description and width:" & vbCrLf &
                                         "Format: Description (0.XXX"")" & vbCrLf & vbCrLf &
                                         "Example: Custom Chipper (0.156"")",
                                         "Add Custom Blade", "")

            If String.IsNullOrWhiteSpace(input) Then
                Return
            End If

            ' Try to parse the input
            Dim blade = ParseBladeFromString(input)
            If blade Is Nothing Then
                MessageBox.Show("Invalid format. Please use: Description (0.XXX"")",
                              "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Check if already exists
            For i As Integer = 0 To chklstAvailableChippers.Items.Count - 1
                If chklstAvailableChippers.Items(i).ToString().Contains(blade.Name) Then
                    MessageBox.Show("A blade with this name already exists.",
                                  "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            Next

            ' Add to list
            chklstAvailableChippers.Items.Add(input, True)

            MessageBox.Show("Custom blade added successfully!",
                          "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "AddCustomBlade", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Save current configuration as preset
    ''' </summary>
    Private Sub SaveCurrentPreset()
        Try
            Dim presetName As String = InputBox("Enter a name for this preset:",
                                              "Save Preset", "My Dado Set")

            If String.IsNullOrWhiteSpace(presetName) Then
                Return
            End If

            Dim config As New DadoSetConfiguration With {
                .Name = presetName,
                .Blades = New List(Of DadoBlade)
            }

            For i As Integer = 0 To chklstAvailableChippers.Items.Count - 1
                If chklstAvailableChippers.GetItemChecked(i) Then
                    Dim blade = ParseBladeFromString(chklstAvailableChippers.Items(i).ToString())
                    If blade IsNot Nothing Then
                        config.Blades.Add(blade)
                    End If
                End If
            Next

            _dadoPresets(presetName) = config

            MessageBox.Show($"Preset '{presetName}' saved successfully!",
                          "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "SaveCurrentPreset", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Load a saved preset
    ''' </summary>
    Private Sub LoadPreset()
        Try
            If _dadoPresets.Count = 0 Then
                MessageBox.Show("No presets available.",
                              "No Presets", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Show dialog to select preset
            Dim presetNames = _dadoPresets.Keys.ToArray()
            Dim selectedPreset As String = Nothing

            ' Simple selection (in a real app, you'd use a custom dialog)
            Dim selection As String = InputBox("Available presets:" & vbCrLf &
                                             String.Join(vbCrLf, presetNames) & vbCrLf & vbCrLf &
                                             "Enter preset name to load:",
                                             "Load Preset", presetNames(0))

            If String.IsNullOrWhiteSpace(selection) OrElse Not _dadoPresets.ContainsKey(selection) Then
                Return
            End If

            selectedPreset = selection

            ' Load the preset
            Dim config = _dadoPresets(selectedPreset)

            ' Update checklist
            For i As Integer = 0 To chklstAvailableChippers.Items.Count - 1
                chklstAvailableChippers.SetItemChecked(i, False)
            Next

            For Each blade In config.Blades
                For i As Integer = 0 To chklstAvailableChippers.Items.Count - 1
                    If chklstAvailableChippers.Items(i).ToString().Contains(blade.Name) Then
                        chklstAvailableChippers.SetItemChecked(i, True)
                        Exit For
                    End If
                Next
            Next

            MessageBox.Show($"Preset '{selectedPreset}' loaded successfully!",
                          "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "LoadPreset", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Export configuration to file
    ''' </summary>
    Private Sub ExportConfiguration()
        Try
            Dim sfd As New SaveFileDialog With {
                .Filter = "JSON Files (*.json)|*.json|Text Files (*.txt)|*.txt",
                .Title = "Export Dado Configuration",
                .FileName = "DadoSet.json"
            }

            If sfd.ShowDialog() = DialogResult.OK Then
                Dim config As New DadoSetConfiguration With {
                    .Name = "Exported Configuration",
                    .Blades = New List(Of DadoBlade)
                }

                For i As Integer = 0 To chklstAvailableChippers.Items.Count - 1
                    If chklstAvailableChippers.GetItemChecked(i) Then
                        Dim blade = ParseBladeFromString(chklstAvailableChippers.Items(i).ToString())
                        If blade IsNot Nothing Then
                            config.Blades.Add(blade)
                        End If
                    End If
                Next

                ' Simple JSON export (in a real app, use Json.NET or System.Text.Json)
                Dim sb As New StringBuilder()
                sb.AppendLine("{")
                sb.AppendLine($"  ""name"": ""{config.Name}"",")
                sb.AppendLine($"  ""blades"": [")
                For i As Integer = 0 To config.Blades.Count - 1
                    Dim blade = config.Blades(i)
                    sb.Append($"    {{""name"": ""{blade.Name}"", ""width"": {blade.Width}}}")
                    If i < config.Blades.Count - 1 Then sb.Append(","c)
                    sb.AppendLine()
                Next
                sb.AppendLine("  ]")
                sb.AppendLine("}")

                IO.File.WriteAllText(sfd.FileName, sb.ToString())

                MessageBox.Show($"Configuration exported to:{vbCrLf}{sfd.FileName}",
                              "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "ExportConfiguration", showToUser:=True)
        End Try
    End Sub

#End Region

End Class

#Region "Supporting Classes"

''' <summary>
''' Represents a dado blade or chipper
''' </summary>
Public Class DadoBlade
    Public Property Name As String
    Public Property Width As Double

    Public Sub New()
    End Sub

    Public Sub New(name As String, width As Double)
        Me.Name = name
        Me.Width = width
    End Sub

End Class

''' <summary>
''' Represents a dado set configuration
''' </summary>
Public Class DadoSetConfiguration
    Public Property Name As String
    Public Property Blades As List(Of DadoBlade)

    Public Sub New()
        Blades = New List(Of DadoBlade)
    End Sub

End Class

''' <summary>
''' Represents the result of a dado stack calculation
''' </summary>
Public Class DadoStackResult
    Public Property Blades As List(Of DadoBlade)
    Public Property TotalWidth As Double
    Public Property ErrorAmount As Double
    Public Property Alternatives As List(Of DadoStackResult)

    Public Sub New()
        Blades = New List(Of DadoBlade)
        Alternatives = New List(Of DadoStackResult)
    End Sub

End Class

''' <summary>
''' Calculator for dado stack combinations
''' </summary>
Public Class DadoStackCalculator

    ''' <summary>
    ''' Calculate the best dado stack combination
    ''' </summary>
    Public Shared Function CalculateBestStack(targetWidth As Double, availableBlades As List(Of DadoBlade)) As DadoStackResult
        Dim result As New DadoStackResult()
        Dim tolerance As Double = 0.005 ' 5 thousandths tolerance

        ' Sort blades by width (largest first)
        Dim sortedBlades = availableBlades.OrderByDescending(Function(b) b.Width).ToList()

        ' Try to find exact or closest match using dynamic programming
        Dim bestCombo = FindBestCombination(targetWidth, sortedBlades, tolerance)

        If bestCombo IsNot Nothing Then
            result.Blades = bestCombo
            result.TotalWidth = bestCombo.Sum(Function(b) b.Width)
            result.ErrorAmount = result.TotalWidth - targetWidth

            ' Find alternatives
            result.Alternatives = FindAlternativeCombinations(targetWidth, sortedBlades, bestCombo, tolerance)
        End If

        Return result
    End Function

    ''' <summary>
    ''' Find the best blade combination
    ''' </summary>
    Private Shared Function FindBestCombination(targetWidth As Double,
                                               blades As List(Of DadoBlade),
                                               tolerance As Double) As List(Of DadoBlade)
        Dim bestCombo As List(Of DadoBlade) = Nothing
        Dim bestError As Double = Double.MaxValue

        ' Ensure outer blades are included
        Dim outerBlades = blades.Where(Function(b) b.Name.Contains("outer", StringComparison.CurrentCultureIgnoreCase)).ToList()
        Dim chippers = blades.Where(Function(b) Not b.Name.Contains("outer", StringComparison.CurrentCultureIgnoreCase)).ToList()

        ' Start with outer blades
        Dim currentWidth = outerBlades.Sum(Function(b) b.Width)
        Dim remaining = targetWidth - currentWidth

        If Math.Abs(remaining) < tolerance Then
            ' Perfect match with just outer blades
            Return New List(Of DadoBlade)(outerBlades)
        End If

        ' Try combinations of chippers
        Dim chipperCombos = GenerateCombinations(chippers, remaining, tolerance)

        For Each combo In chipperCombos
            Dim totalWidth = currentWidth + combo.Sum(Function(b) b.Width)
            Dim errorValue = Math.Abs(totalWidth - targetWidth)

            If errorValue < bestError Then
                bestError = errorValue
                bestCombo = New List(Of DadoBlade)(outerBlades)
                bestCombo.AddRange(combo)
            End If

            ' If we found an exact match, stop searching
            If errorValue < 0.001 Then
                Exit For
            End If
        Next

        Return bestCombo
    End Function

    ''' <summary>
    ''' Generate all possible combinations of blades
    ''' </summary>
    Private Shared Function GenerateCombinations(blades As List(Of DadoBlade),
                                                targetWidth As Double,
                                                tolerance As Double) As List(Of List(Of DadoBlade))
        Dim results As New List(Of List(Of DadoBlade))
        Dim maxBlades As Integer = 10 ' Reasonable limit

        ' Use recursive backtracking
        GenerateCombinationsRecursive(blades, New List(Of DadoBlade), targetWidth, tolerance, maxBlades, results)

        ' Sort by total width closest to target
        Return results.OrderBy(Function(c) Math.Abs(c.Sum(Function(b) b.Width) - targetWidth)).ToList()
    End Function

    ''' <summary>
    ''' Recursive helper for combination generation
    ''' </summary>
    Private Shared Sub GenerateCombinationsRecursive(blades As List(Of DadoBlade),
                                                    current As List(Of DadoBlade),
                                                    remaining As Double,
                                                    tolerance As Double,
                                                    maxDepth As Integer,
                                                    results As List(Of List(Of DadoBlade)))
        If current.Count > maxDepth Then
            Return
        End If

        Dim currentTotal = current.Sum(Function(b) b.Width)

        ' Check if we're within tolerance
        If Math.Abs(currentTotal - remaining) <= tolerance Then
            results.Add(New List(Of DadoBlade)(current))
            Return
        End If

        ' Don't continue if we're too far over
        If currentTotal > remaining + tolerance Then
            Return
        End If

        ' Try adding each blade
        For Each blade In blades
            current.Add(blade)
            GenerateCombinationsRecursive(blades, current, remaining, tolerance, maxDepth, results)
            current.RemoveAt(current.Count - 1)
        Next
    End Sub

    ''' <summary>
    ''' Find alternative combinations
    ''' </summary>
    Private Shared Function FindAlternativeCombinations(targetWidth As Double,
                                                       blades As List(Of DadoBlade),
                                                       bestCombo As List(Of DadoBlade),
                                                       tolerance As Double) As List(Of DadoStackResult)
        Dim alternatives As New List(Of DadoStackResult)

        ' Generate more combinations and filter out the best one
        Dim allCombos = GenerateCombinations(blades, targetWidth, tolerance * 3)

        For Each combo In allCombos.Take(5)
            ' Skip if it's the same as best
            If AreCombinationsEqual(combo, bestCombo) Then
                Continue For
            End If

            Dim alt As New DadoStackResult With {
                .Blades = combo,
                .TotalWidth = combo.Sum(Function(b) b.Width)
            }
            alt.ErrorAmount = alt.TotalWidth - targetWidth

            alternatives.Add(alt)

            If alternatives.Count >= 3 Then
                Exit For
            End If
        Next

        Return alternatives
    End Function

    ''' <summary>
    ''' Check if two combinations are equal
    ''' </summary>
    Private Shared Function AreCombinationsEqual(combo1 As List(Of DadoBlade),
                                                combo2 As List(Of DadoBlade)) As Boolean
        If combo1.Count <> combo2.Count Then
            Return False
        End If

        Dim sorted1 = combo1.OrderBy(Function(b) b.Name).ThenBy(Function(b) b.Width).ToList()
        Dim sorted2 = combo2.OrderBy(Function(b) b.Name).ThenBy(Function(b) b.Width).ToList()

        For i As Integer = 0 To sorted1.Count - 1
            If sorted1(i).Name <> sorted2(i).Name OrElse
               Math.Abs(sorted1(i).Width - sorted2(i).Width) > 0.0001 Then
                Return False
            End If
        Next

        Return True
    End Function

End Class

#End Region
