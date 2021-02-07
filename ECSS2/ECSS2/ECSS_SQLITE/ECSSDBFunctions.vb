Public Class ECSSDBFunctions
    Public Shared PartDB As SQLiteDBFunctions
    Public Shared UserDB As SQLiteDBFunctions

    Public Shared Function SelectTransformer() As DataTable
        Try
            If PartDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT * FROM TRANSFORMER"
            Result = PartDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select Transformer", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function SelectPowerSupply() As DataTable
        Try
            If PartDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT * FROM POWER_SUPPLY"
            Result = PartDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select POWER_SUPPLY", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function SelectEnclosure() As DataTable
        Try
            If PartDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT * FROM ENCLOSURE"
            Result = PartDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select ENCLOSURE", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Public Shared Function SelectServitPost() As DataTable
        Try
            If PartDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT * FROM SERVIT_POST"
            Result = PartDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select SERVIT_POST", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Public Shared Function SelectBreatherDrain() As DataTable
        Try
            If PartDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT * FROM BREATHER_DRAIN"
            Result = PartDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select BREATHER_DRAIN", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function SelectBOM() As DataTable
        Try
            If UserDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT i.* FROM BOMITEM i, BOMLIST l WHERE l.BOMNAME = i.BOMNAME ORDER BY l.createtime DESC"
            Result = UserDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select BREATHER_DRAIN", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function InsertOneBOM(ByVal KVP As KeyValuePair(Of String, List(Of ECSSBOM)), ByVal addProject As Boolean) As Boolean
        Try
            If UserDB Is Nothing Then Return False
            Dim SQL As String = ""
            If addProject Then
                SQL = SQL & "INSERT INTO BOMLIST VALUES('" & KVP.Key & "',date('now'),'ENABLE');" & vbCrLf
            End If
            SQL = SQL & "DELETE FROM BOMITEM WHERE BOMNAME = '" & KVP.Key & "';" & vbCrLf
            If KVP.Value IsNot Nothing Then
                For Each aBOM In KVP.Value
                    SQL = SQL & "INSERT INTO BOMITEM VALUES('" & aBOM.BOMID & "'," & aBOM.QTY & ",'" & aBOM.PartID & "','" & aBOM.Manufacturer & "','" & aBOM.Description & "','" & aBOM.Note & "');" & vbCrLf
                Next
            End If
            Return UserDB.DBModify(SQL)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select BREATHER_DRAIN", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function


End Class
