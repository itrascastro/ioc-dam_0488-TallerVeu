using TallerVeu;

namespace TallerVeu.Tests;

public class GestorReparacionsTests
{
    [Fact]
    public void NovaReparacio_AfegeixUnaReparacioPendent()
    {
        var gestor = new GestorReparacions();
        gestor.ProcessaOrdre("nova reparació");

        Assert.Single(gestor.Reparacions);
        Assert.False(gestor.Reparacions[0].Acabada);
    }

    [Fact]
    public void MarcaAcabada_SenseCapReparacio_NoFallaINoDispara()
    {
        var gestor = new GestorReparacions();
        var missatge = gestor.ProcessaOrdre("marca acabada");

        Assert.Equal("No hi ha cap reparació pendent.", missatge);
    }

    [Fact]
    public void MarcaAcabada_MarcaLaPrimeraPendent()
    {
        var gestor = new GestorReparacions();
        gestor.ProcessaOrdre("nova reparació");
        gestor.ProcessaOrdre("nova reparació");

        gestor.ProcessaOrdre("marca acabada");

        Assert.True(gestor.Reparacions[0].Acabada);
        Assert.False(gestor.Reparacions[1].Acabada);
    }

    [Fact]
    public void LlegeixPendents_NomesListaLesNoAcabades()
    {
        var gestor = new GestorReparacions();
        gestor.ProcessaOrdre("nova reparació");
        gestor.ProcessaOrdre("nova reparació");
        gestor.ProcessaOrdre("marca acabada");

        var text = gestor.TextPendents();

        Assert.Contains("Reparació 2", text);
        Assert.DoesNotContain("Reparació 1", text);
    }

    [Fact]
    public void Surt_MarcaSortidaSollicitada()
    {
        var gestor = new GestorReparacions();
        gestor.ProcessaOrdre("surt");

        Assert.True(gestor.SortidaSollicitada);
    }

    [Fact]
    public void OrdreDesconeguda_RetornaMissatgeGenéric()
    {
        var gestor = new GestorReparacions();
        var missatge = gestor.ProcessaOrdre("balla la conga");

        Assert.Equal("Ordre no reconeguda.", missatge);
    }
}
