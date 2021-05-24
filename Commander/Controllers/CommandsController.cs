using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Commander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo m_Repo;
        private readonly IMapper m_Mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            m_Repo = repository;
            m_Mapper = mapper;
        }

        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = m_Repo.GetAllCommands();

            return Ok(m_Mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
            var commandItem = m_Repo.GetCommandById(id);

            if (commandItem != null)
            {
                return Ok(m_Mapper.Map<CommandReadDto>(commandItem));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            Command commandModel = m_Mapper.Map<Command>(commandCreateDto);
            m_Repo.CreateCommand(commandModel);
            m_Repo.SaveChanges();

            CommandReadDto commandReadDto = m_Mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { commandReadDto.Id }, commandReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            Command commandModel = m_Repo.GetCommandById(id);

            if (commandModel == null)
            {
                return NotFound();
            }

            // Update command
            m_Mapper.Map(commandUpdateDto, commandModel);

            m_Repo.UpdateCommand(commandModel);

            m_Repo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            Command commandModel = m_Repo.GetCommandById(id);

            if (commandModel == null)
            {
                return NotFound();
            }

            m_Repo.DeleteCommand(commandModel);
            m_Repo.SaveChanges();

            return NoContent();
        }
    }
}
