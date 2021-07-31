using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlEstudiantes_Interfaz
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            button3.Enabled = false;
            Diseño();
        }

        private void Diseño()
        {
            PanelSubMenu.Visible = true;
        }

        private void OcultarSubMenu()
        {
            if (PanelSubMenu.Visible == true)
            {
                PanelSubMenu.Visible = true;
            }
        }

        private void MostrarSubMenu(Panel SubMenu)
        {
            if (SubMenu.Visible == false)
            {
                OcultarSubMenu();
                SubMenu.Visible = true;
            }
            else
            {
                SubMenu.Visible = false;
            }
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            MostrarSubMenu(PanelSubMenu);
        }

        private Form FormActivo = null;

        private void MostrarFormHijo(Form Hijo)
        {
            if (FormActivo != null)
            {
                FormActivo.Close();
            }
            FormActivo = Hijo;
            Hijo.TopLevel = false;
            Hijo.FormBorderStyle = FormBorderStyle.None;
            Hijo.Dock = DockStyle.Fill;
            PanelFormHijo.Controls.Add(Hijo);
            PanelFormHijo.Tag = Hijo;
            Hijo.BringToFront();
            Hijo.Show();
        }


    
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PanelMenuLateral_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MostrarFormHijo(new Form2());
            OcultarSubMenu();
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            MostrarFormHijo(new Form3(Form2.pathExcel));
            //OcultarSubMenu();

        }
    }
}
