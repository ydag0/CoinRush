using UnityEngine;
public class InputManager : Singleton<InputManager>
{
    const string mouseXAxis = "Mouse X";
    public float mouseX{ get; private set; }

    void Update()
    {
        if (Input.GetMouseButton(0))
            mouseX = Input.GetAxis(mouseXAxis);
        else
            mouseX = 0;
    }
}
