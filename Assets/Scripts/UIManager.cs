using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager inst;

    [SerializeField] Text infoNameText;
    [SerializeField] Text infoDescText;

    [SerializeField] Text successCounter;
    [SerializeField] Text failCounter;

    [SerializeField] float showTime;

    [SerializeField] Image successImage;
    [SerializeField] Image failImage;

    [SerializeField] float fadingSpeed;

    float timer;

    int successes;
    int fails;

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        
    }


    void Update()
    { 

        timer += Time.deltaTime;

        if (timer >= showTime)
            infoDescText.text = infoNameText.text = "";

        var sColor = successImage.color;
        sColor.a = Mathf.Lerp(sColor.a, 0f, fadingSpeed * Time.deltaTime);
        successImage.color = sColor;

        var fColor = failImage.color;
        fColor.a = Mathf.Lerp(fColor.a, 0f, fadingSpeed * Time.deltaTime);
        failImage.color = fColor;
    }

    public void ShowInfo(string name, string desc) 
    {
        timer = 0f;

        infoNameText.text = name;
        infoDescText.text = desc;
    }

    public void ShowResult(bool isSuccess)
    {
        Color color;

        if (isSuccess)
        {
            color = successImage.color;
            color.a = 1f;
            successImage.color = color;

            successes++;
            successCounter.text = successes.ToString();
        }
        else 
        {
            color = failImage.color;
            color.a = 1f;
            failImage.color = color;

            fails++;
            failCounter.text = fails.ToString();
        }
    }
}
