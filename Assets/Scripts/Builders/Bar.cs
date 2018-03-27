using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Builders {
    public class Bar {
        private Canvas baseCanvas;
        private RawImage background;
        private RawImage foreground;

        private float totalWidth;
        private float currentWidth;
        
        public Bar(Canvas canvas, HUD.ScreenPoint screenPoint, Vector2 size, float borderWidth, Vector2 offset, Color backgroundColour, Color foregroundColour) {
            baseCanvas = canvas;
            background = CreateImage(screenPoint, new Vector2(size.x + borderWidth * 2, size.y + borderWidth * 2), CalcBgOffset(offset, borderWidth, screenPoint), backgroundColour);
            foreground = CreateImage(screenPoint, size, offset, foregroundColour);
            totalWidth = size.x;

            background.name = "BarBackground";
            foreground.name = "BarForeground";
        }

        public Bar(Canvas canvas, HUD.ScreenPoint screenPoint, Vector2 size, float borderWidth, Vector2 offset) : this(canvas, screenPoint, size, borderWidth, offset, Color.grey, Color.white) {
        }

        public void SetWidthPercentage(float width) {
            currentWidth = totalWidth * width;
            Update();
        }

        public Canvas GetCanvas() {
            return baseCanvas;
        }

        public RawImage GetBackgroundImage() {
            return background;
        }

        public RawImage GetForegroundImage() {
            return foreground;
        }

        private void Update() {
            foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
        }

        private RawImage CreateImage(HUD.ScreenPoint screenPoint, Vector2 size, Vector2 offset, Color colour) {
            var go = new GameObject();
            go.transform.SetParent(baseCanvas.transform);

            var img = go.AddComponent<RawImage>();

            // Create default bg
            var rt = img.GetComponent<RectTransform>();

            rt.pivot = CalcPivot(screenPoint);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
            var pos = CalcPos(screenPoint, offset);
            rt.localPosition = new Vector3(pos.x, pos.y);

            img.color = colour;

            return img;
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

        private Vector2 CalcBgOffset(Vector2 offset, float borderWidth, HUD.ScreenPoint screenPoint) {
            float x, y;

            switch (CalcPivot(screenPoint).x.ToString()) {
                case "0":
                    x = offset.x - borderWidth;
                    break;
                case "1":
                    x = offset.x + borderWidth;
                    break;
                default:
                    x = offset.x;
                    break;
            }

            switch (CalcPivot(screenPoint).y.ToString()) {
                case "0":
                    y = offset.y - borderWidth;
                    break;
                case "1":
                    y = offset.y + borderWidth;
                    break;
                default:
                    y = offset.y;
                    break;
            }

            return new Vector2(x, y);
        }
    }
}
