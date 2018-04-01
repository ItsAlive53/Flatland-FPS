using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Builders.Crosshairs {
    public class DottedCross : Crosshair {
        private Dot dot;
        private Cross cross;

        public DottedCross(Canvas canvas) : this(canvas, new CrosshairStyle() {
            OffsetEnabled = true,
            Offset = 20f,
            ThicknessEnabled = true,
            Thickness = 1f,
            Scale = 1f,
            OutlineWidth = 0,
            OutlineColor = new Color(0, 0, 0, 0),
            FillColor = Color.white
        }) { }

        public DottedCross(Canvas canvas, CrosshairStyle style) {
            SetCanvas(canvas);
            Style = style;
            dot = new Dot(canvas, style);
            cross = new Cross(canvas, style);
        }

        protected override void UpdateImage() {
            cross.Update();
        }

        public void SetTexture(Texture dotTexture, Texture crossTexture) {
            dot.SetTexture(dotTexture);
            cross.SetTexture(crossTexture);
        }
    }
}
