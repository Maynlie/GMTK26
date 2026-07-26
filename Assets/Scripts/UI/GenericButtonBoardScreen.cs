
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenericButtonBoardScreen : GenericBoardScreen
{
	[SerializeField] protected Button m_button;
	[SerializeField] protected TextMeshProUGUI m_text;

	[SerializeField] protected AudioSource m_audioSource;
	[SerializeField] protected AudioClip m_hoverSound;
    [SerializeField] protected AudioClip m_clickSound;

	public void HoverSound()
	{
		m_audioSource.PlayOneShot(m_hoverSound);
    }

	public void ClickSound() 
	{
        m_audioSource.PlayOneShot(m_clickSound);
    }
}
