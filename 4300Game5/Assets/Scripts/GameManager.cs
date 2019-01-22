using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	private PlayerController player;
	[SerializeField] private GameObject deathPanel = null;
	[SerializeField] private GameObject deathPanelButton = null;
	[SerializeField] private GameObject endPanel = null;
	[SerializeField] private GameObject endPanelButton = null;
	[SerializeField] private GameObject darkPanel = null;
	[SerializeField] private GameObject postProcessVolumeDof = null;
	[SerializeField] private float timeVisualEffectOnOff = 2.0f;

	public PlayerController Player
	{
		get => player;
		set => player = value;
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoadingScene;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoadingScene;
	}

	//this function is activated every time a scene is loaded
	private void OnLevelFinishedLoadingScene(Scene scene, LoadSceneMode mode)
	{
		Setup();
		Debug.Log("Scene Loaded");
	}

	private void Setup()
	{
		player = FindObjectOfType<PlayerController>();
		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	private void Awake()
	{
        if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			//because we are doing everything on a single scene
			//todo find a way to referentiate gamemanager in canvas without losing link
			//DontDestroyOnLoad(gameObject);
		}

		Setup();
	}

	public void StartGame()
	{
		StartCoroutine(DecreaseWeightPPV(postProcessVolumeDof.GetComponent<PostProcessVolume>(),0.0f,timeVisualEffectOnOff));
		player.CanMove = true;
	}

	public void Death()
	{
		player.Die();
		deathPanel.SetActive(true);
		EventSystem.current.SetSelectedGameObject(deathPanelButton);
	}

	public void EndGame()
	{
		StartCoroutine(player.StopMoving());
		StartCoroutine(FadeToBlack(timeVisualEffectOnOff));
		endPanel.SetActive(true);
		EventSystem.current.SetSelectedGameObject(endPanelButton);
	}

	public IEnumerator FadeToBlack(float time)
	{
		float timer = 0.0f;
		Image darkImage = darkPanel.GetComponent<Image>();
		while (timer < time)
		{
			darkImage.color = new Color(0.0f, 0.0f, 0.0f, Mathf.Lerp(0.0f, 1.0f, timer / time));
			timer += Time.deltaTime;
			yield return null;
		}
	}

	private IEnumerator DecreaseWeightPPV(PostProcessVolume volume, float value, float time)
	{
		float timer = 0.0f;
		float initWeight = volume.weight;
		while (timer < time)
		{
			volume.weight = Mathf.Lerp(initWeight, value, timer/time);
			timer += Time.deltaTime;
			yield return null;
		}
	}

	public void LoadLevel(string nameLevel)
	{
		SceneManager.LoadScene(nameLevel);
	}

	public void QuitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}