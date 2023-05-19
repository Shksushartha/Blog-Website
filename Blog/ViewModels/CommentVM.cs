using System;
namespace Blog.ViewModels
{
	public class CommentVM
	{
		public CommentVM()
		{
		}
		
		public int postId { get; set; }
		public int MainCommentId { get; set; }
		public string comment { get; set; }

	}
}

