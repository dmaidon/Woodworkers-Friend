Public Class PresetManager

    ''' <summary>
    ''' Applies a drawer preset to the form controls
    ''' </summary>
    Public Shared Sub ApplyDrawerPreset(presetName As String, form As FrmMain)
        If form Is Nothing Then Return

        Select Case presetName.ToLower()
            Case "kitchen_standard"
                ApplyKitchenStandardPreset(form)
            Case "bathroom_vanity"
                ApplyBathroomVanityPreset(form)
            Case "office_desk"
                ApplyOfficeDeskPreset(form)
            Case "custom_cabinet"
                ApplyCustomCabinetPreset(form)
            Case "golden_ratio" ' <-- Add new preset
                ApplyGoldenRatioPreset(form)
            Case "reverse_arithmetic"
                ApplyReverseArithmeticPreset(form)
            Case "logarithmic_example"
                ApplyLogarithmicPreset(form)
            Case "exponential_example"
                ApplyExponentialPreset(form)
            Case "custom_ratio_example"
                ApplyCustomRatioPreset(form)
            Case "uniform_example"
                ApplyUniformPreset(form)
        End Select
    End Sub

    Private Shared Sub ApplyKitchenStandardPreset(form As FrmMain)
        With form
            If .TxtDrawerCount IsNot Nothing Then .TxtDrawerCount.Text = "4"
            If .TxtDrawerSpacing IsNot Nothing Then .TxtDrawerSpacing.Text = "0.75"
            If .TxtDrawerWidth IsNot Nothing Then .TxtDrawerWidth.Text = "18"
            If .TxtFirstDrawerHeight IsNot Nothing Then .TxtFirstDrawerHeight.Text = "4"
            If .TxtMultiplier IsNot Nothing Then .TxtMultiplier.Text = "1.25"
            If .TxtArithmeticIncrement IsNot Nothing Then .TxtArithmeticIncrement.Text = "0" ' Clear unused
            If .RbGeometric IsNot Nothing Then .RbGeometric.Checked = True
            If .RbImperial IsNot Nothing Then .RbImperial.Checked = True
        End With
    End Sub

    Private Shared Sub ApplyBathroomVanityPreset(form As FrmMain)
        With form
            If .TxtDrawerCount IsNot Nothing Then .TxtDrawerCount.Text = "3"
            If .TxtDrawerSpacing IsNot Nothing Then .TxtDrawerSpacing.Text = "0.5"
            If .TxtDrawerWidth IsNot Nothing Then .TxtDrawerWidth.Text = "15"
            If .TxtFirstDrawerHeight IsNot Nothing Then .TxtFirstDrawerHeight.Text = "3"
            If .TxtMultiplier IsNot Nothing Then .TxtMultiplier.Text = "1.4"
            If .TxtArithmeticIncrement IsNot Nothing Then .TxtArithmeticIncrement.Text = "0" ' Clear unused
            If .RbGeometric IsNot Nothing Then .RbGeometric.Checked = True
            If .RbImperial IsNot Nothing Then .RbImperial.Checked = True
        End With
    End Sub

    Private Shared Sub ApplyOfficeDeskPreset(form As FrmMain)
        With form
            If .TxtDrawerCount IsNot Nothing Then .TxtDrawerCount.Text = "3"
            If .TxtDrawerSpacing IsNot Nothing Then .TxtDrawerSpacing.Text = "0.25"
            If .TxtDrawerWidth IsNot Nothing Then .TxtDrawerWidth.Text = "12"
            If .TxtFirstDrawerHeight IsNot Nothing Then .TxtFirstDrawerHeight.Text = "3.5" ' Add missing parameter
            If .TxtMultiplier IsNot Nothing Then .TxtMultiplier.Text = "0" ' Clear unused for Fibonacci
            If .TxtArithmeticIncrement IsNot Nothing Then .TxtArithmeticIncrement.Text = "0" ' Clear unused
            If .RbFibonacci IsNot Nothing Then .RbFibonacci.Checked = True
            If .RbImperial IsNot Nothing Then .RbImperial.Checked = True
        End With
    End Sub

    Private Shared Sub ApplyCustomCabinetPreset(form As FrmMain)
        With form
            If .TxtDrawerCount IsNot Nothing Then .TxtDrawerCount.Text = "5"
            If .TxtDrawerSpacing IsNot Nothing Then .TxtDrawerSpacing.Text = "1"
            If .TxtDrawerWidth IsNot Nothing Then .TxtDrawerWidth.Text = "20"
            If .TxtFirstDrawerHeight IsNot Nothing Then .TxtFirstDrawerHeight.Text = "4" ' Add missing parameter
            If .TxtMultiplier IsNot Nothing Then .TxtMultiplier.Text = "0" ' Clear unused for Hambridge
            If .TxtArithmeticIncrement IsNot Nothing Then .TxtArithmeticIncrement.Text = "0" ' Clear unused
            If .RbHambridge IsNot Nothing Then .RbHambridge.Checked = True
            If .RbImperial IsNot Nothing Then .RbImperial.Checked = True
        End With
    End Sub

    ''' <summary>
    ''' New preset demonstrating Golden Ratio calculations
    ''' </summary>
    Private Shared Sub ApplyGoldenRatioPreset(form As FrmMain)
        With form
            If .TxtDrawerCount IsNot Nothing Then .TxtDrawerCount.Text = "4"
            If .TxtDrawerSpacing IsNot Nothing Then .TxtDrawerSpacing.Text = "0.5"
            If .TxtDrawerWidth IsNot Nothing Then .TxtDrawerWidth.Text = "16"
            If .TxtFirstDrawerHeight IsNot Nothing Then .TxtFirstDrawerHeight.Text = "3" ' Starting height
            If .TxtMultiplier IsNot Nothing Then .TxtMultiplier.Text = "0" ' Clear unused
            If .TxtArithmeticIncrement IsNot Nothing Then .TxtArithmeticIncrement.Text = "0" ' Clear unused
            If .RbGoldenRatio IsNot Nothing Then .RbGoldenRatio.Checked = True
            If .RbImperial IsNot Nothing Then .RbImperial.Checked = True
        End With
    End Sub

    ''' <summary>
    ''' Example preset using Arithmetic progression
    ''' </summary>
    Private Shared Sub ApplyArithmeticPreset(form As FrmMain)
        With form
            If .TxtDrawerCount IsNot Nothing Then .TxtDrawerCount.Text = "4"
            If .TxtDrawerSpacing IsNot Nothing Then .TxtDrawerSpacing.Text = "0.75"
            If .TxtDrawerWidth IsNot Nothing Then .TxtDrawerWidth.Text = "18"
            If .TxtFirstDrawerHeight IsNot Nothing Then .TxtFirstDrawerHeight.Text = "3"
            If .TxtMultiplier IsNot Nothing Then .TxtMultiplier.Text = "0" ' Clear unused
            If .TxtArithmeticIncrement IsNot Nothing Then .TxtArithmeticIncrement.Text = "0.75" ' 0.75" increment
            If .RbArithmetic IsNot Nothing Then .RbArithmetic.Checked = True
            If .RbImperial IsNot Nothing Then .RbImperial.Checked = True
        End With
    End Sub

    ' Add this method to PresetManager:
    Private Shared Sub ApplyReverseArithmeticPreset(form As FrmMain)
        With form
            If .TxtDrawerCount IsNot Nothing Then .TxtDrawerCount.Text = "4"
            If .TxtDrawerSpacing IsNot Nothing Then .TxtDrawerSpacing.Text = "0.5"
            If .TxtDrawerWidth IsNot Nothing Then .TxtDrawerWidth.Text = "18"
            If .TxtFirstDrawerHeight IsNot Nothing Then .TxtFirstDrawerHeight.Text = "8" ' Start with tallest
            If .TxtMultiplier IsNot Nothing Then .TxtMultiplier.Text = "0" ' Clear unused
            If .TxtArithmeticIncrement IsNot Nothing Then .TxtArithmeticIncrement.Text = "1.5" ' Decrease by 1.5" each
            If .RbReverseArithmetic IsNot Nothing Then .RbReverseArithmetic.Checked = True
            If .RbImperial IsNot Nothing Then .RbImperial.Checked = True
        End With
    End Sub

    ' Add these methods to PresetManager:

    Private Shared Sub ApplyLogarithmicPreset(form As FrmMain)
        With form
            If .TxtDrawerCount IsNot Nothing Then .TxtDrawerCount.Text = "5"
            If .TxtDrawerSpacing IsNot Nothing Then .TxtDrawerSpacing.Text = "0.5"
            If .TxtDrawerWidth IsNot Nothing Then .TxtDrawerWidth.Text = "18"
            If .TxtFirstDrawerHeight IsNot Nothing Then .TxtFirstDrawerHeight.Text = "3"
            If .TxtMultiplier IsNot Nothing Then .TxtMultiplier.Text = "0"
            If .TxtArithmeticIncrement IsNot Nothing Then .TxtArithmeticIncrement.Text = "0"
            If .RbLogarithmic IsNot Nothing Then .RbLogarithmic.Checked = True
            If .RbImperial IsNot Nothing Then .RbImperial.Checked = True
        End With
    End Sub

    Private Shared Sub ApplyExponentialPreset(form As FrmMain)
        With form
            If .TxtDrawerCount IsNot Nothing Then .TxtDrawerCount.Text = "4"
            If .TxtDrawerSpacing IsNot Nothing Then .TxtDrawerSpacing.Text = "0.75"
            If .TxtDrawerWidth IsNot Nothing Then .TxtDrawerWidth.Text = "16"
            If .TxtFirstDrawerHeight IsNot Nothing Then .TxtFirstDrawerHeight.Text = "2.5"
            If .TxtMultiplier IsNot Nothing Then .TxtMultiplier.Text = "1.6"
            If .TxtArithmeticIncrement IsNot Nothing Then .TxtArithmeticIncrement.Text = "0"
            If .RbExponential IsNot Nothing Then .RbExponential.Checked = True
            If .RbImperial IsNot Nothing Then .RbImperial.Checked = True
        End With
    End Sub

    Private Shared Sub ApplyCustomRatioPreset(form As FrmMain)
        With form
            If .TxtDrawerCount IsNot Nothing Then .TxtDrawerCount.Text = "4"
            If .TxtDrawerSpacing IsNot Nothing Then .TxtDrawerSpacing.Text = "0.5"
            If .TxtDrawerWidth IsNot Nothing Then .TxtDrawerWidth.Text = "18"
            If .TxtFirstDrawerHeight IsNot Nothing Then .TxtFirstDrawerHeight.Text = "4"
            If .TxtMultiplier IsNot Nothing Then .TxtMultiplier.Text = "0"
            If .TxtArithmeticIncrement IsNot Nothing Then .TxtArithmeticIncrement.Text = "0"
            ' Set example custom ratios
            If .TxtCustomRatioInput IsNot Nothing Then
                .TxtCustomRatioInput.Text = "1.0" & vbCrLf & "1.5" & vbCrLf & "2.0" & vbCrLf & "3.0"
            End If
            If .RbCustomRatio IsNot Nothing Then .RbCustomRatio.Checked = True
            If .RbImperial IsNot Nothing Then .RbImperial.Checked = True
        End With
    End Sub

    Private Shared Sub ApplyUniformPreset(form As FrmMain)
        With form
            If .TxtDrawerCount IsNot Nothing Then .TxtDrawerCount.Text = "5"
            If .TxtDrawerSpacing IsNot Nothing Then .TxtDrawerSpacing.Text = "0.25"
            If .TxtDrawerWidth IsNot Nothing Then .TxtDrawerWidth.Text = "15"
            If .TxtFirstDrawerHeight IsNot Nothing Then .TxtFirstDrawerHeight.Text = "0"
            If .TxtMultiplier IsNot Nothing Then .TxtMultiplier.Text = "0"
            If .TxtArithmeticIncrement IsNot Nothing Then .TxtArithmeticIncrement.Text = "0"
            If .RbUniform IsNot Nothing Then .RbUniform.Checked = True
            If .RbImperial IsNot Nothing Then .RbImperial.Checked = True
        End With
    End Sub

End Class