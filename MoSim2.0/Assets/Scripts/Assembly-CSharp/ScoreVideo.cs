using UnityEngine;
using UnityEngine.Video;

public class ScoreVideo : MonoBehaviour
{
	public VideoClip blueWins;

	public VideoClip redWins;

	public VideoClip tie;

	public VideoPlayer player;

	public GameObject screen;

	public GameObject button;

	private bool videoEnded;

	public void OnEnable()
	{
		screen.SetActive(value: true);
		if (Score.redScore > Score.blueScore)
		{
			player.clip = redWins;
		}
		else if (Score.blueScore > Score.redScore)
		{
			player.clip = blueWins;
		}
		else
		{
			player.clip = tie;
		}
		player.loopPointReached += EndOfVideo;
		player.Play();
		GameTimer.canRobotMove = false;
	}

	private void EndOfVideo(VideoPlayer vp)
	{
		videoEnded = true;
	}

	private void Update()
	{
		if (videoEnded && !player.isPlaying)
		{
			GameTimer.canRobotMove = true;
			screen.SetActive(value: false);
			button.SetActive(value: true);
			videoEnded = false;
		}
	}

	private void OnDisable()
	{
		player.loopPointReached -= EndOfVideo;
	}
}
