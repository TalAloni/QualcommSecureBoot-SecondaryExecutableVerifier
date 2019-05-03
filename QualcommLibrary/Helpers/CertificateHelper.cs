using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Utilities;

namespace QualcommLibrary
{
    public class CertificateHelper
    {
        public static RSAParameters GetRSAParameters(string certificatePath)
        {
            byte[] certificateBytes = File.ReadAllBytes(certificatePath);
            return GetRSAParameters(certificateBytes);
        }

        public static RSAParameters GetRSAParameters(byte[] certificateBytes)
        {
            X509Certificate2 x509certificate = new X509Certificate2(certificateBytes);
            if (x509certificate.HasPrivateKey)
            {
                RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)x509certificate.PrivateKey;
                return rsa.ExportParameters(true);
            }
            else
            {
                RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)x509certificate.PublicKey.Key;
                return rsa.ExportParameters(false);
            }
        }

        public static X509Certificate2 GetX509Certificate2(string certificatePath)
        {
            byte[] certificateBytes = File.ReadAllBytes(certificatePath);
            X509Certificate2 x509certificate = new X509Certificate2(certificateBytes);
            return x509certificate;
        }

        /// <summary>
        /// TBS: To Be Signed - the portion of the X.509 certificate that is signed with the CA's private key.
        /// </summary>
        // http://www.codeproject.com/Questions/252741/Where-is-the-signature-value-in-the-certificate
        public static byte[] ExtractTbsCertificate(byte[] certificateBytes)
        {
            if (certificateBytes[0] == 0x30 && certificateBytes[1] == 0x82)
            {
                if (certificateBytes[4] == 0x30 && certificateBytes[5] == 0x82)
                {
                    ushort length = BigEndianConverter.ToUInt16(certificateBytes, 6);
                    return ByteReader.ReadBytes(certificateBytes, 4, 4 + (int)length);
                }
            }
            throw new ArgumentException("The given certificate is not a BER-encoded ASN.1 X.509 certificate");
        }
    }
}
