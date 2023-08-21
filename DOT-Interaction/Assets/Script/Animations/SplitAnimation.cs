using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DOT.Line;
using DOT.Utilities;
using UnityEngine;

namespace DOT.Animations
{
    public class SpliteAnimation : MonoBehaviour
    {
        private GameObject leftMatrix;
        private GameObject rightMatrix;
        private LineRendererController interaction;
        private DotBubbleAnimation dotAnimation;
        private GameObject leftLine;
        private LineRenderer lr;

        private bool animating = false;
        private bool endAnimating = false;
        public float animateTime = 2f;
        public float targetScale = 250f;
        public float offset = 120;
        private float moveFrame;
        private float shrinkRate;

        private bool startAnimation = false;


        // Start is called before the first frame update
        void Start()
        {
            leftMatrix = GameObject.Find("Left");
            rightMatrix = GameObject.Find("Right");
            leftLine = GameObject.Find("LineLeft");
            lr = leftLine.GetComponent<LineRenderer>();
            interaction = GetComponent<LineRendererController>();
            dotAnimation = GetComponent<DotBubbleAnimation>();

            float targetXCoord = ((float)Screen.width / 4) + offset;
            moveFrame = ((targetXCoord) / animateTime) * Time.fixedDeltaTime;
            shrinkRate = ((300 - targetScale) / animateTime) * Time.fixedDeltaTime;

            rightMatrix.transform.position = new Vector3(targetXCoord, 0, 0);
            rightMatrix.transform.localScale = new Vector3(targetScale, targetScale, targetScale);
            Debug.Log(moveFrame / Time.fixedDeltaTime);
            Debug.Log(shrinkRate / Time.fixedDeltaTime);
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (animateTime > 0)
            {
                rightMatrix.SetActive(false);
                leftMatrix.transform.Translate(-moveFrame,0,0);
                Vector3 scale = leftMatrix.transform.localScale;
                leftMatrix.transform.localScale = scale - new Vector3(shrinkRate, shrinkRate, shrinkRate);
                UpdateLeftLineRender();

                animateTime -= Time.fixedDeltaTime;
            }
            else
            {
                // animateTime = 0;
                interaction.SetActivate(true);
                dotAnimation.SetActivate(true);
                leftMatrix.transform.localScale = new Vector3(targetScale, targetScale, targetScale);
                leftMatrix.transform.position = new Vector3((- (float)Screen.width / 4) - offset, 0, 0);
                rightMatrix.SetActive(true);
            }
        }

        void UpdateLeftLineRender()
        {
            List<GameObject> dotList = GameObject.FindGameObjectsWithTag("Matrix1").ToList();
            for (int i = 0; i < Constants.PRE_LOAD_DOTS.Length; i++)
            {
                foreach (GameObject go in dotList)
                {
                    if (go.name.EndsWith(Constants.PRE_LOAD_DOTS[i]))
                    {
                        lr.SetPosition(i, go.transform.position);
                    }
                }
            }
        }

        public void StartAnimation()
        {
            startAnimation = true;
        }
    }

}
