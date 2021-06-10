using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
	GameManager GM;

	public string m_SlidesScene;

	public Animator m_MaskAnimator;

	public GameObject m_MainMenu;
	public GameObject m_HuntChoice;

	[Header("FMOD Events")]
	public string hoverSoundEvent;
	public string clickSoundEvent;
	public string startSoundEvent;
	public string exitSoundEvent;

	void Awake()
	{
		GM = GameManager.Instance;
		GM.OnStateChange += HandleOnStateChange;

		GM.m_Player = null;
		GM.m_Enemy = null;
		GM.m_GameObjectSpawner = null;
		GM.m_WaypointsList = null;


		//FOR DEBUG - BORRAR LUEGO
		//GM.SetGameState(GameState.MAIN_MENU);

	}

	private void OnDestroy()
	{
		GM.OnStateChange -= HandleOnStateChange;
	}

    public void HandleOnStateChange()
	{
		//Debug.Log("OnStateChange! Unfollowing event.");
		//Apply this when we have a real scene change:
		GM.OnStateChange -= HandleOnStateChange;
	}


	public void StartGame()
	{
		m_MaskAnimator.SetTrigger("Hunt Button Press");
		SoundManager.Instance.PlaySound(startSoundEvent, transform.position);
		Invoke("ShowTutorial", 4f);
	}

	private void ShowTutorial()
    {
		m_MainMenu.SetActive(false);
		m_HuntChoice.SetActive(true);
	}

	public void PlayGame()
    {
		
		//Debug.Log($"Veces jugadas: {GM.m_GamesPlayed}, cargando juego por defecto");
		//Debug.Log($"Current GAMESTATE: {GM.gameState}");
		GM.SetGameState(GameState.GAME);
		//Debug.Log($"Current GAMESTATE: {GM.gameState}");
		Initiate.Fade(m_SlidesScene, Color.black, 3f);
	}

	public void PlayTutorial()
	{
		//Debug.Log($"Veces jugadas: {GM.m_GamesPlayed}, cargando juego por defecto");
		//Debug.Log($"Current GAMESTATE: {GM.gameState}");
		GM.SetGameState(GameState.TUTORIAL);
		//Debug.Log($"Current GAMESTATE: {GM.gameState}");
		Initiate.Fade(m_SlidesScene, Color.black, 3f);
	}



	public void Quit()
	{
		Debug.Log("Cerrando el juego");
		SoundManager.Instance.PlaySound(exitSoundEvent, transform.position);
		Application.Quit();
	}

    public void MouseHoverHunt()
    {
		m_MaskAnimator.SetBool("On Hunt Button", true);
		SoundManager.Instance.PlaySound(hoverSoundEvent, transform.position);
		//Debug.Log("Raton encima");
    }

    public void MouseExitHunt()
    {
		m_MaskAnimator.SetBool("On Hunt Button", false);
		//Debug.Log("Raton fuera");
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
	public void ExitSound()
	{
		SoundManager.Instance.PlaySound(exitSoundEvent, transform.position);
	}

}