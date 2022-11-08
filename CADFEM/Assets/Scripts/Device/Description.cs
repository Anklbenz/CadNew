using System;

using UnityEngine;
 
    [Serializable]
    public class Description {
        [SerializeField] private string assetName;
        [SerializeField] private string label;
        [SerializeField] private Sprite presentImage;

        [TextArea(3, 10)]
        [SerializeField] private string description;

        public string AssetSystemName => assetName;
        public Sprite Image => presentImage;
        public string Label => label;
        public string AboutText => description;
    }
