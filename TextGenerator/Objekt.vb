Public Module Objekt

    Dim _obj() As String = {"Auto", "Brot", "Computer", "Dieselmotor", "Elefant", "Freibier", "Geld", "Haus", "Idee", "Jade", "Kunst", "Lampe", "Moor", "Note", "Oldtimer", "Parkplatz", "Qualität", "RAM", "Sahnetorte", "Talisman", "Unterwäsche", "Vogel", "Wald", "Xylophon", "Yps", "Zentrale"}

    Dim _Count As Integer

    Public ReadOnly Property Count As Integer
        Get
            Return _Count
        End Get
    End Property

    Sub New()
        _Count = _obj.Length
    End Sub

    Public ReadOnly Property Item(ix As Integer) As String
        Get
            Return _obj(ix)
        End Get
    End Property


End Module
