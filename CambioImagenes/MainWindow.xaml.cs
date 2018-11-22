using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;

namespace CambioImagenes
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool jugando = true;
        Pacman pacman;
        Stopwatch stopwatch = new Stopwatch();
        TimeSpan tiempoAnterior;

       
        
        public MainWindow()
        {
            InitializeComponent();

            canvasPrincipal.Focus();

            pacman = new Pacman(spritePacman);

            stopwatch.Start();
            tiempoAnterior = stopwatch.Elapsed;


            ThreadStart threadStart = new ThreadStart(cicloPrincipal);
            Thread thread = new Thread(threadStart);
            thread.Start();

        }

        public void cicloPrincipal()
        {
            while (jugando)
            {
                Dispatcher.Invoke(actualizar);
            }
        }

        public void actualizar()
        {
            TimeSpan tiempoActual = stopwatch.Elapsed;
            double deltaTime = tiempoActual.TotalSeconds - tiempoAnterior.TotalSeconds;

            pacman.Mover(deltaTime);
            pacman.Velocidad += 10 * deltaTime;

            tiempoAnterior = tiempoActual;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            jugando = false;
        }

        private void canvasPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.IsRepeat)
            {
                if (e.Key == Key.Left)
                {
                    pacman.CambiarDireccion(Pacman.Direccion.Izquierda);
                }
                if (e.Key == Key.Right)
                {
                    pacman.CambiarDireccion(Pacman.Direccion.Derecha);
                }
                if (e.Key == Key.Up)
                {
                    pacman.CambiarDireccion(Pacman.Direccion.Arriba);
                }
                if (e.Key == Key.Down)
                {
                    pacman.CambiarDireccion(Pacman.Direccion.Abajo);
                }
            }
            
        }
    }
}
