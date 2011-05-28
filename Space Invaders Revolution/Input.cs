using System;
using SlimDX;
using SlimDX.DirectInput;

namespace Space_Invaders_Revolution
{
    class Input
    {
        #region variables
        private DirectInput dinput;
        private Keyboard keyb;
        private Video _video;
        private KeyboardState key_pressed;
        private Key[] keys;
        private bool critical_failure;
        #endregion

        #region Constructor
        public Input(Video video)
        {
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

            /*try
            {
                keys[0] = Properties.Settings.Default.Add_Coin;
                keys[1] = Properties.Settings.Default.Player2_Start;
                keys[2] = Properties.Settings.Default.Player1_Start;
                keys[3] = Properties.Settings.Default.Player1_Fire;
                keys[4] = Properties.Settings.Default.Player1_Left;
                keys[5] = Properties.Settings.Default.Player1_Right;
                keys[6] = Properties.Settings.Default.Tilt_Button;
                keys[7] = Properties.Settings.Default.Player2_Fire;
                keys[8] = Properties.Settings.Default.Player2_Left;
                keys[9] = Properties.Settings.Default.Player2_Right;
            }
            catch
            {*/
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
            //}
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
