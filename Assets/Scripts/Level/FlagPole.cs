using UnityEngine;

public class FlagPole : MonoBehaviour
{
    [SerializeField] private Transform _flagUpperPart; 
    [SerializeField] private float _flagLowerSpeed = 4.5f; 
    [SerializeField] private float _flagBottomY = -1f; 
    [SerializeField] private int _points = 400; 
    private bool _isLowering = false;

    private void Update()
    {
        if (_isLowering && _flagUpperPart.position.y > _flagBottomY)
        {
            _flagUpperPart.position = Vector3.MoveTowards(_flagUpperPart.position, new Vector3(_flagUpperPart.position.x, _flagBottomY, _flagUpperPart.position.z), _flagLowerSpeed * Time.deltaTime);
            if (_flagUpperPart.position.y <= _flagBottomY)
            {
                _isLowering = false;
                UnsecuredEventBus.TriggerFlagLowered();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isLowering)
        {
            _isLowering = true;
            UnsecuredEventBus.TriggerFlagReached(transform.position, _points);
        }
    }
}
