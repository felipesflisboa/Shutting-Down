﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		Player player = other.GetComponentInParent<Player>();
		if (player != null)
			GameManager.I.LoadNextStage();
	}
}
