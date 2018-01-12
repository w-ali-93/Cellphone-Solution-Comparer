Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient

Public Class frmComparison
    Dim conn_local As Common.DbConnection
    Dim cnString As String

    Dim da_local As Common.DbDataAdapter
    Dim da_AT As Common.DbDataAdapter
    Dim da_T As Common.DbDataAdapter

    Dim sqlQRY_local As String
    Dim sqlQRY_AT As String
    Dim sqlQRY_T As String

    Private Sub frmComparison_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If frmMap.IsDisposed Or IsLoaded("frmMap") = False Then
            frmLogin.Close()
        End If
    End Sub

    Private Sub frmComparison_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = 732
        Me.Height = 615
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog

        cnString = "datasource=" & servername & ";username='" & username & "';password='" & password & "';database='" & db & "'"
        conn_local = New MySqlConnection(cnString)

        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToOrderColumns = False
        DataGridView1.AllowUserToResizeColumns = False
        DataGridView1.AllowUserToResizeRows = False

        txtVisitDays.ContextMenuStrip = ContextMenuStrip1
        txtSMS.ContextMenuStrip = ContextMenuStrip1
        txtMinutes.ContextMenuStrip = ContextMenuStrip1
        txtDataUsage.ContextMenuStrip = ContextMenuStrip1

        rdbLocal.Checked = True

    End Sub
    Private Sub txtVisitDays_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtVisitDays.KeyPress, txtSMS.KeyPress, txtMinutes.KeyPress, txtDataUsage.KeyPress
        e.Handled = TrapKey(Asc(e.KeyChar), "Numeric")
    End Sub

    Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click

        sqlQRY_local = "SELECT Country, Full_Cover, Is_Covered, Average, SMS_OUT, Average_Data, CalcCost FROM `" & LCase(frmMap.cboLocalList.Text) & "` ORDER BY Country"
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

            da_local.Fill(ds_local, frmMap.cboLocalList.Text)
            da_AT.Fill(ds_AT, "atnt")
            da_T.Fill(ds_T, "tmobile")

            'Fill the DataGrid with selected cellphone solution
            DataGridView1.DataSource = Nothing
            DataGridView1.DataMember = Nothing
            DataGridView1.Rows.Clear()

            If rdbLocal.Checked Then
                DataGridView1.DataSource = ds_local
                DataGridView1.DataMember = frmMap.cboLocalList.Text
            ElseIf rdbAT.Checked Then
                DataGridView1.DataSource = ds_AT
                DataGridView1.DataMember = "atnt"
            ElseIf rdbT.Checked Then
                DataGridView1.DataSource = ds_T
                DataGridView1.DataMember = "tmobile"
            End If

            'Calculate cost based on provided days and usage, and store in CalcCost field
            Dim LocalTable As DataTable = ds_local.Tables(frmMap.cboLocalList.Text)
            Dim LocalRow() As DataRow
            Dim ATTable As DataTable = ds_AT.Tables("atnt")
            Dim ATRow() As DataRow
            Dim TTable As DataTable = ds_T.Tables("tmobile")
            Dim TRow() As DataRow

            Dim cntry As MapLocations
            Dim mid_east_countries As MapLocations() = LoadMiddleEast()

            For Each cntry In mid_east_countries
                Dim Country As String = cntry.name
                LocalRow = LocalTable.Select("Country='" & Country & "' AND Is_Covered='C'")
                ATRow = ATTable.Select("Country='" & Country & "' AND Is_Covered='C'")
                TRow = TTable.Select("Country='" & Country & "' AND Is_Covered='C'")
                Dim LocalCalcCost, ATCalcCost, TCalcCost As Integer

                If (LocalRow.Count > 0) Then
                    If (LocalRow(0)("Full_Cover") <> "0") Then
                        LocalRow(0).BeginEdit()
                        LocalCalcCost = (Val(txtVisitDays.Text) * (Val(txtMinutes.Text) * LocalRow(0)("Average") + Val(txtDataUsage.Text) * LocalRow(0)("Average_Data") + Val(txtSMS.Text) * LocalRow(0)("SMS_OUT")))
                        LocalRow(0)("CalcCost") = LocalCalcCost
                        LocalRow(0).EndEdit()
                    End If
                Else
                    LocalCalcCost = -1
                End If

                If (ATRow.Count > 0) Then
                    If (ATRow(0)("Full_Cover") <> "0") Then
                        ATRow(0).BeginEdit()
                        ATCalcCost = Val(txtVisitDays.Text) * (Val(txtMinutes.Text) * ATRow(0)("CCall") + Val(txtDataUsage.Text) * ATRow(0)("DData") + Val(txtSMS.Text) * ATRow(0)("SMS"))
                        ATRow(0)("CalcCost") = ATCalcCost
                        ATRow(0).EndEdit()
                    End If
                Else
                    ATCalcCost = -1
                End If

                If (TRow.Count > 0) Then
                    If (TRow(0)("Full_Cover") <> "0") Then
                        TRow(0).BeginEdit()
                        TCalcCost = Val(txtVisitDays.Text) * (Val(txtMinutes.Text) * TRow(0)("CCall") + Val(txtDataUsage.Text) * TRow(0)("DData") + Val(txtSMS.Text) * TRow(0)("SMS"))
                        TRow(0)("CalcCost") = TCalcCost
                        TRow(0).EndEdit()
                    End If
                Else
                    TCalcCost = -1
                End If
                DataGridView1.Refresh()

                'Rename columns of DataGridView
                RenameColumnsOfDataGridView()

                'Locate DataGridView Row of current Country and shade it as per criteria
                PaintDataGridView(Country, LocalCalcCost, ATCalcCost, TCalcCost)

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

    Private Sub PaintDataGridView(country_name As String, local As Integer, att As Integer, tm As Integer)
        Dim row As DataGridViewRow
        Dim costs(3) As Integer


        costs(0) = local
        costs(1) = att
        costs(2) = tm

        Dim CurrCheckedIndex As Integer = -1

        If rdbLocal.Checked = True Then
            CurrCheckedIndex = 0
        ElseIf rdbAT.Checked = True Then
            CurrCheckedIndex = 1
        ElseIf rdbT.Checked = True Then
            CurrCheckedIndex = 2
        End If

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

        'MsgBox("Currently checking:" & country_name & vbNewLine & "Local:" & local & vbNewLine & "AT&T:" & att & vbNewLine & "T-Mobile:" & tm & vbNewLine & "CurrentlySelectedSolution:" & CurrCheckedIndex)

        For Each row In DataGridView1.Rows
            If Not (row.Cells("Country").Value = Nothing) And costs(CurrCheckedIndex) > -1 Then
                If row.Cells("Country").Value.ToString().Equals(country_name) Then
                    Select Case neg_cost
                        Case 0
                            If costs(CurrCheckedIndex) = comparable_costs.Max Then
                                DataGridView1.Item("CalcCost", row.Index).Style.BackColor = Color.Red
                            ElseIf costs(CurrCheckedIndex) = comparable_costs.Min Then
                                DataGridView1.Item("CalcCost", row.Index).Style.BackColor = Color.Green
                            Else
                                DataGridView1.Item("CalcCost", row.Index).Style.BackColor = Color.Yellow
                            End If
                        Case 1
                            If costs(CurrCheckedIndex) = comparable_costs.Max Then
                                DataGridView1.Item("CalcCost", row.Index).Style.BackColor = Color.Red
                            Else
                                DataGridView1.Item("CalcCost", row.Index).Style.BackColor = Color.Green
                            End If
                        Case 2
                            DataGridView1.Item("CalcCost", row.Index).Style.BackColor = Color.Green
                    End Select
                End If
                If row.Cells("Full_Cover").Value.ToString() = "0" Then
                    DataGridView1.Item("CalcCost", row.Index).Style.BackColor = Color.Red
                End If
            End If
        Next
    End Sub

    Private Sub RenameColumnsOfDataGridView()
        With DataGridView1
            .RowHeadersVisible = False
            .Columns(0).HeaderCell.Value = "Country"
            .Columns(1).HeaderCell.Value = "Complete Coverage"
            .Columns(2).HeaderCell.Value = "Basic Coverage"
            .Columns(3).HeaderCell.Value = "Call/min"
            .Columns(4).HeaderCell.Value = "Per SMS"
            .Columns(5).HeaderCell.Value = "Data/MB"
            .Columns(6).HeaderCell.Value = "Calculated Cost"
        End With
    End Sub

    Private Sub DataGridView1_ColumnAdded(sender As Object, e As DataGridViewColumnEventArgs) Handles DataGridView1.ColumnAdded
        DataGridView1.Columns(e.Column.Index).SortMode = DataGridViewColumnSortMode.NotSortable
    End Sub

End Class