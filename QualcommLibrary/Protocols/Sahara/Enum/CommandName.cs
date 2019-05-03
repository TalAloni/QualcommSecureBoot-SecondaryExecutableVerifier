
namespace QualcommLibrary.Protocols.Sahara
{
    public enum CommandName : uint
    {
        Hello = 0x01, // from target to host (e.g. from phone to PC)
        HelloResponse = 0x02, // from host to target
        ReadData = 0x03, // from target to host
        EndOfImageTransfer = 0x04, // from target to host
        Done = 0x05, // done image transfer, from host to target
        DoneResponse = 0x06, // from target to host
        Reset = 0x07, // from host to target
        ResetResponse = 0x08, // from target to host
        MemoryDebug = 0x09, // from target to host
        MemoryRead = 0x0A, // from host to target
        CommandReady = 0x0B, // from target to host
        CommandSwitchMode = 0x0C, // from host to target
        CommandExecute = 0x0D, // from host to target
        CommandExecuteResponse = 0x0E, // from target to host
        CommandExecuteData = 0x0F, // from host to target
    }
}
