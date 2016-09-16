using UnityEngine;
using System.Collections;

public class HatController : MonoBehaviour
{

    public Camera cam;

    private bool canControl;
    private float maxWidth;

    // Use this for initialization
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }

        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float hatWidth = this.GetComponent<Renderer>().bounds.extents.x;
        maxWidth = targetWidth.x - hatWidth;

        canControl = false;
    }
	
    // Update is called once per physics timestep
    void FixedUpdate()
    {
        if (canControl)
        {
            Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPosition = new Vector3(rawPosition.x, 0, 0);
            float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);
            targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);
            this.GetComponent<Rigidbody2D>().MovePosition(targetPosition);
        }
    }

    public void ToggleControl()
    {
        canControl = !canControl;
    }
}
