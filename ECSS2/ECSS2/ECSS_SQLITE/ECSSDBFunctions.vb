Public Class ECSSDBFunctions
    Public Shared PartDB As SQLiteDBFunctions
    Public Shared UserDB As SQLiteDBFunctions


    Public Shared Function SelectWindowKit() As DataTable
        Try
            If PartDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT * FROM WINDOW_KIT"
            Result = PartDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select WINDOW_KIT", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function SelectTheromostat() As DataTable
        Try
            If PartDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT * FROM Theromostat"
            Result = PartDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select Theromostat", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function SelectTempSwitch() As DataTable
        Try
            If PartDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT * FROM TEMP_SWITCH"
            Result = PartDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select TEMP_SWITCH", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

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

    Public Shared Function SelectPilotLight() As DataTable
        Try
            If PartDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT * FROM PILOT_LIGHT"
            Result = PartDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select POWER_SUPPLY", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function SelectConfig() As DataTable
        Try
            If UserDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT * FROM USERSETTINGS"
            Result = UserDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "User Config", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function SelectUnit() As DataTable
        Try
            If PartDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT * FROM SYSTEM_UNITS"
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

    Public Shared Function SelectHeater() As DataTable
        Try
            If PartDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT * FROM HEATER"
            Result = PartDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select HEATER", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function SelectNonIlluminate() As DataTable
        Try
            If PartDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT * FROM NON_ILLUMINATE"
            Result = PartDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select NON_ILLUMINATE", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function SelectBOM() As DataTable
        Try
            If UserDB Is Nothing Then Return Nothing
            Dim Result As DataTable
            Dim sql As String = "SELECT l.BOMTITLE, l.CREATETIME, i.* FROM BOMITEM i, BOMLIST l WHERE l.BOMNAME = i.BOMNAME ORDER BY l.createtime"
            Result = UserDB.DBSelectWithDataTable(sql)
            Return Result
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ECSS Select BREATHER_DRAIN", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Shared Function UpdateUserConfig() As Boolean
        Try
            If UserDB Is Nothing Then Return False
            Dim SQL As String = "UPDATE USERSETTINGS SET MultiKeywords = " & CInt(GlobalSettings.MultiKeywords) & " , MaxDisplay= " & GlobalSettings.MaxDisplay &
                ", UnitType= " & CInt(GlobalSettings.SystemUnit) & ", AlwaysOnTop= " & CInt(GlobalSettings.AlwaysOnTop) & " ;"
            Return UserDB.DBModify(SQL)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "insert bom", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Shared Function InsertOneBOM(ByVal alist As OneBOMList, ByVal addProject As Boolean) As Boolean
        Try
            If UserDB Is Nothing Then Return False
            Dim SQL As String = ""
            If addProject Then
                SQL = SQL & "INSERT INTO BOMLIST VALUES('" & alist.BOMID & "' ,'" & alist.BOMTitle & "' ,datetime('now'),'ENABLE');" & vbCrLf
            Else
                SQL = SQL & "UPDATE BOMLIST SET BOMTITLE ='" & alist.BOMTitle & "' WHERE BOMNAME ='" & alist.BOMID & "' ;" & vbCrLf
            End If
            SQL = SQL & "DELETE FROM BOMITEM WHERE BOMNAME = '" & alist.BOMID & "';" & vbCrLf
            If alist.BOMList IsNot Nothing Then
                For Each aBOM In alist.BOMList
                    SQL = SQL & "INSERT INTO BOMITEM VALUES('" & aBOM.BOMID & "'," & aBOM.QTY & ",'" & aBOM.PartID & "','" & aBOM.Manufacturer & "','" & aBOM.Description & "','" & aBOM.Note & "');" & vbCrLf
                Next
            End If
            Return UserDB.DBModify(SQL)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "insert bom", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Public Shared Function DeleteOneBOM(ByVal BOMID As String) As Boolean
        Try
            If UserDB Is Nothing Then Return False
            Dim SQL As String = ""
            SQL = SQL & "DELETE FROM BOMITEM WHERE BOMNAME = '" & BOMID & "';" & vbCrLf
            SQL = SQL & "DELETE FROM BOMLIST WHERE BOMNAME = '" & BOMID & "';" & vbCrLf
            Return UserDB.DBModify(SQL)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "delete bom", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function


End Class
