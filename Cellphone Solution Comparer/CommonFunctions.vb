Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient

Public Structure MapLocations
    Dim name As String
    Dim coordinates As Point
End Structure
Module CommonFunctions

    Public username As String
    Public password As String
    Public servername As String
    Public db As String
    Public Function TrapKey(ByVal KCode As String, ByVal des_format As String) As Boolean
        Select Case des_format
            Case "Numeric"
                If (KCode >= 48 And KCode <= 57) Or KCode = 8 Then
                    TrapKey = False
                Else
                    TrapKey = True
                End If
            Case Else
                Return False
        End Select
    End Function
    Public Sub UpdateData(ByVal UpdateQuery As String, ByVal conn As Common.DbConnection, ByVal DA As MySqlDataAdapter, ByVal DT As DataTable)
        Dim updateCommand As New MySqlCommand(UpdateQuery, conn)
        DA.UpdateCommand = updateCommand
        DA.Update(DT)
    End Sub

    Public MiddleEast() As String = {"Afghanistan", "Algeria", "Bahrain", "Egypt", "Iran", "Iraq", "Jordan",
        "Kuwait", "Lebanon", "Libya", "Morocco", "Oman", "Pakistan", "Syria", "Tunisia", "United Arab Emirates",
        "Yemen", "Saudi Arabia", "Qatar", "West Bank and Gaza", "Turkey"}

    Public Function LoadMiddleEast() As MapLocations()
        Dim MapColl(21) As MapLocations

        MapColl(0).coordinates.X = 849
        MapColl(0).coordinates.Y = 176
        MapColl(0).name = "Pakistan"

        MapColl(1).coordinates.X = 614
        MapColl(1).coordinates.Y = 177
        MapColl(1).name = "Bahrain"

        MapColl(2).coordinates.X = 814
        MapColl(2).coordinates.Y = 159
        MapColl(2).name = "Afghanistan"

        MapColl(3).coordinates.X = 92
        MapColl(3).coordinates.Y = 190
        MapColl(3).name = "Algeria"

        MapColl(4).coordinates.X = 421
        MapColl(4).coordinates.Y = 207
        MapColl(4).name = "Egypt"

        MapColl(5).coordinates.X = 633
        MapColl(5).coordinates.Y = 135
        MapColl(5).name = "Iran"

        MapColl(6).coordinates.X = 535
        MapColl(6).coordinates.Y = 130
        MapColl(6).name = "Iraq"

        MapColl(7).coordinates.X = 471
        MapColl(7).coordinates.Y = 152
        MapColl(7).name = "Jordan"

        MapColl(8).coordinates.X = 584
        MapColl(8).coordinates.Y = 173
        MapColl(8).name = "Kuwait"

        MapColl(9).coordinates.X = 452
        MapColl(9).coordinates.Y = 124
        MapColl(9).name = "Lebanon"

        MapColl(10).coordinates.X = 275
        MapColl(10).coordinates.Y = 210
        MapColl(10).name = "Libya"

        MapColl(11).coordinates.X = -29
        MapColl(11).coordinates.Y = 182
        MapColl(11).name = "Morocco"

        MapColl(12).coordinates.X = 699
        MapColl(12).coordinates.Y = 267
        MapColl(12).name = "Oman"

        MapColl(13).coordinates.X = 637
        MapColl(13).coordinates.Y = 217
        MapColl(13).name = "Qatar"

        MapColl(14).coordinates.X = 574
        MapColl(14).coordinates.Y = 232
        MapColl(14).name = "Saudi Arabia"

        MapColl(15).coordinates.X = 473
        MapColl(15).coordinates.Y = 114
        MapColl(15).name = "Syria"

        MapColl(16).coordinates.X = 179
        MapColl(16).coordinates.Y = 127
        MapColl(16).name = "Tunisia"

        MapColl(17).coordinates.X = 666
        MapColl(17).coordinates.Y = 227
        MapColl(17).name = "United Arab Emirates"

        MapColl(18).coordinates.X = 624
        MapColl(18).coordinates.Y = 328
        MapColl(18).name = "Yemen"

        MapColl(19).name = "West Bank and Gaza"
        MapColl(20).name = "Turkey"

        Return MapColl
    End Function

    Public Function GetAppPath() As String
        Return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6)
    End Function

    Public Function IsLoaded(FormName As String) As Boolean

        Dim sFormName As String
        Dim f As Form
        sFormName = UCase$(FormName)

        For Each f In Application.OpenForms
            If UCase$(f.Name) = sFormName Then
                IsLoaded = True
                Exit Function
            End If
        Next

        Return False
    End Function

End Module
