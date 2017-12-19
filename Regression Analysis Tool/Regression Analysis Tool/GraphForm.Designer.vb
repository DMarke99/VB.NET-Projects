<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GraphForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GraphForm))
        Me.GraphPictureBox = New System.Windows.Forms.PictureBox()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveGraphToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyGraphToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GraphSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GraphMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        CType(Me.GraphPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GraphMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'GraphPictureBox
        '
        resources.ApplyResources(Me.GraphPictureBox, "GraphPictureBox")
        Me.GraphPictureBox.Name = "GraphPictureBox"
        Me.GraphPictureBox.TabStop = False
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveGraphToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        '
        'SaveGraphToolStripMenuItem
        '
        Me.SaveGraphToolStripMenuItem.Name = "SaveGraphToolStripMenuItem"
        resources.ApplyResources(Me.SaveGraphToolStripMenuItem, "SaveGraphToolStripMenuItem")
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        resources.ApplyResources(Me.ExitToolStripMenuItem, "ExitToolStripMenuItem")
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyGraphToolStripMenuItem, Me.GraphSettingsToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        resources.ApplyResources(Me.EditToolStripMenuItem, "EditToolStripMenuItem")
        '
        'CopyGraphToolStripMenuItem
        '
        Me.CopyGraphToolStripMenuItem.Name = "CopyGraphToolStripMenuItem"
        resources.ApplyResources(Me.CopyGraphToolStripMenuItem, "CopyGraphToolStripMenuItem")
        '
        'GraphSettingsToolStripMenuItem
        '
        Me.GraphSettingsToolStripMenuItem.Name = "GraphSettingsToolStripMenuItem"
        resources.ApplyResources(Me.GraphSettingsToolStripMenuItem, "GraphSettingsToolStripMenuItem")
        '
        'GraphMenuStrip
        '
        Me.GraphMenuStrip.BackColor = System.Drawing.SystemColors.Control
        Me.GraphMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem})
        resources.ApplyResources(Me.GraphMenuStrip, "GraphMenuStrip")
        Me.GraphMenuStrip.Name = "GraphMenuStrip"
        '
        'GraphForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GraphPictureBox)
        Me.Controls.Add(Me.GraphMenuStrip)
        Me.MainMenuStrip = Me.GraphMenuStrip
        Me.MaximizeBox = False
        Me.Name = "GraphForm"
        CType(Me.GraphPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GraphMenuStrip.ResumeLayout(False)
        Me.GraphMenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GraphPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveGraphToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyGraphToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GraphSettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GraphMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker

End Class
