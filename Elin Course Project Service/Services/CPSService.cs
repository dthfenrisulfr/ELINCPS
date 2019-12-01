using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Elin_Course_Project_Service.Services
{
    public class CPSService : ELIN_CPS.ELIN_CPSBase
    {
        private readonly ILogger<CPSService> _logger;
        public CPSService(ILogger<CPSService> logger)
        {
            _logger = logger;
        }
        public override Task<Positions> GetOneFromPosition(PositionRequest request, ServerCallContext context)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBContexts.WindowsDBContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Fenrir\\source\\repos\\ElinCourseProjectService\\ElinCourseProjectService\\WindowsDataBase.mdf;Integrated Security=True;Connect Timeout=30");
            var cnt = new DBContexts.WindowsDBContext(optionsBuilder.Options);
            Positions position = new Positions();
            try
            {
                position = cnt.Positions.Find(1);
            }
            catch(Exception e)
            {
                _logger.LogWarning($"Ошибка в методе GetOneFromPosition {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                return Task.FromResult(position);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(position);
        }
    }
}
