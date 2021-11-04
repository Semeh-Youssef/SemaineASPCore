using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using WebASPCorePremiereAPP.Models;

namespace WebASPCorePremiereAPP.Controllers
{
    public class LivresController : Controller
    {
        private readonly LivreDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public LivresController(LivreDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Livres
        public async Task<IActionResult> Index()
        {
            var livreDbContext = _context.Livres.Include(l => l.Theme);
            return View(await livreDbContext.ToListAsync());
        }

        // GET: Livres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .Include(l => l.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // GET: Livres/Create
        public IActionResult Create()
        {

            // on envoie la liste des themes a la vue qui va les afficher dans un DownList
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Libelle");
            return View();
        }

        // POST: Livres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Livre livre, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                //Methode 1:
                //if (Photo != null && Photo.Length > 0)
                //{
                //    string extension = Path.GetExtension(Photo.FileName);
                //    string filename = Path.GetFileNameWithoutExtension(Photo.FileName) + Guid.NewGuid() + extension;
                //    string dossierImg = "Images/";
                //    var path = dossierImg + filename;
                //    using (Stream fileStream = new FileStream(path, FileMode.Create))
                //    {
                //        Photo.CopyTo(fileStream);
                //    }
                //    livre.Photo = filename;
                //}
                //livre.Photo = Photo.FileName;

                //Methode 2:
                // vérifier si le fichier est valider : 
                // enregistrer sur un dossier (partage de fichers, ....
                // 1- récupérer le chemin du dossier root du site
                string rootPath = webHostEnvironment.WebRootPath;

                string fileName = Path.GetFileNameWithoutExtension(Photo.FileName) + Guid.NewGuid() + Path.GetExtension(Photo.FileName);
                // on crée le chemin fichier qui va contenir l'image (chemin du fichier de dest)
                string path = Path.Combine(rootPath+"/Images/", fileName);
                // On copie la Photo dans le fichier de dest
                // FileAs -- n'existe plus alors on copie physiquement a l'aide du fileStream
                var fileStream = new FileStream(path, FileMode.Create); // on peut utiliser un using
                await Photo.CopyToAsync(fileStream); // faire une copie asynchrone
                fileStream.Close(); // libérer la ressource

                // recupérer le nom du fichier dans l'objet livre

                livre.Photo = fileName;

                _context.Add(livre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Libelle", livre.ThemeId);
            return View(livre);
        }

        // GET: Livres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
            {
                return NotFound();
            }
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Libelle", livre.ThemeId);
            return View(livre);
        }

        // POST: Livres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Annee,Resume,ISBN,Photo,ThemeId")] Livre livre)
        {
            if (id != livre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivreExists(livre.Id))
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
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Libelle", livre.ThemeId);
            return View(livre);
        }

        // GET: Livres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .Include(l => l.Theme)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // POST: Livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livre = await _context.Livres.FindAsync(id);
            _context.Livres.Remove(livre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivreExists(int id)
        {
            return _context.Livres.Any(e => e.Id == id);
        }
    }
}
