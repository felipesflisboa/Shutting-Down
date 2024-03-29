﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
	[SerializeField] GameObject explosionPrefab;

	const float DURATION = 1.1f;
	
	IEnumerator Start () {
		yield return new WaitForSeconds(DURATION);
		CreateExplosion();
		yield return new WaitForSeconds(0.1f);
		Destroy(gameObject);
	}

	void CreateExplosion() {
		Instantiate(explosionPrefab, transform.position, transform.rotation);
	}
}
