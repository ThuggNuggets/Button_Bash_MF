using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectCharacter : MonoBehaviour
{
	UnityEvent m_LockInCharacter;

	private void Awake()
	{
		m_LockInCharacter = new UnityEvent();

		m_LockInCharacter.AddListener(LockInCharacter);
	}

	public void LockInCharacter()
	{
		GameManager gm = GameManager.GetInstance();

		
	}
}