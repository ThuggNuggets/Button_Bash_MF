using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WaveDisplay : MonoBehaviour
{
	public VideoClip[] m_WaveVideoClips;

	public WaveManager m_WaveManager;

	private int m_CurrentWave = 0;

	private VideoPlayer m_VideoPlayer;

	private void Awake()
	{
		m_VideoPlayer = GetComponent<VideoPlayer>();

		PlayVideo();
	}

	// Update is called once per frame
	void Update()
    {
		int waveManWave = m_WaveManager.GetCurrentWave();

		if (m_CurrentWave != waveManWave)
		{
			m_CurrentWave = waveManWave;

			PlayVideo();
		}
    }

	private void PlayVideo()
	{
		m_VideoPlayer.clip = m_WaveVideoClips[m_CurrentWave];
	}
}