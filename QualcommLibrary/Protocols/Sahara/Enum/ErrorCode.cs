
namespace QualcommLibrary.Protocols.Sahara
{
    public enum ErrorCode
    {
        SAHARA_STATUS_SUCCESS = 0x00,
        SAHARA_NAK_INVALID_CMD = 0x01,
        SAHARA_NAK_PROTOCOL_MISMATCH = 0x02,
        SAHARA_NAK_INVALID_TARGET_PROTOCOL = 0x03,
        SAHARA_NAK_INVALID_HOST_PROTOCOL = 0x04,
        SAHARA_NAK_INVALID_PACKET_SIZE = 0x05,
        SAHARA_NAK_UNEXPECTED_IMAGE_ID = 0x06,
        SAHARA_NAK_INVALID_HEADER_SIZE = 0x07,
        SAHARA_NAK_INVALID_DATA_SIZE = 0x08,
        SAHARA_NAK_INVALID_IMAGE_TYPE = 0x09,
        SAHARA_NAK_INVALID_TX_LENGTH = 0x0A,
        SAHARA_NAK_INVALID_RX_LENGTH = 0x0B,
        SAHARA_NAK_GENERAL_TX_RX_ERROR = 0x0C,
        SAHARA_NAK_READ_DATA_ERROR = 0x0D,
        SAHARA_NAK_UNSUPPORTED_NUM_PHDRS = 0x0E,
        SAHARA_NAK_INVALID_PDHR_SIZE = 0x0F,
        SAHARA_NAK_MULTIPLE_SHARED_SEG = 0x10,
        SAHARA_NAK_UNINIT_PHDR_LOC = 0x11,
        SAHARA_NAK_INVALID_DEST_ADDR = 0x12,
        SAHARA_NAK_INVALID_IMG_HDR_DATA_SIZE = 0x13,
        SAHARA_NAK_INVALID_ELF_HDR = 0x14,
        SAHARA_NAK_UNKNOWN_HOST_ERROR = 0x15,
        SAHARA_NAK_TIMEOUT_RX = 0x16,
        SAHARA_NAK_TIMEOUT_TX = 0x17,
        SAHARA_NAK_INVALID_HOST_MODE = 0x18,
        SAHARA_NAK_INVALID_MEMORY_READ = 0x19,
        SAHARA_NAK_INVALID_DATA_SIZE_REQUEST = 0x1A,
        SAHARA_NAK_MEMORY_DEBUG_NOT_SUPPORTED= 0x1B,
        SAHARA_NAK_INVALID_MODE_SWITCH = 0x1C,
        SAHARA_NAK_CMD_EXEC_FAILURE = 0x1D,
        SAHARA_NAK_EXEC_CMD_INVALID_PARAM = 0x1E,
        SAHARA_NAK_EXEC_CMD_UNSUPPORTED = 0x1F,
        SAHARA_NAK_EXEC_DATA_INVALID_CLIENT_CMD = 0x20,
        SAHARA_NAK_HASH_TABLE_AUTH_FAILURE = 0x21,
        SAHARA_NAK_HASH_VERIFICATION_FAILURE = 0x22,
        SAHARA_NAK_HASH_TABLE_NOT_FOUND = 0x23,
        SAHARA_NAK_LAST_CODE = 0x24,
    }
}
