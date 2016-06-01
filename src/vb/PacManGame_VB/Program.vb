
Imports System.Threading
Imports PacManGame_VB.PacManGame

Module Program
        Sub Main()
            Dim board As New Board()
            Dim game As New Game(board)
            Console.WriteLine(game.present())
            While Not game.GameOver
                Dim line As String = Console.ReadKey().KeyChar.ToString()
                If line.ToLower().Equals("w") Then
                    game.Move(game.movement.UP)
                ElseIf line.ToLower().Equals("a") Then
                    game.Move(game.movement.LEFT)
                ElseIf line.ToLower().Equals("s") Then
                    game.Move(game.movement.DOWN)
                ElseIf line.ToLower().Equals("d") Then
                    game.Move(game.movement.RIGHT)
                End If
                Console.WriteLine()
                Console.WriteLine(game.present())
                Thread.Sleep(4000)
            End While
        End Sub
End Module
