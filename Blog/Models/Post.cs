using System;
using Blog.Models.Comments;

namespace Blog.Models
{
	public class Post
	{
		public Post()
		{
		}

		public int Id { get; set; } 
		public string Title { get; set; } = null!;
		public string Body { get; set; } = null!;
		public string Image { get; set; } = "";
		public string Description { get; set; } = "";
        public string Tags { get; set; } = "";
        public string Category { get; set; } = "";

		public List<MainComment> mainComments { get; set; }
    }
}

