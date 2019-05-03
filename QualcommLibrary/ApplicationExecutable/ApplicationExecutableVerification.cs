using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Utilities;

namespace QualcommLibrary
{
    public class ApplicationExecutableVerification
    {
        public static byte[] GetRootCertificateHash(byte[] imageBytes)
        {
            List<byte[]> certificates = ApplicationExecutableHelper.ExtractCertificates(imageBytes);
            int rootCertificateIndex = CertificateValidationHelper.GetRootCertificateIndex(certificates);
            if (rootCertificateIndex == -1)
            {
                return null;
            }

            byte[] rootCertificate = certificates[rootCertificateIndex];
            return SHA256.Create().ComputeHash(rootCertificate);
        }

        public static bool VerifyRootCertificateHash(byte[] imageBytes, byte[] oemPKHash)
        {
            byte[] rootCertificateHash = GetRootCertificateHash(imageBytes);
            if (rootCertificateHash == null)
            {
                return false;
            }
            return ByteUtils.AreByteArraysEqual(rootCertificateHash, oemPKHash);
        }

        public static bool VerifyCertificateStore(byte[] imageBytes)
        {
            List<byte[]> certificates = ApplicationExecutableHelper.ExtractCertificates(imageBytes);
            return CertificateValidationHelper.VerifyCertificateChain(certificates);
        }

        public static bool VerifyImageSignature(byte[] imageBytes, byte[] softwareID, byte[] hardwareID)
        {
            if (softwareID.Length != 8)
            {
                throw new ArgumentException("SoftwareID should be 8 bytes long");
            }

            if (hardwareID.Length != 8)
            {
                throw new ArgumentException("HardwareID should be 8 bytes long");
            }

            byte[] expectedHash = DecryptFileSignature(imageBytes);

            byte[] imageHeader = ByteReader.ReadBytes(imageBytes, 0, ApplicationExecutableHeader.Length);
            byte[] codeBytes = ApplicationExecutableHelper.ExtractCode(imageBytes);

            HashAlgorithm hashAlgorithm;
            if (expectedHash.Length == 20)
            {
                hashAlgorithm = SHA1.Create();
            }
            else if (expectedHash.Length == 32)
            {
                hashAlgorithm = SHA256.Create();
            }
            else
            {
                throw new Exception("Unknown hash algorithm");
            }
            byte[] message = hashAlgorithm.ComputeHash(ByteUtils.Concatenate(imageHeader, codeBytes));
            byte[] hash = HMAC(softwareID, hardwareID, message, hashAlgorithm);
            return ByteUtils.AreByteArraysEqual(expectedHash, hash);
        }

        public static byte[] DecryptFileSignature(byte[] imageBytes)
        {
            List<byte[]> certificates = ApplicationExecutableHelper.ExtractCertificates(imageBytes);
            if (certificates.Count > 0)
            {
                byte[] certificateBytes = certificates[0];
                byte[] signatureBytes = ApplicationExecutableHelper.ExtractSignature(imageBytes);
                RSAParameters rsaParameters = CertificateHelper.GetRSAParameters(certificateBytes);
                byte[] decodedHash = RSAHelper.DecryptSignature(signatureBytes, rsaParameters);
                return decodedHash;
            }
            else
            {
                throw new Exception("According to the header, the file does not contain a certificate");
            }
        }

        private static byte[] HMAC(byte[] i_key, byte[] o_key, byte[] message, HashAlgorithm hashAlgorithm)
        {
            if (i_key.Length != 8 || o_key.Length != 8)
            {
                throw new ArgumentException("i_key and o_key must be 8 bytes long");
            }
            byte[] i_key_pad = new byte[8];
            byte[] o_key_pad = new byte[8];
            for (int index = 0; index < 8; index++)
            {
                i_key_pad[index] = (byte)(i_key[index] ^ 0x36);
                o_key_pad[index] = (byte)(o_key[index] ^ 0x5C);
            }

            byte[] innerHash = hashAlgorithm.ComputeHash(ByteUtils.Concatenate(i_key_pad, message));
            byte[] outerHash = hashAlgorithm.ComputeHash(ByteUtils.Concatenate(o_key_pad, innerHash));
            return outerHash;
        }
    }
}
