using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDrop : MonoBehaviour
{
	public GameObject expDropPrefab;

    void Start() {
		StatsHandler stats = GetComponent<StatsHandler>();

		stats.OnDeath += DropExp;
	}

	void DropExp(BaseController controller) {
		GameObject exp = Instantiate(expDropPrefab, controller.transform.position, Quaternion.identity);
	}
}
