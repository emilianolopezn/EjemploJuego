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


namespace CambioImagenes
{
    public class Pacman
    {
        List<BitmapImage> arriba = new List<BitmapImage>();
        List<BitmapImage> abajo = new List<BitmapImage>();
        List<BitmapImage> izquierda = new List<BitmapImage>();
        List<BitmapImage> derecha = new List<BitmapImage>();

        Image Imagen { get; set; }

        public enum Direccion { Izquierda, Derecha, Arriba, Abajo};
        Direccion DireccionActual { get; set; }

        double PosicionX { get; set; }
        double PosicionY { get; set; }

        public double Velocidad { get; set; }

        int spriteActual = 0;
        double tiempoTranscurridoEnSprite = 0;
        double tiempoPorSprite = 0.25;

        public Pacman(Image imagen)
        {
            Imagen = imagen;
            arriba.Add(new BitmapImage(new Uri("arriba1.png", UriKind.Relative)));
            arriba.Add(new BitmapImage(new Uri("arriba2.png", UriKind.Relative)));

            abajo.Add(new BitmapImage(new Uri("abajo1.png", UriKind.Relative)));
            abajo.Add(new BitmapImage(new Uri("abajo2.png", UriKind.Relative)));

            izquierda.Add(new BitmapImage(new Uri("izquierda1.png", UriKind.Relative)));
            izquierda.Add(new BitmapImage(new Uri("izquierda2.png", UriKind.Relative)));

            derecha.Add(new BitmapImage(new Uri("derecha1.png", UriKind.Relative)));
            derecha.Add(new BitmapImage(new Uri("derecha2.png", UriKind.Relative)));
            Imagen.Source = derecha[0];

            PosicionX = Canvas.GetLeft(imagen);
            PosicionY = Canvas.GetTop(imagen);

            DireccionActual = Direccion.Derecha;

            Velocidad = 20;

        }

        public void CambiarDireccion(Direccion nuevaDireccion)
        {
            DireccionActual = nuevaDireccion;
            switch (DireccionActual)
            {
                case Direccion.Abajo:
                    Imagen.Source = abajo[0];
                    break;
                case Direccion.Arriba:
                    Imagen.Source = arriba[0];
                    break;
                case Direccion.Izquierda:
                    Imagen.Source = izquierda[0];
                    break;
                case Direccion.Derecha:
                    Imagen.Source = derecha[0];
                    break;
                default:
                    break;
            }
        }

        public void Mover(double deltaTime)
        {
            tiempoTranscurridoEnSprite += deltaTime;
            int spriteAnterior = spriteActual;
            if (tiempoTranscurridoEnSprite >= tiempoPorSprite)
            {
                spriteActual++;
                tiempoTranscurridoEnSprite -= tiempoPorSprite;
                if (spriteActual > 1)
                {
                    spriteActual = 0;
                }
            }
            BitmapImage sprite = null;
            switch (DireccionActual)
            {
                case Direccion.Abajo:
                    PosicionY += Velocidad * deltaTime;
                    sprite = abajo[spriteActual];
                    
                    break;
                case Direccion.Arriba:
                    PosicionY -= Velocidad * deltaTime;
                    sprite = arriba[spriteActual];
                    
                    break;
                case Direccion.Izquierda:
                    PosicionX -= Velocidad * deltaTime;
                    sprite = izquierda[spriteActual];
                    break;
                case Direccion.Derecha:
                    PosicionX += Velocidad * deltaTime;
                    sprite = derecha[spriteActual];
                    break;
                default:
                    break;
            }
            if (spriteAnterior != spriteActual && sprite != null)
            {
                Imagen.Source = sprite;
            }
            Canvas.SetLeft(Imagen, PosicionX);
            Canvas.SetTop(Imagen, PosicionY);


        }
    }
}
