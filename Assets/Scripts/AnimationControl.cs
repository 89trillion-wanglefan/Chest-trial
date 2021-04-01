using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public Manager other;
    public void OpenEnd()
    {
        other.OpenEnd();
    }

    public void CloseEnd()
    {
        other.CloseEnd();
    }
}
