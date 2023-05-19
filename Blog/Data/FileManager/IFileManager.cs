using System;
namespace Blog.Data.FileManager
{
	public interface IFileManager
	{
		Task<string> SaveImage(IFormFile file);

		FileStream imageStream(string image);
	}
}

