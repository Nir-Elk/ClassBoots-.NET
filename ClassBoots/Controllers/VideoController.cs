using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassBoots.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ClassBoots.Areas.Identity.Data;
using Accord.MachineLearning.Rules;

namespace ClassBoots.Controllers
{
    [Authorize]
    public class VideoController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ModelContext _context;

        public VideoController(ModelContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Video
        public async Task<IActionResult> Index()
        {
            if (User.FindFirst("Role").Value == "Admin")
                return View(await _context.Video.ToListAsync());

            return NotFound("Access Dinied");
        }

        public async Task<IdentityResult> AddToUserHistory(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return null;
            string history = user.History;
            if (history == "" || history == null)
            {
                history = "" + id;
            }
            else
            {
                bool flag = false;
                List<string> historyList = history.Split(',').ToList();
                historyList.ForEach(x =>
                {
                    if ("" + id == x)
                        flag = true; // NIR?
                });

                if (!flag)
                    history += "," + id + "";
            }
            user.History = history;
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        // GET: Video/View/5 with full layout!
        public async Task<IActionResult> View(int? id)
        {

            if (id == null)
                return NotFound();

            var video = await _context.Video
                    .FirstOrDefaultAsync(m => m.ID == id);

            if (video == null)
                return NotFound();

            video.Views++;
            _context.Update(video);
            await AddToUserHistory(id);
            await _context.SaveChangesAsync();
            return View(video);
        }

        // GET: Video/Details/5 lightweight layout
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var video = await _context.Video
                    .FirstOrDefaultAsync(m => m.ID == id);
            video.Views++;
            _context.Update(video);
            await _context.SaveChangesAsync();
            if (video == null)
            {
                return NotFound();
            }
            await AddToUserHistory(id);
            return View(video);
        }

        // GET: Video/Create
        public IActionResult Create()
        {
            if (User.FindFirst("Role").Value != null)
                return View();
            return NotFound("Access Dinied");
        }

        // POST: Video/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,LectureID,URL,Views,Position,OwnerID")] Video video)
        {
            char[] videoURL = video.URL.ToArray();
            string videoID = "";
            for (int i = 0; i < videoURL.Length - 2; i++)
            {
                if (videoURL[i] == 'v' && videoURL[i + 1] == '=')
                {
                    for (int j = i + 2; j < i + 13; j++)
                    {
                        videoID += video.URL[j];
                    }
                    break;
                }
            }
            if (videoID != "")
            {
                video.URL = videoID;
                if (ModelState.IsValid)
                {
                    video.OwnerID = User.FindFirst(ClaimTypes.Name).Value;
                    _context.Add(video);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(video);
            }
            return NotFound("Video link broke.");
        }

        // GET: Video/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var video = await _context.Video.FindAsync(id);
            if (User.FindFirst("Role").Value == "Admin" || video.OwnerID == User.FindFirst(ClaimTypes.Name).Value)
            {
                if (video == null || id == null)
                    return NotFound();

                return View(video);
            }
            return NotFound("Access Dinied");

        }

        // POST: Video/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,LectureID,URL,Views,Position,OwnerID")] Video video)
        {
            if (User.FindFirst("Role").Value == "Admin" || video.OwnerID == User.FindFirst(ClaimTypes.Name).Value)
            {
                if (id != video.ID)
                {
                    return NotFound();
                }
                var oldVideo = _context.Video.Find(id);
                if (oldVideo != null)
                {
                    oldVideo.Name = video.Name;
                    oldVideo.URL = video.URL;
                    oldVideo.Position = video.Position;
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(oldVideo);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!VideoExists(video.ID))
                            return NotFound();
                        else
                            throw; // ILUZ?
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(video);
            }
            return NotFound("Access Dinied");
        }

        // GET: Video/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var video = await _context.Video
    .FirstOrDefaultAsync(m => m.ID == id);
            if (User.FindFirst("Role").Value == "Admin" || video.OwnerID == User.FindFirst(ClaimTypes.Name).Value)
            {
                if (video == null || id == null)
                    return NotFound();

                return View(video);
            }

            return NotFound("Access Dinied");
        }

        // POST: Video/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var video = await _context.Video.FindAsync(id);
            if (User.FindFirst("Role").Value == "Admin" || video.OwnerID == User.FindFirst(ClaimTypes.Name).Value)
            {
                _context.Video.Remove(video);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound("Access Dinied");
        }

        private bool VideoExists(int id)
        {
            return _context.Video.Any(e => e.ID == id);
        }

        public AssociationRuleMatcher<int> CreateAprioriClassifier()
        {
            // Create a new a-priori learning algorithm with support 3
            Apriori apriori = new Apriori(threshold: 2, confidence: 0);

            int[][] histories = GetAllViewingHistories();

            // Use the algorithm to learn a set matcher
            AssociationRuleMatcher<int> classifier = apriori.Learn(histories);

            return classifier;
        }

        [HttpGet("learn/{videoIDS}/{limit?}")]
        public List<Video> GetRelatedVideos([FromRoute] int[] videoIDS,[FromRoute] int limit = 5)
        {
            AssociationRuleMatcher<int> classifier = CreateAprioriClassifier();

            // Get the videos that appear most with the video who's ID is the given argument videoID
            int[][] matches = classifier.Decide(videoIDS);
            List<Video> videos = new List<Video>();
            HashSet<int> ids = new HashSet<int>();

            // Merge all the video IDs into a single HashSet, this will remove duplicates.
            foreach (int[] e in matches)
                foreach (int f in e)
                    ids.Add(f);
                
            // Take only the given amount of videos from the argument 'limit'.
           ids = ids.Take(limit).ToHashSet();

            // Get the full video model for each of the IDs.
            foreach (int id in ids)
                videos.Add(_context.Video.FirstOrDefault(video => video.ID == id));

            return videos;
        }

        public int[][] GetAllViewingHistories()
        {
            List<User> users = _userManager.Users.ToList();
            List<int[]> histories = new List<int[]>();
            users.ForEach(user =>
            {
                if(user.History != "" && user.History != null)
                    histories.Add(user.History.Split(',').Select(int.Parse).ToArray());
            });

            return histories.ToArray();
        }


    }
}
