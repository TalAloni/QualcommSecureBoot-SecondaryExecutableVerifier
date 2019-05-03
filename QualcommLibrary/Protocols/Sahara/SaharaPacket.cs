using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Utilities;

namespace QualcommLibrary.Protocols.Sahara
{
    public class SaharaPacket
    {
        public CommandName CommandID; // 4 bytes
        // uint PacketLength; // 4 bytes
        public uint? OptionalField1; // 4 bytes
        public uint? OptionalField2; // 4 bytes
        public byte[] RawData = new byte[0];

        public SaharaPacket()
        {
        }

        public SaharaPacket(byte[] buffer)
        {
            CommandID = (CommandName)LittleEndianConverter.ToUInt32(buffer, 0);
            uint packetLength = LittleEndianConverter.ToUInt32(buffer, 4);
            if (packetLength >= 12)
            {
                OptionalField1 = LittleEndianConverter.ToUInt32(buffer, 8);
                if (packetLength >= 16)
                {
                    OptionalField2 = LittleEndianConverter.ToUInt32(buffer, 12);
                    int dataLength = (int)(packetLength - 16);
                    if (dataLength > 0)
                    {
                        RawData = ByteReader.ReadBytes(buffer, 16, dataLength);
                    }
                }
            }
        }

        public byte[] GetBytes()
        {
            uint packetLength;
            if (RawData.Length > 0)
            {
                packetLength = 16 + (uint)RawData.Length;
            }
            else if (OptionalField2.HasValue)
            {
                packetLength = 16;
            }
            else if (OptionalField1.HasValue)
            {
                packetLength = 12;
            }
            else
            {
                packetLength = 8;
            }

            byte[] buffer = new byte[packetLength];
            LittleEndianWriter.WriteUInt32(buffer, 0, (uint)CommandID);
            LittleEndianWriter.WriteUInt32(buffer, 4, packetLength);
            if (packetLength >= 12)
            {
                LittleEndianWriter.WriteUInt32(buffer, 8, OptionalField1.HasValue ? OptionalField1.Value : 0);
                if (packetLength >= 16)
                {
                    LittleEndianWriter.WriteUInt32(buffer, 12, OptionalField2.HasValue ? OptionalField2.Value : 0);
                    ByteWriter.WriteBytes(buffer, 16, RawData);
                }
            }

            return buffer;
        }

        public static SaharaPacket GetHelloResponsePacket(uint version, uint compatibleVersion, DeviceModeName deviceMode)
        {
            SaharaPacket packet = new SaharaPacket();
            packet.CommandID = CommandName.HelloResponse;
            packet.OptionalField1 = version;
            packet.OptionalField2 = compatibleVersion;
            packet.RawData = new byte[32];
            LittleEndianWriter.WriteUInt32(packet.RawData, 4, (uint)deviceMode);
            return packet;
        }

        public static SaharaPacket GetDonePacket()
        {
            SaharaPacket packet = new SaharaPacket();
            packet.CommandID = CommandName.Done;
            return packet;
        }

        public static SaharaPacket GetResetPacket()
        {
            SaharaPacket packet = new SaharaPacket();
            packet.CommandID = CommandName.Reset;
            return packet;
        }

        public static SaharaPacket GetMemoryReadPacket(uint address, uint length)
        {
            SaharaPacket packet = new SaharaPacket();
            packet.CommandID = CommandName.MemoryRead;
            packet.OptionalField1 = address;
            packet.OptionalField2 = length;
            return packet;
        }

        public static SaharaPacket GetCommandExecutePacket(CmdName cmdName)
        {
            SaharaPacket packet = new SaharaPacket();
            packet.CommandID = CommandName.CommandExecute;
            packet.OptionalField1 = (uint)cmdName;
            return packet;
        }

        public static SaharaPacket GetCommandExecuteData(CmdName cmdName)
        {
            SaharaPacket packet = new SaharaPacket();
            packet.CommandID = CommandName.CommandExecuteData;
            packet.OptionalField1 = (uint)cmdName;
            return packet;
        }

        public static SaharaPacket GetCommandSwitchModePacket(DeviceModeName deviceMode)
        {
            SaharaPacket packet = new SaharaPacket();
            packet.CommandID = CommandName.CommandSwitchMode;
            packet.OptionalField1 = (uint)deviceMode;
            return packet;
        }
    }
}
