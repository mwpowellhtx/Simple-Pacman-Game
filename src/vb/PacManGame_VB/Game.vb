Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace PacManGame
    Class Game
        Private mBoard As Board
        Private mPacMansPosition As Coordinate
        Private mMonsterPosition As Coordinate
        Private mGameOver As Boolean
        Private mWon As Boolean

        Public Sub New(pBoard As Board)
            mBoard = pBoard
            mPacMansPosition = New Coordinate(1, 1)
            mMonsterPosition = New Coordinate(8, 8)
        End Sub

        Public Enum movement
            LEFT
            RIGHT
            UP
            DOWN
        End Enum

        Public Sub Move(pMove As movement)
            'legal move
            Dim newPosition As Coordinate
            Select Case pMove
                Case movement.LEFT
                    newPosition = New Coordinate(mPacMansPosition.x - 1, mPacMansPosition.y)
                    Exit Select
                Case movement.RIGHT
                    newPosition = New Coordinate(mPacMansPosition.x + 1, mPacMansPosition.y)
                    Exit Select
                Case movement.UP
                    newPosition = New Coordinate(mPacMansPosition.x, mPacMansPosition.y - 1)
                    Exit Select
                Case Else
                    newPosition = New Coordinate(mPacMansPosition.x, mPacMansPosition.y + 1)
                    Exit Select
            End Select
            ' dont move him
            If mBoard.GetState(newPosition) = BoardStates.WALL Then
            Else
                mPacMansPosition = newPosition
                mBoard.Eat(newPosition)
            End If

            If mPacMansPosition.toString().Equals(mMonsterPosition.toString()) Then
                mGameOver = True
                mWon = False
            End If
            MoveMonster()
            If mPacMansPosition.toString().Equals(mMonsterPosition.toString()) Then
                mGameOver = True
                mWon = False
            End If

            'win/loose
        End Sub

        Private Sub MoveMonster()
            Dim newMonsterPosition As Coordinate
            Dim values As Array = [Enum].GetValues(GetType(movement))
            Dim random As New Random()
            Dim monsterMove As movement = CType(values.GetValue(random.[Next](values.Length)), movement)
            Select Case monsterMove
                Case movement.LEFT
                    newMonsterPosition = New Coordinate(mMonsterPosition.x - 1, mMonsterPosition.y)
                    Exit Select
                Case movement.RIGHT
                    newMonsterPosition = New Coordinate(mMonsterPosition.x + 1, mMonsterPosition.y)
                    Exit Select
                Case movement.UP
                    newMonsterPosition = New Coordinate(mMonsterPosition.x, mMonsterPosition.y - 1)
                    Exit Select
                Case Else
                    newMonsterPosition = New Coordinate(mMonsterPosition.x, mMonsterPosition.y + 1)
                    Exit Select
            End Select
            If mBoard.GetState(newMonsterPosition) = BoardStates.WALL Then
                MoveMonster()
            Else
                mMonsterPosition = newMonsterPosition
            End If
        End Sub

        Public Function present() As String
            If mGameOver Then
                If mWon Then
                    Return mBoard.ToString(mPacMansPosition, mMonsterPosition) & "GAME OVER YOU WON"
                End If
                Return mBoard.ToString(mPacMansPosition, mMonsterPosition, False) & "GAME OVER YOU LOST!!!!1!"
            End If
            Return mBoard.ToString(mPacMansPosition, mMonsterPosition)
        End Function

        Friend ReadOnly Property GameOver() As Boolean
            Get
                Return mGameOver
            End Get
        End Property
    End Class
End Namespace
