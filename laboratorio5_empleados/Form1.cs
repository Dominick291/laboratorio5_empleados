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

namespace laboratorio5_empleados
{
    //cargar empleados 

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Empleado> empleados = new List<Empleado>();
        List<Asistencia> asistencias = new List<Asistencia>();
        List<Reporte> reportes = new List<Reporte>();
        public void CargarEmpleados()
        {
            
            //leer el archivo y cargarlo de la lista 
            String fileName = "Empleados.txt";


            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Empleado empleado = new Empleado();
                empleado.NoEmpleado = Convert.ToInt16(reader.ReadLine());
                empleado.Nombre = reader.ReadLine();
                empleado.SueldoHora = Convert.ToDecimal(reader.ReadLine());

                //guardar el empleado a la lista de empleados 
                empleados.Add(empleado);
            }
            //Cerrar el archivo, esta linea es importante porque sino despues de correr varias veces el programa daría error de que el archivo quedó abierto muchas veces. Entonces es necesario cerrarlo despues de terminar de leerlo.
            reader.Close();
        }

        public void CargarAsistencia()
        {
            string fileName = "Asistencia.txt";

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Asistencia asistencia = new Asistencia();
                asistencia.NoEmpleado = Convert.ToInt16(reader.ReadLine());
                asistencia.HoraMes = Convert.ToInt16(reader.ReadLine());
                asistencia.Mes = Convert.ToInt16(reader.ReadLine());

                //guardar el empleado a la lista de empleados 
                asistencias.Add(asistencia);
            }
            reader.Close();
        }

        public void MostrarEmpleados()
        {

            //mostrar la lista de empleados en el gridview 
            dataGridViewEmpleados.DataSource = null;
            dataGridViewEmpleados.DataSource = empleados;
            dataGridViewEmpleados.Refresh();
        }
        public void MostrarAsistencia()
        {

            //mostrar la lista de empleados en el gridview 
            dataGridViewAsistencia.DataSource = null;
            dataGridViewAsistencia.DataSource = asistencias;
            dataGridViewAsistencia.Refresh();
        }

        private void cargar_Click(object sender, EventArgs e)
        {

            CargarEmpleados();
            MostrarEmpleados();
            CargarAsistencia();
            MostrarAsistencia();


        }

        private void calcular_Click(object sender, EventArgs e)
        {
            
            foreach (Empleado empleado in empleados)
            {
                int noEmpleado = empleado.NoEmpleado;
                foreach (Asistencia asistencia in asistencias)
                {
                    if (empleado.NoEmpleado == asistencia.NoEmpleado)
                    {
                        Reporte reporte = new Reporte();
                        reporte.NombreEmpleado = empleado.Nombre;
                        reporte.Mes = asistencia.Mes;
                        reporte.SueldoMensual = empleado.SueldoHora * asistencia.HoraMes;

                        reportes.Add(reporte);
                    }
                }
            }
            dataGridViewReporte.DataSource = null;
            dataGridViewReporte.DataSource = reportes;
            dataGridViewReporte.Refresh();

           
        }

      
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int NoEmpleado = Convert.ToInt16(comboBox1.SelectedValue);
            Empleado empleadoEncontrado = empleados.Find(c => c.NoEmpleado == NoEmpleado);
            label4.Text = empleadoEncontrado.Nombre;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
            comboBox1.DisplayMember = "Mombre";
            comboBox1.ValueMember = "NoEmpleado";
            comboBox1.DataSource = empleados;
        }
    }
    
}
