using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace QualcommLibrary
{
    public class ApplicationExecutableHeader
    {
        public const int CodeStartOffset = 0x28;
        public const int Length = 0x28;

        public uint Identifier;
        public uint Version;
        public uint LoadingAddress;
        public uint FileSize; // (header + body + signature + certificate store)
        public uint CodeSize; // The size of the executable (without the header / signature / certificate store)
        public uint SignatureAddress;
        public uint SignatureLength;
        public uint CertificateStoreAddress;
        public uint CertificateStoreLength;

        public ApplicationExecutableHeader(byte[] buffer)
        {
            Identifier = LittleEndianConverter.ToUInt32(buffer, 0x00);
            Version = LittleEndianConverter.ToUInt32(buffer, 0x04);
            LoadingAddress = LittleEndianConverter.ToUInt32(buffer, 0x0C);
            FileSize = LittleEndianConverter.ToUInt32(buffer, 0x10);
            CodeSize = LittleEndianConverter.ToUInt32(buffer, 0x14);
            SignatureAddress = LittleEndianConverter.ToUInt32(buffer, 0x18);
            SignatureLength = LittleEndianConverter.ToUInt32(buffer, 0x1C);
            CertificateStoreAddress = LittleEndianConverter.ToUInt32(buffer, 0x20);
            CertificateStoreLength = LittleEndianConverter.ToUInt32(buffer, 0x24);
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

        public static bool IsChainedExecutable(byte[] imageBytes)
        {
            uint version = LittleEndianConverter.ToUInt32(imageBytes, 0x04);
            uint expectedVersion = 0x03;
            return version == expectedVersion;
        }
    }
}
