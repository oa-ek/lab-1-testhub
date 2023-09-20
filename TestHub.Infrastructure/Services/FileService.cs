using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Extensions.Logging;

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

        public async Task Upload(FileStream stream, string fileName)
        {
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
                string link = ("Download link:\n" + await task);
                _logger.LogInformation($"File '{fileName}' uploaded successfully. Download link: {link}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while uploading file '{fileName}': {ex.Message}");
            }
        }
        
        public string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" + Guid.NewGuid().ToString().Substring(0, 6)
                   + Path.GetExtension(fileName);
        }

    }
}