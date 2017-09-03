Public Class Aktivsatzmaschine

    Dim rndSubjekt As New Random(CInt(Now.Ticks))
    Dim rndVerb As New Random(CInt(Now.Ticks))
    Dim rndPraepo As New Random(CInt(Now.Ticks))
    Dim rndObjekt As New Random(CInt(Now.Ticks))

    ''' <summary>
    ''' Würfelt einen neuen Aktivsatz aus.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Naechster_Satz() As String
        Return Person.Item(rndSubjekt.Next(Person.Count - 1)) & " " _
            & Verb.Item(rndVerb.Next(Verb.Count - 1)) & " " _
            & Praeposition.Item(rndPraepo.Next(Praeposition.Count - 1)) & " " _
            & Objekt.Item(rndObjekt.Next(rndObjekt.Next(Objekt.Count - 1))) & "."
    End Function





End Class
