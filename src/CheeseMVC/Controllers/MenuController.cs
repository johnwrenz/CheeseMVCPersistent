using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CheeseMVC.Controllers
{
	public class MenuController : Controller
	{
		private CheeseDbContext context;
		private IList<CheeseMenu> items;

		public MenuController(CheeseDbContext dbContext)
		{
			context = dbContext;
		}
		// GET: /<controller>/
		public IActionResult Index()
		{
			List<Menu> menus = context.Menus.ToList();
			return View(menus);
		}
		[HttpGet]
		public IActionResult Add()
		{
			AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
			return View(addMenuViewModel);
		}
		[HttpPost]
		public IActionResult Add(AddMenuViewModel addMenuViewModel)
		{
			if (ModelState.IsValid)
			{
				Menu newMenu = new Menu()
				// Add the new cheese to my existing cheeses
				//	CheeseCategory cheeseCategory = context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);
				//	Cheese newCheese = new Cheese
				{
					Name = addMenuViewModel.Name
					//Description = addCheeseViewModel.Description,
					//CategoryID = addCheeseViewModel.CategoryID,
					//Category = newCheeseCategory
					// Type = addCheeseViewModel.Type
				};
				context.Menus.Add(newMenu);
				context.SaveChanges();

				return Redirect("/Menu/ViewMenu/" + newMenu.ID);
			}
			return View(addMenuViewModel);
		}

		[HttpGet]
		public IActionResult ViewMenu(int id)
		{
			List<CheeseMenu> Items = context
				.CheeseMenus
				.Include(item => item.Cheese)
				.Where(cm => cm.MenuID == id)
				.ToList();
			Menu theMenu = context.Menus.Single(c => c.ID == id);

			ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel()
			{
				Items = items,
				Menu = theMenu
			};

			return View(viewMenuViewModel);
	}

		[HttpGet]
		public IActionResult AddItem(int id)
	{
		Menu theMenu = context.Menus.Single(m => m.ID == id);
		List<Cheese> cheeses = context.Cheeses.ToList();

		return View(new AddMenuItemViewModel(theMenu, cheeses));
	}

		[HttpPost]
		public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
	{
		if (ModelState.IsValid)
		{
			IList<CheeseMenu> existingItems = context.CheeseMenus
				.Where(cm => cm.CheeseID == addMenuItemViewModel.CheeseID)
				.Where(cm => cm.MenuID == addMenuItemViewModel.MenuID).ToList();

			if (existingItems.Count == 0)
			{
				CheeseMenu newCheeseMenu = new CheeseMenu();
				newCheeseMenu.MenuID = addMenuItemViewModel.MenuID;
				newCheeseMenu.CheeseID = addMenuItemViewModel.CheeseID;

				context.CheeseMenus.Add(newCheeseMenu);
				context.SaveChanges();

				return Redirect(string.Format("/Menu/ViewMenu/{0}", newCheeseMenu.MenuID));

			}

			return Redirect("/Menu");

		}
		return View(addMenuItemViewModel);

		}
	}
}


		/*	public IActionResult Remove()
			{
			ViewBag.title = "Remove Cheeses";
			ViewBag.cheeses = context.Cheeses.ToList();
			return View();
			}
			[HttoPost]
			public IActionResult Remove(int[] cheeseids)
			{
				foreach (int cheeseid in cheeseids)
				{
				Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseid);
				context.Cheeses.Remove(theCheese);
				}
				context.SaveChanges();
				return Redirect("/");
			}
		}
	}
	*/

