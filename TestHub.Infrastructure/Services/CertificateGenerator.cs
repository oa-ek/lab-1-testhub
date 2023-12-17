using iTextSharp.text.pdf;
using TestHub.Core.Dtos;
using Path = System.IO.Path;

namespace TestHub.Infrastructure.Services
{
    public static class CertificateGenerator
    {
        public static FileDto? GenerateCertificate(string userName, string level)
        {
            try
            {
                var src = GetPdfTemplatePath(level);
                var dest = GetCertificateFilePath(userName, level);

                ReplaceTextAndSave(src, dest, userName);

                return CreateFileDto(dest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating certificate: {ex.Message}");
                return null; 
            }
        }

        private static string GetPdfTemplatePath(string level)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "src", "images", level + ".pdf");
        }

        private static string GetCertificateFilePath(string userName, string level)
        {
            var uniqueFileName = $"{userName}_{Guid.NewGuid().ToString()[..6]}_{level}";
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "src", "files", $"{uniqueFileName}.pdf");
        }

        private static void ReplaceTextAndSave(string inputFilePath, string outputFilePath, string userName)
        {
            try
            {
                using var fs = new FileStream(outputFilePath, FileMode.Create);
                using var pdfReader = new PdfReader(inputFilePath);
                using var pdfStamper = new PdfStamper(pdfReader, fs);

                // Get AcroFields
                AcroFields acroFields = pdfStamper.AcroFields;

                // Replace "Date" and "UserName" fields
                acroFields.SetField("Date", DateTime.Now.ToString("dd/MM/yyyy"));
                acroFields.SetField("UserName", userName);

                // Disable actions and make fields read-only
                acroFields.SetFieldProperty("Date", "setfflags", PdfFormField.FF_READ_ONLY, null);
                acroFields.SetFieldProperty("UserName", "setfflags", PdfFormField.FF_READ_ONLY, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error replacing text and saving: {ex.Message}");
            }
        }
        
        private static FileDto? CreateFileDto(string filePath)
        {
            try
            {
                return new FileDto
                {
                    FileName = Path.GetFileName(filePath),
                    ContentType = "application/pdf",
                    Data = File.ReadAllBytes(filePath)
                };
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error creating FileDto: {ex.Message}");
                return null;
            }
        }
    }
}