using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Extensions.Logging;
using TestHub.Core.Dtos;

namespace TestHub.Infrastructure.Services
{
    public class FileService
    {
        private readonly ILogger<FileService> _logger;

        private static readonly string ApiKey = "AIzaSyC497Zyc570AkjlYpqgsjMjc2Z1VQoPn-I";
        private static readonly string Bucket = "testhub-aspdotnetwithmvc.appspot.com";
        private static readonly string AuthEmail = "test@gmail.com";
        private static readonly string AuthPwd = "123567";

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        public async Task<string> UploadImage(FileDto file)
        {
            if (file.Data.Length == 0) return string.Empty;

            string uniqueFileName = GetUniqueFileName(file.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "src", "images", uniqueFileName);

            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await fileStream.WriteAsync(file.Data, 0, file.Data.Length);
            }

            _logger.LogInformation($"Uploading file: {file.FileName}");
             string link = await Upload(new FileStream(path, FileMode.Open), uniqueFileName);
            _logger.LogInformation($"File uploaded: {file.FileName}");

            return link;
        }

        private async Task<string> Upload(FileStream stream, string fileName)
        {
            string link = string.Empty;
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPwd);

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                .Child("images")
                .Child(fileName)
                .PutAsync(stream, cancellation.Token);

            try
            {
                link = await task;
                _logger.LogInformation($"File '{fileName}' uploaded successfully. Download link: {link}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while uploading file '{fileName}': {ex.Message}");
            }

            return link;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 6)
                   + Path.GetExtension(fileName);
        }
    }
}