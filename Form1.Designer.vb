﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(212, 13)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(110, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Load Proxies"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(12, 92)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(194, 147)
        Me.ListBox1.TabIndex = 1
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(328, 13)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(93, 23)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Export Working"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 245)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(409, 23)
        Me.ProgressBar1.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 271)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(139, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Progress: 0/0 checked (0%)"
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(111, 42)
        Me.TrackBar1.Maximum = 500
        Me.TrackBar1.Minimum = 1
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(310, 45)
        Me.TrackBar1.TabIndex = 6
        Me.TrackBar1.TickFrequency = 10
        Me.TrackBar1.Value = 300
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        Me.ToolTip1.ToolTipTitle = "Thread Count"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(16, 13)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(109, 23)
        Me.Button4.TabIndex = 7
        Me.Button4.Text = "<< START >>"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Thread Count:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Location = New System.Drawing.Point(90, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 15)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = Me.TrackBar1.Value
        '
        'ListBox2
        '
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.Location = New System.Drawing.Point(212, 92)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(209, 147)
        Me.ListBox2.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 74)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Unresponsive:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(209, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Working:"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(132, 13)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "Stop"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(328, 271)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(93, 23)
        Me.Button5.TabIndex = 14
        Me.Button5.Text = "Clear All"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(433, 298)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TrackBar1)
        Me.Name = "Form1"
        Me.Text = "Proxy Checker"
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Label1 As Label
    Friend WithEvents TrackBar1 As TrackBar
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Button4 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents ListBox2 As ListBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents ColorDialog1 As ColorDialog
End Class