
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmManageCosts
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TcCosts = New System.Windows.Forms.TabControl()
        Me.TpWoodCosts = New System.Windows.Forms.TabPage()
        Me.ScWood = New System.Windows.Forms.SplitContainer()
        Me.DgvWoodCosts = New System.Windows.Forms.DataGridView()
        Me.PnlWoodButtons = New System.Windows.Forms.Panel()
        Me.BtnDeleteWoodCost = New System.Windows.Forms.Button()
        Me.BtnSaveWoodChanges = New System.Windows.Forms.Button()
        Me.BtnAddWoodCost = New System.Windows.Forms.Button()
        Me.TpEpoxyCosts = New System.Windows.Forms.TabPage()
        Me.ScEpoxy = New System.Windows.Forms.SplitContainer()
        Me.DgvEpoxyCosts = New System.Windows.Forms.DataGridView()
        Me.PnlEpoxyButtons = New System.Windows.Forms.Panel()
        Me.BtnDeleteEpoxyCost = New System.Windows.Forms.Button()
        Me.BtnSaveEpoxyChanges = New System.Windows.Forms.Button()
        Me.BtnAddEpoxyCost = New System.Windows.Forms.Button()
        Me.PnlBottom = New System.Windows.Forms.Panel()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.TcCosts.SuspendLayout()
        Me.TpWoodCosts.SuspendLayout()
        CType(Me.ScWood, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ScWood.Panel1.SuspendLayout()
        Me.ScWood.Panel2.SuspendLayout()
        Me.ScWood.SuspendLayout()
        CType(Me.DgvWoodCosts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlWoodButtons.SuspendLayout()
        Me.TpEpoxyCosts.SuspendLayout()
        CType(Me.ScEpoxy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ScEpoxy.Panel1.SuspendLayout()
        Me.ScEpoxy.Panel2.SuspendLayout()
        Me.ScEpoxy.SuspendLayout()
        CType(Me.DgvEpoxyCosts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlEpoxyButtons.SuspendLayout()
        Me.PnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'TcCosts
        '
        Me.TcCosts.Controls.Add(Me.TpWoodCosts)
        Me.TcCosts.Controls.Add(Me.TpEpoxyCosts)
        Me.TcCosts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TcCosts.Location = New System.Drawing.Point(0, 0)
        Me.TcCosts.Name = "TcCosts"
        Me.TcCosts.SelectedIndex = 0
        Me.TcCosts.Size = New System.Drawing.Size(884, 511)
        Me.TcCosts.TabIndex = 0
        '
        'TpWoodCosts
        '
        Me.TpWoodCosts.Controls.Add(Me.ScWood)
        Me.TpWoodCosts.Location = New System.Drawing.Point(4, 29)
        Me.TpWoodCosts.Name = "TpWoodCosts"
        Me.TpWoodCosts.Padding = New System.Windows.Forms.Padding(3)
        Me.TpWoodCosts.Size = New System.Drawing.Size(876, 478)
        Me.TpWoodCosts.TabIndex = 0
        Me.TpWoodCosts.Text = "Wood Costs"
        Me.TpWoodCosts.UseVisualStyleBackColor = True
        '
        'ScWood
        '
        Me.ScWood.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScWood.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.ScWood.IsSplitterFixed = True
        Me.ScWood.Location = New System.Drawing.Point(3, 3)
        Me.ScWood.Name = "ScWood"
        Me.ScWood.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'ScWood.Panel1
        '
        Me.ScWood.Panel1.Controls.Add(Me.DgvWoodCosts)
        '
        'ScWood.Panel2
        '
        Me.ScWood.Panel2.Controls.Add(Me.PnlWoodButtons)
        Me.ScWood.Size = New System.Drawing.Size(870, 472)
        Me.ScWood.SplitterDistance = 410
        Me.ScWood.TabIndex = 0
        '
        'DgvWoodCosts
        '
        Me.DgvWoodCosts.AllowUserToAddRows = False
        Me.DgvWoodCosts.AllowUserToDeleteRows = False
        Me.DgvWoodCosts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DgvWoodCosts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvWoodCosts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvWoodCosts.Location = New System.Drawing.Point(0, 0)
        Me.DgvWoodCosts.MultiSelect = False
        Me.DgvWoodCosts.Name = "DgvWoodCosts"
        Me.DgvWoodCosts.RowHeadersWidth = 51
        Me.DgvWoodCosts.RowTemplate.Height = 29
        Me.DgvWoodCosts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvWoodCosts.Size = New System.Drawing.Size(870, 410)
        Me.DgvWoodCosts.TabIndex = 0
        '
        'PnlWoodButtons
        '
        Me.PnlWoodButtons.Controls.Add(Me.BtnDeleteWoodCost)
        Me.PnlWoodButtons.Controls.Add(Me.BtnSaveWoodChanges)
        Me.PnlWoodButtons.Controls.Add(Me.BtnAddWoodCost)
        Me.PnlWoodButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlWoodButtons.Location = New System.Drawing.Point(0, 0)
        Me.PnlWoodButtons.Name = "PnlWoodButtons"
        Me.PnlWoodButtons.Size = New System.Drawing.Size(870, 58)
        Me.PnlWoodButtons.TabIndex = 0
        '
        'BtnDeleteWoodCost
        '
        Me.BtnDeleteWoodCost.Location = New System.Drawing.Point(286, 15)
        Me.BtnDeleteWoodCost.Name = "BtnDeleteWoodCost"
        Me.BtnDeleteWoodCost.Size = New System.Drawing.Size(130, 35)
        Me.BtnDeleteWoodCost.TabIndex = 2
        Me.BtnDeleteWoodCost.Text = "Delete"
        Me.BtnDeleteWoodCost.UseVisualStyleBackColor = True
        '
        'BtnSaveWoodChanges
        '
        Me.BtnSaveWoodChanges.Location = New System.Drawing.Point(150, 15)
        Me.BtnSaveWoodChanges.Name = "BtnSaveWoodChanges"
        Me.BtnSaveWoodChanges.Size = New System.Drawing.Size(130, 35)
        Me.BtnSaveWoodChanges.TabIndex = 1
        Me.BtnSaveWoodChanges.Text = "Save Changes"
        Me.BtnSaveWoodChanges.UseVisualStyleBackColor = True
        '
        'BtnAddWoodCost
        '
        Me.BtnAddWoodCost.Location = New System.Drawing.Point(14, 15)
        Me.BtnAddWoodCost.Name = "BtnAddWoodCost"
        Me.BtnAddWoodCost.Size = New System.Drawing.Size(130, 35)
        Me.BtnAddWoodCost.TabIndex = 0
        Me.BtnAddWoodCost.Text = "Add Wood Cost"
        Me.BtnAddWoodCost.UseVisualStyleBackColor = True
        '
        'TpEpoxyCosts
        '
        Me.TpEpoxyCosts.Controls.Add(Me.ScEpoxy)
        Me.TpEpoxyCosts.Location = New System.Drawing.Point(4, 29)
        Me.TpEpoxyCosts.Name = "TpEpoxyCosts"
        Me.TpEpoxyCosts.Padding = New System.Windows.Forms.Padding(3)
        Me.TpEpoxyCosts.Size = New System.Drawing.Size(876, 478)
        Me.TpEpoxyCosts.TabIndex = 1
        Me.TpEpoxyCosts.Text = "Epoxy Costs"
        Me.TpEpoxyCosts.UseVisualStyleBackColor = True
        '
        'ScEpoxy
        '
        Me.ScEpoxy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScEpoxy.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.ScEpoxy.IsSplitterFixed = True
        Me.ScEpoxy.Location = New System.Drawing.Point(3, 3)
        Me.ScEpoxy.Name = "ScEpoxy"
        Me.ScEpoxy.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'ScEpoxy.Panel1
        '
        Me.ScEpoxy.Panel1.Controls.Add(Me.DgvEpoxyCosts)
        '
        'ScEpoxy.Panel2
        '
        Me.ScEpoxy.Panel2.Controls.Add(Me.PnlEpoxyButtons)
        Me.ScEpoxy.Size = New System.Drawing.Size(870, 472)
        Me.ScEpoxy.SplitterDistance = 410
        Me.ScEpoxy.TabIndex = 0
        '
        'DgvEpoxyCosts
        '
        Me.DgvEpoxyCosts.AllowUserToAddRows = False
        Me.DgvEpoxyCosts.AllowUserToDeleteRows = False
        Me.DgvEpoxyCosts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DgvEpoxyCosts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvEpoxyCosts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvEpoxyCosts.Location = New System.Drawing.Point(0, 0)
        Me.DgvEpoxyCosts.MultiSelect = False
        Me.DgvEpoxyCosts.Name = "DgvEpoxyCosts"
        Me.DgvEpoxyCosts.RowHeadersWidth = 51
        Me.DgvEpoxyCosts.RowTemplate.Height = 29
        Me.DgvEpoxyCosts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvEpoxyCosts.Size = New System.Drawing.Size(870, 410)
        Me.DgvEpoxyCosts.TabIndex = 0
        '
        'PnlEpoxyButtons
        '
        Me.PnlEpoxyButtons.Controls.Add(Me.BtnDeleteEpoxyCost)
        Me.PnlEpoxyButtons.Controls.Add(Me.BtnSaveEpoxyChanges)
        Me.PnlEpoxyButtons.Controls.Add(Me.BtnAddEpoxyCost)
        Me.PnlEpoxyButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PnlEpoxyButtons.Location = New System.Drawing.Point(0, 0)
        Me.PnlEpoxyButtons.Name = "PnlEpoxyButtons"
        Me.PnlEpoxyButtons.Size = New System.Drawing.Size(870, 58)
        Me.PnlEpoxyButtons.TabIndex = 0
        '
        'BtnDeleteEpoxyCost
        '
        Me.BtnDeleteEpoxyCost.Location = New System.Drawing.Point(286, 15)
        Me.BtnDeleteEpoxyCost.Name = "BtnDeleteEpoxyCost"
        Me.BtnDeleteEpoxyCost.Size = New System.Drawing.Size(130, 35)
        Me.BtnDeleteEpoxyCost.TabIndex = 2
        Me.BtnDeleteEpoxyCost.Text = "Delete"
        Me.BtnDeleteEpoxyCost.UseVisualStyleBackColor = True
        '
        'BtnSaveEpoxyChanges
        '
        Me.BtnSaveEpoxyChanges.Location = New System.Drawing.Point(150, 15)
        Me.BtnSaveEpoxyChanges.Name = "BtnSaveEpoxyChanges"
        Me.BtnSaveEpoxyChanges.Size = New System.Drawing.Size(130, 35)
        Me.BtnSaveEpoxyChanges.TabIndex = 1
        Me.BtnSaveEpoxyChanges.Text = "Save Changes"
        Me.BtnSaveEpoxyChanges.UseVisualStyleBackColor = True
        '
        'BtnAddEpoxyCost
        '
        Me.BtnAddEpoxyCost.Location = New System.Drawing.Point(14, 15)
        Me.BtnAddEpoxyCost.Name = "BtnAddEpoxyCost"
        Me.BtnAddEpoxyCost.Size = New System.Drawing.Size(130, 35)
        Me.BtnAddEpoxyCost.TabIndex = 0
        Me.BtnAddEpoxyCost.Text = "Add Epoxy Cost"
        Me.BtnAddEpoxyCost.UseVisualStyleBackColor = True
        '
        'PnlBottom
        '
        Me.PnlBottom.Controls.Add(Me.BtnClose)
        Me.PnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PnlBottom.Location = New System.Drawing.Point(0, 511)
        Me.PnlBottom.Name = "PnlBottom"
        Me.PnlBottom.Size = New System.Drawing.Size(884, 50)
        Me.PnlBottom.TabIndex = 1
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnClose.Location = New System.Drawing.Point(762, 8)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(110, 35)
        Me.BtnClose.TabIndex = 0
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'FrmManageCosts
        '
        Me.AcceptButton = Me.BtnClose
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnClose
        Me.ClientSize = New System.Drawing.Size(884, 561)
        Me.Controls.Add(Me.TcCosts)
        Me.Controls.Add(Me.PnlBottom)
        Me.MinimumSize = New System.Drawing.Size(800, 500)
        Me.Name = "FrmManageCosts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Manage Costs - Wood && Epoxy"
        Me.TcCosts.ResumeLayout(False)
        Me.TpWoodCosts.ResumeLayout(False)
        Me.ScWood.Panel1.ResumeLayout(False)
        Me.ScWood.Panel2.ResumeLayout(False)
        CType(Me.ScWood, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ScWood.ResumeLayout(False)
        CType(Me.DgvWoodCosts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlWoodButtons.ResumeLayout(False)
        Me.TpEpoxyCosts.ResumeLayout(False)
        Me.ScEpoxy.Panel1.ResumeLayout(False)
        Me.ScEpoxy.Panel2.ResumeLayout(False)
        CType(Me.ScEpoxy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ScEpoxy.ResumeLayout(False)
        CType(Me.DgvEpoxyCosts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlEpoxyButtons.ResumeLayout(False)
        Me.PnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TcCosts As TabControl
    Friend WithEvents TpWoodCosts As TabPage
    Friend WithEvents TpEpoxyCosts As TabPage
    Friend WithEvents DgvWoodCosts As DataGridView
    Friend WithEvents DgvEpoxyCosts As DataGridView
    Friend WithEvents BtnAddWoodCost As Button
    Friend WithEvents BtnSaveWoodChanges As Button
    Friend WithEvents BtnDeleteWoodCost As Button
    Friend WithEvents BtnAddEpoxyCost As Button
    Friend WithEvents BtnSaveEpoxyChanges As Button
    Friend WithEvents BtnDeleteEpoxyCost As Button
    Friend WithEvents BtnClose As Button
    Friend WithEvents PnlBottom As Panel
    Friend WithEvents ScWood As SplitContainer
    Friend WithEvents PnlWoodButtons As Panel
    Friend WithEvents ScEpoxy As SplitContainer
    Friend WithEvents PnlEpoxyButtons As Panel
End Class
