using System;
using Blog.Models;
using Blog.Models.Comments;

namespace Blog.Data.Repository
{
	public interface IRepository
	{
		List<Post> getPost();
		Post getPostId(int id);
		void updatePost(Post p);
		void addPost(Post p);
		void deletePost(int id);

		void addSubComment(SubComment sc);

		Task<bool> saveChangesAsync();
	}
}

