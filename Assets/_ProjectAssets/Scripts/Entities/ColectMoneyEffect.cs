using System.Collections;
#if !UNITY_EDITOR
using Febucci.UI;
#endif
using TMPro;
using UnityEngine;


public class ColectMoneyEffect : MonoBehaviour
{
    [ColorUsage(true,true)]
    public Color[] textColors;
    public string[] textOptions;
#if !UNITY_EDITOR
    private TypewriterByWord _typewriterByWord;
#endif
    private TextMeshPro _tmp;
    
    
    private void Awake()
    {
        _tmp = GetComponent<TextMeshPro>();
#if !UNITY_EDITOR
        _typewriterByWord = GetComponent<TypewriterByWord>();
#endif
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
#if !UNITY_EDITOR
        _typewriterByWord.StartDisappearingText();
#endif
    }

    public void Destroy()
    {
     Destroy(gameObject,0.3f);       
    }
}
