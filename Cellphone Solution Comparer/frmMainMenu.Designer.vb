<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainMenu
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
        Me.btnShowMap = New System.Windows.Forms.Button()
        Me.btnModLocal = New System.Windows.Forms.Button()
        Me.btnModAT = New System.Windows.Forms.Button()
        Me.btnModT = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnShowMap
        '
        Me.btnShowMap.Location = New System.Drawing.Point(12, 12)
        Me.btnShowMap.Name = "btnShowMap"
        Me.btnShowMap.Size = New System.Drawing.Size(138, 29)
        Me.btnShowMap.TabIndex = 0
        Me.btnShowMap.Text = "Show Map"
        Me.btnShowMap.UseVisualStyleBackColor = True
        '
        'btnModLocal
        '
        Me.btnModLocal.Location = New System.Drawing.Point(13, 82)
        Me.btnModLocal.Name = "btnModLocal"
        Me.btnModLocal.Size = New System.Drawing.Size(138, 29)
        Me.btnModLocal.TabIndex = 2
        Me.btnModLocal.Text = "Modify Local Countries"
        Me.btnModLocal.UseVisualStyleBackColor = True
        '
        'btnModAT
        '
        Me.btnModAT.Location = New System.Drawing.Point(13, 117)
        Me.btnModAT.Name = "btnModAT"
        Me.btnModAT.Size = New System.Drawing.Size(138, 29)
        Me.btnModAT.TabIndex = 3
        Me.btnModAT.Text = "Modify ATnT Rates"
        Me.btnModAT.UseVisualStyleBackColor = True
        '
        'btnModT
        '
        Me.btnModT.Location = New System.Drawing.Point(13, 152)
        Me.btnModT.Name = "btnModT"
        Me.btnModT.Size = New System.Drawing.Size(138, 29)
        Me.btnModT.TabIndex = 4
        Me.btnModT.Text = "Modify T-Mobile Rates"
        Me.btnModT.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(12, 187)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(138, 29)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmMainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(162, 228)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnModT)
        Me.Controls.Add(Me.btnModAT)
        Me.Controls.Add(Me.btnModLocal)
        Me.Controls.Add(Me.btnShowMap)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMainMenu"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Main Menu"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnShowMap As System.Windows.Forms.Button
    Friend WithEvents btnModLocal As System.Windows.Forms.Button
    Friend WithEvents btnModAT As System.Windows.Forms.Button
    Friend WithEvents btnModT As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button

End Class
