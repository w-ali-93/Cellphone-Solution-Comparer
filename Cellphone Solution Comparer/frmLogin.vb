Imports MySql.Data.MySqlClient

Public Class frmLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        username = txtUsername.Text
        password = txtPassword.Text
        servername = txtServer.Text
        db = txtDB.Text
        TestConnection(username, password, servername)
    End Sub

    Private Function TestConnection(u_name As String, pass As String, sv As String) As Boolean
        Try
            Dim conn_local As Common.DbConnection
            Dim cnString As String
            cnString = "datasource=" & servername & ";username='" & username & "';password='" & password & "';database='" & db & "'"
            conn_local = New MySqlConnection(cnString)
            conn_local.Open()
            conn_local.Close()
            MsgBox("Login successful!", , "Welcome")
            frmMap.Show()
            txtPassword.Clear()
            txtUsername.Clear()
            txtServer.Clear()
            Me.Hide()
        Catch ex As System.Exception
            MsgBox(ex.Message.ToString())
        End Try
        Return 1
    End Function

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub txtServer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtServer.KeyPress

    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtServer_TextChanged(sender As Object, e As EventArgs) Handles txtServer.TextChanged

    End Sub

    Private Sub txtDB_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDB.KeyPress
        If Asc(e.KeyChar) = 13 Then
            username = txtUsername.Text
            password = txtPassword.Text
            servername = txtServer.Text
            db = txtDB.Text
            TestConnection(username, password, servername)
        End If
    End Sub

    Private Sub txtDB_TextChanged(sender As Object, e As EventArgs) Handles txtDB.TextChanged

    End Sub
End Class