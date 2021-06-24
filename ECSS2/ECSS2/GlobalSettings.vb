Public Class GlobalSettings

    Public Enum UNITTYPE
        METRIC = 0
        IMPERIAL = 1
    End Enum

    Public Const COMPANY As String = "Prexeco"
    Public Const ECSS_DB As String = "ECSS.db3"
    Public Const ECSSUSER_DB As String = "ECSSUSER.db3"
    Public Const ECSS_TITLE As String = "Engineering Components Search Systems"
    Public Const MAIN_VER As Integer = 0
    Public Const SUB_VER As Integer = 7
    Public Const MIN_VER As Integer = 10
    Public Const CODEBOOK As String = "AlexLongYan"
    Public Const USAGE_URL As String = "http://phpserver1.prexeco.com/ECSSWSDL/REST.php"

    Public Shared SystemUnit As UNITTYPE = UNITTYPE.METRIC
    Public Shared MaxDisplay As Integer = 1000
    Public Shared MultiKeywords As Boolean = False
    Public Shared AlwaysOnTop As Boolean = False
    Public Shared MachineID As String = ""
    Public Shared RegistrationKey As String = ""

    Public Shared Function GetCurrentVersion() As String
        Dim str As String = ""
        str = "Version: " & MAIN_VER & "." & SUB_VER & "." & MIN_VER
        Return str
    End Function

    Public Shared Function GetDEMOVersion() As String
        Return "Version: DEMO"
    End Function

    Public Shared Sub LoadUserConfig()
        Dim DT = ECSSDBFunctions.SelectConfig
        If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then
            For Each aRow As DataRow In DT.Rows
                SystemUnit = CInt(aRow.Item("UnitType"))
                MaxDisplay = CInt(aRow.Item("MaxDisplay"))
                MultiKeywords = CBool(aRow.Item("MultiKeywords"))
                AlwaysOnTop = CBool(aRow.Item("AlwaysOnTop"))
                RegistrationKey = aRow.Item("Authorization").ToString
            Next
        End If
    End Sub

    Public Shared Function ECSSUSER_DBPath() As String
        Dim result As String = ""
        result = System.IO.Path.Combine(FileIO.SpecialDirectories.MyDocuments, "ECSS")
        Return result
    End Function
End Class
