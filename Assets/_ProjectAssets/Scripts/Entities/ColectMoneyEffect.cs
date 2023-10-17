using System.Collections;
using Febucci.UI;
//using Febucci.UI;
using TMPro;
using UnityEngine;


public class ColectMoneyEffect : MonoBehaviour
{
    [ColorUsage(true,true)]
    public Color[] textColors;
    public string[] textOptions;
    
    private TypewriterByWord _typewriterByWord;
    private TextMeshPro _tmp;
    
    
    private void Awake()
    {
        _tmp = GetComponent<TextMeshPro>();
        _typewriterByWord = GetComponent<TypewriterByWord>();
    }

    
    private void Start()
    {
        _tmp.color = textColors[Random.Range(0, textColors.Length)];
        _tmp.text = textOptions[Random.Range(0, textOptions.Length)];
    }

    public void FadeOutText()
    {
        StartCoroutine(HideText());
    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(0.5f);
        _typewriterByWord.StartDisappearingText();
    }

    public void Destroy()
    {
     Destroy(gameObject,0.3f);       
    }
}
