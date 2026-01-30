# IMMEDIATE FIX NEEDED - Missing Control Declarations

## Controls Missing from FrmMain.Designer.vb

Add these control declarations to the Designer file:

```visualbasic
' Wood Properties Reference controls (add after other Friend WithEvents declarations)
Friend WithEvents PnlWoodFilters As Panel
Friend WithEvents RbWoodAll As RadioButton
Friend WithEvents RbWoodHardwoods As RadioButton
Friend WithEvents RbWoodSoftwoods As RadioButton
Friend WithEvents TxtWoodSearch As TextBox
Friend WithEvents BtnWoodClearSearch As Button
Friend WithEvents DgvWoodProperties As DataGridView
Friend WithEvents PnlWoodDetails As Panel
Friend WithEvents LblWoodDetailsHeader As Label
Friend WithEvents RtbWoodDetails As RichTextBox
Friend WithEvents BtnCompareWoods As Button
Friend WithEvents BtnExportWoodData As Button
Friend WithEvents BtnPrintWoodData As Button
```

## Where to Add in Designer

At the END of the Designer file, after all other `Friend WithEvents` declarations, BEFORE the `End Class` statement.

## Also Needed

Add `InitializeWoodPropertiesReference()` call to `FrmMain_Load`:

```visualbasic
Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Try
        _loading = True
        
        ' ... existing initialization code ...
        
        ' Initialize wood properties reference
        InitializeWoodPropertiesReference()
        
        _loading = False
    Catch ex As Exception
        ErrorHandler.HandleError(ex, "FrmMain_Load", showToUser:=True)
    End Try
End Sub
```

Once these controls are added to the Designer, the build errors will be resolved!
