using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentStoreTemplate.Contexts;
using DocumentStoreTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Document = DocumentStoreTemplate.Models.Document;

namespace DocumentStoreTemplate.Controllers
{
    [Route("/api/v1/")]
    [ApiController]
    public class IndexController : Controller
    {
        private readonly DocumentContext _context;

        public IndexController()
        {
            _context = new DocumentContext();
            _context.Database.Migrate();
        }

        [HttpGet]
        public async Task<ActionResult<Document>> GetDocument()
        {
            return _context.Documents.Include(d => d.Rows).OrderByDescending(d => d.DocumentId).FirstOrDefault();
        }

        [HttpPost]
        public async Task<ActionResult<PostDocumentResponse>> PostDocument([FromBody] List<DocumentRow> documentRows)
        {
            Document document = new Document {Version = _context.Documents.Count() + 1, Rows = documentRows};

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return StatusCode(200, new PostDocumentResponse(document.Version));
        }

        public class PostDocumentResponse
        {
            public int Version;

            public PostDocumentResponse(int version)
            {
                Version = version;
            }
        }
    }
}