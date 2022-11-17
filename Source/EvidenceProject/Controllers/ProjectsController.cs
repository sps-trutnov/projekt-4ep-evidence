﻿using EvidenceProject.Controllers.ActionData;
using Microsoft.Extensions.Caching.Memory;

namespace EvidenceProject.Controllers;
public class ProjectController : Controller
{
    private readonly ProjectContext _context;
    private readonly IMemoryCache _cache;
    private readonly ILogger<ProjectController> _logger;
    public ProjectController(ProjectContext context, IMemoryCache cache, ILogger<ProjectController> logger)
    {
        _context = context;
        _cache = cache;
        _logger = logger;
    }

    /// <summary>
    /// projekt (get)
    /// </summary>
    [HttpGet("project")]
    public ActionResult Index()
    {
        if(!UniversalHelper.getLoggedUser(HttpContext, out var userID) && userID != "1") return Redirect("/");
        return Redirect("project/create");
    }

    /// <summary>
    /// Vytvoření/přidání projektu
    /// </summary>        
    [HttpPost("project/create")]
    public ActionResult Create([FromForm] ProjectCreateData projectData, bool test = false)
    {
        string? userID = String.Empty;
        if (!test) if (!UniversalHelper.getLoggedUser(HttpContext, out userID) && userID != "1") return Redirect("/");

        Project project = new()
        {
            name = projectData.projectName,
            projectTechnology = new DialCode(),
            projectType = new DialCode(),
        };
        _logger.LogInformation("User with the id <{}> created a project called \"{}\"", userID, projectData.projectName);
        _context?.projects?.Add(project);
        _context?.SaveChanges();
        UpdateProjectsInCache();
        return Redirect("Index");
    }

    /// <summary>
    /// Create (get)
    /// </summary>
    [HttpGet("project/create")]
    public ActionResult Create()
    {
        var dialCodes = _context?.dialCodes?.ToList();
        return View(dialCodes);
    }

    /// <summary>
    /// Odstranění projektu
    /// </summary>
    [HttpPost("project/{id}")]
    public JsonResult Delete(int id)
    {
        if (!UniversalHelper.getLoggedUser(HttpContext, out var userID) && userID != "1") return Json("Nejsi admin/přihlášen");
        if (!UniversalHelper.getProject(_context, id, out var project)) return Json("Takový projekt neexistuje");
        _logger.LogInformation("User with the id <{}> deleted a project", userID);

        _context?.projects?.Remove(project);
        UpdateProjectsInCache();
        return Json("ok");
    }
    
    /// <summary>
    /// Stránka s projektem 
    /// </summary>
    [HttpGet("projectinfo/{id}")]
    public ActionResult ProjectInfo(int id)
    {
        if (!UniversalHelper.getProject(_context, id, out var project)) return View();
        return View(project);
    }

    /// <summary>
    /// Vyhledávání
    /// </summary>
    [HttpPost("search")]
    public ActionResult Search([FromBody] SearchData data)
    {
        if (data.text == string.Empty) return Ok();
        List<Project> projects  = _context?.projects?.ToList().Where(project => project.name.Contains(data.text)).ToList();
        if (projects == null) return Json("Nic nenalezeno");
        return Json(projects);
    }


    public void UpdateProjectsInCache()
    {
        var projects = _context?.projects?.ToList();
        _cache.Set("AllProjects", projects);
    }
}
