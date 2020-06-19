<%@ Page Title="Blobs" Async="true" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Blobs.aspx.cs" Inherits="WebRole1.Blobs" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <asp:Label runat="server" Font-Bold="true" Font-Size="Larger" ID="Label1"></asp:Label>

    <br />
    <br />

    <div class="col">
        <div class="col-md-12" style="display: inline-block; border-style: outset;">
            <div class="form-group">
                <br />
                <asp:Label ID="Label5" runat="server" Font-Size="Large" Font-Bold="true" Text="Upload Images" Style="margin: 5px 0px 0px 500px;"></asp:Label>
                <br />

                <asp:FileUpload ID="FileUpload" runat="server" Visible="true" BorderColor="Black" Style="margin: 10px 0px 0px 10px;" Width="525px" />

                <asp:Label ID="Label2" runat="server" Width="140px" Text="Description:" Style="margin: 10px 0px 0px 10px;"></asp:Label>
                <asp:TextBox ID="DescriptionTextBox" runat="server" Style="margin: 10px 0px 0px 0px;" Width="250px"></asp:TextBox>
                <asp:Button ID="UploadBlobButton" runat="server" Text="Upload" class="btn btn-primary" Style="margin: 0px 0px 0px 10px;" Width="120px" OnClick="UploadBlobButton_Click"></asp:Button>
                <asp:Label ID="UploadResults" runat="server" Width="200px" Style="margin: 20px 0px 0px 10px;"></asp:Label>


            </div>
        </div>
        <div class="col-md-12" style="display: inline-block; border-style: outset;">
            <div class="form-group">
                <br />
                <asp:Label ID="Label4" runat="server" Font-Size="Large" Font-Bold="true" Text="Load Images" Style="margin: 5px 0px 0px 500px;"></asp:Label>
                <br />


                <asp:Label ID="Label3" runat="server" Width="200px" Text="Please select an image to load:" Style="margin: 20px 0px 0px 10px;"></asp:Label>

                <asp:DropDownList ID="ddlLoad" runat="server" Height="25px" Width="160px" Style="margin: 20px 0px 0px 0px">
                </asp:DropDownList>
                <asp:Button ID="LoadBlobButton" runat="server" Text="Load" class="btn btn-primary" Style="margin: 0px 0px 0px 10px;" Width="120px" OnClick="LoadBlobButton_Click"></asp:Button>
                <asp:Button ID="DeleteButton" runat="server" Text="Delete image" class="btn btn-danger" Visible="false" Style="margin: 0px 0px 0px 10px;" OnClick="DeleteButton_Click" />

                <asp:Label ID="LoadResult" runat="server" Width="200px" Style="margin: 20px 0px 0px 10px;"></asp:Label>
                <br />
                <br />
                <asp:Image ID="LoadedImage" runat="server" />

                <br />

                <asp:Label ID="ImageDescriptionLabel" runat="server" Width="525px" Font-Size="Larger" Style="margin: 20px 0px 0px 10px;"></asp:Label>
                <br />
                <asp:Label ID="NumLikes" runat="server" Width="200px" Style="margin: 10px 0px 0px 10px;"></asp:Label>
                <asp:Label ID="NumDislikes" runat="server" Width="200px" Style="margin: 10px 0px 0px 10px;"></asp:Label>
                <asp:Label ID="NumOfDownloads" runat="server" Width="200px" Style="margin: 10px 0px 0px 10px;"></asp:Label>

                <br />
                <asp:Button ID="LikeButton" runat="server" Text="Like" class="btn btn-success" Visible="false" Style="margin: 10px 0px 0px 10px;" OnClick="LikeButton_Click" />
                <asp:Button ID="DisLikeButton" runat="server" Text="Dislike" class="btn btn-danger" Visible="false" Style="margin: 10px 0px 0px 10px;" OnClick="DisLikeButton_Click" />
                <asp:Button ID="DownloadButton" runat="server" Text="Download" class="btn btn-warning" Visible="false" Style="margin: 10px 0px 0px 10px;" OnClick="DownloadButton_Click" />

                <asp:Label ID="LikeAreaResults" runat="server" Width="200px" Style="margin: 20px 0px 0px 10px;"></asp:Label>

                <br />

                <asp:Label ID="CommentsLabel" runat="server" Width="140px" Text="Comment:" Visible="false" Style="margin: 10px 0px 0px 10px;"></asp:Label>
                <asp:TextBox ID="CommentTextBox" runat="server" Visible="false" Style="margin: 10px 0px 0px 0px;" Width="250px"></asp:TextBox>
                <asp:Button ID="AddCommentButton" runat="server" Text="Add comment" class="btn btn-success" Visible="false" Style="margin: 0px 0px 0px 10px;" OnClick="AddCommentButton_Click" />
                <asp:Button ID="ShowCommentsButton" runat="server" Text="Show comments" class="btn btn-danger" Visible="false" Style="margin: 0px 0px 0px 10px;" OnClick="ShowCommentsButton_Click" />
                <asp:Label ID="CommentsResults" runat="server" Width="525px" Style="margin: 20px 0px 0px 10px;"></asp:Label>
                <br />
                <asp:Label ID="CommentLabel" runat="server" Text="Comments:" Visible="false"  Width="120px" Style="margin: 20px 0px 0px 10px;"></asp:Label>

                <asp:Label ID="CommentAreaResults" runat="server" Width="600px" Style="margin: 20px 0px 0px 10px;"></asp:Label>


            </div>
        </div>
    </div>

</asp:Content>
