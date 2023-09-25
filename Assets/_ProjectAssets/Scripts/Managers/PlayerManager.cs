using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public PlayerLife playerLife;
    public Movement movement;
    public CameraShaking cameraShaking;
    public ParticleSystem deathEffect;
    public ParticleSystem trail;
    
    private SpriteRenderer _spriteRenderer;
    


    private void OnEnable()
    {
        GameManager.onGameOver += PlayerDeath;
        AdsManager.onReviveADFinish += Revive;
    }
    

    private void OnDisable()
    {
        GameManager.onGameOver -= PlayerDeath;
        AdsManager.onReviveADFinish -= Revive;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        trail = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    public void InitPlayer(Item item)
    {
        GetComponent<SpriteRenderer>().sprite = item.sprite;
        trail.GetComponent<Renderer>().material.SetTexture("_BaseMap",item.trailTexture);
        
        ApplyEffect(item);
    }


    private void ApplyEffect( Item item)
    {
        
        switch (item.effects[0].effect)
        {
            case EffectType.Size:
            {
                transform.localScale -= CalculatePercentage(item.effects[0].value)*Vector3.one;
                float scale =  GetComponent<TrailRenderer>().widthMultiplier - 0.12f*PlayerPrefs.GetInt(item.effects[0].name);
                GetComponent<TrailRenderer>().widthMultiplier = scale;
                break;
            }
            case EffectType.Speed:
            {
                movement.speed += CalculateSpeed(movement.speed, item.effects[0].value);
                break;
            }
            case EffectType.Life:
            {
                playerLife.AddLife((int)item.effects[0].value);
                break;
            }
            default:{
                Debug.LogError("NoEffectApllied");
                break;
            }
        }
    }

    private void PlayerDeath()
    {
        deathEffect.Play();
        _spriteRenderer.enabled = false;
        trail.gameObject.SetActive(false);
    }
    
    private void Revive()
    {
        _spriteRenderer.enabled = true;
        trail.gameObject.SetActive(true);
        
        transform.position = Vector3.zero;
        playerLife.AddLife(1);
        GetComponent<BoxCollider2D>().enabled = false;
        LeanTween.value(1, 0.5f, 0.3f).setOnUpdate(value =>
        {
            Color c = GetComponent<SpriteRenderer>().color;
            c.a = value;
            GetComponent<SpriteRenderer>().color = c;
        }).setLoopCount(10).setEaseInOutCubic().setLoopPingPong()
            .setOnComplete(()=> GetComponent<BoxCollider2D>().enabled = true);
    }
    
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("i have got damaged by " + col.collider.gameObject.name);
            playerLife.Damage(1);
            SoundManager.instance.PlaySoundEffect(Constants.Sounds.PlayerGetHit);
            cameraShaking.Shake();
        }

        if (col.gameObject.CompareTag("PowerUp"))
        {
            col.gameObject.GetComponent<PowerUpBehaviour>().Effect();
        }

    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("i have got damaged by " + col.gameObject.name);
            SoundManager.instance.PlaySoundEffect(Constants.Sounds.PlayerGetHit);
            playerLife.Damage(1);
            cameraShaking.Shake();
        }
        
        if (col.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("i have got damaged by " + col.gameObject.name);
            playerLife.Damage(1);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("i have got damaged by " + other.name);
        SoundManager.instance.PlaySoundEffect(Constants.Sounds.PlayerGetHit);
        playerLife.Damage(1);
        cameraShaking.Shake();
    }

    private float CalculatePercentage(float effect)
    {
        float soum = transform.localScale.x * effect;
        return soum / 100;
    }

    private float CalculateSpeed(float defaultSpeed, float speedBonus)
    {
        return (defaultSpeed/100)*speedBonus;
    }

/*#if !UNITY_EDITOR
    private void OnBecameInvisible()
    {
        playerLife.Damage(playerLife.Life);
    }
    
#endif*/
}
