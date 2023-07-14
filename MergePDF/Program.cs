using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Text.RegularExpressions; 
namespace MergePDF
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                string[] lstFiles = new string[3];

                Console.WriteLine("Enter your first Pdf Path:");
                string path = Console.ReadLine();
                lstFiles[0] = path;
                Console.WriteLine("Enter your second Pdf Path:");
                string path2 = Console.ReadLine();
                lstFiles[1] = path2;

                Console.WriteLine("Enter output pdf  path with name:");
                string outputPdfPath = Console.ReadLine();
                PdfReader reader = null;
                Document sourceDocument = null;
                PdfCopy pdfCopyProvider = null;
                PdfImportedPage importedPage;
               //string outputPdfPath =  @"D:/OWN/MergePDF/MergePDF/Cc.pdf"; 

                sourceDocument = new Document();
                pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

                //Open the output file
                sourceDocument.Open();

                try
                {
                    //Loop through the files list
                    for (int f = 0; f < lstFiles.Length - 1; f++)
                    {
                        int pages = GetPageCount(lstFiles[f]);

                        reader = new PdfReader(lstFiles[f]);
                        //Add pages of current file
                        for (int i = 1; i <= pages; i++)
                        {
                            importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                            pdfCopyProvider.AddPage(importedPage);
                        }

                        reader.Close();
                    }
                    //At the end save the output file
                    sourceDocument.Close();
                    Console.WriteLine("Find merged file on below path");
                    Console.WriteLine(outputPdfPath);                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); 
                }


            }
            catch (Exception ex)
            {
                 
            }
          ;
        }
        static int GetPageCount(string file)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(file)))
            {
                Regex regex = new Regex(@"/Type\s*/Page[^s]");
                MatchCollection matches = regex.Matches(sr.ReadToEnd());

                return matches.Count;
            }
        }
    }
}
