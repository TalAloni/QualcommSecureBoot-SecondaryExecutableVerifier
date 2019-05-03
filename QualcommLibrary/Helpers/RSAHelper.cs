using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Org.BouncyCastle;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.X509;

namespace QualcommLibrary
{
    public class RSAHelper
    {
        public static byte[] DecryptSignature(byte[] signatureBytes, RSAParameters rsaParameters)
        {
            RsaKeyParameters publicKey = new RsaKeyParameters(false, new BigInteger(1, rsaParameters.Modulus), new BigInteger(rsaParameters.Exponent));
            IBufferedCipher cipher = CipherUtilities.GetCipher("RSA/NONE/PKCS1Padding");
            cipher.Init(false, publicKey);

            byte[] decrypted = cipher.DoFinal(signatureBytes);
            return decrypted;
        }
    }
}
