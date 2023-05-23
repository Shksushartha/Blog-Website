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
		void removeComment(int id);
		void removeSubComment(int id);
		bool isLiked(int pid, string uid);
		void like(int pid, string uid);
        void unlike(int pid, string uid);
		void addlike(int pid);
		void substractlike(int pid);


		Task<bool> saveChangesAsync();
    }
}

