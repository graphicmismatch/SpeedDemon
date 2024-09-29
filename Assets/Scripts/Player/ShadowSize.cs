using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEngine.Rendering.DebugUI;

public class ShadowSize : MonoBehaviour
{
    [SerializeField] Transform player;  
    [SerializeField] Vector2 PlayerYvallimit;
    [SerializeField] Vector2 ShadowSizelimit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = Vector2.one * map(ShadowSizelimit.x, ShadowSizelimit.y, PlayerYvallimit.x, PlayerYvallimit.y, player.position.y);
    }

    float map(float from , float to , float from2 , float to2 , float value )
    {
        if (value <= from2)
            return from;
        else if (value >= to2)
            return to;
        return (to - from) * ((value - from2) / (to2 - from2)) + from;
    }
}
