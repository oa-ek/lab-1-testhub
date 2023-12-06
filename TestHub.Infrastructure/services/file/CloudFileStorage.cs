using Application.contracts.infrastructure;
using Application.contracts.infrastructure.file;
using Firebase.Auth;
using Firebase.Storage;

namespace TestHub.Infrastructure.services.file
{
    public class CloudFileStorage : ICloudFileStorage
    {
        private static readonly string ApiKey = "AIzaSyC497Zyc570AkjlYpqgsjMjc2Z1VQoPn-I";
        private static readonly string Bucket = "testhub-aspdotnetwithmvc.appspot.com";
        private static readonly string AuthEmail = "test@gmail.com";
        private static readonly string AuthPwd = "123567";

        public async Task<string> UploadToCloud(FileStream stream, string fileName)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var authResult = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPwd);

                var storage = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(authResult.FirebaseToken),
                        ThrowOnCancel = true
                    });

                var cancellation = new CancellationTokenSource();
                var task = storage
                    .Child("images")
                    .Child(fileName)
                    .PutAsync(stream, cancellation.Token);

                return await task;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in UploadToCloud: {ex.Message}");
            }
        }
    }
}