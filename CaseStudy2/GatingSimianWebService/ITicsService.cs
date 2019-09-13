using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace GatingWebService
{
    [ServiceContract]
    interface ITicsService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/ticsservice", RequestFormat = WebMessageFormat.Json)]
        string GateTicsReport(TicsInputRepoModel inputModel);
    }

    [DataContract]
    public class TicsInputRepoModel
    {
        [DataMember]
        public string gitRepo { get; set; }
        [DataMember]
        public int TicsErrorsThreshold { get; set; }
    }
}
