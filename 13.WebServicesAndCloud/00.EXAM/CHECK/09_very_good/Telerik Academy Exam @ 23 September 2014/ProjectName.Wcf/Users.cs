using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Http;

namespace BullsAndCows.Wcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProjectModel1Service" in both code and config file together.
    [ServiceContract]
    public interface Users
    {
        [OperationContract]
        [WebGet(UriTemplate = "")]
        string GetUsers();

        
        [WebGet(UriTemplate = "/?page={page}")]
        string GetUsersWithPage(int page);

        [WebGet(UriTemplate = "/{id}")]
        string GetUserById(string id);
    }
}
