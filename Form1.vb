Imports System.IO
Imports System.Net
Imports System.Threading
Public Class Form1
    'Dim l1, l2 As New ArrayList
    Dim proxies, l1 As New List(Of String)
    Dim l2 As Integer = 0
    Dim totalCount As Integer = 0
    Dim thrdCnt As Integer = 0
    Dim isBox As Boolean = False
    Dim isDone As Boolean = True
    Dim d As New Dictionary(Of String, Thread)()
    Private curProxLock As Object = New Object
    Private l1Lock As Object = New Object
    Private l2Lock As Object = New Object
    Private indexLock As Object = New Object
    Private uiCtrl As Thread = New Thread(AddressOf uiControler)
    Private isRunning As Boolean = False

    'Load proxies dialogue 
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If isRunning Then
            MessageBox.Show("Please stop all threads first!")
            Exit Sub
        End If
        clearVars()
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

        ProgressBar1.Value = 0
        Dim percent As Integer = Math.Round((totalCount - proxies.Count()) / totalCount * 100)
        Label1.Text = "Progress: " & percent & "% checked " & "(" & proxies.Count() & " left)"
        Label1.Update()
    End Sub

    'zeroes variables used by program
    Private Function clearVars() As Boolean
        Try
            If d(1).IsAlive Then
                MessageBox.Show("Please stop all threads first.")
                Return False
            End If
        Catch ex As Exception

        End Try

        proxies.Clear()
        l1.Clear()
        l2 = 0
        totalCount = 0
        d.Clear()
        Return True
    End Function

    'clears UI
    Private Function clearFields() As Boolean
        Try
            If d(1).IsAlive Then
                MessageBox.Show("Please stop all threads first.")
                Return False
            End If
        Catch ex As Exception

        End Try

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
    Private Sub Button2_Click(sender As Object, e As EventArgs)
        isRunning = False
        Label1.Text = "Stopped!"
        Label1.Update()
        While thrdCnt > 0
            isDone = False
        End While
        isDone = True
    End Sub

    'Save dialogue
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (l1.Count() > 0) Then
            Dim tempL As List(Of String)
            SyncLock l1Lock
                tempL = l1
            End SyncLock
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

    'Thread count controller
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        ToolTip1.SetToolTip(TrackBar1, TrackBar1.Value.ToString())
        NumericUpDown1.Value = TrackBar1.Value
        NumericUpDown1.Update()
    End Sub

    'test single proxy
    Function checkProxy(proxy As String) As Boolean
        Dim myProxy As WebProxy
        Dim Temp As String
        Try
            myProxy = New WebProxy(proxy)
            Dim r As HttpWebRequest = HttpWebRequest.Create("http://azenv.net")
            r.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.2 Safari/537.36"
            r.Timeout = 3000
            r.Proxy = myProxy
            Dim re As HttpWebResponse = r.GetResponse()
            Dim rs As Stream = re.GetResponseStream
            Using sr As New StreamReader(rs)
                Temp = sr.ReadToEnd()
            End Using
            Dim Text = Temp
            rs.Dispose()
            rs.Close()
            r.Abort()
            If Text.Contains("HTTP_HOST = azenv.net") Then
                If Text.Contains("REQUEST_TIME =") Then
                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception

        End Try
        Return False
    End Function

    'thread action
    Private Sub threadedProxyChecker()
        Dim proxy As String
        While isRunning
            If proxies.Count() = 0 Then
                Exit While
            End If
            SyncLock curProxLock
                proxy = proxies.Item(0)
                proxies.RemoveAt(0)
            End SyncLock
            If (checkProxy(proxy)) Then
                l1.Add(proxy)
            Else
                l2 += 1
            End If
        End While
        SyncLock l2Lock
            thrdCnt = thrdCnt - 1
        End SyncLock
        'check for job completion
        If thrdCnt = 0 And Not isBox Then
            MessageBox.Show("Done checking!" & vbNewLine & l1.Count() & " working proxies")
            isBox = True
            isRunning = False
            isDone = True
        End If
    End Sub

    'second Stop button controller 
    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        isRunning = False
        Label1.Text = "Stopped!"
        Label1.Update()
        While thrdCnt > 0
            isDone = False
        End While
        isDone = True
    End Sub

    'Start button
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If isRunning Then
            MessageBox.Show("Program is already running!")
            Exit Sub
        End If
        isBox = False
        isRunning = True
        isDone = False
        thrdCnt = TrackBar1.Value

        uiCtrl.IsBackground = True
        uiCtrl.Start()

        For int As Integer = 1 To thrdCnt Step 1
            d(int.ToString) = New Thread(AddressOf threadedProxyChecker)
            d(int.ToString).IsBackground = True
            d(int.ToString).Start()
        Next
    End Sub

    'Clear button
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If isRunning Then
            MessageBox.Show("Please stop all threads first!")
            Exit Sub
        End If
        If clearFields() Then
            clearVars()
        End If
    End Sub

    'Copy to clipboard
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim clip As String = String.Empty
        If l1.Count() > 0 Then
            clip = String.Join(vbNewLine, l1.ToArray())
            Clipboard.SetText(clip)
            MessageBox.Show(l1.Count() & " proxies copied to clipboard!")
        Else
            MessageBox.Show("No proxies to copy!")
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        TrackBar1.Value = NumericUpDown1.Value
        TrackBar1.Update()
    End Sub

    'UI control thread
    Private Sub uiControler()
        Dim percent As Double = 0
        While Not isDone
            Label5.Invoke(Sub()
                              Label5.Text = "Working: " & l1.Count()
                              Label5.Update()
                          End Sub)
            Label4.Invoke(Sub()
                              Label4.Text = "Unresponsive: " & l2
                              Label4.Update()
                          End Sub)
            percent = Math.Round(((totalCount - proxies.Count()) / totalCount * 100), 2, MidpointRounding.AwayFromZero)
            Label1.Invoke(Sub()
                              Label1.Text = "Progress: " & percent & "% checked " & "(" & proxies.Count() & " left)"
                              Label1.Update()
                          End Sub)
            ProgressBar1.Invoke(Sub()
                                    ProgressBar1.Value = Math.Round(percent, 0)
                                    ProgressBar1.Update()
                                End Sub)
            Label7.Invoke(Sub()
                              Label7.Text = thrdCnt

                              Label7.Update()
                          End Sub)
            uiCtrl.Sleep(1)
            'Thread.Sleep(1)
        End While
    End Sub

End Class