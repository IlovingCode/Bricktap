using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColoredBlockSprite : MonoBehaviour
{
    public void SetColor(Color color)
    {
        if(enabled) GetComponent<SpriteRenderer>().color = color;
    }
}
