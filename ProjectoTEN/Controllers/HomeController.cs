using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using ProjectoTEN.Data;
using ProjectoTEN.Models;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NuGet.Protocol;

namespace ProjectoTEN.Controllers {
    public class HomeController : Controller {
        
        private static ILogger<HomeController> _logger;
        private static ProjectoTENContext _context;
        
        static int userId;
        static string name;
        static string email;
        static int tipo;
        static string descricao;
        static string linkedin;
        static private int telefone;
        static private string tagName;
        

        public HomeController(ILogger<HomeController> logger, ProjectoTENContext context) {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index() {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        public IActionResult AdminDashboard() {

            return View("AdminDashboard");
        }
        
        public ActionResult UserDashboard() {
            var videos = _context.LinksVideos.ToList();

            // search all videos per user
            ViewBag.infoVideos = (from u in _context.Utilizadores
            join l in _context.LinksVideos on u.Id equals l.UtilizadorId
            // where u.Email == email
            where u.Id == userId
            select new {
                l.Id,
                l.Titulo,
                l.Link,
                l.Tags
            }).Distinct();

            ViewBag.id = userId;
            ViewBag.Nome = name;
            ViewBag.Email = email;
            ViewBag.Videos = videos;
            return View();
        }
        
        public ActionResult InvestorDashboard() {
            // all data from user
            var userLoggedInInfo = (from u in _context.Utilizadores
                where u.Id == userId
                select u).Distinct();
            
            foreach (var c in userLoggedInInfo) {
                userId = c.Id;
                name = c.Nome;
                email = c.Email;
                tipo = c.Tipo;
                descricao = c.Descricao;
                linkedin = c.Linkedin;
                telefone = c.Telefone;
            }

            var v = _context.LinksVideos.ToList().Distinct();
            foreach (var video in v) {
                video.Link = video.Link.Replace("watch?v=", "embed/");
            }
            
            ViewBag.Tags = (from l in _context.LinksVideos
                select l.Tags).Distinct().ToList();

            if (tagName == null) {
                ViewBag.FullVideos = (from l in _context.LinksVideos
                    select l).Distinct().ToList();
            } else {
                ViewBag.FullVideos = (from l in _context.LinksVideos
                    where l.Tags == tagName
                    select l).Distinct().ToList();
            }

            // ViewBag.FullVideos = (from l in _context.LinksVideos
            //     where l.Tags == tagName
            //     select l).Distinct().ToList();

            ViewBag.id = userId;
            ViewBag.Nome = name;
            ViewBag.Email = email;
            ViewBag.TagName = tagName;
            // ViewBag.FullVideos = v;
            return View();
        }
        
        
        // get user mail of video
        [HttpGet]
        public static string getUserVideoMail(string titulo) {
            string email = "";
            // var userContacts = (from u in _context.Utilizadores
            var userContacts = (from u in _context.Utilizadores
                join l in _context.LinksVideos on u.Id equals l.UtilizadorId
                where l.Titulo == titulo
                select u).Distinct();

            foreach (var c in userContacts) {
                email = c.Email;
            }
            return email;
        }
        
        // get user linkedin of video
        [HttpGet]
        public static string getUserVideoLinkedIn(string titulo) {
            string linkedIn = "";
            var userContacts = (from u in _context.Utilizadores
                join l in _context.LinksVideos on u.Id equals l.UtilizadorId
                where l.Titulo == titulo
                select u).Distinct();
            
            foreach (var c in userContacts) {
                linkedIn = c.Linkedin;
            }
            return linkedIn;
        }
        
        // get getEntrepenurInfo of video
        [HttpGet]
        public static List<object> getEntrepenurInfo(string titulo) {
            // List<string> list = new List<string>();
            List<object> list = new List<object>();
            
            var userContacts = (from u in _context.Utilizadores
                join l in _context.LinksVideos on u.Id equals l.UtilizadorId
                where l.Titulo == titulo
                select u).Distinct();

            foreach (var c in userContacts) {
                list.Add(c.Nome);
                list.Add(c.Tipo);
                list.Add(c.Descricao);
                list.Add(c.Telefone);
            }
            return list;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        // login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model) {

            var utilizador = _context.Utilizadores.FirstOrDefault(u => u.Email.Equals(model.Email));
            var isUserValid = IsValidUser(model);
        
            if (ModelState.IsValid) {
        
                if (isUserValid != null) {
                    
                    // set userid in session
                    HttpContext.Session.SetInt32("ID", utilizador.Id);
                    // set session from user logged in
                    HttpContext.Session.SetString("UserEmail", model.Email);
                    
                    if (utilizador.Tipo == 0) {
                        return RedirectToAction("AdminDashboard");
                    }
        
                    if (utilizador.Tipo == 5) {
                        // all data from user
                        var userInfo = (from u in _context.Utilizadores
                            where u.Id == utilizador.Id
                            select u).Distinct();

                            foreach (var c in userInfo) {
                                userId = c.Id;
                                name = c.Nome;
                                email = c.Email;
                                tipo = c.Tipo;
                                descricao = c.Descricao;
                                linkedin = c.Linkedin;
                                telefone = c.Telefone;
                            }
                        return RedirectToAction("UserDashboard");
                    }
        
                    if (utilizador.Tipo == 4) {
                        var userInfo = (from u in _context.Utilizadores
                            where u.Id == utilizador.Id
                            select u).Distinct();

                        foreach (var c in userInfo) {
                            userId = c.Id;
                            name = c.Nome;
                            email = c.Email;
                            tipo = c.Tipo;
                            descricao = c.Descricao;
                            linkedin = c.Linkedin;
                            telefone = c.Telefone;
                        }
                        return RedirectToAction("InvestorDashboard");
                    }
        
                } else {
                    //If the username and password combination is not present in DB then error message is shown.
                    ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                }
            }
            return View("Error");
        }

        // function to check if User is valid or not
        public Utilizador IsValidUser(LoginViewModel model) {
                var user = _context.Utilizadores.FirstOrDefault(
                    query => query.Email.Equals(model.Email) 
                    && query.Password.Equals(model.Password));
                
                if (user == null)
                    return null;
                return user;
        }


        // register new user
        [HttpPost]
        public async Task<IActionResult> Register(Utilizador utilizador) {
            if (!ModelState.IsValid) {
                return BadRequest("Enter required fields");
            }
            _context.Add(utilizador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        // add new pitch
        [HttpPost]
        public async Task<IActionResult> AddNewPitch(LinkVideo linkVideo) {
            if (!ModelState.IsValid) {
                return BadRequest("Enter required fields");
            }
            _context.Add(linkVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(UserDashboard));
        }

        // view feedback
        [HttpGet]
        public static string ViewFeedback(string link) {
            string criticaVideo = "";
            var critica = (from c in _context.Criticas
                join l in _context.LinksVideos on c.VideoId equals l.Id
                where l.Link == link
                // select c).Distinct();
                select c).Distinct();
            foreach (var cri in critica) {
                criticaVideo = cri.Critica;
            }
            return criticaVideo;
        }

        // delete video
        public void DeleteVideo(int id) {
            var video = new LinkVideo { Id = id };
            _context.LinksVideos.Attach(video);
            _context.LinksVideos.Remove(video);
            _context.SaveChanges();
        }
        
        // add new feedback
        [HttpPost]
        public async Task<IActionResult> AddFeedback(Criticas criticas) {
            if (!ModelState.IsValid) {
                return BadRequest("Enter required fields");
            }
            _context.Add(criticas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(InvestorDashboard));
        }
        
        // count all videos
        [HttpGet]
        public static int countAllVideos() {
            var count = (from l in _context.LinksVideos
                select l).Count();
            return count;
        }
        
        // count user videos
        [HttpGet]
        public static int countAllUserVideos(int id) {
            var count = (from l in _context.LinksVideos
                join u in _context.Utilizadores on l.UtilizadorId equals u.Id
                where u.Id == id
                select l).Count();
            return count;
        }

        // get value from dropdwon
        [HttpPost]
        public ActionResult InvestorDashboard(LinkVideo model) {

            tagName = model.Tags;
            return RedirectToAction("InvestorDashboard");
        }
        
        // get videos by tags
        // public static void videosorder(string tag) {
        //     List<object> list = new List<object>();
        //
        //     // using (var command = _context.Database.GetDbConnection().CreateCommand()) {
        //     //     // command.CommandText = "select distinct * from LinksVideos l where l.Tags = '" + tag + "' ";
        //     //     command.CommandText = "select * from LinksVideos l where l.Tags = 'Mechanic' ";
        //     //
        //     //     _context.Database.OpenConnection();
        //     //     using (var result = command.ExecuteReader()) {
        //     //         // if (result.HasRows) {
        //     //         while (result.Read()) {
        //     //             list.Add(result.GetInt32("Id"));
        //     //             list.Add(result.GetString("Titulo"));
        //     //
        //     //             // foreach (var l in result.GetString("Link")) {
        //     //                 string link = result.GetString("Link").Replace("watch?v=", "embed/");
        //     //                 list.Add(link);
        //     //         }
        //     //         // }
        //     //     }
        //     // }
        //     var data = (from l in _context.LinksVideos
        //         where l.Tags == tag
        //         select l).Distinct().ToList();
        //
        //     foreach (var v in data) {
        //         list.Add(v.Id);
        //         list.Add(v.Titulo);
        //         list.Add(v.Link);
        //     }
        // }
        
        
        
        // [HttpPost]
        // // public IActionResult Login(string username, string password) {
        // public IActionResult Login(string email, string password) {
        //     //verificação de credenciais
        //
        //     // var user = _context.Utilizadores.FirstOrDefault(u => u.Nome.Equals(username));
        //     // var user = _context.Utilizadores.FirstOrDefault(u => u.Email.Equals(email));
        //     var user = _context.Utilizadores.FirstOrDefault(u => u.Email.Contains(email));
        //     var userPassword = _context.Utilizadores.FirstOrDefault(u => u.Password.Contains(password));
        //     
        //     if (user.Equals(null)) {
        //         
        //     }
        //     
        //     if (user.Tipo == 0) {
        //         return View("AdminDashboard");
        //     }
        //     
        //     if (user.Tipo == 4) {
        //         var videos = _context.LinksVideos;
        //         foreach (var video in videos) {
        //             video.Link = video.Link.Replace("watch?v=", "embed/");
        //         }
        //         
        //         ViewBag.Videos = videos;
        //         ViewBag.Nome = user.Nome;
        //         ViewBag.Email = user.Email;
        //         ViewBag.Linkedin = user.Linkedin;
        //         ViewBag.InvestorId = user.Id;
        //         return View("InvestorDashboard");
        //     }
        //     
        //     if (user.Tipo == 5) {
        //         var videos = _context.LinksVideos.Where(v => v.Utilizador.Id == user.Id);
        //         
        //         ViewBag.Videos = videos;
        //         ViewBag.Nome = user.Nome;
        //         ViewBag.Email = user.Email;
        //         ViewBag.UserId = user.Id;
        //         return View("UserDashboard");
        //
        //     }
        //     return View("Index");
        // }

        // get criticas for each video
        // public static string getCriticas(int id) {
        //     string res = "";
        //     using (var command = _context.Database.GetDbConnection().CreateCommand()) {
        //         command.CommandText = "SELECT Critica FROM Criticas c " + 
        //                               " JOIN LinksVideos l ON c.Video = l.Id " + 
        //                               " WHERE l.Id = '" +id+ "' ";
        //         
        //         _context.Database.OpenConnection();
        //         using (var result = command.ExecuteReader()) {
        //             if (result.HasRows) {
        //                 while (result.Read()) {
        //                     res = result.GetString("Critica");
        //                 }
        //             } else {
        //                 Console.WriteLine("No rows found.");
        //             }
        //         }
        //     }
        //     return res;
        // }
        
        // get user video
        // public static string getVideosPerUser(string email) {
        //     string titulo = "";
        //     string link = "";
        //     using (var command = _context.Database.GetDbConnection().CreateCommand()) {
        //         command.CommandText = "select distinct * from LinksVideos l join Utilizadores U on l.UtilizadorId = U.Id where u.Email = '" +email+ "' ";
        //         
        //         _context.Database.OpenConnection();
        //         using (var result = command.ExecuteReader()) {
        //             if (result.HasRows) {
        //                 while (result.Read()) {
        //                     
        //                     titulo = result.GetString("Titulo");
        //                     link = result.GetString("Link");
        //                 }
        //             } else {
        //                 Console.WriteLine("No rows found.");
        //             }
        //         }
        //     }
        //     return titulo;
        // }
        
        
        // register
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Register([Bind("Id," +
        //                                               "Nome," +
        //                                               "Email," +
        //                                               "Password," +
        //                                               "Tipo," +
        //                                               "Descricao," +
        //                                               "Linkedin," +
        //                                               "Telefone")] Utilizador utilizadores) {
        //     if (ModelState.IsValid) {
        //         _context.Add(utilizadores);
        //         await _context.SaveChangesAsync();
        //
        //         ViewBag.Message = "<script>alert('Utilizador registado com sucesso');</script>";
        //         Console.WriteLine("ViewBag.MessageViewBag.Message " +ViewBag.Message.ToString());
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View("Index");
        // }


        
    }
}