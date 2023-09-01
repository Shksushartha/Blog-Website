﻿using System;
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
        public Post getPost()
        {
            return _blogDbContext.Posts.Where(post => post.Id == 1).First();
        }

        public void deletePost(int id)
        {
            _blogDbContext.Posts.Remove(getPostId(id));
        }

        public void updatePost(Post p)
        {
            _blogDbContext.Posts.ToList();
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

        public void removeComment(int id)
        {
            var c = _blogDbContext.mainComments.Where(p => p.Id == id).First();
            _blogDbContext.mainComments.Remove(c);
        }

        public void removeSubComment(int id)
        {
            var c = _blogDbContext.subComments.Where(p => p.Id == id).First();
            _blogDbContext.subComments.Remove(c);
        }

        public bool isLiked(int pid, string uid)
        {
            if (_blogDbContext.postLikesUsers.Where(d => (d.PostId == pid) && (d.user == uid)).FirstOrDefault() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void like(int pid, string uid)
        {
            var l = new postLikesUsers()
            {
                PostId = pid,
                user = uid
            };
            _blogDbContext.postLikesUsers.Add(l);
        }

        public void unlike(int pid, string uid)
        {
            var l = _blogDbContext.postLikesUsers.Where(d => (d.PostId == pid) && (d.user == uid)).First();
            _blogDbContext.postLikesUsers.Remove(l);
        }

        public void addlike(int pid)
        {
            var post = _blogDbContext.Posts.Where(p => p.Id == pid).FirstOrDefault();
            post.likes += 1;
        }

        public void substractlike(int pid)
        {
            var post = _blogDbContext.Posts.Where(p => p.Id == pid).FirstOrDefault();
            post.likes -= 1;
        }

        List<Post> IRepository.getPost()
        {
            var posts = _blogDbContext.Posts.ToList();
            return posts;
        }
    }
}

