using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using QualcommLibrary;
using Utilities;

namespace SecondaryExecutableVerifier
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog1.FileName;
                CleanStatus();
            }
        }

        private void CleanStatus()
        {
            lblRootCertificateStatus.Text = String.Empty;
            lblCertificateStoreStatus.Text = String.Empty;
            lblImageSignatureStatus.Text = String.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text += String.Format(" {0}", version.ToString(3));
            CleanStatus();
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            Verify();
        }

        private void Verify()
        {
            byte[] hardwareID = null;
            try
            {
                hardwareID = ConvertHexStringToByteArray(txtHardwareID.Text.Replace(" ", String.Empty));
            }
            catch
            {
            }

            if (hardwareID == null || hardwareID.Length != 8)
            {
                MessageBox.Show("Invalid HW_ID");
                return;
            }

            byte[] softwareID = null;
            try
            {
                softwareID = ConvertHexStringToByteArray(txtSoftwareID.Text.Replace(" ", String.Empty));
            }
            catch
            {
            }

            if (softwareID == null || softwareID.Length != 8)
            {
                MessageBox.Show("Invalid SW_ID");
                return;
            }

            byte[] oemPKHash = null;
            try
            {
                oemPKHash = ConvertHexStringToByteArray(txtOemPKHash.Text.Replace(" ", String.Empty));
            }
            catch
            {
            }

            if (oemPKHash == null || (oemPKHash.Length != 20 && oemPKHash.Length != 32))
            {
                MessageBox.Show("Invalid OEM_PK_HASH");
                return;
            }

            byte[] imageBytes;
            try
            {
                imageBytes = File.ReadAllBytes(txtFilePath.Text);
            }
            catch
            {
                MessageBox.Show("Unable to read file");
                return;
            }

            try
            {
                bool isRootCertificateValid = SecondaryExecutableVerification.VerifyRootCertificateHash(imageBytes, oemPKHash);
                lblRootCertificateStatus.Text = isRootCertificateValid ? "OK" : "INVALID";
            }
            catch
            {
                lblRootCertificateStatus.Text = "Error";
            }

            try
            {
                bool isCertificateStoreValid = SecondaryExecutableVerification.VerifyCertificateStore(imageBytes);
                lblCertificateStoreStatus.Text = isCertificateStoreValid ? "OK" : "INVALID";
            }
            catch
            {
                lblCertificateStoreStatus.Text = "Error";
            }

            try
            {
                bool isImageSignatureValid = SecondaryExecutableVerification.VerifyImageSignature(imageBytes, softwareID, hardwareID);
                lblImageSignatureStatus.Text = isImageSignatureValid ? "OK" : "INVALID";
            }
            catch
            {
                lblImageSignatureStatus.Text = "Error";
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            byte[] imageBytes;
            try
            {
                imageBytes = File.ReadAllBytes(txtFilePath.Text);
            }
            catch
            {
                MessageBox.Show("Unable to read file");
                return;
            }

            X509Certificate2 certificate = null;
            try
            {
                List<byte[]> certificates;
                if (SecondaryExecutableHeader.IsSecondaryExecutable(imageBytes))
                {
                    certificates = SecondaryExecutableHelper.ExtractCertificates(imageBytes);
                }
                else
                {
                    certificates = ApplicationExecutableHelper.ExtractCertificates(imageBytes);
                }
                certificate = new X509Certificate2(certificates[0]);
            }
            catch
            {
                txtSoftwareID.Text = "Error";
                txtHardwareID.Text = "Error";
            }

            if (certificate != null)
            {
                txtSoftwareID.Text = GetByteArrayString(ReadHexPropertyFromCertificate(certificate, "OU=01", "SW_ID"));
                txtHardwareID.Text = GetByteArrayString(ReadHexPropertyFromCertificate(certificate, "OU=02", "HW_ID"));
            }

            try
            {
                byte[] rootCertificateHash;
                if (SecondaryExecutableHeader.IsSecondaryExecutable(imageBytes))
                {
                    rootCertificateHash = SecondaryExecutableVerification.GetRootCertificateHash(imageBytes);
                }
                else
                {
                    rootCertificateHash = ApplicationExecutableVerification.GetRootCertificateHash(imageBytes);
                }
                txtOemPKHash.Text = GetByteArrayString(rootCertificateHash);
            }
            catch
            {
                txtOemPKHash.Text = "Error";
            }
        }

        public static byte[] ReadHexPropertyFromCertificate(X509Certificate2 certificate, string propertyID, string propertyName)
        {
            string hexString = ReadPropertyFromCertificate(certificate, propertyID, propertyName);
            return ConvertHexStringToByteArray(hexString);
        }

        public static string ReadPropertyFromCertificate(X509Certificate2 certificate, string propertyID, string propertyName)
        {
            string[] entries = certificate.Subject.Split(',');
            for (int index = 0; index < entries.Length; index++)
            {
                string entry = entries[index].Trim();
                if (entry.StartsWith(propertyID) && entry.EndsWith(propertyName))
                {
                    return entry.Substring(6, entry.Length - 12).Trim();
                }
            }
            return null;
        }

        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] byteArray = new byte[hexString.Length / 2];
            for (int index = 0; index < byteArray.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                byteArray[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return byteArray;
        }

        public static string GetByteArrayString(byte[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte b in array)
            {
                builder.Append(b.ToString("X2")); // 2 digit hex
                builder.Append(" ");
            }
            return builder.ToString();
        }
    }
}