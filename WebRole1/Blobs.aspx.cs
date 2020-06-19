using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebRole1.Models;

namespace WebRole1
{
    public partial class Blobs : Page
    {
        private DbHelper dbHelper;
        private BlobHelper blobHelper;



        protected void Page_Load(object sender, EventArgs e)
        {
            dbHelper = new DbHelper();
            blobHelper = new BlobHelper();

            string role = getUserRole();

            RedirectThePage();

            PopulateImagesList();

            // this will disable all the buttons to any user that is not a customer
            if (role != "Customer")
            {
                Label1.Text = "You have to be logged as a customer to use this page";
                UploadBlobButton.Enabled = false;
            }
        }

        // this method will check the lockedoutenable status of the logged user
        // and if it is true the user is redirected to the lockout page.          
        protected async void RedirectThePage()
        {
            if (User.Identity.GetUserId() != null)
            {
                bool isEnabled = await dbHelper.GetlockoutStatus(User.Identity.GetUserId());

                if (!isEnabled)
                {
                    Response.Redirect("/Account/Lockout");
                }
            }

        }

        // to get the role of the logged user
        protected string getUserRole()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (User.Identity.GetUserId() == null) return null;
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roles = roleManager.Roles.ToList();
            foreach (IdentityRole role in roles)
            {
                string roleName = role.Name;
                if (User.IsInRole(roleName))
                {
                    return role.Name;
                }
            }
            return null;
        }

        // to upload an image, it checks if the user is logged in, and the fields are filled! then it uploads
        // note that the image has to be within the same folder.
        protected async void UploadBlobButton_Click(object sender, EventArgs e)
        {
            List<string> imgs = await blobHelper.GetBlobs();

            if (User.Identity.GetUserId() != null)
            {
                if (FileUpload.HasFile && !string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
                {
                    string filePath = Server.MapPath(FileUpload.FileName);
                    string fileName = FileUpload.FileName;
                    string userId = User.Identity.GetUserId();
                    string description = DescriptionTextBox.Text.ToString();

                    if (!imgs.Contains(fileName))
                    {
                        blobHelper.UploadBlob(filePath, fileName, userId, description);
                        UploadResults.Text = "Uploaded!";
                    }
                    else
                    {
                        UploadResults.Text = "An img with this name already exist";

                    }

                    DescriptionTextBox.Text = string.Empty;
                }
                else
                {
                    UploadResults.Text = "Make sure you file the fields";
                }
            }
        }
        // will load the image of the user choice
        protected void LoadBlobButton_Click(object sender, EventArgs e)
        {
            string role = getUserRole();
            // to make sure that the user will not change the value of the selected dropdownitem.
            switch (LoadBlobButton.Text.ToString())
            {
                case "Load":
                    LoadBlobButton.Text = "Reset";
                    ddlLoad.Enabled = false;

                    // to display the values of a pic to the user, number of likes, dislikes, downloads and the description of an image.
                    LoadedImage.ImageUrl = ddlLoad.SelectedItem.Value;
                    NumLikes.Text = "Likes: " + blobHelper.GetLikes(ddlLoad.SelectedItem.Value).ToString();
                    NumDislikes.Text = "Disikes: " + blobHelper.GetDislikes(ddlLoad.SelectedItem.Value).ToString();
                    NumOfDownloads.Text = "Downloads: " + blobHelper.GetNumOfDownloads(ddlLoad.SelectedItem.Value).ToString();

                    ImageDescriptionLabel.Text = "Description: " + blobHelper.GetDescription(ddlLoad.SelectedItem.Value);

                    LoadResult.Text = "Image loaded succesfully";
                    // to allow only customers to like, dislike, download, comment and see comments of a pic
                    if (role == "Customer")
                    {
                        LikeButton.Visible = true;
                        DisLikeButton.Visible = true;
                        DownloadButton.Visible = true;

                        CommentsLabel.Visible = true;
                        CommentTextBox.Visible = true;
                        AddCommentButton.Visible = true;
                        ShowCommentsButton.Visible = true;
                    }

                    // to allow only admin and the owner of the pic to delete it
                    if (role == "Administrator" || blobHelper.getOwner(ddlLoad.SelectedItem.Value) == User.Identity.GetUserId())
                    {
                        DeleteButton.Visible = true;
                    }
                    break;
                    // to reset the page so the user could make see other pictures
                case "Reset":
                    LoadBlobButton.Text = "Load";
                    ddlLoad.Enabled = true;
                    PopulateImagesList();
                    Response.Redirect("Blobs.aspx", false);
                    
                    break;

                default:
                    Response.Redirect("Blobs.aspx", false);
                    break;
            }
            LikeButton.Enabled = true;
            DisLikeButton.Enabled = true;
        }

        // to like an image, it increases the like counter in the blob meta data.
        protected void LikeButton_Click(object sender, EventArgs e)
        {
            LikeButton.Enabled = false;
            DisLikeButton.Enabled = true;
            blobHelper.Like(ddlLoad.SelectedItem.Value);

            LikeAreaResults.Text = "Liked!";

            // to update the values of the labels.
            NumLikes.Text = "Likes: " + blobHelper.GetLikes(ddlLoad.SelectedItem.Value).ToString();
            NumDislikes.Text = "Disikes: " + blobHelper.GetDislikes(ddlLoad.SelectedItem.Value).ToString();
        }

        // to dislike an image, it increases the dislike counter in the blob meta data.
        protected void DisLikeButton_Click(object sender, EventArgs e)
        {
            LikeButton.Enabled = true;
            DisLikeButton.Enabled = false;
            blobHelper.Dislike(ddlLoad.SelectedItem.Value);
            LikeAreaResults.Text = "Disliked!";

            // to update the values of the labels.
            NumLikes.Text = "Likes: " + blobHelper.GetLikes(ddlLoad.SelectedItem.Value).ToString();
            NumDislikes.Text = "Disikes: " + blobHelper.GetDislikes(ddlLoad.SelectedItem.Value).ToString();

        }

        //to download an image
        protected void DownloadButton_Click(object sender, EventArgs e)
        {
            blobHelper.Download(ddlLoad.SelectedItem.Value);
            LikeAreaResults.Text = "Downloaded!";


            NumOfDownloads.Text = "Downloads: " + blobHelper.GetNumOfDownloads(ddlLoad.SelectedItem.Value).ToString();

        }

        // to add a comment to a certain image
        //note you can only make one comment otherwise, u will be told u cannot make more than on comment per pic
        protected void AddCommentButton_Click(object sender, EventArgs e)
        {
            List<Comment> comments = blobHelper.GetComments(ddlLoad.SelectedItem.Value);

            try
            {
                if (comments.Find(a => a.PartitionKey == User.Identity.GetUserId()) == null)
                {
                    blobHelper.AddComment(CommentTextBox.Text, ddlLoad.SelectedItem.Value, User.Identity.GetUserId());
                    CommentsResults.Text = "Comment is added";

                }
            }
            catch (Exception)
            {
                CommentsResults.Text = "You cannot make another comment! you already made a comment on this picutre!";
            }
        }

        // to show the list of the comments of a certain image
        protected void ShowCommentsButton_Click(object sender, EventArgs e)
        {
            CommentLabel.Visible = true;

            List<Comment> comments = blobHelper.GetComments(ddlLoad.SelectedItem.Value);

            foreach (Comment item in comments)
            {
                CommentAreaResults.Text += "Userid=(" + item.RowKey + ") commented: " + item.CommentText + "<br />";

            }

        }

        // to delete an image, it will also delete the comments of this image
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            blobHelper.DeleteBlob(ddlLoad.SelectedItem.Value);

            LoadResult.Text = "Deleted!";

            Response.Redirect("Blobs.aspx", false);

        }

        // this to populate the ddl with the image names from the blob
        protected async void PopulateImagesList()
        {
            List<string> imgs = await blobHelper.GetBlobs();
            if (imgs.Count != 0)
            {
                foreach (string img in imgs)
                {
                    ListItem item = new ListItem(img, img);
                    ddlLoad.Items.Add(item);
                }
            }
            else
            {
                ddlLoad.Items.Add(new ListItem("Not images to show"));
            }
        }


    }
}