Public Module Praeposition

    Dim _pp() As String = {"auf", "am", "an", "im", "um", "über", "unter", "neben"}

    Dim _Count As Integer

    Public ReadOnly Property Count As Integer
        Get
            Return _Count
        End Get
    End Property

    Sub New()
        _Count = _pp.Length
    End Sub

    Public ReadOnly Property Item(ix As Integer) As String
        Get
            Return _pp(ix)
        End Get
    End Property

End Module


