using System;
using UnityEngine;

public class SpriteToBase64Converter {
    public string SpriteToBase64(Sprite sprite){
        var texture = SpriteToTexture(sprite);
        
        byte[] imageData = texture.EncodeToPNG();
        return  Convert.ToBase64String(imageData);
    }

    private Texture2D SpriteToTexture(Sprite sprite){
        var newText = new Texture2D((int)sprite.rect.width,(int)sprite.rect.height);
       
        Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x, 
            (int)sprite.textureRect.y, 
            (int)sprite.textureRect.width, 
            (int)sprite.textureRect.height );
       
        newText.SetPixels(newColors);
        newText.Apply();
        return newText;
    }
}
