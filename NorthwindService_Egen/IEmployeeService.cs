using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NorthwindService_Egen
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployeeService" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        Employee GetEmployeeByID(int EmployeeID);

        [OperationContract]
        int saveEmployee(int EmployeeID, string LastName, string FirstName,
        string Title, string Address, string City, string Country, string Notes);
    }


}
