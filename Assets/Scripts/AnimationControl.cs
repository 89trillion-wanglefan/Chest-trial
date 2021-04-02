using UnityEngine;

/// <summary>
/// 动画帧事件脚本
/// </summary>
public class AnimationControl : MonoBehaviour
{
    public Manager other;
    /// <summary>
    /// 当打开动画播放完毕
    /// </summary>
    public void OpenEnd()
    {
        other.OpenEnd();
    }

    /// <summary>
    /// 关闭动画播放完毕
    /// </summary>
    public void CloseEnd()
    {
        other.CloseEnd();
    }
}
