using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] GameEvent _gameEvent;
    [SerializeField] UnityEvent _response;

    private void OnEnable() {
        
        _gameEvent.RegisterListener(this);
    }

    private void OnDisable() {
        
        _gameEvent.UnegisterListener(this);
    }

    public void RaiseResponse()
    {
        _response.Invoke();
    }
}
