Public Class GlobalSettings

    Public Enum UNITTYPE
        METRIC = 0
        IMPERIAL = 1
    End Enum

    Public Const ECSS_DB As String = "ECSS.db3"
    Public Const ECSSUSER_DB As String = "ECSSUSER.db3"
    Public Const ECSS_TITLE As String = "Engineering Components Search Systems"
    Public Const MAIN_VER As Integer = 0
    Public Const SUB_VER As Integer = 1
    Public Const MIN_VER As Integer = 1

    Public SystemUnit As UNITTYPE = UNITTYPE.METRIC

    Public Shared Function GetCurrentVersion() As String
        Dim str As String = ""
        str = "Version: " & MAIN_VER & "." & SUB_VER & "." & MIN_VER
        Return str
    End Function

    Public Shared Function GetDEMOVersion() As String
        Return "Version: DEMO"
    End Function
End Class
