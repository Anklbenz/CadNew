using UnityEngine;
using UnityEngine.UI;

public class ReferenceButton : MonoBehaviour {
    [SerializeField] private Button button;
    
    private string _url;
    
    public void Initialize(string url){
        _url = url;
    }

    public void SetEnable(bool enable){
        button.interactable = enable;
    }
}
