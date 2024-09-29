using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static float rageMeter;
    public static float timer;
    public static float maxTime = 2;
    public TMP_Text rageScore;
    public Image rageImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rageScore.text =""+ rageMeter;
        rageImage.fillAmount = timer / maxTime;
    }
}
