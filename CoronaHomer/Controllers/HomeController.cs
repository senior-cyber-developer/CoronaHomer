using CoronaHomer.Data;
using CoronaHomer.Models;
using CoronaHomer.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaHomer.Controllers
{
	public class HomeController : Controller
	{
		private static Random random = new Random();

		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _context;
		private readonly UserManager<User> _userManager;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<User> userManager)
		{
			_logger = logger;
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			ViewBag.NoMoreQuests = false;

			if (User.Identity.IsAuthenticated)
			{
				var user = await _userManager.GetUserAsync(User);
				var quest = await GetRandomQuestForUserAsync(user);

				if (quest == null)
				{
					ViewBag.NoMoreQuests = true;
					return View();
				}

				return View(quest);
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(int questId)
		{
			ViewBag.NoMoreQuests = false;

			var user = await _userManager.GetUserAsync(User);
			user = await _context
				.Users
				.Where(u => u.Id == user.Id).Include(u => u.CompletedQuests)
				.FirstAsync();

			var quest = await _context
				.Quest
				.Where(q => q.Id == questId)
				.FirstAsync();

			if (user.CompletedQuests.Where(uq => uq.QuestId == questId).Count() == 0)
			{
				user.SolidarityScore += quest.Score;
				user.CompletedQuests.Add(new UserQuest
				{
					User = user,
					Quest = quest,
				});

				await _context.SaveChangesAsync();
			}

			var newQuest = await GetRandomQuestForUserAsync(user);

			if (newQuest == null)
			{
				ViewBag.NoMoreQuests = true;
				return View();
			}

			return View(newQuest);
		}

		[HttpPost]
		public async Task CompleteQuestAsync(Quest quest)
		{
			var user = await _userManager.GetUserAsync(User);

			user.SolidarityScore += quest.Score;
			await _context.SaveChangesAsync();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public async Task<IActionResult> Ranking()
		{
			var top10 = await _context
				.Users
				.OrderByDescending(u => u.SolidarityScore)
				.Take(10)
				.ToListAsync();

			return View(top10);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		private async Task<Quest> GetRandomQuestForUserAsync(User user)
		{
			var questsCompletedByUser = await _context
				.UserQuest
				.Where(uq => uq.UserId == user.Id)
				.Select(uq => uq.Quest)
				.ToListAsync();

			var quests = await _context
				.Quest
				.Where(q => !questsCompletedByUser.Contains(q))
				.ToListAsync();

			if (quests.Count == 0)
			{
				return null;
			}

			return quests[random.Next(quests.Count)];
		}
	}
}