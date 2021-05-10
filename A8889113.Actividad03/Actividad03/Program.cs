using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;


namespace Actividad03
{
    static class Globals
    {
        public static List<int> Asientos = new List<int>();
        public static List<DateTime> Fechas = new List<DateTime>();
        public static List<string> Codigos = new List<string>();
        public static List<int> NumerosDebe = new List<int>();
        public static List<int> NumerosHaber = new List<int>();

        public static int ContadorContable = 0;
    }

    class Program
    {

        static void Main(string[] args)
        {
            if( File.Exists("Diario.txt"))
            {
                File.Delete("Diario.txt");
            }
            bool seguirCreando = true;
            while (seguirCreando == true)
            {
                Asiento.Creacion();

                if (Globals.ContadorContable == 0)
                {
                    Console.WriteLine("Asiento cargado con exito");
                    for (int x = 0; x < Globals.Asientos.Count; x++)
                    {
                        Asiento.NroAsiento = Globals.Asientos[x];
                        Asiento.Fecha = Globals.Fechas[x];
                        Asiento.CodigoCuenta = Globals.Codigos[x];
                        Asiento.Debe = Globals.NumerosDebe[x];
                        Asiento.Haber = Globals.NumerosHaber[x];
                        Asiento.GrabarEn();
                    }

                }
                else
                {
                    Console.WriteLine("Recuerde que Activo = Pasivo + PN. Cargue el asiento nuevamente");
                }              

                Console.WriteLine("Desea cargar otro asiento (S/N)?");
                string respuesta = Console.ReadLine();
                if (respuesta == "S")
                {
                    Globals.ContadorContable = 0;
                    Globals.Asientos.Clear();
                    Globals.Fechas.Clear();
                    Globals.Codigos.Clear();
                    Globals.NumerosDebe.Clear();
                    Globals.NumerosHaber.Clear();
                    seguirCreando = true;
                }
                else
                {
                    seguirCreando = false;
                }
            }
            
        }

    }





    public class Asiento
    {
        public static int NroAsiento { get; set; }
        public static DateTime Fecha { get; set; }
        public static string CodigoCuenta { get; set; }
        public static int Debe { get; set; }
        public static int Haber { get; set; }
        public Asiento() { }
        public Asiento(string linea)
        {
            var datos = linea.Split('|');

            NroAsiento = int.Parse(datos[0]);
            Fecha = DateTime.Parse(datos[1]);
            CodigoCuenta = datos[2];
            Debe = int.Parse(datos[3]);
            Haber = int.Parse(datos[4]);
        }

        public static void GrabarEn()
        {       
            File.AppendAllText("Diario.txt", $"{NroAsiento}|{Fecha:yyyy-MM-dd}|{CodigoCuenta}|{Debe}|{Haber}\n");
            
        }

        public static void Creacion()
        {
            bool esNumero = false;
            int intNumeroAsiento = 0;
            while (esNumero == false)
            {
                Console.WriteLine("Ingrese el numero de asiento a registrar (debe ser un numero)");
                string strNumeroAsiento = Console.ReadLine();
                esNumero = Int32.TryParse(strNumeroAsiento, out intNumeroAsiento);
                if(esNumero == false)
                {
                    Console.WriteLine("El numero de asiento debe ser un numero");
                }
            }


            int continuar = 0;

            while (continuar == 0)
            {
                Globals.Asientos.Add(intNumeroAsiento);
                bool continuar2 = true;
                string format = "dd/MM/yyyy";
                while (continuar2 == true)
                {
                    Console.WriteLine("Escriba la fecha del movimiento en el siguiente formato: dd/MM/yyyy");
                    string FechaIngresada = Console.ReadLine();
                    //TODO: CHEQUEAR FORMATO
                    if (DateTime.TryParseExact(FechaIngresada, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaMovimiento) == false)
                    {
                        Console.WriteLine("No es una fecha válida");
                        continuar2 = true;
                    }

                    else if (fechaMovimiento > DateTime.Now)
                    {
                        Console.WriteLine("La fecha debe ser menor a la actual");
                        continuar2 = true;
                    }

                    else
                    {
                        continuar2 = false;
                        Globals.Fechas.Add(fechaMovimiento);
                    }
                }

                bool existe = false;
                while (existe == false)
                {
                    Console.WriteLine("Seleccione el movimiento a realizar:");
                    Console.WriteLine("11- Construcciones en proceso");
                    Console.WriteLine("12- Otras propiedades, planta y equipo");
                    Console.WriteLine("13- Activos intangibles y plusvalía");
                    Console.WriteLine("14- Marcas comerciales");
                    Console.WriteLine("15- Programas de computador");
                    Console.WriteLine("16- Licencias y franquicias");
                    Console.WriteLine("17- Derechos de propiedad intelectual, patentes y otros");
                    Console.WriteLine("18- Recetas, fórmulas, modelos, diseños y prototipos");
                    Console.WriteLine("19- Activos intangibles en desarrollo");
                    Console.WriteLine("20- Inversiones en subsidiarias, negocios conjuntos y asociadas");
                    Console.WriteLine("21- Deudores comerciales");
                    Console.WriteLine("22- Cuentas por cobrar");
                    Console.WriteLine("23- Activos financieros");
                    Console.WriteLine("24- Efectivo en caja");
                    Console.WriteLine("25- Cuentas corrientes");
                    Console.WriteLine("26- Depósitos a corto plazo");
                    Console.WriteLine("27- Inversiones a corto plazo");
                    Console.WriteLine("28- Cuentas por pagar comerciales");
                    Console.WriteLine("29- Impuestos por pagar");
                    Console.WriteLine("30- Préstamos bancarios");
                    Console.WriteLine("31- Obligaciones con el público");
                    Console.WriteLine("32- Obligaciones por leasing");
                    Console.WriteLine("33- Capital emitido");
                    Console.WriteLine("34- Ganancias (pérdidas) acumuladas");

                    string strMovimientoElegido = Console.ReadLine();
                    string TipoMovimiento;
                    int intMontoMovimiento = 0;

                    bool esNumero2 = false;
                    while (esNumero2 == false)
                    {
                        Console.WriteLine("Cual fue el monto de este movimiento? (Numero Entero)");
                        var ingreso = Console.ReadLine();
                        esNumero2 = int.TryParse(ingreso, out intMontoMovimiento);
                        if (esNumero == false)
                        {
                            Console.WriteLine("No ha ingreso un numero valido");
                        }
                    }

                    if ((strMovimientoElegido == ("11") || (strMovimientoElegido == "12") || (strMovimientoElegido == "13") || (strMovimientoElegido == "14") || (strMovimientoElegido == "15") || (strMovimientoElegido == "16") || (strMovimientoElegido == "17") || (strMovimientoElegido == "18") || (strMovimientoElegido == "19") || (strMovimientoElegido == "20") || (strMovimientoElegido == "21") || (strMovimientoElegido == "22") || (strMovimientoElegido == "23") || (strMovimientoElegido == "24") || (strMovimientoElegido == "25") || (strMovimientoElegido == "26") || (strMovimientoElegido == "27")))
                    {
                        TipoMovimiento = "Activo";
                        existe = true;
                        Globals.Codigos.Add(strMovimientoElegido);
                        Globals.NumerosDebe.Add(intMontoMovimiento);
                        Globals.NumerosHaber.Add(0);
                        Globals.ContadorContable = Globals.ContadorContable + intMontoMovimiento;

                    }
                    else if ((strMovimientoElegido == "28") || (strMovimientoElegido == "29") || (strMovimientoElegido == "30") || (strMovimientoElegido == "31") || (strMovimientoElegido == "32"))
                    {
                        TipoMovimiento = "Pasivo";
                        existe = true;
                        Globals.Codigos.Add(strMovimientoElegido);
                        Globals.NumerosHaber.Add(intMontoMovimiento);
                        Globals.NumerosDebe.Add(0);
                        Globals.ContadorContable = Globals.ContadorContable - intMontoMovimiento;

                    }
                    else if ((strMovimientoElegido == "33") || (strMovimientoElegido == "34"))
                    {
                        TipoMovimiento = "Patrimonio Neto";
                        existe = true;
                        Globals.Codigos.Add(strMovimientoElegido);
                        Globals.NumerosHaber.Add(intMontoMovimiento);
                        Globals.NumerosDebe.Add(0);
                        Globals.ContadorContable = Globals.ContadorContable - intMontoMovimiento;

                    }
                    else
                    {
                        Console.WriteLine("La opcion elegida no existe. Debe ser un numero del 11 al 34");
                        existe = false;
                    }
                }


                bool loop2 = true;
                while(loop2 == true)
                {
                    Console.WriteLine("Deseas cargar otro movimiento? (S/N)");
                    string respuestaContinuar = Console.ReadLine();
                    if (respuestaContinuar == "S")
                    {
                        continuar = 0;
                        loop2 = false;
                    }
                    else if (respuestaContinuar == "N")
                    {
                        continuar = 1;
                        loop2 = false;
                    }
                    else
                    {
                        Console.WriteLine("La respuesta debe ser S o N. Intente de nuevo.");
                        loop2 = true;
                    }
                }

            }

        }
    }
}