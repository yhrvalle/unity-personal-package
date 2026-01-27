using UnityEngine;

public interface IPlayerInputReader
{
    public Vector2 Direction { get; }
    public void EnablePlayerInputActions();
    public void DisablePlayerInputActions();
}
