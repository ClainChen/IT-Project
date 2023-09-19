using DOT.Utilities;
using UnityEngine;

namespace DOT.Animations
{
    public class DotBubbleAnimation : MonoBehaviour
    {
        private GameObject[] rightDots;
        private Vector3 initialDotScales;
        [Range(1.0f, 2.0f)] [SerializeField] private float scale;

        public PlaySound PlaySound;
        private GameObject currentDot;

        // Start is called before the first frame update
        void Start()
        {
            rightDots = ObjectGetter.dotsRight.ToArray();
            initialDotScales = rightDots[0].transform.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            DotAnimation();
        }

        void DotAnimation()
        {
            foreach (var dot in rightDots)
            {
                if (MouseInDot(dot.GetComponent<CircleCollider2D>()))
                {
                    if (!dot.Equals(currentDot))
                    {
                        if (GetComponent<AudioSource>().enabled)
                        {
                            PlaySound.PlayAudio();
                            currentDot = dot;
                        }
                    }
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
            
            return dot.bounds.Contains(Utils.GetMouseWorldPosition());
        }
    }

}
