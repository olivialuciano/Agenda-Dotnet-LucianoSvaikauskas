using AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces;
using AgendaApiLucianoSvaikaukas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApiLucianoSvaikaukas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        //INYECCIÓN DE DEPENDENCIAS
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpGet]
        public IActionResult GetAll() //ActionResult -- tipo de devolución que usamos para los endpoints
        {
            int userId = Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value); //es un enum que tiene todos los tipos de claim
            return Ok(_groupRepository.GetAllByUser(userId));
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetOne(int Id)
        {
            var userId = Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var groups = _groupRepository.GetAll(userId).Where(x => x.Id == Id && x.UserId == userId).ToList();
            return Ok(groups);
        }


        [HttpPost] //nuevo grupo
        public IActionResult CreateGroup(GroupForCreationDTO createGroupDto)
        {
            try
            {
                var userId = Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
                _groupRepository.Create(createGroupDto, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("Created", createGroupDto);
        }

        [HttpPut] //actualizar grupo
        public IActionResult UpdateGroup(GroupForCreationDTO dto, int groupId)
        {
            try
            {
                var userId = Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);

                _groupRepository.Update(dto, userId, groupId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return NoContent();
        }

        [HttpDelete] //eliminar grupo
        public IActionResult DeleteGroupById(int id)
        {
            try
            {
                var userId = Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);

                _groupRepository.Delete(id, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
    }
}

