using ControlEstudiantes_Interfaz.ViewModel;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ControlEstudiantes_Interfaz
{
    public partial class Form3 : Form
    {
        List<Alumnos> ListaAlumnos = new List<Alumnos>(); // Lista de Alumnos (Datos de excel)
        
        int posicion;
        static string Ruta;

        public Form3(string _ruta)
        {
            InitializeComponent();
            CargarDatos();

            button3.Enabled = false;

            Ruta = _ruta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /* --- BUSCAR DATOS --- */
        private void button2_Click(object sender, EventArgs e)
        {
            //dgvDatos.Focus();
            BuscarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                SLDocument sL = new SLDocument(Form2.pathExcel);

                int iRow = 2; // Leer desde fila 2

                    while (!string.IsNullOrEmpty(sL.GetCellValueAsString(iRow, 1))) // mientras el excel no tenga una fila/columna vacia
                    {
                        Alumnos DataAlumnos = new Alumnos();
                        DataAlumnos.Marca_Temporal = sL.GetCellValueAsDateTime(iRow, 1);
                        DataAlumnos.Carne = sL.GetCellValueAsInt32(iRow, 2); //obtiene el valor de (Fila, columna) y lo guarda en el objeto
                        DataAlumnos.Carrera = sL.GetCellValueAsString(iRow, 3);
                        DataAlumnos.Curso = sL.GetCellValueAsString(iRow, 4);
                        DataAlumnos.Modalidad = sL.GetCellValueAsString(iRow, 5);
                        DataAlumnos.Seccion = sL.GetCellValueAsInt32(iRow, 6);
                        DataAlumnos.Grupo = sL.GetCellValueAsString(iRow, 7);

                        ListaAlumnos.Add(DataAlumnos); //Guardar en lista
                        iRow++;
                        
                    }

                dgvDatos.DataSource = ListaAlumnos;

            }
            catch (Exception)
            {
                MessageBox.Show("Error al Cargar el Archivo");
                MessageBox.Show("Verifique que el archivo de excel no este abierto");
                
            }
            
        }

        /* --- MODIFICAR DATOS ---*/
        private void button3_Click(object sender, EventArgs e)
        {
            
            string Modalidad;
            string NuevaMarca;

            NuevaMarca = DateTime.Now.ToLongDateString() + " "+ DateTime.Now.ToString("hh:mm:ss");
            Modalidad = txtModalidad.Text;
            dgvDatos[4, posicion].Value = txtModalidad.Text;
            dgvDatos[0, posicion].Value = NuevaMarca;
            txtCarne.Clear();
            txtCarrera.Clear();
            txtCurso.Clear();
            txtModalidad.Text = "";

            BuscarDatos();
            Guardar(Ruta);
        }

        // Evento Click para las celdas
        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            posicion = dgvDatos.CurrentRow.Index;

            // Solo se modifica la Modalidad

            txtCarne.Text = dgvDatos[1, posicion].Value.ToString();
            txtCarrera.Text = dgvDatos[2, posicion].Value.ToString();
            txtCurso.Text = dgvDatos[3, posicion].Value.ToString();
            txtModalidad.Text = dgvDatos[4, posicion].Value.ToString();

            button3.Enabled = true; //Activa el boton modificar al seleccionar un dato
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtCarne.Clear();
            txtCarrera.Clear();
            txtCurso.Clear();
            txtModalidad.Text = "";

            BuscarDatos();
            
        }

        // GUARDAR COMO 
        private void button6_Click(object sender, EventArgs e)
        {
           
            try
            {
                this.sfdGuardarArchivo.DefaultExt = ".xlsx";
                this.sfdGuardarArchivo.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";

                if (sfdGuardarArchivo.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfdGuardarArchivo.FileName))
                    {

                        Guardar(sfdGuardarArchivo.FileName);
                        MessageBox.Show("Documento guardado");
                    }
                    else
                    {
                        Guardar(sfdGuardarArchivo.FileName);
                        MessageBox.Show("Documento guardado");
                    }
                }


            }
            catch (Exception)
            {

                MessageBox.Show("Error al Guardar");
            }
        }

        private void Guardar(string Ruta)
        {
            SLDocument sl = new SLDocument();

            int iC = 1;
            foreach (DataGridViewColumn Columna in dgvDatos.Columns) //Extrae los nombres de las columnas
            {
                sl.SetCellValue(1, iC, Columna.HeaderText.ToString());
                iC++;
            }

            int iR = 2;
            foreach (DataGridViewRow Fila in dgvDatos.Rows) // Extrae el valor de cada fila
            {
                sl.SetCellValue(iR, 1, Convert.ToDateTime(Fila.Cells[0].Value));
                sl.SetCellValue(iR, 2, int.Parse(Fila.Cells[1].Value.ToString()));
                sl.SetCellValue(iR, 3, Fila.Cells[2].Value.ToString());
                sl.SetCellValue(iR, 4, Fila.Cells[3].Value.ToString());
                sl.SetCellValue(iR, 5, Fila.Cells[4].Value.ToString());
                sl.SetCellValue(iR, 6, int.Parse(Fila.Cells[5].Value.ToString()));
                sl.SetCellValue(iR, 7, Fila.Cells[6].Value.ToString());
                iR++;
            }
            sl.SaveAs(@Ruta);
            
            
        }

        // busqueda Contagios 
        private void button4_Click(object sender, EventArgs e)
        {
            var ListaGeneral = ListaAlumnos;
            var ListaPresencial = new List<Alumnos>(); // Lista de los Posibles contagios
            var ListaDePosiblescontagios = new Dictionary<int, Alumnos>();

            foreach (DataGridViewRow Fila in dgvDatos.Rows)
            {
                //var temp = dgvDatos.
                Alumnos Contagiado = new Alumnos();
                if (Fila.Cells[4].Value.ToString().Trim().ToLower() == "presencial")
                {
                    Contagiado.Carne = Convert.ToInt32(Fila.Cells[1].Value.ToString());
                    Contagiado.Carrera = Fila.Cells[2].Value.ToString();
                    Contagiado.Curso = Fila.Cells[3].Value.ToString();
                    Contagiado.Modalidad = Fila.Cells[4].Value.ToString();
                    Contagiado.Seccion = Convert.ToInt32(Fila.Cells[5].Value.ToString());
                    Contagiado.Grupo = Fila.Cells[6].Value.ToString();

                    ListaPresencial.Add(Contagiado);
                }
            }

            if (ListaPresencial != null)
            {
                foreach (var clase in ListaPresencial)
                {
                    var posibleContagio = (from estudiante in ListaGeneral
                                           where clase.Curso == estudiante.Curso && 
                                            clase.Seccion == estudiante.Seccion && 
                                            clase.Grupo == estudiante.Grupo
                                            select estudiante
                                           ).AsParallel().ToList();

                    foreach (var item in posibleContagio)
                    {
                        if (!ListaDePosiblescontagios.TryGetValue(item.Carne, out Alumnos alumno))
                        {
                            ListaDePosiblescontagios.Add(item.Carne, item);
                        }
                    }


                }
            }
                                              
            dgvDatos.DataSource = ListaDePosiblescontagios.Select(x => x.Value).ToList();
        }

        private void BuscarDatos()
        {
            posicion = 0;
            var resultado = ListaAlumnos;

            // Carrera
            if (txtCarrera.Text.Trim() != "")
            {
                resultado = (from item in resultado
                             where item.Carrera.ToLower().Contains(txtCarrera.Text.ToLower().Trim())
                             select item).AsParallel().ToList();
            }

            // Curso
            if (txtCurso.Text.Trim() != "")
            {
                resultado = (from item in resultado
                             where item.Curso.ToLower().Contains(txtCurso.Text.ToLower().Trim())
                             select item).AsParallel().ToList();
            }

            // Modalidad
            if (txtModalidad.Text.Trim() != "")
            {
                resultado = (from item in resultado
                             where item.Modalidad.ToLower().Contains(txtModalidad.Text.ToLower().Trim())
                             select item).AsParallel().ToList();
            }

            // Carne
            if (txtCarne.Text.Trim() != "")
            {
                resultado = (from item in resultado
                             where item.Carne == Convert.ToInt32(txtCarne.Text)
                             select item).AsParallel().ToList();
            }
            dgvDatos.DataSource = resultado;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Guardar(Ruta);
        }
    }
}
