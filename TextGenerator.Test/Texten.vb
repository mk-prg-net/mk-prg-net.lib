Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class Texten

    <TestMethod()> Public Sub Texten_Aktivsaetze()

        Dim generator As New TextGenerator.Aktivsatzmaschine
        Dim satz = generator.Naechster_Satz()
        Dim satz2 = generator.Naechster_Satz()
        Dim satz3 = generator.Naechster_Satz()

        Assert.IsNotNull(satz)
        Assert.IsTrue(satz <> satz2)
        Assert.IsTrue(satz2 <> satz3)
        Assert.IsTrue(satz <> satz3)


    End Sub

End Class