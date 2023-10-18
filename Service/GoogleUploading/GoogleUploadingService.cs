using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.IO;
namespace DreamerStore2.Service.GoogleUploadingService
{
    internal class GoogleUploadingService
    {
        private static GoogleUploadingService instance;
        private GoogleUploadingService() { }
        public static GoogleUploadingService Instance
        {
            get {
                if (instance == null)
                {
                            instance = new GoogleUploadingService();
                }
                return instance; 
            }
        }
        public void Upload(IFormFile photo)
        {
            GoogleCredential credential;

            using(var stream = new FileStream("Service/GoogleUploading/cre.json",FileMode.Open,FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(new[]
                {
                    DriveService.ScopeConstants.DriveFile
                });
                UploadFile(photo, "1R_HeVqALg8t_Qk_K4MCsYJzus3u5zXET", credential);
            }

        }
        private Google.Apis.Drive.v3.Data.File UploadFile(IFormFile formFile, string folderId, GoogleCredential credential)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = formFile.FileName,
                Parents = new List<string> { folderId } // Thay "folderId" bằng ID của thư mục cha
            };

            using (var stream = formFile.OpenReadStream())
            {
                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Your Application Name"
                });

                var request = service.Files.Create(fileMetadata, stream, formFile.ContentType);
                request.Fields = "id"; // Lựa chọn các trường cần trả về cho tệp tin tải lên
                request.Upload();

                var uploadedFile = request.ResponseBody;
                return uploadedFile;
            }
        }
    }
}
