namespace jelado
{
    internal class Program
    {
        struct Jel
        {
            public int ora;
            public int perc;
            public int masodperc;
            public int x;
            public int y;
        }
        static List<Jel> Jelek = new List<Jel>();
        static void Beolvas()
        {
            var Be = new StreamReader(@".\jel.txt");
            while (!Be.EndOfStream)
            {
                string sor = Be.ReadLine();
                string[] seged = sor.Split(" ");
                Jel s = new Jel();
                s.ora = int.Parse(seged[0]);
                s.perc = int.Parse(seged[1]);
                s.masodperc = int.Parse(seged[2]);
                s.x = int.Parse(seged[3]);
                s.y = int.Parse(seged[4]);
                Jelek.Add(s);
            }
            Be.Close();

        }
        static double eltelt(int elsoO, int elsoP, int elsoM, int masodikO, int masodikP, int masodikM)
        {
            double elso = TimeSpan.Parse(elsoO.ToString() + ":" + elsoP.ToString() + ":" + elsoM.ToString()).TotalSeconds;
            double masodik = TimeSpan.Parse(masodikO.ToString() + ":" + masodikP.ToString() + ":" + masodikM.ToString()).TotalSeconds;
            return Math.Abs(elso - masodik);
        }
        static void Main(string[] args)
        {
            Beolvas();
            Console.WriteLine($"2. feladat\nAdja meg a jel sorszámát! ");
            int i = int.Parse(Console.ReadLine());
            Console.WriteLine($"x={Jelek[i - 1].x} y={Jelek[i - 1].y}");
            Jel elso = Jelek.First();
            Jel masodik = Jelek.Last();
            TimeSpan Idotartam = TimeSpan.FromSeconds(eltelt(elso.ora, elso.perc, elso.masodperc, masodik.ora, masodik.perc, masodik.masodperc));
            Console.WriteLine($"4. feladat\nIdőtartam: {Idotartam.Hours}:{Idotartam.Minutes}:{Idotartam.Seconds}");
            Console.WriteLine($"5. feladat\nBal alsó: {Jelek.Max().y} {Jelek.Min().x} , jobb felső: {Jelek.Min().y} {Jelek.Max().x} \n");
        }
    }
}