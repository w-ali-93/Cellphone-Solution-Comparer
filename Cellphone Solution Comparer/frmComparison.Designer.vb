<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmComparison
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
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtVisitDays = New System.Windows.Forms.TextBox()
        Me.txtDataUsage = New System.Windows.Forms.TextBox()
        Me.txtMinutes = New System.Windows.Forms.TextBox()
        Me.txtSMS = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.rdbLocal = New System.Windows.Forms.RadioButton()
        Me.rdbAT = New System.Windows.Forms.RadioButton()
        Me.rdbT = New System.Windows.Forms.RadioButton()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(736, 15)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Visit Days:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(640, 53)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(169, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Approximate Data Usage:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(667, 96)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(143, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Approximate Minutes:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(767, 134)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(41, 17)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "SMS:"
        '
        'txtVisitDays
        '
        Me.txtVisitDays.Location = New System.Drawing.Point(819, 15)
        Me.txtVisitDays.Margin = New System.Windows.Forms.Padding(4)
        Me.txtVisitDays.Name = "txtVisitDays"
        Me.txtVisitDays.Size = New System.Drawing.Size(115, 22)
        Me.txtVisitDays.TabIndex = 4
        Me.txtVisitDays.Text = "5"
        '
        'txtDataUsage
        '
        Me.txtDataUsage.Location = New System.Drawing.Point(819, 53)
        Me.txtDataUsage.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDataUsage.Name = "txtDataUsage"
        Me.txtDataUsage.Size = New System.Drawing.Size(115, 22)
        Me.txtDataUsage.TabIndex = 5
        Me.txtDataUsage.Text = "5"
        '
        'txtMinutes
        '
        Me.txtMinutes.Location = New System.Drawing.Point(819, 96)
        Me.txtMinutes.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMinutes.Name = "txtMinutes"
        Me.txtMinutes.Size = New System.Drawing.Size(115, 22)
        Me.txtMinutes.TabIndex = 6
        Me.txtMinutes.Text = "10"
        '
        'txtSMS
        '
        Me.txtSMS.Location = New System.Drawing.Point(819, 134)
        Me.txtSMS.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSMS.Name = "txtSMS"
        Me.txtSMS.Size = New System.Drawing.Size(115, 22)
        Me.txtSMS.TabIndex = 7
        Me.txtSMS.Text = "5"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(20, 175)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(915, 521)
        Me.DataGridView1.TabIndex = 12
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(20, 113)
        Me.btnGenerate.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(89, 37)
        Me.btnGenerate.TabIndex = 16
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'rdbLocal
        '
        Me.rdbLocal.AutoSize = True
        Me.rdbLocal.Location = New System.Drawing.Point(20, 15)
        Me.rdbLocal.Margin = New System.Windows.Forms.Padding(4)
        Me.rdbLocal.Name = "rdbLocal"
        Me.rdbLocal.Size = New System.Drawing.Size(63, 21)
        Me.rdbLocal.TabIndex = 17
        Me.rdbLocal.TabStop = True
        Me.rdbLocal.Text = "Local"
        Me.rdbLocal.UseVisualStyleBackColor = True
        '
        'rdbAT
        '
        Me.rdbAT.AutoSize = True
        Me.rdbAT.Location = New System.Drawing.Point(20, 43)
        Me.rdbAT.Margin = New System.Windows.Forms.Padding(4)
        Me.rdbAT.Name = "rdbAT"
        Me.rdbAT.Size = New System.Drawing.Size(65, 21)
        Me.rdbAT.TabIndex = 18
        Me.rdbAT.TabStop = True
        Me.rdbAT.Text = "AT&&T"
        Me.rdbAT.UseVisualStyleBackColor = True
        '
        'rdbT
        '
        Me.rdbT.AutoSize = True
        Me.rdbT.Location = New System.Drawing.Point(20, 71)
        Me.rdbT.Margin = New System.Windows.Forms.Padding(4)
        Me.rdbT.Name = "rdbT"
        Me.rdbT.Size = New System.Drawing.Size(84, 21)
        Me.rdbT.TabIndex = 19
        Me.rdbT.TabStop = True
        Me.rdbT.Text = "T-Mobile"
        Me.rdbT.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(67, 4)
        '
        'frmComparison
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(955, 710)
        Me.Controls.Add(Me.rdbT)
        Me.Controls.Add(Me.rdbAT)
        Me.Controls.Add(Me.rdbLocal)
        Me.Controls.Add(Me.btnGenerate)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtSMS)
        Me.Controls.Add(Me.txtMinutes)
        Me.Controls.Add(Me.txtDataUsage)
        Me.Controls.Add(Me.txtVisitDays)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmComparison"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Comparison"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtVisitDays As System.Windows.Forms.TextBox
    Friend WithEvents txtDataUsage As System.Windows.Forms.TextBox
    Friend WithEvents txtMinutes As System.Windows.Forms.TextBox
    Friend WithEvents txtSMS As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents rdbLocal As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAT As System.Windows.Forms.RadioButton
    Friend WithEvents rdbT As System.Windows.Forms.RadioButton
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
End Class
