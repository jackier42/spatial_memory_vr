using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLoadImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Texture2D tex = Resources.Load<Texture2D>("EmojiImages/1");
        Material mat = Resources.Load<Material>("EmojiImages/Materials/1");
        print(mat.name);
        this.GetComponentInChildren<RawImage>().texture = tex;
        
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
