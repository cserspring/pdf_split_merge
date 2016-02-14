// ----------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//     All rights reserved.
// </copyright>
// <author>
//     cserspring@github
// </author>
// <summary>
//     PDF file splitter and merger.
// </summary>
//-------------------------------------------------------------------------------------------------

namespace PDFSplitMerge
{
    using System;
    using System.Linq;

    /// <summary>
    /// PDF file splitter and merger.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int argsLen = args.Length;
            const string usage = "-usage\n" +
                                 "      Print the usage information.\n" +
                                 "-split\n" +
                                 "      Please use \"PDFSplitMerge.exe -split [SourceFile] [Pages] [OutputFolder]\", DO NOT contain any space in [Pages].\n" +
                                 "      For example:\n" +
                                 "      PDFSplitMerge.exe -split c:\\Users\\foo\\Desktop\\bar.pdf 1,2,3-4 c:\\Users\\foo\\Desktop\\\n" +
                                 "-merge\n" +
                                 "      Please use \"PDFSplitMerge.exe -merge [File1] [File2] ... [FileN] [OutputFolder]\"\n" +
                                 "      For example:\n" +
                                 "      PDFSplitMerge.exe -merge c:\\Users\\foo\\Desktop\\bar.pdf c:\\Users\\foo\\Desktop\\bar2.pdf c:\\Users\\foo\\Desktop\\";
            if (argsLen >= 1 && args[0].Equals("-usage", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(usage);
            }
            else if (argsLen == 4 && args[0].Equals("-split", StringComparison.OrdinalIgnoreCase))
            {
                string sourceFile = args[1];
                string sourceFileNameWithExtension = sourceFile.Split(new char[] {'\\'}, StringSplitOptions.RemoveEmptyEntries).Last();
                string sourceFileName = sourceFileNameWithExtension.Substring(0, sourceFileNameWithExtension.LastIndexOf('.'));

                string outputPath = args[3].EndsWith("\\") ? args[3] : (args[3] + "\\");

                string[] arr = args[2].Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < arr.Length; i++)
                {
                    string[] startAndEnd = arr[i].Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries);
                    int start = int.Parse(startAndEnd[0]);
                    if (startAndEnd.Length > 1)
                    {
                        int end = int.Parse(startAndEnd[1]);
                        PdfUtilities.ExtractPages(sourceFile, outputPath + sourceFileName + "_" + start + "_to_" + end + ".pdf", start, end);
                    }
                    else
                    {
                        PdfUtilities.ExtractPage(sourceFile, outputPath + sourceFileName + "_" + start + ".pdf", start);
                    }
                }
            }
            else if (argsLen >= 4 && args[0].Equals("-merge", StringComparison.OrdinalIgnoreCase))
            {
                string[] sources = new string[argsLen - 2];
                string outputPath = args[argsLen - 1].EndsWith("\\") ? args[argsLen - 1] : (args[argsLen - 1] + "\\");

                for (int i = 0; i < argsLen - 2; i++)
                {
                    sources[i] = args[i + 1];
                }

                PdfUtilities.Merge(sources, outputPath + "Merged" + ".pdf");
            }
        }
    }
}
