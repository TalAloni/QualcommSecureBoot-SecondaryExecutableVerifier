using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace QualcommLibrary
{
    public class SecondaryExecutableHeader
    {
        public static readonly byte[] HeaderSignature = new byte[] { 0xD1, 0xDC, 0x4B, 0x84, 0x34, 0x10, 0xD7, 0x73 };
        public static int Length = 0x50;

        public uint ImageType;
        public uint CodeStartOffset;
        public uint LoadingAddress;
        public uint FileSize; // (header + body + signature + certificate store)
        public uint CodeSize; // The size of the executable (without the header / signature / certificate store)
        public uint SignatureAddress;
        public uint SignatureLength;
        public uint CertificateStoreAddress;
        public uint CertificateStoreLength;

        public SecondaryExecutableHeader(byte[] buffer, int offset)
        {
            ImageType = LittleEndianConverter.ToUInt32(buffer, offset + 0x08);
            CodeStartOffset = LittleEndianConverter.ToUInt32(buffer, offset + 0x14);
            LoadingAddress = LittleEndianConverter.ToUInt32(buffer, offset + 0x18);
            FileSize = LittleEndianConverter.ToUInt32(buffer, offset + 0x1C);
            CodeSize = LittleEndianConverter.ToUInt32(buffer, offset + 0x20);
            SignatureAddress = LittleEndianConverter.ToUInt32(buffer, offset + 0x24);
            SignatureLength = LittleEndianConverter.ToUInt32(buffer, offset + 0x28);
            CertificateStoreAddress = LittleEndianConverter.ToUInt32(buffer, offset + 0x2C);
            CertificateStoreLength = LittleEndianConverter.ToUInt32(buffer, offset + 0x30);
        }

        public uint SignatureOffset
        {
            get
            {
                return CodeStartOffset + SignatureAddress - LoadingAddress;
            }
        }

        public uint CertificateStoreOffset
        {
            get
            {
                return CodeStartOffset + CertificateStoreAddress - LoadingAddress;
            }
        }

        private static SecondaryExecutableHeader ReadHeader(byte[] buffer, int offset)
        {
            byte[] signature = ByteReader.ReadBytes(buffer, offset, 8);
            if (ByteUtils.AreByteArraysEqual(signature, HeaderSignature))
            {
                return new SecondaryExecutableHeader(buffer, offset);
            }
            else
            {
                return null;
            }
        }

        public static SecondaryExecutableHeader ReadActiveHeader(byte[] buffer)
        {
            int offset = 0;
            SecondaryExecutableHeader header = ReadHeader(buffer, offset);
            if (header != null && header.ImageType == 0x7D0B435A)
            {
                offset += 0x2800;
                header = ReadHeader(buffer, offset);
            }
            return header;
        }

        public static bool IsSecondaryExecutable(byte[] imageBytes)
        {
            byte[] signature = ByteReader.ReadBytes(imageBytes, 0, 8);
            return ByteUtils.AreByteArraysEqual(signature, HeaderSignature);
        }
    }
}
