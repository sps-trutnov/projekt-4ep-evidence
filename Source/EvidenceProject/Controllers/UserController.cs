using EvidenceProject.Controllers.RequestClasses;
using EvidenceProject.Helpers;

namespace EvidenceProject.Controllers;
public class UserController : Controller
{
    private readonly ProjectContext _context;

    public UserController(ProjectContext context)
    {
        _context = context;
    }

    
    [HttpGet("admin")]
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString(UniversalHelper.LoggedInKey) != "1")  return Redirect("/");
        return View();
    }

    // <summary>
    // Login view (get)
    // </summary>
    [HttpGet("users/login")]
    public IActionResult Login() => View();
    
    // <summary>
    // Login (post)
    // </summary>
    [HttpPost("users/login")]
    public IActionResult LoginPost([FromForm] LoginData data) 
    {
        AuthUser? user = _context.globalUsers?.ToList().FirstOrDefault(u => u.username == data.username);
        if (user == null) return Json(UniversalHelper.SomethingWentWrongMessage);

        if (data.password == null || data.username == null) return Json(UniversalHelper.SomethingWentWrongMessage);

        if (!PasswordHelper.VerifyHash(data.password, user.password)) return Json(UniversalHelper.SomethingWentWrongMessage);

        HttpContext.Session.SetString(UniversalHelper.LoggedInKey, user.id.ToString());
        return Redirect("/");
    }


    // <summary>
    // Register view (get)
    // </summary>
    [HttpGet("users/register")]
    public IActionResult Register() => View();

    // <summary>
    // Register (post)
    // </summary>
    [HttpPost("users/register")]
    public IActionResult RegisterPost([FromForm] LoginData data)
    {
        if (data.username == null || data.password == null) return Json(UniversalHelper.SomethingWentWrongMessage);
        var contextList = _context?.globalUsers?.ToList();

        var isUserExisting = contextList.Any(u => u.username == data.username);
        if ((bool)isUserExisting) return Json("U�ivatel ji� existuje"); // Don't allow 2 users with the same name

        var isFirstUser = contextList.Any(); // if there is no user 
        string passwordHash = PasswordHelper.CreateHash(data.password);
        var newUser = new AuthUser()
        {
            fullName = data.username,
            username = data.username,
            password = passwordHash,
            studyField = null,
            contactDetails = null,
            globalAdmin = !isFirstUser
        };

        _context?.globalUsers?.Add(newUser);
        _context?.SaveChanges();
        return Redirect("/users/login");
    }
}