
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

        [HttpPost]
        public IActionResult CreateUser(UserForCreationDTO dto)
        {
            try
            {
                _userRepository.Create(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Created("Created", dto);
        }

        [HttpPut("{id}")] //para editar telefono y nombre//editUserData/
        public IActionResult EditUserData(int id, UserForModificationDTO dto)
        {
            try
            {
                //int userId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
                int userSesionId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                //var userId = Int32.Parse(HttpContext.User.Claims.First(e => e.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
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











        //[HttpDelete]
        //[Route("{Id}")]
        //public IActionResult DeleteUser(int Id)
        //{
        //    try
        //    {
        //        _userRepository.Delete(Id);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //    return Ok();
        //}
    }
}
