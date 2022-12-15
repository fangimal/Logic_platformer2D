using System.Collections;
using UnityEngine;
using System;

    public class AnimationTransform : MonoBehaviour
    {
        private static AnimationTransform animationTransform;

        private static AnimationTransform GetAnimationTransform
        {
            get
            {
                if (!animationTransform)
                {
                    animationTransform = new GameObject("AnimationTransform").AddComponent<AnimationTransform>();
                    DontDestroyOnLoad(animationTransform);
                }

                return animationTransform;
            }
        }

        private AnimationCurve moveCurve;

        private AnimationCurve GetMoveCurve
        {
            get
            {
                if(moveCurve == null)
                {
                    Keyframe[] keyframes = new Keyframe[2];
                    keyframes[0] = new Keyframe(0f, 0f, 0f, 0f, 0f, 0f)
                    {
                        weightedMode = WeightedMode.None
                    };

                    keyframes[1] = new Keyframe(1f, 1f, 0f, 0f, 0.25f, 0f)
                    {
                        weightedMode = WeightedMode.None
                    };

                    moveCurve = new AnimationCurve(keyframes);
                }
                return moveCurve;
            }
        }

        private AnimationCurve jumpCurve;

        private AnimationCurve GetJumpCurve
        {
            get
            {
                if (jumpCurve == null)
                {
                    Keyframe[] keyframes = new Keyframe[3];
                    keyframes[0] = new Keyframe(0f, 0f, 3.25f, 3.25f, 0f, 0.05f)
                    {
                        weightedMode = WeightedMode.None
                    };

                    keyframes[1] = new Keyframe(0.5f, 1f, 0.017f, 0.017f, 0.333f, 0.333f)
                    {
                        weightedMode = WeightedMode.None
                    };

                    keyframes[2] = new Keyframe(1f, 0f, -3.339621f, -3.339621f, 0.08333f, 0f)
                    {
                        weightedMode = WeightedMode.None
                    };

                    jumpCurve = new AnimationCurve(keyframes);
                }
                return jumpCurve;
            }
        }
        #region Rotate
        public static void Rotate(Transform tfm, Vector3 forward, float time)
        {
            GetAnimationTransform.ThisRotate(tfm, forward, time);
        }

        private void ThisRotate(Transform tfm, Vector3 forward, float time)
        {
            StartCoroutine(AnimateRotate(tfm, forward, time));
        }

        private IEnumerator AnimateRotate(Transform tfm, Vector3 forward, float rotateTime)
        {
            float time = 0f;
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

            Vector3 strForward = tfm.forward;

            while (tfm && time < rotateTime)
            {
                tfm.LookAt(tfm.position + Vector3.Lerp(strForward, forward, GetMoveCurve.Evaluate(time / rotateTime)));
                time += Time.deltaTime;
                yield return waitForEndOfFrame;
            }
            if (tfm)
            {
                tfm.LookAt(tfm.position + forward);
            }
        }

        #endregion

        #region Move

        public static void Move(Transform tfm, Vector3 position, float time)
        {
            GetAnimationTransform.ThisMove(tfm, position, time);
        }

        private void ThisMove(Transform tfm, Vector3 position, float time)
        {
            StartCoroutine(AnimateMove(tfm, position, time));
        }

        private IEnumerator AnimateMove(Transform tfm, Vector3 position, float moveTime)
        {
            float time = 0f;
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

            Vector3 strPosition = tfm.position;

            while (tfm && time < moveTime)
            {
                tfm.position = Vector3.Lerp(strPosition, position, GetMoveCurve.Evaluate(time / moveTime));
                time += Time.deltaTime;
                yield return waitForEndOfFrame;
            }
            if (tfm)
            {
                tfm.position = position;
            }
        }
        #endregion

        #region Jump

        public static void Jump(Transform tfm, Vector3 position, float time, float height, Action onComplited = null)
        {
            GetAnimationTransform.ThisJump(tfm, position, time, height, onComplited);
        }

        private void ThisJump(Transform tfm, Vector3 position, float time, float height, Action onComplited)
        {
            StartCoroutine(AnimateJump(tfm, position, time, height, onComplited));
        }

        private IEnumerator AnimateJump(Transform tfm, Vector3 position, float jumpTime, float jumpHeight, Action onComplited)
        {
            float time = 0f;
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

            Vector3 strPosition = tfm.position;

            while (tfm && time < jumpTime)
            {
                float t = time / jumpTime;
                tfm.position = Vector3.Lerp(strPosition, position, GetMoveCurve.Evaluate(t)) +
                    new Vector3(0f, jumpHeight * GetJumpCurve.Evaluate(t), 0f);

                time += Time.deltaTime;
                yield return waitForEndOfFrame;
            }
            if (tfm)
            {
                tfm.position = position;
                onComplited?.Invoke();
            }
        }

        public static void Jump(Transform tfm, Transform position, float time, float height, Action onComplited = null)
        {
            GetAnimationTransform.ThisJump(tfm, position, time, height, onComplited);
        }

        private void ThisJump(Transform tfm, Transform position, float time, float height, Action onComplited)
        {
            StartCoroutine(AnimateJump(tfm, position, time, height, onComplited));
        }

        private IEnumerator AnimateJump(Transform tfm, Transform position, float jumpTime, float jumpHeight, Action onComplited)
        {
            float time = 0f;
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

            Vector3 strPosition = tfm.position;

            while (tfm && time < jumpTime)
            {
                float t = time / jumpTime;
                tfm.position = Vector3.Lerp(strPosition, position.position, GetMoveCurve.Evaluate(t)) +
                    new Vector3(0f, jumpHeight * GetJumpCurve.Evaluate(t), 0f);

                time += Time.deltaTime;
                yield return waitForEndOfFrame;
            }
            if (tfm)
            {
                tfm.position = position.position;
                onComplited?.Invoke();
            }
        }

        #endregion

        #region Scale
        public static void Scale(Transform tfm, Vector3 scale, float time, Action complited = null)
        {
            GetAnimationTransform.ThisScale(tfm, scale, time, complited);
        }

        private void ThisScale(Transform tfm, Vector3 scale, float time, Action complited)
        {
            StartCoroutine(AnimateScale(tfm, scale, time, complited));
        }

        private IEnumerator AnimateScale(Transform tfm, Vector3 scale, float scaleTime, Action complited)
        {
            float time = 0f;
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

            Vector3 strLocalScale = tfm.localScale;

            while (tfm && time < scaleTime)
            {
                float t = time / scaleTime;
                tfm.localScale = Vector3.Lerp(strLocalScale, scale, t);

                time += Time.deltaTime;
                yield return waitForEndOfFrame;
            }
            if (tfm)
            {
                tfm.localScale = scale;
            }
        }
        #endregion

        #region MoveInCurve

        public static void MoveInCurve(Transform tfm, float time, float height, float distance,
            Vector3 direction, float deltaY, AnimationCurve curve, AnimationCurve timeCurve,
            Action onComplitedMove, Action onComplited, Action<float> onUpdate, float waitTime = 0f)
        {
            GetAnimationTransform.ThisMoveInCurve(tfm, time, height, distance, direction, deltaY, curve, timeCurve,
                onComplitedMove, onComplited, onUpdate, waitTime);
        }

        private void ThisMoveInCurve(Transform tfm, float time, float height, float distance,
            Vector3 direction, float deltaY, AnimationCurve curve, AnimationCurve timeCurve,
            Action onComplitedMove, Action onComplited, Action<float> onUpdate, float waitTime)
        {
            StartCoroutine(AnimateMoveInCurve(tfm, time, height, distance, direction, deltaY, curve, timeCurve,
               onComplitedMove, onComplited, onUpdate, waitTime));
        }

        private IEnumerator AnimateMoveInCurve(Transform tfm, float moveTime, float height, float distance,
            Vector3 direction, float deltaY, AnimationCurve curve, AnimationCurve timeCurve,
            Action onComplitedMove, Action onComplited, Action<float> onUpdate, float waitTime)
        {
            float time = 0f;
            WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

            Vector3 strPosition = tfm.position;

            while (tfm && time < moveTime)
            {
                float t = timeCurve.Evaluate(time / moveTime);
                float cDistance = Mathf.Lerp(0f, distance, t);
                tfm.position = strPosition + cDistance * direction +
                    (height * curve.Evaluate(t) + Mathf.Lerp(0f, deltaY, t)) * Vector3.up;
                onUpdate?.Invoke(t);
                time += Time.deltaTime;
                yield return waitForEndOfFrame;
            }

            if (tfm)
            {
                tfm.position = strPosition + distance * direction + (height * curve.Evaluate(1f) + deltaY) * Vector3.up;
                onUpdate?.Invoke(1f);
                if (tfm)
                {
                    onComplitedMove?.Invoke();
                }
                yield return new WaitForSeconds(waitTime);
                if (tfm)
                {
                    onComplited?.Invoke();
                }
            }
        }
        #endregion
    }

