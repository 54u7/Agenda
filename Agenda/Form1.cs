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

namespace Agenda
{
    public partial class Form1 : Form
    {
        private List<Contacto> Contactos = new List<Contacto>();
        private int indice = -1;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            TextWriter escribir = new StreamWriter("Agenda.txt");  
            foreach(Contacto persona in Contactos)
            {
                escribir.WriteLine(persona.Nombre +"|"+ persona.Apellido +"|"+ persona.Direccion +"|"+ 
                    persona.Telefono +"|"+ persona.Movil + "|" + persona.Estado + "|" + persona.Nacimiento + "|" + 
                    persona.Genero + "|" + persona.Correo);
            }
            escribir.Close();
            MessageBox.Show("Contactos guardados");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Contacto persona = new Contacto();
            persona.Nombre = txtNom.Text;
            persona.Apellido = txtApe.Text;
            persona.Direccion = txtDire.Text;
            persona.Telefono = txtTele.Text;
            persona.Movil = txtMov.Text;
            persona.Estado = txtEstado.Text;
            persona.Nacimiento = txtFecha.Text;
            persona.Genero = txtGenero.Text;
            persona.Correo = txtCorreo.Text;

            if (indice > -1)
            {
                Contactos[indice] = persona;
                indice = -1;
            }
            else
            {
                Contactos.Add(persona);
            }

            limpiarCampos();
            actualizarVista();
        }
        private void actualizarVista()
        {
            dgv.DataSource = null;
            dgv.DataSource = Contactos;
            dgv.ClearSelection();
        }

        private void dgv_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = dgv.SelectedRows[0];
            indice = dgv.Rows.IndexOf(dgvr);
            Contacto Persona = Contactos[indice];
            txtNom.Text = Persona.Nombre;
            txtApe.Text = Persona.Apellido;
            txtDire.Text = Persona.Direccion;
            txtTele.Text = Persona.Telefono;
            txtMov.Text = Persona.Movil;
            txtEstado.Text = Persona.Estado;
            txtFecha.Text = Persona.Nacimiento;
            txtGenero.Text = Persona.Genero;
            txtCorreo.Text = Persona.Correo;

        }
        private void limpiarCampos()
        {
            txtNom.Text = null;
            txtApe.Text = null;
            txtDire.Text = null;
            txtTele.Text = null;
            txtMov.Text = null;
            txtEstado.Text = null;
            txtFecha.Text = null;
            txtGenero.Text = null;
            txtCorreo.Text = null;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (indice > -1)
            {
                Contactos.RemoveAt(indice);
                actualizarVista();
                limpiarCampos();
                indice = -1;
            }
            else
            {
                MessageBox.Show("Debe seleccionar un campo a borrar");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader lector = new StreamReader("Agenda.txt");
                string linea;
                while((linea = lector.ReadLine()) != null)
                {
                    int posicion;
                    Contacto persona = new Contacto();
                    posicion = linea.IndexOf("|");
                    persona.Nombre = linea.Substring(0,posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    persona.Apellido = linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    persona.Direccion = linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    persona.Telefono = linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    persona.Movil = linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    persona.Estado = linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    persona.Nacimiento = linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    persona.Genero = linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    persona.Correo = linea.Substring(0, posicion);
                    linea = linea.Substring(posicion + 1);
                    posicion = linea.IndexOf("|");

                    Contactos.Add(persona);
                }
                lector.Close();
                actualizarVista();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Ejecucion finalizada");
            }
        }
    }
}
