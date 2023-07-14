
using AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces;
using AgendaApiLucianoSvaikaukas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AgendaApiLucianoSvaikaukas.Controllers
{
    [Route("api/[controller]")] //AUTOCOMPLETA SOLO CON CONTACT
    [ApiController]
    [Authorize]
    public class ContactController : ControllerBase //HEREDA DE:
    {
        //INYECCIÓN DE DEPENDENCIAS
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        //met en el repository getall user
        //mandarle que user
        [HttpGet]
        public IActionResult GetAll() //ActionResult -- tipo de devolución que usamos para los endpoints
        {
            int userId = Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value); //es un enum que tiene todos los tipos de claim
            //return Ok(_contactRepository.GetAll());
            return Ok(_contactRepository.GetAllByUser(userId));
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetOne(int Id)
        {
            var userId = Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var contacts = _contactRepository.GetAll(userId).Where(x => x.Id == Id && x.UserId == userId).ToList();
            return Ok(contacts);

        }


        [HttpPost] //nuevo contacto
        public IActionResult CreateContact(ContactForCreationDTO createContactDto)
        {
            try
            {
                var userId =  Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
               
                _contactRepository.Create(createContactDto, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("Created", createContactDto);
        }

        [HttpPut] //actualizar contacto
        public IActionResult UpdateContact(ContactForCreationDTO dto, int contactId)
        {
            try
            {
                var userId = Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
                
                _contactRepository.Update(dto, userId, contactId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return NoContent();
        }

        [HttpDelete] //eliminar contacto
        public IActionResult DeleteContactById(int id)
        {
            try
            {
                var userId = Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
                _contactRepository.Delete(id, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
    }
}
