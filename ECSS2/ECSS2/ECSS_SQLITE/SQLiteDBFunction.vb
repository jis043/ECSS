Imports System.Data.SQLite
Imports System.Data.OleDb


Public Class SQLiteDBFunctions
    Public SQLconnect As SQLiteConnection
    Public SQLcommand As SQLiteCommand
    Private DBPath As String
    Private RetryCount As Integer = 0 ' if connect retry over 10 times then disconnect 
    Private IsDistrDB As Boolean = False

    Public Sub New()

    End Sub

    Public Sub New(ByVal dbpath As String, ByVal NeedPassword As Boolean)
        Try
            'If System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(dbpath)) = False Then
            '    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dbpath))
            'End If
            If System.IO.File.Exists(dbpath) = False Then
                System.IO.File.Create(dbpath).Dispose()
            End If
            Me.DBPath = dbpath
            Me.DBConnect(NeedPassword)
        Catch ex As Exception
            MessageBox.Show("Exception when open exiting SQLite file, Try to reset a new DB file." & vbCrLf & ex.ToString)
        End Try
    End Sub

    'Public Sub New(ByVal dbpath As String, ByVal IsDistr As Boolean)
    '    'This only use fro SVSOILS
    '    Try
    '        If System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(dbpath)) = False Then
    '            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dbpath))
    '        End If
    '        If System.IO.File.Exists(dbpath) = False Then
    '            System.IO.File.Create(dbpath).Dispose()
    '        End If
    '        Me.DBPath = dbpath
    '        Me.IsDistrDB = IsDistr
    '        If IsDistrDB Then
    '            DBConnect_Distr5()
    '        Else
    '            DBConnect_SV5()
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("Exception when open exiting SQLite file, Try to reset a new DB file." & vbCrLf & ex.ToString)
    '    End Try

    'End Sub

    Public Sub DBConnect(Optional ByVal NeedPassword As Boolean = False)
        SQLconnect = New SQLite.SQLiteConnection()
        SQLconnect.ConnectionString = "Data Source=" & Me.DBPath & ";"
        If NeedPassword Then SQLconnect.SetPassword(GlobalSettings.CODEBOOK)
        SQLconnect.Open()
    End Sub

    Public Sub DBConnect_SV5()
        If Me.RetryCount > 10 Then Debug.Print("Try to connect the SQLite over 10 times.") : Me.Close() : Exit Sub

        Try
            SQLconnect = New SQLite.SQLiteConnection()
            SQLconnect.ConnectionString = "Data Source=" & Me.DBPath & ";"

            'SQLconnect.SetPassword(SVS.GlobalSettings.SQLitePassword)

            SQLconnect.Open()
            SQLcommand = SQLconnect.CreateCommand
            SQLcommand.CommandText = "SELECT name FROM sqlite_master;"
            SQLcommand.ExecuteReader()

        Catch ex As Exception
            If ex.Message.Contains("file is encrypted") Then
                'first time open a uncrypt db then encrypt it.

                Me.RetryCount += 1
                DBConnect()
            End If
            Debug.Print(ex.ToString)
        Finally
            SQLcommand.Dispose()
        End Try

    End Sub

    Public Sub DBConnect_Distr5()
        'If GlobalSettings.SoftwareID <> GlobalSettings.SVS_SOFTWARE.SOILVISION Then Exit Sub
        If Me.RetryCount > 10 Then MessageBox.Show("Try to connect the SQLite over 10 times.") : Me.Close() : Exit Sub

        Try
            'Distr DB must encrypted
            SQLconnect = New SQLite.SQLiteConnection()
            SQLconnect.ConnectionString = "Data Source=" & Me.DBPath & ";"
            'SQLconnect.SetPassword(SVS.GlobalSettings.SQLitePassword)
            SQLconnect.Open()
            SQLcommand = SQLconnect.CreateCommand
            SQLcommand.CommandText = "SELECT name FROM sqlite_master;"
            SQLcommand.ExecuteReader()

        Catch ex As Exception
            Me.RetryCount += 1
            DBConnect_Distr5()
            Debug.Print(ex.ToString)
        Finally
            SQLcommand.Dispose()
        End Try
    End Sub

    Public Function DBSelect(ByVal Query As String) As List(Of String())
        Try
            Dim Result As New List(Of String())
            Dim i As Integer
            If Me.SQLconnect.State = ConnectionState.Closed OrElse Me.SQLconnect.State = ConnectionState.Broken Then
                Me.DBConnect()
            End If
            SQLcommand = SQLconnect.CreateCommand
            SQLcommand.CommandText = Query
            Dim SQLreader As SQLiteDataReader = SQLcommand.ExecuteReader()
            While SQLreader.Read()
                Dim TString(SQLreader.FieldCount - 1) As String
                For i = 0 To SQLreader.FieldCount - 1
                    TString(i) = SQLreader(i).ToString
                Next
                Result.Add(TString)
            End While
            SQLcommand.Dispose()
            Return Result
        Catch dbex As System.Data.SQLite.SQLiteException
            If dbex.ErrorCode = -2147467259 Then
                'transatction error 
                Debug.Print("Transaction Error" & vbCrLf & dbex.ToString)
                SQLcommand.Dispose()
                Me.Close()
            Else
                Debug.Print("SQLite Error" & vbCrLf & dbex.ToString)
                SQLcommand.Dispose()
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("QueryDB" & vbCrLf & ex.ToString)
            SQLcommand.Dispose()
            Return Nothing
        End Try
    End Function

    Public Function DBSelectWithDataTable(ByVal Query As String) As DataTable
        Try
            Dim DB As SQLiteDataAdapter
            Dim DT As DataTable
            Dim DS As New DataSet()
            If Me.SQLconnect.State = ConnectionState.Closed OrElse Me.SQLconnect.State = ConnectionState.Broken Then
                Me.DBConnect()
            End If
            DB = New SQLiteDataAdapter(Query, Me.SQLconnect)
            DS.Reset()
            DB.Fill(DS)
            DT = DS.Tables(0)
            Return DT
        Catch dbex As System.Data.SQLite.SQLiteException
            If dbex.ErrorCode = -2147467259 Then
                'transatction error 
                Debug.Print("Transaction Error" & vbCrLf & dbex.ToString)
                SQLcommand.Dispose()
                Me.Close()
            Else
                Debug.Print("SQLite Error" & vbCrLf & dbex.ToString)
                SQLcommand.Dispose()
            End If
            Return Nothing
        Catch ex As Exception
            Debug.Print("QueryDB" & vbCrLf & ex.ToString)
            SQLcommand.Dispose()
            Return Nothing
        End Try
    End Function

    Public Function DBModify(ByVal Query As String, Optional isNeedCommit As Boolean = True) As Boolean
        Try
            Dim result As Integer
            If Me.SQLconnect.State = ConnectionState.Closed OrElse Me.SQLconnect.State = ConnectionState.Broken Then
                Me.DBConnect()
            End If

            'If SQLcommand IsNot Nothing Then
            '    SQLcommand.Dispose()
            'End If

            SQLcommand = SQLconnect.CreateCommand
            If isNeedCommit Then
                SQLcommand.CommandText = "BEGIN; " & Query & " COMMIT;"
            Else
                SQLcommand.CommandText = Query
            End If

            result = SQLcommand.ExecuteNonQuery()
            SQLcommand.Dispose()
            If result < 0 Then
                Return False
            Else
                Return True
            End If
        Catch dbex As System.Data.SQLite.SQLiteException
            If dbex.ErrorCode = System.Data.SQLite.SQLiteErrorCode.Error Then
                'transatction error 
                If dbex.Message.Contains("cannot start a transaction within a transaction") Then
                    Threading.Thread.Sleep(2000)
                    DBModify(Query)
                Else
                    Debug.Print("Transaction Error" & vbCrLf & dbex.ToString)
                    SQLcommand.Dispose()
                    Me.Close()
                End If
            Else
                Debug.Print("SQLite Error" & vbCrLf & dbex.ToString)
                SQLcommand.Dispose()
            End If
            Return False
        Catch ex As Exception
            Debug.Print("UpdateDB" & vbCrLf & ex.ToString)
            SQLcommand.Dispose()
            Return False
        End Try
    End Function


    Public Sub Close()
        SQLconnect.Close()
    End Sub

    Public Function sqliteEscape(keyWord As String) As String
        keyWord = keyWord.Replace("/", "//")
        keyWord = keyWord.Replace("'", "''")
        'keyWord = keyWord.Replace("[", "/[")
        'keyWord = keyWord.Replace("]", "/]")
        'keyWord = keyWord.Replace("%", "/%")
        'keyWord = keyWord.Replace("&", "/&")
        'keyWord = keyWord.Replace("_", "/_")
        'keyWord = keyWord.Replace("(", "/(")
        'keyWord = keyWord.Replace(")", "/)")
        Return keyWord
    End Function

    Public Shared Function IsSQLiteFileEncrypted(ByVal DBPath As String) As Boolean
        Dim Sconnect As SQLiteConnection = Nothing
        Dim Scommand As SQLiteCommand = Nothing
        Try
            Sconnect = New SQLite.SQLiteConnection()
            Sconnect.ConnectionString = "Data Source=" & DBPath & ";"
            Sconnect.Open()
            Scommand = Sconnect.CreateCommand
            Scommand.CommandText = "SELECT name FROM sqlite_master;"
            Scommand.ExecuteReader()
            Return False
        Catch ex As Exception
            If ex.Message.Contains("not a database") Then
                Return True
            End If
            Return False
            Debug.Print("QueryDB" & vbCrLf & ex.ToString)
        Finally
            If Scommand IsNot Nothing Then Scommand.Dispose()
            If Sconnect IsNot Nothing Then Sconnect.Close()
        End Try
    End Function

    Public Shared Sub EncryptSQLiteFile(ByVal DBFile As String)
        Dim SQLconnect As SQLiteConnection = Nothing
        Try
            If System.IO.File.Exists(DBFile) Then
                SQLconnect = New SQLite.SQLiteConnection()
                SQLconnect.ConnectionString = "Data Source=" & DBFile & ";"
                SQLconnect.Open()
                SQLconnect.ChangePassword(GlobalSettings.CODEBOOK)
                'SVS.GlobalSettings.IsSQLiteDatabaseFileEncrypted = True
            End If
        Catch ex As Exception
            If ex.Message.Contains("not a database") Then
                'The db file is encrypted already.
                Exit Sub
            End If
        Finally
            If SQLconnect IsNot Nothing Then SQLconnect.Close()
        End Try
    End Sub

    Public Shared Sub DecryptSQLiteFile(ByVal DBFile As String)
        Dim SQLconnect As SQLiteConnection = Nothing
        If System.IO.File.Exists(DBFile) Then
            Try
                SQLconnect = New SQLite.SQLiteConnection()
                SQLconnect.ConnectionString = "Data Source=" & DBFile & ";"
                SQLconnect.SetPassword(GlobalSettings.CODEBOOK)
                SQLconnect.Open()
                SQLconnect.ChangePassword("")
                SQLconnect.Close()
            Catch ex As Exception
                If TypeOf ex Is System.Data.SQLite.SQLiteException AndAlso DirectCast(ex, System.Data.SQLite.SQLiteException).ErrorCode = 26 Then
                    Debug.Print("Not Error!")
                Else
                    Debug.Print("DB Error")
                End If
            End Try

        End If
    End Sub

End Class


