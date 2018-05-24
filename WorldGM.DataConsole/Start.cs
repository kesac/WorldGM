using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WorldGM.DataConsole
{
    public class Start
    {

        public static void Main(string[] args)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(@"E:\Projects\C#\WorldGM\WorldGM.DataConsole\World.xml");

            var root = xml.FirstChild;

            using(var db = new AppContext())
            {
                if(root.Name == "world")
                {
                    var worldName = root.Attributes["name"].Value;
                    var world = db.EnsureWorldExists(worldName);
                }
            }
        }
    }
}
