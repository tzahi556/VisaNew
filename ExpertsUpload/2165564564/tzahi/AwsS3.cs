using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BalcarNew.AppCode
{
    public class AwsS3
    {



        string AWS_bucketName = "balcar";
        string AWS_defaultFolder = "CarLicenceFiles";

        private string AWS_accessKey;

        private string AWS_secretKey;

        public AwsS3(string AccessKey , string secretKey) {

            AWS_accessKey = AccessKey;
         
            AWS_secretKey = secretKey;

        }
        
        // you must set your accessKey and secretKey
        // for getting your accesskey and secretKey go to your Aws amazon console



        public async Task UploadImage(IFormFile file)
        {

            try
            {


                //var credentials = new BasicAWSCredentials(AWS_accessKey, AWS_secretKey);
                var config = new AmazonS3Config
                {
                    RegionEndpoint = Amazon.RegionEndpoint.EUNorth1
                };
                using var client = new AmazonS3Client(AWS_accessKey, AWS_secretKey, config);
                await using var newMemoryStream = new MemoryStream();
                file.CopyTo(newMemoryStream);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = newMemoryStream,
                    Key = AWS_defaultFolder + "/" + file.FileName,
                    BucketName = AWS_bucketName,
                    CannedACL = S3CannedACL.NoACL
                };

                var fileTransferUtility = new TransferUtility(client);
                await fileTransferUtility.UploadAsync(uploadRequest);


            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<Stream> ReadObjectData( string fileName)
        {
            var credentials = new BasicAWSCredentials(AWS_accessKey, AWS_secretKey);
            var config = new AmazonS3Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.EUNorth1
            };
            
            try
            {
                using (var client = new AmazonS3Client(credentials, config))
                {
                    var request = new GetObjectRequest
                    {
                        BucketName = AWS_bucketName,
                        Key = AWS_defaultFolder + "/" + fileName
                    };

                    using (var getObjectResponse = await client.GetObjectAsync(request))
                    {
                        using (var responseStream = getObjectResponse.ResponseStream)
                        {
                            var stream = new MemoryStream();
                            await responseStream.CopyToAsync(stream);
                            stream.Position = 0;
                            return stream;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Read object operation failed.", exception);
            }
        }


        //public async Task<IActionResult> GetProfilePicture()
        //{
        //    var user = await GetUserFromBearerToken();

        //    Stream imageStream = await _fileService.ReadObjectData(MediaFolder.Profiles, user.ProfileImageFileName);

        //    Response.Headers.Add("Content-Disposition", new ContentDisposition
        //    {
        //        FileName = "Image.jpg",
        //        Inline = true // false = prompt the user for downloading; true = browser to try to show the file inline
        //    }.ToString());

        //    return File(imageStream, "image/jpeg");
        //}
    }
}
