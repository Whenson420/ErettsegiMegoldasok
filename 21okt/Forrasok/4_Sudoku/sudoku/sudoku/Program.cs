namespace sudoku
{
    internal class Program
    {
        struct LehetsegesL
        {
            public int szam;
            public int sor;
            public int oszl;
        }
        static List<List<int>> Tabla = new List<List<int>>();
        static List<LehetsegesL> Lepes = new List<LehetsegesL>();
        static void Beolv(string Dir)
        {
            int i = 0;
            var Be = new StreamReader(File.Exists(Dir)? Dir : @".\konnyu.txt");
            while (!Be.EndOfStream)
            {
                string sor = Be.ReadLine();
                string[] tored = sor.Split(" ");
                if (i < 9)
                {
                    List<int> seged = new List<int>();
                    for (int j = 0; j < 9; j++)
                    {
                        seged.Add(int.Parse(tored[j]));
                    }
                    Tabla.Add(seged);
                    i++;
                }
                else
                {
                    LehetsegesL s = new LehetsegesL();
                    s.szam = int.Parse(tored[0]);
                    s.sor = int.Parse(tored[1]);
                    s.oszl = int.Parse(tored[2]);
                    Lepes.Add(s);
                }
            }
            Be.Close();
        }
        static int Resztabla(int s, int o)
        {
            int megoldas = 0;
            if (s < 3)
            {
                if (o < 3)
                {
                    megoldas = 1;
                }
                else if (o < 6)
                {
                    megoldas = 2;
                }
                else if (o < 9)
                {
                    megoldas = 3;
                }
            }
            else if (s < 6)
            {
                if (o < 3)
                {
                    megoldas = 4;
                }
                else if (o < 6)
                {
                    megoldas = 5;
                }
                else if (o < 9)
                {
                    megoldas = 6;
                }
            }
            else if (s < 9)
            {
                if (o < 3)
                {
                    megoldas = 7;
                }
                else if (o < 6)
                {
                    megoldas = 8;
                }
                else if (o < 9)
                {
                    megoldas = 9;
                }
            }
            return megoldas;
        }
        static double SzazalekUres()
        {
            double seged = 0;
            foreach (List<int> i in Tabla)
            {
                foreach (int j in i)
                {
                    if (j == 0)
                    {
                        seged++;
                    }
                }
            }
            return Math.Round(seged * 100 / 81,1);
        }
        static string KezdoIndexek(int s, int o)
        {
            int resztabla = Resztabla(s, o);
            int sor = 0;
            int oszlop = 0;
            bool folyt = true;
            for (int i = 0; i < 9 && folyt; i++)
            {
                for (int j = 0; j < 9 && folyt; j++)
                {
                    if (Resztabla(i, j) == resztabla)
                    {
                        sor = i;
                        oszlop = j;
                        folyt = false;
                    }
                }
            }
            return sor.ToString() + " " + oszlop.ToString();
        }
        static void LepesE()
        {
            foreach (LehetsegesL i in Lepes)
            {
                Console.WriteLine($"A kiválasztott sor: {i.sor} oszlop: {i.oszl} a szám: {i.szam}");
                bool megvan = false;
                if (Tabla[i.sor - 1][i.oszl - 1] != 0)
                {
                    Console.WriteLine("A helyet már kitöltötték");
                    megvan = true;
                }
                for (int x = 0; x < 9 && !megvan; x++)
                {
                    if (Tabla[i.oszl - 1][x] == i.szam)
                    {
                        Console.WriteLine("Az adott sorban már szerepel a szám");
                        megvan = true;
                    }
                    if (Tabla[x][i.sor - 1] == i.szam)
                    {
                        Console.WriteLine("Az adott oszlopban már szerepel a szám");
                        megvan = true;
                    }
                }
                int Ksor = int.Parse(KezdoIndexek(i.sor - 1, i.oszl - 1).Split(" ")[0]);
                int Koszlop = int.Parse(KezdoIndexek(i.sor - 1, i.oszl - 1).Split(" ")[1]);
                for (int x = Ksor; x < Ksor + 3 && !megvan; x++)
                {
                    for (int y = Koszlop; y < Koszlop + 3 && !megvan; y++)
                    {
                        if (Tabla[x][y] == i.szam)
                        {
                            Console.WriteLine("Az adott résztáblázatban már szerepel a szám");
                            megvan = true;
                        }
                    }
                }
                if (!megvan)
                {
                    Console.WriteLine("A lépés megtehető");
                }
            }
        }
        static void Main(string[] args)
        {
            Console.Write("1. feladat\nAdja meg a bemeneti fájl nevét! ");
            string file = Console.ReadLine();
            Console.Write("Adja meg egy sor számát! ");
            int sor = int.Parse(Console.ReadLine());
            Console.Write("Adja meg egy oszlop számát! ");
            int oszlop = int.Parse(Console.ReadLine());
            Beolv(file);
            Console.WriteLine($"3. feladat\nAz adott helyen szereplő szám: {(Tabla[sor - 1][oszlop - 1] != 0 ? Tabla[sor - 1][oszlop - 1] : "Az adott helyet még nem töltötték ki.")}\nA hely a(z) {Resztabla(sor-1,oszlop-1)} résztáblázathoz tartozik. ");
            Console.WriteLine($"4. feladat\nAz üres helyek aránya: {SzazalekUres()}% ");
            Console.WriteLine("5. feladat");
            LepesE();
        }
    }
}