using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class Game_Slides : MonoBehaviour
{
	GameManager GM;
	public string m_GameScene;
	public string m_TutorialScene;


	public GameObject m_TutorialSlidesCanvas;
	public GameObject m_GameSlidesCanvas;
	public GameObject m_GameSlide_1;
	public GameObject m_GameSlide_2;
	public GameObject m_GameSlide_3;


	public TextMeshProUGUI m_PressAnyButton;
	public TextMeshProUGUI m_PressStartButton;
	public TextMeshProUGUI m_SkipSlide_1_Button;
	public TextMeshProUGUI m_SkipSlide_2_Button;

	[Header("FMOD Events")]
	public string hoverSoundEvent;
	public string clickSoundEvent;

	void Awake()
	{
		GM = GameManager.Instance;
		GM.OnStateChange += HandleOnStateChange;
	}

	private void OnDestroy()
	{
		GM.OnStateChange -= HandleOnStateChange;
	}

	void Start()
	{
		m_TutorialSlidesCanvas.SetActive(false);
		m_GameSlidesCanvas.SetActive(false);
		m_GameSlide_1.SetActive(true);
		m_GameSlide_2.SetActive(false);
		m_GameSlide_3.SetActive(false);

		Cursor.lockState = CursorLockMode.None;


		//DEBUG GAME STATE - BORRAR AL TERMINAR!
		//GM.SetGameState(GameState.GAME);
		Debug.Log($"Current GAMESTATE en GAME_SLIDES: {GM.gameState}");

		switch (GM.gameState)
        {
			case GameState.GAME:
				m_GameSlidesCanvas.SetActive(true);
				break;

			case GameState.TUTORIAL:
				m_TutorialSlidesCanvas.SetActive(true);
				break;
		}

		StartBlinking(m_PressAnyButton);
		StartBlinking(m_PressStartButton);
		StartBlinking(m_SkipSlide_1_Button);
		StartBlinking(m_SkipSlide_2_Button);
		
	}

	private void StartBlinking(TextMeshProUGUI button)
	{
		StopCoroutine("FadeTo");
		switch (Mathf.RoundToInt(button.color.a).ToString())
		{
			case "0":
				StartCoroutine(FadeTo(1.0f, 1.5f, button));
				break;
			case "1":
				StartCoroutine(FadeTo(0.0f, 1.1f, button));
				break;
		}
	}

	IEnumerator FadeTo(float aValue, float aTime, TextMeshProUGUI button)
	{
		float alpha = button.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			float newAlpha = Mathf.Lerp(alpha, aValue, t);
			Color newColor = new Color(1, 1, 1, newAlpha);
			button.color = newColor;
			yield return null;
		}
		StartBlinking(button);
	}


    private void Update()
    {
        if (Input.anyKeyDown && m_TutorialSlidesCanvas.activeSelf)
        {
			switch (GM.gameState)
			{
				case GameState.TUTORIAL:
					//StartCoroutine(LoadAsyncOperation(m_TutorialScene));
					MouseClickSound();
					Invoke("LoadTutorial", 1f);
					break;

				case GameState.GAME:
					//StartCoroutine(LoadAsyncOperation(m_GameScene));
					MouseClickSound();
					Invoke("LoadGame", 1f);
					break;
			}
		}
    }

	public void StartGameButton()
    {
		switch (GM.gameState)
		{
			case GameState.TUTORIAL:
				//StartCoroutine(LoadAsyncOperation(m_TutorialScene));
				MouseClickSound();
				Invoke("LoadTutorial", 1f);
				break;

			case GameState.GAME:
				//StartCoroutine(LoadAsyncOperation(m_GameScene));
				MouseClickSound();
				Invoke("LoadGame", 1f);
				break;
		}
	}

	IEnumerator LoadAsyncOperation(string scene)
    {
		//Async scene load
		int y = SceneManager.GetActiveScene().buildIndex;
		SceneManager.UnloadSceneAsync(y);

		AsyncOperation level = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

		while (level.progress < 1)
		{
			yield return new WaitForEndOfFrame();
		}
		Debug.Log($"Loading Level { scene }");

		/*switch (GM.gameState)
		{
			case GameState.TUTORIAL:
				
				
				break;

			case GameState.GAME:
				AsyncOperation l_GameLevel = SceneManager.LoadSceneAsync(m_GameScene, LoadSceneMode.Additive);
                while (!l_GameLevel.isDone)
                {
                    yield return null;
                }
				Debug.Log($"Loading Level { m_GameScene }");
				break;
		}*/
	}


	public void HandleOnStateChange()
	{
		GM.OnStateChange -= HandleOnStateChange;
	}

	public void LoadGame()
	{
		GM.SetGameState(GameState.GAME);
		Initiate.Fade(m_GameScene, Color.black, 5f);
	}

	public void LoadTutorial()
	{
		GM.SetGameState(GameState.TUTORIAL);
		Initiate.Fade(m_TutorialScene, Color.black, 5f);
	}

	public void MouseHoverSound()
	{
		//Vector3 cuac = Camera.main.ScreenToWorldPoint(transform.position);
		SoundManager.Instance.PlaySound(hoverSoundEvent, transform.position);
	}

	public void MouseClickSound()
	{
		SoundManager.Instance.PlaySound(clickSoundEvent, transform.position);
	}
}