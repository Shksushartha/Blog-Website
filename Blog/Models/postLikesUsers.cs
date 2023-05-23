using System;
namespace Blog.Models
{
	public class postLikesUsers
	{
		public postLikesUsers()
		{
		}

		public int Id { get; set; }
		public string user { get; set; } = "";
		public int PostId { get; set; }

	}
}

