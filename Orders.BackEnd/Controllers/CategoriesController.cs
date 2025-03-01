using Microsoft.AspNetCore.Mvc;
using Orders.BackEnd.UnitsOfWork.Interfaces;
using Orders.Shared.Entities;

namespace Orders.BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : GenericController<Category>
    {
        public CategoriesController(IGenericUnitOfWork<Category> unitOfWork) : base(unitOfWork)
        {
        }
    }
}
