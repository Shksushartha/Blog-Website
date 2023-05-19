using System;
using Blog.Models;
using Blog.Models.Comments;
using Blog.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Repository
{
	public class Repository : IRepository
	{
        private readonly BlogDbContext _blogDbContext;

        public Repository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public void addPost(Post p)
        {
            _blogDbContext.Posts.Add(p);
            
        }

        public Post getPostId(int id)
        {
            return _blogDbContext.Posts
                .Include(p => p.mainComments)
                    .ThenInclude(m => m.subComments)
                .First(p => p.Id == id);
        }

        public Post getPostTitle(SearchVM s)
        {
            return _blogDbContext.Posts
                .Include(p => p.mainComments)
                    .ThenInclude(m => m.subComments)
                .First(p => p.Title == s.Title);
        }
        public List<Post> getPost()
        {
            return _blogDbContext.Posts.ToList();
        }

        public void deletePost(int id)
        {
            _blogDbContext.Posts.Remove(getPostId(id));
        }

        public void updatePost(Post p)
        {
            _blogDbContext.Posts.Update(p);
        }

        public async Task<bool> saveChangesAsync()
        {
            if (await _blogDbContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public void addSubComment(SubComment sc)
        {
            _blogDbContext.subComments.Add(sc);
        }
    }
}

