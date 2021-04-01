Public Class DoubleTrackBarWithLabels
    Private Sub DoubleTrackBarWithLabels_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.lblMax.Text = Me.dtb.SelectedMax
            Me.lblMin.Text = Me.dtb.SelectedMin
        Catch ex As Exception
            MessageBox.Show(System.Reflection.MethodInfo.GetCurrentMethod.Name & vbCrLf & ex.ToString)
        End Try
    End Sub

    Public Property Min As Integer
        Get
            Return Me.dtb.Min
        End Get
        Set(value As Integer)
            Me.dtb.Min = value
        End Set
    End Property

    Public Property Max As Integer
        Get
            Return Me.dtb.Max
        End Get
        Set(value As Integer)
            Me.dtb.Max = value
        End Set
    End Property

    Public Property SelectedMin As Integer
        Get
            Return Me.dtb.SelectedMin
        End Get
        Set(value As Integer)
            Me.dtb.SelectedMin = value
        End Set
    End Property

    Public Property SelectedMax As Integer
        Get
            Return Me.dtb.SelectedMax
        End Get
        Set(value As Integer)
            Me.dtb.SelectedMax = value
        End Set
    End Property

    Public Property TrackBarColor As Color
        Get
            Return Me.dtb.TrackBarColor
        End Get
        Set(value As Color)
            Me.dtb.TrackBarColor = value
        End Set
    End Property

    Private Sub dtb_SelectionChanged(sender As Object, e As EventArgs) Handles dtb.SelectionChanged
        Me.lblMin.Text = Me.dtb.SelectedMin
        Me.lblMax.Text = Me.dtb.SelectedMax
        RaiseEvent TrackBarSelectionChanged(Me, e)
    End Sub

    Public Sub Reset()
        Me.dtb.SelectedMin = Me.dtb.Min
        Me.dtb.SelectedMax = Me.dtb.Max
        Me.dtb.Validate()
    End Sub

    Public Event TrackBarMouseUp(sender As Object, e As MouseEventArgs)
    Public Event TrackBarSelectionChanged(sender As Object, e As EventArgs)
    Private Sub dtb_MouseUp(sender As Object, e As MouseEventArgs) Handles dtb.MouseUp
        RaiseEvent TrackBarMouseUp(Me, e)
    End Sub
End Class
