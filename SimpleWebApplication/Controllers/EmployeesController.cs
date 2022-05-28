using SimpleWebApplication.BasicAuthentication;
using SimpleWebApplication.DTO;
using SimpleWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleWebApplication.Controllers
{
    [Authorize]
    public class EmployeesController : ApiController
    {
        public HttpResponseMessage GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee();
            using (EmployeeDBContext dBContext = new EmployeeDBContext())
            {
                var result = dBContext.GetAllEmployee().ToList();
                for(int i=0;i<result.Count();i++)
                {
                    employee.ID = result[i].ID;
                    employee.FirstName = result[i].FirstName;
                    employee.LastName = result[i].LastName;
                    employee.Gender = result[i].Gender;
                    employee.Salary = Convert.ToDecimal(result[i].Salary);
                    employees.Add(employee);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, employees);
        }
        public HttpResponseMessage GetEmployeeById(int id)
        {
            Employee employee = new Employee();
            using (EmployeeDBContext dBContext = new EmployeeDBContext())
            {
                var result = dBContext.GetEmployeeByID(id).FirstOrDefault();
                if(result != null)
                {
                    employee.ID = result.ID;
                    employee.FirstName = result.FirstName;
                    employee.LastName = result.LastName;
                    employee.Gender = result.Gender;
                    employee.Salary = Convert.ToDecimal(result.Salary);
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
                else
                {
                    throw new Exception(String.Format("No Employee Found with ID={0}",id));
                }
            }
        }
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDBContext dBContext = new EmployeeDBContext())
                {
                    var result = dBContext.AddEmployee(employee.FirstName, employee.LastName, employee.Gender, Convert.ToInt32(employee.Salary)).FirstOrDefault();
                    if(result > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, result);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Duplicate User");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDBContext dBContext = new EmployeeDBContext())
                {
                    var result = dBContext.UpdateEmployeeById(id, employee.FirstName, employee.LastName, employee.Gender, Convert.ToInt32(employee.Salary));
                    if(result > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDBContext dBContext = new EmployeeDBContext())
                {
                    var result = dBContext.DeleteEmployeeByID(id);
                    if (result == -1)
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    else
                        return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
