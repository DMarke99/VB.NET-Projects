<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainMenu))
        Me.CreateNewSession = New System.Windows.Forms.Button()
        Me.ExitProject = New System.Windows.Forms.Button()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.UserGuideButton = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CreateNewSession
        '
        Me.CreateNewSession.Location = New System.Drawing.Point(341, 12)
        Me.CreateNewSession.Name = "CreateNewSession"
        Me.CreateNewSession.Size = New System.Drawing.Size(133, 23)
        Me.CreateNewSession.TabIndex = 0
        Me.CreateNewSession.Text = "Create New Session"
        Me.CreateNewSession.UseVisualStyleBackColor = True
        '
        'ExitProject
        '
        Me.ExitProject.Location = New System.Drawing.Point(341, 70)
        Me.ExitProject.Name = "ExitProject"
        Me.ExitProject.Size = New System.Drawing.Size(133, 23)
        Me.ExitProject.TabIndex = 1
        Me.ExitProject.Text = "Exit Program"
        Me.ExitProject.UseVisualStyleBackColor = True
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "data.csv"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(323, 199)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'UserGuideButton
        '
        Me.UserGuideButton.Location = New System.Drawing.Point(341, 41)
        Me.UserGuideButton.Name = "UserGuideButton"
        Me.UserGuideButton.Size = New System.Drawing.Size(132, 23)
        Me.UserGuideButton.TabIndex = 3
        Me.UserGuideButton.Text = "User Guide"
        Me.UserGuideButton.UseVisualStyleBackColor = True
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(480, 223)
        Me.Controls.Add(Me.UserGuideButton)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ExitProject)
        Me.Controls.Add(Me.CreateNewSession)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Main Menu"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CreateNewSession As System.Windows.Forms.Button
    Friend WithEvents ExitProject As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents UserGuideButton As System.Windows.Forms.Button
End Class
