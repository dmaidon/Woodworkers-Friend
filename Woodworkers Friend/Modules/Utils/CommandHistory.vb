' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation - Undo/Redo framework with ICommand pattern,
'          command history management, and example TextBoxUpdateCommand
' ============================================================================

''' <summary>
''' Interface for commands that can be executed and undone
''' </summary>
Public Interface ICommand

    ''' <summary>
    ''' Executes the command
    ''' </summary>
    Sub Execute()

    ''' <summary>
    ''' Undoes the command
    ''' </summary>
    Sub Undo()

    ''' <summary>
    ''' Gets a description of the command for display
    ''' </summary>
    ReadOnly Property Description As String

End Interface

''' <summary>
''' Manages command history for undo/redo functionality
''' </summary>
Public Class CommandHistory

    Private ReadOnly _undoStack As New Stack(Of ICommand)
    Private ReadOnly _redoStack As New Stack(Of ICommand)
    Private ReadOnly _maxHistorySize As Integer

    ''' <summary>
    ''' Creates a new command history
    ''' </summary>
    ''' <param name="maxHistorySize">Maximum number of commands to keep in history</param>
    Public Sub New(Optional maxHistorySize As Integer = 50)
        _maxHistorySize = maxHistorySize
    End Sub

    ''' <summary>
    ''' Executes a command and adds it to the undo stack
    ''' </summary>
    Public Sub Execute(command As ICommand)
        Try
            command.Execute()

            ' Add to undo stack
            _undoStack.Push(command)

            ' Limit stack size
            If _undoStack.Count > _maxHistorySize Then
                Dim tempStack As New Stack(Of ICommand)
                For i As Integer = 1 To _maxHistorySize
                    tempStack.Push(_undoStack.Pop())
                Next
                _undoStack.Clear()
                While tempStack.Count > 0
                    _undoStack.Push(tempStack.Pop())
                End While
            End If

            ' Clear redo stack
            _redoStack.Clear()
        Catch ex As Exception
            ErrorHandler.HandleError(ex, $"CommandHistory.Execute - {command.Description}", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Undoes the last command
    ''' </summary>
    Public Sub Undo()
        If CanUndo Then
            Try
                Dim command = _undoStack.Pop()
                command.Undo()
                _redoStack.Push(command)
            Catch ex As Exception
                ErrorHandler.HandleError(ex, "CommandHistory.Undo", showToUser:=True)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Redoes the last undone command
    ''' </summary>
    Public Sub Redo()
        If CanRedo Then
            Try
                Dim command = _redoStack.Pop()
                command.Execute()
                _undoStack.Push(command)
            Catch ex As Exception
                ErrorHandler.HandleError(ex, "CommandHistory.Redo", showToUser:=True)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Gets whether undo is available
    ''' </summary>
    Public ReadOnly Property CanUndo As Boolean
        Get
            Return _undoStack.Count > 0
        End Get
    End Property

    ''' <summary>
    ''' Gets whether redo is available
    ''' </summary>
    Public ReadOnly Property CanRedo As Boolean
        Get
            Return _redoStack.Count > 0
        End Get
    End Property

    ''' <summary>
    ''' Gets the description of the next undo command
    ''' </summary>
    Public ReadOnly Property NextUndoDescription As String
        Get
            If CanUndo Then
                Return _undoStack.Peek().Description
            End If
            Return String.Empty
        End Get
    End Property

    ''' <summary>
    ''' Gets the description of the next redo command
    ''' </summary>
    Public ReadOnly Property NextRedoDescription As String
        Get
            If CanRedo Then
                Return _redoStack.Peek().Description
            End If
            Return String.Empty
        End Get
    End Property

    ''' <summary>
    ''' Clears all command history
    ''' </summary>
    Public Sub Clear()
        _undoStack.Clear()
        _redoStack.Clear()
    End Sub

    ''' <summary>
    ''' Gets the number of commands in undo history
    ''' </summary>
    Public ReadOnly Property UndoCount As Integer
        Get
            Return _undoStack.Count
        End Get
    End Property

    ''' <summary>
    ''' Gets the number of commands in redo history
    ''' </summary>
    Public ReadOnly Property RedoCount As Integer
        Get
            Return _redoStack.Count
        End Get
    End Property

End Class

''' <summary>
''' Example command for updating a textbox value
''' </summary>
Public Class TextBoxUpdateCommand
    Implements ICommand

    Private ReadOnly _textBox As TextBox
    Private ReadOnly _oldValue As String
    Private ReadOnly _newValue As String

    Public Sub New(textBox As TextBox, newValue As String)
        _textBox = textBox
        _oldValue = textBox.Text
        _newValue = newValue
    End Sub

    Public Sub Execute() Implements ICommand.Execute
        _textBox.Text = _newValue
    End Sub

    Public Sub Undo() Implements ICommand.Undo
        _textBox.Text = _oldValue
    End Sub

    Public ReadOnly Property Description As String Implements ICommand.Description
        Get
            Return $"Update {_textBox.Name}"
        End Get
    End Property

End Class
