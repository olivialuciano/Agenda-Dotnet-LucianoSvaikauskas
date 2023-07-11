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
            return Ok(_groupRepository.GetAll());
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetOne(int Id)
        {
            return Ok(_groupRepository.GetAll().Where(x => x.Id == Id));
        }


        [HttpPost] //nuevo contacto
        public IActionResult CreateGroup(GroupForCreationDTO createGroupDto)
        {
            try
            {
                _groupRepository.Create(createGroupDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("Created", createGroupDto);
        }

        [HttpPut] //actualizar contacto
        public IActionResult UpdateGroup(GroupForCreationDTO dto)
        {
            try
            {
                _groupRepository.Update(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return NoContent();
        }

        [HttpDelete] //eliminar contacto
        public IActionResult DeleteGroupById(int id)
        {
            try
            {
                _groupRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
    }
}

