Imports System
Imports System.Text
Imports System.Drawing
Imports System.Windows.Forms

Public Class GrowLabel
    Inherits Label

    Private mGrowing As Boolean

    Public Sub New()
        Me.AutoSize = False
    End Sub

    Private Sub resizeLabel()
        If mGrowing Then Return

        Try
            mGrowing = True
            Dim sz As Size = New Size(Me.Width, Int32.MaxValue)
            sz = TextRenderer.MeasureText(Me.Text, Me.Font, sz, TextFormatFlags.WordBreak)
            Me.Height = sz.Height
        Finally
            mGrowing = False
        End Try
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        resizeLabel()
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
        MyBase.OnFontChanged(e)
        resizeLabel()
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        resizeLabel()
    End Sub
End Class

