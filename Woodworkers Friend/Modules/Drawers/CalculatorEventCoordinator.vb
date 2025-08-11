Public Class CalculatorEventCoordinator

    Private ReadOnly _form As FrmMain

    Public Sub New(form As FrmMain)
        _form = form
    End Sub

    ''' <summary>
    ''' Initializes all event handlers for the calculator
    ''' </summary>
    Public Shared Sub InitializeEventHandlers()
        ' Add any setup code here if needed
        ' This method was being called but was missing
    End Sub

    ''' <summary>
    ''' Handles drawer calculation method change events
    ''' </summary>
    Public Sub HandleDrawerCalculationMethodChange(sender As Object, e As EventArgs)
        If _form Is Nothing Then Return

        ' Enable/disable controls based on selected method
        Dim selectedMethod As DrawerCalculationMethod = GetSelectedMethod()

        ' First, disable all method-specific controls
        ControlUtility.SetControlsEnabled(False,
            _form.TxtMultiplier,
            _form.TxtArithmeticIncrement,
            _form.TxtCustomRatioInput) ' Add this line

        ' Enable controls based on selected method
        Select Case selectedMethod
            Case DrawerCalculationMethod.Geometric
                ControlUtility.SetControlsEnabled(True, _form.TxtMultiplier, _form.TxtFirstDrawerHeight)

            Case DrawerCalculationMethod.Arithmetic
                ControlUtility.SetControlsEnabled(True, _form.TxtArithmeticIncrement, _form.TxtFirstDrawerHeight)

            Case DrawerCalculationMethod.ReverseArithmetic
                ControlUtility.SetControlsEnabled(True, _form.TxtArithmeticIncrement, _form.TxtFirstDrawerHeight)

            Case DrawerCalculationMethod.Logarithmic
                ControlUtility.SetControlsEnabled(True, _form.TxtFirstDrawerHeight)

            Case DrawerCalculationMethod.Exponential
                ControlUtility.SetControlsEnabled(True, _form.TxtMultiplier, _form.TxtFirstDrawerHeight)

            Case DrawerCalculationMethod.CustomRatio
                ControlUtility.SetControlsEnabled(True, _form.TxtFirstDrawerHeight, _form.TxtCustomRatioInput) ' Add this line

            Case DrawerCalculationMethod.Uniform
                ' Uniform only needs drawer count, width, and spacing - no height parameters
                ControlUtility.SetControlsEnabled(False, _form.TxtFirstDrawerHeight)

            Case DrawerCalculationMethod.GoldenRatio
                ControlUtility.SetControlsEnabled(True, _form.TxtFirstDrawerHeight)

            Case DrawerCalculationMethod.Fibonacci, DrawerCalculationMethod.Hambridge
                ControlUtility.SetControlsEnabled(True, _form.TxtFirstDrawerHeight)
        End Select
    End Sub

    ''' <summary>
    ''' Handles general calculator control change events
    ''' </summary>
    Public Sub HandleCalculatorControlChanged(sender As Object, e As EventArgs)
        ' Can be used for real-time validation or updates
        ' For now, just ensure the form state is consistent
        If _form IsNot Nothing Then
            ' Could add real-time validation here
        End If
    End Sub

    ''' <summary>
    ''' Sets the enabled state of main calculator controls
    ''' </summary>
    Public Sub SetControlsEnabled(enabled As Boolean)
        If _form Is Nothing Then Return

        ControlUtility.SetControlsEnabled(enabled,
            _form.TxtDrawerCount,
            _form.TxtDrawerSpacing,
            _form.TxtDrawerWidth,
            _form.TxtFirstDrawerHeight,
            _form.TxtMultiplier,
            _form.TxtArithmeticIncrement,
            _form.TxtCustomRatioInput, ' Add this line
            _form.RbHambridge,
            _form.RbGeometric,
            _form.RbFibonacci,
            _form.RbArithmetic,
            _form.RbLogarithmic,
            _form.RbExponential,
            _form.RbCustomRatio,
            _form.RbUniform,
            _form.RbReverseArithmetic,
            _form.RbGoldenRatio,
            _form.RbImperial,
            _form.RbMetric,
            _form.BtnCalculateDrawers)
    End Sub

    Private Function GetSelectedMethod() As DrawerCalculationMethod
        If _form.RbHambridge IsNot Nothing AndAlso _form.RbHambridge.Checked Then
            Return DrawerCalculationMethod.Hambridge
        ElseIf _form.RbGeometric IsNot Nothing AndAlso _form.RbGeometric.Checked Then
            Return DrawerCalculationMethod.Geometric
        ElseIf _form.RbFibonacci IsNot Nothing AndAlso _form.RbFibonacci.Checked Then
            Return DrawerCalculationMethod.Fibonacci
        ElseIf _form.RbArithmetic IsNot Nothing AndAlso _form.RbArithmetic.Checked Then
            Return DrawerCalculationMethod.Arithmetic
        ElseIf _form.RbLogarithmic IsNot Nothing AndAlso _form.RbLogarithmic.Checked Then
            Return DrawerCalculationMethod.Logarithmic
        ElseIf _form.RbExponential IsNot Nothing AndAlso _form.RbExponential.Checked Then
            Return DrawerCalculationMethod.Exponential
        ElseIf _form.RbCustomRatio IsNot Nothing AndAlso _form.RbCustomRatio.Checked Then
            Return DrawerCalculationMethod.CustomRatio
        ElseIf _form.RbUniform IsNot Nothing AndAlso _form.RbUniform.Checked Then
            Return DrawerCalculationMethod.Uniform
        ElseIf _form.RbReverseArithmetic IsNot Nothing AndAlso _form.RbReverseArithmetic.Checked Then
            Return DrawerCalculationMethod.ReverseArithmetic
        ElseIf _form.RbGoldenRatio IsNot Nothing AndAlso _form.RbGoldenRatio.Checked Then
            Return DrawerCalculationMethod.GoldenRatio
        Else
            Return DrawerCalculationMethod.Hambridge ' Default
        End If
    End Function

End Class