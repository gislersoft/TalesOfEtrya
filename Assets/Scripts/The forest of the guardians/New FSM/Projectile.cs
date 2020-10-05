using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Transform target;
    private AradisStats aradisStats;
    public float movementSpeed = 2f;
    private Vector3 direction;
    private ParticleSystem[] particleSystems;
    public ParticleSystem impact;
    public PlayParticleSystem impactParticles;
    private FX_Manager fxManager;
    
    private void Start()
    {
        target = TFOGPlayerManager.instance.player.transform;
        aradisStats = TFOGPlayerManager.instance.Aradis.GetComponent<AradisStats>();
        
        direction = target.position - transform.position;
        direction.z = direction.y = 0f;
        direction = direction.normalized * movementSpeed;

        particleSystems = GetComponentsInChildren<ParticleSystem>();
        fxManager = FX_Manager.Instance;

        var playComponents = TFOGPlayerManager.instance.player.GetComponents<PlayParticleSystem>();
        for (int i = 0; i < playComponents.Length; i++)
        {
            if(playComponents[i].particleName == StringsInGame.ImpactParticlesName)
            {
                impactParticles = playComponents[i];
                break;
            }
        }

        if(impactParticles == null)
        {
            impactParticles = target.gameObject.AddComponent<PlayParticleSystem>();           
            impactParticles.particles = Instantiate(impact, target, false) as ParticleSystem;
            impactParticles.particleName = StringsInGame.ImpactParticlesName;
        }
    }
    
    void Update () {
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, Time.deltaTime);
	}

    private void OnEnable()
    {
        if(target != null)
        {
            direction = target.position - transform.position;
            direction.z = direction.y = 0f;
            direction = direction.normalized * movementSpeed;
        }
    }

    private void Deactivate()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Stop();
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == StringsInGame.PlayerTag)
        {
            CharacterStats characterStats = collision.gameObject.GetComponent<CharacterStats>();

            if(characterStats != null && aradisStats != null)
            {
                characterStats.TakeDamage(aradisStats.castDamage.GetValue());
                impactParticles.PlayParticles();
            }
            Deactivate();
            if (fxManager != null)
            {
                fxManager.Shake();
            }
        }
        else if(collision.tag == StringsInGame.TerrainTag)
        {
            Deactivate();
        }        
    }

}
