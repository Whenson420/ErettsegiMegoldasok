using System.Xml;

namespace rgb
{
    internal class Program
    {
        struct RGB
        {
            public int R;
            public int G;
            public int B;
        }
        static RGB[][] rgb = new RGB[360][];
        static void Beolvas()
        {
            var Be = new StreamReader(@".\kep.txt");
            for (int i = 0; i < 360; i++)
            {
                string seged = Be.ReadLine();
                RGB[] seg = new RGB[640];
                string[] sor = seged.Split(' ');
                for (int j = 0; j < 640; j++)
                {
                    int kezdo = j == 0 ? 0 : j * 3;
                    seg[j].R = int.Parse(sor[kezdo]);
                    seg[j].G = int.Parse(sor[kezdo + 1]);
                    seg[j].B = int.Parse(sor[kezdo + 2]);
                }
                rgb[i] = seg;
            }
            Be.Close();
        }
        static int Világos()
        {
            int db = 0;
            foreach (RGB[] i in rgb)
            {
                foreach (RGB j in i)
                {
                    if(j.R+j.G+j.B>600)
                        db++;
                }
            }
            return db;
        }
        static void Sotet()
        {
            List<RGB> eredmeny = new List<RGB>();
            RGB seged = new RGB();
            seged.R = 255;
            seged.G = 255;
            seged.B = 255;
            eredmeny.Add(seged);
            foreach (RGB[] i in rgb)
            {
                foreach (RGB j in i)
                {
                    RGB utolsoE = eredmeny.Last();
                    if (j.R + j.G + j.B == utolsoE.R + utolsoE.G + utolsoE.B)
                    {
                        eredmeny.Add(j);
                    }
                    else if (j.R + j.G + j.B < utolsoE.R + utolsoE.G + utolsoE.B)
                    {
                        eredmeny.Clear();
                        eredmeny.Add(j);
                    }
                }
            }
            Console.WriteLine($"A legsötétebb pont RGB összege: {eredmeny[0].R+ eredmeny[0].G + eredmeny[0].B }\nA legsötétebb pixelek színe:");
            foreach (RGB j in eredmeny)
            {
                Console.WriteLine($"RGB({j.R},{j.G},{j.B})");
            }   
        }
        static bool hatar(int sorSzama, int elteresHatar)
        {
            for (int i = 1; i < 640; i++)
            {
                if (Math.Abs(rgb[sorSzama][i].B - rgb[sorSzama][i - 1].B) > elteresHatar)
                    return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            Beolvas();
            Console.Write("2. feladat:\nKérem egy képpont adatait!\nSor:");
            int x = int.Parse(Console.ReadLine());
            Console.Write("Oszlop:");
            int y = int.Parse(Console.ReadLine());
            Console.WriteLine($"A képpont színe RGB({rgb[x][y].R},{rgb[x][y].G},{rgb[x][y].B})");
            Console.WriteLine("3. feladat:\nA világos képpontok száma: " + Világos());
            Console.WriteLine("4.feladat:");
            Sotet();
            int utolso = 0;
            int elso = 0;
            for (int i = 0; i < 360; i++)
            {
                if (elso == 0 && hatar(i, 10))
                {
                    elso = i;
                }
                else if (hatar(i, 10))
                {
                    utolso = i;
                }
            }
            Console.WriteLine($"6. feladat:\nA felhő legfelső sora: {elso+1}\nA felhő legalsó sora: {utolso+1} \n");
        }
    }
}