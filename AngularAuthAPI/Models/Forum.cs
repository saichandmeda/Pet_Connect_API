using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularAuthAPI.Models
{
	public class Forum
	{
        //public Forum()
        //{
        //}
        //public class ForumUser
        //{
        //    //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //    [Key]
        //    public int UserId { get; set; }
        //    public string UserName { get; set; }
        //    public string Email { get; set; }
        //    public string ImageUrl { get; set; }
        //    public DateTime CreatedDate { get; set; }
        //    public string Password { get; set; }
        //}

        public class ForumPrivateChat
        {
            //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int chatId { get; set; }
            public int fromUserId { get; set; }
            public int toUserId { get; set; }
            public DateTime createdDate { get; set; }
            public string messages { get; set; }
            public bool isRead { get; set; }
        }

        //[Table("forumPost")]
        public class ForumPost
        {
            //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int PostId { get; set; }
            public int UserId { get; set; }
            public string PostTitle { get; set; }
            public string PostDescription { get; set; }
            public DateTime CreatedDate { get; set; }
            public bool IsOpen { get; set; }
            public int LikeCount { get; set; }
            public Nullable<DateTime> ModifiedDate { get; set; }
        }

        

        //[Table("forumComments")]
        public class ForumComment
        {
            //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int CommentId { get; set; }
            public int PostId { get; set; }
            public int ParentCommentId { get; set; }
            public string Comment { get; set; }
            public int CommentUserId { get; set; }
            public DateTime CommentDate { get; set; }
        }

        //[Table("forumLikes")]
        public class ForumLike
        {
            //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int LikeId { get; set; }
            public int PostId { get; set; }
            public int UserId { get; set; }
        }

        public class ForumCommentList
        {

            [Key]
            public int CommentId { get; set; }
            public int PostId { get; set; }
            public int ParentCommentId { get; set; }
            public string Comment { get; set; }
            public int CommentUserId { get; set; }
            public DateTime CommentDate { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }

        }

        public class ForumPostList
        {
            [Key]
            public int PostId { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public int UserId { get; set; }
            public string PostTitle { get; set; }
            public string PostDescription { get; set; }
            public DateTime CreatedDate { get; set; }
            public bool IsOpen { get; set; }
            public int Liked { get; set; }
            public int TotalComments { get; set; }
            public int TotalLikes { get; set; }
        }
    }
}

