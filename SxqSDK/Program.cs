namespace SxqCore
{
    using System;
    using SxqCore.Tools;

    class Program
    {

        static private void GetFileToBase64Str(string pdfPath)
        {
            string contractBase64Str = QuickSignUtil.FileToBase64Str(pdfPath);
            Console.WriteLine("ContractBase64({0}): {1}", contractBase64Str.Length, contractBase64Str);
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("GainNo: {0}", QuickSignUtil.GainNo());
            string contractPdfPath1 = "/Users/ben/Downloads/TwoPeople.pdf";
            string contractPdfPath2 = "/Users/ben/Downloads/TwoPeopleLarge.pdf";
            GetFileToBase64Str(contractPdfPath1);
            GetFileToBase64Str(contractPdfPath2);
        }

    }
}
