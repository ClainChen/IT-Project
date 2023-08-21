using UnityEngine;

namespace DOT.Utilities
{
    public class CoordinateTester : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Touch"))
            {
                Vector3 mousePos = Utils.GetMouseWorldPosition();
                Debug.Log(mousePos);
            }
        }
    }

}
