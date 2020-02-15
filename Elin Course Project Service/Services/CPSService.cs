using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CPS;

namespace Elin_Course_Project_Service.Services
{
    /// <summary>
    ///  Базовый класс gRPC сервиса отвечающий на запросы клиента
    /// </summary>
    public class CPSService : ELIN_CPS.ELIN_CPSBase
    {
        /// <summary>
        ///  Стандартный конструктор класса
        /// </summary>
        public CPSService(ILogger<CPSService> logger)
        {
            _logger = logger;
        }


        /// <summary>
        ///  В качестве ответа на запрос по ID метод отправляет информацию о должности
        /// </summary>
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
        /// <summary>
        ///  В качестве ответа на запрос по ID метод отправляет информацию о сотруднике
        /// </summary>
        public override Task<Staff> GetOneFromStaff(StaffRequest request, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            Staff staff = new Staff();
            try
            {
                //staff = cnt.Staff.Single(x => x.Passport == request.Passport);
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
        /// <summary>
        ///  В качестве ответа на запрос по ID метод отправляет информацию об отделе
        /// </summary>
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
        /// <summary>
        ///  В качестве ответа на запрос по ID метод отправляет информацию о заказчике
        /// </summary>
        public override Task<Customers> GetOneFormCustomers(CustomerRequest request, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            Customers customers = new Customers();
            try
            {
                //customers = cnt.Customers.Single(x => x.CustomerID == request.CustomerID);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе GetOneFormCustomers {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                return Task.FromResult(customers);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(customers);
        }
        /// <summary>
        ///  В качестве ответа на запрос по ID метод отправляет информацию о заказе
        /// </summary>
        public override Task<Orders> GetOneFromOrders(OrderRequest request, ServerCallContext context)
        {
            return base.GetOneFromOrders(request, context);
        }


        /// <summary>
        ///  В качестве ответа на запрос метод отправляет список должностей
        /// </summary>
        public override async Task GetPositionList(PositionRequest request, IServerStreamWriter<Positions> responseStream, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            List<Positions> positions;
            try
            {
                positions = cnt.Positions.ToList();

                foreach (var response in positions)
                {
                    await responseStream.WriteAsync(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе GetPositionList {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
            }
            finally
            {
                cnt.Dispose();
            }
        }
        /// <summary>
        ///  В качестве ответа на запрос метод отправляет список отделов
        /// </summary>
        public override async Task GetDepartmentList(DepartmentRequest request, IServerStreamWriter<Departments> responseStream, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            List<Departments> departments = new List<Departments>();
            try
            {
                departments = cnt.Departments.ToList();

                foreach (var response in departments)
                {
                    await responseStream.WriteAsync(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе GetDepartmentList {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
            }
            finally
            {
                cnt.Dispose();
            }
        }
        /// <summary>
        ///  В качестве ответа на запрос метод отправляет список сотрудников
        /// </summary>
        public override async Task GetStaffList(StaffRequest request, IServerStreamWriter<Staff> responseStream, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            List<Staff> staff = new List<Staff>();
            try
            {
                var temp = cnt.Staff.ToList();
                foreach (var t in temp)
                {
                    var bDate = t.BDate.Ticks;
                    var rDate = t.DateOfReceipt.Ticks;
                    staff.Add(new Staff { Passport = t.Passport, 
                        Name = t.Name, 
                        SecondName = t.SecondName, 
                        MiddleName = t.MiddleName, 
                        Experience = t.Experience,
                        Gender = t.Gender, 
                        BDate = bDate, 
                        DateOfReceipt = rDate, 
                        Department = cnt.Departments.Single(x => x.DepartmentID == t.DepartmentID).Department, 
                        Position = cnt.Positions.Single(x => x.PositionID == t.PositionID).Position});
                }
                foreach (var response in staff)
                {
                    await responseStream.WriteAsync(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе GetCustomersList {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
            }
            finally
            {
                cnt.Dispose();
            }
        }
        /// <summary>
        ///  В качестве ответа на запрос метод отправляет список заказчиков
        /// </summary>
        public override async Task GetCustomersList(CustomerRequest request, IServerStreamWriter<Customers> responseStream, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            List<Customers> customers = new List<Customers>();
            try
            {
                var temp = cnt.Customers.ToList();
                foreach (var t in temp)
                {
                    var tmp = t.DateOfContractCompletion.Ticks;
                    var customerInfo = cnt.CustomerInfo.Single(x => x.CustomerID == t.CustomerID);
                    customers.Add(new Customers{ CustomerID = t.CustomerID, 
                        DateOfContractCompletion = tmp, 
                        PaymentAccount = t.PaymentAccount, 
                        Address = customerInfo.Address, 
                        Name = customerInfo.Name, 
                        Organization = customerInfo.Organization, 
                        SecondName = customerInfo.SecondName });
                }
                foreach (var response in customers)
                {
                    await responseStream.WriteAsync(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе GetCustomersList {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
            }
            finally
            {
                cnt.Dispose();
            }
        }
        /// <summary>
        ///  В качестве ответа на запрос по ID метод отправляет список товаров
        /// </summary>
        public override async Task GetProductList(ProductRequest request, IServerStreamWriter<Products> responseStream, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            List<Products> products = new List<Products>();
            try
            {
                products = cnt.Products.ToList();

                foreach (var response in products)
                {
                    await responseStream.WriteAsync(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе GetProductList {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
            }
            finally
            {
                cnt.Dispose();
            }
        }
        /// <summary>
        ///  В качестве ответа на запрос метод отправляет список заказов
        /// </summary>
        public override async Task GetOrderList(OrderRequest request, IServerStreamWriter<Orders> responseStream, ServerCallContext context)
        {
            var cnt = new DBContexts.WindowsDBContext();
            List<Orders> orders= new List<Orders>();
            try
            {
                var BDorders = cnt.ProductsToOrders.ToList();

                for(int i = 1; i <= BDorders.Max(x=>x.OrderID); i++)
                {
                    var products = BDorders.Where(x => x.OrderID == i).Select(x=>x.ProductID).ToList();
                    string productList = "";
                    foreach(var prod in products)
                    {
                        productList += cnt.Products.Single(x=>x.ProductID == prod).ProductName  + ", ";
                    }
                    var order = cnt.Orders.Where(x=>x.OrderID == i).FirstOrDefault();
                    var cutomerInfo = cnt.CustomerInfo.Single(x => x.CustomerID == order.CustomerID);
                    var customer = cnt.Customers.Single(x => x.CustomerID == order.CustomerID);
                    var customerWithInfo = new Customers{ CustomerID = order.CustomerID, 
                        Address = cutomerInfo.Address, 
                        DateOfContractCompletion = customer.DateOfContractCompletion.Ticks, 
                        Name = cutomerInfo.Name, 
                        Organization = cutomerInfo.Organization, 
                        PaymentAccount = customer.PaymentAccount, 
                        SecondName = cutomerInfo.SecondName };

                    orders.Add(new Orders { OrderID = i, Customer = customerWithInfo, AllProducts = productList, Condition = order.Condition, Date = order.Date.Ticks });
                }
                foreach (var response in orders)
                {
                    await responseStream.WriteAsync(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе GetPositionList {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
            }
            finally
            {
                cnt.Dispose();
            }
        }


        /// <summary>
        ///  Полученную по HTTP\2 должность помещает в БД
        /// </summary>
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
        /// <summary>
        ///  Полученный по HTTP\2 отдел помещает в БД
        /// </summary>
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
        /// <summary>
        ///  Полученного по HTTP\2 сотрудника помещает в БД
        /// </summary>
        public override Task<Response> AddStaff(Staff request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                //cnt.Staff.Add(request);
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
        /// <summary>
        ///  Полученный по HTTP\2 заказ помещает в БД
        /// </summary>
        public override Task<Response> AddOrder(Orders request, ServerCallContext context)
        {
            return base.AddOrder(request, context);
        }
        /// <summary>
        ///  Полученный по HTTP\2 продукт помещает в БД
        /// </summary>
        public override Task<Response> AddProduct(Products request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                cnt.Products.Add(request);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.AddOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе AddProduct {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.AddFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }
        /// <summary>
        ///  Полученного по HTTP\2 заказчика помещает в БД
        /// </summary>
        public override Task<Response> AddSCustomer(Customers request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                //cnt.Customers.Add(request);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.AddOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе AddSCustomer {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.AddFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }


        /// <summary>
        ///  В качестве ответа на запрос по ID метод удаляет должность
        /// </summary>
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
        /// <summary>
        ///  В качестве ответа на запрос по ID метод удаляет отдел
        /// </summary>
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
        /// <summary>
        ///  В качестве ответа на запрос по ID метод удаляет сотрудника
        /// </summary>
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
        /// <summary>
        ///  В качестве ответа на запрос по ID метод удаляет заказчика
        /// </summary>
        public override Task<Response> DeleteCustomer(CustomerRequest request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                var delCustomer = cnt.Customers.Single(x => x.CustomerID == request.CustomerID);
                cnt.Customers.Remove(delCustomer);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.DeleteOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе DeleteCustomer {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.DeleteFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }
        /// <summary>
        ///  В качестве ответа на запрос по ID метод удаляет заказ
        /// </summary>
        public override Task<Response> DeleteOrder(OrderRequest request, ServerCallContext context)
        {
            return base.DeleteOrder(request, context);
        }
        /// <summary>
        ///  В качестве ответа на запрос по ID метод удаляет товар
        /// </summary>
        public override Task<Response> DeleteProduct(ProductRequest request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                var delProduct = cnt.Products.Single(x => x.ProductID == request.ProductID);
                cnt.Products.Remove(delProduct);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.DeleteOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе DeleteProduct {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.DeleteFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }


        /// <summary>
        ///  В качестве ответа на запрос по ID метод обновлет информацию о должности
        /// </summary>
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
        /// <summary>
        ///  В качестве ответа на запрос по ID метод обновляет информацию об отделе
        /// </summary>
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
        /// <summary>
        ///  В качестве ответа на запрос по ID метод обновляет информацию о сотруднике
        /// </summary>
        public override Task<Response> UpdateStaff(Staff request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                var temp = cnt.Staff.Single(x => x.Passport == request.Passport);
                cnt.Remove(temp);
                //cnt.Staff.Add(request);
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
        /// <summary>
        ///  В качестве ответа на запрос по ID метод обновляет информацию о заказчике
        /// </summary>
        public override Task<Response> UpdateCustomer(Customers request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                var temp = cnt.Customers.Single(x => x.CustomerID == request.CustomerID);
                cnt.Remove(temp);
                //cnt.Customers.Add(request);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.UpdateOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе UpdateCustomer {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.UpdateFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }
        /// <summary>
        ///  В качестве ответа на запрос по ID метод обновляет информацию о товаре
        /// </summary>
        public override Task<Response> UpdateProduct(Products request, ServerCallContext context)
        {
            Response response = new Response();
            var cnt = new DBContexts.WindowsDBContext();
            try
            {
                var temp = cnt.Products.Single(x => x.ProductID == request.ProductID);
                cnt.Remove(temp);
                cnt.Products.Add(request);
                cnt.SaveChanges();
                response.ResponseMessage = ResponseEnum.UpdateOk;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Ошибка в методе UpdateProduct {e.Message}" + '\n' + "Stack:" + '\n' + e.ToString());
                response.ResponseMessage = ResponseEnum.UpdateFail;
                return Task.FromResult(response);
            }
            finally
            {
                cnt.Dispose();
            }
            return Task.FromResult(response);
        }
        /// <summary>
        ///  В качестве ответа на запрос по ID метод обновляет информацию о заказе
        /// </summary>
        public override Task<Response> UpdateOrder(Orders request, ServerCallContext context)
        {
            return base.UpdateOrder(request, context);
        }

        private readonly ILogger<CPSService> _logger;
    }
}
