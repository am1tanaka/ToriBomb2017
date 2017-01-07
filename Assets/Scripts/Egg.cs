using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour {
	/** アニメーション*/
	private Animator anim;
	/** ひよこプレハブ*/
	public GameObject prefHiyoko;
	/** 点数*/
	public int POINT = 10;
	/** 得点プレハブ*/
	public GameObject prefPoint;
	/** リジッドボディ*/
	private Rigidbody2D rig;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rig = GetComponent<Rigidbody2D>();
	}

	// ゲームを停止する
	void StopGame() {
		GetComponent<CircleCollider2D>().enabled = false;
		rig.constraints = RigidbodyConstraints2D.FreezeAll;
		rig.velocity = Vector3.zero;
		rig.rotation = 0f;
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
			int basepoint = (int)(POINT*(1f-rig.velocity.y));
			int yubaku = col.GetComponent<Bomb>().getYubaku();
			int point = basepoint*yubaku;
			GameController.addScore(point);
			col.gameObject.SendMessage("addYubaku");
			// スコア表示
			GameObject go = Instantiate(prefPoint, transform.position, Quaternion.identity) as GameObject;
			go.GetComponent<Point>().setPoint(basepoint, yubaku);
			// 卵を消す
			Destroy(gameObject);
		}
	}
}
