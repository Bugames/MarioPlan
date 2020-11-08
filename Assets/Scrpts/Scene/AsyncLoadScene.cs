using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AsyncLoadScene : MonoBehaviour
{
    #region public members
    /// <summary>
    /// 文字
    /// </summary>
    public TextMeshProUGUI loadTest;
    /// <summary>
    /// 进度条
    /// </summary>
    public Slider loadProgressbar;
    /// <summary>
    /// 下一个场景的名字
    /// </summary>
    [Tooltip("下个场景的名字")]
    public int nextSceneIndex;
    #endregion

    #region private members
    /// <summary>
    /// 进度条百分比值
    /// </summary>
    private float currentBarvalue = 0.0f;
    /// <summary>
    /// 加载场景进程控制
    /// </summary>
    private AsyncOperation asyncOperation;
    #endregion

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        nextSceneIndex = SceneController.Instance.nextSceneindex;
        //开始加载协程
        StartCoroutine("AsyncLoading");
    }

    #region public methods
    /// <summary>
    /// 协程异步加载场景
    /// </summary>
    /// <returns></returns>
    public IEnumerator AsyncLoading()
    {
        asyncOperation = SceneManager.LoadSceneAsync(nextSceneIndex);
        asyncOperation.allowSceneActivation = false;

        while(!asyncOperation.isDone)
        {
            if(asyncOperation.progress<0.9f)
            {
                currentBarvalue = asyncOperation.progress;
            }
            else
            {
                currentBarvalue = 1.0f;
            }

            loadProgressbar.value = currentBarvalue;

            if(asyncOperation.progress>=0.9f)
            {
                loadTest.text = "按任意键继续";
                if(Input.anyKey)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
    #endregion
}
