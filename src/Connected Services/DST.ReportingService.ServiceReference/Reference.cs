﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DST.ReportingService.ServiceReference
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DST.ReportingService.ServiceReference.IReportingService")]
    public interface IReportingService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/GetSurveyDefinition", ReplyAction="http://tempuri.org/IReportingService/GetSurveyDefinitionResponse")]
        System.Threading.Tasks.Task<DST.ReportingService.ServiceReference.GetSurveyDefinitionResponse> GetSurveyDefinitionAsync(DST.ReportingService.ServiceReference.GetSurveyDefinitionRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/SubmitReport", ReplyAction="http://tempuri.org/IReportingService/SubmitReportResponse")]
        System.Threading.Tasks.Task<DST.ReportingService.ServiceReference.SubmitReportResponse> SubmitReportAsync(DST.ReportingService.ServiceReference.SubmitReportRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReportingService/SubmitReportString", ReplyAction="http://tempuri.org/IReportingService/SubmitReportStringResponse")]
        System.Threading.Tasks.Task<DST.ReportingService.ServiceReference.SubmitReportStringResponse> SubmitReportStringAsync(DST.ReportingService.ServiceReference.SubmitReportStringRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetSurveyDefinition", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetSurveyDefinitionRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string surveyId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public System.DateTime periodBegin;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public System.DateTime periodEnd;
        
        public GetSurveyDefinitionRequest()
        {
        }
        
        public GetSurveyDefinitionRequest(string surveyId, System.DateTime periodBegin, System.DateTime periodEnd)
        {
            this.surveyId = surveyId;
            this.periodBegin = periodBegin;
            this.periodEnd = periodEnd;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetSurveyDefinitionResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class GetSurveyDefinitionResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string GetSurveyDefinitionResult;
        
        public GetSurveyDefinitionResponse()
        {
        }
        
        public GetSurveyDefinitionResponse(string GetSurveyDefinitionResult)
        {
            this.GetSurveyDefinitionResult = GetSurveyDefinitionResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SubmitReport", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class SubmitReportRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.Xml.XmlElement report;
        
        public SubmitReportRequest()
        {
        }
        
        public SubmitReportRequest(System.Xml.XmlElement report)
        {
            this.report = report;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SubmitReportResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class SubmitReportResponse
    {
        
        public SubmitReportResponse()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SubmitReportString", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class SubmitReportStringRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string report;
        
        public SubmitReportStringRequest()
        {
        }
        
        public SubmitReportStringRequest(string report)
        {
            this.report = report;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SubmitReportStringResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class SubmitReportStringResponse
    {
        
        public SubmitReportStringResponse()
        {
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    public interface IReportingServiceChannel : DST.ReportingService.ServiceReference.IReportingService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.1")]
    public partial class ReportingServiceClient : System.ServiceModel.ClientBase<DST.ReportingService.ServiceReference.IReportingService>, DST.ReportingService.ServiceReference.IReportingService
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ReportingServiceClient() : 
                base(ReportingServiceClient.GetDefaultBinding(), ReportingServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpsBinding_IReportingService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ReportingServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(ReportingServiceClient.GetBindingForEndpoint(endpointConfiguration), ReportingServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ReportingServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ReportingServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ReportingServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ReportingServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ReportingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<DST.ReportingService.ServiceReference.GetSurveyDefinitionResponse> GetSurveyDefinitionAsync(DST.ReportingService.ServiceReference.GetSurveyDefinitionRequest request)
        {
            return base.Channel.GetSurveyDefinitionAsync(request);
        }
        
        public System.Threading.Tasks.Task<DST.ReportingService.ServiceReference.SubmitReportResponse> SubmitReportAsync(DST.ReportingService.ServiceReference.SubmitReportRequest request)
        {
            return base.Channel.SubmitReportAsync(request);
        }
        
        public System.Threading.Tasks.Task<DST.ReportingService.ServiceReference.SubmitReportStringResponse> SubmitReportStringAsync(DST.ReportingService.ServiceReference.SubmitReportStringRequest request)
        {
            return base.Channel.SubmitReportStringAsync(request);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpsBinding_IReportingService))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpsBinding_IReportingService))
            {
                return new System.ServiceModel.EndpointAddress("https://srvtwebsvc1.dst.dk/ReportingService/ReportingService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return ReportingServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpsBinding_IReportingService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return ReportingServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpsBinding_IReportingService);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpsBinding_IReportingService,
        }
    }
}
