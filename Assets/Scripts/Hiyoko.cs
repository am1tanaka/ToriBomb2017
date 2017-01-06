using UnityEngine;
using System.Collections;

public class Hiyoko : MonoBehaviour {
	// アニメ
	private Animator anim;
	// リジッドボディ
	private Rigidbody2D rig;
	// 跳ねる速度
	public Vector3 boundVelocity = new Vector3(2f, 3f, 0f);

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rig = GetComponent<Rigidbody2D>();
		transform.parent = GameController.me.sceneObjects[(int)GameController.SCENES.SC_GAME].transform;

		// 左半分の時、フリップ
		if (transform.position.x < 0f) {
			GetComponent<SpriteRenderer>().flipX = true;
			boundVelocity.x = -boundVelocity.x;
		}
	}
	
	// Update is called once per frame
	void Update () {
		AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo(0);

		// 着地していたら、歩き続ける
		if (animInfo.IsName("Walk") && GameController.isGame()) {
			rig.velocity = boundVelocity;
		}
	}

	// 着地
	void OnCollisionEnter2D(Collision2D col) {
		AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo(0);

		// 誕生
		if (animInfo.IsName("Birth")) {
			// バウンド
			rig.velocity = boundVelocity;
			// アニメ設定
			anim.SetTrigger("grounded");
		}
		else if (animInfo.IsName("Bound")) {
			// バウンド
			boundVelocity.y = 0f;
			rig.velocity = boundVelocity;
			// アニメ設定
			anim.SetTrigger("grounded");
		}
	}

	/** カメラから消えることを判定*/
	void OnBecameInvisible() {
		Destroy(gameObject);
	}

	/** ゲームオーバーで停止*/
	void StopGame() {
		// 物理挙動を停止
		rig.isKinematic = true;
		rig.velocity = Vector3.zero;
		// アニメ停止
		anim.enabled = false;
		// 判定停止
		GetComponent<BoxCollider2D>().enabled = false;
	}
}
