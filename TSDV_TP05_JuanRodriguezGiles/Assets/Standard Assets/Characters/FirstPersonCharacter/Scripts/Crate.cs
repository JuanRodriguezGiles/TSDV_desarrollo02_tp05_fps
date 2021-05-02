using UnityEngine;
public class Crate : MonoBehaviour,GameManager.IPickUp
{
    public delegate void CratePickUp(Crate crate);
    public CratePickUp OnCratePickUp;
    private int points = 50;
    void Awake()
    {
        OnCratePickUp += PickedUpAction;
    }
    void PickedUpAction(Crate crate)
    {
        GameManager.Get().PlayerScore += crate.points;
        UIGameplay.Get().UpdateScoreText();
        Destroy(gameObject);
    }
    public void OnPickUp()
    {
        OnCratePickUp?.Invoke(this);
    }
}