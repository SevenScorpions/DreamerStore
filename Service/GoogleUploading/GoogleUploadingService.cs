using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class GoogleUploadingService
{
    private readonly DriveService _driveService;
    private const string FolderId = "1R_HeVqALg8t_Qk_K4MCsYJzus3u5zXET";

    public GoogleUploadingService(string credentialPath)
    {
        GoogleCredential credential;

        using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream).CreateScoped(new[]
            {
                    DriveService.ScopeConstants.DriveFile
            });
        }

        _driveService = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "Google Drive API .NET",
        });
    }

    public async Task<string> UploadImage(IFormFile photo)
    {
        var fileMetadata = new Google.Apis.Drive.v3.Data.File()
        {
            Name = photo.FileName,
            Parents = new List<string> { FolderId }
        };

        FilesResource.CreateMediaUpload request;
        using (var stream = photo.OpenReadStream())
        {
            request = _driveService.Files.Create(fileMetadata, stream, photo.ContentType);
            request.Fields = "id";
            await request.UploadAsync();
        }

        var file = request.ResponseBody;
        return file.Id;
    }

    public string GetImage(string id)
    {
        var request = _driveService.Files.Get(id);
        var stream = new MemoryStream();
        request.MediaDownloader.ProgressChanged += (IDownloadProgress progress) =>
        {
            switch (progress.Status)
            {
                case DownloadStatus.Downloading:
                    break;
                case DownloadStatus.Completed:
                    break;
                case DownloadStatus.Failed:
                    throw new Exception("Failed to download the image.");
            }
        };

        request.Download(stream);
        stream.Position = 0;
        var dataUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(stream.ToArray())}";
        return dataUrl;
    }

    public void DeleteImage(string id)
    {
        if(id.IsNullOrEmpty()) return;
        _driveService.Files.Delete(id).Execute();
    }
}