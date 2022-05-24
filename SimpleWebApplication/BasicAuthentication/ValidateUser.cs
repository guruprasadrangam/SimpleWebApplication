using SimpleWebApplication.DTO;
using SimpleWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebApplication.BasicAuthentication
{
    public class ValidateUser: IDisposable
    {
        public static bool Login(string username, string password)
        {
            if(username == null && password == null)
            {
                return false;
            }
            using (EmployeeDBContext employeeDBContext = new EmployeeDBContext())
            {
                var result = employeeDBContext.GetUser(username, password).FirstOrDefault();
                if (result.UserName != null && result.Password != null)
                    return true;
                else
                    return false;
            }
        }
        public User ValidateUserByUserNameAndPassword(string username, string password)
        {
            User user = new User();
            if (username == null && password == null)
            {
                return user;
            }
            using (EmployeeDBContext employeeDBContext = new EmployeeDBContext())
            {
                var result = employeeDBContext.GetUser(username, password).FirstOrDefault();
                if (result.UserName != null && result.Password != null)
                {
                    user.UserName = result.UserName;
                    user.Password = result.Password;
                    user.UserEmailID = result.UserEmailID;
                }
            }
            return user;
        }
        public void Dispose()
        {
            
        }
    }
}