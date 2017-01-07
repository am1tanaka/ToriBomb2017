using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	/** シングルトン*/
	public static GameController me;

	/** デバッグ*/
	public bool isDebug = false;

	/** 卵の出現範囲。xは幅。yは高さ*/
	public Vector3 eggRange = new Vector3(5,11,0);
	/** 最初の卵が出現するまでの秒数*/
	public float FIRST_OUT = 3f;
	/** 最初の出現率。何秒に1つ出現するか*/
	public float INIT_OUT_RATE = 3f; 
	/** 最高レベル*/
	public float MAX_OUT_RATE = 1f;
	/** 最高レベルに達するまでの秒数*/
	public float MAX_LEVEL_TIME = 60f;

	/** 経過秒数*/
	public float gameTime;

	/** シーンのトップオブジェクト*/
	public GameObject []sceneObjects;

	/** シーンテキスト*/
	public Text textScene;
	/** スコアテキスト*/
	public Text textScore;

	/** 卵プレハブ*/
	public GameObject prefEgg;

	/** スコアの上限*/
	private const int SCORE_MAX = 9999999;

	/** シーン定義*/
	public enum SCENES {
		SC_NONE=-1,
		SC_TITLE=0,
		SC_GAME,
		SC_GAMEOVER
	}
	/** 現在のシーン*/
	public SCENES nowScene = SCENES.SC_NONE;
	/** 次のシーン*/
	public SCENES nextScene = SCENES.SC_TITLE;

	/** スコア*/
	private int iScore;

	// Use this for initialization
	void Start () {
		me = this;
		textScene.enabled = isDebug;
		nowScene = SCENES.SC_NONE;
		nextScene = SCENES.SC_TITLE;
		iScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		/** シーンの切り替えを確認*/
		changeScene();

		/** 処理*/
		updateScene();
	}

	/** スコアの加算*/
	public static void addScore(int add) {
		me.iScore += add;
		if (me.iScore > SCORE_MAX) {
			me.iScore = SCORE_MAX;
		}
	}

	/** シーンの切り替え処理*/
	void changeScene() {
		if (nextScene != SCENES.SC_NONE) {
			nowScene = nextScene;
			nextScene = SCENES.SC_NONE;

			switch (nowScene) {
			case SCENES.SC_TITLE:
				textScene.text = "TITLE";
				sceneObjects[(int)SCENES.SC_TITLE].SetActive(true);
				sceneObjects[(int)SCENES.SC_GAMEOVER].SetActive(false);
				// ゲームオブジェクトを全て削除
				removeAllGameObjects();
				break;
			case SCENES.SC_GAME:
				textScene.text = "GAME";
				sceneObjects[(int)SCENES.SC_TITLE].SetActive(false);
				// ゲームパラメーターを初期化
				iScore = 0;
				gameTime = 0f;
				break;
			case SCENES.SC_GAMEOVER:
				textScene.text = "GAMEOVER";
				sceneObjects[(int)SCENES.SC_GAMEOVER].SetActive(true);

				// ゲームの挙動を停止
				sceneObjects[(int)SCENES.SC_GAME].BroadcastMessage("StopGame");

				break;
			}
		}
	}

	/** 更新処理*/
	void updateScene() {
		/** スコア表示*/
		string sc = "00000"+iScore;
		textScore.text = "Score:"+sc.Substring(sc.Length-6,6);

		/** シーン別処理*/
		switch (nowScene) {
		case SCENES.SC_TITLE:
		// タイトルの切り替え
			if (Input.GetMouseButtonDown(0)) {
				nextScene = SCENES.SC_GAME;
			}
			break;
		case SCENES.SC_GAME:
			updateGame();
			/** 切り替えチェック*/
			if (isDebug) {
								if (Input.GetMouseButtonDown(1)) {
					nextScene = SCENES.SC_GAMEOVER;
				}
			}

			break;
		case SCENES.SC_GAMEOVER:
			// シーンの切り替え
			if (Input.GetMouseButtonDown(0)) {
				nextScene = SCENES.SC_TITLE;
			}
			break;
		}
	}

	/** ゲームの更新処理*/
	void updateGame() {
		spawnEgg();
	}

	/** 全てのゲームオブジェクトを削除*/
	void removeAllGameObjects() {
		string [] tags = {"Egg", "Bomb", "Exp", "Hiyoko", "Point"};
		foreach (string tag in tags) {
			GameObject [] objects = GameObject.FindGameObjectsWithTag(tag);
			foreach(GameObject obj in objects) {
				Destroy(obj);
			}
		}
	}

	/** 卵を出現させる*/
	void spawnEgg() {
		// 出現率の向上
		gameTime += Time.deltaTime;
		float span = Mathf.Clamp(gameTime, 0f, MAX_LEVEL_TIME);
		float nowRate = Mathf.Lerp(INIT_OUT_RATE, MAX_OUT_RATE, span/MAX_LEVEL_TIME);

		// 1フレームの秒数: Time.deltaTime
		// 1秒あたりのフレーム数: 1f/Time.deltaTime
		float frame_sec = 1f/Time.deltaTime;
		// 出現率: 1/(nowRate*1秒あたりのフレーム数)
		float frame_num = nowRate*frame_sec;
		// 乱数
		float rand = Random.value*frame_num;
		// 上記が1以下の時、卵を出現
		if (rand <= 1f) {
			Vector3 pos = new Vector3(Random.Range(-eggRange.x, eggRange.x), eggRange.y, 0f);
			GameObject egg = Instantiate(prefEgg, pos, Quaternion.identity) as GameObject;
			egg.transform.parent = sceneObjects[(int)SCENES.SC_GAME].transform;
		}
	}

	/** ギズモの描画*/
	void OnDrawGizmos() {
		// 卵の出現範囲
		Gizmos.color = Color.red;
		Gizmos.DrawLine(
			new Vector3(-eggRange.x, eggRange.y, eggRange.z),
			new Vector3(eggRange.x, eggRange.y, eggRange.z)
		);
	}

	/** ゲームオーバーへ移行*/
	public static void gameOver() {
		me.nextScene = SCENES.SC_GAMEOVER;
	}

	/** ゲーム中か*/
	public static bool isGame() {
		return me.nowScene == SCENES.SC_GAME;
	}
}
