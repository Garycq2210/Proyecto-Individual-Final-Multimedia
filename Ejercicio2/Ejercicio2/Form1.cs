using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ejercicio2
{
    public partial class Form1 : Form
    {
        Bitmap imagenOriginal;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        // Botón para cargar imagen
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                imagenOriginal = new Bitmap(abrir.FileName);
                pictureBox1.Image = imagenOriginal;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        // Botón para aplicar filtro promedio 3x3
        private void button2_Click(object sender, EventArgs e)
        {
            if (imagenOriginal == null)
            {
                MessageBox.Show("Primero debes cargar una imagen.");
                return;
            }

            Bitmap imagenSuavizada = AplicarFiltroPromedio3x3(imagenOriginal); ;

            for (int i = 0; i < 4; i++)
            {
                 imagenSuavizada = AplicarFiltroPromedio3x3(imagenOriginal);
            }             

            pictureBox2.Image = imagenSuavizada;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private Bitmap AplicarFiltroPromedio3x3(Bitmap imagen)
        {
            Bitmap resultado = new Bitmap(imagen.Width, imagen.Height);

            for (int y = 1; y < imagen.Height - 1; y++)
            {
                for (int x = 1; x < imagen.Width - 1; x++)
                {
                    int sumaR = 0;
                    int sumaG = 0;
                    int sumaB = 0;

                    // Ventana de 3x3 píxeles
                    for (int j = -1; j <= 1; j++)
                    {
                        for (int i = -1; i <= 1; i++)
                        {
                            Color pixel = imagen.GetPixel(x + i, y + j);

                            sumaR += pixel.R;
                            sumaG += pixel.G;
                            sumaB += pixel.B;
                        }
                    }

                    int promedioR = sumaR / 9;
                    int promedioG = sumaG / 9;
                    int promedioB = sumaB / 9;

                    resultado.SetPixel(x, y, Color.FromArgb(promedioR, promedioG, promedioB));
                }
            }

            // Copiar bordes originales para que no queden negros
            for (int x = 0; x < imagen.Width; x++)
            {
                resultado.SetPixel(x, 0, imagen.GetPixel(x, 0));
                resultado.SetPixel(x, imagen.Height - 1, imagen.GetPixel(x, imagen.Height - 1));
            }

            for (int y = 0; y < imagen.Height; y++)
            {
                resultado.SetPixel(0, y, imagen.GetPixel(0, y));
                resultado.SetPixel(imagen.Width - 1, y, imagen.GetPixel(imagen.Width - 1, y));
            }

            return resultado;
        }
    }
}