' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation - Joinery calculator UI for mortise & tenon,
'          dovetails, and box joints with visual diagrams
' ============================================================================

Partial Public Class FrmMain

#Region "Joinery Calculator"

    ''' <summary>
    ''' Handles mortise and tenon calculation
    ''' </summary>
    Private Sub CalculateMortiseAndTenon()
        Try
            ' Get input values
            Dim stockThickness = InputValidator.TryParseDoubleWithDefault(TxtJointStockThickness.Text, 0.75)
            Dim stockWidth = InputValidator.TryParseDoubleWithDefault(TxtJointStockWidth.Text, 3.0)

            ' Validate
            Dim validation = JoineryCalculator.ValidateJointDimensions(stockThickness, stockWidth)
            If Not validation.IsValid Then
                MessageBox.Show(validation.ErrorMessage, "Invalid Dimensions", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get tenon type
            Dim tenonType As String = "Standard"
            If RbTenonHaunched IsNot Nothing AndAlso RbTenonHaunched.Checked Then
                tenonType = "Haunched"
            ElseIf RbTenonThrough IsNot Nothing AndAlso RbTenonThrough.Checked Then
                tenonType = "Through"
            End If

            ' Calculate
            Dim joint = JoineryCalculator.CalculateMortiseAndTenon(stockThickness, stockWidth, tenonType)

            ' Display results
            If LblTenonThickness IsNot Nothing Then LblTenonThickness.Text = $"Tenon Thickness: {joint.TenonThickness:N3}"""
            If LblTenonLength IsNot Nothing Then LblTenonLength.Text = $"Tenon Length: {joint.TenonLength:N3}"""
            If LblTenonWidth IsNot Nothing Then LblTenonWidth.Text = $"Tenon Width: {joint.TenonWidth:N3}"""
            If LblMortiseDepth IsNot Nothing Then LblMortiseDepth.Text = $"Mortise Depth: {joint.MortiseDepth:N3}"""
            If LblMortiseWidth IsNot Nothing Then LblMortiseWidth.Text = $"Mortise Width: {joint.MortiseWidth:N3}"""
            If LblShoulderOffset IsNot Nothing Then LblShoulderOffset.Text = $"Shoulder Offset: {joint.ShoulderOffset:N3}"""

            ' Draw diagram if panel exists
            If PbJointDiagram IsNot Nothing Then
                DrawJointDiagram(joint, stockThickness, stockWidth)
            End If
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "CalculateMortiseAndTenon", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Handles dovetail calculation
    ''' </summary>
    Private Sub CalculateDovetails()
        Try
            Dim boardThickness = InputValidator.TryParseDoubleWithDefault(TxtDovetailThickness.Text, 0.75)
            Dim boardWidth = InputValidator.TryParseDoubleWithDefault(TxtDovetailWidth.Text, 6.0)
            Dim spacing = InputValidator.TryParseDoubleWithDefault(TxtDovetailSpacing.Text, 0.5)
            Dim isHardwood = ChkDovetailHardwood IsNot Nothing AndAlso ChkDovetailHardwood.Checked

            Dim dovetails = JoineryCalculator.CalculateDovetails(boardThickness, boardWidth, isHardwood, spacing)

            ' Display results
            If LblDovetailAngle IsNot Nothing Then LblDovetailAngle.Text = $"Angle: 1:{dovetails.Angle:N0}"
            If LblDovetailPinWidth IsNot Nothing Then LblDovetailPinWidth.Text = $"Pin Width: {dovetails.PinWidth:N3}"""
            If LblDovetailTailWidth IsNot Nothing Then LblDovetailTailWidth.Text = $"Tail Width: {dovetails.TailWidth:N3}"""
            If LblDovetailCount IsNot Nothing Then LblDovetailCount.Text = $"Number of Tails: {dovetails.NumberOfTails}"
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "CalculateDovetails", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Handles box joint calculation
    ''' </summary>
    Private Sub CalculateBoxJoint()
        Try
            Dim stockThickness = InputValidator.TryParseDoubleWithDefault(TxtBoxJointThickness.Text, 0.5)
            Dim boardWidth = InputValidator.TryParseDoubleWithDefault(TxtBoxJointWidth.Text, 6.0)

            Dim result = JoineryCalculator.CalculateBoxJoint(stockThickness, boardWidth)

            If LblBoxJointPinWidth IsNot Nothing Then LblBoxJointPinWidth.Text = $"Pin Width: {result.PinWidth:N3}"""
            If LblBoxJointCount IsNot Nothing Then LblBoxJointCount.Text = $"Pin Count: {result.PinCount}"
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "CalculateBoxJoint", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Draws a simple joint diagram
    ''' </summary>
    Private Sub DrawJointDiagram(joint As JointDimensions, stockThickness As Double, stockWidth As Double)
        If PbJointDiagram Is Nothing Then Return

        Try
            Dim bmp As New Bitmap(PbJointDiagram.Width, PbJointDiagram.Height)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.White)
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                ' Calculate scale
                Dim scale = Math.Min((PbJointDiagram.Width - 40) / stockWidth,
                                    (PbJointDiagram.Height - 40) / (stockThickness * 3))

                Dim offsetX = 20
                Dim offsetY = PbJointDiagram.Height / 2

                ' Draw mortise piece (top)
                Dim mortisePiece As New Rectangle(offsetX, CInt(offsetY - stockThickness * scale - 10),
                                                  CInt(stockWidth * scale), CInt(stockThickness * scale))
                g.FillRectangle(Brushes.LightGray, mortisePiece)
                g.DrawRectangle(Pens.Black, mortisePiece)

                ' Draw mortise hole
                Dim mortiseHole As New Rectangle(CInt(offsetX + (stockWidth - joint.TenonWidth) * scale / 2),
                                                mortisePiece.Top,
                                                CInt(joint.TenonWidth * scale),
                                                CInt(joint.MortiseDepth * scale))
                g.FillRectangle(Brushes.White, mortiseHole)
                g.DrawRectangle(New Pen(Color.Red, 2), mortiseHole)

                ' Draw tenon piece (bottom)
                Dim tenonPiece As New Rectangle(offsetX, CInt(offsetY + 10),
                                                CInt(stockWidth * scale), CInt(stockThickness * scale))
                g.FillRectangle(Brushes.LightBlue, tenonPiece)
                g.DrawRectangle(Pens.Black, tenonPiece)

                ' Draw tenon projection
                Dim tenonProj As New Rectangle(CInt(offsetX + (stockWidth - joint.TenonWidth) * scale / 2),
                                               CInt(tenonPiece.Top - joint.TenonLength * scale),
                                               CInt(joint.TenonWidth * scale),
                                               CInt(joint.TenonLength * scale))
                g.FillRectangle(Brushes.LightBlue, tenonProj)
                g.DrawRectangle(New Pen(Color.Blue, 2), tenonProj)

                ' Add labels
                g.DrawString("Mortise", New Font("Arial", 8), Brushes.Black, offsetX, mortisePiece.Top - 15)
                g.DrawString("Tenon", New Font("Arial", 8), Brushes.Black, offsetX, tenonPiece.Bottom + 5)
            End Using

            PbJointDiagram.Image?.Dispose()
            PbJointDiagram.Image = bmp
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DrawJointDiagram")
        End Try
    End Sub

    ''' <summary>
    ''' Wire up joinery calculator events
    ''' </summary>
    Private Sub InitializeJoineryCalculator()
        ' Mortise & Tenon
        If TxtJointStockThickness IsNot Nothing Then
            AddHandler TxtJointStockThickness.TextChanged, AddressOf JoineryInput_Changed
            AddHandler TxtJointStockWidth.TextChanged, AddressOf JoineryInput_Changed
        End If
        
        If RbTenonStandard IsNot Nothing Then
            AddHandler RbTenonStandard.CheckedChanged, AddressOf JoineryInput_Changed
            AddHandler RbTenonHaunched.CheckedChanged, AddressOf JoineryInput_Changed
            AddHandler RbTenonThrough.CheckedChanged, AddressOf JoineryInput_Changed
        End If
        
        ' Dovetails
        If TxtDovetailThickness IsNot Nothing Then
            AddHandler TxtDovetailThickness.TextChanged, AddressOf DovetailInput_Changed
            AddHandler TxtDovetailWidth.TextChanged, AddressOf DovetailInput_Changed
            AddHandler TxtDovetailSpacing.TextChanged, AddressOf DovetailInput_Changed
        End If
        
        If ChkDovetailHardwood IsNot Nothing Then
            AddHandler ChkDovetailHardwood.CheckedChanged, AddressOf DovetailInput_Changed
        End If
        
        ' Box Joints
        If TxtBoxJointThickness IsNot Nothing Then
            AddHandler TxtBoxJointThickness.TextChanged, AddressOf BoxJointInput_Changed
            AddHandler TxtBoxJointWidth.TextChanged, AddressOf BoxJointInput_Changed
        End If
        
        ' Dados
        If TxtDadoStockThickness IsNot Nothing Then
            AddHandler TxtDadoStockThickness.TextChanged, AddressOf DadoInput_Changed
            AddHandler TxtDadoShelfThickness.TextChanged, AddressOf DadoInput_Changed
        End If
    End Sub
    
    Private Sub JoineryInput_Changed(sender As Object, e As EventArgs)
        CalculateMortiseAndTenon()
    End Sub
    
    Private Sub DovetailInput_Changed(sender As Object, e As EventArgs)
        CalculateDovetails()
    End Sub
    
    Private Sub BoxJointInput_Changed(sender As Object, e As EventArgs)
        CalculateBoxJoint()
    End Sub
    
    Private Sub DadoInput_Changed(sender As Object, e As EventArgs)
        CalculateDado()
    End Sub

    Private Sub BtnCalculateJoinery_Click(sender As Object, e As EventArgs) Handles BtnCalculateJoinery.Click
        CalculateMortiseAndTenon()
        CalculateDovetails()
        CalculateBoxJoint()
        CalculateDado()
    End Sub

    ''' <summary>
    ''' Handles dado/groove calculation
    ''' </summary>
    Private Sub CalculateDado()
        Try
            Dim stockThickness = InputValidator.TryParseDoubleWithDefault(TxtDadoStockThickness.Text, 0.75)
            Dim shelfThickness = InputValidator.TryParseDoubleWithDefault(TxtDadoShelfThickness.Text, 0.5)
            
            Dim result = JoineryCalculator.CalculateDado(stockThickness, shelfThickness)

            If LblDadoDepth IsNot Nothing Then LblDadoDepth.Text = $"Dado Depth: {result.Depth:N3}"""
            If LblDadoWidth IsNot Nothing Then LblDadoWidth.Text = $"Dado Width: {result.Width:N3}"""
            
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "CalculateDado", showToUser:=True)
        End Try
    End Sub

#End Region

End Class
