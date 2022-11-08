using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MouseDraw1 : MonoBehaviour {

    private RawImage _canvasImage;

    private Color backgroundColor = new Color(0, 0, 0, 0);

    private int _penRadius = 10;
    private Color _penColor = Color.red;
    
    private Vector2? _lastPos;

    private void Start(){
        _canvasImage = transform.GetComponent<RawImage>();
       
        var texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
        for (var i = 0; i < texture.width; i++){
            for (var j = 0; j < texture.height; j++){
                texture.SetPixel(i,j,backgroundColor);
            }
        }        
        
        texture.Apply();
    
         _canvasImage.texture = texture;
    }
    
    private void Update(){

        if(Input.GetMouseButtonUp(0))
            _lastPos = null;
        if (!Input.GetMouseButton(0)) return;
        var position = Input.mousePosition;
      DrawPixels(position);
 
    }

    public void DrawPixels(Vector2 pos){
      //  pos /= m_scaleFactor;
        var mainTex =  _canvasImage.texture;
        var tex2d = new Texture2D(mainTex.width, mainTex.height, TextureFormat.RGBA32, false);

        var curTex = RenderTexture.active;
        var renTex = new RenderTexture(mainTex.width, mainTex.height, 32);

        Graphics.Blit(mainTex, renTex);
        RenderTexture.active = renTex;

        tex2d.ReadPixels(new Rect(0, 0, mainTex.width, mainTex.height), 0, 0);

        //var col = IsEraser ? backroundColour : penColour;
        var positions = _lastPos.HasValue ? GetLinearPositions(_lastPos.Value, pos) : new List<Vector2>() { pos };

        foreach (var position in positions){
            var pixels = GetNeighbouringPixels(new Vector2(mainTex.width, mainTex.height), position, _penRadius);

            if (pixels.Count > 0)
                foreach (var p in pixels)
                    tex2d.SetPixel((int)p.x, (int)p.y, _penColor);
        }

        tex2d.Apply();

        RenderTexture.active = curTex;
        renTex.Release();
        Destroy(renTex);
        Destroy(mainTex);
        curTex = null;
        renTex = null;
        mainTex = null;

        _canvasImage.texture = tex2d;
        _lastPos = pos;
    }
    
    
    private List<Vector2> GetNeighbouringPixels(Vector2 textureSize, Vector2 position, int brushRadius)
    {
        var pixels = new List<Vector2>();

        for (var i = -brushRadius; i < brushRadius; i++)
        {
            for (var j = -brushRadius; j < brushRadius; j++)
            {
                var pxl = new Vector2(position.x + i, position.y + j);
                if (pxl.x > 0 && pxl.x < textureSize.x && pxl.y > 0 && pxl.y < textureSize.y)
                    pixels.Add(pxl);
            }
        }

        return pixels;
    }
    
    private List<Vector2> GetLinearPositions(Vector2 firstPos, Vector2 secondPos, int spacing = 2)
    {
        var positions = new List<Vector2>();
        var dir = secondPos - firstPos;

        if (dir.magnitude <= spacing)
        {
            positions.Add(secondPos);
            return positions;
        }
        
        
        for (int i = 0; i < dir.magnitude; i += spacing)
        {
            var v = Vector2.ClampMagnitude(dir, i);
            positions.Add(firstPos + v);
        }

        positions.Add(secondPos);
        return positions;
    }
}