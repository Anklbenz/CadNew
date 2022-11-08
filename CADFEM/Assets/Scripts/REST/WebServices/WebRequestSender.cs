using Cysharp.Threading.Tasks;
using UnityEngine;

public class WebRequestSender : RequestDataGetter {
    private string _servicesUrl;

    protected WebRequestSender(string hostUrl, string servicesUrl, string authorizationHeader) : base(hostUrl,authorizationHeader){
        _servicesUrl = hostUrl + servicesUrl;
    }

    public void SetServicesUrl(string url) => _servicesUrl = url;
    public void SetAuthorizationHeader(string header) => AuthorizationHeader = header;

    //T request result, TU param class
    protected async UniTask<T> GetServiceResult<T, TU>(string serviceName, TU param = null) where T : class where TU : class{
        var requestUrl = _servicesUrl + serviceName;
        var operationIdParamInJson = JsonUtility.ToJson(param);
        return await Get<T>(requestUrl, operationIdParamInJson);
    }

    protected async UniTask<T> GetServiceResult<T>(string serviceName) where T : class{
        var requestUrl = _servicesUrl + serviceName;
        return await Get<T>(requestUrl);
    }
}
