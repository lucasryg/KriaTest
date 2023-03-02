using KriaMVC.Context;
using KriaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace KriaMVC.Controllers
{
    public class FavoritoController : Controller
    {
        private readonly KriaContext _context;

        public FavoritoController(KriaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var favorito = _context.Favoritos.ToList();

            if (favorito == null)
            {
                return View();
            }

            foreach (var item in favorito)
            {
                var repositorio = await _context.Repositorios
               .FindAsync(item.RepoId);

                item.Repo = repositorio;
            }


            return View(favorito);
        }
       
        public IActionResult AddFav(int id, [Bind("FavId,RepoId")] Favorito favorito)
        {
            if (ModelState.IsValid)
            {
                favorito.RepoId = id;
                _context.Add(favorito);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteFav(int id)
        {
            var favorito = await _context.Favoritos.FindAsync(id);
            if (favorito != null)
            {
                _context.Favoritos.Remove(favorito);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}