using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kantan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog();
            if (opd.ShowDialog() == DialogResult.OK)
            {
                label1.Text = opd.FileName.ToString();
                Form1.ActiveForm.Text = "Kantan ー " + opd.SafeFileName;
                try
                {
                    var sr = new StreamReader(opd.FileName);
                    caixaTexto.Text = sr.ReadToEnd();
                    sr.Close();
                }
                catch (SecurityException ex)
                {
                }
            }
        }

        private void RecortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caixaTexto.Cut();
        }

        private void CopiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caixaTexto.Copy();
        }

        private void ColarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caixaTexto.Paste();
        }

        private void CaixaTexto_SelectionChanged(object sender, EventArgs e)
        {
            String rol = (1 + caixaTexto.GetLineFromCharIndex(caixaTexto.SelectionStart)).ToString();
            String col = (1 + caixaTexto.SelectionStart - (caixaTexto.GetFirstCharIndexFromLine(1 + caixaTexto.GetLineFromCharIndex(caixaTexto.SelectionStart) - 1))).ToString();
            labelContador.Text = "Ln "+ rol + ", Col " + col ;
        }

        private void NovoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (caixaTexto.TextLength > 0)
            {
                var caixaMensagem = MessageBox.Show("Deseja salvar o arquivo?", "Salvar Arquivo?", MessageBoxButtons.YesNo);
                if (caixaMensagem == DialogResult.Yes)
                {
                    salvarArquivo();
                    caixaTexto.ResetText();
                    label1.Text = "Kantan 0.1";
                    Form1.ActiveForm.Text = "Kantan ー Sem Título";
                }
                else
                {
                    caixaTexto.ResetText();
                    label1.Text = "Kantan 0.1";
                    Form1.ActiveForm.Text = "Kantan ー Sem Título";
                }
            }
        }
        private void salvarArquivo()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var sw = new StreamWriter(sfd.InitialDirectory + sfd.FileName);
                sw.Write(caixaTexto.Text);
                sw.Close();
            }
        }

        private void SalvarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            salvarArquivo();
        }

        private void QuebraAutomáticaDeLinhaToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            String texto = caixaTexto.Text;
            caixaTexto.ResetText();
            caixaTexto.WordWrap = !caixaTexto.WordWrap;
            caixaTexto.Text = texto;
        }
    }
}
