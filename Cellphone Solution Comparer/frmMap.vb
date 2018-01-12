Imports System
Imports System.Reflection
Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient

Public Class frmMap
    Dim conn_local As Common.DbConnection
    Dim cnString As String

    Dim da_local As Common.DbDataAdapter
    Dim da_AT As Common.DbDataAdapter
    Dim da_T As Common.DbDataAdapter

    Dim sqlQRY_local As String
    Dim sqlQRY_AT As String
    Dim sqlQRY_T As String

    Dim curr_checked_index As Integer = -1
    Dim form_went_off_screen As Boolean = False
    Dim is_painted As Boolean = False

    Private Sub btnComp_Click(sender As Object, e As EventArgs) Handles btnComp.Click
        frmComparison.Show()
    End Sub

    Private Sub frmMap_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If frmComparison.IsDisposed Or IsLoaded("frmComparison") = False Then
            frmLogin.Close()
        End If
    End Sub

    Private Sub frmMap_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.Text = 50
        TextBox3.Text = 50
        cboLocalList.ContextMenuStrip = ContextMenuStrip1

        Try
            Dim table As String
            For Each table In GetTableList(db, servername)
                If table <> "atnt" And table <> "tmobile" Then
                    Dim titled_table = StrConv(table, VbStrConv.ProperCase)
                    If Not Trim(titled_table) = "" Then
                        cboLocalList.Items.Add(titled_table)
                        cboLocalList.Text = cboLocalList.Items(0)
                    End If
                End If
            Next table
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub frmMap_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        TextBox4.Text = System.Windows.Forms.Cursor.Position.X()
        TextBox5.Text = System.Windows.Forms.Cursor.Position.Y()
    End Sub

    Private Sub frmMap_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        TextBox7.Text = System.Windows.Forms.Cursor.Position.X()
        TextBox6.Text = System.Windows.Forms.Cursor.Position.Y()
    End Sub

    Private Sub frmMap_Move(sender As Object, e As EventArgs) Handles Me.Move
        If is_painted = True And curr_checked_index > -1 Then
            Dim workingRectangle As System.Drawing.Rectangle = Screen.PrimaryScreen.WorkingArea
            Dim sizeX = workingRectangle.Width
            Dim sizeY = workingRectangle.Height

            If (Me.Location.X < 0 Or (Me.Location.X + Me.Size.Width) > sizeX) Or (Me.Location.Y < 0 Or (Me.Location.Y + Me.Size.Height) > sizeY) And form_went_off_screen = False Then
                form_went_off_screen = True
            ElseIf (Me.Location.X > 0 And (Me.Location.X + Me.Size.Width) < sizeX) And (Me.Location.Y > 0 And (Me.Location.Y + Me.Size.Height) < sizeY) And form_went_off_screen = True Then
                PaintMap(curr_checked_index)
                form_went_off_screen = False
            End If
        End If
    End Sub

    Private Sub frmMap_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim Img_alpha As Image = Image.FromFile(GetAppPath() & "\Images\MiddleEast.png")
        e.Graphics.FillRectangle(Brushes.CornflowerBlue, 0, 135, 997, 349)
        e.Graphics.DrawImage(Img_alpha, 13, 150, 970, 327)
        is_painted = True
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        TextBox9.Text = Val(TextBox7.Text) - (Val(TextBox4.Text) - Val(TextBox2.Text))
        TextBox8.Text = Val(TextBox6.Text) - (Val(TextBox5.Text) - Val(TextBox3.Text))
    End Sub

    Public Function GetImageSize(Path As String) As Point
        Dim pt As Point
        Dim bmp As New Bitmap(Path)
        pt.X = bmp.Width
        pt.Y = bmp.Height
        Return pt
    End Function

    Public Function FetchCountryIndex(country_name As String, CountryList As MapLocations()) As Integer
        Return Array.FindIndex(CountryList, Function(f) f.name = country_name)
    End Function

    Public Function GetTableList(databasename As String, servername As String) As String()
        Try
            Dim strConn As String
            strConn = "datasource=" & servername & ";username=" & username & ";password=" & password & ";"

            strConn &= "Database = " & databasename & "; pooling=false;"
            Dim connTable As New MySqlConnection(strConn)

            Dim cmdTables As New MySqlCommand("SHOW TABLES", connTable)
            Dim drTables As MySqlDataReader
            connTable.Open()
            drTables = cmdTables.ExecuteReader

            Dim i As Integer = 1
            Dim TableList(i) As String

            Do While drTables.Read
                TableList(i - 1) = drTables.GetString(0)
                i = i + 1
                ReDim Preserve TableList(i)
            Loop
            drTables.Close()
            connTable.Close()

            Return TableList
        Catch
        End Try
    End Function

    Sub PaintMap(CurrCheckedIndex As Integer)
        If servername.Trim = "" Then Exit Sub

        cnString = "datasource=" & servername & ";username='" & username & "';password='" & password & "';database='" & db & "'"
        conn_local = New MySqlConnection(cnString)

        sqlQRY_local = "SELECT Country, Full_Cover, Is_Covered, Average, SMS_OUT, Average_Data, CalcCost FROM `" & LCase(cboLocalList.Text) & "` ORDER BY Country"
        sqlQRY_AT = "SELECT Country, Full_Cover, Is_Covered, CCall, SMS, DData, CalcCost FROM atnt ORDER BY Country"
        sqlQRY_T = "SELECT Country, Full_Cover, Is_Covered, CCall, SMS, DData, CalcCost FROM tmobile ORDER BY Country"

        Dim ds_local As DataSet = New DataSet
        Dim ds_AT As DataSet = New DataSet
        Dim ds_T As DataSet = New DataSet

        Try
            conn_local.Open()

            da_local = New MySqlDataAdapter(sqlQRY_local, conn_local)
            da_AT = New MySqlDataAdapter(sqlQRY_AT, conn_local)
            da_T = New MySqlDataAdapter(sqlQRY_T, conn_local)

            Dim cb_local As MySqlCommandBuilder = New MySqlCommandBuilder(da_local)
            Dim cb_AT As MySqlCommandBuilder = New MySqlCommandBuilder(da_AT)
            Dim cb_T As MySqlCommandBuilder = New MySqlCommandBuilder(da_T)

            da_local.Fill(ds_local, cboLocalList.Text)
            da_AT.Fill(ds_AT, "atnt")
            da_T.Fill(ds_T, "tmobile")


            'Calculate cost based on provided days and usage, and store in CalcCost field
            Dim LocalTable As DataTable = ds_local.Tables(cboLocalList.Text)
            Dim LocalRow() As DataRow
            Dim ATTable As DataTable = ds_AT.Tables("atnt")
            Dim ATRow() As DataRow
            Dim TTable As DataTable = ds_T.Tables("tmobile")
            Dim TRow() As DataRow

            Dim mid_east_countries As MapLocations() = LoadMiddleEast()
            Dim Country As MapLocations

            Me.Refresh()

            For Each Country In mid_east_countries
                LocalRow = LocalTable.Select("Country='" & Country.name & "' AND Is_Covered='C'")
                ATRow = ATTable.Select("Country='" & Country.name & "' AND Is_Covered='C'")
                TRow = TTable.Select("Country='" & Country.name & "' AND Is_Covered='C'")
                Dim LocalCalcCost, ATCalcCost, TCalcCost As Integer

                If (LocalRow.Count > 0) Then
                    If (LocalRow(0)("Full_Cover") <> "0") Then
                        LocalCalcCost = (5 * (10 * LocalRow(0)("Average") + 5 * LocalRow(0)("Average_Data") + 5 * LocalRow(0)("SMS_OUT")))
                    End If
                Else
                    LocalCalcCost = -1
                End If

                If (ATRow.Count > 0) Then
                    If (ATRow(0)("Full_Cover") <> "0") Then
                        ATCalcCost = (5 * (10 * ATRow(0)("CCall") + 5 * ATRow(0)("DData") + 5 * ATRow(0)("SMS")))
                    End If
                Else
                    ATCalcCost = -1
                End If

                If (TRow.Count > 0) Then
                    If (TRow(0)("Full_Cover") <> "0") Then
                        TCalcCost = (5 * (10 * TRow(0)("CCall") + 5 * TRow(0)("DData") + 5 * TRow(0)("SMS")))
                    End If
                Else
                    TCalcCost = -1
                End If


                Dim costs(3) As Integer
                costs(0) = LocalCalcCost
                costs(1) = ATCalcCost
                costs(2) = TCalcCost

                Dim i, neg_cost As Integer                          'Counts the number of solutions (AT&T, T-Mobile and Local) which have -ve CalcCost
                Dim comparable_costs As New List(Of Integer)
                neg_cost = 0
                For i = 0 To 2
                    If costs(i) = -1 Then
                        neg_cost = neg_cost + 1
                    Else
                        comparable_costs.Add(costs(i))
                    End If
                Next



                Dim generic_image_path As String = GetAppPath() & "\Images\" & Country.name
                Dim decision As String = "None"

                If File.Exists(generic_image_path & "_red.png") And costs(CurrCheckedIndex) > -1 Then
                    Dim g As Graphics = Me.CreateGraphics()
                    Dim temprow() As DataRow
                    If CurrCheckedIndex = 0 Then
                        temprow = LocalRow
                    ElseIf CurrCheckedIndex = 1 Then
                        temprow = ATRow
                    ElseIf CurrCheckedIndex = 2 Then
                        temprow = TRow
                    End If
                    Dim painter As PaintEventArgs = New System.Windows.Forms.PaintEventArgs(g, New Rectangle(0, 0, Me.Width, Me.Height))
                    Select Case neg_cost
                        Case 0
                            If costs(CurrCheckedIndex) = comparable_costs.Max Or temprow(0)("Full_Cover") = "0" Then
                                Dim img As Image = Image.FromFile(generic_image_path & "_red.png")
                                painter.Graphics.DrawImage(img, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.X, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.Y, GetImageSize(generic_image_path & "_red.png").X, GetImageSize(generic_image_path & "_red.png").Y)
                                decision = "Red"
                            ElseIf costs(CurrCheckedIndex) = comparable_costs.Min Then
                                Dim img As Image = Image.FromFile(generic_image_path & "_green.png")
                                painter.Graphics.DrawImage(img, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.X, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.Y, GetImageSize(generic_image_path & "_green.png").X, GetImageSize(generic_image_path & "_green.png").Y)
                                decision = "Green"
                            Else
                                Dim img As Image = Image.FromFile(generic_image_path & "_yellow.png")
                                painter.Graphics.DrawImage(img, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.X, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.Y, GetImageSize(generic_image_path & "_yellow.png").X, GetImageSize(generic_image_path & "_yellow.png").Y)
                                decision = "Yellow"
                            End If
                        Case 1
                            If costs(CurrCheckedIndex) = comparable_costs.Max Or temprow(0)("Full_Cover") = "0" Then
                                Dim img As Image = Image.FromFile(generic_image_path & "_red.png")
                                painter.Graphics.DrawImage(img, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.X, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.Y, GetImageSize(generic_image_path & "_red.png").X, GetImageSize(generic_image_path & "_red.png").Y)
                                decision = "Red"
                            Else
                                Dim img As Image = Image.FromFile(generic_image_path & "_green.png")
                                painter.Graphics.DrawImage(img, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.X, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.Y, GetImageSize(generic_image_path & "_green.png").X, GetImageSize(generic_image_path & "_green.png").Y)
                                decision = "Green"
                            End If
                        Case 2
                            If temprow(0)("Full_Cover") = "0" Then
                                Dim img As Image = Image.FromFile(generic_image_path & "_red.png")
                                painter.Graphics.DrawImage(img, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.X, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.Y, GetImageSize(generic_image_path & "_red.png").X, GetImageSize(generic_image_path & "_red.png").Y)
                                decision = "Red"
                            Else
                                Dim img As Image = Image.FromFile(generic_image_path & "_green.png")
                                painter.Graphics.DrawImage(img, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.X, mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.Y, GetImageSize(generic_image_path & "_green.png").X, GetImageSize(generic_image_path & "_green.png").Y)
                                decision = "Green"
                            End If
                    End Select
                End If
                'MsgBox("Currently checking:" & Country.name & vbNewLine & "Local:" & costs(0) & vbNewLine & "AT&T:" & costs(1) & vbNewLine & "T-Mobile:" & costs(2) & vbNewLine & "CurrentlySelectedSolution:" & CurrCheckedIndex & vbNewLine & "Decision:" & decision)
            Next


        Catch ex As Common.DbException
            MsgBox(ex.ToString)
        Finally
            conn_local.Close()

            da_local = Nothing
            da_AT = Nothing
            da_T = Nothing
            ds_local = Nothing
            ds_AT = Nothing
            ds_T = Nothing
        End Try
    End Sub

    Private Sub cmdLocal_Click(sender As Object, e As EventArgs) Handles cmdLocal.Click
        PaintMap(0)
        curr_checked_index = 0
        Label26.Text = "Currently comparing Local against AT&&T and T-Mobile"
    End Sub

    Private Sub btnAT_Click(sender As Object, e As EventArgs) Handles btnAT.Click
        PaintMap(1)
        curr_checked_index = 1
        Label26.Text = "Currently comparing AT&&T against Local and T-Mobile"
    End Sub

    Private Sub btnT_Click(sender As Object, e As EventArgs) Handles btnT.Click
        PaintMap(2)
        curr_checked_index = 2
        Label26.Text = "Currently comparing T-Mobile against Local and AT&&T"
    End Sub

    Private Sub cboLocalList_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboLocalList.KeyPress
        e.Handled = True
    End Sub

    Private Sub cboLocalList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLocalList.SelectedIndexChanged
        PaintMap(0)
    End Sub

    Private Sub cmdCombo3_Click(sender As Object, e As EventArgs) Handles cmdCombo3.Click
        frmLocal.Location = New System.Drawing.Point(0, 0)
        frmLocal.StartPosition = FormStartPosition.Manual
        frmLocal.Show()

        frmATT.Location = New System.Drawing.Point(0, frmT.Height)
        frmATT.StartPosition = FormStartPosition.Manual
        frmATT.Show()

        frmT.Location = New System.Drawing.Point(0, 2 * frmLocal.Height)
        frmT.StartPosition = FormStartPosition.Manual
        frmT.Show()
    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs) Handles Label25.Click
        frmLogin.Show()
        Me.Hide()
    End Sub

    Private Sub Label25_MouseEnter(sender As Object, e As EventArgs) Handles Label25.MouseEnter
        Label25.Font = New Font(Label25.Font, FontStyle.Bold + FontStyle.Underline)
        Label25.ForeColor = Color.Blue
    End Sub

    Private Sub Label25_MouseLeave(sender As Object, e As EventArgs) Handles Label25.MouseLeave
        Label25.Font = New Font(Label25.Font, FontStyle.Bold)
        Label25.ForeColor = Color.Black
    End Sub

    Private Sub btnClearMap_Click(sender As Object, e As EventArgs) Handles btnClearMap.Click
        Me.Invalidate()
        Label26.Text = "Currently comparing none"
        curr_checked_index = -1
    End Sub
End Class