<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AnalysisForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AnalysisForm))
        Me.AnalysisTab = New System.Windows.Forms.TabPage()
        Me.SummaryTextbox = New System.Windows.Forms.TextBox()
        Me.AnalysisMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.AutomaticAnalysisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManualAnalysisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PlotRegressionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LinearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PolynomialToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NoExpressionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RegressionSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SingleLinearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MultipleLinearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MultiplicativeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PolynomialSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatisticalSummaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PMCCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SpearmansRankToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SummaryStatisticsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IndependenceAnalysisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LinearSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MultiplicativeSearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnalysisWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UndoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RedoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RegressionFormMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToMenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitProgramToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableTab = New System.Windows.Forms.TabPage()
        Me.csvViewer = New System.Windows.Forms.DataGridView()
        Me.MenuScreenTabs = New System.Windows.Forms.TabControl()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.AnalysisTab.SuspendLayout()
        Me.AnalysisMenuStrip.SuspendLayout()
        Me.RegressionFormMenuStrip.SuspendLayout()
        Me.TableTab.SuspendLayout()
        CType(Me.csvViewer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuScreenTabs.SuspendLayout()
        Me.SuspendLayout()
        '
        'AnalysisTab
        '
        Me.AnalysisTab.Controls.Add(Me.SummaryTextbox)
        Me.AnalysisTab.Controls.Add(Me.AnalysisMenuStrip)
        Me.AnalysisTab.Location = New System.Drawing.Point(4, 22)
        Me.AnalysisTab.Name = "AnalysisTab"
        Me.AnalysisTab.Padding = New System.Windows.Forms.Padding(3)
        Me.AnalysisTab.Size = New System.Drawing.Size(752, 502)
        Me.AnalysisTab.TabIndex = 1
        Me.AnalysisTab.Text = "Analysis"
        Me.AnalysisTab.UseVisualStyleBackColor = True
        '
        'SummaryTextbox
        '
        Me.SummaryTextbox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SummaryTextbox.Font = New System.Drawing.Font("Consolas", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SummaryTextbox.Location = New System.Drawing.Point(6, 30)
        Me.SummaryTextbox.Multiline = True
        Me.SummaryTextbox.Name = "SummaryTextbox"
        Me.SummaryTextbox.ReadOnly = True
        Me.SummaryTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.SummaryTextbox.Size = New System.Drawing.Size(740, 466)
        Me.SummaryTextbox.TabIndex = 2
        '
        'AnalysisMenuStrip
        '
        Me.AnalysisMenuStrip.BackColor = System.Drawing.Color.White
        Me.AnalysisMenuStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.AnalysisMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AutomaticAnalysisToolStripMenuItem, Me.ManualAnalysisToolStripMenuItem, Me.AnalysisWindowToolStripMenuItem})
        Me.AnalysisMenuStrip.Location = New System.Drawing.Point(6, 3)
        Me.AnalysisMenuStrip.Name = "AnalysisMenuStrip"
        Me.AnalysisMenuStrip.Size = New System.Drawing.Size(346, 24)
        Me.AnalysisMenuStrip.TabIndex = 5
        Me.AnalysisMenuStrip.Text = "Analysis Tab Menu Strip"
        '
        'AutomaticAnalysisToolStripMenuItem
        '
        Me.AutomaticAnalysisToolStripMenuItem.Name = "AutomaticAnalysisToolStripMenuItem"
        Me.AutomaticAnalysisToolStripMenuItem.Size = New System.Drawing.Size(121, 20)
        Me.AutomaticAnalysisToolStripMenuItem.Text = "Automatic Analysis"
        '
        'ManualAnalysisToolStripMenuItem
        '
        Me.ManualAnalysisToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PlotRegressionToolStripMenuItem, Me.RegressionSummaryToolStripMenuItem, Me.StatisticalSummaryToolStripMenuItem, Me.IndependenceAnalysisToolStripMenuItem})
        Me.ManualAnalysisToolStripMenuItem.Name = "ManualAnalysisToolStripMenuItem"
        Me.ManualAnalysisToolStripMenuItem.Size = New System.Drawing.Size(108, 20)
        Me.ManualAnalysisToolStripMenuItem.Text = " Manual Analysis"
        '
        'PlotRegressionToolStripMenuItem
        '
        Me.PlotRegressionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LinearToolStripMenuItem, Me.PolynomialToolStripMenuItem, Me.NoExpressionToolStripMenuItem})
        Me.PlotRegressionToolStripMenuItem.Name = "PlotRegressionToolStripMenuItem"
        Me.PlotRegressionToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.PlotRegressionToolStripMenuItem.Text = "Plot Regression"
        '
        'LinearToolStripMenuItem
        '
        Me.LinearToolStripMenuItem.Name = "LinearToolStripMenuItem"
        Me.LinearToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.LinearToolStripMenuItem.Text = "Linear"
        '
        'PolynomialToolStripMenuItem
        '
        Me.PolynomialToolStripMenuItem.Name = "PolynomialToolStripMenuItem"
        Me.PolynomialToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.PolynomialToolStripMenuItem.Text = "Polynomial"
        '
        'NoExpressionToolStripMenuItem
        '
        Me.NoExpressionToolStripMenuItem.Name = "NoExpressionToolStripMenuItem"
        Me.NoExpressionToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.NoExpressionToolStripMenuItem.Text = "No Expression"
        '
        'RegressionSummaryToolStripMenuItem
        '
        Me.RegressionSummaryToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SingleLinearToolStripMenuItem, Me.MultipleLinearToolStripMenuItem, Me.MultiplicativeToolStripMenuItem, Me.PolynomialSummaryToolStripMenuItem})
        Me.RegressionSummaryToolStripMenuItem.Name = "RegressionSummaryToolStripMenuItem"
        Me.RegressionSummaryToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.RegressionSummaryToolStripMenuItem.Text = "Regression Summary"
        '
        'SingleLinearToolStripMenuItem
        '
        Me.SingleLinearToolStripMenuItem.Name = "SingleLinearToolStripMenuItem"
        Me.SingleLinearToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.SingleLinearToolStripMenuItem.Text = "Single Linear"
        '
        'MultipleLinearToolStripMenuItem
        '
        Me.MultipleLinearToolStripMenuItem.Name = "MultipleLinearToolStripMenuItem"
        Me.MultipleLinearToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.MultipleLinearToolStripMenuItem.Text = "Multiple Linear"
        '
        'MultiplicativeToolStripMenuItem
        '
        Me.MultiplicativeToolStripMenuItem.Name = "MultiplicativeToolStripMenuItem"
        Me.MultiplicativeToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.MultiplicativeToolStripMenuItem.Text = "Multiplicative"
        '
        'PolynomialSummaryToolStripMenuItem
        '
        Me.PolynomialSummaryToolStripMenuItem.Name = "PolynomialSummaryToolStripMenuItem"
        Me.PolynomialSummaryToolStripMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.PolynomialSummaryToolStripMenuItem.Text = "Polynomial"
        '
        'StatisticalSummaryToolStripMenuItem
        '
        Me.StatisticalSummaryToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PMCCToolStripMenuItem, Me.SpearmansRankToolStripMenuItem, Me.SummaryStatisticsToolStripMenuItem})
        Me.StatisticalSummaryToolStripMenuItem.Name = "StatisticalSummaryToolStripMenuItem"
        Me.StatisticalSummaryToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.StatisticalSummaryToolStripMenuItem.Text = "Statistical Summary"
        '
        'PMCCToolStripMenuItem
        '
        Me.PMCCToolStripMenuItem.Name = "PMCCToolStripMenuItem"
        Me.PMCCToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.PMCCToolStripMenuItem.Text = "PMCC"
        '
        'SpearmansRankToolStripMenuItem
        '
        Me.SpearmansRankToolStripMenuItem.Name = "SpearmansRankToolStripMenuItem"
        Me.SpearmansRankToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.SpearmansRankToolStripMenuItem.Text = "Spearman's Rank"
        '
        'SummaryStatisticsToolStripMenuItem
        '
        Me.SummaryStatisticsToolStripMenuItem.Name = "SummaryStatisticsToolStripMenuItem"
        Me.SummaryStatisticsToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.SummaryStatisticsToolStripMenuItem.Text = "Summary Statistics"
        '
        'IndependenceAnalysisToolStripMenuItem
        '
        Me.IndependenceAnalysisToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LinearSearchToolStripMenuItem, Me.MultiplicativeSearchToolStripMenuItem})
        Me.IndependenceAnalysisToolStripMenuItem.Name = "IndependenceAnalysisToolStripMenuItem"
        Me.IndependenceAnalysisToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.IndependenceAnalysisToolStripMenuItem.Text = "Independence Analysis"
        '
        'LinearSearchToolStripMenuItem
        '
        Me.LinearSearchToolStripMenuItem.Name = "LinearSearchToolStripMenuItem"
        Me.LinearSearchToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.LinearSearchToolStripMenuItem.Text = "Linear Search"
        '
        'MultiplicativeSearchToolStripMenuItem
        '
        Me.MultiplicativeSearchToolStripMenuItem.Name = "MultiplicativeSearchToolStripMenuItem"
        Me.MultiplicativeSearchToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.MultiplicativeSearchToolStripMenuItem.Text = "Multiplicative Search"
        '
        'AnalysisWindowToolStripMenuItem
        '
        Me.AnalysisWindowToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoToolStripMenuItem, Me.RedoToolStripMenuItem, Me.ClearToolStripMenuItem, Me.SaveToolStripMenuItem})
        Me.AnalysisWindowToolStripMenuItem.Name = "AnalysisWindowToolStripMenuItem"
        Me.AnalysisWindowToolStripMenuItem.Size = New System.Drawing.Size(109, 20)
        Me.AnalysisWindowToolStripMenuItem.Text = "Analysis Window"
        '
        'UndoToolStripMenuItem
        '
        Me.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
        Me.UndoToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.UndoToolStripMenuItem.Text = "Undo"
        '
        'RedoToolStripMenuItem
        '
        Me.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem"
        Me.RedoToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.RedoToolStripMenuItem.Text = "Redo"
        '
        'ClearToolStripMenuItem
        '
        Me.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem"
        Me.ClearToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.ClearToolStripMenuItem.Text = "Clear"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'RegressionFormMenuStrip
        '
        Me.RegressionFormMenuStrip.BackColor = System.Drawing.SystemColors.Control
        Me.RegressionFormMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.RegressionFormMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.RegressionFormMenuStrip.Name = "RegressionFormMenuStrip"
        Me.RegressionFormMenuStrip.Size = New System.Drawing.Size(784, 24)
        Me.RegressionFormMenuStrip.TabIndex = 2
        Me.RegressionFormMenuStrip.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToMenuToolStripMenuItem, Me.ExitProgramToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ExitToMenuToolStripMenuItem
        '
        Me.ExitToMenuToolStripMenuItem.Name = "ExitToMenuToolStripMenuItem"
        Me.ExitToMenuToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ExitToMenuToolStripMenuItem.Text = "Back to Main Menu"
        '
        'ExitProgramToolStripMenuItem
        '
        Me.ExitProgramToolStripMenuItem.Name = "ExitProgramToolStripMenuItem"
        Me.ExitProgramToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ExitProgramToolStripMenuItem.Text = "Exit Program"
        '
        'TableTab
        '
        Me.TableTab.Controls.Add(Me.csvViewer)
        Me.TableTab.Location = New System.Drawing.Point(4, 22)
        Me.TableTab.Name = "TableTab"
        Me.TableTab.Padding = New System.Windows.Forms.Padding(3)
        Me.TableTab.Size = New System.Drawing.Size(752, 502)
        Me.TableTab.TabIndex = 0
        Me.TableTab.Text = "Data"
        Me.TableTab.UseVisualStyleBackColor = True
        '
        'csvViewer
        '
        Me.csvViewer.AllowUserToAddRows = False
        Me.csvViewer.AllowUserToDeleteRows = False
        Me.csvViewer.AllowUserToResizeRows = False
        Me.csvViewer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.csvViewer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.csvViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.csvViewer.Location = New System.Drawing.Point(6, 6)
        Me.csvViewer.Name = "csvViewer"
        Me.csvViewer.ReadOnly = True
        Me.csvViewer.RowHeadersVisible = False
        Me.csvViewer.RowTemplate.ReadOnly = True
        Me.csvViewer.Size = New System.Drawing.Size(740, 490)
        Me.csvViewer.TabIndex = 0
        '
        'MenuScreenTabs
        '
        Me.MenuScreenTabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MenuScreenTabs.Controls.Add(Me.TableTab)
        Me.MenuScreenTabs.Controls.Add(Me.AnalysisTab)
        Me.MenuScreenTabs.Location = New System.Drawing.Point(12, 27)
        Me.MenuScreenTabs.Name = "MenuScreenTabs"
        Me.MenuScreenTabs.SelectedIndex = 0
        Me.MenuScreenTabs.Size = New System.Drawing.Size(760, 528)
        Me.MenuScreenTabs.TabIndex = 1
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog"
        '
        'AnalysisForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.MenuScreenTabs)
        Me.Controls.Add(Me.RegressionFormMenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "AnalysisForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Analysis"
        Me.AnalysisTab.ResumeLayout(False)
        Me.AnalysisTab.PerformLayout()
        Me.AnalysisMenuStrip.ResumeLayout(False)
        Me.AnalysisMenuStrip.PerformLayout()
        Me.RegressionFormMenuStrip.ResumeLayout(False)
        Me.RegressionFormMenuStrip.PerformLayout()
        Me.TableTab.ResumeLayout(False)
        CType(Me.csvViewer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuScreenTabs.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AnalysisTab As System.Windows.Forms.TabPage
    Friend WithEvents SummaryTextbox As System.Windows.Forms.TextBox
    Friend WithEvents TableTab As System.Windows.Forms.TabPage
    Friend WithEvents csvViewer As System.Windows.Forms.DataGridView
    Friend WithEvents MenuScreenTabs As System.Windows.Forms.TabControl
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents AnalysisMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents AutomaticAnalysisToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManualAnalysisToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PlotRegressionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LinearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PolynomialToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RegressionSummaryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SingleLinearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MultipleLinearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MultiplicativeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PolynomialSummaryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatisticalSummaryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PMCCToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SpearmansRankToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SummaryStatisticsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RegressionFormMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToMenuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitProgramToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AnalysisWindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UndoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RedoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NoExpressionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IndependenceAnalysisToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LinearSearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MultiplicativeSearchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
