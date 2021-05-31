Imports System
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Management

Public Class Authorization

    Public Enum AUTHORIZATION_LEVEL
        NONE = 0
        STANDARD = 1
        DEVELOPER = 2
    End Enum


    Public Shared Function GetProcessorId() As String
        Dim strProcessorId As String = String.Empty
        Dim query As New SelectQuery("Win32_processor")
        Dim search As New ManagementObjectSearcher(query)
        Dim info As ManagementObject

        For Each info In search.Get()
            strProcessorId = info("processorId").ToString()
        Next
        Return strProcessorId

    End Function

    Public Shared Function BiosSerNr() As String

        Dim query As New SelectQuery("Win32_bios")
        Dim search As New ManagementObjectSearcher(query)
        Dim info As ManagementObject

        Dim Temp() As String

        Dim BiosInfo As String = ""

        For Each info In search.Get()
            BiosInfo &= info("serialnumber").ToString & ","
        Next

        Temp = BiosInfo.Split(","c)

        'Dialog1.Text = Temp(1)

        BiosInfo = Temp(0)

        Return BiosInfo

    End Function

    Shared Function GetHash(theInput As String) As String

        Using sha256Hash As SHA256 = SHA256.Create()   ' create hash object

            ' Convert to byte array and get hash
            Dim dbytes As Byte() =
             sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(theInput))

            ' sb to create string from bytes
            Dim sBuilder As New StringBuilder()

            ' convert byte data to hex string
            For n As Integer = 0 To dbytes.Length - 1
                sBuilder.Append(dbytes(n).ToString("X2"))
            Next n

            Return sBuilder.ToString()
        End Using

    End Function

    Public Shared Function CovertASCII(ByVal ascii As String) As String
        Dim NewValue As String = ""
        ' Below is used twice in a row in case Ascii is still over 20 once the first if statement runs.
        ' This only happens if the code is all lower case c which is Ascii value of 99 (i.e. code = cccc-cccc-cccc-cccc-cccc)

        If ascii.Length > 20 Then
            ascii = ascii.Remove(20, ascii.Length - 20)
        End If


        ' NOTE: IntToAdd contains 20 decimal numbers. Each Ascii character can be from 0 to 9. When each Ascii characters value is added
        ' to the IntoToAdd value a new decimal value is returned. This value converted to Ascii will either be a 0 to 9 or a capital
        ' letter A to Z.
        ' For the Integers range of 48 to 57 a 48 is placed in a location in InToAdd and will always return between a
        ' 0 to 9 for that position in the created "key".
        ' For the Capital letters range of 65 to 90 anything up to an 81 can be placed in a location in InToAdd and will always return
        ' a range of 10 letters for that position. For example a 65 will always return between an A to J. 81 will always return between
        ' Q to Z.
        ' In the first position of IntToAdd a 64 for letters or 47 for numbers could be used as the first character in Ascii will always
        ' be greater than zero.

        ' Also IntoToAdd must be the same in this program as it is in the Application - Authorization Key Maker program.

        ' Integers = Ascii Decimal 48 to 57
        ' Capital letters = Ascii Decimal 65 to 90

        ascii = ascii.Insert(4, "-")
        ascii = ascii.Insert(9, "-")
        ascii = ascii.Insert(14, "-")
        ascii = ascii.Insert(19, "-")

        Return ascii
    End Function

    Public Shared Function GenMachineID() As String
        Dim Raw As String = ""
        Raw = GetProcessorId() & BiosSerNr()
        Dim result = CovertASCII(GetHash(Raw))
        Return result
    End Function

    Public Shared Function Keygen(ByVal mID As String, Optional ByVal IsDeveloper As Boolean = False) As String
        Dim key As String = ""
        If IsDeveloper = False AndAlso String.IsNullOrEmpty(mID) Then
            MessageBox.Show("Invalid Machine Id!")
            Return ""
        End If

        If IsDeveloper Then
            Dim raw As String = GlobalSettings.CODEBOOK & "DEVELOPER" & GlobalSettings.COMPANY
            Dim result = GetHash(raw)
            Debug.Print(result)
            Dim StartPoint As Integer = CInt(Math.Ceiling(Rnd() * (result.Count - 20)))
            key = CovertASCII(StrReverse(result.Substring(StartPoint, 20)))
        Else
            Dim raw As String = mID.Replace("-", "") & GlobalSettings.CODEBOOK & GlobalSettings.COMPANY & "RC" ' for RC version only 
            Dim result = GetHash(raw)
            Debug.Print(result)
            Dim StartPoint As Integer = CInt(Math.Ceiling(Rnd() * (result.Count - 20)))
            key = CovertASCII(StrReverse(result.Substring(StartPoint, 20)))
        End If

        Return key
    End Function

    Public Shared Function Verfication() As AUTHORIZATION_LEVEL
        Dim ALevel As AUTHORIZATION_LEVEL = AUTHORIZATION_LEVEL.NONE
        Try
            If String.IsNullOrEmpty(GlobalSettings.RegistrationKey) Then
                Return ALevel
            Else
                Dim CorrectRC As String = StrReverse(GlobalSettings.RegistrationKey.Replace("-", ""))
                'check developer
                Dim raw As String = GlobalSettings.CODEBOOK & "DEVELOPER" & GlobalSettings.COMPANY
                Dim result = GetHash(raw)
                If result.Contains(CorrectRC) Then
                    ALevel = AUTHORIZATION_LEVEL.DEVELOPER
                    Return ALevel
                End If

                'check standard 
                raw = GlobalSettings.MachineID.Replace("-", "") & GlobalSettings.CODEBOOK & GlobalSettings.COMPANY & "RC" ' for RC version only 
                result = GetHash(raw)
                If result.Contains(CorrectRC) Then
                    ALevel = AUTHORIZATION_LEVEL.STANDARD
                    Return ALevel
                End If
            End If
        Catch ex As Exception
            Return AUTHORIZATION_LEVEL.NONE
        End Try

        Return ALevel
    End Function

    Public Shared Function IsAuthorized() As Boolean
        Return (Verfication() <> AUTHORIZATION_LEVEL.NONE)
    End Function

    Public Shared Function IsDevelopement() As Boolean
        Return (Verfication() = AUTHORIZATION_LEVEL.DEVELOPER)
    End Function

    Public Shared Function IsStandard() As Boolean
        Return (Verfication() = AUTHORIZATION_LEVEL.STANDARD)
    End Function

    Public Shared Function AuthorizationName() As String
        Select Case Verfication()
            Case AUTHORIZATION_LEVEL.NONE
                Return "No Authorization"
            Case AUTHORIZATION_LEVEL.STANDARD
                Return "Standard"
            Case AUTHORIZATION_LEVEL.DEVELOPER
                Return "Development"
            Case Else
                Return "No Authorization"
        End Select
    End Function

End Class
