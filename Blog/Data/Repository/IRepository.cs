using System;
using Blog.Models;
using Blog.Models.Comments;
using Blog.ViewModels;

namespace Blog.Data.Repository
{
	public interface IRepository
	{
		List<Post> getPost();
		Post getPostId(int id);
		void updatePost(Post p);
		void addPost(Post p);
		void deletePost(int id);
		Post getPostTitle(SearchVM s);

		void addSubComment(SubComment sc);

		Task<bool> saveChangesAsync();
        
    }
}

