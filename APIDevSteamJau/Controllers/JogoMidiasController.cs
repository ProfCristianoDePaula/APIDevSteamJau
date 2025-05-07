using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDevSteamJau.Data;
using APIDevSteamJau.Models;
using Microsoft.AspNetCore.Identity;

namespace APIDevSteamJau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoMidiasController : ControllerBase
    {
        private readonly APIContext _context;

        public JogoMidiasController(APIContext context)
        {
            _context = context;
        }

        // GET: api/JogoMidias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoMidia>>> GetJogosMidia()
        {
            return await _context.JogosMidia.ToListAsync();
        }

        // GET: api/JogoMidias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JogoMidia>> GetJogoMidia(Guid id)
        {
            var jogoMidia = await _context.JogosMidia.FindAsync(id);

            if (jogoMidia == null)
            {
                return Ok();
            }

            return jogoMidia;
        }

        // PUT: api/JogoMidias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJogoMidia(Guid id, JogoMidia jogoMidia)
        {
            if (id != jogoMidia.JogoMidiaId)
            {
                return BadRequest();
            }

            _context.Entry(jogoMidia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JogoMidiaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/JogoMidias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JogoMidia>> PostJogoMidia(JogoMidia jogoMidia, IFormFile file, Guid jogoId)
        {
            // Verificar se o jogo existe
            var jogo = await _context.Jogos.FindAsync(jogoId);
            if (jogo == null)
            {
                return NotFound("Jogo não encontrado.");
            }

            // Verificar se o arquivo foi enviado
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nenhum arquivo enviado.");
            }

            // Verificar se o arquivo enviado é do tipo imagem ou vídeo
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".mp4", ".avi" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (allowedExtensions.Contains(fileExtension))
            {
                // Salvar o arquivo no servidor (ou em um serviço de armazenamento)
                var filePath = Path.Combine("Resources", "Medias", jogoId.ToString(), file.FileName + "_" + Guid.NewGuid());
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Definir se o Tipo é imagem ou vídeo
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    jogoMidia.Tipo = "Imagem";
                }
                else if (fileExtension == ".mp4" || fileExtension == ".avi")
                {
                    jogoMidia.Tipo = "Vídeo";
                }
                else
                {
                    return BadRequest("Tipo de arquivo não suportado.");
                }

                // Criar o objeto JogoMidia
                jogoMidia.JogoId = jogoId;
                jogoMidia.Jogo = jogo; // Associar o Jogo ao JogoMidia
                jogoMidia.JogoMidiaId = Guid.NewGuid(); // Gerar um novo ID para o JogoMidia
                jogoMidia.Url = filePath; // URL do arquivo salvo
                                          // Adicionar o JogoMidia ao contexto e salvar}

                _context.JogosMidia.Add(jogoMidia);
                await _context.SaveChangesAsync();
            }
            return CreatedAtAction("GetJogoMidia", new { id = jogoMidia.JogoMidiaId }, jogoMidia);
        }


        // DELETE: api/JogoMidias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJogoMidia(Guid id)
        {
            var jogoMidia = await _context.JogosMidia.FindAsync(id);
            if (jogoMidia == null)
            {
                return NotFound();
            }

            _context.JogosMidia.Remove(jogoMidia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JogoMidiaExists(Guid id)
        {
            return _context.JogosMidia.Any(e => e.JogoMidiaId == id);
        }
    }
}
