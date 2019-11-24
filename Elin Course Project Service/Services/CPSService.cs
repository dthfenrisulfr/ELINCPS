using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
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
        public override Task<Staff> GetOneFromStaff(StaffRequest request, ServerCallContext context)
        {
            return Task.FromResult(new Staff
            {
                // Passport = //BDREQUEST;
                // Name = //BDREQUEST;
                // SecondName = //BDREQUEST;
                // MiddleName = //BDREQUEST;
                // BDate = //BDREQUEST;
                // Gender = //BDREQUEST;
                // DateOfReceipt = //BDREQUEST;
                // Experience = //BDREQUEST;
            });
        }
    }
}
