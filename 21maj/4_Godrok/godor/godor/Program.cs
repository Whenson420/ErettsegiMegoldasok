namespace godor
{
    internal class Program
    {
        static List<int> Melyseg = new List<int>();
        static void Beolvas()
        {
            var Be = new StreamReader(@".\melyseg.txt");
            while (!Be.EndOfStream)
            {
                Melyseg.Add(int.Parse(Be.ReadLine()));
            }
            Be.Close();
        }
        static double HSz()
        {
            double erintetlen = 0;
            foreach (int i in Melyseg)
            {
                if(i == 0)
                    erintetlen++;
            }
            return Math.Round(100 * erintetlen / Melyseg.Count,2);
        }
        static void Godrok()
        {
            var Beir = new StreamWriter(new FileStream(@".\godrok.txt",FileMode.Create,FileAccess.Write),System.Text.Encoding.UTF8);
            string seged = string.Empty;
            bool volt = false;
            foreach (int i in Melyseg)
            {
                if (i != 0)
                {
                    seged = seged + i;
                    volt = true;
                }
                else if(volt)
                {
                    volt = false;
                    Beir.WriteLine(seged);
                    seged = string.Empty;
                }
            }
            Beir.Close();
        }
        static int GodrokSz()
        {
            int GodrokDB = 0;
            for (int i = 1; i < Melyseg.Count; i++)
            {
                if (Melyseg[i] == 0 && Melyseg[i-1] != 0)
                    GodrokDB++;
            }
            return GodrokDB;
        }
        static int GodorKV(int Opc,int Godor)
        {
            int M = 0;
            if (Opc == 0)
            {
                for (int i = Godor; Melyseg[i] != 0; i--)
                {
                    M = i;
                }
            }
            if (Opc == 1)
            {
                for (int i = Godor; Melyseg[i] != 0; i++)
                {
                    M = i;
                }
            }
            return M;
        }
        static string FolyamatosM(int Godor)
        {
            for (int i = 0; Melyseg[Godor - i]!=0 || Melyseg[Godor + i] != 0; i++)
            {
                if (Melyseg[Godor - i] != Melyseg[Godor + i])
                {
                    return "Nem mélyül folyamatosan";
                } 
            }
            return "Folyamatosan mélyül.";
        }
        static int legnagyobbM(int Godor)
        {
            int legnagyobb = 0;
            int kezdes = GodorKV(0, Godor);
            int vege = GodorKV(1, Godor);
            for (int i = kezdes; i < vege; i++)
            {
                if (Melyseg[i] > Melyseg[legnagyobb])
                {
                    legnagyobb = i;
                }
            }
            return Melyseg[legnagyobb];
        }
        static int Terfogata(int Godor)
        {
            int terfOssz = 0;
            int kezdes = GodorKV(0, Godor);
            int vege = GodorKV(1, Godor);
            for (int i = kezdes; i <= vege; i++)
            {
                terfOssz += Melyseg[i] * 10;
            }
            return terfOssz;
        }
        static int vizM(int Godor)
        {
            int terfOssz = 0;
            int kezdes = GodorKV(0, Godor);
            int vege = GodorKV(1, Godor);
            for (int i = kezdes; i <= vege; i++)
            {
                terfOssz += (Melyseg[i]-1) * 10;
            }
            return terfOssz;
        }
        static void Main(string[] args)
        {
            Beolvas();
            Console.WriteLine("1. feladat\nA fájl adatainak száma: " + Melyseg.Count);
            Console.Write("2. feladat\nAdjon meg egy távolságértéket! ");
            int seged = int.Parse(Console.ReadLine());
            Console.WriteLine($"Ezen a helyen a felszín {Melyseg[seged-1]} méter mélyen van. ");
            Console.WriteLine($"3. feladat\r\nAz érintetlen terület aránya {HSz()}%. ");
            Godrok();
            Console.WriteLine("5. feladat\r\nA gödrök száma: " + GodrokSz());
            Console.WriteLine($"6. feladat\na)\nA gödör kezdete: {GodorKV(0,seged)+1} méter, a gödör vége: {GodorKV(1,seged)+1} méter. b)\n{FolyamatosM(seged)}\nc)\nA legnagyobb mélysége {legnagyobbM(seged)} méter.\nd)\nA térfogata {Terfogata(seged)} m^3. \n e)\r\nA vízmennyiség {vizM(seged)} m^3. ");
        }
    }
}