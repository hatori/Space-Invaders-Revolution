using System.IO;
using System.Windows.Forms;

namespace Space_Invaders_Revolution
{
    class Memory
    {
        #region variables
        private Form form1_reference;

        private byte[] memory;
        private byte[] temp_storage;
        private FileStream read;
        private MemoryStream rom;
        private Video video_reference;
        #endregion

        #region constructor
        public Memory(Form _form1_reference, Video _video_reference)
        {
            form1_reference = _form1_reference;
            video_reference = _video_reference;

            memory = new byte[0x41B9];
            temp_storage = new byte[0x800];

            Initialize_Memory();
            Load_Rom();
        }
        #endregion

        #region initialize memory
        public void Initialize_Memory()
        {
            for (int i = 0; i < memory.Length; i++)
            {
                memory[i] = 0;
            }
        }
        #endregion

        #region reinit memory
        public void reinit_memory()
        {
            for (int i = 0x2000; i < 0x4000; i++)
            {
                memory[i] = 0;
            }
        }
        #endregion

        #region load rom
        private void Load_Rom()
        {
            if ((!Load_File("invaders.h", 0)) || (!Load_File("invaders.g", 1)) ||
            (!Load_File("invaders.f", 2)) || (!Load_File("invaders.e", 3)))
            {
                System.Windows.Forms.MessageBox.Show("An error loading the Space Invaders rom has been detected.\nSpace Invaders emulator will not run until it is fixed.", "Error!", System.Windows.Forms.MessageBoxButtons.OK);
                ((Video)(video_reference)).Fatal_error = true;
            }
        }
        #endregion

        #region load file
        private bool Load_File(string file, int file_num)
        {
            if ((((Form1)(form1_reference)).retrieve_resources != null) && (((Form1)(form1_reference)).retrieve_resources.EntryFileNames.Contains(file)))
            {
                rom = new MemoryStream();
                ((Form1)(form1_reference)).retrieve_resources.Extract(file, rom);
                temp_storage = rom.ToArray();

                for (int i = (file_num * (int)rom.Length); i < ((int)rom.Length * (file_num + 1)); i++)
                {
                    memory[i] = temp_storage[i % rom.Length];
                }
                rom.Close();
                return true;
            }
            else if (File.Exists(file))
            {
                read = new FileStream(file, FileMode.Open, FileAccess.Read);

                for (int i = (file_num * (int)read.Length); i < ((int)read.Length * (file_num + 1)); i++)
                {
                    memory[i] = (byte)read.ReadByte();
                }
                read.Close();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region read byte
        public byte read_byte(ushort address)
        {
            if (address >= 0x0 && address < memory.Length)
            {
                return memory[address];
            }
            else
            {
                return 0x0;
            }
        }
        #endregion

        #region read ushort
        public ushort read_ushort(ushort address)
        {
            if (address >= 0x0 && address < (memory.Length - 1))
            {
                return (ushort)((memory[(ushort)(address + 1)] << 8) | memory[(ushort)(address)]);
            }
            else
            {
                return 0x0;
            }
        }
        #endregion

        #region write byte
        public void write_byte(ushort address, byte data)
        {
            if (address >= 0x0 && address < memory.Length)
            {
                memory[address] = data;
            }
        }
        #endregion

        #region write ushort
        public void write_ushort(ushort address, ushort data)
        {
            if (address >= 0x0 && address < (memory.Length - 1))
            {
                memory[(ushort)(address)] = (byte)(data & 0xFF);
                memory[(ushort)(address + 1)] = (byte)((data >> 8) & 0xFF);
            }
        }
        #endregion

        #region memory access
        public byte[] Memory_Access
        {
            get
            {
                return memory;
            }

            set
            {
                memory = value;
            }
        }
        #endregion
    }
}
