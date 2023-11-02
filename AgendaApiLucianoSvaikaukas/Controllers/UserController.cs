
using AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces;
using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgendaApiLucianoSvaikaukas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserController(IUserRepository userRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            return Ok(_userRepository.GetAll());
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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

        [HttpPost("authenticate")]
        public ActionResult<string> Autenticar(AuthenticationRequestBody authenticationRequestBody) //Enviamos como parámetro la clase que creamos arriba
        {
            //Validamos las credenciales
            //llamar a una función que valide los parámetros que enviamos.
            var user = _userRepository.ValidateUser(authenticationRequestBody);

            if (user is null)
                return Unauthorized(); //st 401

            //Creación el token
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            //CLAIMS - data-valor ---- LA PARTE DEL PAYLOAD DEL JWT
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString())); //esto lo usamos para el user en el get del contact controller
            claimsForToken.Add(new Claim("given_name", user.Name));
            claimsForToken.Add(new Claim("family_name", user.LastName));
            //ACÁ SE CREA EL TOKEN
            var jwtSecurityToken = new JwtSecurityToken(
              _config["Authentication:Issuer"],
              _config["Authentication:Audience"],
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              credentials);

            var tokenToReturn = new JwtSecurityTokenHandler() //Pasamos el token a string
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }
        [HttpPost]//("newuser")
        public IActionResult PostUser(UserForCreationDTO dto)
        {
            try
            {
                //var user = _mapper.Map<User>(userDtoCreacion);
                var user = new User()
                {
                    Name = dto.Name,
                    LastName = dto.LastName,
                    Password = dto.Password,
                    Email = dto.Email,
                };

                var usersActivos = _userRepository.GetListUser();

                foreach (var userActivo in usersActivos)
                {
                    if (user.Email == userActivo.Email)
                    {
                        return BadRequest("El email ingresado ya es utilizado en una cuenta activa");
                    }
                }

                var userItem = _userRepository.AddUser(user);

                var userItemDto = _mapper.Map<UserForCreationDTO>(userItem);

                return Created("Created", userItemDto); ///*************
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }


}
