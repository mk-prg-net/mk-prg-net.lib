Public Module Person

    Dim _Personen() As String = {"Alexander", "Karl der Große", "Siegfried", "Brünhilde", "Hagen", "Iwan der Schreckliche", "Helmut", "Dshingis Kahn", "Buffalo Bill", _
                                 "Neil Amstrong", "Juri Gagarin", "Alf", "E.T.", "Albert Einstein", "Newton", "Kepler", "James Watt", "Archimedes"}


    Dim _Count As Integer

    Public ReadOnly Property Count As Integer
        Get
            Return _Count
        End Get
    End Property

    Sub New()
        _Count = _Personen.Length
    End Sub

    Public ReadOnly Property Item(ix As Integer) As String
        Get
            Return _Personen(ix)
        End Get
    End Property


End Module


