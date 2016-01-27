using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace NorthwindService_Egen
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmployeeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmployeeService.svc or EmployeeService.svc.cs at the Solution Explorer and start debugging.
    public class EmployeeService : IEmployeeService
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["theDB"].ToString();
        public Employee GetEmployeeByID(int EmployeeID)
        {
            Employee emp = new Employee();

            string getQuery = "SELECT [EmployeeID], [LastName], [FirstName], [Title], [Address], [City], [Country], [Notes]" +
                 "FROM [NORTHWND].[dbo].[Employees] WHERE [EmployeeID] =" + EmployeeID;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(getQuery, connection);

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    emp.EmployeeID = Convert.ToInt32(reader["EmployeeID"].ToString());
                    emp.LastName = reader["LastName"].ToString();
                    emp.FirstName = reader["FirstName"].ToString();
                    emp.Title = reader["Title"].ToString();
                    emp.Address = reader["Address"].ToString();
                    emp.City = reader["City"].ToString();
                    emp.Country = reader["Country"].ToString();
                    emp.Notes = reader["Notes"].ToString();
                }
            }
            return emp;           
        }

        public int saveEmployee(int EmployeeID, string LastName, string FirstName, string Title, string Address, string City, string Country, string Notes)
        {
            string updateQuery = ""
        }
    }
}
