using System;
namespace Blog.Models.Comments
{
	public class MainComment : Comment
	{
		public MainComment()
		{
		}

		public List<SubComment> subComments { get; set; }
	}
}

