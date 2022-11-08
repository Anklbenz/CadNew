using System.Net;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ThingWorxAPI : MonoBehaviour
{
    private const string THINGWORX_APP_KEY = "f154cb1a-e591-4a28-8c58-9c9b8f740d0e";
    
    private const string THINGWORX_AR_THING = "CADFEM.Solutions.EAM.AugmentedRealityServicesThing";

    private readonly string _url = $"https://twxall-win-dev.digitaltwin.ru/Thingworx/Things/{THINGWORX_AR_THING}/Services/";

    [System.Serializable]
    public class CounterData
    {
        public int counter;
    }

    [System.Serializable]
    public class AssetData
    {
        public string Number;
        public string Id;
        public string displayName;
        public string description;
    }

    [System.Serializable]
    public class AssetsData
    {
        public AssetData[] rows;
    }

    [System.Serializable]
    public class SaveDataInfo
    {
        public int numberProperty;
        public string textProperty;
        public AssetData jsonProperty;
    }

    private async Task<string> GetThingWorxARServiceResult(string serviceName, string jsonData = ""){
        string url = _url + serviceName;
        //  string url = $"https://twxall-win-dev.digitaltwin.ru/Thingworx/Things/{THINGWORX_AR_THING}/Services/{serviceName}";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.Accept = "application/json";
        request.ContentType = "application/json";

        request.Headers.Add("appKey", THINGWORX_APP_KEY);

        if (jsonData != ""){
            Debug.Log("GetThingWorxARServiceResult jsonData: " + jsonData);

            StreamWriter stream = new StreamWriter(request.GetRequestStream());
            stream.Write(jsonData);
            stream.Close();
        }

        HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

        StreamReader reader = new StreamReader(response.GetResponseStream());
        string result = reader.ReadToEnd();

        Debug.Log("GetThingWorxARServiceResult Response: " + result);

        return result;
    }

    private async Task<CounterData> GetCounterFromThingworx()
    {
        string json = await GetThingWorxARServiceResult("GetCounterValue");

        CounterData counter = JsonUtility.FromJson<CounterData>(json);
        return counter;        
    }

    /// <summary>
    /// Метод для вызова сервиса загрузки активов (GetAssets) и преобразования результата в AssetsData
    /// </summary>
    /// <returns>Возвращает список активов</returns>
    private async Task<AssetsData> GetAssetsFromThingworx()
    {
        string json = await GetThingWorxARServiceResult("GetAssets");

        AssetsData assets = JsonUtility.FromJson<AssetsData>(json);
        return assets;
    }

   

    /// <summary>
    /// Метод для демонстрации загрузки активов из ThingWorx по REST API
    /// </summary>
    public async void LoadAssetsButton_Clicked()
    {
        Text txt = transform.Find("AssetsListText").GetComponent<Text>();
        txt.text = "---";

        AssetsData assets = await GetAssetsFromThingworx();
        
        string result = $"Total: {assets.rows.Length}\n";
        foreach(var asset in assets.rows)
        {
            result += $"{asset.Number}: {asset.Id}\n";
        }
        
        txt.text = result;
    }


    /// <summary>
    /// Метод для демонстрации передачи данных в ThingWorx по REST API в параметры сервиса SaveData
    /// </summary>
    public async void SaveDataButton_Clicked()
    {
        string textProperty = transform.Find("TextPropertyInputField").GetComponent<InputField>().text;
        string numberProperty = transform.Find("NumberPropertyInputField").GetComponent<InputField>().text;

        SaveDataInfo sdi = new SaveDataInfo();
        
        sdi.textProperty = textProperty;
        
        int.TryParse(numberProperty, out sdi.numberProperty);
        
        sdi.jsonProperty = new AssetData();
        sdi.jsonProperty.Id = "Asset_Test Equipment";
        sdi.jsonProperty.Number = "AS00000123";
        sdi.jsonProperty.displayName = "Test Equipment";
        sdi.jsonProperty.description = "Equipment Description";
        
        string json = JsonUtility.ToJson(sdi);

        await GetThingWorxARServiceResult("SaveData", json);
    } 
    public async void LoadCounterButton_Clicked()
         {
             Text txt = transform.Find("CounterText").GetComponent<Text>();
             txt.text = "---";
     
             CounterData cnt = await GetCounterFromThingworx();
             string result = cnt.counter.ToString();
             
             txt.text = result;
         }
}