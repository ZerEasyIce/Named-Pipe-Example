<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.btnStart = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tsslPipeStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsslPipeStatus_Value = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblCurrentStatus = New System.Windows.Forms.Label()
        Me.lblCurrentStatus_Value = New System.Windows.Forms.Label()
        Me.lblCurrentStep = New System.Windows.Forms.Label()
        Me.lblCurrentStep_Value = New System.Windows.Forms.Label()
        Me.lblPipeStatus = New System.Windows.Forms.Label()
        Me.lblPipeStep = New System.Windows.Forms.Label()
        Me.lblPipeStatus_Value = New System.Windows.Forms.Label()
        Me.lblPipeStep_Value = New System.Windows.Forms.Label()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(90, 86)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(10)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(75, 23)
        Me.btnStart.TabIndex = 0
        Me.btnStart.TabStop = False
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsslPipeStatus, Me.tsslPipeStatus_Value})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 119)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(254, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tsslPipeStatus
        '
        Me.tsslPipeStatus.Name = "tsslPipeStatus"
        Me.tsslPipeStatus.Size = New System.Drawing.Size(68, 17)
        Me.tsslPipeStatus.Text = "Pipe Status:"
        '
        'tsslPipeStatus_Value
        '
        Me.tsslPipeStatus_Value.Name = "tsslPipeStatus_Value"
        Me.tsslPipeStatus_Value.Size = New System.Drawing.Size(12, 17)
        Me.tsslPipeStatus_Value.Text = "-"
        '
        'lblCurrentStatus
        '
        Me.lblCurrentStatus.AutoSize = True
        Me.lblCurrentStatus.Location = New System.Drawing.Point(19, 19)
        Me.lblCurrentStatus.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblCurrentStatus.Name = "lblCurrentStatus"
        Me.lblCurrentStatus.Size = New System.Drawing.Size(74, 13)
        Me.lblCurrentStatus.TabIndex = 2
        Me.lblCurrentStatus.Text = "CurrentStatus:"
        '
        'lblCurrentStatus_Value
        '
        Me.lblCurrentStatus_Value.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.lblCurrentStatus_Value.Location = New System.Drawing.Point(96, 19)
        Me.lblCurrentStatus_Value.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblCurrentStatus_Value.Name = "lblCurrentStatus_Value"
        Me.lblCurrentStatus_Value.Size = New System.Drawing.Size(20, 13)
        Me.lblCurrentStatus_Value.TabIndex = 2
        Me.lblCurrentStatus_Value.Text = "-"
        Me.lblCurrentStatus_Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCurrentStep
        '
        Me.lblCurrentStep.AutoSize = True
        Me.lblCurrentStep.Location = New System.Drawing.Point(27, 42)
        Me.lblCurrentStep.Margin = New System.Windows.Forms.Padding(10)
        Me.lblCurrentStep.Name = "lblCurrentStep"
        Me.lblCurrentStep.Size = New System.Drawing.Size(66, 13)
        Me.lblCurrentStep.TabIndex = 2
        Me.lblCurrentStep.Text = "CurrentStep:"
        '
        'lblCurrentStep_Value
        '
        Me.lblCurrentStep_Value.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.lblCurrentStep_Value.Location = New System.Drawing.Point(96, 42)
        Me.lblCurrentStep_Value.Margin = New System.Windows.Forms.Padding(10)
        Me.lblCurrentStep_Value.Name = "lblCurrentStep_Value"
        Me.lblCurrentStep_Value.Size = New System.Drawing.Size(20, 13)
        Me.lblCurrentStep_Value.TabIndex = 2
        Me.lblCurrentStep_Value.Text = "-"
        Me.lblCurrentStep_Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPipeStatus
        '
        Me.lblPipeStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPipeStatus.AutoSize = True
        Me.lblPipeStatus.Location = New System.Drawing.Point(144, 19)
        Me.lblPipeStatus.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblPipeStatus.Name = "lblPipeStatus"
        Me.lblPipeStatus.Size = New System.Drawing.Size(66, 13)
        Me.lblPipeStatus.TabIndex = 2
        Me.lblPipeStatus.Text = "Status Main:"
        '
        'lblPipeStep
        '
        Me.lblPipeStep.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPipeStep.AutoSize = True
        Me.lblPipeStep.Location = New System.Drawing.Point(152, 42)
        Me.lblPipeStep.Margin = New System.Windows.Forms.Padding(10)
        Me.lblPipeStep.Name = "lblPipeStep"
        Me.lblPipeStep.Size = New System.Drawing.Size(58, 13)
        Me.lblPipeStep.TabIndex = 2
        Me.lblPipeStep.Text = "Step Main:"
        '
        'lblPipeStatus_Value
        '
        Me.lblPipeStatus_Value.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPipeStatus_Value.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.lblPipeStatus_Value.Location = New System.Drawing.Point(215, 19)
        Me.lblPipeStatus_Value.Margin = New System.Windows.Forms.Padding(10, 10, 10, 0)
        Me.lblPipeStatus_Value.Name = "lblPipeStatus_Value"
        Me.lblPipeStatus_Value.Size = New System.Drawing.Size(20, 13)
        Me.lblPipeStatus_Value.TabIndex = 2
        Me.lblPipeStatus_Value.Text = "-"
        Me.lblPipeStatus_Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPipeStep_Value
        '
        Me.lblPipeStep_Value.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPipeStep_Value.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.lblPipeStep_Value.Location = New System.Drawing.Point(215, 42)
        Me.lblPipeStep_Value.Margin = New System.Windows.Forms.Padding(10)
        Me.lblPipeStep_Value.Name = "lblPipeStep_Value"
        Me.lblPipeStep_Value.Size = New System.Drawing.Size(20, 13)
        Me.lblPipeStep_Value.TabIndex = 2
        Me.lblPipeStep_Value.Text = "-"
        Me.lblPipeStep_Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(254, 141)
        Me.Controls.Add(Me.lblPipeStep_Value)
        Me.Controls.Add(Me.lblPipeStatus_Value)
        Me.Controls.Add(Me.lblCurrentStep_Value)
        Me.Controls.Add(Me.lblPipeStep)
        Me.Controls.Add(Me.lblCurrentStatus_Value)
        Me.Controls.Add(Me.lblPipeStatus)
        Me.Controls.Add(Me.lblCurrentStep)
        Me.Controls.Add(Me.lblCurrentStatus)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnStart)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Named Pipe"
        Me.TopMost = True
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnStart As Button
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents tsslPipeStatus As ToolStripStatusLabel
    Friend WithEvents tsslPipeStatus_Value As ToolStripStatusLabel
    Friend WithEvents lblCurrentStatus As Label
    Friend WithEvents lblCurrentStatus_Value As Label
    Friend WithEvents lblCurrentStep As Label
    Friend WithEvents lblCurrentStep_Value As Label
    Friend WithEvents lblPipeStatus As Label
    Friend WithEvents lblPipeStep As Label
    Friend WithEvents lblPipeStatus_Value As Label
    Friend WithEvents lblPipeStep_Value As Label
End Class
