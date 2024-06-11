using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public TMP_Dropdown graphicsDropdown;

	public TMP_Dropdown gamemodeDropdown;

	public TMP_Dropdown cameraDropdown;

	public GameObject allianceToggle;

	public GameObject blueRobotSelector;

	public GameObject redRobotSelector;

	public Slider movespeed;

	public Slider rotatespeed;

	private bool toggled;

	public Toggle matchVideo;

	private void Start()
	{
		matchVideo.isOn = PlayerPrefs.GetFloat("endVideo") == 1f;
		movespeed.value = PlayerPrefs.GetFloat("movespeed");
		rotatespeed.value = PlayerPrefs.GetFloat("rotatespeed");
		graphicsDropdown.value = PlayerPrefs.GetInt("quality");
		gamemodeDropdown.value = PlayerPrefs.GetInt("gamemode");
		cameraDropdown.value = PlayerPrefs.GetInt("cameraMode");
		QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
		if (PlayerPrefs.GetInt("gamemode") == 0)
		{
			redRobotSelector.SetActive(PlayerPrefs.GetString("alliance") == "red");
			blueRobotSelector.SetActive(PlayerPrefs.GetString("alliance") == "blue");
			allianceToggle.SetActive(value: true);
		}
		else
		{
			redRobotSelector.SetActive(value: true);
			blueRobotSelector.SetActive(value: true);
			allianceToggle.SetActive(value: false);
		}
	}

	public void PlayGame()
	{
		SceneManager.LoadScene("Crescendo");
	}

	public void SaveMoveSpeed()
	{
		PlayerPrefs.SetFloat("movespeed", movespeed.value);
	}

	public void SaveRotateSpeed()
	{
		PlayerPrefs.SetFloat("rotatespeed", rotatespeed.value);
	}

	public void ToggleEndVideo(bool value)
	{
		if (value)
		{
			PlayerPrefs.SetFloat("endVideo", 1f);
		}
		else
		{
			PlayerPrefs.SetFloat("endVideo", 0f);
		}
	}

	public void SetCamera()
	{
		PlayerPrefs.SetInt("cameraMode", cameraDropdown.value);
	}

	public void SetGamemode()
	{
		PlayerPrefs.SetInt("gamemode", gamemodeDropdown.value);
		if (PlayerPrefs.GetInt("gamemode") == 0)
		{
			redRobotSelector.SetActive(PlayerPrefs.GetString("alliance") == "red");
			blueRobotSelector.SetActive(PlayerPrefs.GetString("alliance") == "blue");
			allianceToggle.SetActive(value: true);
		}
		else
		{
			redRobotSelector.SetActive(value: true);
			blueRobotSelector.SetActive(value: true);
			allianceToggle.SetActive(value: false);
		}
	}

	public void ToggleSingleplayerAlliance()
	{
		if (toggled)
		{
			toggled = false;
			PlayerPrefs.SetString("alliance", "blue");
			redRobotSelector.SetActive(value: false);
			blueRobotSelector.SetActive(value: true);
		}
		else
		{
			toggled = true;
			PlayerPrefs.SetString("alliance", "red");
			blueRobotSelector.SetActive(value: false);
			redRobotSelector.SetActive(value: true);
		}
	}

	public void SetQuality(int index)
	{
		QualitySettings.SetQualityLevel(index);
		PlayerPrefs.SetInt("quality", graphicsDropdown.value);
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Debug.Log("Toggled Fullscreen to " + isFullscreen);
		Screen.fullScreen = isFullscreen;
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
