using UnityEngine;

public class ViewMenuResources : MonoBehaviour, IViewWindow
{
    public void WindowShow()
    {
        gameObject.SetActive(true);
    }

    public void WindowHide()
    {
        gameObject.SetActive(false);
    }
}