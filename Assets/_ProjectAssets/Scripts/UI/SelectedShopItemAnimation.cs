using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedShopItemAnimation : MonoBehaviour
{
    private bool continueAnimation=true;
    private bool isSmall;
    private GameObject mainTexture;
    private void Awake() {
        mainTexture = gameObject.transform.Find("MainTexture").gameObject;
        if(!mainTexture){
            Debug.LogError("textureNotFound");
        }
        MakeBigger();
    }

    private void MakeBigger(){
        isSmall= false;
        LeanTween.scale(gameObject, new Vector3(1.1f,1.1f,1.1f), 1f)
            .setEase(LeanTweenType.easeOutQuad) // You can choose different ease types
            .setOnComplete(ContinueAnimation);
    }
    private void MakeSmaller(){
        isSmall=true;
        LeanTween.scale(gameObject, new Vector3(0.9f,0.9f,0.9f), 1f)
            .setEase(LeanTweenType.easeOutQuad) // You can choose different ease types
            .setOnComplete(ContinueAnimation);
    }

    private void ContinueAnimation(){
        if(continueAnimation){
            if(isSmall){
                MakeBigger();
            }else{
                MakeSmaller();
            }
        }else{
            LeanTween.scale(gameObject, new Vector3(1f,1f,1), 0.5f)
            .setEase(LeanTweenType.easeOutQuad);
            Destroy(this);
        }
    }
    public void StopAnimation(){
        continueAnimation=false;
    }
}
