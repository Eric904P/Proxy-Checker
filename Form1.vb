Imports System.IO
Imports System.Net
Imports System.Threading
Public Class Form1
    Dim l1, l2 As New ArrayList
    Dim proxies As New List(Of String)
    Dim count As Integer = 0
    Dim index As Integer = 0
    Dim curProx As String
    Dim d As New Dictionary(Of String, Thread)()
    Private curProxLock As Object = New Object
    Private indexLock As Object = New Object
    Private l1Lock As Object = New Object
    Private l2Lock As Object = New Object

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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

            ProgressBar1.Maximum = proxies.Count()
            ProgressBar1.Minimum = 0
            ProgressBar1.Step = 1
            Dim percent As Integer = Math.Round(count / proxies.Count() * 100)
            Label1.Text = "Progress: " & count & "/" & proxies.Count() & " checked " & "(" & percent & "%)"
            Label1.Update()
        End If
    End Sub

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
        l2.Clear()
        count = 0
        index = 0
        curProx = ""
        d.Clear()
        Return True
    End Function

    Private Function clearFields() As Boolean
        Try
            If d(1).IsAlive Then
                MessageBox.Show("Please stop all threads first.")
                Return False
            End If
        Catch ex As Exception

        End Try

        ListBox2.Items.Clear()
        Label1.Text = "Progress: 0/0 checked (0.00%)"
        Label4.Text = "Unresponsive:"
        Label5.Text = "Working:"
        ProgressBar1.Value = 0

        ListBox2.Update()
        Label1.Update()
        Label4.Update()
        Label5.Update()
        ProgressBar1.Update()

        Return True
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            stopThreads()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If (ListBox2.Items.Count > 0) Then
            Dim tempL As ArrayList
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

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        ToolTip1.SetToolTip(TrackBar1, TrackBar1.Value.ToString())
        Label3.Text = TrackBar1.Value
        Label3.Update()
    End Sub

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
            Return False
        End Try
        Return False
    End Function

    Private Sub threadedProxyChecker()
        While (index < (proxies.Count() - 1))
            If curProx = proxies.Item(index) Then
                GoTo IndexInc
            Else
                SyncLock curProxLock
                    curProx = proxies.Item(index)
                End SyncLock
            End If
            If l2.Contains(proxies.Item(index)) = False Then
                If l1.Contains(proxies.Item(index)) = False Then
                    If (checkProxy(proxies.Item(index))) Then
                        SyncLock l1Lock
                            performStep(True, proxies.Item(index))
                            l1.Add(proxies.Item(index))
                        End SyncLock
                    Else
                        SyncLock l2Lock
                            performStep(False, proxies.Item(index))
                            l2.Add(proxies.Item(index))
                        End SyncLock
                    End If
                End If
            End If
IndexInc:
            SyncLock indexLock
                index = index + 1
            End SyncLock
        End While
        clearVars()
    End Sub

    Private Sub stopThreads()
        Dim thrdNum As Integer = 1
        For Each t As KeyValuePair(Of String, Thread) In d
            t.Value.Abort()
            Label1.Text = "Stopping: " & thrdNum & "/" & d.Count()
            Label1.Update()
            thrdNum = thrdNum + 1
        Next
        Label1.Text = "Stopped!"
        Label1.Update()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            stopThreads()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim threadCount As Integer = TrackBar1.Value

        For int As Integer = 1 To threadCount Step 1
            d(int.ToString) = New Thread(AddressOf threadedProxyChecker)
            d(int.ToString).IsBackground = True
            d(int.ToString).Start()
        Next
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If clearFields() Then
            clearVars()
        End If
    End Sub

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

    Function performStep(bool As Boolean, proxy As String)
        If bool Then
            ListBox2.Invoke(Sub()
                                ListBox2.Items.Add(proxy)
                                ListBox2.TopIndex = ListBox2.Items.Count - 1
                                ListBox2.Update()
                                Label5.Text = "Working: " & l1.Count()
                                Label5.Update()
                            End Sub)
        Else
            Label4.Invoke(Sub()
                              Label4.Text = "Unresponsive: " & l2.Count()
                              Label4.Update()
                          End Sub)
        End If

        count = count + 1

        ProgressBar1.Invoke(Sub()
                                ProgressBar1.PerformStep()
                                ProgressBar1.Update()
                            End Sub)

        Label1.Invoke(Sub()
                          Dim percent As Double = Math.Round((count / proxies.Count() * 100), 2, MidpointRounding.AwayFromZero)
                          Label1.Text = "Progress: " & count & "/" & proxies.Count() & " checked " & "(" & percent & "%)"
                          Label1.Update()
                      End Sub)
        Return True
    End Function

End Class