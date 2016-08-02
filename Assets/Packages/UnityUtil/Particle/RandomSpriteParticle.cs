using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ParticleSystemRenderer))]
public class RandomSpriteParticle : MonoBehaviour {

    public Texture[] sprites;
    Material material;

	void Start () {

        material = GetComponent<ParticleSystemRenderer>().material;
        if (!sprites.IsEmpty())
        {
            material.mainTexture = Util.RandomSelect(sprites);
        }
	
	}

    void OnDestroy()
    {
        DestroyImmediate(material);
    }
}
