using UnityEngine;

namespace Builders {
    public abstract class Crosshair {
        protected Canvas baseCanvas;

        protected string type;

        protected CrosshairStyle style;

        public string GetCrosshairType() {
            return type;
        }

        public void SetCrosshairType(string name) {
            type = name;
        }

        public CrosshairStyle GetStyle() {
            return style;
        }

        public void SetStyle(CrosshairStyle style) {
            this.style = style;
        }

        public Canvas GetCanvas() {
            return baseCanvas;
        }

        public void SetCanvas(Canvas canvas) {
            baseCanvas = canvas;
        }
    }
}
