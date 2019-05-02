﻿using System.IO;
using ApprovalTests;
using ApprovalTests.Namers;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

namespace AsposeApprovalTests
{
    public static partial class AsposeApprovals
    {
        public static void VerifyPdf(string path)
        {
            using (var document = new Document(path))
            {
                VerifyPdf(document);
            }
        }

        public static void VerifyPdf(Stream stream)
        {
            using (var document = new Document(stream))
            {
                VerifyPdf(document);
            }
        }

        static void VerifyPdf(Document document)
        {
            foreach (var page in document.Pages)
            {
                using (NamerFactory.AsEnvironmentSpecificTest(() => $"{page.Number}"))
                using (var outputStream = new MemoryStream())
                {
                    var pngDevice = new PngDevice();
                    pngDevice.Process(page, outputStream);
                    VerifyBinary(outputStream, page.Number, document.Pages.Count);
                }
            }
        }
    }
}