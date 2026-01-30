# Contributing to Woodworker's Friend

Thank you for your interest in contributing to **Woodworker's Friend**! 🎉

This document provides guidelines for contributing to the project.

---

## 📋 Table of Contents

- [Code of Conduct](#code-of-conduct)
- [How Can I Contribute?](#how-can-i-contribute)
- [Development Setup](#development-setup)
- [Coding Standards](#coding-standards)
- [Submitting Changes](#submitting-changes)
- [Reporting Bugs](#reporting-bugs)
- [Suggesting Features](#suggesting-features)

---

## 🤝 Code of Conduct

### Our Pledge

We are committed to providing a welcoming and inclusive environment for all contributors, regardless of experience level, background, or identity.

### Expected Behavior

- ✅ Be respectful and considerate
- ✅ Welcome newcomers and help them learn
- ✅ Accept constructive criticism gracefully
- ✅ Focus on what's best for the project
- ✅ Show empathy towards others

### Unacceptable Behavior

- ❌ Harassment or discriminatory language
- ❌ Personal attacks or trolling
- ❌ Publishing others' private information
- ❌ Other conduct deemed unprofessional

---

## 🛠️ How Can I Contribute?

### **Types of Contributions**

#### **1. Bug Reports** 🐛
Found a bug? Please report it! See [Reporting Bugs](#reporting-bugs) below.

#### **2. Feature Requests** 💡
Have an idea for a new calculator or feature? See [Suggesting Features](#suggesting-features) below.

#### **3. Code Contributions** 💻
Ready to write code? See [Submitting Changes](#submitting-changes) below.

#### **4. Documentation** 📚
- Improve README or help content
- Add code comments
- Create tutorials or guides
- Fix typos or clarify instructions

#### **5. Testing** 🧪
- Test on different Windows versions
- Verify calculations with known values
- Test edge cases and unusual inputs
- Report issues found

#### **6. Design** 🎨
- Improve UI/UX
- Create icons or graphics
- Suggest layout improvements
- Enhance visual diagrams

---

## 💻 Development Setup

### **Prerequisites**

- **Visual Studio 2022** or later (Community Edition is free)
- **.NET 10.0 SDK** (or later)
- **Windows 10 SDK** (10.0.22000.0 or later)
- **Git** for version control

### **Setup Steps**

1. **Fork the repository**
   ```bash
   # Click "Fork" button on GitHub
   ```

2. **Clone your fork**
   ```bash
   git clone https://github.com/YOUR_USERNAME/Woodworkers-Friend.git
   cd Woodworkers-Friend
   ```

3. **Open in Visual Studio**
   ```
   Open "Woodworkers Friend.sln"
   ```

4. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

5. **Build the solution**
   ```bash
   dotnet build
   ```

6. **Run the application**
   ```bash
   Press F5 in Visual Studio
   ```

### **Database Setup**

On first run, the application automatically:
- Creates database at `%APPDATA%\Woodworkers Friend\WoodworkersFriend.db`
- Seeds initial data (wood species, joinery types, hardware, help content)
- Creates default user preferences

To **reset the database** for testing:
```bash
# Delete the database file
del "%APPDATA%\Woodworkers Friend\WoodworkersFriend.db"

# Next run will recreate it
```

---

## 📐 Coding Standards

### **VB.NET Style Guide**

#### **Naming Conventions**

```vb
' Classes: PascalCase
Public Class DrawerCalculator
End Class

' Methods: PascalCase
Public Function CalculateBoardFeet() As Double
End Function

' Private fields: _camelCase
Private _themeManager As ThemeManager

' Properties: PascalCase
Public Property CurrentTheme As AppTheme

' Constants: PascalCase or UPPER_CASE
Public Const MaxDrawers As Integer = 20
Private Const DEFAULT_KERF_WIDTH As Double = 0.125

' Local variables: camelCase
Dim drawerCount As Integer = 5

' Form controls: PascalCase with prefix
' - Txt = TextBox
' - Lbl = Label
' - Btn = Button
' - Cmb = ComboBox
' - Dgv = DataGridView
' - Gbx = GroupBox
' - Rb = RadioButton
' - Chk = CheckBox
Friend WithEvents TxtDrawerCount As TextBox
Friend WithEvents BtnCalculate As Button
```

#### **Comments**

```vb
''' <summary>
''' Calculate board feet for a single board.
''' Formula: (Thickness × Width × Length) / 144
''' </summary>
''' <param name="thickness">Board thickness in inches</param>
''' <param name="width">Board width in inches</param>
''' <param name="length">Board length in inches</param>
''' <returns>Board feet as Double</returns>
Public Function CalculateBoardFeet(thickness As Double, width As Double, length As Double) As Double
    ' Validate inputs
    ArgumentNullException.ThrowIfNull(thickness)
    
    ' Calculate board feet
    Dim boardFeet = (thickness * width * length) / 144.0
    
    Return boardFeet
End Function
```

#### **Error Handling**

```vb
' ALWAYS use ErrorHandler for exceptions
Try
    ' Your code here
    Dim result = PerformCalculation()
Catch ex As Exception
    ErrorHandler.LogError(ex, "MethodName")
    ' Show user-friendly message
    MessageBox.Show("An error occurred. Please check your inputs.", 
                    "Calculation Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error)
End Try
```

#### **Input Validation**

```vb
' Validate before calculating
If Not ValidateInputs() Then
    Return
End If

' Use ValidationManager where appropriate
If Not _validationManager.ValidateDrawerInputs() Then
    MessageBox.Show("Please correct the highlighted fields.", "Validation Error")
    Return
End If
```

#### **Dispose Pattern**

```vb
' Properly dispose of resources
Using conn As New SQLiteConnection(connectionString)
    conn.Open()
    ' Use connection
End Using ' Auto-disposed

' For forms
Using dlg As New FrmAddWoodSpecies()
    If dlg.ShowDialog(Me) = DialogResult.OK Then
        ' Process result
    End If
End Using ' Auto-disposed
```

### **Code Organization**

#### **Partial Classes for FrmMain**

Each feature gets its own partial class file:

```
FrmMain.vb              ' Main form, initialization, managers
FrmMain.Designer.vb     ' Designer-generated code (DO NOT EDIT MANUALLY)
FrmMain.Drawers.vb      ' Drawer calculator logic
FrmMain.Doors.vb        ' Door calculator logic
FrmMain.Joinery.vb      ' Joinery calculator logic
... etc
```

#### **File Structure Pattern**

```vb
' ============================================================================
' Filename: FrmMain.Drawers.vb
' Purpose: Drawer height calculator logic for FrmMain
' Last Updated: 2026-01-30
' ============================================================================

' Partial Class FrmMain
Partial Class FrmMain

    ' ===== DRAWER CALCULATOR REGION =====
    
    #Region "Drawer Calculator"
    
    ' Event handlers
    Private Sub BtnCalculateDrawers_Click(sender As Object, e As EventArgs) Handles BtnCalculateDrawers.Click
        ' Implementation
    End Sub
    
    ' Helper methods
    Private Function ValidateDrawerInputs() As Boolean
        ' Validation logic
    End Function
    
    #End Region
    
End Class
```

### **Database Operations**

```vb
' ALWAYS use DatabaseManager singleton
Dim species = DatabaseManager.Instance.GetAllWoodSpecies()

' Use transactions for multiple operations
Using conn As New SQLiteConnection(connectionString)
    conn.Open()
    Using transaction = conn.BeginTransaction()
        Try
            ' Multiple operations
            transaction.Commit()
        Catch ex As Exception
            transaction.Rollback()
            Throw
        End Try
    End Using
End Using

' ALWAYS use parameterized queries (prevent SQL injection)
Using cmd As New SQLiteCommand("SELECT * FROM WoodSpecies WHERE CommonName = @name", conn)
    cmd.Parameters.AddWithValue("@name", speciesName)
    ' Execute
End Using
```

### **Designer Code Rules**

**CRITICAL:** Follow these rules for `FrmMain.Designer.vb`:

❌ **NEVER** put these in `InitializeComponent`:
- `If` statements or loops
- Ternary operators (`? :`)
- Null coalescing (`??`, `?.`)
- Lambdas
- Collection expressions

✅ **ONLY** simple assignments and method calls like:
- `SuspendLayout()` / `ResumeLayout()`
- `Controls.Add()`
- Property assignments

See **WinForms Development Guidelines** instruction file for complete rules.

---

## 🚀 Submitting Changes

### **Pull Request Process**

1. **Create a feature branch**
   ```bash
   git checkout -b feature/my-awesome-feature
   ```

2. **Make your changes**
   - Follow coding standards
   - Add XML comments
   - Update documentation
   - Test thoroughly

3. **Commit with clear messages**
   ```bash
   git commit -m "Add shelf sag calculator with edge stiffener support"
   ```

4. **Push to your fork**
   ```bash
   git push origin feature/my-awesome-feature
   ```

5. **Open a Pull Request**
   - Provide clear title and description
   - Reference related issues
   - Include screenshots for UI changes
   - List what was tested

### **Commit Message Format**

```
<type>: <short summary>

<detailed description (optional)>

<footer (optional)>
```

**Types:**
- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation changes
- `style:` - Code formatting (no logic change)
- `refactor:` - Code restructuring
- `test:` - Adding tests
- `chore:` - Build process, dependencies

**Examples:**
```
feat: Add metric support to shelf sag calculator

- Add millimeter input fields
- Convert calculations to metric
- Update help documentation

Closes #42
```

```
fix: Correct board feet calculation rounding error

- Changed division to use Double instead of Integer
- Added test cases for edge values
- Updated validation ranges

Fixes #38
```

---

## 🐛 Reporting Bugs

### **Before Submitting**

1. **Check existing issues** - Your bug may already be reported
2. **Test on latest version** - Update to latest release
3. **Gather information** - Collect error logs and steps to reproduce

### **Bug Report Template**

```markdown
**Describe the bug**
A clear description of what the bug is.

**To Reproduce**
Steps to reproduce the behavior:
1. Go to '...'
2. Click on '...'
3. Enter values '...'
4. See error

**Expected behavior**
What you expected to happen.

**Actual behavior**
What actually happened.

**Screenshots**
If applicable, add screenshots to help explain the problem.

**Environment:**
- OS: [e.g., Windows 11 23H2]
- .NET Version: [e.g., .NET 10.0]
- Application Version: [e.g., 1.0.0]

**Error Logs**
Attach relevant error logs from %APPDATA%\Woodworkers Friend\Logs\

**Additional context**
Any other context about the problem.
```

### **Where to Report**

File bug reports at: [GitHub Issues](https://github.com/dmaidon/Woodworkers-Friend/issues)

---

## 💡 Suggesting Features

### **Feature Request Template**

```markdown
**Is your feature request related to a problem?**
A clear description of the problem. Ex. "I'm always frustrated when [...]"

**Describe the solution you'd like**
A clear description of what you want to happen.

**Describe alternatives you've considered**
Other solutions or features you've considered.

**Woodworking use case**
Specific woodworking scenario where this would be useful.

**Additional context**
Add any other context, screenshots, or examples.
```

### **Good Feature Requests**

✅ **Specific** - Clearly defined scope
✅ **Useful** - Solves a real woodworking problem
✅ **Feasible** - Technically possible to implement
✅ **Documented** - Includes examples or references

---

## 🧪 Testing

### **Manual Testing Checklist**

Before submitting a PR, test:

- [ ] All affected calculators work correctly
- [ ] Input validation works properly
- [ ] Error messages are clear and helpful
- [ ] Dark and Light themes display correctly
- [ ] Imperial and Metric scales work
- [ ] Database operations succeed
- [ ] Export functions work (CSV, Text, HTML)
- [ ] Help content displays correctly
- [ ] No compiler warnings
- [ ] No runtime exceptions

### **Test Cases**

Add test cases for calculations:

```vb
' Example test case comments
' Test Case 1: Standard 3/4" × 6" × 96" board
'   Input: 0.75, 6, 96
'   Expected: 4.0 board feet

' Test Case 2: Edge case - minimum dimensions
'   Input: 0.25, 1, 12
'   Expected: 0.0208 board feet

' Test Case 3: Zero handling
'   Input: 0, 6, 96
'   Expected: 0 or validation error
```

---

## 📝 Documentation

### **Code Comments**

Use XML documentation comments for all public APIs:

```vb
''' <summary>
''' Calculates the deflection of a shelf under load.
''' Uses standard beam deflection formula: δ = (5 × w × L⁴) / (384 × E × I)
''' </summary>
''' <param name="span">Distance between supports in inches</param>
''' <param name="load">Total load on shelf in pounds</param>
''' <param name="elasticity">Modulus of elasticity (psi)</param>
''' <param name="momentOfInertia">Moment of inertia (in⁴)</param>
''' <returns>Deflection in inches</returns>
''' <remarks>
''' Industry standard: Maximum sag should not exceed 1/360 of span.
''' Example: 36" shelf should sag no more than 0.1"
''' </remarks>
Public Function CalculateShelfSag(span As Double, load As Double, 
                                   elasticity As Double, momentOfInertia As Double) As Double
```

### **Help Content**

When adding features, update help content in `DataMigration.vb`:

```vb
' Add new help topic to MigrateHelpContent()
New DatabaseManager.HelpContentData With {
    .ModuleName = "NewFeature",
    .Title = "New Feature Calculator",
    .Category = "Calculators",
    .SortOrder = 99,
    .Keywords = "feature,calculator,new",
    .Content = "#TITLE:New Feature" & vbLf & 
               "##SECTION:Description" & vbLf &
               "*BULLET:Feature capability 1" & vbLf &
               "?NOTE:Helpful tip here!"
}
```

---

## 🔍 Code Review Process

### **What We Look For**

✅ **Correctness** - Does it work? Are calculations accurate?
✅ **Code quality** - Follows standards, readable, maintainable
✅ **Performance** - No unnecessary loops or memory leaks
✅ **Error handling** - Graceful failure with logging
✅ **Documentation** - Comments, XML docs, help content
✅ **Testing** - Manual testing performed
✅ **No regressions** - Doesn't break existing features

### **Review Timeline**

- **Initial response:** Within 3-5 days
- **Full review:** Within 1-2 weeks
- **Feedback iterations:** As needed
- **Merge decision:** After all feedback addressed

---

## 🏗️ Project Architecture

### **Key Design Patterns**

#### **Partial Classes**
`FrmMain` is split into partial classes by feature:

```vb
' FrmMain.vb - Main initialization and managers
' FrmMain.Drawers.vb - Drawer calculator
' FrmMain.Doors.vb - Door calculator
' etc.
```

**When adding a calculator:**
1. Create `FrmMain.[Feature].vb` in `Partials/` folder
2. Add `Partial Class FrmMain` declaration
3. Use `#Region` for organization
4. Implement event handlers and logic

#### **Manager Pattern**

```vb
' Centralized business logic
Private _themeManager As ThemeManager
Private _validationManager As ValidationManager
Private _projectManager As ProjectManager

' Initialize in FrmMain.InitializeManagers()
Private Sub InitializeManagers()
    _themeManager = New ThemeManager(Me)
    _validationManager = New ValidationManager()
    _projectManager = New ProjectManager()
End Sub
```

#### **Singleton Pattern**

```vb
' Database access through singleton
Public Class DatabaseManager
    Private Shared _instance As DatabaseManager
    
    Public Shared ReadOnly Property Instance As DatabaseManager
        Get
            If _instance Is Nothing Then
                _instance = New DatabaseManager()
            End If
            Return _instance
        End Get
    End Property
    
    Private Sub New()
        ' Initialize
    End Sub
End Class

' Usage
Dim species = DatabaseManager.Instance.GetAllWoodSpecies()
```

---

## 🎯 Adding a New Calculator

### **Step-by-Step Guide**

#### **1. Create Partial Class File**

`Woodworkers Friend\Partials\FrmMain.MyCalculator.vb`

```vb
' ============================================================================
' Filename: FrmMain.MyCalculator.vb
' Purpose: My Calculator logic for FrmMain
' Last Updated: 2026-01-30
' ============================================================================

Partial Class FrmMain

    #Region "My Calculator"
    
    Private Sub BtnCalculateMyFeature_Click(sender As Object, e As EventArgs) Handles BtnCalculateMyFeature.Click
        Try
            ' Validate inputs
            If Not ValidateMyInputs() Then
                Return
            End If
            
            ' Perform calculation
            Dim result = PerformMyCalculation()
            
            ' Display results
            DisplayMyResults(result)
            
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnCalculateMyFeature_Click")
            MessageBox.Show("Calculation error. Please check your inputs.", 
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    
    Private Function ValidateMyInputs() As Boolean
        ' Validation logic
        Return True
    End Function
    
    Private Function PerformMyCalculation() As Double
        ' Calculation logic
        Return 0.0
    End Function
    
    Private Sub DisplayMyResults(result As Double)
        ' Display logic
    End Sub
    
    #End Region
    
End Class
```

#### **2. Create Calculator Module**

`Woodworkers Friend\Modules\Calculators\MyCalculator.vb`

```vb
''' <summary>
''' Performs [description] calculations.
''' </summary>
Public Class MyCalculator
    
    ''' <summary>
    ''' Calculate [what it calculates]
    ''' </summary>
    Public Shared Function Calculate(param1 As Double, param2 As Double) As Double
        ' Calculation implementation
        Return result
    End Function
    
End Class
```

#### **3. Add UI Controls in Designer**

1. Open `FrmMain.Designer.vb`
2. Add new `TabPage` for your calculator
3. Add input controls (`TextBox`, `Label`, etc.)
4. Add button: `BtnCalculate[Feature]`
5. Add results display (`RichTextBox` or `Label`)
6. Follow existing naming conventions

#### **4. Add Help Content**

In `DataMigration.vb`, add to `MigrateHelpContent()`:

```vb
New DatabaseManager.HelpContentData With {
    .ModuleName = "MyCalculator",
    .Title = "My Calculator Help",
    .Category = "Calculators",
    .SortOrder = 99,
    .Keywords = "my,calculator,feature",
    .Content = "#TITLE:My Calculator" & vbLf &
               "##SECTION:Purpose|What this calculator does" & vbLf &
               "*BULLET:Feature 1" & vbLf &
               "*BULLET:Feature 2" & vbLf &
               "?NOTE:Helpful tip!"
}
```

#### **5. Update README**

Add your feature to the Features section in `README.md`.

#### **6. Test Thoroughly**

- [ ] Calculations are accurate
- [ ] Input validation works
- [ ] Error handling is graceful
- [ ] Help content displays
- [ ] Export works (if applicable)
- [ ] Themes work correctly

---

## 🧩 Adding Reference Data

### **Adding Wood Species**

Use the **in-app Add Species** feature, or add to `WoodPropertiesDatabase.vb`:

```vb
New WoodPropertiesData With {
    .CommonName = "Species Name",
    .ScientificName = "Genus species",
    .WoodType = "Hardwood",
    .JankaHardness = 1000,
    .SpecificGravity = 0.55,
    .Density = 34.0,
    .MoistureContent = 12.0,
    .ShrinkageRadial = 4.5,
    .ShrinkageTangential = 8.2,
    .TypicalUses = "Furniture, flooring, cabinetry",
    .Workability = "Good machining, glues well",
    .Cautions = "May cause allergic reactions",
    .Notes = "Additional notes here"
}
```

### **Adding Joinery Types**

In `DataMigration.MigrateJoineryTypes()`:

```vb
New JoineryType With {
    .Name = "New Joint Type",
    .Category = JoineryCategory.Frame,
    .StrengthRating = 8,
    .DifficultyLevel = JoineryDifficulty.Intermediate,
    .Description = "Description of the joint",
    .TypicalUses = "Where this joint is used",
    .RequiredTools = "Tools needed",
    .StrengthCharacteristics = "Why it's strong",
    .GlueRequired = True,
    .ReinforcementOptions = "How to reinforce",
    .HistoricalNotes = "Historical context"
}
```

### **Adding Hardware**

In `DataMigration.MigrateHardwareStandards()`:

```vb
New HardwareStandard With {
    .Category = HardwareCategory.Hinges,
    .Type = "Hardware Type",
    .Brand = "Brand Name",
    .Description = "What it is",
    .Dimensions = "Size specifications",
    .WeightCapacity = "Load rating",
    .MountingRequirements = "How to mount",
    .TypicalUses = "Common applications",
    .InstallationNotes = "Installation tips"
}
```

---

## 📚 Resources

### **Learning VB.NET & WinForms**

- [Microsoft VB.NET Documentation](https://docs.microsoft.com/en-us/dotnet/visual-basic/)
- [Windows Forms Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)
- [SQLite Documentation](https://www.sqlite.org/docs.html)

### **Woodworking References**

- [Wood Database](https://www.wood-database.com/) - Wood species data
- [USDA Forest Products Lab](https://www.fpl.fs.fed.us/) - Scientific wood data
- Traditional woodworking texts for joinery specifications

---

## ❓ Questions?

- 💬 **GitHub Discussions** - Ask questions, share ideas
- 🐛 **GitHub Issues** - Report bugs, request features
- 📧 **Email** - Contact maintainer directly (see README)

---

## 🎉 Recognition

### **Contributors**

All contributors will be recognized in:
- GitHub Contributors page
- About tab in application
- Release notes

### **Types of Contributions Valued**

- Code contributions (features, fixes)
- Documentation improvements
- Bug reports and testing
- Feature ideas and feedback
- Woodworking expertise and formula verification
- UI/UX suggestions

---

## ⚖️ Legal

By contributing, you agree that:

1. Your contributions are your own work or properly attributed
2. You have the right to submit the contribution
3. Your contribution is licensed under the MIT License
4. You understand woodworking calculations require user verification

---

**Thank you for contributing to Woodworker's Friend!** 🪚

**Together, we're making woodworking more accessible and precise for everyone!** 🪵

---

*Last Updated: January 30, 2026*
