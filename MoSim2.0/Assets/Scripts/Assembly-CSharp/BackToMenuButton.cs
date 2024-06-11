using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuButton : MonoBehaviour
{
	private Controls controls;

	private void Start()
	{
		controls = new Controls();
		controls.RobotControls.Enable();
	}

	private void Update()
	{
		if (controls.RobotControls.Menu.IsPressed() || Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("MainMenu");
		}
		else if ((controls.RobotControls.RestartGame.IsPressed() || Input.GetKeyDown(KeyCode.R)) && SceneManager.GetActiveScene().name == "MatchScore")
		{
			SceneManager.LoadScene("Crescendo");
		}
	}
}
