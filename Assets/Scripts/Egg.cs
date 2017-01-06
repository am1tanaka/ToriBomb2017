using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour {
	/** アニメーション*/
	private Animator anim;
	/** ひよこプレハブ*/
	public GameObject prefHiyoko;
	/** 点数*/
	public int POINT = 100;

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
		// 爆発とぶつかった
		else if (col.gameObject.CompareTag("Exp")) {
			// ひよこを孵す
			Instantiate(prefHiyoko, transform.position, Quaternion.identity);
			// スコア
			GameController.addScore(POINT);
			// 卵を消す
			Destroy(gameObject);
		}
	}
}
