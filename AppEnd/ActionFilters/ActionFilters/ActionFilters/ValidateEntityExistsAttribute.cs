using ActionFilters.Contracts;
using ActionFilters.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace ActionFilters.ActionFilters
{
    public class ValidateEntityExistsAttribute<T> : IActionFilter where T: class, IEntity
    {
        private readonly MovieContext _context;

        public ValidateEntityExistsAttribute(MovieContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Guid id = Guid.Empty;

            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (Guid)context.ActionArguments["id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }

            var entity = _context.Set<T>().SingleOrDefault(x => x.Id.Equals(id));     
            if(entity == null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("entity", entity);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
