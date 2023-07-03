using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace ApiMovimientos
{
    public class Program
    {
        #region test-web
        static int[] myArray = { 1, 2, 1, 3, 3, 1, 2, 1, 5, 1 };

        public void Ejercicio9()
        {
            string uno = "", dos = "", tres = "", cuatro = "", cinco = "";
            bool valido = true;
            for (int i = 0; i < myArray.Length; i++)
            {
                if (myArray[i] < 1 || myArray[i] > 5) { valido = false; break; }
                switch (myArray[i])
                {
                    case 1:
                        uno += "*";
                        break;
                    case 2: dos += "*"; break;
                    case 3: tres += "*"; break;
                    case 4: cuatro += "*"; break;
                    case 5: cinco += "*"; break;
                }
            }

            if (!valido)
            {
                Console.WriteLine("enteros fuera del rango de 1 a 5");
            }
            else
            {
                Console.WriteLine("1: " + uno);
                Console.WriteLine("2: " + dos);
                Console.WriteLine("3: " + tres);
                Console.WriteLine("4: " + cuatro);
                Console.WriteLine("5: " + cinco);
            }

        }
        public void Ejercicio10()
        {
            int mayorSec = 1;
            int mayorSecAux = 1;
            int numeroMayorSec = 0;
            int numeroAux = 0;
            for (int i = 0; i < myArray.Length; i++)
            {
                if (myArray[i] == numeroAux)
                { mayorSecAux += 1; }
                else
                { mayorSecAux = 0; }
                if (mayorSecAux > mayorSec)
                {
                    mayorSec = mayorSecAux;
                    numeroMayorSec = myArray[i];
                }
                numeroAux = myArray[i];
            }
            Console.WriteLine("Longest: " + (mayorSec + 1));
            Console.WriteLine("Number: " + numeroMayorSec);
        }

        public void Ejercicio11()
        {
            int[][] array = new int[3][]
          {
                new int[3] {1,2,9},
                new int[3] {2,5,3},
                new int[3] {5,1,5}
          };
            int posIni = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i][0] < array[posIni][0])
                {
                    posIni = i;
                }
            }

            int posInis = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i][1] < array[posInis][1])
                {
                    posInis = i;
                }

            }

            int posInit = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i][2] < array[posInit][2])
                {
                    posInit = i;
                }
            }
            Console.Write(array[posIni][0]);
            Console.Write(array[posInis][1]);
            Console.Write(array[posInit][2]);
        }
        #endregion
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
