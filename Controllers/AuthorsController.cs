using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseLibrary.API.Services;
using CourseLibrary.API.Models;
using CourseLibrary.API.Helpers;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        public AuthorsController(ICourseLibraryRepository courseLibraryRepository)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
        }

        [HttpGet()]
        public IActionResult GetAuthors() {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();
            var authors = new List<AuthorDto>();
            
            foreach (var author in authorsFromRepo)
            {
                authors.Add(new AuthorDto()
                {
                    Id = author.Id,
                    Name = $"{author.FirstName} {author.LastName}",
                    MainCategory = author.MainCategory,
                    Age = author.DateOfBirth.GetCurrentAge()
                }); 
                
            }

            return Ok(authors);

        }

        [HttpGet("{authorId}")]
        public IActionResult GetAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);
            return Ok(authorFromRepo);
            
        }
        
        [HttpPost("{authorId}")]
        public IActionResult DeleteAuthor(Guid authorId)
        {
            if (_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            //_courseLibraryRepository.DeleteAuthor(authorId);
            return Ok();
        }
        
    }
}
