using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Nyuszi
    {
        public int x, y;

        public static Random r = new Random();
        


        public Nyuszi(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }


    public class Mezo
    {
       private static List<Nyuszi> nyuszik = new List<Nyuszi>();
       private static List<Nyuszi> kovetkezo = new List<Nyuszi>();
       public static string[,] mezok = new string[0,0]; 

        public Mezo(int nyulszam)
        {

            Random r = new Random();
            Console.WriteLine("A mező négyzet alakú,egy számot adj meg(pl.10 akkor 10x10 - 100 meződ lesz)");
            Console.WriteLine("Mekkora legyen a mező?: ");
            int mezon = Convert.ToInt32(Console.ReadLine());
            if (mezon * mezon <= Menu.nyulak)
            {
                Console.WriteLine("A meződ nem lehet kisebb(se egyenlő) mint a megadott nyulak száma!");
                Menu.randomnyulak();
            }
            else
            {
                mezok = new string[mezon, mezon];

                List<int[]> koord = new List<int[]>();


                for (int j = 0; j < mezon; j++)
                {
                    for (int k = 0; k < mezon; k++)
                    {
                        mezok[j, k] = " ";
                    }
                }

                bool elso = true;
                for (int i = 0; i < nyulszam; i++)
                {
                    Nyuszi nyul = new Nyuszi(r.Next(0, mezon), r.Next(0, mezon));
                    int[] kordinatak = new int[] { nyul.x, nyul.y };

                    if (elso == true)
                    {
                        koord.Add(kordinatak);
                        elso = false;
                    }

                    int ok = 0;

                    for (int j = 0; j < koord.Count; j++)
                    {
                        if (koord[j][0] == nyul.x && koord[j][1] == nyul.y)
                        {
                            ok++;
                        }
                    }
                    if (ok == 0)
                    {
                        koord.Add(kordinatak);
                        nyuszik.Add(nyul);

                    }
                    else
                    {
                        i--;
                    }


                }

                for (int i = 0; i < nyuszik.Count; i++)
                {
                    mezok[nyuszik[i].x, nyuszik[i].y] = "x";
                }
            }
            
        }

        public Mezo(string fajl)
        {
            StreamReader sr = new StreamReader(fajl);
            List<string> mezomeret =sr.ReadLine().Split(' ').ToList();
            int a = Convert.ToInt32(mezomeret[0]);
            mezok = new string[a,a];
            for (int j = 0; j < a; j++)
            {
                for (int k = 0; k<a; k++)
                {
                    mezok[j, k] = " ";
                }
            }

            int i = 0;
            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                char[] karakterek = sor.ToCharArray();
                for (int k = 0; k < karakterek.Length; k++)
                {

                    if (karakterek[k] == 'x')
                    {
                        Nyuszi nyuszi = new Nyuszi(i, k);
                        nyuszik.Add(nyuszi);
                    }
                   
                }
                i++;
            }

            for (int j = 0; j < nyuszik.Count; j++)
            {
                mezok[nyuszik[j].x, nyuszik[j].y] = "x";
            }


            
            



        }

        


        public static void Kiir()
        {
            Console.Clear();
            int x = mezok.GetLength(0);
            int y = mezok.GetLength(1);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Console.Write(mezok[i,j]);
                }
                Console.WriteLine();
            }
            
            

        }

        public static void Fajlbair(int sorszam)
        {
            int x = mezok.GetLength(0);
            int y = mezok.GetLength(1);
            StreamWriter sw = new StreamWriter("allapot"+sorszam+".txt");
            sw.WriteLine(x);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    sw.Write(mezok[i, j]);
                }
                sw.WriteLine();
            }
            sw.Close();
        }

        private static void Szuletes()
        {
            int x = mezok.GetLength(0);
            int y = mezok.GetLength(1);
            int db = 0;
            for (int i = 1; i < x-1; i++)
            {
                for (int j = 1; j < y-1; j++)
                {
                    if (mezok[i,j] == " ")
                    {
                        if (mezok[i+1,j] == "x")
                            db++;
                        if (mezok[i-1,j] == "x")
                            db++;
                        if (mezok[i,j+1] == "x")
                            db++;
                        if (mezok [i,j-1] == "x")
                            db++;
                        //átlók
                        if (mezok[i + 1, j + 1] == "x")
                            db++;
                        if (mezok[i - 1, j + 1] == "x")
                            db++;
                        if (mezok[i - 1, j - 1] == "x")
                            db++;
                        if (mezok[i + 1, j - 1] == "x")
                            db++;
                        if (db == 3)
                        {
                            Nyuszi nyuszi = new Nyuszi(i, j);
                            kovetkezo.Add(nyuszi);
                        }
                        db = 0;
                    }
                }
            }

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (mezok[i,j] == " ")
                    {
                        //sarkok
                        //balfelső
                        if (i == 0 && j == 0)
                        {
                            if(mezok[i,y-1] == "x")
                                db++;
                            if (mezok[x-1,j] == "x")
                                db++;
                            if (mezok[i+1,j] == "x")
                                db++;
                            if(mezok[i,j+1] == "x")
                                db++;
                            //átlók
                            if (mezok[i + 1, j + 1] == "x")
                                db++;
                            if (mezok[x - 1, y - 1] == "x")
                                db++;

                        }
                        //balalsó
                        if(i == x-1 && j == 0)
                        {
                            if (mezok[i, y-1] == "x")
                                db++;
                            if (mezok[0, j] == "x")
                                db++;
                            if (mezok[i - 1, j] == "x")
                                db++;
                            if (mezok[i, j + 1] == "x")
                                db++;
                            //átlók
                            if (mezok[0, y - 1] == "x")
                                db++;
                            if (mezok[i - 1, j + 1] == "x")
                                db++;
                        }
                        //jobbfelső
                        if (i == 0 && j == y-1)
                        {
                            if (mezok[i, 0] == "x")
                                db++;
                            if (mezok[x-1, j] == "x")
                                db++;
                            if (mezok[i + 1, j] == "x")
                                db++;
                            if (mezok[i, j - 1] == "x")
                                db++;
                            //átlók
                            if (mezok[x - 1, 0] == "x")
                                db++;
                            if (mezok[i + 1, j - 1] == "x")
                                db++;

                        }
                        //jobbalsó
                        if (i == x-1 && j == y - 1)
                        {
                            if (mezok[0, y-1] == "x")
                                db++;
                            if (mezok[i, 0] == "x")
                                db++;
                            if (mezok[i - 1, j] == "x")
                                db++;
                            if (mezok[i, j - 1] == "x")
                                db++;
                            //átlók
                            if (mezok[0, 0] == "x")
                                db++;
                            if (mezok[i - 1, j - 1] == "x")
                                db++;
                        }

                        //szélek
                        
                        //teteje
                        if (i == 0 && j > 0 && j < y - 1)
                        {
                            if (mezok[x-1,j] == "x")
                                db++;
                            if (mezok[i + 1, j] == "x")
                                db++;
                            if (mezok[i, j + 1] == "x")
                                db++;
                            if (mezok[i, j - 1] == "x")
                                db++;
                            //átlók
                            if (mezok[x - 1, j + 1] == "x")
                                db++;
                            if (mezok[x - 1, j - 1] == "x")
                                db++;
                            if (mezok[i + 1, j + 1] == "x")
                                db++;
                            if (mezok[i + 1, j - 1] == "x")
                                db++;
                        }
                        //alja
                        if (i == x - 1 && j > 0 && j < y - 1)
                        {
                            if (mezok[0,j] == "x")
                                db++;
                            if (mezok[i - 1, j] == "x")
                                db++;
                            if (mezok[i, j + 1] == "x")
                                db++;
                            if (mezok[i, j - 1] == "x")
                                db++;
                            //átlók
                            if (mezok[0, j + 1] == "x")
                                db++;
                            if (mezok[0, j - 1] == "x")
                                db++;
                            if (mezok[i - 1, j + 1] == "x")
                                db++;
                            if (mezok[i - 1, j - 1] == "x")
                                db++;
                        }
                        //baloldal
                        if (j == 0 && i > 0 && i < x-1)
                        {
                            if (mezok[i,y-1] == "x")
                                db++;
                            if (mezok[i + 1, j] == "x")
                                db++;
                            if (mezok[i - 1, j] == "x")
                                db++;
                            if (mezok[i, j + 1] == "x")
                                db++;
                            //átlók
                            if (mezok[i + 1, j + 1] == "x")
                                db++;
                            if (mezok[i - 1, j + 1] == "x")
                                db++;
                            if (mezok[i + 1, y - 1] == "x")
                                db++;
                            if (mezok[i - 1, y - 1] == "x")
                                db++;
                        }
                        //jobboldal
                        if (j == y-1 && i > 0 && i < x-1)
                        {
                            if (mezok[i,0] == "x")
                                db++;
                            if (mezok[i + 1, j] == "x")
                                db++;
                            if (mezok[i - 1, j] == "x")
                                db++;
                            if (mezok[i, j - 1] == "x")
                                db++;
                            //átlók
                            if (mezok[i + 1, j - 1] == "x")
                                db++;
                            if (mezok[i - 1, j - 1] == "x")
                                db++;
                            if (mezok[i + 1, 0] == "x")
                                db++;
                            if (mezok[i - 1, 0] == "x")
                                db++;
                        }
                        if (db == 3)
                        {
                            Nyuszi nyuszi = new Nyuszi(i, j);
                            kovetkezo.Add(nyuszi);
                        }
                        db = 0;
                    }
                }
            }
            
            /*for (int i = 0; i < kovetkezo.Count; i++)
            {
                Console.WriteLine(kovetkezo[i].x + " " + kovetkezo[i].y);
            }
            Console.WriteLine("---------------");*/

            
        }        

        public static void KovetkezoAllapot ()
        {
            int x = mezok.GetLength(0);
            int y = mezok.GetLength(1);
            int db = 0;
            for (int i = 1; i < x - 1; i++)
            {
                for (int j = 1; j < y - 1; j++)
                {
                    if (mezok[i, j] == "x")
                    {
                        
                        if (mezok[i + 1, j] == "x")
                            db++;
                        if (mezok[i - 1, j] == "x")
                            db++;
                        if (mezok[i, j + 1] == "x")
                            db++;
                        if (mezok[i, j - 1] == "x")
                            db++;
                        //átlók
                        if (mezok[i + 1, j + 1] == "x")
                            db++;
                        if (mezok[i - 1, j + 1] == "x")
                            db++;
                        if (mezok[i - 1, j - 1] == "x")
                            db++;
                        if (mezok[i + 1, j - 1] == "x")
                            db++;
                        if (db == 2 || db == 3)
                        {
                            Nyuszi nyuszi = new Nyuszi(i, j);
                            kovetkezo.Add(nyuszi);
                        }
                        db = 0;
                    }
                }
            }

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (mezok[i, j] == "x")
                    {
                        //sarkok
                        //balfelső
                        if (i == 0 && j == 0)
                        {
                            if (mezok[i, y - 1] == "x")
                                db++;
                            if (mezok[x - 1, j] == "x")
                                db++;
                            if (mezok[i + 1, j] == "x")
                                db++;
                            if (mezok[i, j + 1] == "x")
                                db++;
                            //átlók
                            if (mezok[i + 1, j + 1] == "x")
                                db++;
                            if (mezok[x - 1, y - 1] == "x")
                                db++;

                        }
                        //balalsó
                        if (i == x - 1 && j == 0)
                        {
                            if (mezok[i, y - 1] == "x")
                                db++;
                            if (mezok[0, j] == "x")
                                db++;
                            if (mezok[i - 1, j] == "x")
                                db++;
                            if (mezok[i, j + 1] == "x")
                                db++;
                            //átlók
                            if (mezok[0, y - 1] == "x")
                                db++;
                            if (mezok[i - 1, j + 1] == "x")
                                db++;
                        }
                        //jobbfelső
                        if (i == 0 && j == y - 1)
                        {
                            if (mezok[i, 0] == "x")
                                db++;
                            if (mezok[x - 1, j] == "x")
                                db++;
                            if (mezok[i + 1, j] == "x")
                                db++;
                            if (mezok[i, j - 1] == "x")
                                db++;
                            //átlók
                            if (mezok[x - 1, 0] == "x")
                                db++;
                            if (mezok[i + 1, j - 1] == "x")
                                db++;

                        }
                        //jobbalsó
                        if (i == x - 1 && j == y - 1)
                        {
                            if (mezok[0, y - 1] == "x")
                                db++;
                            if (mezok[i, 0] == "x")
                                db++;
                            if (mezok[i - 1, j] == "x")
                                db++;
                            if (mezok[i, j - 1] == "x")
                                db++;
                            //átlók
                            if (mezok[0, 0] == "x")
                                db++;
                            if (mezok[i - 1, j - 1] == "x")
                                db++;
                        }


                        //szélek

                        //teteje
                        if (i == 0 && j > 0 && j < y - 1)
                        {
                            if (mezok[x - 1, j] == "x")
                                db++;
                            if (mezok[i + 1, j] == "x")
                                db++;
                            if (mezok[i, j + 1] == "x")
                                db++;
                            if (mezok[i, j - 1] == "x")
                                db++;
                            //átlók
                            if (mezok[x - 1, j + 1] == "x")
                                db++;
                            if (mezok[x - 1, j - 1] == "x")
                                db++;
                            if (mezok[i + 1, j + 1] == "x")
                                db++;
                            if (mezok[i + 1, j - 1] == "x")
                                db++;
                        }
                        //alja
                        if (i == x - 1 && j > 0 && j < y - 1)
                        {
                            if (mezok[0, j] == "x")
                                db++;
                            if (mezok[i - 1, j] == "x")
                                db++;
                            if (mezok[i, j + 1] == "x")
                                db++;
                            if (mezok[i, j - 1] == "x")
                                db++;
                            //átlók
                            if (mezok[0, j + 1] == "x")
                                db++;
                            if (mezok[0, j - 1] == "x")
                                db++;
                            if (mezok[i - 1, j + 1] == "x")
                                db++;
                            if (mezok[i - 1, j - 1] == "x")
                                db++;
                        }
                        //baloldal
                        if (j == 0 && i > 0 && i < x - 1)
                        {
                            if (mezok[i, y - 1] == "x")
                                db++;
                            if (mezok[i + 1, j] == "x")
                                db++;
                            if (mezok[i - 1, j] == "x")
                                db++;
                            if (mezok[i, j + 1] == "x")
                                db++;
                            //átlók
                            if (mezok[i + 1, j + 1] == "x")
                                db++;
                            if (mezok[i - 1, j + 1] == "x")
                                db++;
                            if (mezok[i + 1, y - 1] == "x")
                                db++;
                            if (mezok[i - 1, y - 1] == "x")
                                db++;
                        }
                        //jobboldal
                        if (j == y - 1 && i > 0 && i < x - 1)
                        {
                            if (mezok[i, 0] == "x")
                                db++;
                            if (mezok[i + 1, j] == "x")
                                db++;
                            if (mezok[i - 1, j] == "x")
                                db++;
                            if (mezok[i, j - 1] == "x")
                                db++;
                            //átlók
                            if (mezok[i + 1, j - 1] == "x")
                                db++;
                            if (mezok[i - 1, j - 1] == "x")
                                db++;
                            if (mezok[i + 1, 0] == "x")
                                db++;
                            if (mezok[i - 1, 0] == "x")
                                db++;
                        }
                        if (db == 3 || db == 2)
                        {
                            Nyuszi nyuszi = new Nyuszi(i, j);
                            kovetkezo.Add(nyuszi);
                        }
                        db = 0;
                    }
                }
            }

            /*List<Nyuszi> sortedlist = kovetkezo.OrderBy(o => o.x).ToList();
            for (int i = 0; i < sortedlist.Count; i++)
            {
                Console.WriteLine(sortedlist[i].x + " " + sortedlist[i].y);
            }*/
            Szuletes();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    mezok[i, j] = " ";
                }
            }

            for (int i = 0; i < kovetkezo.Count; i++)
            {
                mezok[kovetkezo[i].x, kovetkezo[i].y] = "x";
            }
            
            Kiir();
            kovetkezo.Clear();
            
        }
    }
}
