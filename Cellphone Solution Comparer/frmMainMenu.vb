Public Class frmMainMenu
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnShowMap_Click(sender As Object, e As EventArgs) Handles btnShowMap.Click
        frmMap.Show()
    End Sub

    Private Sub frmMainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim s As String = "1.55,1.45,1.75"
        Dim r() As String = Split(s, ",")
    End Sub
End Class
