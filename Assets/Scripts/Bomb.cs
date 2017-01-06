using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {
	/** 爆弾の数*/
	private static int iCount = 0;

	/** 現在の爆弾の数を孵す*/
	public static int getCount() {
		return iCount;
	}

	/** タグを変更する*/
	void changeExplosionTag() {
		tag = "Exp";
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
	}
}
