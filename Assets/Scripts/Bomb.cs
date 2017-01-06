using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	/** タグを変更する*/
	void changeExplosionTag() {
		tag = "Exp";
	}

	/** 自分を消す*/
	void destroyMe() {
		Destroy(gameObject);
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
