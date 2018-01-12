Imports System
Imports System.Reflection
Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Public Class frmATT
    Dim conn_local As Common.DbConnection
    Dim cnString As String

    Dim da_local As Common.DbDataAdapter
    Dim da_AT As Common.DbDataAdapter
    Dim da_T As Common.DbDataAdapter

    Dim sqlQRY_local As String
    Dim sqlQRY_AT As String
    Dim sqlQRY_T As String

    Dim offsetX As Integer = 13
    Dim offsetY As Integer = 150

    Dim form_went_off_screen As Boolean = False
    Dim is_painted As Boolean = False

    Sub PaintMap(CurrCheckedIndex As Integer)
        If servername.Trim = "" Then Exit Sub
        cnString = "datasource=" & servername & ";username='" & username & "';password='" & password & "';database='" & db & "'"
        conn_local = New MySqlConnection(cnString)

        sqlQRY_local = "SELECT Country, Is_Covered, Average, SMS_OUT, Average_Data, CalcCost, Full_Cover FROM `" & LCase(frmMap.cboLocalList.Text) & "` ORDER BY Country"
        sqlQRY_AT = "SELECT Country, Is_Covered, CCall, SMS, DData, CalcCost, Full_Cover FROM atnt ORDER BY Country"
        sqlQRY_T = "SELECT Country, Is_Covered, CCall, SMS, DData, CalcCost, Full_Cover FROM tmobile ORDER BY Country"

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

            da_local.Fill(ds_local, frmMap.cboLocalList.Text)
            da_AT.Fill(ds_AT, "atnt")
            da_T.Fill(ds_T, "tmobile")


            'Calculate cost based on provided days and usage, and store in CalcCost field
            Dim LocalTable As DataTable = ds_local.Tables(frmMap.cboLocalList.Text)
            Dim LocalRow() As DataRow
            Dim ATTable As DataTable = ds_AT.Tables("atnt")
            Dim ATRow() As DataRow
            Dim TTable As DataTable = ds_T.Tables("tmobile")
            Dim TRow() As DataRow

            Dim mid_east_countries As MapLocations() = LoadMiddleEast()
            Dim Country As MapLocations

            'Me.Refresh()

            Dim g As Graphics = Me.CreateGraphics()
            Dim painter As PaintEventArgs = New System.Windows.Forms.PaintEventArgs(g, New Rectangle(0, 0, Me.Width, Me.Height))

            'Paint the base map
            Dim Img_alpha As Image = Image.FromFile(GetAppPath() & "\Images\MiddleEast.png")
            painter.Graphics.FillRectangle(Brushes.Aquamarine, 0, 0, 997, 349)
            painter.Graphics.DrawImage(Img_alpha, 0, 0, 970, 327)

            For Each Country In mid_east_countries
                LocalRow = LocalTable.Select("Country='" & Country.name & "' AND Is_Covered='C'")
                ATRow = ATTable.Select("Country='" & Country.name & "' AND Is_Covered='C'")
                TRow = TTable.Select("Country='" & Country.name & "' AND Is_Covered='C'")
                Dim LocalCalcCost, ATCalcCost, TCalcCost As Integer

                If (LocalRow.Count > 0) Then
                    LocalCalcCost = (5 * (10 * LocalRow(0)("Average") + 5 * LocalRow(0)("Average_Data") + 5 * LocalRow(0)("SMS_OUT")))
                Else
                    LocalCalcCost = -1
                End If

                If (ATRow.Count > 0) Then
                    ATCalcCost = (5 * (10 * ATRow(0)("Ccall") + 5 * ATRow(0)("DData") + 5 * ATRow(0)("SMS")))
                Else
                    ATCalcCost = -1
                End If

                If (TRow.Count > 0) Then
                    TCalcCost = (5 * (10 * TRow(0)("Ccall") + 5 * TRow(0)("DData") + 5 * TRow(0)("SMS")))
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
                    Dim temprow() As DataRow
                    If CurrCheckedIndex = 0 Then
                        temprow = LocalRow
                    ElseIf CurrCheckedIndex = 1 Then
                        temprow = ATRow
                    ElseIf CurrCheckedIndex = 2 Then
                        temprow = TRow
                    End If

                    Dim x_loc As Integer = mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.X - offsetX
                    Dim y_loc As Integer = mid_east_countries(FetchCountryIndex(Country.name, mid_east_countries)).coordinates.Y - offsetY

                    'Paint the colored countries
                    Select Case neg_cost
                        Case 0
                            If costs(CurrCheckedIndex) = comparable_costs.Max Or temprow(0)("Full_Cover") = "0" Then
                                Dim img As Image = Image.FromFile(generic_image_path & "_red.png")
                                painter.Graphics.DrawImage(img, x_loc, y_loc, frmMap.GetImageSize(generic_image_path & "_red.png").X, frmMap.GetImageSize(generic_image_path & "_red.png").Y)
                                decision = "Red"
                            ElseIf costs(CurrCheckedIndex) = comparable_costs.Min Then
                                Dim img As Image = Image.FromFile(generic_image_path & "_green.png")
                                painter.Graphics.DrawImage(img, x_loc, y_loc, frmMap.GetImageSize(generic_image_path & "_green.png").X, frmMap.GetImageSize(generic_image_path & "_green.png").Y)
                                decision = "Green"
                            Else
                                Dim img As Image = Image.FromFile(generic_image_path & "_yellow.png")
                                painter.Graphics.DrawImage(img, x_loc, y_loc, frmMap.GetImageSize(generic_image_path & "_yellow.png").X, frmMap.GetImageSize(generic_image_path & "_yellow.png").Y)
                                decision = "Yellow"
                            End If
                        Case 1
                            If costs(CurrCheckedIndex) = comparable_costs.Max Or temprow(0)("Full_Cover") = "0" Then
                                Dim img As Image = Image.FromFile(generic_image_path & "_red.png")
                                painter.Graphics.DrawImage(img, x_loc, y_loc, frmMap.GetImageSize(generic_image_path & "_red.png").X, frmMap.GetImageSize(generic_image_path & "_red.png").Y)
                                decision = "Red"
                            Else
                                Dim img As Image = Image.FromFile(generic_image_path & "_green.png")
                                painter.Graphics.DrawImage(img, x_loc, y_loc, frmMap.GetImageSize(generic_image_path & "_green.png").X, frmMap.GetImageSize(generic_image_path & "_green.png").Y)
                                decision = "Green"
                            End If
                        Case 2
                            If temprow(0)("Full_Cover") = "0" Then
                                Dim img As Image = Image.FromFile(generic_image_path & "_red.png")
                                painter.Graphics.DrawImage(img, x_loc, y_loc, frmMap.GetImageSize(generic_image_path & "_red.png").X, frmMap.GetImageSize(generic_image_path & "_red.png").Y)
                                decision = "Red"
                            Else
                                Dim img As Image = Image.FromFile(generic_image_path & "_green.png")
                                painter.Graphics.DrawImage(img, x_loc, y_loc, frmMap.GetImageSize(generic_image_path & "_green.png").X, frmMap.GetImageSize(generic_image_path & "_green.png").Y)
                                decision = "Green"
                            End If
                    End Select
                End If
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

    Public Function FetchCountryIndex(country_name As String, CountryList As MapLocations()) As Integer
        Return Array.FindIndex(CountryList, Function(f) f.name = country_name)
    End Function

    Private Sub frmATT_Move(sender As Object, e As EventArgs) Handles Me.Move
        If is_painted = True Then
            Dim workingRectangle As System.Drawing.Rectangle = Screen.PrimaryScreen.WorkingArea
            Dim sizeX = workingRectangle.Width
            Dim sizeY = workingRectangle.Height

            If (Me.Location.X < 0 Or (Me.Location.X + Me.Size.Width) > sizeX) Or (Me.Location.Y < 0 Or (Me.Location.Y + Me.Size.Height) > sizeY) And form_went_off_screen = False Then
                form_went_off_screen = True
            ElseIf (Me.Location.X > 0 And (Me.Location.X + Me.Size.Width) < sizeX) And (Me.Location.Y > 0 And (Me.Location.Y + Me.Size.Height) < sizeY) And form_went_off_screen = True Then
                PaintMap(1)
                form_went_off_screen = False
            End If
        End If
    End Sub

    Private Sub frmATT_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        is_painted = True
    End Sub

    Private Sub tmr_Tick(sender As Object, e As EventArgs) Handles tmr.Tick
        If is_painted = True Then
            PaintMap(1)
            tmr.Enabled = False
        End If
    End Sub

End Class