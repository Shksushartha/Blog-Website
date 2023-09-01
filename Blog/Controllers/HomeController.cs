using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Blog.Data;
using Blog.Data.Repository;
using Blog.Data.FileManager;
using Blog.ViewModels;
using Blog.Models.Comments;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using Rotativa.AspNetCore;
using System.Data.SqlClient;

namespace Blog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository _ctx;
    private readonly IFileManager _fileManager;

    public HomeController(ILogger<HomeController> logger, IRepository ctx, IFileManager fileManager)
    {
        _logger = logger;
        _ctx = ctx;
        _fileManager = fileManager;
    }



    public IActionResult Index()
    {
        var p = _ctx.getPost();

        return View(p);
    }

    [HttpGet]
    public ActionResult Post(int id)
    {
        var post = _ctx.getPostId(id);

        ViewBag.check = _ctx.isLiked(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
        ViewBag.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return View(post);
    }

    [HttpGet]
    public IActionResult PostPdf(int id)
    {

        var pdf = new ViewAsPdf("Post", id);
        SqlCommand s1 = new SqlCommand();
        var result = s1.ExecuteNonQuery();

        return pdf;
    }

    [HttpPost]
    public ActionResult Post(SearchVM s)
    {
        var post = _ctx.getPostTitle(s);
        ViewBag.check = _ctx.isLiked(post.Id, User.FindFirstValue(ClaimTypes.NameIdentifier));
        List<Post> p = new List<Post>();
        p.Add(post);
        return new ViewAsPdf(post);


    }

    [HttpGet("/Image/{image}")]
    public IActionResult Image(string image)
    {
        var mime = image.Substring(image.LastIndexOf('.') + 1);
        return new FileStreamResult(_fileManager.imageStream(image), $"image/{mime}");
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View(new PostVM());
    }

    [HttpPost]
    public async Task<IActionResult> Add(PostVM post)
    {
        var post1 = new Post
        {
            Id = post.Id,
            Body = post.Body,
            Title = post.Title,
            Image = await _fileManager.SaveImage(post.Image),
            Description = post.Description,
            Tags = post.Tags,
            Category = post.Category
        };
        _ctx.addPost(post1);
        if (await _ctx.saveChangesAsync())
        {
            return RedirectToAction("Index");
        }

        return View(post);
    }


    [HttpPost]
    public async Task<IActionResult> Comment(CommentVM cm)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }

        var post = _ctx.getPostId(cm.postId);

        if (cm.MainCommentId == 0)
        {
            post.mainComments = post.mainComments ?? new List<MainComment>();

            post.mainComments.Add(new MainComment()
            {
                comment = cm.comment,
                commentor = "no"
            });

            _ctx.updatePost(post);

        }
        else
        {
            var comment = new SubComment()
            {
                comment = cm.comment,
                MainCommentId = cm.MainCommentId,
                commentor = "no"

            };
            _ctx.addSubComment(comment);
        }
        await _ctx.saveChangesAsync();

        return RedirectToAction("Post", new { id = cm.postId });
    }

    [HttpGet]
    public IActionResult Search()
    {
        return View(new SearchVM());
    }

    [HttpPost]
    public IActionResult Search(SearchVM s)
    {
        try
        {
            var post = _ctx.getPostTitle(s);
            return RedirectToAction("Post", post);

        }
        catch (Exception e)
        {
            Console.Write(e);
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Like(postLikesUsers p)
    {
        if (_ctx.isLiked(p.PostId, User.FindFirstValue(ClaimTypes.NameIdentifier)))
        {
            _ctx.unlike(p.PostId, p.user);
            _ctx.substractlike(p.PostId);

        }
        else
        {
            _ctx.like(p.PostId, p.user);
            _ctx.addlike(p.PostId);
        }
        await _ctx.saveChangesAsync();
        var post = _ctx.getPostId(p.PostId);
        return RedirectToAction("Post", post);
    }







}

