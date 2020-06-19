using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    
    public class Comment : TableEntity
    {

        public Comment()
        {
        }
        public string CommentText { get; set; }

    }
}