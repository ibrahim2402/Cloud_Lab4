using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebRole1.Models;

namespace WebRole1
{
        /*This class handle the blob User interface
         * Holds and initialize account name and storage key of azure account as storage credential
         * Create the table for comments, blob upload/ load and list blob files
         */
    public class BlobHelper
    {
        private CloudStorageAccount account;
        private CloudBlobClient client;
        private CloudBlobContainer container;

        private CloudTableClient cloudTableClient;
        private CloudTable table;
        private StorageCredentials storageCred;
        private string acctName;
        private string storageKey;

        public BlobHelper()
        {

            // Create connection and storage Blob if not exist
            // Change the credential to valid azure storage account details

            acctName = "xxxxxxxxx"; // Blob accont name
            storageKey = "xxxxxxx00000"; // storage key value

            storageCred = new StorageCredentials(acctName, storageKey);
            account = new CloudStorageAccount(storageCred, true);
            client = account.CreateCloudBlobClient();
            container = client.GetContainerReference("blobs");
            container.CreateIfNotExists();

                 // Create connection and storage table if not exist

            cloudTableClient = account.CreateCloudTableClient();
            table = cloudTableClient.GetTableReference("table_comments");
            table.CreateIfNotExists();
        }


        // Upload file to blob
        public bool UploadBlob(string filePath, string fileName, string userId, string description)
        {
            try
            {
                CloudBlockBlob blob = container.GetBlockBlobReference(fileName);     // the name of the image

                using (Stream file = File.OpenRead(filePath))    // image's path
                {
                    // image's meta data
                    blob.Metadata["owner"] = userId;
                    blob.Metadata["description"] = description;
                    blob.Metadata["likes"] = 0 + "";
                    blob.Metadata["dislikes"] = 0 + "";
                    blob.Metadata["downloads"] = 0 + "";
                    blob.UploadFromStream(file);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

                 // Get list of blob files
        public async Task<List<string>> GetBlobs()
        {
            BlobContinuationToken blobContinuationToken = null;
            List<string> uris = new List<string>();
            try
            {
                do
                {
                    var results = await container.ListBlobsSegmentedAsync(null, blobContinuationToken);
                    
                           // Get the value of the token.
                    blobContinuationToken = results.ContinuationToken;

                    foreach (IListBlobItem item in results.Results)
                    {
                        uris.Add(item.Uri.Segments.Last());
                    }

                    // Loop continuation token if not null

                } while (blobContinuationToken != null); 

                return uris;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

            // Get blob block(image) description

        public string GetDescription(string blobName)
        {
            var blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.FetchAttributes();
            string description = blockBlob.Metadata["description"];

            return description;
        }

              // Get blob block(image) likes
        public void Like(string blobName)
        {
            int like = GetLikes(blobName);
            like++;
            var blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.FetchAttributes();
            blockBlob.Metadata["likes"] = like + "";
            blockBlob.SetMetadata();
        }

             // Get blob block(image) dislike
        public void Dislike(string blobName)
        {
            int dislike = GetDislikes(blobName);
            dislike++;
            var blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.FetchAttributes();
            blockBlob.Metadata["dislikes"] = dislike + "";
            blockBlob.SetMetadata();
        }

             // Get blob block(image) No. of likes
        public int GetLikes(string blobName)
        {
            var blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.FetchAttributes();
            int likes = int.Parse(blockBlob.Metadata["likes"]);

            return likes;
        }

              // Get blob block(image) No. of dislikes
        public int GetDislikes(string blobName)
        {
            var blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.FetchAttributes();
            int dislikes = int.Parse(blockBlob.Metadata["dislikes"]);

            return dislikes;
        }

            // Download blob
        public void Download(string blobName)
        {
            int downloads = GetNumOfDownloads(blobName);
            downloads++;
            var blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.FetchAttributes();
            blockBlob.Metadata["downloads"] = downloads + "";
            blockBlob.SetMetadata();
        }

        // No. of blob download
        public int GetNumOfDownloads(string blobName)
        {
            var blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.FetchAttributes();
            int downloads = int.Parse(blockBlob.Metadata["downloads"]);

            return downloads;
        }

                  // Get Blob owner
        public string getOwner(string blobName)
        {

            var blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.FetchAttributes();
            string owner = blockBlob.Metadata["owner"];

            return owner;
        }

              // Delete a blob
        public bool DeleteBlob(string blobName)
        {
            try
            {
                DeleteComments(blobName);

                CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
                blob.FetchAttributes();

                blob.Delete();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Add comment image file, insert to table.
        public void AddComment(string comment, string blobName, string userId)
        {
            Comment c = new Comment()
            {
                CommentText = comment
            };
            c.PartitionKey = blobName;
            c.RowKey = userId;

            TableOperation insert = TableOperation.Insert(c);
            table.Execute(insert);
        }

             // List of comments to image
        public List<Comment> GetComments(string blobName)
        {
            var entities = table.ExecuteQuery(new TableQuery<Comment>()).ToList();
            List<Comment> comments = new List<Comment>();

            foreach (Comment entity in entities)
            {
                if (entity.PartitionKey == blobName)
                {
                    comments.Add(entity);
                }
            }

            return comments;
        }

              // Delete image file comments
        public void DeleteComments(string blobName)
        {
            var entities = table.ExecuteQuery(new TableQuery<Comment>()).ToList();

            foreach (var entity in entities)
            {
                if (entity.PartitionKey == blobName)
                {
                    table.Execute(TableOperation.Delete(entity));
                }
            }
        }

    }
}