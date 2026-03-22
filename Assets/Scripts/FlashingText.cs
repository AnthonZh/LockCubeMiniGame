using UnityEngine;
using TMPro;

public class FlashingText : MonoBehaviour
{
    public TMP_Text text;
    public float flashSpeed = 1f;
    void Update()
    {
        float alpha = Mathf.Round(Mathf.PingPong(Time.unscaledTime * flashSpeed, 1f));
        text.alpha = alpha;
    }
}
