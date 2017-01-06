using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	/** 爆弾プレハブ*/
	public GameObject prefBomb;

	// Update is called once per frame
	void Update () {
		if (GameController.me.nowScene != GameController.SCENES.SC_GAME) {
			return ;
		}
		// クリックを判定
		if (Input.GetMouseButtonDown(0)) {
			// クリック場所をチェック
			Vector3 mspos = Input.mousePosition;
			mspos.z = -Camera.main.transform.position.z;
			Vector3 pos = Camera.main.ScreenToWorldPoint(mspos);
			Instantiate(prefBomb, pos, Quaternion.identity);
		}
	}
}
