using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QualcommLibrary.Protocols.HDLC
{
    public class HDLCPacketHelper
    {
        public static byte[] EncodePacket(byte[] buffer)
        {
            MemoryStream stream = new MemoryStream();
            for (int index = 0; index < buffer.Length; index++)
            {
                if (buffer[index] == 0x7D)
                {
                    stream.WriteByte(0x7D);
                    stream.WriteByte(0x5D);
                }
                else if (buffer[index] == 0x7E)
                {
                    stream.WriteByte(0x7D);
                    stream.WriteByte(0x5E);
                }
                else
                {
                    stream.WriteByte(buffer[index]);
                }
            }
            return stream.ToArray();
        }

        public static byte[] DecodePacket(byte[] buffer)
        {
            MemoryStream stream = new MemoryStream();
            for (int index = 0; index < buffer.Length; index++)
            {
                if (index < buffer.Length - 1 && buffer[index] == 0x7D && buffer[index + 1] == 0x5D)
                {
                    stream.WriteByte(0x7D);
                    index++;
                }
                else if (index < buffer.Length - 1 && buffer[index] == 0x7D && buffer[index + 1] == 0x5E)
                {
                    stream.WriteByte(0x7E);
                    index++;
                }
                else
                {
                    stream.WriteByte(buffer[index]);
                }
            }
            return stream.ToArray();
        }

        public static int GetEndOfPacketIndex(byte[] buffer)
        {
            if (buffer.Length > 3)
            {
                for (int index = 3; index < buffer.Length; index++)
                {
                    if (buffer[index] == 0x7E)
                    {
                        return index;
                    }
                }
            }
            return -1;
        }
    }
}
