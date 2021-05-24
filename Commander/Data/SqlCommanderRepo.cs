using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext m_Context;

        public SqlCommanderRepo(CommanderContext context)
        {
            m_Context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            m_Context.Commands.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            m_Context.Commands.Remove(cmd);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return m_Context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return m_Context.Commands.FirstOrDefault(c => c.Id == id);
        }

        public bool SaveChanges()
        {
           return m_Context.SaveChanges() >= 0;
        }

        public void UpdateCommand(Command cmd)
        {
        }
    }
}
