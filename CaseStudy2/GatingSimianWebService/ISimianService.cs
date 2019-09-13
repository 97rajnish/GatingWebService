using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GatingWebService
{
  
    [ServiceContract]
    public interface ISimianService
    {

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/simianservice", RequestFormat = WebMessageFormat.Json,ResponseFormat =WebMessageFormat.Json)]
        string GateSimianReport(InputRepoModel inputModel);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/configuresimian", RequestFormat = WebMessageFormat.Json, ResponseFormat =WebMessageFormat.Json)]
        string ConfigureSimian(SimianOptions options);

    }

    [DataContract]
    public class InputRepoModel
    {
        [DataMember]
        public string gitRepo { get; set; }
        [DataMember]
        public int SimianDuplicatesThreshold { get; set; }
    }
    [DataContract]
    public class SimianOptions
    {
        public static readonly string temp = "+";
        [DataMember]
        public string ignoreCurlyBraces { get; set; }
        [DataMember]
        public string ignoreStringCase { get; set; }
        [DataMember]
        public string ignoreCharacterCase { get; set; }
        [DataMember]
        public string language { get; set; }
        [DataMember]
        public int threshold { get; set; }


        public override string ToString()
        {
            return " -ignoreCurlyBraces" + ignoreCurlyBraces + " -ignoreStringCase" + ignoreStringCase
                + " -ignoreCharacterCase" + ignoreCharacterCase + " -language=" + language +
                " -threshold=" + threshold + " -failOnDuplication" + temp + " ";
        }

    }

}
