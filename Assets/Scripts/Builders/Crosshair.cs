using UnityEngine;
using UnityEngine.UI;

namespace Builders {
    public abstract class Crosshair {
        protected Canvas baseCanvas;
        protected float BaseSizeHorizontal = 1f;
        protected float BaseSizeVertical = 1f;

        public CrosshairStyle Style { get; set; }

        protected abstract void UpdateImage();

        public Canvas GetCanvas() {
            return baseCanvas;
        }

        public void SetCanvas(Canvas canvas) {
            baseCanvas = canvas;
        }

        protected RawImage CreateImage() {
            var go = new GameObject("Crosshair");
            go.transform.SetParent(baseCanvas.transform);

            var img = go.AddComponent<RawImage>();

            // Create default bg
            var rt = img.GetComponent<RectTransform>();

            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = Vector2.zero;
            rt.sizeDelta = Vector2.zero;
            rt.localScale = Vector3.one;
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Style.Scale * BaseSizeHorizontal);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Style.Scale * BaseSizeVertical);
            var pos = Vector2.zero;
            rt.localPosition = new Vector3(pos.x, pos.y);

            img.color = Style.FillColor;

            return img;
        }
    }
}
