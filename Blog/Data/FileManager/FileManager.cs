using System;

namespace Blog.Data.FileManager
{
	public class FileManager : IFileManager
	{
        private string? _imagePath;

        public FileManager(IConfiguration config)
		{
            _imagePath = config["Path:Images"];
		}    



        public async Task<string> SaveImage(IFormFile file)
        {

            try
            {
                var save_dir = Path.Combine(_imagePath);
                if (!Directory.Exists(save_dir))
                {
                    Directory.CreateDirectory(save_dir);
               
                }

                var mime = file.FileName.Substring(file.FileName.LastIndexOf('.'));

                var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";

                using (var fileStream = new FileStream(Path.Combine(save_dir, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return fileName;

            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }
        }

        public FileStream imageStream(string image)
        {
            return new FileStream(Path.Combine(_imagePath, image),FileMode.Open, FileAccess.Read);
        }


    }
}


