using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using XMLSerialisering;

namespace SokobanClasses
{
    public class SokobanLevels
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }

        public LevelCollection LevelCollection { get; set; }
    }

    public class LevelCollection
    {
        [XmlAttribute]
        public string Copyright { get; set; }
        [XmlAttribute] public string MaxWidth { get; set; }
        [XmlAttribute] public string MaxHeight { get; set; }
        [XmlElement("Level")]
        public List<XMLLevel> Levels { get; set; }
    }
    public class XMLLevel
    {
        [XmlAttribute]
        public string Id { get; set; }
        [XmlAttribute]
        public string Width{ get; set; }
        [XmlAttribute]
        public int Height { get; set; }
        [XmlElement("L")]
        public List<string> Lines { get; set; }
    }
}
