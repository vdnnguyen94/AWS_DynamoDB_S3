using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using DotNetEnv;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _301289600Nguyen_Lab2
{
    public static class Helper
    {
        //get S3 AWS
        public static readonly IAmazonS3 s3Client;

        // Shared MemoryStream for the selected book
        public static MemoryStream SelectedBookStream { get; set; }

        // Optional metadata for tracking
        public static string SelectedS3Key { get; set; }
        public static string SelectedTitle { get; set; }

        static Helper()
        {
            Env.Load();

            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
            var regionName = Environment.GetEnvironmentVariable("AWS_REGION") ?? "us-east-2";

            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var region = RegionEndpoint.GetBySystemName(regionName);

            s3Client = new AmazonS3Client(credentials, region);
        }
    }
}
