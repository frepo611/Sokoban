using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLSerialisering;

namespace SokobanClasses
{
    internal class LevelTest
    {
         SokobanLevels test = ObjectXMLSerializer<SokobanLevels>.Load("compact.slc");

    }
}
