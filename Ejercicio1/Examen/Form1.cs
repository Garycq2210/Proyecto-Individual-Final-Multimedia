using System;
using System.Drawing;
using System.Windows.Forms;

namespace Examen
{
    public partial class Form1 : Form
    {
        Bitmap imagenOriginal;

        public Form1()
        {
            InitializeComponent();
        }

        // Botón para cargar imagen
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                imagenOriginal = new Bitmap(dialogo.FileName);
                pictureBox1.Image = imagenOriginal;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        // Botón para clasificar texturas
        private void button2_Click(object sender, EventArgs e)
        {
            if (imagenOriginal == null)
            {
                MessageBox.Show("Primero cargue una imagen.");
                return;
            }

            Bitmap resultado = ClasificarTexturas(imagenOriginal);
            pictureBox2.Image = resultado;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private Bitmap ClasificarTexturas(Bitmap imagen)
        {
            Bitmap salida = new Bitmap(imagen.Width, imagen.Height);

            int bloque = 10; // Tamaño del bloque de análisis

            for (int y = 0; y < imagen.Height; y += bloque)
            {
                for (int x = 0; x < imagen.Width; x += bloque)
                {
                    int sumaR = 0;
                    int sumaG = 0;
                    int sumaB = 0;
                    int contador = 0;

                    for (int j = y; j < y + bloque && j < imagen.Height; j++)
                    {
                        for (int i = x; i < x + bloque && i < imagen.Width; i++)
                        {
                            Color pixel = imagen.GetPixel(i, j);

                            sumaR += pixel.R;
                            sumaG += pixel.G;
                            sumaB += pixel.B;
                            contador++;
                        }
                    }

                    int promedioR = sumaR / contador;
                    int promedioG = sumaG / contador;
                    int promedioB = sumaB / contador;

                    Color colorClase = ClasificarColor(promedioR, promedioG, promedioB);

                    for (int j = y; j < y + bloque && j < imagen.Height; j++)
                    {
                        for (int i = x; i < x + bloque && i < imagen.Width; i++)
                        {
                            salida.SetPixel(i, j, colorClase);
                        }
                    }
                }
            }

            return salida;
        }

        private Color ClasificarColor(int r, int g, int b)
        {
            int brillo = (r + g + b) / 3;

            // Agua: tonos azules o azul verdosos
            if (b > r + 20 && b > g + 10)
            {
                return Color.Blue;
            }

            // Agua verdosa u oscura
            if (g > r + 10 && b > r + 10 && brillo < 130)
            {
                return Color.DarkBlue;
            }

            // Césped: predominio del verde
            if (g > r + 20 && g > b + 20)
            {
                return Color.Green;
            }

            // Tierra: tonos marrones
            if (r > 80 && g > 50 && b < 80 && r > b && g > b)
            {
                return Color.SaddleBrown;
            }

            // Cemento: gris claro
            if (Math.Abs(r - g) < 25 && Math.Abs(g - b) < 25 && brillo >= 120)
            {
                return Color.LightGray;
            }

            // Asfalto: gris oscuro
            if (Math.Abs(r - g) < 30 && Math.Abs(g - b) < 30 && brillo < 120)
            {
                return Color.DarkGray;
            }

            // No clasificado
            return Color.Black;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}