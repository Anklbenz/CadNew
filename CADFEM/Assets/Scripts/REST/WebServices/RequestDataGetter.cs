#nullable enable
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public abstract class RequestDataGetter {
    private const int RESPONSE_OK = 200;
    private const string REQUEST_CONTENT_TYPE = "application/json";
    private const string REQUEST_METHOD = "POST";
    private string _hostUrl;

    public event Action<string>? OnServerResponseEvent;
    protected string AuthorizationHeader;

    protected RequestDataGetter(string hostUrl, string authorizationHeader){
        _hostUrl = hostUrl;
        AuthorizationHeader = authorizationHeader;
    }

    public async UniTask<T?> Get<T>(string url, string param = ""){
        var json = await GetRequestResponse(url, param);
        return json != null ? JsonUtility.FromJson<T>(json) : default;
    }

    private async UniTask<string?> GetRequestResponse(string url, string paramJson = ""){
        var webRequest = UnityWebRequest.Get(url);
        webRequest.method = REQUEST_METHOD;
        webRequest.SetRequestHeader("Accept", REQUEST_CONTENT_TYPE);
        webRequest.SetRequestHeader("Content-Type", REQUEST_CONTENT_TYPE);
        webRequest.SetRequestHeader("Authorization", AuthorizationHeader);
        //  webRequest.SetRequestHeader("appKey", THINGWORX_APP_KEY);
        if (paramJson != "")
            webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(paramJson));

        webRequest.certificateHandler = new ForceAcceptAllCertificates();
        var operation = webRequest.SendWebRequest();

        while (!operation.isDone)
            await UniTask.Yield();

        var responseCode = webRequest.responseCode;

        ResultNotify(responseCode, url);

        webRequest.certificateHandler.Dispose();
        webRequest.uploadHandler?.Dispose();

        return responseCode == RESPONSE_OK ? webRequest.downloadHandler.text : null;
    }

    private void ResultNotify(long responseCode, string url){
        var lastIndex = url.LastIndexOf("/", StringComparison.Ordinal);
        lastIndex++;
        var service = url.Substring(lastIndex, url.Length - lastIndex);
        OnServerResponseEvent?.Invoke($"{service}: {responseCode.ToString()} ");
    }

    public async UniTask<Texture2D?> GetTexture(string imageUrl){
        var url = _hostUrl + imageUrl;
        var webRequest = UnityWebRequestTexture.GetTexture(url);
     //   webRequest.method = REQUEST_METHOD;
        webRequest.SetRequestHeader("Accept", REQUEST_CONTENT_TYPE);
        webRequest.SetRequestHeader("Content-Type", REQUEST_CONTENT_TYPE);
        webRequest.SetRequestHeader("Authorization", AuthorizationHeader);
        webRequest.certificateHandler = new ForceAcceptAllCertificates();

        var operation = webRequest.SendWebRequest();

        while (!operation.isDone)
            await UniTask.Yield();

        var responseCode = webRequest.responseCode;

        webRequest.certificateHandler.Dispose();
        webRequest.uploadHandler?.Dispose();
       // webRequest.Dispose();

        return responseCode == RESPONSE_OK ? DownloadHandlerTexture.GetContent(webRequest) : null;
    }
}
