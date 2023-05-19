using System;
using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
	[Authorize(Roles = "Admin")]
	public class PanelController : Controller
	{
        private readonly IRepository _ctx;
        private readonly IFileManager _fileManager;

        public PanelController(IRepository ctx, IFileManager fileManager)
		{
            _ctx = ctx;
            _fileManager = fileManager;
        }

        public IActionResult Index()
        {
            return View(_ctx.getPost());
        }       

       

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _ctx.getPostId(id);
            return View(new PostVM
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Description = post.Description,
                Category = post.Category,
                Tags = post.Tags
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostVM post)
        {
            _ctx.updatePost(new Post
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Image = await _fileManager.SaveImage(post.Image),
                Description = post.Description,
                Tags = post.Tags,
                Category = post.Category
            }) ;
            if (await _ctx.saveChangesAsync())
            {
                return RedirectToAction("Index");

            }
            return View(post);
        }

        public async Task<IActionResult> Remove(int id)
        {
            _ctx.deletePost(id);
            if (await _ctx.saveChangesAsync())
            {
                return RedirectToAction("Index");
            }
            else return View(id);
        }
    }
}

