using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Menu
    {
        public static int nyulak;

        public static void fomenu()
        {
            int input;
            do
            {
                Console.Clear();
                Console.WriteLine("Random nyulak generálása - 1     Fájlból beolvasás - 2");
                input = Convert.ToInt32(Console.ReadLine());
            } while (input > 2 || input < 1);

            if (input == 1)
                randomnyulak();
            else 
                fajlbeolvasosnyulak();

        }

        public static void nav()
        {
            Console.WriteLine("Insert - Következő állapot     Delete - Fájlbament    Home - Vissza a főmenübe      Escape - Kilépés");
            var gomb = Console.ReadKey();
            if (gomb.Key == ConsoleKey.Insert)
                lepesekszama();
            if (gomb.Key == ConsoleKey.Delete)
                fajlbairtxt();
            if (gomb.Key == ConsoleKey.Home)
                fomenu();
            if (gomb.Key == ConsoleKey.Escape)
                Environment.Exit(0);
        }

        public static void randomnyulak()
        {
            
           Console.WriteLine("Hány nyulat szeretnél generálni?");
           nyulak = Convert.ToInt32(Console.ReadLine());
           Mezo a = new Mezo(nyulak);
           Mezo.Kiir();
           nav();
            
        }

        public static void fajlbeolvasosnyulak()
        {
            Console.WriteLine("Add meg az elérési útvonalát a .txt file-odnak(.txt végződéssel): ");
            string fajl = Console.ReadLine();
            bool exists = File.Exists(fajl);
            if (exists)
            {
                Mezo a = new Mezo(fajl);
                Mezo.Kiir();
                nav();
            }
            else
            {
                fajlbeolvasosnyulak();
            }
            
        }

        public static void lepesekszama()
        {
            Console.WriteLine("Hány állapotot szeretnél előlre lépni?: ");
            int allpotoksz = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < allpotoksz; i++)
            {
                Mezo.KovetkezoAllapot();
            }
            nav();
        }

        public static void fajlbairtxt()
        {
            Console.WriteLine("Add meg az állapot számát amivel le akarod menteni a meződet: ");
            string bemenet = Console.ReadLine();
            int allapot;
            bool szame = int.TryParse(bemenet, out allapot);
            if (szame == false)
            {
                fajlbairtxt();
                
            }
            else
            {
                if (File.Exists("allapot" + allapot + ".txt"))
                {
                    Console.WriteLine("A fájl már létezik,felül szeretné írni?(y/n)");
                    string yn = Console.ReadLine();
                    if (yn == "y" || yn == "n")
                    {
                        if (yn == "y")
                        {
                            Mezo.Fajlbair(allapot);
                            Mezo.Kiir();
                            nav();
                        }
                        else
                        {
                            Mezo.Kiir();
                            nav();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Hibás parancs");
                        fajlbairtxt();
                    }
                }
                else
                {
                    Mezo.Fajlbair(allapot);
                    Mezo.Kiir();
                    nav();
                }
               
            }
        }

    }
}
