using AgendaApiLucianoSvaikaukas.Data.Repository.Interfaces;
using AgendaApiLucianoSvaikaukas.Entities;
using AgendaApiLucianoSvaikaukas.Models;
using AutoMapper;


namespace AgendaApiLucianoSvaikaukas.Data.Repository.Implementations
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AgendaContext _context;
        private readonly IMapper _mapper;

        public GroupRepository(AgendaContext context, IMapper autoMapper)
        {
            _context = context;
            _mapper = autoMapper;
        }
        public List<Group> GetAll()
        {
            return _context.Groups.ToList();
        }

        public void Create(GroupForCreationDTO dto)
        {
            _context.Groups.Add(_mapper.Map<Group>(dto));
        }

        public void Update(GroupForCreationDTO dto)
        {
            _context.Groups.Update(_mapper.Map<Group>(dto));
        }
        public void Delete(int id)
        {
            _context.Groups.Remove(_context.Groups.Single(g => g.Id == id));
        }

    }
}
