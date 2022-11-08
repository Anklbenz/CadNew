using UnityEngine;

[RequireComponent(typeof(CanvasLineRenderer))]
public class Pin : MonoBehaviour {
    public CanvasLineRenderer Renderer{ get; private set; }

    private void Awake(){
        Renderer = GetComponent<CanvasLineRenderer>();
    }

    public void Initialize(Color color, float width){
        Renderer.color = color;
        Renderer.LineWidth = width;
    }
}