using System;
namespace Blog.Models.Comments
{
	public class SubComment:Comment
	{
		public SubComment()
		{
		}

		public int MainCommentId { get; set; }
	}
}

