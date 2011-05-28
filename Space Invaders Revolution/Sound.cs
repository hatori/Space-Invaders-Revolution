using System;
using System.IO;
using System.Windows.Forms;
using SlimDX;
using SlimDX.DirectSound;
using SlimDX.Multimedia;
using Ionic.Zip;

namespace Space_Invaders_Revolution
{
    class Sound
    {
        #region variables
        public Form form1_reference;

        private Video video_reference;
        private DirectSound device;
        private WaveFormat wave;
        private SoundBufferDescription buf_desc;
        private MemoryStream stream;
        private byte[] sound_data;
        private bool critical_failure;

        private SecondarySoundBuffer[] sound_buffers;
        private WaveStream[] sound_files;
        private bool[] sound_statuses;
        #endregion

        #region constructor
        public Sound(Form _form1_reference, Video _video_reference)
        {
            try
            {
                form1_reference = _form1_reference;
                video_reference = _video_reference;
                critical_failure = false;

                init_directsound();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString(), "Error!", System.Windows.Forms.MessageBoxButtons.OK);
                return;
            }
        }
        #endregion

        #region init directsound
        private void init_directsound()
        {
            try
            {
                device = new DirectSound(DirectSoundGuid.DefaultPlaybackDevice);
                device.SetCooperativeLevel(((Video)(video_reference)).Screen_Handle, CooperativeLevel.Normal);

                sound_buffers = new SecondarySoundBuffer[10];
                sound_files = new WaveStream[10];
                sound_statuses = new bool[10];

                load_sound_file(ref stream, ref sound_files[0], ref sound_data, ref buf_desc, ref sound_buffers[0], "Ufo.wav", ref sound_statuses[0]);
                load_sound_file(ref stream, ref sound_files[1], ref sound_data, ref buf_desc, ref sound_buffers[1], "Shot.wav", ref sound_statuses[1]);
                load_sound_file(ref stream, ref sound_files[2], ref sound_data, ref buf_desc, ref sound_buffers[2], "BaseHit.wav", ref sound_statuses[2]);
                load_sound_file(ref stream, ref sound_files[3], ref sound_data, ref buf_desc, ref sound_buffers[3], "InvHit.wav", ref sound_statuses[3]);
                load_sound_file(ref stream, ref sound_files[4], ref sound_data, ref buf_desc, ref sound_buffers[4], "Walk1.wav", ref sound_statuses[4]);
                load_sound_file(ref stream, ref sound_files[5], ref sound_data, ref buf_desc, ref sound_buffers[5], "Walk2.wav", ref sound_statuses[5]);
                load_sound_file(ref stream, ref sound_files[6], ref sound_data, ref buf_desc, ref sound_buffers[6], "Walk3.wav", ref sound_statuses[6]);
                load_sound_file(ref stream, ref sound_files[7], ref sound_data, ref buf_desc, ref sound_buffers[7], "Walk4.wav", ref sound_statuses[7]);
                load_sound_file(ref stream, ref sound_files[8], ref sound_data, ref buf_desc, ref sound_buffers[8], "UfoHit.wav", ref sound_statuses[8]);
                load_sound_file(ref stream, ref sound_files[9], ref sound_data, ref buf_desc, ref sound_buffers[9], "ELife.wav", ref sound_statuses[9]);
            }
            catch
            {
                MessageBox.Show("A failure has been detected during DirectSound initialization, please contact the author for assistance.", "Error!", MessageBoxButtons.OK);
                critical_failure = true;
            }
        }
        #endregion

        #region uninit directsound
        public void uninit_directsound()
        {
            if (critical_failure == false)
            {
                for (int i = 0; i < sound_buffers.Length; i++)
                {
                    if (sound_buffers[i] != null)
                    {
                        sound_buffers[i].Dispose();
                        sound_buffers[i] = null;
                    }
                }

                if (device != null)
                {
                    device.Dispose();
                    device = null;
                }
            }
        }
        #endregion

        #region reinit directsound
        public void reinit_directsound()
        {
            if (critical_failure == false)
            {
                uninit_directsound();
                init_directsound();
            }
        }
        #endregion

        #region load sound file
        void load_sound_file(ref MemoryStream sound_stream, ref WaveStream wave_stream, ref byte[] data_array, ref SoundBufferDescription buf_desc, ref SecondarySoundBuffer buf, string file, ref bool executed)
        {
            try
            {
                if ((((Form1)(form1_reference)).retrieve_resources != null) && (((Form1)(form1_reference)).retrieve_resources.EntryFileNames.Contains(file)))
                {
                    sound_stream = new MemoryStream();
                    ((Form1)(form1_reference)).retrieve_resources.Extract(file, sound_stream);
                    data_array = new byte[Convert.ToInt32(sound_stream.Length)];
                    data_array = sound_stream.ToArray();
                    wave = new WaveFormat();
                    wave.FormatTag = WaveFormatTag.Pcm;
                    wave.BitsPerSample = (short)((data_array[35] << 8) | data_array[34]);
                    wave.BlockAlignment = (short)((data_array[33] << 8) | data_array[32]);
                    wave.Channels = (short)((data_array[23] << 8) | data_array[22]);
                    wave.SamplesPerSecond = (int)((data_array[27] << 24) | (data_array[26] << 16) | (data_array[25] << 8) | (data_array[24]));
                    wave.AverageBytesPerSecond = (int)((data_array[27] << 24) | (data_array[26] << 16) | (data_array[25] << 8) | (data_array[24]));

                    sound_stream = new MemoryStream(data_array);
                    wave_stream = new WaveStream(sound_stream);
                    buf_desc = new SoundBufferDescription();
                    buf_desc.Flags = BufferFlags.GlobalFocus;
                    buf_desc.SizeInBytes = (int)sound_stream.Length;
                    buf_desc.Format = wave;
                    if (sound_stream.Length > 0)
                    {
                        buf = new SecondarySoundBuffer(device, buf_desc);
                        wave_stream.Read(data_array, 0, buf_desc.SizeInBytes);
                        buf.Write(data_array, 0, LockFlags.EntireBuffer);
                    }
                    executed = false;
                    sound_stream.Close();
                }
                else
                {
                    buf_desc = new SoundBufferDescription();
                    buf_desc.Flags = BufferFlags.GlobalFocus;
                    if (File.Exists(file))
                    {
                        wave_stream = new WaveStream(file);
                        buf_desc.Format = wave_stream.Format;
                        buf_desc.SizeInBytes = (int)wave_stream.Length;
                        data_array = new byte[wave_stream.Length];
                        buf = new SecondarySoundBuffer(device, buf_desc);
                        wave_stream.Read(data_array, 0, buf_desc.SizeInBytes);
                        buf.Write(data_array, 0, LockFlags.EntireBuffer);
                    }
                    executed = false;
                }
            }
            catch (DirectSoundException e)
            {
                MessageBox.Show(e.ToString(), "Error!", MessageBoxButtons.OK);
            }
        }
        #endregion

        #region play sound file
        public void play_sound(int sound_name)
        {
            if (sound_buffers[sound_name] != null)
            {

                sound_buffers[sound_name].Play(0, PlayFlags.None);
            }
        }
        #endregion

        #region sound statuses access
        public bool[] Sound_statuses
        {
            get
            {
                return sound_statuses;
            }

            set
            {
                sound_statuses = value;
            }
        }
        #endregion
    }
}
