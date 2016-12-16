using UnityEngine;
using System.Collections;

public class CGrid : MonoBehaviour {
	// X方向グリッド線
	public GameObject prefGridLineX;
	// Y方向グリッド線
	public GameObject prefGridLineY;
	// グリッド幅
	public float gridSpan = 2f;

	// Use this for initialization
	void Start () {
		float w = Camera.main.orthographicSize * Camera.main.aspect;
		// Y方向の線を描画
		for (float x = -Mathf.Floor(w); x <= w; x += gridSpan) {
			GameObject go = Instantiate (prefGridLineY, new Vector3(x, 0f, 0f), Quaternion.identity) as GameObject;
			go.transform.parent = transform;
		}
		// X方向の線を描画
		for (float y = -Mathf.Floor(Camera.main.orthographicSize); y <= Camera.main.orthographicSize; y += gridSpan) {
			GameObject go = Instantiate (prefGridLineX,
				new Vector3 (0f, y, 0f), Quaternion.identity) as GameObject;
			go.transform.parent = transform ;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
