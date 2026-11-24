using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light flashlight;

    [SerializeField] private Vector2 outerSpotRange = new Vector2(20f, 120f);
    [SerializeField] private Vector2 lightRange = new Vector2(3f, 10f);

    [SerializeField] private float sensitivity = 0.05f;

    private float _normalizedSpotAngle;

    private void Start()
    {
        flashlight.spotAngle = Clamp(flashlight.spotAngle, outerSpotRange);
        flashlight.range = Clamp(flashlight.range, lightRange);

        _normalizedSpotAngle = InverseLerp(outerSpotRange, flashlight.spotAngle);
    }

    private void Update()
    {
        float deltaY = GetMouseScrollDeltaY();

        // 光线越散，照射距离近
        // 光线越聚，照射距离远

        _normalizedSpotAngle = Mathf.Clamp01(_normalizedSpotAngle + deltaY * sensitivity);
        float t = _normalizedSpotAngle;

        flashlight.spotAngle = Lerp(outerSpotRange, t);
        flashlight.range = Lerp(lightRange, 1f - t);
    }

    private float GetMouseScrollDeltaY()
    {
#if ENABLE_INPUT_SYSTEM
        return Mouse.current.scroll.ReadValue().y;
#else
        return Input.GetAxis("Mouse ScrollWheel");
#endif
    }

    private float Clamp(float value, Vector2 range)
    {
        return Mathf.Clamp(value, range.x, range.y);
    }

    private float InverseLerp(Vector2 range, float value)
    {
        return (value - range.x) / (range.y - range.x);
    }

    private float Lerp(Vector2 range, float t)
    {
        return Mathf.Lerp(range.x, range.y, t);
    }
}