﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameCntrl : MonoBehaviour {

	public GameObject pLose;
	public GameObject colBlock;
	public Vector3 [] positions;
	private GameObject block;
	private GameObject [] blocks = new GameObject[6];

	private int rand, count;
	private float rCol, gCol, bCol;
	public Text score;
	private static Color aColor;

	[HideInInspector]
	public bool next, lose;

	void Start () 
	{
		count = 0; //количество правильных ответов пользователя
		next = false;
		lose = false;
		rand = Random.Range (0, positions.Length);
		for (int i = 0; i < positions.Length; i++) {
			blocks [i] = Instantiate (colBlock, positions[i], Quaternion.identity) as GameObject;
			if (rand == i)
				block = blocks [i];
		}
		block.GetComponent <RandCol> ().right = true;
	}

	void Update () {
		if (lose)
			playerLose ();
		if (next && !lose) //проверка на правильный ответ и переход к генерации следующего цвета
			nextColors ();
	}

	void nextColors () {
		count++;
		score.text = count.ToString ();
		aColor = new Vector4 (Random.Range (0.1f, 1f), Random.Range (0.1f, 1f), Random.Range (0.1f, 1f), 1);
		GetComponent <Renderer> ().material.color = aColor;
		next = false;

		if (count < 3) {
			rCol = 0.4f;
			gCol = 0.4f;
			bCol = 0.4f;
		} else if (count >= 3 && count < 5) {
			rCol = 0.3f;
			gCol = 0.3f;
			bCol = 0.3f;
		} else if (count >= 5) {
			rCol = 0.1f;
			gCol = 0.1f;
			bCol = 0.1f;
		}

		// новые цвета для блоков
		rand = Random.Range (0, positions.Length);
		for (int i = 0; i < positions.Length; i++) {
			if (i == rand)
				blocks [i].GetComponent <Renderer> ().material.color = aColor;
			else {
				float r = aColor.r + Random.Range (0.1f, rCol) > 1f ? 1f : aColor.r + Random.Range (0.1f, rCol);
				float g = aColor.g + Random.Range (0.1f, gCol) > 1f ? 1f : aColor.g + Random.Range (0.1f, gCol);
				float b = aColor.b + Random.Range (0.1f, bCol) > 1f ? 1f : aColor.b + Random.Range (0.1f, bCol);
				blocks [i].GetComponent <Renderer> ().material.color = new Vector4 (r, g, b, aColor.a);
			}
		}
	}

	void playerLose () {
		if(PlayerPrefs.GetInt ("score") < count );
		PlayerPrefs.SetInt("score", count);
		pLose.SetActive (true);
	}
}