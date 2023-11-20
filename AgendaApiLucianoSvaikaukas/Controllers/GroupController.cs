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
        

        private readonly IGroupRepository _groupRepository;
        private readonly IContactRepository _contactRepository;
        private readonly AgendaContext _context;

        public GroupController(AgendaContext context, IGroupRepository groupRepository, IContactRepository contactRepository)
        {
            _context = context;
            _groupRepository = groupRepository;
            _contactRepository = contactRepository;
        }



        ////////// GET //////////

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
            var groups = _groupRepository.GetGroupById(Id);
            return Ok(groups);
        }



        ////////// POST //////////

        [HttpPost]
        public IActionResult CreateGroup([FromBody] GroupForCreationDTO groupDTO)
        {
            if (groupDTO == null)
            {
                return BadRequest();
            }
            int userId = Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);

            var group = new Group
            {
                Name = groupDTO.Name,
                UserId = userId
            };

            _groupRepository.CreateGroup(group);

            return Ok();
        }


        [HttpPost("{groupId}/assign-contact")]
        public IActionResult AssignContactToGroup(ContactForAssignGroupDTO DTO)
        {
            try
            {
                _groupRepository.AddContact(DTO);
                return Created("Created", DTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("Created", DTO);


        }



        ////////// PUT //////////

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



        ////////// DELETE //////////

        [HttpDelete] 
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

