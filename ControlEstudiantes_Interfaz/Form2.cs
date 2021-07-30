using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlEstudiantes_Interfaz.ViewModel;
using SpreadsheetLight;

namespace ControlEstudiantes_Interfaz
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public static string pathExcel = "";

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.ofdCargarArchivo.InitialDirectory = @"C:\";
                this.ofdCargarArchivo.Title = "Carga de Archivo";
                this.ofdCargarArchivo.CheckFileExists = true;
                this.ofdCargarArchivo.CheckPathExists = true;
                this.ofdCargarArchivo.DefaultExt = ".xlsx";
                this.ofdCargarArchivo.Filter = "Archivos de Excel (*.xlsx)|*.xlsx"; //OJO
                this.ofdCargarArchivo.RestoreDirectory = true;
                this.ofdCargarArchivo.FilterIndex = 2;
                this.ofdCargarArchivo.ShowReadOnly = true;
                this.ofdCargarArchivo.ReadOnlyChecked = true;

                if (this.ofdCargarArchivo.ShowDialog() == DialogResult.OK)
                {
                    this.txtRutaArchivo.Text = this.ofdCargarArchivo.FileName;
                    
                }
            }
            catch (Exception ex)
            {
                string n = ex.Message;

                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pathExcel = this.ofdCargarArchivo.FileName;
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
    }
}
