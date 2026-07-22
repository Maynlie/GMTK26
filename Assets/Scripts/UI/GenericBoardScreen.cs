using UnityEngine;
using UnityEngine.UI;

public class GenericBoardScreen : Screen
{
    [SerializeField] protected RectTransform m_borderRectTransform;
	[SerializeField] protected RectTransform[] m_horizontalSideRectTransform;
	[SerializeField] protected RectTransform[] m_verticalSideRectTransform;

	[SerializeField] protected int m_sideHeight;

	private void Start()
	{
		ScaleSide();
	}

	private void ScaleSide()
    {
        for(int i = 0; i < m_horizontalSideRectTransform.Length; i++)
        {
			m_horizontalSideRectTransform[i].sizeDelta = new Vector2(m_borderRectTransform.sizeDelta.x, m_sideHeight);
		}

		for (int i = 0; i < m_verticalSideRectTransform.Length; i++)
		{
			m_verticalSideRectTransform[i].sizeDelta = new Vector2(m_borderRectTransform.sizeDelta.y, m_sideHeight);
		}
	}
}
	