using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Builders {
    public class HUD {
        public enum ValueType {
            Pixels,
            Percentage
        }

        public enum ScreenPoint {
            TopLeft,
            TopMiddle,
            TopRight,
            CentreLeft,
            CentreMiddle,
            CentreRight,
            BottomLeft,
            BottomMiddle,
            BottomRight
        }

        private Canvas baseCanvas;

        public HUD(Canvas c) {
            baseCanvas = c;
            c.gameObject.name = "HUD";

            baseCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var canvasScaler = baseCanvas.GetComponent<CanvasScaler>() ? baseCanvas.GetComponent<CanvasScaler>() : baseCanvas.gameObject.AddComponent<CanvasScaler>();

            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920f, 1080f);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0;
            canvasScaler.referencePixelsPerUnit = 100f;
        }

        public HUD() : this(new GameObject().AddComponent<Canvas>()) {
        }

        public Canvas GetCanvas() {
            return baseCanvas;
        }

        public Bar CreateBar(ScreenPoint screenPoint, Vector2 size, ValueType valueType, float borderWidth, Vector2 offset) {
            if (valueType == ValueType.Percentage) {
                return new Bar(baseCanvas, screenPoint, new Vector2(size.x * baseCanvas.pixelRect.width, size.y * baseCanvas.pixelRect.height), borderWidth, offset);
            }
            return new Bar(baseCanvas, screenPoint, size, borderWidth, offset);
        }

        public Bar CreateBar(ScreenPoint screenPoint, Vector2 size, ValueType valueType, float borderWidth, Vector2 offset, Color backgroundColour, Color foregroundColour) {
            if (valueType == ValueType.Percentage) {
                return new Bar(baseCanvas, screenPoint, new Vector2(size.x * baseCanvas.pixelRect.width, size.y * baseCanvas.pixelRect.height), borderWidth, offset, backgroundColour, foregroundColour);
            }
            return new Bar(baseCanvas, screenPoint, size, borderWidth, offset, backgroundColour, foregroundColour);
        }

        public CustomText CreateText(ScreenPoint screenPoint, int fontSize, Font font, Vector2 offset, Color colour) {
            return new CustomText(baseCanvas, screenPoint, fontSize, font, offset, colour);
        }
    }
}
