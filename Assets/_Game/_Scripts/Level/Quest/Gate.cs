using LogicPlatformer.Player;
using System.Collections;
using UnityEngine;

namespace LogicPlatformer
{
    public class Gate : IActivate
    {
        [SerializeField] private SetDoor setDoor;

        [SerializeField] private Transform lockIcon;
        [SerializeField] private BoxCollider2D colider;

        [SerializeField] private Transform gateTransform;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private float duration = 1f;
        [SerializeField] private int multiClickValue = 4;


        [Space(10), Header("Set Key")]
        [SerializeField] private Key key;

        private Animation anim;
        private bool isOpen = true;
        private Coroutine coroutine;

        private int clickValue = 0;
        private int ClickValue
        {
            get
            {
                return clickValue;
            }
            set
            {
                if (value < 0)
                {
                    clickValue = 0;
                }
                else if (clickValue > multiClickValue)
                {
                    clickValue = multiClickValue;
                }
                else
                {
                    clickValue = value;
                }
            }
        }
        private float cTime = 0f;
        private enum SetDoor
        {
            Key,
            Activation,
            MultiClicks
        }

        private void Start()
        {
            anim = GetComponent<Animation>();
            lockIcon.gameObject.SetActive(setDoor == SetDoor.Key);
            colider.enabled = setDoor == SetDoor.Key;

            if (key != null)
            {
                lockIcon.gameObject.GetComponent<SpriteRenderer>().color = key.gameObject.GetComponent<SpriteRenderer>().color;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>() && collision.GetComponent<PlayerManager>().Key != null)
            {
                if (key.GetKeyID == collision.GetComponent<PlayerManager>().Key.GetKeyID)
                {
                    colider.enabled = false;
                    collision.GetComponent<PlayerManager>().FreedArm();
                    anim.Play();
                }
            }
        }

        public override void Activate()
        {
            Animation();
        }

        private void Animation()
        {
            switch (setDoor)
            {
                case SetDoor.Key:
                    OpenCloseGate();
                    break;
                case SetDoor.Activation:
                    OpenCloseGate();
                    break;
                case SetDoor.MultiClicks:
                    MultiClickOpen();
                    break;
                default:
                    OpenCloseGate();
                    break;
            }
        }

        private void OpenCloseGate()
        {
            if (isOpen)
            {
                isOpen = false;
                AnimationTransform.Move(gateTransform, endPoint.position, duration);
            }
            else
            {
                isOpen = true;
                AnimationTransform.Move(gateTransform, startPoint.position, duration);
            }
        }
        private Vector3 SetEndPoint()
        {
            float yPos = (endPoint.position - gateTransform.position).magnitude;
            float y = gateTransform.position.y + yPos / (float)multiClickValue * (float)ClickValue;

            return new Vector3(gateTransform.position.x, y, gateTransform.position.z);
        }
        private void MultiClickOpen()
        {
            ClickValue++;
            coroutine = StartCoroutine(AnimateMove(gateTransform, SetEndPoint(), duration));

            if (cTime <= 0)
            {
                StartCoroutine(Timer());
            }
            cTime--;
        }

        public override void Deactivate()
        {
            switch (setDoor)
            {
                case SetDoor.Key:
                    Debug.Log("Don't set action (SetDoor.Key)");
                    break;
                case SetDoor.Activation:
                    OpenCloseGate();
                    break;
                case SetDoor.MultiClicks:
                    StopCoroutine(coroutine);
                    AnimationTransform.Move(gateTransform, startPoint.position, duration);
                    break;
                default:
                    break;
            }
        }

        private IEnumerator AnimateMove(Transform tfm, Vector3 position, float moveTime)
        {
            float time = 0f;
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

            Vector3 strPosition = tfm.position;

            if (position.y > endPoint.position.y)
            {
                position = endPoint.position;
            }

            while (tfm && time < moveTime )
            {
                float t = time / moveTime;
                tfm.position = Vector3.Lerp(strPosition, position, t);
                time += Time.deltaTime;
                yield return waitForEndOfFrame;
            }
            if (tfm)
            {
                tfm.position = position;
            }

        }

        private IEnumerator Timer()
        {
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

            while (cTime < duration)
            {
                cTime += Time.deltaTime;
                yield return waitForEndOfFrame;
            }
            ClickValue = 0;
        }
    }
}
