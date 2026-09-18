using System.Globalization;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace TallerVeu;

public partial class Form1 : Form
{
    private readonly GestorReparacions gestor = new();
    private SpeechRecognitionEngine? reconeixedor;
    private readonly SpeechSynthesizer sintetitzador = new();
    private bool escoltant;

    public Form1()
    {
        InitializeComponent();
        SeleccionaMillorVeuDisponible();
        ActualitzaLlista();
    }

    /// <summary>
    /// Windows no sol portar cap veu en català instal·lada per defecte (SAPI). En lloc de
    /// deixar que .NET triï una veu qualsevol en silenci (que sol acabar sent una veu en
    /// anglès), es busca explícitament una veu en català i, si no n'hi ha cap, es tria
    /// castellà com a alternativa més propera i s'informa a l'estat de l'aplicació.
    /// </summary>
    private void SeleccionaMillorVeuDisponible()
    {
        var veusCatala = sintetitzador.GetInstalledVoices(new CultureInfo("ca-ES"));
        if (veusCatala.Count > 0)
        {
            sintetitzador.SelectVoice(veusCatala[0].VoiceInfo.Name);
            return;
        }

        var veusCastella = sintetitzador.GetInstalledVoices(new CultureInfo("es-ES"));
        if (veusCastella.Count > 0)
        {
            sintetitzador.SelectVoice(veusCastella[0].VoiceInfo.Name);
            lblEstat.Text = "Estat: sense veu en català instal·lada; s'utilitza castellà (" + sintetitzador.Voice.Name + ")";
            return;
        }

        lblEstat.Text = "Estat: sense veu en català ni castellà; s'utilitza " + sintetitzador.Voice.Name;
    }

    private void BtnEscoltar_Click(object? sender, EventArgs e)
    {
        if (escoltant)
        {
            AturaEscolta();
            return;
        }

        try
        {
            reconeixedor = new SpeechRecognitionEngine();

            var ordres = new Choices();
            ordres.Add(new string[]
            {
                "nova reparació", "marca acabada", "llegeix pendents",
                "atura la lectura", "ajuda", "surt"
            });
            reconeixedor.LoadGrammar(new Grammar(new GrammarBuilder(ordres)));
            reconeixedor.SpeechRecognized += Reconeixedor_SpeechRecognized;

            reconeixedor.SetInputToDefaultAudioDevice();
            reconeixedor.RecognizeAsync(RecognizeMode.Multiple);

            escoltant = true;
            lblEstat.Text = "Estat: escoltant...";
            btnEscoltar.Text = "Atura l'escolta";
        }
        catch (Exception ex)
        {
            lblEstat.Text = "Estat: no s'ha pogut activar el micròfon (" + ex.GetType().Name + ")";
        }
    }

    private void AturaEscolta()
    {
        reconeixedor?.RecognizeAsyncStop();
        reconeixedor?.Dispose();
        reconeixedor = null;

        escoltant = false;
        lblEstat.Text = "Estat: aturat";
        btnEscoltar.Text = "Comença a escoltar";
    }

    private void Reconeixedor_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
    {
        string ordre = e.Result.Text;

        // La interacció amb la UI ha de fer-se al fil de la interfície.
        if (InvokeRequired)
        {
            Invoke(new Action(() => ProcessaOrdreReconeguda(ordre)));
        }
        else
        {
            ProcessaOrdreReconeguda(ordre);
        }
    }

    private void ProcessaOrdreReconeguda(string ordre)
    {
        lblUltimaOrdre.Text = $"Última ordre: {ordre}";

        if (string.Equals(ordre, "atura la lectura", StringComparison.OrdinalIgnoreCase))
        {
            sintetitzador.SpeakAsyncCancelAll();
            lblEstat.Text = "Estat: lectura aturada";
            return;
        }

        if (string.Equals(ordre, "surt", StringComparison.OrdinalIgnoreCase))
        {
            var resposta = MessageBox.Show("Vols sortir de l'aplicació?", "Confirma",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resposta == DialogResult.Yes) Application.Exit();
            return;
        }

        string missatge = gestor.ProcessaOrdre(ordre);
        ActualitzaLlista();

        if (string.Equals(ordre, "llegeix pendents", StringComparison.OrdinalIgnoreCase))
        {
            sintetitzador.SpeakAsync(missatge);
        }

        lblEstat.Text = "Estat: " + missatge;
    }

    private void ActualitzaLlista()
    {
        lstReparacions.Items.Clear();
        foreach (var r in gestor.Reparacions)
        {
            lstReparacions.Items.Add(r.ToString());
        }
    }
}
