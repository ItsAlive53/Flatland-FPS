using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Builders {
    public class CustomText {
        Canvas baseCanvas;
        Text text;

        public CustomText(Canvas canvas, HUD.ScreenPoint screenPoint, int fontSize, Font font, Vector2 offset, Color colour) {
            baseCanvas = canvas;

            text = CreateText(screenPoint, offset);
            text.font = font;
            text.fontSize = fontSize;
            text.color = colour;
            text.alignment = TextAnchor.MiddleCenter;
        }

        public void SetTextString(string textString) {
            text.text = textString;
        }

        private Text CreateText(HUD.ScreenPoint screenPoint, Vector2 offset) {
            var go = new GameObject();
            go.transform.SetParent(baseCanvas.transform);

            var txt = go.AddComponent<Text>();

            var rt = txt.rectTransform;
            rt.pivot = CalcPivot(screenPoint);
            rt.localPosition = CalcPos(screenPoint, offset);

            return txt;
        }

        private Vector2 CalcPos(HUD.ScreenPoint screenPoint, Vector2 offset) {
            float x, y;

            if (screenPoint == HUD.ScreenPoint.BottomLeft || screenPoint == HUD.ScreenPoint.CentreLeft || screenPoint == HUD.ScreenPoint.TopLeft) {
                x = -(baseCanvas.GetComponent<CanvasScaler>().referenceResolution.x / 2f) + offset.x;
            } else if (screenPoint == HUD.ScreenPoint.BottomMiddle || screenPoint == HUD.ScreenPoint.CentreMiddle || screenPoint == HUD.ScreenPoint.TopMiddle) {
                x = offset.x;
            } else {
                x = (baseCanvas.GetComponent<CanvasScaler>().referenceResolution.x / 2f) + offset.x;
            }

            if (screenPoint == HUD.ScreenPoint.TopLeft || screenPoint == HUD.ScreenPoint.TopMiddle || screenPoint == HUD.ScreenPoint.TopRight) {
                y = (baseCanvas.GetComponent<CanvasScaler>().referenceResolution.y / 2f) + offset.y;
            } else if (screenPoint == HUD.ScreenPoint.CentreLeft || screenPoint == HUD.ScreenPoint.CentreMiddle || screenPoint == HUD.ScreenPoint.CentreRight) {
                y = offset.y;
            } else {
                y = -(baseCanvas.GetComponent<CanvasScaler>().referenceResolution.y / 2f) + offset.y;
            }

            return new Vector2(x, y);
        }

        private Vector2 CalcPivot(HUD.ScreenPoint screenPoint) {
            float x, y;

            if (screenPoint == HUD.ScreenPoint.BottomLeft || screenPoint == HUD.ScreenPoint.CentreLeft || screenPoint == HUD.ScreenPoint.TopLeft) {
                x = 0;
            } else if (screenPoint == HUD.ScreenPoint.BottomMiddle || screenPoint == HUD.ScreenPoint.CentreMiddle || screenPoint == HUD.ScreenPoint.TopMiddle) {
                x = 0.5f;
            } else {
                x = 1f;
            }

            if (screenPoint == HUD.ScreenPoint.TopLeft || screenPoint == HUD.ScreenPoint.TopMiddle || screenPoint == HUD.ScreenPoint.TopRight) {
                y = 1f;
            } else if (screenPoint == HUD.ScreenPoint.CentreLeft || screenPoint == HUD.ScreenPoint.CentreMiddle || screenPoint == HUD.ScreenPoint.CentreRight) {
                y = 0.5f;
            } else {
                y = 0;
            }

            return new Vector2(x, y);
        }
    }
}
