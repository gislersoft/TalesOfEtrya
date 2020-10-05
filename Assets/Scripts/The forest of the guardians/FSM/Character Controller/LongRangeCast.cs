using System.Collections;
using UnityEngine;

public class LongRangeCast : MonoBehaviour {

    private ParticleSystem[] particles;
    public GameObject prefab;
    private GameObject[] particlesGameobj;
    public float duration = 5f;
    public int maxSize = 5;
    private int currentParticle = 0;
    public bool autoDespawn = false;

    private void Start()
    {
        particlesGameobj = new GameObject[maxSize];
        particles = new ParticleSystem[maxSize];

        for (int i = 0; i < maxSize; i++)
        {
            particlesGameobj[i] = Instantiate(prefab, Vector3.one * 100, Quaternion.identity);
            particlesGameobj[i].SetActive(false);
            particles[i] = particlesGameobj[i].GetComponent<ParticleSystem>();
        }

        currentParticle = 0;
    }

    public void Spawn(Vector3 position)
    {
        particlesGameobj[currentParticle].transform.position = position;
        particlesGameobj[currentParticle].SetActive(true);

        particles[currentParticle].Play(true);
        int indexCopy = currentParticle;
        currentParticle = (currentParticle + 1) % maxSize;

        if (autoDespawn)
        {
            StartCoroutine(Despawn(indexCopy));
        }

    }

    private IEnumerator Despawn(int particleIndex)
    {
        yield return new WaitForSeconds(duration);

        particles[particleIndex].Stop(true);
        particlesGameobj[particleIndex].SetActive(false);
    }
}
