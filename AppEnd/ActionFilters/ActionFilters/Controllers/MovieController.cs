using System;
using System.Linq;
using ActionFilters.ActionFilters;
using ActionFilters.Entities;
using ActionFilters.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ActionFilters.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly MovieContext _context;

        public MovieController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var movies = _context.Movies.ToList();

            return Ok(movies);
        }

        [HttpGet("{id}", Name = "MovieById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Movie>))]
        public IActionResult Get(Guid id)
        {
            var dbMovie = HttpContext.Items["entity"] as Movie;

            return Ok(dbMovie);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Post([FromBody] Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return CreatedAtRoute("MovieById", new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Movie>))]
        public IActionResult Put(Guid id, [FromBody]Movie movie)
        {
            var dbMovie = HttpContext.Items["entity"] as Movie;

            dbMovie.Map(movie);

            _context.Movies.Update(dbMovie);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Movie>))]
        public IActionResult Delete(Guid id)
        {
            var dbMovie = HttpContext.Items["entity"] as Movie;

            _context.Movies.Remove(dbMovie);
            _context.SaveChanges();

            return NoContent();
        }
    }
}