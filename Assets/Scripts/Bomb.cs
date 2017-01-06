using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	/** 爆弾の数*/
	private static int iCount = 0;
	/** 誘爆カウンタ*/
	private int iYubaku = 1;

	/** 現在の爆弾の数を孵す*/
	public static int getCount() {
		return iCount;
	}

	/** タグを変更する*/
	void changeExplosionTag() {
		gameObject.tag = "Exp";
		gameObject.layer = LayerMask.NameToLayer("Exp");
	}

	/** 自分を消す*/
	void destroyMe() {
		Destroy(gameObject);
	}

	void OnDestroy()  {
		iCount--;
	}

	// Use this for initialization
	void Start () {
		iCount++;
		iYubaku = 1;
	}

	/** ゲーム時の挙動を停止する*/
	void StopGame() {
		GetComponent<CircleCollider2D>().enabled = false;
		GetComponent<Animator>().enabled = false;
	}

	/** 誘爆判定*/
	void OnTriggerEnter2D(Collider2D col) {
		// 自分が爆弾の時のみ
		if (!CompareTag("Bomb")) {
			return ;
		}

		// 相手が爆発
		if (col.CompareTag("Exp")) {
			GetComponent<Animator>().SetTime(89f/60f);
			// カウンタを増やす
			iYubaku = col.GetComponent<Bomb>().getYubaku()+1;
		}
	}

	/** 誘爆数を増やす*/
	public int getYubaku() {
		return iYubaku;
	}
}
