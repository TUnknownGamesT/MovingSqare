using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectManager : MonoBehaviour
{  
    private static Volume _globalVolume;
    private static Vignette _vignette;

    // Start is called before the first frame update
    void Start()
    {
        _globalVolume = GetComponent<Volume>();
        _globalVolume.profile.TryGet(out _vignette);
    }
     
    public static void DamageEffect()
    {
        LeanTween.value(0, 1, 0.1f).setOnUpdate(value=>
        {
            _vignette.intensity.value = value;
        }).setEaseInElastic().setOnComplete(() =>
        {
            LeanTween.value(1, 0, 0.1f).setOnUpdate(value =>
            {
                _vignette.intensity.value = value;
            }).setEaseOutElastic();
        });
    }
}
