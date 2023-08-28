using DOT.Utilities;
using UnityEngine;

namespace DOT.Animations
{
    public class DotBubbleAnimation : MonoBehaviour
    {
        private GameObject[] rightDots;
        private Vector3 initialDotScales;
        [Range(1.0f, 2.0f)] [SerializeField] private float scale;
        [SerializeField] private float animateDuration = 0.1f;
        [SerializeField] private bool isActivate = false;

        public void SetActivate(bool activate)
        {
            isActivate = activate;
        }

        // Start is called before the first frame update
        void Awake()
        {
            rightDots = ObjectGetter.dotsRight.ToArray();
            initialDotScales = rightDots[0].transform.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            if (isActivate)
            {
                DotAnimation();
            }
        }

        void DotAnimation()
        {
            foreach (var dot in rightDots)
            {
                if (MouseInDot(dot.GetComponent<CircleCollider2D>()))
                {
                    dot.transform.localScale = initialDotScales * scale;
                }
                else
                {
                    dot.transform.localScale = initialDotScales;
                }
            }
        }

        bool MouseInDot(CircleCollider2D dot)
        {
            Bounds dotBound = dot.bounds;
            Vector3 mousePosition = Utils.GetMouseWorldPosition();
            return dotBound.Contains(mousePosition);
        }
    }

}
