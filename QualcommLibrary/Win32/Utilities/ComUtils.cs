using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;
using DiskAccessLibrary;

namespace Utilities
{
    [StructLayout(LayoutKind.Sequential)]
    public struct COMMTIMEOUTS
    {
        public UInt32 ReadIntervalTimeout;
        public UInt32 ReadTotalTimeoutMultiplier;
        public UInt32 ReadTotalTimeoutConstant;
        public UInt32 WriteTotalTimeoutMultiplier;
        public UInt32 WriteTotalTimeoutConstant;
    }

    public class ComUtils
    {
        public static SafeFileHandle GetComPortHandle(int portIndex, FileAccess access, ShareMode shareMode)
        {
            string fileName = String.Format(@"\\.\COM{0}", portIndex);
            return HandleUtils.GetFileHandle(fileName, access, shareMode);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetCommTimeouts(SafeFileHandle hFile, out COMMTIMEOUTS lpCommTimeouts);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetCommTimeouts(SafeFileHandle hFile, ref COMMTIMEOUTS lpCommTimeouts);

        public static COMMTIMEOUTS GetCommTimeouts(SafeFileHandle portHandle)
        {
            COMMTIMEOUTS result;
            bool success = GetCommTimeouts(portHandle, out result);
            if (!success)
            {
                int error = Marshal.GetLastWin32Error();
                throw new IOException("Unable to get COM timeouts, Error: " + error.ToString());
            }
            return result;
        }

        public static SafeFileHandle GetComPortHandle(int portIndex, FileAccess access, ShareMode shareMode, uint readTimeout, uint writeTimeout)
        {
            SafeFileHandle handle = ComUtils.GetComPortHandle(portIndex, FileAccess.ReadWrite, ShareMode.None);
            if (!handle.IsInvalid)
            {
                SetCommTimeouts(handle, readTimeout, writeTimeout);
            }
            return handle;
        }

        private static void SetCommTimeouts(SafeFileHandle handle, uint readTimeout, uint writeTimeout)
        {
            COMMTIMEOUTS commTimeouts;
            try
            {
                commTimeouts = GetCommTimeouts(handle);
            }
            catch
            {
                throw new IOException("Can't get COMM timeouts");
            }

            commTimeouts.ReadIntervalTimeout = 0xFFFFFFFF;
            commTimeouts.ReadTotalTimeoutConstant = readTimeout;
            commTimeouts.ReadTotalTimeoutMultiplier = 0xFFFFFFFF;
            commTimeouts.WriteTotalTimeoutConstant = writeTimeout;
            commTimeouts.WriteTotalTimeoutMultiplier = 0;

            try
            {
                bool success = ComUtils.SetCommTimeouts(handle, ref commTimeouts);
            }
            catch(Exception ex)
            {
                throw new IOException("Can't set COMM timeouts: " + ex.ToString());
            }
        }

        public static SafeFileHandle GetComPortHandle(string name, FileAccess access, ShareMode shareMode, uint readTimeout, uint writeTimeout)
        {
            string fileName = String.Format(@"\\.\{0}", name);
            SafeFileHandle handle = HandleUtils.GetFileHandle(fileName, access, shareMode);
            if (!handle.IsInvalid)
            {
                SetCommTimeouts(handle, readTimeout, writeTimeout);
            }
            return handle;
        }
    }
}
