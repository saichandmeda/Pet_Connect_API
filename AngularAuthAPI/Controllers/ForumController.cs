using System;
using System.IdentityModel.Tokens.Jwt;
using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AngularAuthAPI.Models.Forum;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    //[Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class ForumController:Controller
	{
        private readonly AppDbContext _context;
        public ForumController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        [HttpGet("getUnReadMessages")]
        public async Task<IActionResult> getUnReadMessages(int touserId)
        {
            try
            {
                var all = await (from chat in _context.ForumPrivateChats
                                 where chat.toUserId == touserId && chat.isRead == false 
                                 join user in _context.Users on chat.fromUserId equals user.Id
                                 select new 
                                 {
                                     toUserId = chat.toUserId,
                                     createdDate = chat.createdDate,
                                     fromUserId = chat.fromUserId,
                                     isRead = chat.isRead,
                                     messages = chat.messages,
                                     UserName = user.UserName,
                                     chatId = chat.chatId
                                 }).OrderByDescending(m => m.chatId).ToListAsync(); 
                 
                return Ok(new
                {
                    data = all
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("getAllChatMessages")]
        public async Task<IActionResult> getAllChatMessages(int fromUserId, int touserId)
        {
            try
            {
                var all = await _context.ForumPrivateChats.Where(m => m.fromUserId == fromUserId && m.toUserId == touserId || m.fromUserId == touserId && m.toUserId == fromUserId).OrderBy(m => m.createdDate).ToListAsync();
                foreach (var item in all)
                {
                    item.isRead = true;
                    _context.SaveChanges();
                }
                return Ok(new
                {
                    data = all
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("createNewChat")]
        public async Task<IActionResult> createNewPost([FromBody] ForumPrivateChat obj)
        {
            try
            {
                if (obj == null)
                    return BadRequest();

                obj.isRead = false;

                await _context.ForumPrivateChats.AddAsync(obj);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    data = obj,
                    Message = "Message Sent Succefully!"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }


        [HttpGet("GetAllForumUsers")]
        public async Task<IActionResult> GetAllForumUsers()
        {
            try
            {
                var all = await _context.Users.OrderByDescending(m => m.Id).ToListAsync();

                return Ok(new
                {
                    data = all
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetAllPost")]
        public async Task<IActionResult> GetAllPost(int userid)
        {
            try
            {

                List<ForumPostList> posts = new List<ForumPostList>();
                var query = (from p in _context.ForumPosts
                             join u in _context.Users on p.UserId equals u.Id
                             join l in _context.ForumLikes.Where(x => x.UserId == userid) on p.PostId equals l.PostId into lj
                             from liked in lj.DefaultIfEmpty()
                             group new { p, u, liked } by new { p.PostId, p.PostTitle, p.PostDescription, p.CreatedDate, p.IsOpen, u.Id, u.UserName, u.Email } into g
                             select new ForumPostList
                             {
                                 PostId = g.Key.PostId,
                                 PostTitle = g.Key.PostTitle,
                                 PostDescription = g.Key.PostDescription,
                                 CreatedDate = g.Key.CreatedDate,
                                 IsOpen = g.Key.IsOpen,
                                 UserId = g.Key.Id,
                                 UserName = g.Key.UserName,
                                 Email = g.Key.Email,
                                 Liked = g.Any(x => x.liked != null) ? 1 : 0,
                                 TotalComments = g.Count(),
                                 TotalLikes = g.Sum(x => x.liked != null ? 1 : 0)
                             }).OrderByDescending(m=>m.CreatedDate).ToList();





                //var all = await _context.getAllPostForUserId(userid).OrderByDescending(m => m.PostId).ToListAsync();

                return Ok(new
                {
                    data = query
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpGet("GetAllPost")]
        //public async Task<IActionResult> GetAllPost(int userid)
        //{
        //    try
        //    {
        //        //var data = _context.ForumPostLists.FromSqlRaw("getAllPostsByUserId @userId",
        //        //    new MySqlParameter
        //        //    {
        //        //        ParameterName = "@userId",
        //        //        MySqlDbType = MySqlDbType.Int64,
        //        //        Value = userid
        //        //    }).ToList();

        //        // var all = await _context.getAllPostForUserId(userid).OrderByDescending(m => m.PostId).ToListAsync();



        //        var userIdParam = new MySqlParameter("@userId", MySqlDbType.Int64)
        //        {
        //            Value = userid
        //        };

        //        var data = _context.ForumPostLists
        //            .FromSqlRaw("CALL getAllPostsByUserId(@userId)", userIdParam)
        //            .ToList();

        //        return Ok(new
        //        {
        //            data = data
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}




        [HttpGet("getPostByPostId")]
        public async Task<IActionResult> getPostByPostId(int userId, int postid)
        {
            try
            {
                List<ForumPostList> posts = new List<ForumPostList>();
                var query = (from p in _context.ForumPosts
                             where p.PostId ==  postid
                             join u in _context.Users on p.UserId equals u.Id
                             join l in _context.ForumLikes.Where(x => x.UserId == userId) on p.PostId equals l.PostId into lj
                             from liked in lj.DefaultIfEmpty()
                             group new { p, u, liked } by new { p.PostId, p.PostTitle, p.PostDescription, p.CreatedDate, p.IsOpen, u.Id, u.UserName, u.Email } into g
                             select new ForumPostList
                             {
                                 PostId = g.Key.PostId,
                                 PostTitle = g.Key.PostTitle,
                                 PostDescription = g.Key.PostDescription,
                                 CreatedDate = g.Key.CreatedDate,
                                 IsOpen = g.Key.IsOpen,
                                 UserId = g.Key.Id,
                                 UserName = g.Key.UserName,
                                 Email = g.Key.Email,
                                 Liked = g.Any(x => x.liked != null) ? 1 : 0,
                                 TotalComments = g.Count(),
                                 TotalLikes = g.Sum(x => x.liked != null ? 1 : 0)
                             }).SingleOrDefault();


                //var post = await _context.forumPosts.SingleOrDefaultAsync(m => m.PostId == id);
                if (query != null)
                {
                    return Ok(new
                    {
                        data = query
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        message = "Post Not Found"
                    });
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("createNewPost")]
        public async Task<IActionResult> createNewPost([FromBody] ForumPost obj)
        {
            try
            {
                if (obj == null)
                    return BadRequest();

                if (await CheckPostTitleExistAsync(obj.PostTitle))
                    return BadRequest(new { Message = "Post Title Already Exist!" });

                await _context.ForumPosts.AddAsync(obj);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    data = obj,
                    Message = "Post Created Succefully!"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpPut("updatePost")]
        public async Task<IActionResult> updatePost([FromBody] ForumPost obj)
        {
            try
            {
                if (obj == null)
                    return BadRequest();
                _context.Entry(obj).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return Ok(new
                {
                    data = obj,
                    Message = "Post Updated Succefully!"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpDelete("deletePost")]
        public async Task<IActionResult> deletePost(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();
                ForumPost post = _context.ForumPosts.SingleOrDefault(m => m.PostId == id);
                if (post != null)
                {
                    _context.ForumPosts.Remove(post);
                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        Message = "Post Deleted Succefully!"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        message = "Post Not Found"
                    });
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpPost("likePost")]
        public async Task<IActionResult> likePost([FromBody] ForumLike obj)
        {
            try
            {
                if (obj == null)
                    return BadRequest();

                if (await checkIfLikeExist(obj.UserId, obj.PostId))
                    return BadRequest(new { Message = "Post Title Already Exist!" });

                await _context.ForumLikes.AddAsync(obj);
                await _context.SaveChangesAsync();


                return Ok(new
                {
                    data = obj,
                    Message = "You Likes Post Succefully!"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpPost("removelikePost")]
        public async Task<IActionResult> removelikePost([FromBody] ForumLike obj)
        {
            try
            {
                if (obj == null)
                    return BadRequest();

                var likeData = await _context.ForumLikes.SingleOrDefaultAsync(m => m.PostId == obj.PostId && m.UserId == obj.UserId);
                _context.ForumLikes.Remove(likeData);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    data = obj,
                    Message = "You Likes Post Succefully!"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private Task<bool> checkIfLikeExist(int userid, int postid)
          => _context.ForumLikes.AnyAsync(x => x.UserId == userid && x.PostId == postid);

        private Task<bool> CheckPostTitleExistAsync(string title)
            => _context.ForumPosts.AnyAsync(x => x.PostTitle == title);




        #region forumComments

        [HttpGet("GetAllCommentByPostId")]
        public async Task<IActionResult> GetAllCommentByPostId(int postId)
        {
            try
            {
                var all = await (from comment in _context.ForumComments
                                 where comment.PostId == postId
                                 join post in _context.ForumPosts on comment.PostId equals post.PostId
                                 join user in _context.Users on comment.CommentUserId equals user.Id
                                 select new ForumCommentList
                                 {
                                     Comment = comment.Comment,
                                     CommentDate = comment.CommentDate,
                                     CommentId = comment.CommentId,
                                     CommentUserId = comment.CommentUserId,
                                     ParentCommentId = comment.ParentCommentId,
                                     Email = user.Email,
                                     PostId = post.PostId,
                                     UserName = user.UserName
                                 }).OrderByDescending(m => m.CommentId).ToListAsync();

                return Ok(new
                {
                    data = all
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost("addNewComment")]
        public async Task<IActionResult> addNewComment([FromBody] ForumComment obj)
        {
            try
            {
                if (obj == null)
                    return BadRequest();

                await _context.ForumComments.AddAsync(obj);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    data = obj,
                    Message = "Comment Added Succefully!"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpPut("updateComment")]
        public async Task<IActionResult> updateComment([FromBody] ForumComment obj)
        {
            try
            {
                if (obj == null)
                    return BadRequest();
                _context.Entry(obj).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    data = obj,
                    Message = "Comment Updated Succefully!"
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpDelete("deleteComment")]
        public async Task<IActionResult> deleteComment(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();
                ForumComment comment = _context.ForumComments.SingleOrDefault(m => m.CommentId == id);
                if (comment != null)
                {
                    _context.ForumComments.Remove(comment);
                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        Message = "Comment Deleted Succefully!"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        message = "Comment Not Found"
                    });
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }



     #endregion


    }
}

