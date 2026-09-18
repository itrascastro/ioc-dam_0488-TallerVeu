namespace TallerVeu;

/// <summary>
/// Conté tota la lògica de negoci del taller, completament independent del
/// reconeixement de veu: es pot testejar cridant <see cref="ProcessaOrdre"/>
/// amb text, sense necessitat de cap micròfon.
/// </summary>
public class GestorReparacions
{
    private readonly List<Reparacio> reparacions = new();

    public IReadOnlyList<Reparacio> Reparacions => reparacions;

    public bool SortidaSollicitada { get; private set; }

    public string ProcessaOrdre(string ordre)
    {
        switch (ordre.Trim().ToLowerInvariant())
        {
            case "nova reparació":
            case "nova reparacio":
                return NovaReparacio();
            case "marca acabada":
                return MarcaAcabada();
            case "llegeix pendents":
                return TextPendents();
            case "ajuda":
                return TextAjuda();
            case "surt":
                SortidaSollicitada = true;
                return "Vols sortir de l'aplicació?";
            default:
                return "Ordre no reconeguda.";
        }
    }

    private string NovaReparacio()
    {
        int numero = reparacions.Count + 1;
        reparacions.Add(new Reparacio { Descripcio = $"Reparació {numero}" });
        return $"S'ha creat la reparació {numero}.";
    }

    private string MarcaAcabada()
    {
        var pendent = reparacions.FirstOrDefault(r => !r.Acabada);
        if (pendent is null) return "No hi ha cap reparació pendent.";

        pendent.Acabada = true;
        return $"{pendent.Descripcio} marcada com a acabada.";
    }

    public string TextPendents()
    {
        var pendents = reparacions.Where(r => !r.Acabada).ToList();
        if (pendents.Count == 0) return "No hi ha cap reparació pendent.";

        return "Reparacions pendents: " + string.Join(", ", pendents.Select(r => r.Descripcio)) + ".";
    }

    private static string TextAjuda() =>
        "Ordres disponibles: nova reparació, marca acabada, llegeix pendents, ajuda, surt.";
}
