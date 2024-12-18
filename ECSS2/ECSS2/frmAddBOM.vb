﻿Imports System.ComponentModel

Public Class frmAddBOM
    Public CurrentBOM As OneBOMList
    Public QTY As Integer
    Private BOMdic As New Dictionary(Of String, OneBOMList)
    Private SelectedBOM As String
    Public Sub New(ByVal Bdic As Dictionary(Of String, OneBOMList), ByVal sBOM As String)
        Me.BOMdic = Bdic
        Me.SelectedBOM = sBOM
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmAddBOM_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.UpdateBOMcombolist()
        Me.txtName.Text = "BOM" & Date.Now.ToString("yyyyMMddHHmmss") 'default name
        If Me.BOMdic Is Nothing OrElse Me.BOMdic.Count > 0 Then
            Me.radExisting.Checked = True
        Else
            Me.radNew.Checked = True
        End If
        Me.TopMost = True
    End Sub


    Private Sub UpdateBOMcombolist()
        Dim selectedIndex As Integer = 0
        Me.cboBOM.Items.Clear()
        If Me.BOMdic Is Nothing OrElse Me.BOMdic.Count > 0 Then
            For Each kvp In Me.BOMdic.Reverse
                Me.cboBOM.Items.Add(kvp.Value.BOMTitle)
                If kvp.Key = Me.SelectedBOM Then selectedIndex = Me.cboBOM.Items.Count - 1
            Next
            Me.cboBOM.SelectedIndex = selectedIndex
        End If
    End Sub

    Private Sub txtName_Validating(sender As Object, e As CancelEventArgs) Handles txtName.Validating
        Try
            Dim str As String = Me.txtName.Text.Trim
            If String.IsNullOrEmpty(str) Then
                MessageBox.Show("BOM Title cannot be empty!", "BOM Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.txtName.SelectAll()
                Me.txtName.Focus()
                Exit Sub
            End If

            If Me.BOMdic.Where(Function(k) k.Value.BOMTitle.ToUpper = str.ToUpper).Count > 0 Then
                MessageBox.Show("BOM Title already exists, please choose a different title!", "BOM Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.txtName.SelectAll()
                Me.txtName.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub txtQty_Validating(sender As Object, e As CancelEventArgs) Handles txtQty.Validating
        Try
            Dim str As String = Me.txtQty.Text.Trim
            If String.IsNullOrEmpty(str) Then
                MessageBox.Show("Qty cannot be empty!", "Qty Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.txtQty.SelectAll()
                Me.txtQty.Focus()
                Exit Sub
            End If

            If IsNumeric(str) = False Then
                MessageBox.Show("Please input a valid number!", "Qty Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.txtQty.SelectAll()
                Me.txtQty.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        Me.QTY = CInt(Me.txtQty.Text)

        If Me.radNew.Checked Then
            If String.IsNullOrEmpty(Me.txtName.Text.Trim) Then
                MessageBox.Show("BOM Title cannot be empty.")
                Me.txtName.SelectAll()
                Me.txtName.Focus()
                Exit Sub
            End If
            Me.CurrentBOM = New OneBOMList
            Me.CurrentBOM.BOMList = New List(Of ECSSBOM)
            Me.CurrentBOM.BOMTitle = Me.txtName.Text.Trim
            Me.CurrentBOM.BOMID = "BOM" & Date.Now.ToString("yyyyMMddHHmmss")
            Me.BOMdic.Add(Me.CurrentBOM.BOMID, Me.CurrentBOM)

            ECSSDBFunctions.InsertOneBOM(Me.CurrentBOM, True)
        ElseIf Me.radExisting.Checked Then
            If Me.cboBOM.SelectedIndex < Me.BOMdic.Count Then
                Dim tList = Me.BOMdic.Where(Function(b) b.Value.BOMTitle = Me.cboBOM.SelectedItem.ToString).Select(Function(b) b.Value)
                If tList.Count > 0 Then Me.CurrentBOM = tList(0)
                'Me.CurrentBOM =
                'Me.CurrentBOM.BOMTitle = Me.cboBOM.SelectedItem.ToString
                'Me.CurrentBOM.BOMID = Me.BOMdic.ElementAt(Me.cboBOM.SelectedIndex).Key
            Else
                MessageBox.Show("BOM index error, please check.")
            End If
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub radNew_CheckedChanged(sender As Object, e As EventArgs) Handles radNew.CheckedChanged
        Me.txtName.Enabled = Me.radNew.Checked
        Me.cboBOM.Enabled = Me.radExisting.Checked
    End Sub

End Class