using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Reservaciones_Proyecto_II_.Formularios
{
    public partial class MesasForm : Form
    {
        public MesasForm()
        {
            InitializeComponent();
            ocultarTab();
            this.Text = "Mesas";
        }
        private void MesasForm_Load(object sender, EventArgs e)
        {

        }
        private void ocultarTab()
        {
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.Appearance = TabAppearance.FlatButtons;
        }
        private void siguienteTab()
        {
            if (tabControl1.SelectedIndex < tabControl1.TabCount - 1)
            {
                tabControl1.SelectedIndex++;
            }
        }
        private void tabAnterior()
        {
            if (tabControl1.SelectedIndex > 0)
            {
                tabControl1.SelectedIndex--;
            }
        }

        //Tab Menu
        private void iconButton1_Click(object sender, EventArgs e)
        {
            tabAnterior();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            siguienteTab();
            if(pnDesayuno.Visible == false) { pnDesayuno.Visible = true; }
            pnAlmuerzo.Visible = false;
            pnPostres.Visible = false;
            pnBebidas.Visible = false;
        }

        private void btnAlmuerzos_Click(object sender, EventArgs e)
        {
            siguienteTab();
            if (pnAlmuerzo.Visible == false) { pnAlmuerzo.Visible = true; }
            pnDesayuno.Visible = false;
            pnPostres.Visible = false;
            pnBebidas.Visible = false;
        }

        private void btnPostres_Click(object sender, EventArgs e)
        {
            siguienteTab();
            if (pnPostres.Visible == false) { pnPostres.Visible = true; }
            pnDesayuno.Visible = false;
            pnAlmuerzo.Visible = false;
            pnBebidas.Visible = false;
        }

        private void btnBebidas_Click(object sender, EventArgs e)
        {
            siguienteTab();
            if (pnBebidas.Visible == false) { pnBebidas.Visible = true; }
            pnDesayuno.Visible = false;
            pnAlmuerzo.Visible = false;
            pnPostres.Visible = false;
        }

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            siguienteTab();
        }
        private void btnAtrasSillas_Click(object sender, EventArgs e)
        {
            siguienteTab();
        }
    }
}
