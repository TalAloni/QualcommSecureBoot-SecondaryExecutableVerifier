/* Copyright (C) 2014 Tal Aloni <tal.aloni.il@gmail.com>. All rights reserved.
 * 
 * You can redistribute this program and/or modify it under the terms of
 * the GNU Lesser Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace DiskAccessLibrary
{
    // FileStream reads will try to fill the internal buffer,
    // this may cause issues when reading the last few sectors of the disk,
    // (FileStream will try to read sectors that do not exist).
    // This can be solved by setting the FileStream buffer size to the sector size.
    // An alternative is to use FileStreamEx which does not use an internal read buffer at all.
    public class FileStreamEx : FileStream
    {
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool ReadFile(SafeFileHandle handle, byte[] buffer, uint numberOfBytesToRead, out uint numberOfBytesRead, IntPtr lpOverlapped);

        private bool m_releaseHandle = true;

        public FileStreamEx(SafeFileHandle handle, FileAccess access) : base(handle, access)
        {

        }

        /// <param name="offset">The byte offset in array at which the read bytes will be placed</param>
        /// <param name="count">The maximum number of bytes to read</param>
        public override int Read(byte[] array, int offset, int count)
        {
            uint result;
            bool success;
            if (offset == 0)
            {
                success = ReadFile(this.SafeFileHandle, array, (uint)count, out result, IntPtr.Zero);
            }
            else
            {
                byte[] buffer = new byte[count];
                success = ReadFile(this.SafeFileHandle, buffer, (uint)buffer.Length, out result, IntPtr.Zero);
                Array.Copy(buffer, 0, array, offset, buffer.Length);
            }

            if (success)
            {
                return (int)result;    
            }
            else
            {
                int error = Marshal.GetLastWin32Error();
                if (error == (int)Win32Error.ERROR_ACCESS_DENIED)
                {
                    // UnauthorizedAccessException will be thrown if stream was opened only for writing
                    throw new UnauthorizedAccessException();
                }
                /*else if (error == (int)Win32Error.ERROR_SECTOR_NOT_FOUND)
                {
                    string message = String.Format("Could not read from position {0} the requested number of bytes ({1}). The sector does not exist.", this.Position, count);
                    throw new SectorNotFoundException(message);
                }*/
                else if (error == (int)Win32Error.ERROR_CRC)
                {
                    throw new IOException("Data error (cyclic redundancy check).");
                }
                else if (error == (int)Win32Error.ERROR_NO_SYSTEM_RESOURCES)
                {
                    throw new OutOfMemoryException();
                }
                else
                {
                    string message = String.Format("Could not read");
                    if (this.CanSeek)
                    {
                        message += String.Format(" from position {0}", this.Position);
                    }
                    message += String.Format(" the requested number of bytes ({0}). Win32 Error: {1}", count, error);
                    throw new IOException(message);
                }
            }
        }

        // we are working with disks, and we are only supposed to read sectors
        public override int ReadByte()
        {
            throw new NotImplementedException("Cannot read a single byte from disk");
        }

        public void Close(bool releaseHandle)
        {
            m_releaseHandle = releaseHandle;
            this.Close();
        }

        /// <summary>
        /// This will prevent the handle from being disposed
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (m_releaseHandle)
            {
                base.Dispose(disposing);
            }
            else
            {
                try
                {
                    this.Flush();
                }
                catch
                { 
                }
            }
        }
    }
}
