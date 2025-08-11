Imports System.Text

Friend Module DoorCalculationRoutines

    ''' <summary>
    ''' Calculates door rail, stile, and panel dimensions
    ''' </summary>
    Public Function CalculateDoorComponents(parameters As DoorCalculationParameters) As DoorCalculationResult
        Try
            ' Validate parameters
            Dim validationResult As ValidationResult = ValidateDoorParameters(parameters)
            If Not validationResult.IsValid Then
                Return New DoorCalculationResult() With {
                    .IsValid = False,
                    .ErrorMessage = validationResult.ErrorMessage
                }
            End If

            Dim result As New DoorCalculationResult()

            ' Calculate door dimensions
            CalculateDoorDimensions(parameters, result)

            ' Calculate rail and stile lengths
            CalculateRailAndStileDimensions(parameters, result)

            ' Calculate panel dimensions
            CalculatePanelDimensions(parameters, result)

            ' Set units and generate report
            result.Unit = If(parameters.Scale = MeasurementScale.Imperial, "in", "mm")
            result.Details = GenerateDoorReport(parameters, result)

            Return result
        Catch ex As Exception
            Return New DoorCalculationResult() With {
                .IsValid = False,
                .ErrorMessage = $"Calculation error: {ex.Message}"
            }
        End Try
    End Function

    ''' <summary>
    ''' Calculates overall door dimensions
    ''' </summary>
    Private Sub CalculateDoorDimensions(parameters As DoorCalculationParameters, result As DoorCalculationResult)
        If parameters.IsOverlay Then
            ' Overlay doors: Door size = opening size + overlay amount
            result.DoorHeight = parameters.CabinetOpeningHeight + (2 * parameters.DoorOverlay)

            If parameters.IsTwoDoor Then
                ' Two overlay doors: each door gets half the width + overlay, minus gap between doors
                result.DoorWidth = (parameters.CabinetOpeningWidth + (2 * parameters.DoorOverlay) - parameters.GapSize) / 2
                result.NumberOfDoors = 2
            Else
                ' Single overlay door: full width + overlay
                result.DoorWidth = parameters.CabinetOpeningWidth + (2 * parameters.DoorOverlay)
                result.NumberOfDoors = 1
            End If
        Else
            ' Inset doors: Door size = opening size minus gaps
            If parameters.IsTwoDoor Then
                result.DoorHeight = parameters.CabinetOpeningHeight - (2 * parameters.GapSize)
                result.DoorWidth = (parameters.CabinetOpeningWidth - (3 * parameters.GapSize)) / 2
                result.NumberOfDoors = 2
            Else
                result.DoorHeight = parameters.CabinetOpeningHeight - (2 * parameters.GapSize)
                result.DoorWidth = parameters.CabinetOpeningWidth - (2 * parameters.GapSize)
                result.NumberOfDoors = 1
            End If
        End If
    End Sub

    ''' <summary>
    ''' Validates door calculation parameters
    ''' </summary>
    Private Function ValidateDoorParameters(parameters As DoorCalculationParameters) As ValidationResult
        Dim result As New ValidationResult()

        If parameters.CabinetOpeningHeight <= 0 Then
            result.AddError("Cabinet opening height must be greater than 0")
        End If

        If parameters.CabinetOpeningWidth <= 0 Then
            result.AddError("Cabinet opening width must be greater than 0")
        End If

        If parameters.StileWidth <= 0 Then
            result.AddError("Stile width must be greater than 0")
        End If

        If parameters.RailWidth <= 0 Then
            result.AddError("Rail width must be greater than 0")
        End If

        If parameters.PanelGrooveDepth <= 0 Then
            result.AddError("Panel groove depth must be greater than 0")
        End If

        If parameters.PanelExpansionGap < 0 Then
            result.AddError("Panel expansion gap cannot be negative")
        End If

        ' Validate gap size based on door configuration
        If parameters.IsTwoDoor Then
            If parameters.GapSize < 0 Then
                result.AddError("Gap between doors cannot be negative")
            End If
            If parameters.GapSize > parameters.CabinetOpeningWidth / 4 Then
                result.AddError("Gap between doors is too large relative to cabinet width")
            End If
        End If

        ' Validate overlay amount for overlay doors
        If parameters.IsOverlay Then
            If parameters.DoorOverlay < 0 Then
                result.AddError("Door overlay amount cannot be negative")
            End If
            If parameters.DoorOverlay > parameters.CabinetOpeningWidth / 4 Then
                result.AddError("Door overlay amount seems excessive")
            End If
        End If

        ' Calculate minimum door dimensions to ensure they're reasonable
        Dim calculatedDoorWidth As Double
        Dim calculatedDoorHeight As Double

        If parameters.IsOverlay Then
            calculatedDoorHeight = parameters.CabinetOpeningHeight + (2 * parameters.DoorOverlay)
            If parameters.IsTwoDoor Then
                calculatedDoorWidth = (parameters.CabinetOpeningWidth + (2 * parameters.DoorOverlay) - parameters.GapSize) / 2
            Else
                calculatedDoorWidth = parameters.CabinetOpeningWidth + (2 * parameters.DoorOverlay)
            End If
        Else
            If parameters.IsTwoDoor Then
                calculatedDoorHeight = parameters.CabinetOpeningHeight - (2 * parameters.GapSize)
                calculatedDoorWidth = (parameters.CabinetOpeningWidth - (3 * parameters.GapSize)) / 2
            Else
                calculatedDoorHeight = parameters.CabinetOpeningHeight - (2 * parameters.GapSize)
                calculatedDoorWidth = parameters.CabinetOpeningWidth - (2 * parameters.GapSize)
            End If
        End If

        ' Check if stiles/rails are too large for calculated door size
        If parameters.StileWidth * 2 >= calculatedDoorWidth Then
            result.AddError("Stile width is too large for the calculated door width")
        End If

        If parameters.RailWidth * 2 >= calculatedDoorHeight Then
            result.AddError("Rail width is too large for the calculated door height")
        End If

        Return result
    End Function

    ''' <summary>
    ''' Calculates rail and stile lengths
    ''' </summary>
    Private Sub CalculateRailAndStileDimensions(parameters As DoorCalculationParameters, result As DoorCalculationResult)
        ' Stile length = door height (full height)
        result.StileLength = result.DoorHeight

        ' Rail length = door width minus width of both stiles
        result.RailLength = result.DoorWidth - (2 * parameters.StileWidth)

        ' Ensure rail length is positive
        If result.RailLength <= 0 Then
            result.IsValid = False
            result.ErrorMessage = "Calculated rail length is zero or negative. Reduce stile width."
        End If
    End Sub

    ''' <summary>
    ''' Calculates panel dimensions
    ''' </summary>
    Private Sub CalculatePanelDimensions(parameters As DoorCalculationParameters, result As DoorCalculationResult)
        ' Panel width = door width - both stiles + groove depths - expansion gaps
        result.PanelWidth = result.DoorWidth - (2 * parameters.StileWidth) + (2 * parameters.PanelGrooveDepth) - (2 * parameters.PanelExpansionGap)

        ' Panel height = door height - both rails + groove depths - expansion gaps
        result.PanelHeight = result.DoorHeight - (2 * parameters.RailWidth) + (2 * parameters.PanelGrooveDepth) - (2 * parameters.PanelExpansionGap)

        ' Ensure panel dimensions are positive
        If result.PanelWidth <= 0 Then
            result.IsValid = False
            result.ErrorMessage = "Calculated panel width is zero or negative. Check stile width and groove settings."
        End If

        If result.PanelHeight <= 0 Then
            result.IsValid = False
            result.ErrorMessage = "Calculated panel height is zero or negative. Check rail width and groove settings."
        End If
    End Sub

    ''' <summary>
    ''' Generates detailed door calculation report
    ''' </summary>
    Private Function GenerateDoorReport(parameters As DoorCalculationParameters, result As DoorCalculationResult) As String
        Dim report As New StringBuilder()

        report.AppendLine("DOOR CALCULATION REPORT")
        report.AppendLine("=" & New String("="c, 35))
        report.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
        report.AppendLine()

        ' Input parameters
        report.AppendLine("INPUT PARAMETERS:")
        report.AppendLine($"  Cabinet Opening: {parameters.CabinetOpeningWidth:N3} × {parameters.CabinetOpeningHeight:N3} {result.Unit}")
        report.AppendLine($"  Door Configuration: {If(parameters.IsTwoDoor, "Two Doors", "Single Door")}")
        report.AppendLine($"  Door Type: {If(parameters.IsOverlay, "Overlay", "Inset")}")

        If parameters.IsTwoDoor Then
            report.AppendLine($"  Gap Between Doors: {parameters.GapSize:N3} {result.Unit}")
        End If

        If parameters.IsOverlay Then
            report.AppendLine($"  Door Overlay: {parameters.DoorOverlay:N3} {result.Unit}")
        End If

        report.AppendLine($"  Stile Width: {parameters.StileWidth:N3} {result.Unit}")
        report.AppendLine($"  Rail Width: {parameters.RailWidth:N3} {result.Unit}")
        report.AppendLine($"  Panel Groove Depth: {parameters.PanelGrooveDepth:N3} {result.Unit}")
        report.AppendLine($"  Panel Expansion Gap: {parameters.PanelExpansionGap:N3} {result.Unit}")
        report.AppendLine()

        ' Calculated dimensions
        report.AppendLine("CALCULATED DIMENSIONS:")
        report.AppendLine($"  Number of Doors: {result.NumberOfDoors}")
        report.AppendLine($"  Individual Door Size: {result.DoorWidth:N3} × {result.DoorHeight:N3} {result.Unit}")

        If parameters.IsOverlay Then
            report.AppendLine($"  Door extends {parameters.DoorOverlay:N3} {result.Unit} beyond opening on all sides")
        End If

        report.AppendLine()

        report.AppendLine("COMPONENT DIMENSIONS:")
        report.AppendLine($"  Rail Length: {result.RailLength:N3} {result.Unit}")
        report.AppendLine($"  Stile Length: {result.StileLength:N3} {result.Unit}")
        report.AppendLine($"  Panel Width: {result.PanelWidth:N3} {result.Unit}")
        report.AppendLine($"  Panel Height: {result.PanelHeight:N3} {result.Unit}")
        report.AppendLine()

        ' Material requirements
        report.AppendLine("MATERIAL REQUIREMENTS:")
        Dim railsNeeded As Integer = result.NumberOfDoors * 2
        Dim stilesNeeded As Integer = result.NumberOfDoors * 2
        Dim panelsNeeded As Integer = result.NumberOfDoors

        report.AppendLine($"  Rails Needed: {railsNeeded} pieces @ {result.RailLength:N3} {result.Unit} long")
        report.AppendLine($"  Stiles Needed: {stilesNeeded} pieces @ {result.StileLength:N3} {result.Unit} long")
        report.AppendLine($"  Panels Needed: {panelsNeeded} pieces @ {result.PanelWidth:N3} × {result.PanelHeight:N3} {result.Unit}")

        Return report.ToString()
    End Function

End Module