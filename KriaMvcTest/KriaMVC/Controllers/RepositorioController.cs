using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KriaMVC.Context;
using KriaMVC.Models;

namespace KriaMVC.Controllers
{
    public class RepositorioController : Controller
    {
        private readonly KriaContext _context;

        public RepositorioController(KriaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _context.Repositorios.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> SearchByName(string strSearch)
        {
            ViewData["GetRepositoryNames"] = strSearch;

            var query = from x in _context.Repositorios select x;
            if(!String.IsNullOrEmpty(strSearch))
            {
                query = query.Where(x => x.Nome.Contains(strSearch));
            }
            return View(query);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Repositorios == null)
            {
                return NotFound();
            }

            var repositorio = await _context.Repositorios
                .FirstOrDefaultAsync(m => m.RepoId == id);
            if (repositorio == null)
            {
                return NotFound();
            }

            return View(repositorio);
        }

        public IActionResult Create()
        {
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RepoId,Nome,Descricao,Linguagem,UltimaAtt,DonoRepositorio")] Repositorio repositorio)
            {
            if (ModelState.IsValid)
            {
                _context.Add(repositorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(repositorio);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Repositorios == null)
            {
                return NotFound();
            }

            var repositorio = await _context.Repositorios.FindAsync(id);
            if (repositorio == null)
            {
                return NotFound();
            }
            return View(repositorio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RepoId,Nome,Descricao,Linguagem,UltimaAtt,DonoRepositorio")] Repositorio repositorio)
        {
            if (id != repositorio.RepoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repositorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepositorioExists(repositorio.RepoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(repositorio);
        }
      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Repositorios == null)
            {
                return NotFound();
            }

            var repositorio = await _context.Repositorios
                .FirstOrDefaultAsync(m => m.RepoId == id);
            if (repositorio == null)
            {
                return NotFound();
            }

            return View(repositorio);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Repositorios == null)
            {
                return Problem("Entity set 'KriaContext.Repositorios'  is null.");
            }
            var repositorio = await _context.Repositorios.FindAsync(id);
            if (repositorio != null)
            {
                _context.Repositorios.Remove(repositorio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepositorioExists(int id)
        {
          return _context.Repositorios.Any(e => e.RepoId == id);
        }
    }
}