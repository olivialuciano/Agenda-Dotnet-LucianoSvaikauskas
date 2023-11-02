
//using AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces;
//using AgendaApiLucianoSvaikaukas.Entities;
//using AgendaApiLucianoSvaikaukas.Models;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace AgendaApiLucianoSvaikaukas.Controllers
//{
//    [Route("api/authentication")]
//    [ApiController]
//    public class AuthenticationController : ControllerBase
//    {
//        private readonly IConfiguration _config;
//        private readonly IUserRepository _userRepository;
//        private readonly IMapper _mapper;


//        public AuthenticationController(IConfiguration config, IUserRepository userRepository, IMapper mapper)
//        {
//            _config = config; //INYECCIÓN DE DEPENDENCIAS para poder usar el appsettings.json
//            _userRepository = userRepository;
//            _mapper = mapper; 

//        }
        

//        [HttpPost("authenticate")]
//        public ActionResult<string> Autenticar(AuthenticationRequestBody authenticationRequestBody) //Enviamos como parámetro la clase que creamos arriba
//        {
//            //Validamos las credenciales
//            //llamar a una función que valide los parámetros que enviamos.
//            var user = _userRepository.ValidateUser(authenticationRequestBody);

//            if (user is null)
//                return Unauthorized(); //st 401

//            //Creación el token
//            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

//            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

//            //CLAIMS - data-valor ---- LA PARTE DEL PAYLOAD DEL JWT
//            var claimsForToken = new List<Claim>();
//            claimsForToken.Add(new Claim("sub", user.Id.ToString())); //esto lo usamos para el user en el get del contact controller
//            claimsForToken.Add(new Claim("given_name", user.Name));
//            claimsForToken.Add(new Claim("family_name", user.LastName));
//            //ACÁ SE CREA EL TOKEN
//            var jwtSecurityToken = new JwtSecurityToken(
//              _config["Authentication:Issuer"],
//              _config["Authentication:Audience"],
//              claimsForToken,
//              DateTime.UtcNow,
//              DateTime.UtcNow.AddHours(1),
//              credentials);

//            var tokenToReturn = new JwtSecurityTokenHandler() //Pasamos el token a string
//                .WriteToken(jwtSecurityToken);

//            return Ok(tokenToReturn);
//        }
//        [HttpPost]//("newuser")
//        public IActionResult PostUser(UserForCreationDTO dto)
//        {
//            try
//            {
//                //var user = _mapper.Map<User>(userDtoCreacion);
//                var user = new User()
//                {
//                    Name = dto.Name,
//                    LastName = dto.LastName,
//                    Password = dto.Password,
//                    Email = dto.Email,
//                };

//                var usersActivos = _userRepository.GetListUser();

//                foreach (var userActivo in usersActivos)
//                {
//                    if (user.Email == userActivo.Email)
//                    {
//                        return BadRequest("El email ingresado ya es utilizado en una cuenta activa");
//                    }
//                }

//                var userItem = _userRepository.AddUser(user);

//                var userItemDto = _mapper.Map<UserForCreationDTO>(userItem);

//                return Created("Created", userItemDto); ///*************
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//    }
//}