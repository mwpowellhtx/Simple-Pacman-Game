Imports System.Collections.Generic
Imports System.Text

Namespace PacManGame
    Class Board

        Private map As Dictionary(Of String, BoardStates)

        Public Sub New()
            map = New Dictionary(Of String, BoardStates)()
            convertToMap("##########")
            convertToMap("# *******#")
            convertToMap("#*##**##*#")
            convertToMap("#********#")
            convertToMap("#*#*##*#*#")
            convertToMap("#*#*##*#*#")
            convertToMap("#********#")
            convertToMap("#*##**##*#")
            convertToMap("#********#")
            convertToMap("##########")
        End Sub

        Private lineCount As Integer = 0
        Private Sub convertToMap(pMapString As String)
            For i As Integer = 0 To pMapString.Length - 1
                Dim coordinate As New Coordinate(i, lineCount)
                Dim state As BoardStates
                If pMapString(i).Equals("#"c) Then
                    state = BoardStates.WALL
                ElseIf pMapString(i).Equals("*"c) Then
                    state = BoardStates.PILL
                Else
                    state = BoardStates.EMPTY
                End If
                map.Add(coordinate.toString(), state)
            Next

            lineCount += 1
        End Sub

        Public Overloads Function ToString(pPacMan As Coordinate, pMonster As Coordinate, Optional pPacLives As Boolean = True) As String
			'P = Pac-Man
			'M = Monster
			'# = Wall
			'* = Pill
            Dim board As String() = New String(9) {}
            For i As Integer = 0 To 9
                board(i) = ""
            Next
            For Each boardStatese As KeyValuePair(Of String, BoardStates) In map
                Dim character As String = ""
                If Not pPacLives Then
                    character = "X"
                ElseIf boardStatese.Key.Equals(pPacMan.toString()) Then
                    character = "P"
                ElseIf boardStatese.Key.Equals(pMonster.toString()) Then
                    character = "M"
                ElseIf boardStatese.Value = BoardStates.WALL Then
                    character = "#"
                ElseIf boardStatese.Value = BoardStates.PILL Then
                    character = "*"
                Else
                    character = " "
                End If


                board(Convert.ToInt32(boardStatese.Key.Substring(2, 2))) = board(Convert.ToInt32(boardStatese.Key.Substring(2, 2))).Insert(Convert.ToInt32(boardStatese.Key.Substring(0, 2)), character)
            Next
            Dim test As New StringBuilder()
            For Each s As String In board
                test.AppendLine(s)
            Next
            Return test.ToString()
        End Function

        Public Function GetState(pNewPosition As Coordinate) As BoardStates
            Return map(pNewPosition.toString())
        End Function

        Public Sub Eat(pNewPosition As Coordinate)
            map(pNewPosition.toString()) = BoardStates.EMPTY
        End Sub
    End Class

    Public Enum BoardStates
        WALL
        PILL
        EMPTY
    End Enum


    Public Class Coordinate
        Public x As Integer
        Public y As Integer

        Public Sub New(pX As Integer, pY As Integer)
            x = pX
            y = pY
        End Sub

        Public Shadows Function toString() As String
            Return String.Format("{0}{1}", x.ToString("D2"), y.ToString("D2"))
        End Function

    End Class
End Namespace
