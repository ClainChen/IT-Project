using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace DOT.Utilities
{
    public class Utils
    {
        /// <summary>
        /// 1. Get the world position and translate it to screen position (needs z-axis coordinate)
        /// 2. Get the mouse position
        /// 3. mouse's z-axis coordinate should be equal to world's z-axis coordinate
        /// 4. Change the mouse position to world position
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(Vector3.zero);
            Vector3 mousePosOnScreen = Input.mousePosition;
            mousePosOnScreen.z = screenPos.z;
            return Camera.main.ScreenToWorldPoint(mousePosOnScreen);
        }
    }
}
