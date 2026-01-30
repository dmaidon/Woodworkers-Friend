Imports System.Windows.Forms

''' <summary>
''' Dialog for adding custom wood species to database
''' </summary>
Public Class FrmAddWoodSpecies
    Inherits Form

    Private txtCommonName As TextBox
    Private txtScientificName As TextBox
    Private cmbWoodType As ComboBox
    Private numJankaHardness As NumericUpDown
    Private numSpecificGravity As NumericUpDown
    Private numDensity As NumericUpDown
    Private numMoistureContent As NumericUpDown
    Private numShrinkageRadial As NumericUpDown
    Private numShrinkageTangential As NumericUpDown
    Private txtTypicalUses As TextBox
    Private txtWorkability As TextBox
    Private txtCautions As TextBox
    Private txtNotes As TextBox
    Private btnOK As Button
    Private btnCancel As Button
    Private _speciesData As WoodPropertiesData

    ''' <summary>
    ''' Gets the wood species data entered by user
    ''' </summary>
    Public ReadOnly Property WoodSpeciesData As WoodPropertiesData
        Get
            Return _speciesData
        End Get
    End Property

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "Add Custom Wood Species"
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Size = New Size(600, 700)

        Dim tlp As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 2,
            .RowCount = 15,
            .Padding = New Padding(10)
        }

        ' Column styles
        tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 180))
        tlp.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))

        ' Row styles (all AutoSize)
        For i = 0 To 14
            tlp.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        Next

        Dim row = 0

        ' Common Name (Required)
        tlp.Controls.Add(New Label With {.Text = "Common Name: *", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 0, row)
        txtCommonName = New TextBox With {.Dock = DockStyle.Fill}
        tlp.Controls.Add(txtCommonName, 1, row)
        row += 1

        ' Scientific Name
        tlp.Controls.Add(New Label With {.Text = "Scientific Name:", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 0, row)
        txtScientificName = New TextBox With {.Dock = DockStyle.Fill}
        tlp.Controls.Add(txtScientificName, 1, row)
        row += 1

        ' Wood Type (Required)
        tlp.Controls.Add(New Label With {.Text = "Wood Type: *", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 0, row)
        cmbWoodType = New ComboBox With {.Dock = DockStyle.Fill, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbWoodType.Items.AddRange({"Hardwood", "Softwood"})
        cmbWoodType.SelectedIndex = 0
        tlp.Controls.Add(cmbWoodType, 1, row)
        row += 1

        ' Janka Hardness
        tlp.Controls.Add(New Label With {.Text = "Janka Hardness (lbf):", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 0, row)
        numJankaHardness = New NumericUpDown With {.Dock = DockStyle.Fill, .Maximum = 10000, .Minimum = 0, .Value = 1000}
        tlp.Controls.Add(numJankaHardness, 1, row)
        row += 1

        ' Specific Gravity
        tlp.Controls.Add(New Label With {.Text = "Specific Gravity:", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 0, row)
        numSpecificGravity = New NumericUpDown With {.Dock = DockStyle.Fill, .Maximum = 2, .Minimum = 0, .DecimalPlaces = 2, .Increment = 0.01D, .Value = 0.5D}
        tlp.Controls.Add(numSpecificGravity, 1, row)
        row += 1

        ' Density
        tlp.Controls.Add(New Label With {.Text = "Density (lb/ft³):", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 0, row)
        numDensity = New NumericUpDown With {.Dock = DockStyle.Fill, .Maximum = 150, .Minimum = 0, .Value = 40}
        tlp.Controls.Add(numDensity, 1, row)
        row += 1

        ' Moisture Content
        tlp.Controls.Add(New Label With {.Text = "Moisture Content (%):", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 0, row)
        numMoistureContent = New NumericUpDown With {.Dock = DockStyle.Fill, .Maximum = 100, .Minimum = 0, .DecimalPlaces = 2, .Increment = 0.01D, .Value = 12D}
        tlp.Controls.Add(numMoistureContent, 1, row)
        row += 1

        ' Shrinkage Radial
        tlp.Controls.Add(New Label With {.Text = "Shrinkage Radial (%):", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 0, row)
        numShrinkageRadial = New NumericUpDown With {.Dock = DockStyle.Fill, .Maximum = 20, .Minimum = 0, .DecimalPlaces = 3, .Increment = 0.001D, .Value = 0.05D}
        tlp.Controls.Add(numShrinkageRadial, 1, row)
        row += 1

        ' Shrinkage Tangential
        tlp.Controls.Add(New Label With {.Text = "Shrinkage Tangential (%):", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}, 0, row)
        numShrinkageTangential = New NumericUpDown With {.Dock = DockStyle.Fill, .Maximum = 20, .Minimum = 0, .DecimalPlaces = 3, .Increment = 0.001D, .Value = 0.08D}
        tlp.Controls.Add(numShrinkageTangential, 1, row)
        row += 1

        ' Typical Uses
        tlp.Controls.Add(New Label With {.Text = "Typical Uses:", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.TopLeft}, 0, row)
        txtTypicalUses = New TextBox With {.Dock = DockStyle.Fill, .Multiline = True, .Height = 60, .ScrollBars = ScrollBars.Vertical}
        tlp.Controls.Add(txtTypicalUses, 1, row)
        row += 1

        ' Workability
        tlp.Controls.Add(New Label With {.Text = "Workability:", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.TopLeft}, 0, row)
        txtWorkability = New TextBox With {.Dock = DockStyle.Fill, .Multiline = True, .Height = 60, .ScrollBars = ScrollBars.Vertical}
        tlp.Controls.Add(txtWorkability, 1, row)
        row += 1

        ' Cautions
        tlp.Controls.Add(New Label With {.Text = "Cautions:", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.TopLeft}, 0, row)
        txtCautions = New TextBox With {.Dock = DockStyle.Fill, .Multiline = True, .Height = 60, .ScrollBars = ScrollBars.Vertical}
        tlp.Controls.Add(txtCautions, 1, row)
        row += 1

        ' Notes
        tlp.Controls.Add(New Label With {.Text = "Notes:", .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.TopLeft}, 0, row)
        txtNotes = New TextBox With {.Dock = DockStyle.Fill, .Multiline = True, .Height = 60, .ScrollBars = ScrollBars.Vertical}
        tlp.Controls.Add(txtNotes, 1, row)
        row += 1

        ' Buttons
        Dim flowButtons As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.RightToLeft,
            .AutoSize = True
        }

        btnCancel = New Button With {.Text = "Cancel", .DialogResult = DialogResult.Cancel, .Width = 80}
        btnOK = New Button With {.Text = "Add Species", .DialogResult = DialogResult.OK, .Width = 100}
        AddHandler btnOK.Click, AddressOf BtnOK_Click

        flowButtons.Controls.Add(btnCancel)
        flowButtons.Controls.Add(btnOK)

        tlp.Controls.Add(New Label With {.Text = "* Required fields", .Dock = DockStyle.Fill, .ForeColor = Color.Gray}, 0, row)
        tlp.Controls.Add(flowButtons, 1, row)

        Me.Controls.Add(tlp)
        Me.AcceptButton = btnOK
        Me.CancelButton = btnCancel
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs)
        ' Validate required fields
        If String.IsNullOrWhiteSpace(txtCommonName.Text) Then
            MessageBox.Show("Please enter a common name for the wood species.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCommonName.Focus()
            Me.DialogResult = DialogResult.None
            Return
        End If

        ' Create the wood species data object
        _speciesData = New WoodPropertiesData With {
            .CommonName = txtCommonName.Text.Trim(),
            .ScientificName = txtScientificName.Text.Trim(),
            .WoodType = cmbWoodType.SelectedItem.ToString(),
            .JankaHardness = CInt(numJankaHardness.Value),
            .SpecificGravity = CDbl(numSpecificGravity.Value),
            .Density = CInt(numDensity.Value),
            .MoistureContent = CDbl(numMoistureContent.Value) / 100, ' Convert % to decimal
            .ShrinkageRadial = CDbl(numShrinkageRadial.Value) / 100, ' Convert % to decimal
            .ShrinkageTangential = CDbl(numShrinkageTangential.Value) / 100, ' Convert % to decimal
            .TypicalUses = txtTypicalUses.Text.Trim(),
            .Workability = txtWorkability.Text.Trim(),
            .Cautions = txtCautions.Text.Trim(),
            .Notes = txtNotes.Text.Trim()
        }
    End Sub

End Class
