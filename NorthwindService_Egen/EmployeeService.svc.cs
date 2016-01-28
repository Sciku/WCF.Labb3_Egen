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
                using (SqlCommand cmd = new SqlCommand(getQuery, connection))
                { 
                    try
                    {
                        connection.Open();
                    }
                    catch (FaultException ex)
                    {

                        throw new FaultException($"Fel med tjänsten, se följande felmeddelande för mer information:\r\n{ex.Message}");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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
                        var nullEmployee = emp.LastName == null || emp.FirstName == null || emp.Title == null || emp.Address == null || emp.City == null || emp.Country == null || emp.Notes == null;

                        if (nullEmployee)
                        {
                            throw new FaultException($"Fel med tjänsten, se följande felmeddelande för mer information:");
                        }
                    }
                }                      
            }
            return emp;
        }

        public int saveEmployee(int EmployeeID, string LastName, string FirstName, string Title, string Address, string City, string Country, string Notes)
        {
            string updateQuery = "UPDATE [NORTHWND].[dbo].[Employees] SET" +
                " LastName = @LastName," +
                " FirstName = @FirstName," +
                " Title = @Title," +
                " Address = @Address," +
                " City = @City," +
                " Country = @Country," +
                " Notes = @Notes" +
                " WHERE EmployeeID = @EmployeeID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(updateQuery, connection);

                    SqlParameter paramEmployeeID = new SqlParameter("@EmployeeID", EmployeeID);
                    cmd.Parameters.Add(paramEmployeeID);

                    SqlParameter paramLastName = new SqlParameter("@LastName", LastName);
                    cmd.Parameters.Add(paramLastName);

                    SqlParameter paramFirstName = new SqlParameter("@FirstName", FirstName);
                    cmd.Parameters.Add(paramFirstName);

                    SqlParameter paramTitle = new SqlParameter("@Title", Title);
                    cmd.Parameters.Add(paramTitle);

                    SqlParameter paramAddress = new SqlParameter("@Address", Address);
                    cmd.Parameters.Add(paramAddress);

                    SqlParameter paramCity = new SqlParameter("@City", City);
                    cmd.Parameters.Add(paramCity);

                    SqlParameter paramCountry = new SqlParameter("@Country", Country);
                    cmd.Parameters.Add(paramCountry);

                    SqlParameter paramNotes = new SqlParameter("@Notes", Notes);
                    cmd.Parameters.Add(paramNotes);

                    connection.Open();
                    return cmd.ExecuteNonQuery();
                }
                catch (FaultException ex)
                {
                    throw new FaultException($"Fel med tjänsten, se följande felmeddelande för mer information:\r\n{ex.Message}");
                }

            }
        }
    }
}
