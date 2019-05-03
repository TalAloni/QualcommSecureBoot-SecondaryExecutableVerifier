using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace QualcommLibrary.Protocols.HDLC
{
    public class HDLCTests
    {
        public static bool TestCrc16Ccitt_1()
        {
            byte[] buffer = new byte[] { 0x06 };
            byte[] checksum = new byte[]{ 0x4E, 0x95};
            byte[] result = LittleEndianConverter.GetBytes(Crc16Ccitt.ComputeChecksum(buffer));
            return ByteUtils.AreByteArraysEqual(checksum, result);
        }

        public static bool TestCrc16Ccitt_2()
        {
            byte[] buffer = new byte[] {0x0d, 0x0f, 0x50, 0x42, 0x4c, 0x5f, 0x44, 0x6c, 0x6f, 0x61, 0x64, 0x56, 0x45, 0x52, 0x31, 0x2e, 0x30};
            byte[] checksum = new byte[]{ 0x37, 0x41};
            byte[] result = LittleEndianConverter.GetBytes(Crc16Ccitt.ComputeChecksum(buffer));
            return ByteUtils.AreByteArraysEqual(checksum, result);
        }
    }
}
