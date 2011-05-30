using System;
using SlimDX;
using SlimDX.DirectInput;

namespace Space_Invaders_Revolution
{
    class Input
    {
        #region variables
        private System.Windows.Forms.Form _form1_reference;

        private DirectInput dinput;
        private Keyboard keyb;
        private Video _video;
        private KeyboardState key_pressed;
        private Key[] keys;
        private bool critical_failure;
        #endregion

        #region Constructor
        public Input(System.Windows.Forms.Form form1_reference, Video video)
        {
            _form1_reference = form1_reference;
            _video = video;
            critical_failure = false;

            Initialize_Keyboard();
        }
        #endregion

        #region initialize keyboard
        private void Initialize_Keyboard()
        {
            try
            {
                dinput = new DirectInput();
                keyb = new Keyboard(dinput);
                keyb.SetCooperativeLevel(((Video)_video).Screen_Handle, CooperativeLevel.Background | CooperativeLevel.Nonexclusive);
                keyb.Acquire();
                setup_keys();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("A failure has been detected during DirectInput initialization, please contact the author for assistance.", "Error!", System.Windows.Forms.MessageBoxButtons.OK);
                critical_failure = true;
            }
        }
        #endregion

        #region uninitialize keyboard
        public void Uninitialize_Keyboard()
        {
            if (critical_failure == false)
            {
                if (dinput != null)
                {
                    dinput.Dispose();
                    dinput = null;
                }

                if (keyb != null)
                {
                    keyb.Dispose();
                    keyb = null;
                }
            }
        }
        #endregion

        #region reinitialize keyboard
        public void Reinitialize_Keyboard()
        {
            if (critical_failure == false)
            {
                try
                {
                    Uninitialize_Keyboard();
                    dinput = new DirectInput();
                    keyb = new Keyboard(dinput);
                    keyb.SetCooperativeLevel(((Video)_video).Screen_Handle, CooperativeLevel.Background | CooperativeLevel.Nonexclusive);
                    keyb.Acquire();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.ToString(), "Error!", System.Windows.Forms.MessageBoxButtons.OK);
                    return;
                }
            }
        }
        #endregion

        #region setup keys
        private void setup_keys()
        {
            keys = new Key[10];

            try
            {
                keys[0] = (Key)(Convert.ToByte(((Form1)_form1_reference).main_settings.read_config_setting("Input", "key0")));
                keys[1] = (Key)(Convert.ToByte(((Form1)_form1_reference).main_settings.read_config_setting("Input", "key1"))); ;
                keys[2] = (Key)(Convert.ToByte(((Form1)_form1_reference).main_settings.read_config_setting("Input", "key2"))); ;
                keys[3] = (Key)(Convert.ToByte(((Form1)_form1_reference).main_settings.read_config_setting("Input", "key3"))); ;
                keys[4] = (Key)(Convert.ToByte(((Form1)_form1_reference).main_settings.read_config_setting("Input", "key4"))); ;
                keys[5] = (Key)(Convert.ToByte(((Form1)_form1_reference).main_settings.read_config_setting("Input", "key5"))); ;
                keys[6] = (Key)(Convert.ToByte(((Form1)_form1_reference).main_settings.read_config_setting("Input", "key6"))); ;
                keys[7] = (Key)(Convert.ToByte(((Form1)_form1_reference).main_settings.read_config_setting("Input", "key7"))); ;
                keys[8] = (Key)(Convert.ToByte(((Form1)_form1_reference).main_settings.read_config_setting("Input", "key8"))); ;
                keys[9] = (Key)(Convert.ToByte(((Form1)_form1_reference).main_settings.read_config_setting("Input", "key9"))); ;
            }
            catch
            {
                keys[0] = Key.Return;
                keys[1] = Key.D2;
                keys[2] = Key.D1;
                keys[3] = Key.Space;
                keys[4] = Key.A;
                keys[5] = Key.D;
                keys[6] = Key.T;
                keys[7] = Key.NumberPad0;
                keys[8] = Key.LeftArrow;
                keys[9] = Key.RightArrow;
            }
        }
        #endregion

        #region retrieve keyboardstate
        public KeyboardState retrieve_keyboardstate()
        {
            return keyb.GetCurrentState();
        }
        #endregion

        #region retrieve keys
        public Key[] Keys
        {
            get
            {
                return keys;
            }
        }
        #endregion

        #region keyboardstate
        public KeyboardState Key_Pressed
        {
            get
            {
                return key_pressed;
            }

            set
            {
                key_pressed = value;
            }
        }
        #endregion
    }
}
