using UnityEngine;
using System.Collections;
using TMPro;

public class Game_Slides : MonoBehaviour
{
	GameManager GM;
	public string m_GameScene;
	public string m_TutorialScene;


	public GameObject m_TutorialSlidesCanvas;
	public GameObject m_GameSlidesCanvas;


	public TextMeshProUGUI m_PressAnyButton;

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

		StartBlinking();

	}

	IEnumerator FadeTo(float aValue, float aTime)
	{
		float alpha = m_PressAnyButton.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			float newAlpha = Mathf.Lerp(alpha, aValue, t);
			Color newColor = new Color(1, 1, 1, newAlpha);
			m_PressAnyButton.color = newColor;
			yield return null;
		}
		StartBlinking();
	}

	private void StartBlinking()
    {
		StopCoroutine("FadeTo");
		switch (Mathf.RoundToInt( m_PressAnyButton.color.a ).ToString())
		{
			case "0":
				StartCoroutine(FadeTo(1.0f, 1.5f));
				break;
			case "1":
				StartCoroutine(FadeTo(0.0f, 1.1f));
				break;

		}	
	}

    private void Update()
    {
        if (Input.anyKeyDown)
        {
			switch (GM.gameState)
			{
				case GameState.TUTORIAL:
					Invoke("LoadTutorial", 1f);
					break;

				case GameState.GAME:
					Invoke("LoadGame", 1f);
					break;
			}
		}
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
}