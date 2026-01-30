# Board Feet History Integration - Step-by-Step Guide

## 🎯 Option B: Quick Win (15 minutes)

This guide shows you **exactly** how to add calculation history to the Board Feet calculator.

---

## ✅ What You'll Get

- 💾 **Save to History** button
- 📂 **Load from History** button
- Full search, favorites, rename, delete functionality
- All calculations persist across app restarts

---

## 📝 Step 1: Add Buttons in Designer (2 minutes)

### **Option A: Using Designer UI**
1. Open Visual Studio Designer (View → Designer or Shift+F7)
2. Click on `TpBoardfeet` tab
3. Find a good location for 2 buttons (I recommend bottom-right near existing buttons)
4. Add **Button 1:**
   - **(Name):** `BtnSaveBoardFeetHistory`
   - **Text:** `💾 Save to History`
   - **Size:** 120, 30
5. Add **Button 2:**
   - **(Name):** `BtnLoadBoardFeetHistory`
   - **Text:** `📂 Load from History`
   - **Size:** 130, 30

### **Option B: Quick Code Addition (if you prefer code)**
Add to `FrmMain.Designer.vb` in the Board Feet controls section:

```vb
' Board Feet History buttons (Phase 6)
Me.BtnSaveBoardFeetHistory = New Button()
Me.BtnLoadBoardFeetHistory = New Button()

' ... in InitializeComponent() ...

' BtnSaveBoardFeetHistory
Me.BtnSaveBoardFeetHistory.Location = New Point(500, 550)
Me.BtnSaveBoardFeetHistory.Name = "BtnSaveBoardFeetHistory"
Me.BtnSaveBoardFeetHistory.Size = New Size(120, 30)
Me.BtnSaveBoardFeetHistory.Text = "💾 Save to History"
Me.TpBoardfeet.Controls.Add(Me.BtnSaveBoardFeetHistory)

' BtnLoadBoardFeetHistory
Me.BtnLoadBoardFeetHistory.Location = New Point(630, 550)
Me.BtnLoadBoardFeetHistory.Name = "BtnLoadBoardFeetHistory"
Me.BtnLoadBoardFeetHistory.Size = New Size(130, 30)
Me.BtnLoadBoardFeetHistory.Text = "📂 Load from History"
Me.TpBoardfeet.Controls.Add(Me.BtnLoadBoardFeetHistory)

' Add WithEvents declarations at top of class
Friend WithEvents BtnSaveBoardFeetHistory As Button
Friend WithEvents BtnLoadBoardFeetHistory As Button
```

---

## 📝 Step 2: Add Event Handlers (5 minutes)

Open `Partials\FrmMain.Boardfoot.vb` and add this code **at the end of the file** (before `End Class`):

```vb
#Region "Board Feet History (Phase 6)"

    ''' <summary>
    ''' Saves current board feet calculation to history
    ''' </summary>
    Private Sub BtnSaveBoardFeetHistory_Click(sender As Object, e As EventArgs) Handles BtnSaveBoardFeetHistory.Click
        Try
            ' Validate we have data
            If DgvBoardfeet.Rows.Count = 0 OrElse DgvBoardfeet.Rows.Count = 1 AndAlso DgvBoardfeet.Rows(0).IsNewRow Then
                MessageBox.Show("No calculations to save! Enter dimensions first.", 
                              "Save History", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Get values from first data row
            Dim row = DgvBoardfeet.Rows(0)
            Dim thickness, width, length As Double
            Dim quantity As Integer

            If Not TryGetCellValue(row, "Thickness", thickness) Then thickness = 0
            If Not TryGetCellValue(row, "Width", width) Then width = 0
            If Not TryGetCellValue(row, "Length", length) Then length = 0
            If Not TryGetCellValue(row, "Quantity", quantity) Then quantity = 1

            ' Validate we have actual values
            If thickness = 0 OrElse width = 0 OrElse length = 0 Then
                MessageBox.Show("Please enter thickness, width, and length values.", 
                              "Save History", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get calculated board feet
            Dim boardFeet As Double
            If Not TryGetCellValue(row, "TotalBoardFeet", boardFeet) Then
                boardFeet = thickness * width * length / 144.0 * quantity
            End If

            ' Calculate cubic measurements
            Dim cubicInches = thickness * width * length * quantity
            Dim cubicFeet = cubicInches / 1728.0

            ' Ask for optional name
            Dim name = InputBox($"Save calculation:{vbCrLf}" &
                               $"Thickness: {thickness}""{vbCrLf}" &
                               $"Width: {width}""{vbCrLf}" &
                               $"Length: {length}""{vbCrLf}" &
                               $"Quantity: {quantity}{vbCrLf}{vbCrLf}" &
                               "Enter a name (or leave blank):", 
                               "Save Calculation", 
                               $"{thickness}x{width}x{length} ({quantity})")

            ' Save to database
            If BoardFeetHistoryHelper.SaveCalculation(thickness, width, length, quantity, 
                                                     boardFeet, cubicInches, cubicFeet, name) Then
                MessageBox.Show("✅ Calculation saved to history!", "Success", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to save calculation. Check error log for details.", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnSaveBoardFeetHistory_Click")
            MessageBox.Show($"Error saving calculation:{vbCrLf}{ex.Message}", 
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Loads a calculation from history
    ''' </summary>
    Private Sub BtnLoadBoardFeetHistory_Click(sender As Object, e As EventArgs) Handles BtnLoadBoardFeetHistory.Click
        Try
            ' Show history dialog
            Dim history = BoardFeetHistoryHelper.ShowHistoryDialog()
            If history Is Nothing Then Return ' User cancelled

            ' Extract values from history
            Dim thickness, width, length As Double
            Dim quantity As Integer

            If Not BoardFeetHistoryHelper.LoadCalculation(history, thickness, width, length, quantity) Then
                MessageBox.Show("Failed to load calculation data.", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Clear existing rows
            DgvBoardfeet.Rows.Clear()

            ' Add new row with loaded values
            Dim rowIndex = DgvBoardfeet.Rows.Add()
            Dim newRow = DgvBoardfeet.Rows(rowIndex)

            ' Populate cells
            newRow.Cells("Thickness").Value = thickness
            newRow.Cells("Width").Value = width
            newRow.Cells("Length").Value = length
            newRow.Cells("Quantity").Value = quantity

            ' Trigger recalculation
            DgvBoardfeet_CellValueChanged(DgvBoardfeet, New DataGridViewCellEventArgs(0, rowIndex))

            ' Success message
            Dim calcName = If(String.IsNullOrEmpty(history.CalculationName), "(unnamed)", history.CalculationName)
            MessageBox.Show($"✅ Loaded: {calcName}{vbCrLf}{vbCrLf}" &
                          $"Thickness: {thickness}""{vbCrLf}" &
                          $"Width: {width}""{vbCrLf}" &
                          $"Length: {length}""{vbCrLf}" &
                          $"Quantity: {quantity}", 
                          "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnLoadBoardFeetHistory_Click")
            MessageBox.Show($"Error loading calculation:{vbCrLf}{ex.Message}", 
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region
```

---

## 📝 Step 3: Build and Test (5 minutes)

### **Build:**
```
Press F6 or Build → Build Solution
```

### **Test Save:**
1. Run the application
2. Navigate to Board Feet tab
3. Enter some dimensions:
   - Thickness: 0.75
   - Width: 12
   - Length: 96
   - Quantity: 10
4. Click **"💾 Save to History"**
5. Enter name: "Bookshelf boards"
6. Should see success message

### **Test Load:**
1. Clear the grid
2. Click **"📂 Load from History"**
3. Should see history dialog with your saved calculation
4. Double-click or select and click "Load"
5. Values should populate in grid
6. Calculation should auto-run

### **Test Features:**
- ⭐ **Favorite** - Click the star button
- 🔍 **Search** - Type "bookshelf" in search box
- ✏️ **Rename** - Select and click "Rename..."
- 🗑️ **Delete** - Select and click "Delete"

---

## 🎨 Suggested Button Placement

### **Option A: Bottom of Grid**
```
[DataGridView - Board Feet Grid]

[💾 Save to History]  [📂 Load from History]  [Other buttons...]
```

### **Option B: Right Side Panel**
```
[DataGridView]    [💾 Save to History]
                  [📂 Load from History]
                  [Other buttons...]
```

### **Option C: Toolbar at Top**
```
[💾 Save] [📂 Load] [Other tools...]

[DataGridView - Board Feet Grid]
```

---

## 🔍 How It Works

### **Save:**
1. Gets values from first row of DataGridView
2. Serializes to JSON: `{"thickness":0.75,"width":12,"length":96,"quantity":10}`
3. Saves to CalculationHistory table in database
4. User can optionally name it

### **Load:**
1. Shows FrmCalculationHistory dialog
2. User searches, filters, selects a calculation
3. Deserializes JSON back to values
4. Populates DataGridView
5. Triggers calculation event to update totals

### **Database:**
- Stored in: `Data\WoodworkersFriend.db`
- Table: `CalculationHistory`
- Persists forever (or until user deletes)

---

## 🎁 Bonus: Auto-Save (Optional)

Want to automatically save every calculation? Add this to the calculation method:

```vb
Private Sub DgvBoardfeet_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DgvBoardfeet.CellValueChanged
    ' ... existing calculation code ...
    
    ' Auto-save to history (optional)
    If boardFeetImperial > 0 Then
        ' Save silently without prompting
        BoardFeetHistoryHelper.SaveCalculation(thickness, width, length, quantity, 
                                              boardFeetImperial, cubicInches, cubicFeet, 
                                              $"Auto-saved {DateTime.Now:yyyy-MM-dd HH:mm}")
    End If
End Sub
```

---

## ✅ Success Checklist

- [ ] Buttons added to Board Feet tab
- [ ] Code compiles without errors
- [ ] Can save a calculation with name
- [ ] Can load calculation from history
- [ ] History dialog shows saved calculations
- [ ] Search works
- [ ] Can favorite, rename, delete
- [ ] Values populate correctly when loaded
- [ ] Calculation auto-runs after load

---

## 🚀 Next Steps

After Board Feet works:
1. **Add to Shelf Sag** - Copy the pattern
2. **Add to Wood Movement** - Similar implementation
3. **Add to other calculators** - Reuse the same approach

Each calculator just needs:
- 2 buttons in UI
- 2 event handlers
- 1 helper class (copy BoardFeetHistoryHelper pattern)

---

## 🐛 Troubleshooting

### **"TryGetCellValue not found"**
Make sure you're using the correct method to get cell values. Replace with:
```vb
Dim thickness As Double = Convert.ToDouble(row.Cells("Thickness").Value)
```

### **"Buttons don't appear"**
- Check Designer - are buttons on TpBoardfeet tab?
- Check they're not hidden behind other controls
- Verify `Visible = True` in properties

### **"Event not firing"**
- Verify `Handles BtnSaveBoardFeetHistory.Click` clause exists
- Check `WithEvents` declaration in Designer.vb
- Rebuild solution (F6)

### **"History dialog empty"**
- Save a calculation first
- Check database exists: `Data\WoodworkersFriend.db`
- Check error log for database errors

---

## 📊 Time Breakdown

- **Step 1 (Buttons):** 2 minutes
- **Step 2 (Code):** 5 minutes  
- **Step 3 (Test):** 5 minutes
- **Total:** ~12-15 minutes

---

**Status:** Ready to Implement  
**Difficulty:** Easy (copy-paste example)  
**Files to Modify:** 2 (Designer.vb + Boardfoot.vb)  
**New Files:** 0 (already created in Phase 6)
