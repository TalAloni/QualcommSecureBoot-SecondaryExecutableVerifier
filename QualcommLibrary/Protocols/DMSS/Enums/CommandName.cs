
namespace QualcommLibrary.Protocols.DMSS
{
    public enum CommandName : byte
    {
        SoftwareVersionRequest = 0x0C,
        SoftwareVersionResponse = 0x0D,
        MemoryReadRequest = 0x12,
        MemoryReadResponse = 0x13,
    }
}
