using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace Nyulak_beadandó
{
    public class Program
    {
        static void Main(string[] args)
        {
            //ha 300 nyulat generálok egy 20x20as mezőn vagy túlzsufolt a mezőm ,akkor kb 2. állapotnál mind meghalnak,úgy vettem észre hogy 300 nyúlhoz egy 25x25ös minimum kellene,
            //szóval olyan 100-200al nagyobb legyen a mező.
            Menu.fomenu();
            Console.ReadKey();
        }
    }
}
