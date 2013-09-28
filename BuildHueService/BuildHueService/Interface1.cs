using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BuildHueService
{
    [ServiceContract]
    public interface IStateNotify
    {
        [WebInvoke(Method = "POST", UriTemplate = "buildStatus/{build}/{result}")]
        //[WebGet(UriTemplate = "buildStatus/{build}/{result}")]
        [OperationContract]
        void SetState(string build, string result);
    }
}
