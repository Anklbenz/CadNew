using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Screenshot {
    public async UniTask<Sprite> Take(MonoBehaviour coroutineRunner){
        var screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        await UniTask.WaitForEndOfFrame(coroutineRunner);

        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();
        return Sprite.Create(screenShot, new Rect(0, 0, screenShot.width, screenShot.height), new Vector2(1f, 1f), 100f);
    }
}
