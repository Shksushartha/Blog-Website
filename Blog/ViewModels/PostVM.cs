﻿using System;
namespace Blog.ViewModels
{
	public class PostVM
	{
		public PostVM()
		{
		}
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Body { get; set; }
        public IFormFile Image { get; set; }

        public string Description { get; set; } = "";
        public string Tags { get; set; } = "";
        public string Category { get; set; } = "";
    }
}
