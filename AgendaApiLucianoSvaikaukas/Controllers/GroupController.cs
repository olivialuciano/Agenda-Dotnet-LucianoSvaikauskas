using AgendaApiLucianoSvaikaukas.Data;
using AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces;
using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendaApiLucianoSvaikaukas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        

        //INYECCIÓN DE DEPENDENCIAS
        private readonly IGroupRepository _groupRepository;
        private readonly IContactRepository _contactRepository;
        private readonly AgendaContext _context;

        public GroupController(AgendaContext context, IGroupRepository groupRepository, IContactRepository contactRepository)
        {
            _context = context;
            _groupRepository = groupRepository;
            _contactRepository = contactRepository;
        }




        [HttpPost]
        public IActionResult CreateGroup([FromBody] GroupForCreationDTO groupDTO)
        {
            if (groupDTO == null)
            {
                return BadRequest();
            }
            int userId = Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            // Mapear el objeto GroupDTO a un objeto Group
            var group = new Group
            {
                Name = groupDTO.Name,
                UserId = userId
            };

            // Llamar al método en el repositorio para crear el grupo
            _groupRepository.CreateGroup(group);

            return Ok();
        }


        [HttpPost("{groupId}/assign-contact")]
        public IActionResult AssignContactToGroup(int groupId, [FromBody] ContactForAssignGroupDTO contactDTO)
        {
            if (contactDTO == null)
            {
                return BadRequest();
            }

            // Obtener el grupo por su ID
            var group = _groupRepository.GetGroupById(groupId);

            if (group == null)
            {
                return NotFound();
            }

            // Obtener el contacto por su ID
            var contact = _contactRepository.GetContactById(contactDTO.Id);

            if (contact == null)
            {
                return NotFound();
            }

            // Asignar el contacto al grupo
            group.Contacts.Add(contact);
            _context.SaveChanges();

            return Ok();
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

        [HttpPut("{groupId}/update-name")]
        public IActionResult UpdateGroupName(int groupId, [FromBody] string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                return BadRequest();
            }

            _groupRepository.UpdateGroupName(groupId, newName);

            return NoContent();
        }


        //[HttpPut] //actualizar grupo
        //public IActionResult UpdateGroup(GroupForCreationDTO dto, int groupId)
        //{
        //    try
        //    {
        //        var userId = Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);

        //        _groupRepository.Update(dto, userId, groupId);
        //        _context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //    return NoContent();
        //}

        [HttpDelete] //eliminar grupo
        [Route("{Id}")]
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

