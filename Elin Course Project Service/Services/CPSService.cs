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

        #region GetSingleMethods
        public override Task<Positions> GetOneFromPosition(PositionRequest request, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            Positions position = new Positions();
            try
            {
                position = cnt.Positions.Single(x=>x.PositionID == request.PositionID);
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
        public override Task<Staff> GetOneFromStaff(StaffRequest request, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            Staff staff = new Staff();
            try
            {
                staff = cnt.Staff.Single(x => x.Passport == request.Passport);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе GetOneFromStaff {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                return Task.FromResult(staff);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(staff);
        }
        public override Task<Departments> GetOneFromDepartment(DepartmentRequest request, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            Departments departments = new Departments();
            try
            {
                departments = cnt.Departments.Single(x => x.DepartmentID == request.DepartmentID);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе GetOneFromDepartment {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                return Task.FromResult(departments);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(departments);
        }
        #endregion
        #region GetListsMethods
        public override Task GetPositionList(PositionRequest request, IServerStreamWriter<Positions> responseStream, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            List<Positions> position = new List<Positions>();
            try
            {
                position = cnt.Positions.ToList();
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе GetPositionList {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                return Task.FromResult(position);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(position);
        }
        public override Task GetDepartmentList(DepartmentRequest request, IServerStreamWriter<Departments> responseStream, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            List<Departments> position = new List<Departments>();
            try
            {
                position = cnt.Departments.ToList();
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе GetDepartmentList {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                return Task.FromResult(position);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(position);
        }
        public override Task GetStaffList(StaffRequest request, IServerStreamWriter<Staff> responseStream, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            List<Staff> position = new List<Staff>();
            try
            {
                position = cnt.Staff.ToList();
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе GetStaffList {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                return Task.FromResult(position);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(position);
        }
        #endregion
        #region AddMethods
        public override Task<Response> AddPosition(Positions request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                cnt.Positions.Add(request);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.AddOk;
            }
            catch(Exception e)
            {
                _logger.LogWarning($"Ошибка в методе AddPosition {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.AddFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }
        public override Task<Response> AddDepartment(Departments request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                cnt.Departments.Add(request);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.AddOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе AddDepartment {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.AddFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }
        public override Task<Response> AddStaff(Staff request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                cnt.Staff.Add(request);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.AddOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе AddStaff {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.AddFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }
        #endregion
        #region DeleteMethods
        public override Task<Response> DeletePosition(PositionRequest request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                var delPos = cnt.Positions.Single(x => x.PositionID == request.PositionID);
                cnt.Positions.Remove(delPos);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.DeleteOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе DeletePosition {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.DeleteFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }
        public override Task<Response> DeleteDepartment(DepartmentRequest request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                var delDep = cnt.Departments.Single(x => x.DepartmentID == request.DepartmentID);
                cnt.Departments.Remove(delDep);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.DeleteOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе DeleteDepartment {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.DeleteFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }
        public override Task<Response> DeleteStaff(StaffRequest request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                var delStaff = cnt.Staff.Single(x => x.Passport == request.Passport);
                cnt.Staff.Remove(delStaff);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.DeleteOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе DeleteStaff {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.DeleteFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }
        #endregion
        #region UpdateMethods
        public override Task<Response> UpdatePosition(Positions request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                var temp = cnt.Positions.Single(x => x.PositionID == request.PositionID);
                cnt.Remove(temp);
                cnt.Positions.Add(request);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.UpdateOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе UpdatePosition {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.UpdateFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }
        public override Task<Response> UpdateDepartment(Departments request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                var temp = cnt.Departments.Single(x => x.DepartmentID == request.DepartmentID);
                cnt.Remove(temp);
                cnt.Departments.Add(request);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.UpdateOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе UpdateDepartment {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.UpdateFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }
        public override Task<Response> UpdateStaff(Staff request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                var temp = cnt.Staff.Single(x => x.Passport == request.Passport);
                cnt.Remove(temp);
                cnt.Staff.Add(request);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.UpdateOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе UpdateStaff {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.UpdateFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }
        #endregion
    }
}
