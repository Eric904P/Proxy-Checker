Imports System.IO
Imports System.Net
Imports System.Threading
Public Class Form1
    'Dim l1, l2 As New ArrayList
    Dim totalCount As Integer = 0
    Dim isDone As Boolean = True
    Private uiCtrl As Thread = New Thread(AddressOf UiControler)
    Private Checker As Checker = New Checker(New List(Of String), TrackBar1.Value)

    'Load proxies dialogue 
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim proxies As List(Of String) = New List(Of String)
        If Checker.IsRunning Then
            MessageBox.Show("Please stop all threads first!")
            Exit Sub
        End If
        Clear()
        Dim fo As New OpenFileDialog
        fo.RestoreDirectory = True
        fo.Multiselect = False
        fo.Filter = "txt files (*.txt)|*.txt"
        fo.FilterIndex = 1
        fo.ShowDialog()
        If (Not fo.FileName = Nothing) Then
            Using sr As New StreamReader(fo.FileName)
                While sr.Peek <> -1
                    proxies.Add(sr.ReadLine())
                End While
            End Using
        End If
        totalCount = proxies.Count()
        Checker.SetProxies(proxies)

        ProgressBar1.Value = 0
        Dim percent As Integer = Math.Round((totalCount - proxies.Count()) / totalCount * 100)
        Label1.Text = "Progress: " & percent & "% checked " & "(" & proxies.Count() & " left)"
        Label1.Update()
    End Sub

    'zeroes variables used by program
    Private Function Clear() As Boolean
        Try
            If Checker.IsRunning Then
                MessageBox.Show("Please stop all threads first.")
                Return False
            End If
        Catch ex As Exception

        End Try

        'Program Reset
        totalCount = 0
        Checker.Clear()

        'UI Reset
        Label1.Text = "Progress: 0.00% checked (0 left)"
        Label4.Text = "Unresponsive:"
        Label5.Text = "Working:"
        ProgressBar1.Value = 0

        Label1.Update()
        Label4.Update()
        Label5.Update()
        ProgressBar1.Update()
        Return True
    End Function


    'Stop button controller
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Checker.IsRunning = False
        Label1.Text = "Stopped!"
        Label1.Update()
        While Checker.GetThreadCount() > 0
            isDone = False
        End While
        isDone = True
    End Sub

    'Save dialogue
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (Checker.ReturnWorking().Any()) Then
            Dim tempL As List(Of String)
            tempL = Checker.ReturnWorking()
            Dim fs As New SaveFileDialog
            fs.RestoreDirectory = True
            fs.Filter = "txt files (*.txt)|*.txt"
            fs.FilterIndex = 1
            fs.ShowDialog()
            If Not (fs.FileName = Nothing) Then
                Using sw As New StreamWriter(fs.FileName)
                    For Each line As String In tempL
                        sw.WriteLine(line)
                    Next
                End Using
            End If
        Else
            MessageBox.Show("No working proxies to save!")
        End If
    End Sub

    'Start button
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Checker.IsRunning Then
            MessageBox.Show("Program is already running!")
            Exit Sub
        End If
        isDone = False
        Checker.SetThreadMax(TrackBar1.Value)

        uiCtrl.IsBackground = True
        uiCtrl.Start()

        If Checker.CheckHerder() Then
            isDone = True
        End If
    End Sub

    'Clear button
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Checker.IsRunning Then
            MessageBox.Show("Please stop all threads first!")
            Exit Sub
        End If
        Clear()
    End Sub

    'Copy to clipboard
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim clip As String = String.Empty
        If Checker.ReturnWorking().Any() Then
            clip = String.Join(vbNewLine, Checker.ReturnWorking().ToArray())
            Clipboard.SetText(clip)
            MessageBox.Show(Checker.ReturnWorking().Count() & " proxies copied to clipboard!")
        Else
            MessageBox.Show("No proxies to copy!")
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        TrackBar1.Value = NumericUpDown1.Value
        TrackBar1.Update()
    End Sub

    'UI control thread
    Private Sub UiControler()
        While Not isDone
            Label5.Invoke(Sub()
                              Label5.Text = "Working: " & Checker.ReturnWorkingCount()
                              Label5.Update()
                          End Sub)
            Label4.Invoke(Sub()
                              Label4.Text = "Unresponsive: " & (Checker.ReturnCheckedCount() - Checker.ReturnWorkingCount())
                              Label4.Update()
                          End Sub)
            Label1.Invoke(Sub()
                              Label1.Text = "Progress: " & Math.Round(Checker.ReturnPercent(), 2, MidpointRounding.AwayFromZero) & "% checked " & "(" & Checker.ReturnToCheckCount() & " left)"
                              Label1.Update()
                          End Sub)
            ProgressBar1.Invoke(Sub()
                                    ProgressBar1.Value = Math.Round(Checker.ReturnPercent(), 0)
                                    ProgressBar1.Update()
                                End Sub)
            Label7.Invoke(Sub()
                              Label7.Text = Checker.GetThreadCount()

                              Label7.Update()
                          End Sub)
            Thread.Sleep(100)
            'Thread.Sleep(1)
        End While
    End Sub

End Class