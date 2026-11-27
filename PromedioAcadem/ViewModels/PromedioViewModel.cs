using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PromedioAcadem.Models;

namespace PromedioAcadem.ViewModels
{
    internal class PromedioViewModel : INotifyPropertyChanged
    {
        private string _cal1 = "", _cal2 = "", _cal3 = "";
        private string _promedio = "", _nivel = "", _mensaje = "";

        //entradas

        public string Cal1 { get => _cal1; set { _cal1 = value; Notificar(); Limpiar();  } }
        public string Cal2 { get => _cal2; set { _cal2 = value; Notificar(); Limpiar(); } }
        public string Cal3 { get => _cal3; set { _cal3 = value; Notificar(); Limpiar(); } }

        //Salidas

        public string Promedio { get => _promedio; set { _promedio = value; Notificar(); } }
        public string Nivel { get => _nivel; set { _nivel = value; Notificar(); } }

        public string Mensaje { get => _mensaje; set { _mensaje = value; Notificar(); } }

        public ICommand CalcularCommand { get; }

        public PromedioViewModel()
        {
            CalcularCommand = new Command(Calcular);
        }

        //logica

        private void Calcular()
        {
            Limpiar();
            if(!Validar(Cal1, out decimal c1) ||
                !Validar(Cal2, out decimal c2) ||
                !Validar(Cal3, out decimal c3))
            {
                Mensaje = "Ingresa las 3 Calificaciones Entre 0 y 10";
                return;
            }

            var datos = new Calificaciones { Unidad1 = c1, Unidad2 = c2, Unidad3 = c3 };
            decimal resultado = datos.CalcularPromedio();

            Promedio = "Promedio: " + resultado.ToString("F2", CultureInfo.InvariantCulture);
            if (resultado < 7) Nivel = "Nivel: No acreditado";
            else if (resultado < 8) Nivel = "Nivel: Suficiente";
            else if (resultado < 9) Nivel = "Nivel Bueno";
            else Nivel = "Excelente";

        }
        private bool Validar(string texto, out decimal numero)
        {
            return decimal.TryParse((texto ?? "").Replace('.', ','),
                NumberStyles.Float, CultureInfo.InvariantCulture, out numero)
                && numero >= 0 && numero <= 10;


        }
        private void Limpiar()
        {
            Promedio = "";
            Nivel = "";
            Mensaje = "";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void Notificar([CallerMemberName]string? nombre = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));

    }

}
