using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Utilities;

namespace QualcommLibrary
{
    public class ApplicationExecutableHelper
    {
        public static byte[] ExtractCode(byte[] imageBytes)
        {
            ApplicationExecutableHeader header = new ApplicationExecutableHeader(imageBytes);
            return ByteReader.ReadBytes(imageBytes, (int)ApplicationExecutableHeader.CodeStartOffset, (int)header.CodeSize);
        }

        public static byte[] ExtractSignature(byte[] imageBytes)
        {
            ApplicationExecutableHeader header = new ApplicationExecutableHeader(imageBytes);
            return ByteReader.ReadBytes(imageBytes, (int)header.SignatureOffset, (int)header.SignatureLength);
        }

        public static List<byte[]> ExtractCertificates(byte[] imageBytes)
        {
            List<byte[]> certificates = new List<byte[]>();
            ApplicationExecutableHeader header = new ApplicationExecutableHeader(imageBytes);
            byte[] certificateStoreBytes = ByteReader.ReadBytes(imageBytes, (int)header.CertificateStoreOffset, (int)header.CertificateStoreLength);
            int offset = 0;
            while (offset < certificateStoreBytes.Length - 4)
            {
                ushort type = BigEndianReader.ReadUInt16(certificateStoreBytes, ref offset);
                if (type == 0x3082) // ASN.1 SEQUENCE
                {
                    ushort length = BigEndianReader.ReadUInt16(certificateStoreBytes, ref offset);
                    offset -= 4;
                    byte[] certificateBytes = ByteReader.ReadBytes(certificateStoreBytes, ref offset, length + 4);
                    certificates.Add(certificateBytes);
                }
                else
                {
                    break;
                }
            }
            return certificates;
        }

        public static void ExtractFileComponents(string imagePath, string outputDirectory)
        {
            string filenameWithoutExtention = Path.GetFileNameWithoutExtension(imagePath);
            if (!outputDirectory.EndsWith("\\"))
            {
                outputDirectory += "\\";
            }
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            ApplicationExecutableHeader header = new ApplicationExecutableHeader(imageBytes);
            string codeFilename = String.Format("{0}\\{1}-code-0x{2}.bin", outputDirectory, filenameWithoutExtention, header.LoadingAddress.ToString("X"));
            string signatureFilename = String.Format("{0}\\{1}-Signature.bin", outputDirectory, filenameWithoutExtention);

            byte[] code = ExtractCode(imageBytes);
            File.WriteAllBytes(codeFilename, code);
            byte[] signatureBytes = ExtractSignature(imageBytes);
            File.WriteAllBytes(signatureFilename, signatureBytes);
            List<byte[]> certificates = ExtractCertificates(imageBytes);
            for (int index = 0; index < certificates.Count; index++)
            {
                string certificateFilename = String.Format("{0}\\{1}-Certificate-L{2}.cer", outputDirectory, filenameWithoutExtention, index + 1);
                File.WriteAllBytes(certificateFilename, certificates[index]);
            }
        }
    }
}
