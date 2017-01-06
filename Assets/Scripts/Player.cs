using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	/** 爆弾プレハブ*/
	public GameObject prefBomb;
	/** 爆弾数*/
	public int BOMB_MAX = 4;

	// Update is called once per frame
	void Update () {
		if (GameController.me.nowScene != GameController.SCENES.SC_GAME) {
			return ;
		}
		// クリックを判定
		if (Input.GetMouseButtonDown(0) && (Bomb.getCount() < BOMB_MAX)) {
			// クリック場所をチェック
			Vector3 mspos = Input.mousePosition;
			mspos.z = -Camera.main.transform.position.z;
			Vector3 pos = Camera.main.ScreenToWorldPoint(mspos);

			// 爆弾を出現
			GameObject go = Instantiate(prefBomb, pos, Quaternion.identity) as GameObject;
			go.transform.parent = GameController.me.sceneObjects[(int)GameController.SCENES.SC_GAME].transform;
		}
	}
}
