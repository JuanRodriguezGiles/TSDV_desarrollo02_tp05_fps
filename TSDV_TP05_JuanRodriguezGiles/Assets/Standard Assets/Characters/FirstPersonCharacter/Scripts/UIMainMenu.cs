using UnityEngine;
public class UIMainMenu : MonoBehaviour
{
    public void LoadGameplayScene()
    {
       GameManager.Get().LoadGameplayScene();
    }
}