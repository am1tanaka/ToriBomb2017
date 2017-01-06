using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	/** シングルトン*/
	public static GameController me;

	/** デバッグ*/
	public bool isDebug = true;

	/** シーンのトップオブジェクト*/
	public GameObject []sceneObjects;

	/** シーンテキスト*/
	public Text textScene;
	/** スコアテキスト*/
	public Text textScore;

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
				break;
			case SCENES.SC_GAME:
				textScene.text = "GAME";
				sceneObjects[(int)SCENES.SC_TITLE].SetActive(false);
				iScore = 0;
				break;
			case SCENES.SC_GAMEOVER:
				textScene.text = "GAMEOVER";
				sceneObjects[(int)SCENES.SC_GAMEOVER].SetActive(true);
				break;
			}
		}
	}

	/** 更新処理*/
	void updateScene() {
		/** スコア表示*/
		string sc = "00000"+iScore;
		textScore.text = "Score:"+sc.Substring(sc.Length-6,6);

		/** 切り替えチェック*/
		if (isDebug) {
			switch (nowScene) {
			case SCENES.SC_TITLE:
				if (Input.GetMouseButtonDown(0)) {
					nextScene = SCENES.SC_GAME;
				}
				break;
			case SCENES.SC_GAME:
				if (Input.GetMouseButtonDown(1)) {
					nextScene = SCENES.SC_GAMEOVER;
				}
				break;
			case SCENES.SC_GAMEOVER:
				if (Input.GetMouseButtonDown(0)) {
					nextScene = SCENES.SC_TITLE;
				}
				break;
			}
		}
	}
}
