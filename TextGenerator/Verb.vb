Public Module Verb

    Dim _v() As String = {"arbeitet", "baut", "denkt", "erklärt", "fragt", "fährt", "gibt", "haut", "injeziert", "jagt", "klaut", "lügt", "macht", "nietet", "orakelt", "pausiert", "quatscht", "reist", "sieht", "tafelt", "untersucht", "versucht", "wagt", "zankt"}

    Dim _Count As Integer

    Public ReadOnly Property Count As Integer
        Get
            Return _Count
        End Get
    End Property

    Sub New()
        _Count = _v.Length
    End Sub

    Public ReadOnly Property Item(ix As Integer) As String
        Get
            Return _v(ix)
        End Get
    End Property


End Module
