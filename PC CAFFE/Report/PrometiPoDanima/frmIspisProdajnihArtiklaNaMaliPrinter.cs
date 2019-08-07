﻿using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.PrometiPoDanima
{
    public partial class frmIspisProdajnihArtiklaPoDanima : Form
    {
        public frmIspisProdajnihArtiklaPoDanima()
        {
            InitializeComponent();
        }

        private void frmIspisProdajnihArtiklaNaMaliPrinter_Load(object sender, EventArgs e)
        {
            dtpOD.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            dtpDO.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            SetCB();
            //this.Paint += new PaintEventHandler(Form1_Paint);
        }

        //void Form1_Paint (object sender, PaintEventArgs e) {
        //    Color x = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(197)))));
        //    Color y = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(109)))), ((int)(((byte)(135)))));

        //    Graphics c = e.Graphics;
        //    Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), x, y, 250);
        //    c.FillRectangle(bG, 0, 0, Width, Height);
        //}

        private DataTable DT_Zaposlenik;
        private DataTable DT_Skladiste;
        private DataTable DT_Ducan;
        private DataTable DT_Zaposlenik2 = new DataTable();
        private DataTable DT_Skladiste2 = new DataTable();
        private DataTable DT_Ducan2 = new DataTable();
        //DataTable DT_Ducan;

        private void SetCB()
        {
            //fill komercijalist
            DT_Ducan = classSQL.select("SELECT ime_ducana,id_ducan FROM ducan", "ducan").Tables[0];
            cbDucan.DataSource = DT_Ducan;
            cbDucan.DisplayMember = "ime_ducana";
            cbDucan.ValueMember = "id_ducan";

            //fill grupe
            DataTable DT_cbGrupa = classSQL.select("SELECT grupa,id_grupa FROM grupa", "grupa").Tables[0];
            cbGrupa.DataSource = DT_cbGrupa;
            cbGrupa.DisplayMember = "grupa";
            cbGrupa.ValueMember = "id_grupa";

            //fill komercijalist
            DT_Zaposlenik = classSQL.select("SELECT ime +' '+prezime as IME,id_zaposlenik FROM zaposlenici", "zaposlenici").Tables[0];
            cbZaposlenik.DataSource = DT_Zaposlenik;
            cbZaposlenik.DisplayMember = "IME";
            cbZaposlenik.ValueMember = "id_zaposlenik";

            DT_Skladiste2.Columns.Add("id_podgrupa", typeof(string));
            DT_Skladiste2.Columns.Add("naziv", typeof(string));

            DT_Skladiste = classSQL.select("SELECT * FROM podgrupa", "podgrupa").Tables[0];
            for (int i = 0; i < DT_Skladiste.Rows.Count; i++)
            {
                DT_Skladiste2.Rows.Add(DT_Skladiste.Rows[i]["id_podgrupa"].ToString(), DT_Skladiste.Rows[i]["naziv"].ToString());
            }

            cbSkladiste.DataSource = DT_Skladiste2;
            cbSkladiste.DisplayMember = "naziv";
            cbSkladiste.ValueMember = "id_podgrupa";
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 7, h + 7);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 7, h - 7);
        }

        private void dtpOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                btnIspis.PerformClick();
            }
        }

        private void btnIspis_Click(object sender, EventArgs e)
        {
            Report.PrometiPoDanima.frmListePoDanima form = new Report.PrometiPoDanima.frmListePoDanima();
            form.datumOD = dtpOD.Value;
            form.datumDO = dtpDO.Value;
            form.ImeForme = "Promet po danima";
            form.dokumenat = "PrometRobe";

            if (chbDucan.Checked)
            {
                form.ducan = cbDucan.SelectedValue.ToString();
            }
            if (chbBlagajnik.Checked)
            {
                form.blagajnik = cbZaposlenik.SelectedValue.ToString();
            }
            if (chbSkladiste.Checked)
            {
                form.id_podgrupa = cbSkladiste.SelectedValue.ToString();
            }

            if (chbGrupa.Checked)
            {
                form.grupa = cbGrupa.SelectedValue.ToString();
            }

            form.ShowDialog();
        }
    }
}