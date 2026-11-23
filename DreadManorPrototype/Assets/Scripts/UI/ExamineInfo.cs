using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamineInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private RectTransform Content;
    [SerializeField] private RectTransform Viewport;
    [SerializeField] private Scrollbar Scrollbar;
    [SerializeField] private float scrollSpeed = 400f;
    [SerializeField] private float MaxScrollHeight = 5000f;

    private float _contentHeight;

    public void ShowInfo(IExaminable examinable)
    {
        Text.text = examinable.GetInfo();
        Content.anchoredPosition = Vector2.zero;

        _contentHeight = Text.GetPreferredValues(Text.text, Text.rectTransform.rect.width, Mathf.Infinity).y;
        float viewportHeight = Viewport.rect.height;

        bool canScroll = _contentHeight > viewportHeight;
        Scrollbar.gameObject.SetActive(canScroll);
        enabled = canScroll;
        
        if (canScroll)
        {
            Scrollbar.size = (_contentHeight - viewportHeight) / (MaxScrollHeight - viewportHeight);
        }
    }

    private void Update()
    {
        if (Content == null)
            return;

        float viewportHeight = Viewport.rect.height;
        float scrollLimit = _contentHeight - viewportHeight + Mathf.Abs(Text.rectTransform.offsetMax.y) * 2;
        float newScrollHeight = Content.anchoredPosition.y + scrollSpeed * Time.deltaTime;

        Content.anchoredPosition = new Vector2(0f, Mathf.Clamp(newScrollHeight, 0f, scrollLimit));
    }
}
