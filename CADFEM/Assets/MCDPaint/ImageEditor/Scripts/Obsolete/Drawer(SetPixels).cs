using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Drawer1  {
    public Color PencilColor{ get; set; } = new(1,0,0,1f);
    public int  PencilSize{ get; set; } = 10;

    private readonly RawImage _canvasImage;
    private readonly Color _backgroundColor = new Color(0, 0, 0, 0);
    
    private Color[] _backgroundColors;
    private Vector2? _lastPos;

    public Drawer1(RawImage canvasImage){
        _canvasImage = canvasImage;
        Initialize();
    }
    private void Initialize(){
        var texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);

        _backgroundColors = Enumerable.Repeat(_backgroundColor, texture.width * texture.height).ToArray();
        texture.SetPixels(_backgroundColors);
        texture.Apply();

        _canvasImage.texture = texture;
    }

    public void Draw(Vector2 pos){
        //  pos /= m_scaleFactor;
        var mainTex = _canvasImage.texture;
        var tex2d = new Texture2D(mainTex.width, mainTex.height, TextureFormat.RGBA32, false);

        var curTex = RenderTexture.active;
        var renTex = new RenderTexture(mainTex.width, mainTex.height, 32);

        Graphics.Blit(mainTex, renTex);
        RenderTexture.active = renTex;

        tex2d.ReadPixels(new Rect(0, 0, mainTex.width, mainTex.height), 0, 0);

        //var col = IsEraser ? backroundColour : penColour;
        var positions = _lastPos.HasValue ? GetLinearPositions(_lastPos.Value, pos) : new List<Vector2>() { pos };

        foreach (var position in positions){
            var pixels = GetNeighbouringPixels(new Vector2(mainTex.width, mainTex.height), position, PencilSize);

            if (pixels.Count > 0)
                foreach (var p in pixels)
                    tex2d.SetPixel((int)p.x, (int)p.y, PencilColor);
        }

        tex2d.Apply();

        RenderTexture.active = curTex;
        renTex.Release();
        Object.Destroy(renTex);
        Object.Destroy(mainTex);
        curTex = null;
        renTex = null;
        mainTex = null;

        _canvasImage.texture = tex2d;
        _lastPos = pos;
    }

    public void StopDraw() => _lastPos = null;

    private List<Vector2> GetNeighbouringPixels(Vector2 textureSize, Vector2 position, int brushRadius){
        var pixels = new List<Vector2>();

        for (var i = -brushRadius; i < brushRadius; i++){
            for (var j = -brushRadius; j < brushRadius; j++){
                var pxl = new Vector2(position.x + i, position.y + j);
                if (pxl.x > 0 && pxl.x < textureSize.x && pxl.y > 0 && pxl.y < textureSize.y)
                    pixels.Add(pxl);
            }
        }

        return pixels;
    }

    private List<Vector2> GetLinearPositions(Vector2 firstPos, Vector2 secondPos, int spacing = 2){
        var positions = new List<Vector2>();
        var dir = secondPos - firstPos;

        if (dir.magnitude <= spacing){
            positions.Add(secondPos);
            return positions;
        }

        for (int i = 0; i < dir.magnitude; i += spacing){
            var v = Vector2.ClampMagnitude(dir, i);
            positions.Add(firstPos + v);
        }

        positions.Add(secondPos);
        return positions;
    }
}