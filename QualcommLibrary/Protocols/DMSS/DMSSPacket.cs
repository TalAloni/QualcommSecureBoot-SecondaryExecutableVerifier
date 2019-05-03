using System;
using System.Collections.Generic;
using System.Text;
using Utilities;
using QualcommLibrary.Protocols.HDLC;

namespace QualcommLibrary.Protocols.DMSS
{
    public class DMSSPacket
    {
        public CommandName Command;
        public byte[] Data = new byte[0];
        public ushort CRC16;

        public DMSSPacket()
        {
        }

        public DMSSPacket(byte[] buffer, int offset, int length)
        {
            Command = (CommandName)buffer[offset + 0];
            Data = ByteReader.ReadBytes(buffer, offset + 1, length - 3);
            CRC16 = LittleEndianConverter.ToUInt16(buffer, offset + length - 2);
        }

        public byte[] GetBytes()
        {
            byte[] buffer = new byte[1 + Data.Length + 2];
            buffer[0] = (byte)Command;
            ByteWriter.WriteBytes(buffer, 1, Data);
            ushort crc16 = Crc16Ccitt.ComputeChecksum(buffer, 1 + Data.Length);
            LittleEndianWriter.WriteUInt16(buffer, 1 + Data.Length, crc16);
            return buffer;
        }

        public byte[] GetEncodedBytes()
        {
            byte[] buffer = GetBytes();
            return HDLCPacketHelper.EncodePacket(buffer);
        }
    }
}
