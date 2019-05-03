
namespace QualcommLibrary.Protocols.Sahara
{
    /// <summary>
    /// CmdID in "Command Mode"
    /// </summary>
    public enum CmdName : uint
    {
        GetSerialNumber = 1,
        GetHardwareID = 2,
        GetOemPKHash = 3, // returns 96 bytes
        Unknown6 = 6, // returns 3904 bytes
        GetSBLSV = 7,
    }
}
