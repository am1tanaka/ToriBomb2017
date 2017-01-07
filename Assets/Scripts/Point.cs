using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Point : MonoBehaviour {
	/** 表示秒数*/
	public float lifeTime = 3f;
	/** 基礎点*/
	[SerializeField]
	private int basePoint;
	/** 倍率*/
	[SerializeField]
	private int bairitsu;
	/** 移動速度*/
	[SerializeField]
	private Vector3 moveUp = new Vector3(0f, 1f, 0f);
	/** 倍率スプライト*/
	public Sprite[] spBairitsu;
	/** 倍率スプライトレンダラ―*/
	private SpriteRenderer rendererBairitsu;
	/** 点数テキスト*/
	private Text textPoint;

	/** 得点と倍率を設定*/
	public void setPoint(int pnt, int ritsu) {
		basePoint = pnt;
		bairitsu = ritsu;
	}

	// Use this for initialization
	void Start () {
		rendererBairitsu = GetComponentInChildren<SpriteRenderer>();
		textPoint = GetComponentInChildren<Text>();
		Invoke("destroyMe", lifeTime);
	}

	void destroyMe() {
		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {
		// 基礎点
		textPoint.text = ""+basePoint;
		Vector3 next = transform.position+moveUp*Time.deltaTime;
		transform.position = next;

		// 倍率
		int idx = bairitsu-2;
		if ((idx < 0) || (idx>=spBairitsu.Length)) {
			rendererBairitsu.enabled=false;
		}
		else {
			rendererBairitsu.enabled=true;
			rendererBairitsu.sprite = spBairitsu[idx];
		}
	}
}
