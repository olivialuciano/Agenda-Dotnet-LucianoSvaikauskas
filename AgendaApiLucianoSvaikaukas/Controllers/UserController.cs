
using AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces;
using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AgendaApiLucianoSvaikaukas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userRepository.GetAll());
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetOneById(int Id)
        {
            try
            {
                return Ok(_userRepository.GetById(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")] //para editar nombrem, apellido y email.
        public IActionResult EditUserData(int id, UserForModificationDTO dto)
        {
            try
            {
                int userSesionId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var user = new User()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    LastName = dto.LastName,
                    Email = dto.Email,
                };
                

                if (id != userSesionId)
                {
                    return Unauthorized();
                }

                if (id != user.Id)
                {
                    return Unauthorized();
                }

                var userItem = _userRepository.GetUser(id);

                if (userItem == null)
                {
                    return NotFound();
                }

                _userRepository.UpdateUserData(user);

                var userModificado = _userRepository.GetUser(id);

                var userModificadoDto = _mapper.Map<UserForCreationDTO>(userModificado);

                return Ok(userModificadoDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]//deleteUser/
        public IActionResult DeleteUser(int id)
        {
            try
            {
                int userSesionId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var user = _userRepository.GetUser(id);

                if (user == null)
                {
                    return NotFound();
                }

                if (id != userSesionId)
                {
                    return Unauthorized();
                }

                _userRepository.DeleteUser(user);

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
