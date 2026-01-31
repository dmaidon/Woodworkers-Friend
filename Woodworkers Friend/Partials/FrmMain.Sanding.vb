''' <summary>
''' Partial class for FrmMain - Sanding Grit Progression Calculator
''' Handles optimal sanding grit sequence calculations for woodworking projects
''' </summary>
Partial Public Class FrmMain

#Region "Sanding Grit Progression - Initialization"

    ''' <summary>
    ''' Initialize Sanding Grit Progression calculator controls
    ''' </summary>
    Private Sub InitializeSandingGritCalculator()
        Try
            ' Initialize Wood Type ComboBox
            CmbWoodType.Items.Clear()
            CmbWoodType.Items.AddRange({"Softwood (Pine, Fir, Cedar)", "Hardwood (Oak, Maple, Walnut)"})
            CmbWoodType.SelectedIndex = 1 ' Default to Hardwood

            ' Initialize Starting Grit ComboBox (already has items in Designer)
            If CmbStartGrit.Items.Count = 0 Then
                CmbStartGrit.Items.AddRange({"40", "60", "80", "100", "120", "150"})
            End If
            CmbStartGrit.SelectedIndex = 2 ' Default to 80

            ' Initialize Final Grit ComboBox (already has items in Designer)
            If CmbFinalGrit.Items.Count = 0 Then
                CmbFinalGrit.Items.AddRange({"150", "180", "220", "320", "400", "600"})
            End If
            CmbFinalGrit.SelectedIndex = 2 ' Default to 220

            ' Set default progression type
            RbSkipGrit.Checked = False
            RbSequential.Checked = True

            ' Initialize description label
            LblOptimalGritSequence.Text = "Calculate optimal sanding grit sequence" & vbCrLf &
                                         vbCrLf &
                                         "This calculator helps you determine the best progression " &
                                         "of sanding grits for achieving a smooth finish on your " &
                                         "woodworking project." & vbCrLf &
                                         vbCrLf &
                                         "Select your wood type, starting grit, final grit, and " &
                                         "progression method, then click Calculate."

            ' Clear results
            LblGritResult.Text = "Progression: --"
            TxtGritNotes.Text = ""

            ErrorHandler.LogError(New Exception("Sanding Grit Progression calculator initialized"), "InitializeSandingGritCalculator")

        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeSandingGritCalculator")
            MessageBox.Show("Error initializing Sanding Grit calculator: " & ex.Message,
                          "Initialization Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Sanding Grit Progression - Calculation Logic"

    ''' <summary>
    ''' Calculate optimal sanding grit progression based on user inputs
    ''' </summary>
    Private Sub BtnCalculateGrit_Click(sender As Object, e As EventArgs) Handles BtnCalculateGrit.Click
        Try
            ' Validate inputs
            If CmbWoodType.SelectedIndex = -1 Then
                MessageBox.Show("Please select a wood type.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                CmbWoodType.Focus()
                Return
            End If

            If CmbStartGrit.SelectedIndex = -1 Then
                MessageBox.Show("Please select a starting grit.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                CmbStartGrit.Focus()
                Return
            End If

            If CmbFinalGrit.SelectedIndex = -1 Then
                MessageBox.Show("Please select a final grit.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                CmbFinalGrit.Focus()
                Return
            End If

            ' Get input values
            Dim startGrit As Integer = CInt(CmbStartGrit.SelectedItem.ToString())
            Dim finalGrit As Integer = CInt(CmbFinalGrit.SelectedItem.ToString())
            Dim skipGrits As Boolean = RbSkipGrit.Checked
            Dim isSoftwood As Boolean = CmbWoodType.SelectedIndex = 0

            ' Validate grit range
            If startGrit >= finalGrit Then
                MessageBox.Show("Starting grit must be coarser (lower number) than final grit.",
                              "Invalid Range",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning)
                Return
            End If

            ' Calculate progression
            Dim progression = CalculateGritProgression(startGrit, finalGrit, skipGrits)

            ' Display results
            DisplayGritResults(progression, skipGrits, isSoftwood, startGrit, finalGrit)

        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnCalculateGrit_Click")
            MessageBox.Show("Error calculating grit progression: " & ex.Message,
                          "Calculation Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Calculate the grit progression sequence
    ''' </summary>
    Private Function CalculateGritProgression(startGrit As Integer, finalGrit As Integer, skipGrits As Boolean) As List(Of Integer)
        ' Standard grit sequence
        Dim allGrits As Integer() = {40, 60, 80, 100, 120, 150, 180, 220, 320, 400, 600}

        Dim progression As New List(Of Integer)

        ' Get all grits in range
        For Each grit In allGrits
            If grit >= startGrit AndAlso grit <= finalGrit Then
                progression.Add(grit)
            End If
        Next

        ' If skip-grit method, remove every other grit (except first and last)
        If skipGrits AndAlso progression.Count > 2 Then
            Dim skippedProgression As New List(Of Integer)
            skippedProgression.Add(progression(0)) ' Always keep first

            ' Add every other grit from the middle
            For i As Integer = 2 To progression.Count - 2 Step 2
                skippedProgression.Add(progression(i))
            Next

            ' Always keep last
            If Not skippedProgression.Contains(progression(progression.Count - 1)) Then
                skippedProgression.Add(progression(progression.Count - 1))
            End If

            progression = skippedProgression
        End If

        Return progression
    End Function

    ''' <summary>
    ''' Display grit progression results with detailed notes
    ''' </summary>
    Private Sub DisplayGritResults(progression As List(Of Integer), skipGrits As Boolean, isSoftwood As Boolean, startGrit As Integer, finalGrit As Integer)
        Try
            ' Display progression sequence
            LblGritResult.Text = "Progression: " & String.Join(" → ", progression)
            LblGritResult.ForeColor = If(progression.Count > 0, Color.Green, Color.Red)

            ' Build detailed notes
            Dim notes As New System.Text.StringBuilder()

            notes.AppendLine("═══════════════════════════════════════")
            notes.AppendLine("SANDING GRIT PROGRESSION RESULTS")
            notes.AppendLine("═══════════════════════════════════════")
            notes.AppendLine()

            ' Method information
            notes.AppendLine("📋 Method: " & If(skipGrits, "Skip-Grit (Fast)", "Sequential (Thorough)"))
            notes.AppendLine("🌲 Wood Type: " & If(isSoftwood, "Softwood", "Hardwood"))
            notes.AppendLine("📊 Total Steps: " & progression.Count.ToString())
            notes.AppendLine()

            ' Time estimate
            Dim minTime As Integer = progression.Count * 3
            Dim maxTime As Integer = progression.Count * 5
            notes.AppendLine("⏱️ Estimated Time: " & minTime.ToString() & "-" & maxTime.ToString() & " minutes")
            notes.AppendLine()

            ' Method-specific notes
            notes.AppendLine("───────────────────────────────────────")
            If skipGrits Then
                notes.AppendLine("⚡ SKIP-GRIT METHOD")
                notes.AppendLine("───────────────────────────────────────")
                notes.AppendLine("• Faster sanding process")
                notes.AppendLine("• May leave visible scratches")
                notes.AppendLine("• Good for: Painted surfaces, stained wood")
                notes.AppendLine("• Check for cross-grain scratches between grits")
                notes.AppendLine("• Not recommended for clear finish")
            Else
                notes.AppendLine("🎯 SEQUENTIAL METHOD")
                notes.AppendLine("───────────────────────────────────────")
                notes.AppendLine("• Best finish quality")
                notes.AppendLine("• Each grit removes previous scratches")
                notes.AppendLine("• Recommended for: Clear finish, natural wood")
                notes.AppendLine("• Takes more time but better results")
            End If
            notes.AppendLine()

            ' Wood-specific recommendations
            notes.AppendLine("───────────────────────────────────────")
            If isSoftwood Then
                notes.AppendLine("🌲 SOFTWOOD TIPS")
                notes.AppendLine("───────────────────────────────────────")
                notes.AppendLine("• Don't skip grits - shows scratches easily")
                notes.AppendLine("• Use light pressure to avoid dishing")
                notes.AppendLine("• Sand with the grain direction")
                notes.AppendLine("• Watch for raised grain after first grit")
                notes.AppendLine("• Consider wet-sanding between coats")
            Else
                notes.AppendLine("🪵 HARDWOOD TIPS")
                notes.AppendLine("───────────────────────────────────────")
                notes.AppendLine("• Can skip grits if needed")
                notes.AppendLine("• Higher final grit = better clarity")
                notes.AppendLine("• Check for mill marks before sanding")
                notes.AppendLine("• Use card scraper for difficult grain")
                notes.AppendLine("• Final grit shows grain figure best")
            End If
            notes.AppendLine()

            ' Grit descriptions
            notes.AppendLine("───────────────────────────────────────")
            notes.AppendLine("GRIT REFERENCE GUIDE")
            notes.AppendLine("───────────────────────────────────────")
            notes.AppendLine()

            For Each grit In progression
                notes.AppendLine(GetGritDescription(grit))
            Next

            notes.AppendLine()
            notes.AppendLine("───────────────────────────────────────")
            notes.AppendLine("⚠️ SAFETY REMINDERS")
            notes.AppendLine("───────────────────────────────────────")
            notes.AppendLine("• Always wear dust mask/respirator")
            notes.AppendLine("• Use dust collection or work outdoors")
            notes.AppendLine("• Check direction of sanding marks")
            notes.AppendLine("• Test finish on scrap wood first")
            notes.AppendLine("• Clean surface between grits")
            notes.AppendLine()

            ' Display notes
            TxtGritNotes.Text = notes.ToString()

            ErrorHandler.LogError(New Exception($"Grit progression calculated: {String.Join(" → ", progression)}"), "DisplayGritResults")

        Catch ex As Exception
            ErrorHandler.LogError(ex, "DisplayGritResults")
            TxtGritNotes.Text = "Error displaying results: " & ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Get description for a specific grit number
    ''' </summary>
    Private Function GetGritDescription(grit As Integer) As String
        Select Case grit
            Case 40
                Return "• 40 Grit: Extra coarse - Heavy stock removal, shaping"
            Case 60
                Return "• 60 Grit: Coarse - Remove mill marks, rough shaping"
            Case 80
                Return "• 80 Grit: Medium-coarse - Smooth rough surfaces"
            Case 100
                Return "• 100 Grit: Medium - General smoothing, prep work"
            Case 120
                Return "• 120 Grit: Medium-fine - Standard starting point"
            Case 150
                Return "• 150 Grit: Fine - Pre-finish prep for most projects"
            Case 180
                Return "• 180 Grit: Fine - Final prep for stain or paint"
            Case 220
                Return "• 220 Grit: Very fine - Final prep for clear finish"
            Case 320
                Return "• 320 Grit: Extra fine - Between finish coats"
            Case 400
                Return "• 400 Grit: Ultra fine - Final smoothing, wet sanding"
            Case 600
                Return "• 600 Grit: Ultra fine - High-gloss finish prep"
            Case Else
                Return "• " & grit.ToString() & " Grit"
        End Select
    End Function

#End Region

#Region "Sanding Grit Progression - Event Handlers"

    ''' <summary>
    ''' Auto-calculate when selection changes (optional convenience)
    ''' </summary>
    Private Sub SandingInput_Changed(sender As Object, e As EventArgs) _
        Handles CmbStartGrit.SelectedIndexChanged,
                CmbFinalGrit.SelectedIndexChanged,
                RbSkipGrit.CheckedChanged,
                RbSequential.CheckedChanged

        ' Auto-calculate if all inputs are valid
        If CmbWoodType.SelectedIndex <> -1 AndAlso
           CmbStartGrit.SelectedIndex <> -1 AndAlso
           CmbFinalGrit.SelectedIndex <> -1 Then

            ' Optional: Uncomment to enable auto-calculate
            ' BtnCalculateGrit_Click(sender, e)
        End If
    End Sub

#End Region

End Class
