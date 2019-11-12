using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WaveDisplay : MonoBehaviour
{
	/// <summary>
	/// The video clips to play at the start of a new wave.
	/// </summary>
	public VideoClip[] m_WaveVideoClips;

	/// <summary>
	/// The wave manager.
	/// </summary>
	public WaveManager m_WaveManager;

	/// <summary>
	/// The current wave.
	/// </summary>
	private int m_CurrentWave = 0;

	/// <summary>
	/// The video player.
	/// </summary>
	private VideoPlayer m_VideoPlayer;

	/// <summary>
	/// On startup.
	/// </summary>
	private void Awake()
	{
		m_VideoPlayer = GetComponent<VideoPlayer>();

		// Play the first video on startup.
		PlayVideo();
	}

	/// <summary>
	/// Update.
	/// </summary>
	void Update()
    {
		int waveManWave = m_WaveManager.GetCurrentWave();

		if (m_CurrentWave != waveManWave)
		{
			m_CurrentWave = waveManWave;

			PlayVideo();
		}
    }

	/// <summary>
	/// Play the current video for the current wave.
	/// </summary>
	private void PlayVideo()
	{
		m_VideoPlayer.clip = m_WaveVideoClips[m_CurrentWave];
	}
}