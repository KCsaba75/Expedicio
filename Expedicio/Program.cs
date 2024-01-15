using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedicio
{
    class Program
    {
        // Struktúra a nap, radioamator és uzenet adatainak tarolasara
        struct UzenetAdatok
        {
            public int NapSzam;
            public int RadioAmator;
            public string Uzenet;
        }

        static void Main(string[] args)
        {
            // Segedvaltozo a strukturahoz

            UzenetAdatok seged;

            // Lista az UzenetAdatok strukturaval adataink tarolasara

            List<UzenetAdatok> uzenetek = new List<UzenetAdatok>();
            string sor;

            // Fajl olvasas
            using (StreamReader olvaso = new StreamReader("veetel.txt"))
            {
                // Amig nem ertunk a fajl veget
                while ((sor = olvaso.ReadLine()) != null)
                {
                    // Sor feldolgozasa es hozzaadas a listahoz
                    seged.NapSzam = Convert.ToInt32(sor.Split(' ')[0]);
                    seged.RadioAmator = Convert.ToInt32(sor.Split(' ')[1]);
                    seged.Uzenet = olvaso.ReadLine();
                    uzenetek.Add(seged);
                }
            }

            // 2. feladat: Elso es utolso uzenet rögzítője
            Console.WriteLine("2. feladat: ");
            Console.WriteLine("Az elso uzenet rögzitője: {0}", uzenetek[0].RadioAmator);
            Console.WriteLine("Az utolso uzenet rögzitője: {0}", uzenetek[uzenetek.Count - 1].RadioAmator);

            // 3. feladat: Farkas szot tartalmazó uzenetek
            Console.WriteLine("\n3. feladat:");
            foreach (var uzenet in uzenetek)
            {
                if (uzenet.Uzenet.Contains("farkas"))
                {
                    Console.WriteLine("{0}. nap {1}. radioamator", uzenet.NapSzam, uzenet.RadioAmator);
                }
            }

            // 4. feladat: Napokonkenti radioamatorok szamanak kiirasa
            Console.WriteLine("\n4. feladat:");
            int[] napokRadioAmator = new int[11];

            foreach (var uzenet in uzenetek)
            {
                napokRadioAmator[uzenet.NapSzam - 1]++;
            }

            for (int i = 0; i < napokRadioAmator.Length; i++)
            {
                Console.WriteLine("{0}. nap: {1} radioamator", i + 1, napokRadioAmator[i]);
            }

            // Uzenetek mátrixba rendezése es kiirasa fajlba
            char[,] uzenetMatrix = new char[11, 90];
            using (StreamWriter iro = new StreamWriter("adaas.txt"))
            {
                // Matrix inicializalasa
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < 90; j++)
                    {
                        uzenetMatrix[i, j] = '$';
                    }
                }

                // Uzenetek matrixba rendezese
                foreach (var uzenet in uzenetek)
                {
                    for (int j = 0; j < 90; j++)
                    {
                        if (uzenet.Uzenet[j] != '$' && uzenet.Uzenet[j] != '#')
                        {
                            uzenetMatrix[uzenet.NapSzam - 1, j] = uzenet.Uzenet[j];
                        }
                    }
                }

                // Matrix kiirasa fajlba
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < 90; j++)
                    {
                        iro.Write(uzenetMatrix[i, j]);
                    }
                    iro.Write('\n');
                }
            }

            // Varakozas egy billentyű lenyomasara
            Console.ReadKey();
        }
    }
}
        
 


    
