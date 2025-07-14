using UnityEngine;
using UnityEngine.UIElements;

// public enum TransitionType { None, Fade }
public abstract class ScreenUIToolkitBase : MonoBehaviour, INavigationScreen {
    protected UIDocument _uiDocument;
    protected VisualElement Root => _uiDocument.rootVisualElement;

    public virtual void OnEnter(object param = null)
    {
        // gameObject.SetActive(true);
        Root.SetEnabled(true);
    }
    public virtual void OnExit()
    {
        // gameObject.SetActive(false);    
        Root.SetEnabled(false);
    }
}