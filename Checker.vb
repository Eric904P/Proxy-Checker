Imports System.IO
Imports System.Net
Imports System.Threading

Public Class Checker
    '///////////////START OF CHECKER BLOCK/////////////////
    Dim _thrdCnt As Integer = 0
    Private _thrdMax As Integer = 50
    ReadOnly _listLock As Object = New Object
    ReadOnly _d As New Dictionary(Of String, Thread)()
    ReadOnly _working As List(Of String) = New List(Of String)
    Private _toCheck As List(Of String) = New List(Of String)
    Private _toCheckTotal As Integer = 0
    Public IsRunning As Boolean = False

    Public Sub New(toCheck As List(Of String), thrdMax As Integer)
        _toCheck = toCheck
        _thrdMax = thrdMax
    End Sub

    Function CheckHerder() As Boolean
        IsRunning = True
        _toCheckTotal = _toCheck.Count
        Dim thrdIndex = 1
        While _toCheck.Count > 0 And IsRunning
            If _thrdCnt <= _thrdMax Then
                _d(thrdIndex.ToString) = New Thread(AddressOf CheckTask)
                _d(thrdIndex.ToString).IsBackground = True
                _d(thrdIndex.ToString).Start()
                _thrdCnt = _thrdCnt + 1
                thrdIndex = thrdIndex + 1
            End If
        End While
        IsRunning = False
        Return True
    End Function

    Private Sub CheckTask()
        If _toCheck.Count > 0 Then
            Dim toCheck As String
            SyncLock _listLock
                toCheck = _toCheck.Item(0)
                _toCheck.RemoveAt(0)
            End SyncLock
            If CheckProxy(toCheck) Then
                _working.Add(toCheck)
                Console.WriteLine(toCheck)
            End If
            _thrdCnt = _thrdCnt - 1
        End If
    End Sub

    'test single proxy
    Function CheckProxy(proxy As String) As Boolean
        Try 'uses azenv.net proxy judge
            Dim r As HttpWebRequest = HttpWebRequest.Create("http://azenv.net")
            r.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.2 Safari/537.36"
            r.Timeout = 3000
            r.ReadWriteTimeout = 3000
            r.Proxy = New WebProxy(proxy)
            Using sr As New StreamReader(r.GetResponse().GetResponseStream())
                If sr.ReadToEnd().Contains("HTTP_HOST = azenv.net") Then
                    r.Abort()
                    Return True
                End If
            End Using
            r.Abort()
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Public Function ReturnWorking() As List(Of String)
        Return _working
    End Function

    Public Function ReturnThreadCount() As Integer
        Return _thrdCnt
    End Function

    Public Function ReturnPercent() As Double
        Return ((_toCheckTotal - _toCheck.Count) / _toCheckTotal)
    End Function

    Public Function ReturnCheckedCount() As Integer
        Return (_toCheckTotal - _toCheck.Count)
    End Function

    Public Function ReturnToCheckTotal() As Integer
        Return _toCheckTotal
    End Function

    Public Function GetThreadCount() As Integer
        Return _thrdCnt
    End Function

    Public Function ReturnToCheckCount() As Integer
        Return _toCheck.Count
    End Function

    Public Sub SetThreadMax(thrdMax As Integer)
        _thrdMax = thrdMax
    End Sub

    Public Sub SetProxies(proxies As List(Of String))
        _toCheck = proxies
    End Sub

    Public Sub Clear()
        _toCheck.Clear()
        _toCheckTotal = 0
        _thrdCnt = 0
        _working.Clear()
        _d.Clear()
    End Sub

    Public Function ReturnWorkingCount() As Integer
        Return _working.Count
    End Function
End Class