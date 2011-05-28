using System;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Space_Invaders_Revolution
{
    class Settings
    {
        #region variables
        private XmlDocument update_config;
        private XmlTextReader read_config;
        private XmlTextWriter write_config;

        private XPathDocument doc;
        private XPathNavigator nav;
        private XPathExpression exp;
        XPathNodeIterator iter;
        #endregion

        #region constructor
        public Settings()
        {
            load_config();
        }
        #endregion

        #region write config setting
        void write_config_setting(string section, string name, string value)
        {
            //read_config = new XmlTextReader("configuration.xml");
            doc = new XPathDocument("configuration.xml");
            nav = doc.CreateNavigator();

            exp = nav.Compile("/SPACE_INVADERS_REVOLUTION_CONFIGURATION/" + section + "/" + name);
            iter = nav.Select(exp);
        }
        #endregion

        #region write default config
        void write_default_config()
        {
            write_config = new XmlTextWriter("configuration.xml", null);
            write_config.Formatting = Formatting.Indented;

            write_config.WriteStartDocument();
            write_config.WriteStartElement("SPACE_INVADERS_REVOLUTION_CONFIGURATION");
            write_config.WriteStartElement("Game");
            write_config.WriteElementString("Coin_Info", "on");
            write_config.WriteElementString("Bonus_Life", "1500");
            write_config.WriteElementString("Starting_Lives", "3");
            write_config.WriteEndElement();
            /*write_config.WriteStartElement("Input");
            write_config.WriteElementString("input0", ((uint)Key.NumPad0).ToString());
            write_config.WriteElementString("input1", ((uint)Key.NumPad1).ToString());
            write_config.WriteElementString("input2", ((uint)Key.NumPad2).ToString());
            write_config.WriteElementString("input3", ((uint)Key.NumPad3).ToString());
            write_config.WriteElementString("input4", ((uint)Key.NumPad4).ToString());
            write_config.WriteElementString("input5", ((uint)Key.NumPad5).ToString());
            write_config.WriteElementString("input6", ((uint)Key.NumPad6).ToString());
            write_config.WriteElementString("input7", ((uint)Key.NumPad7).ToString());
            write_config.WriteElementString("input8", ((uint)Key.NumPad8).ToString());
            write_config.WriteElementString("input9", ((uint)Key.NumPad9).ToString());
            write_config.WriteElementString("input10", ((uint)Key.NumPadSlash).ToString());
            write_config.WriteElementString("input11", ((uint)Key.NumPadStar).ToString());
            write_config.WriteElementString("input12", ((uint)Key.NumPadMinus).ToString());
            write_config.WriteElementString("input13", ((uint)Key.NumPadPlus).ToString());
            write_config.WriteElementString("input14", ((uint)Key.NumPadEnter).ToString());
            write_config.WriteElementString("input15", ((uint)Key.NumPadPeriod).ToString());
            write_config.WriteEndElement();*/
            write_config.WriteStartElement("Video");
            write_config.WriteElementString("color0", ((uint)Color.Red.ToArgb()).ToString());
            write_config.WriteElementString("color1", ((uint)Color.Green.ToArgb()).ToString());
            write_config.WriteElementString("color2", ((uint)Color.Green.ToArgb()).ToString());
            write_config.WriteElementString("color3", ((uint)Color.White.ToArgb()).ToString());
            write_config.WriteElementString("color4", ((uint)Color.Black.ToArgb()).ToString());
            write_config.WriteEndElement();
            write_config.WriteEndElement();
            write_config.WriteEndDocument();
            write_config.Flush();
            write_config.Close();

            write_config_setting("Game", "Coin_Info", "on");
        }
        #endregion

        #region load config
        void load_config()
        {
            if (!File.Exists("configuration.xml"))
            {
                write_default_config();

                read_config = new XmlTextReader("configuration.xml");
            }
            else
            {
                read_config = new XmlTextReader("configuration.xml");
                read_config.ReadStartElement("Game");
            }
        }
        #endregion
    }
}
