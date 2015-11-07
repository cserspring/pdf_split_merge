namespace PDFSplitMerge
{
    using System;
    using System.IO;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    /// <summary>
    /// To manipulate the pdf files.
    /// </summary>
    class PdfUtilities
    {
        /// <summary>
        /// Extract a single page from PDF file.
        /// </summary>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="outputFile">The output file.</param>
        /// <param name="pageNumber">The specific page number.</param>
        public static void ExtractPage(string sourceFile, string outputFile, int pageNumber)
        {
            try
            {
                PdfReader reader = new PdfReader(sourceFile);
                if (pageNumber > reader.NumberOfPages)
                {
                    Console.WriteLine("This page number is out of reach.");
                    return;
                }

                Document document = new Document();
                PdfCopy pdfCopy = new PdfCopy(document, new FileStream(outputFile, FileMode.Create));
                document.Open();

                PdfImportedPage importedPage = pdfCopy.GetImportedPage(reader, pageNumber);
                pdfCopy.AddPage(importedPage);

                document.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Extract pages from a pdf file.
        /// </summary>
        /// <param name="source">The source file.</param>
        /// <param name="output">The output file.</param>
        /// <param name="startPage">The start page.</param>
        /// <param name="endPage">The last page.</param>
        public static void ExtractPages(string source, string output, int startPage, int endPage)
        {
            try
            {
                PdfReader reader = new PdfReader(source);
                Document document = new Document();

                PdfCopy pdfCopy = new PdfCopy(document, new FileStream(output, FileMode.Create));

                document.Open();
                for (int i = startPage; i <= endPage; i++)
                {
                    PdfImportedPage importedPage = pdfCopy.GetImportedPage(reader, i);
                    pdfCopy.AddPage(importedPage);
                }

                document.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Merge multiple pdf files into one pdf file.
        /// </summary>
        /// <param name="sources">The source files.</param>
        /// <param name="outputFile">The output file.</param>
        public static void Merge(string[] sources, string outputFile)
        {
            try
            {
                Document document = new Document();
                PdfCopy pdfCopy = new PdfCopy(document, new FileStream(outputFile, FileMode.Create));

                document.Open();
                foreach (var source in sources)
                {
                    PdfReader reader = new PdfReader(source);
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        PdfImportedPage page = pdfCopy.GetImportedPage(reader, i);
                        pdfCopy.AddPage(page);
                    }
                    reader.Close();
                }

                pdfCopy.Close();
                document.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
