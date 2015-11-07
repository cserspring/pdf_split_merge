namespace PDFSplitMerge
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            const string usage = "PDFSplitMerge.exe -split [SourceFile] [Pages] [OutputFolder]\n" +
                                 "PDFSplitMerge.exe -merge [File1] [File2] ... [FileN] [OutputFolder]";
            if (args[0].Equals("-usage", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(usage);
            }
            else if (args[0].Equals("-split", StringComparison.OrdinalIgnoreCase))
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
            else if (args[0].Equals("-merge", StringComparison.OrdinalIgnoreCase))
            {
                string[] sources = new string[args.Length - 2];
                string outputPath = args[3].EndsWith("\\") ? args[3] : (args[3] + "\\");

                for (int i = 0; i < args.Length - 2;i++)
                {
                    sources[i] = args[i + 1];
                }

                PdfUtilities.Merge(sources, outputPath + "Merged" + ".pdf");
            }
        }
    }
}
