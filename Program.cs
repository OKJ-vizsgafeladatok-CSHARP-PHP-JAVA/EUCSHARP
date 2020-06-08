using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EUCSHARP
{
    class Tagallam
    {
        public string orszag { get; set; }
        public DateTime csatlakozas { get; set; }

        public Tagallam(string orszag, DateTime csatlakozas)
        {
            this.orszag = orszag;
            this.csatlakozas = csatlakozas;
        }
    }

    
    class Program
    {
        public static List<Tagallam> lista = beolvas();

        public static List<Tagallam> beolvas()
        {
            List<Tagallam> list = new List<Tagallam>();
            try
            {
                using (StreamReader sr=new StreamReader(new FileStream("EUcsatlakozas.txt",FileMode.Open),Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        var split = sr.ReadLine().Split(';');
                        Tagallam o = new Tagallam(split[0],Convert.ToDateTime(split[1]));
                        list.Add(o);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Hiba a beolvasásnál. "+e.Message);
            }

            return list;
        }

        static void Main(string[] args)
        {
            #region 3. feladat
            Console.WriteLine("3. feladat: EU tagállamainak száma: "+lista.Count()+" db");
            #endregion

            #region 4. feladat
            var db = lista.Where(x => x.csatlakozas.Year == 2007).Count();
            Console.WriteLine("4. feladat: 2007-ben "+db+" ország csatlakozott");
            #endregion

            #region 5. feladat
            var date = lista.Where(x=>x.orszag.Equals("Magyarország")).First().csatlakozas.ToString("yyyy.MM.dd");
            Console.WriteLine("5. feladat: Magyarország csatlakozásának dátuma: "+date);
            #endregion

            #region 6. feladat
            var valasz = "Májusban nem volt csatlakozás!";
            var volte = lista.Any(x=>x.csatlakozas.Month==05);
            if (volte)
            {
                valasz = "Májusban volt csatlakozás!";
            }
            Console.WriteLine("6. feladat: "+valasz);
            #endregion

            #region 7. feladat
            var legu = lista.OrderByDescending(x=>x.csatlakozas).First();
            Console.WriteLine("7. feladat: Legutoljára csatlakozott ország: "+legu.orszag);
            #endregion

            #region 8. feladat
            Console.WriteLine("8. feladat: Statisztika");
            var stat = lista
                .GroupBy(x => x.csatlakozas.Year)
                .Select(y=>new { 
                    ev=y.Key,
                    db=y.Count()
                })
                .OrderBy(z=>z.ev)
                .ToList();
            stat.ForEach(x => Console.WriteLine( $"\t{x.ev} - {x.db} ország"));
            #endregion

            Console.ReadKey();
        }
    }
}
