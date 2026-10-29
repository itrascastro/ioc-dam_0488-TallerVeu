namespace TallerVeu;

partial class Form1
{
    private System.ComponentModel.IContainer components = null!;

    private System.Windows.Forms.Button btnEscoltar = null!;
    private System.Windows.Forms.Label lblTitolOrdres = null!;
    private System.Windows.Forms.Label lblOrdresDisponibles = null!;
    private System.Windows.Forms.Label lblTitolAvisos = null!;
    private System.Windows.Forms.Label lblEstat = null!;
    private System.Windows.Forms.Label lblUltimaOrdre = null!;
    private System.Windows.Forms.ListBox lstReparacions = null!;
    private System.Windows.Forms.Label lblTitolLlista = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();

        this.btnEscoltar = new System.Windows.Forms.Button();
        this.lblTitolOrdres = new System.Windows.Forms.Label();
        this.lblOrdresDisponibles = new System.Windows.Forms.Label();
        this.lblTitolAvisos = new System.Windows.Forms.Label();
        this.lblEstat = new System.Windows.Forms.Label();
        this.lblUltimaOrdre = new System.Windows.Forms.Label();
        this.lblTitolLlista = new System.Windows.Forms.Label();
        this.lstReparacions = new System.Windows.Forms.ListBox();

        this.SuspendLayout();
        //
        // btnEscoltar
        //
        this.btnEscoltar.Location = new System.Drawing.Point(20, 20);
        this.btnEscoltar.Name = "btnEscoltar";
        this.btnEscoltar.Size = new System.Drawing.Size(180, 32);
        this.btnEscoltar.TabIndex = 0;
        this.btnEscoltar.Text = "Comença a escoltar";
        this.btnEscoltar.UseVisualStyleBackColor = true;
        this.btnEscoltar.Click += new System.EventHandler(this.BtnEscoltar_Click);
        //
        // lblTitolOrdres
        //
        this.lblTitolOrdres.AutoSize = true;
        this.lblTitolOrdres.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.lblTitolOrdres.Location = new System.Drawing.Point(20, 65);
        this.lblTitolOrdres.Name = "lblTitolOrdres";
        this.lblTitolOrdres.Text = "Ordres disponibles:";
        //
        // lblOrdresDisponibles
        //
        // Control permanent i sempre visible: no depèn de dir "ajuda" per veure quines
        // ordres es poden fer servir.
        this.lblOrdresDisponibles.Location = new System.Drawing.Point(20, 85);
        this.lblOrdresDisponibles.Name = "lblOrdresDisponibles";
        this.lblOrdresDisponibles.Size = new System.Drawing.Size(420, 105);
        this.lblOrdresDisponibles.Text = "• nova reparació\r\n• marca acabada\r\n• llegeix pendents\r\n• atura la lectura\r\n• ajuda\r\n• surt";
        //
        // lblTitolAvisos
        //
        this.lblTitolAvisos.AutoSize = true;
        this.lblTitolAvisos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.lblTitolAvisos.Location = new System.Drawing.Point(20, 200);
        this.lblTitolAvisos.Name = "lblTitolAvisos";
        this.lblTitolAvisos.Text = "Avisos:";
        //
        // lblEstat
        //
        // Aquesta etiqueta és la "zona d'avisos" de l'aplicació: hi apareix tant l'estat
        // de l'escolta com el resultat de cada ordre processada.
        this.lblEstat.AutoSize = true;
        this.lblEstat.Location = new System.Drawing.Point(20, 220);
        this.lblEstat.Name = "lblEstat";
        this.lblEstat.Size = new System.Drawing.Size(80, 15);
        this.lblEstat.TabIndex = 1;
        this.lblEstat.Text = "Estat: aturat";
        //
        // lblUltimaOrdre
        //
        this.lblUltimaOrdre.AutoSize = true;
        this.lblUltimaOrdre.Location = new System.Drawing.Point(20, 245);
        this.lblUltimaOrdre.Name = "lblUltimaOrdre";
        this.lblUltimaOrdre.Size = new System.Drawing.Size(100, 15);
        this.lblUltimaOrdre.TabIndex = 2;
        this.lblUltimaOrdre.Text = "Última ordre: -";
        //
        // lblTitolLlista
        //
        this.lblTitolLlista.AutoSize = true;
        this.lblTitolLlista.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.lblTitolLlista.Location = new System.Drawing.Point(20, 280);
        this.lblTitolLlista.Name = "lblTitolLlista";
        this.lblTitolLlista.Text = "Reparacions:";
        //
        // lstReparacions
        //
        this.lstReparacions.FormattingEnabled = true;
        this.lstReparacions.Location = new System.Drawing.Point(20, 300);
        this.lstReparacions.Name = "lstReparacions";
        this.lstReparacions.Size = new System.Drawing.Size(420, 200);
        this.lstReparacions.TabIndex = 3;
        //
        // Form1
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(460, 525);
        this.Controls.Add(this.lstReparacions);
        this.Controls.Add(this.lblTitolLlista);
        this.Controls.Add(this.lblUltimaOrdre);
        this.Controls.Add(this.lblEstat);
        this.Controls.Add(this.lblTitolAvisos);
        this.Controls.Add(this.lblOrdresDisponibles);
        this.Controls.Add(this.lblTitolOrdres);
        this.Controls.Add(this.btnEscoltar);
        this.Name = "Form1";
        this.Text = "TallerVeu - Control per veu del taller";
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
