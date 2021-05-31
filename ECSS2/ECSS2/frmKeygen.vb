Public Class frmKeygen
    Private MachineID As String

    Private Sub btnDev_Click(sender As Object, e As EventArgs) Handles btnDev.Click
        Me.txtDev.Text = Authorization.Keygen("", True)
    End Sub

    Private Sub btnStandard_Click(sender As Object, e As EventArgs) Handles btnStandard.Click
        Me.MachineID = Me.txtMachineID.Text.Trim
        If String.IsNullOrEmpty(Me.MachineID) Then
            MessageBox.Show("Please input Machine ID first!")
            Exit Sub
        Else
            Me.txtKey.Text = Authorization.Keygen(Me.MachineID)
        End If
    End Sub
End Class