using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticleSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn(Color color, Vector3 pos)
    {
        if (!GetComponent<ParticleSystem>())
        {
            GameObject ps = Instantiate(Resources.Load<ParticleSystem>("Prefabs/ParticleSystems/CelebrationPS").gameObject, pos, Quaternion.identity);
            ps.transform.parent = transform;
            ps.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            ParticleSystem celebrationParticles = ps.GetComponent<ParticleSystem>();
            celebrationParticles.startColor = color;
            celebrationParticles.Play();
        }
    }
}
