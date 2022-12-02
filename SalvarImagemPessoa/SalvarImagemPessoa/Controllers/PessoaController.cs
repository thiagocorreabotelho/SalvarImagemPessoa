using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalvarImagemPessoa.Models;

namespace SalvarImagemPessoa.Controllers
{
    public class PessoaController : Controller
    {
        private readonly PessoaContext _context;
        private string _filePath;

        public PessoaController(PessoaContext context, IWebHostEnvironment env)
        {
            _context = context;
            _filePath = env.WebRootPath;
        }

        // GET: Pessoa
        public async Task<IActionResult> Index()
        {
            return _context.PessoaModel != null ?
                        View(await _context.PessoaModel.ToListAsync()) :
                        Problem("Entity set 'PessoaContext.PessoaModel'  is null.");
        }

        // GET: Pessoa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PessoaModel == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // GET: Pessoa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Foto")] PessoaModel pessoaModel, IFormFile anexo)
        {
            var nome = SalvarArquivo(anexo);

            if (ModelState.IsValid)
            {

                if (!ValidarImagem(anexo))
                    return View(pessoaModel);

                if (!string.IsNullOrEmpty(nome))
                    pessoaModel.Foto = nome;

                _context.Add(pessoaModel);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(pessoaModel);
        }

        // GET: Pessoa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PessoaModel == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }
            return View(pessoaModel);
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Foto")] PessoaModel pessoaModel)
        {
            if (id != pessoaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaModelExists(pessoaModel.Id))
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
            return View(pessoaModel);
        }

        // GET: Pessoa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PessoaModel == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PessoaModel == null)
            {
                return Problem("Entity set 'PessoaContext.PessoaModel'  is null.");
            }
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel != null)
            {
                _context.PessoaModel.Remove(pessoaModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaModelExists(int id)
        {
            return (_context.PessoaModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #region Validação de imagens

        public bool ValidarImagem(IFormFile file)
        {
            switch (file.ContentType)
            {
                case "image/jpeg":
                    return true;

                case "image/bmp":
                    return true;

                case "image/gif":
                    return true;

                case "image/png":
                    return true;
                default:
                    return false;
                    break;
            }
        }

        public string SalvarArquivo(IFormFile file)
        {
            var nome = Guid.NewGuid().ToString() + file.FileName;
            var filePath = _filePath + "\\fotos";

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            using (var stream = System.IO.File.Create(filePath + "\\" + nome))
            {
                file.CopyToAsync(stream);
            }

            return nome;
        }
        #endregion
    }
}
