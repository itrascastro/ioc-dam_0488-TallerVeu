namespace TallerVeu;

public class Reparacio
{
    public string Descripcio { get; set; } = string.Empty;
    public bool Acabada { get; set; }

    public override string ToString() => $"{Descripcio} ({(Acabada ? "acabada" : "pendent")})";
}
