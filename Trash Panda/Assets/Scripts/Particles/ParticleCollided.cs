using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollided : MonoBehaviour {

	public ParticleSystem particleSystem;
	public ParticleSystem emit;

	void OnParticleCollision(GameObject other) {
		emit.Emit(200);
    }
}
