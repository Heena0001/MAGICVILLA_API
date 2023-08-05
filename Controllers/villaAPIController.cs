using MAGICVILLA_API.Data;
using MAGICVILLA_API.models.DTO;
using Microsoft.AspNetCore.Mvc;
using MAGICVILLA_API.models;


namespace MAGICVILLA_API.Controllers
{   //[Route("api/[controller]")]
    [Route("api/villaAPI")]
    [ApiController]


    public class villaAPIController : ControllerBase
    {
        [HttpGet] //it defines the end point
        /*IEnumerable<models.Villa>: This is the return
        type of the method. It means the method will 
        return a collection (or list) of objects of type models.Villa.*/
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Villadto>> GetVillas() //by using action result we can return any value type that we want
        {
            return Ok(villastrore.villalist);
        }
        //by using [HttpGet] we geeting all the villas but if we want to get only one villa
        [HttpGet("{id:int}", Name = "GetVilla")]
        /*[ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]*/
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Villadto> GetVilla(int id)

        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = villastrore.villalist.FirstOrDefault(u => u.Id == id);
            //return Ok(villastrore.villalist.FirstOrDefault(u=>u.Id==id)); //list operation
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
        [HttpPost]//to creating a villa
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Villadto> CreateVilla([FromBody] Villadto villadto)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            if (villastrore.villalist.FirstOrDefault(u => u.Name.ToLower() == villadto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("customError", "Villa already Exists");
                return BadRequest(ModelState);
            }
            if (villadto == null)
            {
                return BadRequest(villadto);
            }
            if (villadto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //for fetching next id
            villadto.Id = villastrore.villalist.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            villastrore.villalist.Add(villadto);
            return CreatedAtRoute("GetVilla", new { id = villadto.Id }, villadto);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]

        public IActionResult DeleteVilla(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var villa =villastrore.villalist.FirstOrDefault(u =>u.Id == id);//if id!=0
            if (villa == null)
            {
                return NotFound();
            }
            villastrore.villalist.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id:int}",Name ="updateVilla")] //to update all ressource
        public IActionResult updateVilla(int Id , [FromBody]Villadto villadto)
        {
            if (villadto== null || Id != villadto.Id)
            {
                return BadRequest();
            }
            var villa = villastrore.villalist.FirstOrDefault(u => u.Id == Id);
            villa.Name= villadto.Name;
            villa.Sqft = villadto.Sqft;
            villa.Occupancy = villadto.Occupancy;

            return NoContent();
         
        }
    }
}
