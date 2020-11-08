using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    /// <summary>
    /// 文本
    /// </summary>
    public Text text;
    /// <summary>
    /// 向上移动的时间
    /// </summary>
    public float upDistance = 50.0f;
    /// <summary>
    /// 向上的速度
    /// </summary>
    public float speed = 50.0f;
    /// <summary>
    /// 消失的秒数
    /// </summary>
    public float outTime = 0.1f;

    private void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(TextFadeOut());
    }
    /// <summary>
    /// 设置文本的颜色
    /// </summary>
    /// <param name="color"></param>
    public void SetTextColor(Color color)
    {
        text.color = color;
    }
    /// <summary>
    /// 文字向上并且逐渐消失
    /// </summary>
    /// <returns></returns>
    public IEnumerator TextFadeOut()
    {
        while(upDistance>0)
        {
            float upDelta = Time.deltaTime * speed;
            upDistance -= upDelta;
            Vector3 ori = this.GetComponent<RectTransform>().anchoredPosition3D;
            Vector3 positon = new Vector3(ori.x, ori.y + upDelta, ori.z);
            GetComponent<RectTransform>().anchoredPosition3D = positon;
            yield return 0;
        }

        while (outTime > 0)
        {
            Color ori = text.color;
            ori.a = Mathf.Lerp(ori.a, 0, Time.deltaTime);
            SetTextColor(ori);
            outTime -= Time.deltaTime;
            yield return 0;
        }
        DestroyImmediate(this.gameObject);
    }
}
