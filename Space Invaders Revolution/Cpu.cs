using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Space_Invaders_Revolution
{
    class Cpu
    {
        #region variables
        private Memory _memory;
        private Video _video;
        private Sound _sound;
        private Input _input;

        private int width_offset = 0;
        private byte hold = 0;

        [StructLayout(LayoutKind.Explicit)]
        private struct Regs
        {
            [FieldOffset(0)]
            public byte F;

            [FieldOffset(1)]
            public byte A;

            [FieldOffset(2)]
            public byte C;

            [FieldOffset(3)]
            public byte B;

            [FieldOffset(4)]
            public byte E;

            [FieldOffset(5)]
            public byte D;

            [FieldOffset(6)]
            public byte L;

            [FieldOffset(7)]
            public byte H;

            [FieldOffset(0)]
            public ushort AF;

            [FieldOffset(2)]
            public ushort BC;

            [FieldOffset(4)]
            public ushort DE;

            [FieldOffset(6)]
            public ushort HL;
        }

        private Regs regs;
        private byte INTE;
        private ushort pc;
        private ushort sp;

        private byte opcode;
        private int cycles;
        private byte extra_cycles;
        private int frame;
        private byte[] parity_loopup;
        private byte[] cycles_lookup;

        private byte byte_temp1;
        private byte byte_temp2;
        private ushort ushort_temp1;
        private ushort ushort_temp2;

        private byte DipSwitch;
        private ushort shift_register;
        private byte shift_register_offset;
        private byte read_port1;
        private byte read_port2;
        #endregion

        #region constructor
        public Cpu(Form form1_reference, ref Memory memory, ref Video video, ref Sound sound, ref Input input)
        {
            _memory = memory;
            _video = video;
            _sound = sound;
            _input = input;
            cycles = 0;
            extra_cycles = 0;
            frame = 0;
            byte_temp1 = byte_temp2 = 0;
            ushort_temp1 = ushort_temp2 = 0;
            shift_register = 0;
            shift_register_offset = 0;

            regs.A = regs.B = regs.C = regs.D = regs.E = regs.H = regs.L = 0;
            regs.F = 6;
            pc = 0;
            sp = 0;
            opcode = 0;
            INTE = 0;
            try
            {
                DipSwitch = byte.Parse(((Form1)form1_reference).main_settings.read_config_setting("Game", "Dip_Switch_Total"), System.Globalization.NumberStyles.HexNumber);
            }
            catch
            {
                DipSwitch = 0;
            }

            parity_loopup = new byte[0x100];
            cycles_lookup = new byte[] {
	        4, 10,7, 5, 5, 5, 7, 4, 0, 10,7, 5, 5, 5, 7, 4,
	        0, 10,7, 5, 5, 5, 7, 4, 0, 10,7, 5, 5, 5, 7, 4,
	        0, 10,16,5, 5, 5, 7, 4, 0, 10,16,5, 5, 5, 7, 4,
	        0, 10,13,5, 10,10,10,4, 0, 10,13,5, 5, 5, 7, 4,
	        5, 5, 5, 5, 5, 5, 7, 5, 5, 5, 5, 5, 5, 5, 7, 5,
	        5, 5, 5, 5, 5, 5, 7, 5, 5, 5, 5, 5, 5, 5, 7, 5,
	        5, 5, 5, 5, 5, 5, 7, 5, 5, 5, 5, 5, 5, 5, 7, 5,
	        7, 7, 7, 7, 7, 7, 7, 7, 5, 5, 5, 5, 5, 5, 7, 5,
	        4, 4, 4, 4, 4, 4, 7, 4, 4, 4, 4, 4, 4, 4, 7, 4,
	        4, 4, 4, 4, 4, 4, 7, 4, 4, 4, 4, 4, 4, 4, 7, 4,
	        4, 4, 4, 4, 4, 4, 7, 4, 4, 4, 4, 4, 4, 4, 7, 4,
	        4, 4, 4, 4, 4, 4, 7, 4, 4, 4, 4, 4, 4, 4, 7, 4,
	        5, 10,10,10,11,11,7, 11,5, 10,10,0, 11,17,7, 11,
	        5, 10,10,10,11,11,7, 11,5, 0, 10,10,11,0, 7, 11,
	        5, 10,10,18,11,11,7, 11,5, 5, 10,4, 11,0, 7, 11,
	        5, 10,10,4, 11,11,7, 11,5, 5, 10,4, 11,0, 7, 11
            };

            for (int i = 0; i < parity_loopup.Length; i++)
            {
                parity_loopup[i] = (byte)(4 & (4 ^ (i << 2) ^ (i << 1) ^ i ^ (i >> 1) ^ (i >> 2) ^ (i >> 3) ^ (i >> 4) ^ (i >> 5)));
            }
        }
        #endregion

        #region select status check
        private bool select_status_check(string type)
        {
            bool result = false;

            switch (type)
            {
                case "JC":
                case "CC":
                case "RC":
                    {
                        if (check_carry_flag())
                        {
                            result = true;
                        }
                        break;
                    }
                case "JNC":
                case "CNC":
                case "RNC":
                    {
                        if (!check_carry_flag())
                        {
                            result = true;
                        }
                        break;
                    }
                case "JZ":
                case "CZ":
                case "RZ":
                    {

                        if (check_zero_flag())
                        {
                            result = true;
                        }
                        break;
                    }
                case "JNZ":
                case "CNZ":
                case "RNZ":
                    {

                        if (!check_zero_flag())
                        {
                            result = true;
                        }
                        break;
                    }
                case "JM":
                case "CM":
                case "RM":
                    {
                        if (check_sign_flag())
                        {
                            result = true;
                        }
                        break;
                    }
                case "JP":
                case "CP":
                case "RP":
                    {
                        if (!check_sign_flag())
                        {
                            result = true;
                        }
                        break;
                    }
                case "JPE":
                case "CPE":
                case "RPE":
                    {
                        if (check_parity_flag())
                        {
                            result = true;
                        }
                        break;
                    }
                case "JPO":
                case "CPO":
                case "RPO":
                    {
                        if (!check_parity_flag())
                        {
                            result = true;
                        }
                        break;
                    }
            }

            return result;
        }
        #endregion

        #region normal carry set
        private void normal_carry_flag_set(ushort result)
        {
            if (result > 0xFF)
            {
                if (!check_carry_flag())
                {
                    set_carry_flag(true);
                }
            }
            else if (check_carry_flag())
            {
                set_carry_flag(false);
            }
        }
        #endregion

        #region sub carry set
        private void sub_carry_flag_set(ushort result)
        {
            if (result < 0x100)
            {
                if (!check_carry_flag())
                {
                    set_carry_flag(true);
                }
            }
            else if (check_carry_flag())
            {
                set_carry_flag(false);
            }
        }
        #endregion

        #region dad carry set
        private void dad_carry_flag_set(ushort result)
        {
            if (result > 0xFFFF)
            {
                if (!check_carry_flag())
                {
                    set_carry_flag(true);
                }
            }
            else if (check_carry_flag())
            {
                set_carry_flag(false);
            }
        }
        #endregion

        #region set carry
        private void set_carry_flag(bool set)
        {
            if (set)
            {
                if (!check_carry_flag())
                {
                    regs.F ^= 0x1;
                }
            }
            else if (check_carry_flag())
            {
                regs.F ^= 0x1;
            }
        }
        #endregion

        #region check carry
        private bool check_carry_flag()
        {
            if ((regs.F & 0x1) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region auxiliary carry set
        private void auxiliary_carry_flag_set(byte result)
        {
            if ((regs.A & 0xF) > (result & 0xF))
            {
                if (!check_auxiliary_flag())
                {
                    set_auxiliary_carry_flag(true);
                }
            }
            else
            {
                if (check_auxiliary_flag())
                {
                    set_auxiliary_carry_flag(false);
                }
            }
        }
        #endregion

        #region set auxiliary carry
        private void set_auxiliary_carry_flag(bool set)
        {
            if (set)
            {
                if (!check_auxiliary_flag())
                {
                    regs.F ^= 10;
                }
            }
            else if (check_auxiliary_flag())
            {
                regs.F ^= 10;
            }
        }
        #endregion

        #region check auxiliary carry
        private bool check_auxiliary_flag()
        {
            if ((regs.F >> 4 & 0x1) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region zero set
        private void zero_flag_set(byte result)
        {
            if (result == 0)
            {
                if (!check_zero_flag())
                {
                    set_zero_flag(true);
                }
            }
            else if (check_zero_flag())
            {
                set_zero_flag(false);
            }
        }
        #endregion

        #region set zero
        private void set_zero_flag(bool set)
        {
            if (set)
            {
                if (!check_zero_flag())
                {
                    regs.F ^= 0x40;
                }
            }
            else if (check_zero_flag())
            {
                regs.F ^= 0x40;
            }
        }
        #endregion

        #region check zero
        private bool check_zero_flag()
        {
            if ((regs.F >> 6 & 0x1) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region sign set
        private void sign_flag_set(byte result)
        {
            if ((result >> 7 & 0x1) == 1)
            {
                if (!check_sign_flag())
                {
                    set_sign_flag(true);
                }
            }
            else if (check_sign_flag())
            {
                set_sign_flag(false);
            }
        }
        #endregion

        #region set sign
        private void set_sign_flag(bool set)
        {
            if (set)
            {
                if (!check_sign_flag())
                {
                    regs.F ^= 0x80;
                }
            }
            else if (check_sign_flag())
            {
                regs.F ^= 0x80;
            }
        }
        #endregion

        #region check sign
        private bool check_sign_flag()
        {
            if ((regs.F >> 7 & 0x1) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region parity set
        private void parity_flag_set(byte result)
        {
            if (parity_loopup[result] == 4)
            {
                if (!check_parity_flag())
                {
                    set_parity_flag(true);
                }
            }
            else if (check_parity_flag())
            {
                set_parity_flag(false);
            }
        }
        #endregion

        #region set parity
        private void set_parity_flag(bool set)
        {
            if (set)
            {
                if (!check_parity_flag())
                {
                    regs.F ^= 0x4;
                }
            }
            else if (check_parity_flag())
            {
                regs.F ^= 0x4;
            }
        }
        #endregion

        #region check parity
        private bool check_parity_flag()
        {
            if ((regs.F >> 2 & 0x1) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region vblank
        private void VBlank()
        {
            read_port1 = 0x0;
            read_port2 = 0x0;
            _video.lock_texture();

            for (int y = 0; y < 224; y++)
            {
                width_offset = 255;
                for (int x = 0; x < 32; x++)
                {
                    hold = _memory.read_byte((ushort)(0x2400 + ((y * 32) + x)));
                    
                    for (int i = 0; i < 8; i++)
                    {
                        width_offset--;
                        if ((hold >> i & 0x1) == 1)
                        {
                            if ((width_offset > 31) && (width_offset < 63))
                            {
                                _video.set_pixel(y, width_offset, (uint)_video.Colors[0].ToArgb());
                            }
                            else if ((width_offset > 182) && (width_offset < 239))
                            {
                                _video.set_pixel(y, width_offset, (uint)_video.Colors[1].ToArgb());
                            }
                            else if ((width_offset > 238) && (width_offset < 256) && (y > 15) && (y < 103))
                            {
                                _video.set_pixel(y, width_offset, (uint)_video.Colors[2].ToArgb());
                            }
                            else
                            {
                                _video.set_pixel(y, width_offset, (uint)_video.Colors[3].ToArgb());
                            }
                        }
                        else
                        {
                            _video.set_pixel(y, width_offset, (uint)_video.Colors[4].ToArgb());
                        }
                        if (width_offset == 0)
                        {
                            width_offset = 255;
                        }
                    }
                }
            }

            _video.unlock_texture();
            _video.Renderer();

            #region input handling
            if (((Video)_video).Directx_Window.Focused)
            {
                if (_input.Key_Pressed.IsPressed(SlimDX.DirectInput.Key.C))
                {
                    _memory.write_ushort(0x20F8, 0x9989);
                }

                if (_input.Key_Pressed.IsPressed(_input.Keys[0]))
                {
                    if ((read_port1 & 0x1) == 0x0)
                    {
                        read_port1 ^= 0x1;
                    }
                }

                if (_input.Key_Pressed.IsPressed(_input.Keys[1]))
                {
                    if ((read_port1 >> 1 & 0x1) == 0x0)
                    {
                        read_port1 ^= 0x2;
                    }
                }

                if (_input.Key_Pressed.IsPressed(_input.Keys[2]))
                {
                    if ((read_port1 >> 2 & 0x1) == 0x0)
                    {
                        read_port1 ^= 0x4;
                    }
                }

                if (_input.Key_Pressed.IsPressed(_input.Keys[3]))
                {
                    if ((read_port1 >> 4 & 0x1) == 0x0)
                    {
                        read_port1 ^= 0x10;
                    }
                }

                if (_input.Key_Pressed.IsPressed(_input.Keys[4]))
                {
                    if ((read_port1 >> 5 & 0x1) == 0x0)
                    {
                        read_port1 ^= 0x20;
                    }
                }

                if (_input.Key_Pressed.IsPressed(_input.Keys[5]))
                {
                    if ((read_port1 >> 6 & 0x1) == 0x0)
                    {
                        read_port1 ^= 0x40;
                    }
                }

                if (_input.Key_Pressed.IsPressed(_input.Keys[6]))
                {
                    if ((read_port2 >> 2 & 0x1) == 0x0)
                    {
                        read_port2 ^= 0x4;
                    }
                }

                if (_input.Key_Pressed.IsPressed(_input.Keys[7]))
                {
                    if ((read_port2 >> 4 & 0x1) == 0x0)
                    {
                        read_port2 ^= 0x10;
                    }
                }

                if (_input.Key_Pressed.IsPressed(_input.Keys[8]))
                {
                    if ((read_port2 >> 5 & 0x1) == 0x0)
                    {
                        read_port2 ^= 0x20;
                    }
                }

                if (_input.Key_Pressed.IsPressed(_input.Keys[9]))
                {
                    if ((read_port2 >> 6 & 0x1) == 0x0)
                    {
                        read_port2 ^= 0x40;
                    }
                }
            }
            #endregion
        }
        #endregion

        #region execute
        public void execute()
        {
            frame++;

            if (cycles < 0)
            {
                extra_cycles += (byte)((~cycles) + 1);
            }

            if ((frame % 3) == 0)
            {
                cycles = (17066 - extra_cycles);
                extra_cycles = 0;
            }
            else
            {
                cycles = (17067 - extra_cycles);
                extra_cycles = 0;
            }

            while (cycles > 0)
            {
                Fetch_Opcode();
                Decode_Opcode();
            }

            RST(1);
            _input.Key_Pressed = _input.retrieve_keyboardstate();
            VBlank();

            if (cycles < 0)
            {
                extra_cycles += (byte)((~cycles) + 1);
            }

            if ((frame % 3) == 0)
            {
                cycles = (17066 - extra_cycles);
                extra_cycles = 0;
            }
            else
            {
                cycles = (17067 - extra_cycles);
                extra_cycles = 0;
            }

            while (cycles > 0)
            {
                Fetch_Opcode();
                Decode_Opcode();
            }

            RST(2);
        }
        #endregion

        #region fetch opcode
        private void Fetch_Opcode()
        {
            opcode = _memory.read_byte(pc);
        }
        #endregion

        #region decode opcode
        private void Decode_Opcode()
        {
            cycles -= cycles_lookup[opcode];

            switch (opcode)
            {
                case 0x37:
                    {
                        STC();
                        return;
                    }
                case 0x3F:
                    {
                        CMC();
                        return;
                    }
                case 0x04:
                    {
                        INR(ref regs.B);
                        return;
                    }
                case 0x0C:
                    {
                        INR(ref regs.C);
                        return;
                    }
                case 0x14:
                    {
                        INR(ref regs.D);
                        return;
                    }
                case 0x1C:
                    {
                        INR(ref regs.E);
                        return;
                    }
                case 0x24:
                    {
                        INR(ref regs.H);
                        return;
                    }
                case 0x2C:
                    {
                        INR(ref regs.L);
                        return;
                    }
                case 0x34:
                    {
                        INR(ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x3C:
                    {
                        INR(ref regs.A);
                        return;
                    }
                case 0x05:
                    {
                        DCR(ref regs.B);
                        return;
                    }
                case 0x0D:
                    {
                        DCR(ref regs.C);
                        return;
                    }
                case 0x15:
                    {
                        DCR(ref regs.D);
                        return;
                    }
                case 0x1D:
                    {
                        DCR(ref regs.E);
                        return;
                    }
                case 0x25:
                    {
                        DCR(ref regs.H);
                        return;
                    }
                case 0x2D:
                    {
                        DCR(ref regs.L);
                        return;
                    }
                case 0x35:
                    {
                        DCR(ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x3D:
                    {
                        DCR(ref regs.A);
                        return;
                    }
                case 0x2F:
                    {
                        CMA();
                        return;
                    }
                case 0x0:
                    {
                        nop();
                        return;
                    }
                case 0x27:
                    {
                        DAA();
                        return;
                    }
                case 0x40:
                    {
                        MOV(ref regs.B, ref regs.B);
                        return;
                    }
                case 0x41:
                    {
                        MOV(ref regs.B, ref regs.C);
                        return;
                    }
                case 0x42:
                    {
                        MOV(ref regs.B, ref regs.D);
                        return;
                    }
                case 0x43:
                    {
                        MOV(ref regs.B, ref regs.E);
                        return;
                    }
                case 0x44:
                    {
                        MOV(ref regs.B, ref regs.H);
                        return;
                    }
                case 0x45:
                    {
                        MOV(ref regs.B, ref regs.L);
                        return;
                    }
                case 0x46:
                    {
                        MOV(ref regs.B, ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x47:
                    {
                        MOV(ref regs.B, ref regs.A);
                        return;
                    }
                case 0x48:
                    {
                        MOV(ref regs.C, ref regs.B);
                        return;
                    }
                case 0x49:
                    {
                        MOV(ref regs.C, ref regs.C);
                        return;
                    }
                case 0x4A:
                    {
                        MOV(ref regs.C, ref regs.D);
                        return;
                    }
                case 0x4B:
                    {
                        MOV(ref regs.C, ref regs.E);
                        return;
                    }
                case 0x4C:
                    {
                        MOV(ref regs.C, ref regs.H);
                        return;
                    }
                case 0x4D:
                    {
                        MOV(ref regs.C, ref regs.L);
                        return;
                    }
                case 0x4E:
                    {
                        MOV(ref regs.C, ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x4F:
                    {
                        MOV(ref regs.C, ref regs.A);
                        return;
                    }
                case 0x50:
                    {
                        MOV(ref regs.D, ref regs.B);
                        return;
                    }
                case 0x51:
                    {
                        MOV(ref regs.D, ref regs.C);
                        return;
                    }
                case 0x52:
                    {
                        MOV(ref regs.D, ref regs.D);
                        return;
                    }
                case 0x53:
                    {
                        MOV(ref regs.D, ref regs.E);
                        return;
                    }
                case 0x54:
                    {
                        MOV(ref regs.D, ref regs.H);
                        return;
                    }
                case 0x55:
                    {
                        MOV(ref regs.D, ref regs.L);
                        return;
                    }
                case 0x56:
                    {
                        MOV(ref regs.D, ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x57:
                    {
                        MOV(ref regs.D, ref regs.A);
                        return;
                    }
                case 0x58:
                    {
                        MOV(ref regs.E, ref regs.B);
                        return;
                    }
                case 0x59:
                    {
                        MOV(ref regs.E, ref regs.C);
                        return;
                    }
                case 0x5A:
                    {
                        MOV(ref regs.E, ref regs.D);
                        return;
                    }
                case 0x5B:
                    {
                        MOV(ref regs.E, ref regs.E);
                        return;
                    }
                case 0x5C:
                    {
                        MOV(ref regs.E, ref regs.H);
                        return;
                    }
                case 0x5D:
                    {
                        MOV(ref regs.E, ref regs.L);
                        return;
                    }
                case 0x5E:
                    {
                        MOV(ref regs.E, ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x5F:
                    {
                        MOV(ref regs.E, ref regs.A);
                        return;
                    }
                case 0x60:
                    {
                        MOV(ref regs.H, ref regs.B);
                        return;
                    }
                case 0x61:
                    {
                        MOV(ref regs.H, ref regs.C);
                        return;
                    }
                case 0x62:
                    {
                        MOV(ref regs.H, ref regs.D);
                        return;
                    }
                case 0x63:
                    {
                        MOV(ref regs.H, ref regs.E);
                        return;
                    }
                case 0x64:
                    {
                        MOV(ref regs.H, ref regs.H);
                        return;
                    }
                case 0x65:
                    {
                        MOV(ref regs.H, ref regs.L);
                        return;
                    }
                case 0x66:
                    {
                        MOV(ref regs.H, ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x67:
                    {
                        MOV(ref regs.H, ref regs.A);
                        return;
                    }
                case 0x68:
                    {
                        MOV(ref regs.L, ref regs.B);
                        return;
                    }
                case 0x69:
                    {
                        MOV(ref regs.L, ref regs.C);
                        return;
                    }
                case 0x6A:
                    {
                        MOV(ref regs.L, ref regs.D);
                        return;
                    }
                case 0x6B:
                    {
                        MOV(ref regs.L, ref regs.E);
                        return;
                    }
                case 0x6C:
                    {
                        MOV(ref regs.L, ref regs.H);
                        return;
                    }
                case 0x6D:
                    {
                        MOV(ref regs.L, ref regs.L);
                        return;
                    }
                case 0x6E:
                    {
                        MOV(ref regs.L, ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x6F:
                    {
                        MOV(ref regs.L, ref regs.A);
                        return;
                    }
                case 0x70:
                    {
                        MOV(ref _memory.Memory_Access[regs.HL], ref regs.B);
                        return;
                    }
                case 0x71:
                    {
                        MOV(ref _memory.Memory_Access[regs.HL], ref regs.C);
                        return;
                    }
                case 0x72:
                    {
                        MOV(ref _memory.Memory_Access[regs.HL], ref regs.D);
                        return;
                    }
                case 0x73:
                    {
                        MOV(ref _memory.Memory_Access[regs.HL], ref regs.E);
                        return;
                    }
                case 0x74:
                    {
                        MOV(ref _memory.Memory_Access[regs.HL], ref regs.H);
                        return;
                    }
                case 0x75:
                    {
                        MOV(ref _memory.Memory_Access[regs.HL], ref regs.L);
                        return;
                    }
                case 0x77:
                    {
                        MOV(ref _memory.Memory_Access[regs.HL], ref regs.A);
                        return;
                    }
                case 0x78:
                    {
                        MOV(ref regs.A, ref regs.B);
                        return;
                    }
                case 0x79:
                    {
                        MOV(ref regs.A, ref regs.C);
                        return;
                    }
                case 0x7A:
                    {
                        MOV(ref regs.A, ref regs.D);
                        return;
                    }
                case 0x7B:
                    {
                        MOV(ref regs.A, ref regs.E);
                        return;
                    }
                case 0x7C:
                    {
                        MOV(ref regs.A, ref regs.H);
                        return;
                    }
                case 0x7D:
                    {
                        MOV(ref regs.A, ref regs.L);
                        return;
                    }
                case 0x7E:
                    {
                        MOV(ref regs.A, ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x7F:
                    {
                        MOV(ref regs.A, ref regs.A);
                        return;
                    }
                case 0x02:
                    {
                        STAX(ref regs.BC);
                        return;
                    }
                case 0x12:
                    {
                        STAX(ref regs.DE);
                        return;
                    }
                case 0x0A:
                    {
                        LDAX(ref regs.BC);
                        return;
                    }
                case 0x1A:
                    {
                        LDAX(ref regs.DE);
                        return;
                    }
                case 0x80:
                    {
                        ADD(ref regs.B);
                        return;
                    }
                case 0x81:
                    {
                        ADD(ref regs.C);
                        return;
                    }
                case 0x82:
                    {
                        ADD(ref regs.D);
                        return;
                    }
                case 0x83:
                    {
                        ADD(ref regs.E);
                        return;
                    }
                case 0x84:
                    {
                        ADD(ref regs.H);
                        return;
                    }
                case 0x85:
                    {
                        ADD(ref regs.L);
                        return;
                    }
                case 0x86:
                    {
                        ADD(ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x87:
                    {
                        ADD(ref regs.A);
                        return;
                    }
                case 0x88:
                    {
                        ADC(ref regs.B);
                        return;
                    }
                case 0x89:
                    {
                        ADC(ref regs.C);
                        return;
                    }
                case 0x8A:
                    {
                        ADC(ref regs.D);
                        return;
                    }
                case 0x8B:
                    {
                        ADC(ref regs.E);
                        return;
                    }
                case 0x8C:
                    {
                        ADC(ref regs.H);
                        return;
                    }
                case 0x8D:
                    {
                        ADC(ref regs.L);
                        return;
                    }
                case 0x8E:
                    {
                        ADC(ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x8F:
                    {
                        ADC(ref regs.A);
                        return;
                    }
                case 0x90:
                    {
                        SUB(ref regs.B);
                        return;
                    }
                case 0x91:
                    {
                        SUB(ref regs.C);
                        return;
                    }
                case 0x92:
                    {
                        SUB(ref regs.D);
                        return;
                    }
                case 0x93:
                    {
                        SUB(ref regs.E);
                        return;
                    }
                case 0x94:
                    {
                        SUB(ref regs.H);
                        return;
                    }
                case 0x95:
                    {
                        SUB(ref regs.L);
                        return;
                    }
                case 0x96:
                    {
                        SUB(ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x97:
                    {
                        SUB(ref regs.A);
                        return;
                    }
                case 0x98:
                    {
                        SBB(ref regs.B);
                        return;
                    }
                case 0x99:
                    {
                        SBB(ref regs.C);
                        return;
                    }
                case 0x9A:
                    {
                        SBB(ref regs.D);
                        return;
                    }
                case 0x9B:
                    {
                        SBB(ref regs.E);
                        return;
                    }
                case 0x9C:
                    {
                        SBB(ref regs.H);
                        return;
                    }
                case 0x9D:
                    {
                        SBB(ref regs.L);
                        return;
                    }
                case 0x9E:
                    {
                        SBB(ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x9F:
                    {
                        SBB(ref regs.A);
                        return;
                    }
                case 0xA0:
                    {
                        ANA(ref regs.B);
                        return;
                    }
                case 0xA1:
                    {
                        ANA(ref regs.C);
                        return;
                    }
                case 0xA2:
                    {
                        ANA(ref regs.D);
                        return;
                    }
                case 0xA3:
                    {
                        ANA(ref regs.E);
                        return;
                    }
                case 0xA4:
                    {
                        ANA(ref regs.H);
                        return;
                    }
                case 0xA5:
                    {
                        ANA(ref regs.L);
                        return;
                    }
                case 0xA6:
                    {
                        ANA(ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0xA7:
                    {
                        ANA(ref regs.A);
                        return;
                    }
                case 0xA8:
                    {
                        XRA(ref regs.B);
                        return;
                    }
                case 0xA9:
                    {
                        XRA(ref regs.C);
                        return;
                    }
                case 0xAA:
                    {
                        XRA(ref regs.D);
                        return;
                    }
                case 0xAB:
                    {
                        XRA(ref regs.E);
                        return;
                    }
                case 0xAC:
                    {
                        XRA(ref regs.H);
                        return;
                    }
                case 0xAD:
                    {
                        XRA(ref regs.L);
                        return;
                    }
                case 0xAE:
                    {
                        XRA(ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0xAF:
                    {
                        XRA(ref regs.A);
                        return;
                    }
                case 0xB0:
                    {
                        ORA(ref regs.B);
                        return;
                    }
                case 0xB1:
                    {
                        ORA(ref regs.C);
                        return;
                    }
                case 0xB2:
                    {
                        ORA(ref regs.D);
                        return;
                    }
                case 0xB3:
                    {
                        ORA(ref regs.E);
                        return;
                    }
                case 0xB4:
                    {
                        ORA(ref regs.H);
                        return;
                    }
                case 0xB5:
                    {
                        ORA(ref regs.L);
                        return;
                    }
                case 0xB6:
                    {
                        ORA(ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0xB7:
                    {
                        ORA(ref regs.A);
                        return;
                    }
                case 0xB8:
                    {
                        CMP(ref regs.B);
                        return;
                    }
                case 0xB9:
                    {
                        CMP(ref regs.C);
                        return;
                    }
                case 0xBA:
                    {
                        CMP(ref regs.D);
                        return;
                    }
                case 0xBB:
                    {
                        CMP(ref regs.E);
                        return;
                    }
                case 0xBC:
                    {
                        CMP(ref regs.H);
                        return;
                    }
                case 0xBD:
                    {
                        CMP(ref regs.L);
                        return;
                    }
                case 0xBE:
                    {
                        CMP(ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0xBF:
                    {
                        CMP(ref regs.A);
                        return;
                    }
                case 0x07:
                    {
                        RLC();
                        return;
                    }
                case 0x0F:
                    {
                        RRC();
                        return;
                    }
                case 0x17:
                    {
                        RAL();
                        return;
                    }
                case 0x1F:
                    {
                        RAR();
                        return;
                    }
                case 0xC5:
                    {
                        PUSH(ref regs.B, ref regs.C);
                        return;
                    }
                case 0xD5:
                    {
                        PUSH(ref regs.D, ref regs.E);
                        return;
                    }
                case 0xE5:
                    {
                        PUSH(ref regs.H, ref regs.L);
                        return;
                    }
                case 0xF5:
                    {
                        PUSH(ref regs.A, ref regs.F);
                        return;
                    }
                case 0xC1:
                    {
                        POP(ref regs.B, ref regs.C);
                        return;
                    }
                case 0xD1:
                    {
                        POP(ref regs.D, ref regs.E);
                        return;
                    }
                case 0xE1:
                    {
                        POP(ref regs.H, ref regs.L);
                        return;
                    }
                case 0xF1:
                    {
                        POP(ref regs.A, ref regs.F);
                        return;
                    }
                case 0x09:
                    {
                        DAD(ref regs.BC);
                        return;
                    }
                case 0x19:
                    {
                        DAD(ref regs.DE);
                        return;
                    }
                case 0x29:
                    {
                        DAD(ref regs.HL);
                        return;
                    }
                case 0x39:
                    {
                        DAD(ref sp);
                        return;
                    }
                case 0x03:
                    {
                        INX(ref regs.BC);
                        return;
                    }
                case 0x13:
                    {
                        INX(ref regs.DE);
                        return;
                    }
                case 0x23:
                    {
                        INX(ref regs.HL);
                        return;
                    }
                case 0x33:
                    {
                        INX(ref sp);
                        return;
                    }
                case 0x0B:
                    {
                        DCX(ref regs.BC);
                        return;
                    }
                case 0x1B:
                    {
                        DCX(ref regs.DE);
                        return;
                    }
                case 0x2B:
                    {
                        DCX(ref regs.HL);
                        return;
                    }
                case 0x3B:
                    {
                        DCX(ref sp);
                        return;
                    }
                case 0xEB:
                    {
                        XCHG();
                        return;
                    }
                case 0xE3:
                    {
                        XTHL();
                        return;
                    }
                case 0xF9:
                    {
                        SPHL();
                        return;
                    }
                case 0x01:
                    {
                        LXI(ref regs.BC);
                        return;
                    }
                case 0x11:
                    {
                        LXI(ref regs.DE);
                        return;
                    }
                case 0x21:
                    {
                        LXI(ref regs.HL);
                        return;
                    }
                case 0x31:
                    {
                        LXI(ref sp);
                        return;
                    }
                case 0x06:
                    {
                        MVI(ref regs.B);
                        return;
                    }
                case 0x0E:
                    {
                        MVI(ref regs.C);
                        return;
                    }
                case 0x16:
                    {
                        MVI(ref regs.D);
                        return;
                    }
                case 0x1E:
                    {
                        MVI(ref regs.E);
                        return;
                    }
                case 0x26:
                    {
                        MVI(ref regs.H);
                        return;
                    }
                case 0x2E:
                    {
                        MVI(ref regs.L);
                        return;
                    }
                case 0x36:
                    {
                        MVI(ref _memory.Memory_Access[regs.HL]);
                        return;
                    }
                case 0x3E:
                    {
                        MVI(ref regs.A);
                        return;
                    }
                case 0xC6:
                    {
                        ADI();
                        return;
                    }
                case 0xCE:
                    {
                        ACI();
                        return;
                    }
                case 0xD6:
                    {
                        SUI();
                        return;
                    }
                case 0xDE:
                    {
                        SBI();
                        return;
                    }
                case 0xE6:
                    {
                        ANI();
                        return;
                    }
                case 0xEE:
                    {
                        XRI();
                        return;
                    }
                case 0xF6:
                    {
                        ORI();
                        return;
                    }
                case 0xFE:
                    {
                        CPI();
                        return;
                    }
                case 0x32:
                    {
                        STA();
                        return;
                    }
                case 0x3A:
                    {
                        LDA();
                        return;
                    }
                case 0x22:
                    {
                        SHLD();
                        return;
                    }
                case 0x2A:
                    {
                        LHLD();
                        return;
                    }
                case 0xE9:
                    {
                        PCHL();
                        return;
                    }
                case 0xC3:
                    {
                        JMP("");
                        return;
                    }
                case 0xDA:
                    {
                        JMP("JC");
                        return;
                    }
                case 0xD2:
                    {
                        JMP("JNC");
                        return;
                    }
                case 0xCA:
                    {
                        JMP("JZ");
                        return;
                    }
                case 0xC2:
                    {
                        JMP("JNZ");
                        return;
                    }
                case 0xFA:
                    {
                        JMP("JM");
                        return;
                    }
                case 0xF2:
                    {
                        JMP("JP");
                        return;
                    }
                case 0xEA:
                    {
                        JMP("JPE");
                        return;
                    }
                case 0xE2:
                    {
                        JMP("JPO");
                        return;
                    }
                case 0xCD:
                    {
                        CALL("");
                        return;
                    }
                case 0xDC:
                    {
                        CALL("CC");
                        return;
                    }
                case 0xD4:
                    {
                        CALL("CNC");
                        return;
                    }
                case 0xCC:
                    {
                        CALL("CZ");
                        return;
                    }
                case 0xC4:
                    {
                        CALL("CNZ");
                        return;
                    }
                case 0xFC:
                    {
                        CALL("CM");
                        return;
                    }
                case 0xF4:
                    {
                        CALL("CP");
                        return;
                    }
                case 0xEC:
                    {
                        CALL("CPE");
                        return;
                    }
                case 0xE4:
                    {
                        CALL("CPO");
                        return;
                    }
                case 0xC9:
                    {
                        RET("");
                        return;
                    }
                case 0xD8:
                    {
                        RET("RC");
                        return;
                    }
                case 0xD0:
                    {
                        RET("RNC");
                        return;
                    }
                case 0xC8:
                    {
                        RET("RZ");
                        return;
                    }
                case 0xC0:
                    {
                        RET("RNZ");
                        return;
                    }
                case 0xF8:
                    {
                        RET("RM");
                        return;
                    }
                case 0xF0:
                    {
                        RET("RP");
                        return;
                    }
                case 0xE8:
                    {
                        RET("RPE");
                        return;
                    }
                case 0xE0:
                    {
                        RET("RPO");
                        return;
                    }
                case 0xC7:
                    {
                        RST(0);
                        return;
                    }
                case 0xCF:
                    {
                        RST(1);
                        return;
                    }
                case 0xD7:
                    {
                        RST(2);
                        return;
                    }
                case 0xDF:
                    {
                        RST(3);
                        return;
                    }
                case 0xE7:
                    {
                        RST(4);
                        return;
                    }
                case 0xEF:
                    {
                        RST(5);
                        return;
                    }
                case 0xF7:
                    {
                        RST(6);
                        return;
                    }
                case 0xFF:
                    {
                        RST(7);
                        return;
                    }
                case 0xFB:
                    {
                        EI();
                        return;
                    }
                case 0xF3:
                    {
                        DI();
                        return;
                    }
                case 0xDB:
                    {
                        IN();
                        return;
                    }
                case 0xD3:
                    {
                        OUT();
                        return;
                    }
                case 0x76:
                    {
                        HLT();
                        return;
                    }
                default:
                    {
                        pc++;
                        return;
                    }
            }
        }
        #endregion

        #region stc
        private void STC()
        {
            set_carry_flag(true);
            pc++;
        }
        #endregion

        #region cmc
        private void CMC()
        {
            regs.F ^= 0x1;
            pc++;
        }
        #endregion

        #region inr
        private void INR(ref byte reg)
        {
            byte_temp1 = reg;

            reg++;
            pc++;

            if ((byte_temp1 & 0xF) > (reg & 0xF))
            {
                set_auxiliary_carry_flag(true);
            }
            else
            {
                set_auxiliary_carry_flag(false);
            }

            zero_flag_set(reg);
            sign_flag_set(reg);
            parity_flag_set(reg);
        }
        #endregion

        #region dcr
        private void DCR(ref byte reg)
        {
            byte_temp1 = 1;
            byte_temp1 = (byte)((~byte_temp1) + 1);

            if (((reg & 0xF) + (byte_temp1 & 0xF)) > 15)
            {
                set_auxiliary_carry_flag(true);
            }
            else
            {
                set_auxiliary_carry_flag(false);
            }

            reg--;
            pc++;

            zero_flag_set(reg);
            sign_flag_set(reg);
            parity_flag_set(reg);
        }
        #endregion

        #region cma
        private void CMA()
        {
            regs.A = (byte)~(regs.A);
            pc++;
        }
        #endregion

        #region nop
        private void nop()
        {
            pc++;
        }
        #endregion

        #region daa
        private void DAA()
        {
            if (((regs.A & 0xF) > 0x09) || (check_auxiliary_flag()))
            {
                byte_temp1 = (byte)((regs.A & 0xF) + 6);
                regs.A += 6;

                if (byte_temp1 > 15)
                {
                    set_auxiliary_carry_flag(true);
                }
                else
                {
                    set_auxiliary_carry_flag(false);
                }
            }
            if ((regs.A >> 4 & 0xF) > 0x09 || check_carry_flag())
            {
                normal_carry_flag_set((ushort)(regs.A + 96));

                regs.A += 96;
            }

            pc++;

            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
            parity_flag_set(regs.A);
        }
        #endregion

        #region mov
        private void MOV(ref byte dst, ref byte src)
        {
            dst = src;
            pc++;
        }
        #endregion

        #region stax
        private void STAX(ref ushort reg)
        {
            _memory.write_byte(reg, regs.A);
            pc++;
        }
        #endregion

        #region ldax
        private void LDAX(ref ushort reg)
        {
            regs.A = _memory.read_byte(reg);
            pc++;
        }
        #endregion

        #region add
        private void ADD(ref byte reg)
        {
            auxiliary_carry_flag_set((byte)((regs.A + reg)));
            normal_carry_flag_set((ushort)(regs.A + reg));

            regs.A += reg;
            pc++;

            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region adc
        private void ADC(ref byte reg)
        {
            byte_temp1 = regs.A;

            regs.A += (byte)(reg + (regs.F & 0x1));
            pc++;

            auxiliary_carry_flag_set((byte)(byte_temp1 + reg + (regs.F & 0x1)));
            normal_carry_flag_set((ushort)(byte_temp1 + reg + (regs.F & 0x1)));
            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region sub
        private void SUB(ref byte reg)
        {
            byte_temp1 = (byte)((~reg) + 1);

            auxiliary_carry_flag_set((byte)(regs.A + byte_temp1));
            sub_carry_flag_set((ushort)(regs.A + byte_temp1));

            regs.A += byte_temp1;
            pc++;

            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region sbb
        private void SBB(ref byte reg)
        {
            byte_temp1 = (byte)((~reg) + 1 + (regs.F & 0x1));

            auxiliary_carry_flag_set((byte)(regs.A + byte_temp1));
            sub_carry_flag_set((ushort)(regs.A + byte_temp1));

            regs.A += byte_temp1;
            pc++;

            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region ana
        private void ANA(ref byte reg)
        {
            auxiliary_carry_flag_set((byte)(regs.A & reg));

            regs.A &= reg;
            pc++;

            set_carry_flag(false);
            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region xra
        private void XRA(ref byte reg)
        {
            byte_temp1 = regs.A;

            regs.A ^= reg;
            pc++;

            set_auxiliary_carry_flag(false);
            set_carry_flag(false);
            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region ora
        private void ORA(ref byte reg)
        {
            regs.A |= reg;
            pc++;

            set_auxiliary_carry_flag(false);
            set_carry_flag(false);
            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region cmp
        private void CMP(ref byte reg)
        {
            byte_temp1 = (byte)((~reg) + 1);
            byte_temp2 = (byte)(regs.A + byte_temp1);

            pc++;

            auxiliary_carry_flag_set((byte)(regs.A + byte_temp1));
            sub_carry_flag_set((ushort)(regs.A + byte_temp1));
            parity_flag_set(byte_temp2);
            zero_flag_set(byte_temp2);
            sign_flag_set(byte_temp2);
        }
        #endregion

        #region rlc
        private void RLC()
        {
            set_carry_flag(false);

            regs.F += (byte)(regs.A >> 7 & 0x1);
            regs.A <<= 1;
            regs.A += (byte)(regs.F & 0x1);
            pc++;
        }
        #endregion

        #region rrc
        private void RRC()
        {
            set_carry_flag(false);

            regs.F += (byte)(regs.A & 0x1);
            regs.A >>= 1;
            regs.A += (byte)((regs.F & 0x1) * 128);
            pc++;
        }
        #endregion

        #region ral
        private void RAL()
        {
            byte_temp1 = (byte)(regs.F & 0x1);

            set_carry_flag(false);

            regs.F += (byte)(regs.A >> 7 & 0x1);
            regs.A <<= 1;
            regs.A += byte_temp1;
            pc++;
        }
        #endregion

        #region rar
        private void RAR()
        {
            byte_temp1 = (byte)(regs.F & 0x1);

            set_carry_flag(false);

            regs.F += (byte)(regs.A & 0x1);
            regs.A >>= 1;
            regs.A += (byte)(byte_temp1 * 128);
            pc++;
        }
        #endregion

        #region push
        private void PUSH(ref byte reg1, ref byte reg2)
        {
            _memory.write_byte((ushort)(sp - 1), reg1);
            _memory.write_byte((ushort)(sp - 2), reg2);
            sp -= 2;
            pc++;
        }
        #endregion

        #region pop
        private void POP(ref byte reg1, ref byte reg2)
        {
            reg2 = _memory.read_byte(sp);
            reg1 = _memory.read_byte((ushort)(sp + 1));
            sp += 2;
            pc++;
        }
        #endregion

        #region dad
        private void DAD(ref ushort reg)
        {
            ushort_temp1 = regs.HL;
            ushort_temp2 = reg;

            dad_carry_flag_set((ushort)(ushort_temp1 + ushort_temp2));
            pc++;

            ushort_temp1 += ushort_temp2;
            regs.HL = ushort_temp1;
        }
        #endregion

        #region inx
        private void INX(ref ushort reg)
        {
            reg++;
            pc++;
        }
        #endregion

        #region dcx
        private void DCX(ref ushort reg)
        {
            reg--;
            pc++;
        }
        #endregion

        #region xchg
        private void XCHG()
        {
            ushort_temp1 = regs.DE;

            regs.DE = regs.HL;
            regs.HL = ushort_temp1;
            pc++;
        }
        #endregion

        #region xthl
        private void XTHL()
        {
            byte_temp1 = _memory.read_byte((ushort)(sp + 1));
            byte_temp2 = _memory.read_byte(sp);

            _memory.write_byte((ushort)(sp + 1), regs.H);
            _memory.write_byte(sp, regs.L);
            regs.H = byte_temp1;
            regs.L = byte_temp2;
            pc++;
        }
        #endregion

        #region sphl
        private void SPHL()
        {
            sp = regs.HL;
            pc++;
        }
        #endregion

        #region lxi
        private void LXI(ref ushort reg)
        {
            reg = _memory.read_ushort((ushort)(pc + 1));
            pc += 3;
        }
        #endregion

        #region mvi
        private void MVI(ref byte reg1)
        {
            reg1 = _memory.read_byte((ushort)(pc + 1));
            pc += 2;
        }
        #endregion

        #region adi
        private void ADI()
        {
            byte_temp1 = _memory.read_byte((ushort)(pc + 1));

            auxiliary_carry_flag_set((byte)(regs.A + byte_temp1));
            normal_carry_flag_set((ushort)(regs.A + byte_temp1));

            regs.A += byte_temp1;
            pc += 2;

            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region aci
        private void ACI()
        {
            byte_temp1 = regs.A;
            byte_temp2 = _memory.read_byte((ushort)(pc + 1));

            regs.A += byte_temp2;
            pc += 2;

            auxiliary_carry_flag_set((byte)(byte_temp1 + byte_temp2 + (regs.F & 0x1)));
            normal_carry_flag_set((ushort)(byte_temp1 + byte_temp2 + (regs.F & 0x1)));
            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region sui
        private void SUI()
        {
            byte_temp1 = _memory.read_byte((ushort)(pc + 1));
            byte_temp2 = (byte)((~byte_temp1) + 1);

            auxiliary_carry_flag_set((byte)(regs.A + byte_temp2));
            sub_carry_flag_set((ushort)(regs.A + byte_temp2));

            regs.A += byte_temp2;
            pc += 2;

            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region sbi
        private void SBI()
        {
            byte_temp1 = _memory.read_byte((ushort)(pc + 1));
            byte_temp2 = (byte)((~byte_temp1) + 1 + (regs.F & 0x1));

            auxiliary_carry_flag_set((byte)(regs.A + byte_temp2));
            sub_carry_flag_set((ushort)(regs.A + byte_temp2));

            regs.A += byte_temp2;
            pc += 2;

            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region ani
        private void ANI()
        {
            byte_temp1 = _memory.read_byte((ushort)(pc + 1));

            regs.A &= byte_temp1;
            pc += 2;

            set_auxiliary_carry_flag(false);
            set_carry_flag(false);
            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region xri
        private void XRI()
        {
            byte_temp1 = _memory.read_byte((ushort)(pc + 1));

            regs.A ^= byte_temp1;
            pc += 2;

            set_auxiliary_carry_flag(false);
            set_carry_flag(false);
            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region ori
        private void ORI()
        {
            byte_temp1 = _memory.read_byte((ushort)(pc + 1));

            regs.A |= byte_temp1;
            pc += 2;

            set_auxiliary_carry_flag(false);
            set_carry_flag(false);
            parity_flag_set(regs.A);
            zero_flag_set(regs.A);
            sign_flag_set(regs.A);
        }
        #endregion

        #region cpi
        private void CPI()
        {
            byte_temp1 = _memory.read_byte((ushort)(pc + 1));
            byte_temp2 = (byte)((~byte_temp1) + 1);
            byte_temp1 = (byte)(regs.A + byte_temp2);

            pc += 2;

            auxiliary_carry_flag_set((byte)(regs.A + byte_temp2));
            sub_carry_flag_set((ushort)(regs.A + byte_temp2));
            parity_flag_set(byte_temp1);
            zero_flag_set(byte_temp1);
            sign_flag_set(byte_temp1);
        }
        #endregion

        #region sta
        private void STA()
        {
            _memory.write_byte((ushort)((_memory.read_byte((ushort)(pc + 2)) << 8) | _memory.read_byte((ushort)(pc + 1))), regs.A);
            pc += 3;
        }
        #endregion

        #region lda
        private void LDA()
        {
            regs.A = _memory.read_byte((ushort)((_memory.read_byte((ushort)(pc + 2)) << 8) | _memory.read_byte((ushort)(pc + 1))));
            pc += 3;
        }
        #endregion

        #region shld
        private void SHLD()
        {
            _memory.write_byte((ushort)((_memory.read_byte((ushort)(pc + 2)) << 8) | _memory.read_byte((ushort)(pc + 1))), regs.L);
            _memory.write_byte((ushort)((_memory.read_byte((ushort)(pc + 2)) << 8) | _memory.read_byte((ushort)(pc + 1)) + 1), regs.H);
            pc += 3;
        }
        #endregion

        #region lhld
        private void LHLD()
        {
            regs.L = _memory.read_byte((ushort)((_memory.read_byte((ushort)(pc + 2)) << 8) | _memory.read_byte((ushort)(pc + 1))));
            regs.H = _memory.read_byte((ushort)((_memory.read_byte((ushort)(pc + 2)) << 8) | _memory.read_byte((ushort)(pc + 1)) + 1));
            pc += 3;
        }
        #endregion

        #region pchl
        private void PCHL()
        {
            pc = regs.HL;
        }
        #endregion

        #region jmp
        private void JMP(string type)
        {
            bool result = false;

            switch (type)
            {
                case "":
                    {
                        pc = (ushort)((_memory.read_byte((ushort)(pc + 2)) << 8) | _memory.read_byte((ushort)(pc + 1)));
                        return;
                    }
                default:
                    {
                        result = select_status_check(type);
                        break;
                    }
            }

            if (result)
            {
                pc = (ushort)((_memory.read_byte((ushort)(pc + 2)) << 8) | _memory.read_byte((ushort)(pc + 1)));
            }
            else
            {
                pc += 3;
            }
        }
        #endregion

        #region call
        private void CALL(string type)
        {
            bool result = false;

            pc += 3;

            switch (type)
            {
                case "":
                    {
                        _memory.write_ushort((ushort)(sp - 2), pc);
                        sp -= 2;

                        pc = (ushort)((_memory.read_byte((ushort)(pc - 1)) << 8) | _memory.read_byte((ushort)(pc - 2)));
                        return;
                    }
                default:
                    {
                        result = select_status_check(type);
                        break;
                    }
            }

            if (result)
            {
                _memory.write_ushort((ushort)(sp - 2), pc);
                sp -= 2;
                cycles -= 6;

                pc = (ushort)((_memory.read_byte((ushort)(pc - 1)) << 8) | _memory.read_byte((ushort)(pc - 2)));
            }
        }
        #endregion

        #region ret
        private void RET(string type)
        {
            bool result = false;

            switch (type)
            {
                case "":
                    {
                        pc = (ushort)((_memory.read_byte((ushort)(sp + 1)) << 8) | _memory.read_byte(sp));
                        sp += 2;
                        return;
                    }
                default:
                    {
                        result = select_status_check(type);
                        break;
                    }
            }

            if (result)
            {
                pc = (ushort)((_memory.read_byte((ushort)(sp + 1)) << 8) | _memory.read_byte(sp));
                sp += 2;
                cycles -= 6;
            }
            else
            {
                pc++;
            }
        }
        #endregion

        #region rst
        private void RST(byte exp)
        {
            if (INTE == 1)
            {
                DI();
                _memory.write_ushort((ushort)(sp - 2), pc);
                sp -= 2;

                pc = (ushort)(exp * 8);
            }
        }
        #endregion

        #region ei
        private void EI()
        {
            INTE = 1;
            pc++;
        }
        #endregion

        #region di
        private void DI()
        {
            INTE = 0;
        }
        #endregion

        #region in
        private void IN()
        {
            switch (_memory.read_byte((ushort)(pc + 1)))
            {
                case 1:
                    {
                        regs.A = read_port1;
                        break;
                    }
                case 2:
                    {
                        regs.A = (byte)(read_port2 | DipSwitch);
                        break;
                    }
                case 3:
                    {
                        regs.A = (byte)(((shift_register) << shift_register_offset) >> 8);
                        break;
                    }
            }

            pc += 2;
        }
        #endregion

        #region out
        private void OUT()
        {
            switch (_memory.read_byte((ushort)(pc + 1)))
            {
                case 2:
                    {
                        shift_register_offset = (byte)(regs.A & 0x7);
                        break;
                    }
                case 3:
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (((regs.A & (0x10 >> 4)) != 0))
                            {
                                _sound.play_sound(0);
                                _sound.Sound_statuses[0] = true;
                            }

                            if (((regs.A & (0x10 >> 3)) != 0) && (_sound.Sound_statuses[1] == false))
                            {
                                _sound.play_sound(1);
                                _sound.Sound_statuses[1] = true;
                            }
                            else if ((_sound.Sound_statuses[1] == true) && ((regs.A & (0x10 >> 3)) == 0))
                            {
                                _sound.Sound_statuses[1] = false;
                            }

                            if (((regs.A & (0x10 >> 2)) != 0) && (_sound.Sound_statuses[2] == false))
                            {
                                _sound.play_sound(2);
                                _sound.Sound_statuses[2] = true;
                            }
                            else if ((_sound.Sound_statuses[2] == true) && ((regs.A & (0x10 >> 2)) == 0))
                            {
                                _sound.Sound_statuses[2] = false;
                            }

                            if (((regs.A & (0x10 >> 1)) != 0) && (_sound.Sound_statuses[3] == false))
                            {
                                _sound.play_sound(3);
                                _sound.Sound_statuses[3] = true;
                            }
                            else if ((_sound.Sound_statuses[3] == true) && ((regs.A & (0x10 >> 1)) == 0))
                            {
                                _sound.Sound_statuses[3] = false;
                            }

                            if (((regs.A & 0x10) != 0) && (_sound.Sound_statuses[9] == false))
                            {
                                _sound.play_sound(9);
                                _sound.Sound_statuses[9] = true;
                            }
                            else if ((_sound.Sound_statuses[9] == true) && ((regs.A & 0x10) == 0))
                            {
                                _sound.Sound_statuses[9] = false;
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        shift_register = (ushort)((shift_register >> 8) | (regs.A << 8));
                        break;
                    }
                case 5:
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (((regs.A & (0x10 >> 4)) != 0) && (_sound.Sound_statuses[4] == false))
                            {
                                _sound.play_sound(4);
                                _sound.Sound_statuses[4] = true;
                            }
                            else if ((_sound.Sound_statuses[4] == true) && ((regs.A & (0x10 >> 4)) == 0))
                            {
                                _sound.Sound_statuses[4] = false;
                            }

                            if (((regs.A & (0x10 >> 3)) != 0) && (_sound.Sound_statuses[5] == false))
                            {
                                _sound.play_sound(5);
                                _sound.Sound_statuses[5] = true;
                            }
                            else if ((_sound.Sound_statuses[5] == true) && ((regs.A & (0x10 >> 3)) == 0))
                            {
                                _sound.Sound_statuses[5] = false;
                            }

                            if (((regs.A & (0x10 >> 2)) != 0) && (_sound.Sound_statuses[6] == false))
                            {
                                _sound.play_sound(6);
                                _sound.Sound_statuses[6] = true;
                            }
                            else if ((_sound.Sound_statuses[6] == true) && ((regs.A & (0x10 >> 2)) == 0))
                            {
                                _sound.Sound_statuses[6] = false;
                            }

                            if (((regs.A & (0x10 >> 1)) != 0) && (_sound.Sound_statuses[7] == false))
                            {
                                _sound.play_sound(7);
                                _sound.Sound_statuses[7] = true;
                            }
                            else if ((_sound.Sound_statuses[7] == true) && ((regs.A & (0x10 >> 1)) == 0))
                            {
                                _sound.Sound_statuses[7] = false;
                            }

                            if (((regs.A & 0x10) != 0) && (_sound.Sound_statuses[8] == false))
                            {
                                _sound.play_sound(8);
                                _sound.Sound_statuses[8] = true;
                            }
                            else if ((_sound.Sound_statuses[8] == true) && ((regs.A & 0x10) == 0))
                            {
                                _sound.Sound_statuses[8] = false;
                            }
                        }
                        break;
                    }
                case 6:
                    {
                        break;
                    }
            }

            pc += 2;
        }
        #endregion

        #region hlt
        private void HLT()
        {
            cycles = 0;
            pc++;
        }
        #endregion
    }
}
