using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour {
	/** アニメーション*/
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	// ゲームを停止する
	void StopGame() {
		GetComponent<Rigidbody2D>().isKinematic = true;
		GetComponent<CircleCollider2D>().enabled = false;
	}

	/** 接触チェック*/
	void OnTriggerEnter2D(Collider2D col) {
		// 床とぶつかった
		if (col.gameObject.CompareTag("Floor")) {
			anim.SetBool("miss", true);
			GameController.gameOver();
		}
	}
}
