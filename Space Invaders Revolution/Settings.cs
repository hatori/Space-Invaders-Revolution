using System;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Space_Invaders_Revolution
{
    public class Settings
    {
        #region variables
        private string config;

        private XmlDocument update_config;
        private XmlTextWriter write_config;

        private XmlNode node;
        private XmlElement newelement;
        #endregion

        #region constructor
        public Settings()
        {
            load_config();
        }
        #endregion

        #region read config setting
        public string read_config_setting(string section, string name)
        {
            try
            {
                if (update_config == null)
                {
                    update_config = new XmlDocument();
                    update_config.Load(config);
                }

                node = update_config.SelectSingleNode("SPACE_INVADERS_REVOLUTION_CONFIGURATION/" + section + "/" + name);

                if (node != null)
                {
                    return node.InnerText;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return "";
            }
        }
        #endregion

        #region write config setting
        public void write_config_setting(string section, string name, string value)
        {
            try
            {
                if (update_config == null)
                {
                    if (!File.Exists(config))
                    {
                        write_config = new XmlTextWriter(config, null);
                        write_config.Formatting = Formatting.Indented;

                        write_config.WriteStartDocument();
                        write_config.WriteStartElement("SPACE_INVADERS_REVOLUTION_CONFIGURATION");
                        write_config.WriteEndElement();
                        write_config.Flush();
                        write_config.Close();
                    }

                    update_config = new XmlDocument();
                    update_config.Load(config);
                }

                node = update_config.SelectSingleNode("SPACE_INVADERS_REVOLUTION_CONFIGURATION/" + section + "/" + name);

                if (node != null)
                {
                    if (node.InnerText != value)
                    {
                        node.InnerText = value;
                        update_config.Save(config);
                    }
                }
                else
                {
                    node = update_config.SelectSingleNode("SPACE_INVADERS_REVOLUTION_CONFIGURATION/" + section);

                    if (node != null)
                    {
                        newelement = update_config.CreateElement(name);
                        newelement.InnerText = value;
                        node.AppendChild(newelement);
                        update_config.Save(config);
                    }
                    else
                    {
                        node = update_config.SelectSingleNode("SPACE_INVADERS_REVOLUTION_CONFIGURATION");

                        if (node != null)
                        {
                            newelement = update_config.CreateElement(section);
                            node.AppendChild(newelement);
                            update_config.Save(config);

                            write_config_setting(section, name, value);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        #region write default config
        private void write_default_config()
        {
            write_config_setting("Game", "Coin_Info", "on");
            write_config_setting("Game", "Bonus_Life", "1500");
            write_config_setting("Game", "Starting_Lives", "3");
            write_config_setting("Game", "Dip_Switch_Total", "0");
            write_config_setting("Game", "Window_Resolution", "224 x 256");
            write_config_setting("Video", "color0", ((int)Color.Red.ToArgb()).ToString());
            write_config_setting("Video", "color1", ((int)Color.Green.ToArgb()).ToString());
            write_config_setting("Video", "color2", ((int)Color.Green.ToArgb()).ToString());
            write_config_setting("Video", "color3", ((int)Color.White.ToArgb()).ToString());
            write_config_setting("Video", "color4", ((int)Color.Black.ToArgb()).ToString());
            write_config_setting("Input", "key0", SlimDX.DirectInput.Key.Return.ToString());
            write_config_setting("Input", "key1", SlimDX.DirectInput.Key.D2.ToString());
            write_config_setting("Input", "key2", SlimDX.DirectInput.Key.D1.ToString());
            write_config_setting("Input", "key3", SlimDX.DirectInput.Key.Space.ToString());
            write_config_setting("Input", "key4", SlimDX.DirectInput.Key.A.ToString());
            write_config_setting("Input", "key5", SlimDX.DirectInput.Key.D.ToString());
            write_config_setting("Input", "key6", SlimDX.DirectInput.Key.T.ToString());
            write_config_setting("Input", "key7", SlimDX.DirectInput.Key.NumberPad0.ToString());
            write_config_setting("Input", "key8", SlimDX.DirectInput.Key.LeftArrow.ToString());
            write_config_setting("Input", "key9", SlimDX.DirectInput.Key.RightArrow.ToString());
        }
        #endregion

        #region load config
        private void load_config()
        {
            config = "configuration.xml";

            if (!File.Exists(config))
            {
                write_default_config();
            }
        }
        #endregion
    }
}
