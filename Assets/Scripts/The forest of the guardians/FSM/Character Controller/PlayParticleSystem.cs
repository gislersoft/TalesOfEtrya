using UnityEngine;

public class PlayParticleSystem : MonoBehaviour {
    public ParticleSystem particles;
    public string particleName;


    public void PlayParticles()
    {
        particles.Play(true);
    }
}
