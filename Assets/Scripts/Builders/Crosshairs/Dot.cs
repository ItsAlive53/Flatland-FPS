using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Builders.Crosshairs {
    public class Dot : Crosshair {
        private RawImage crosshairImage;

        public Dot(Canvas canvas) : this(canvas, new CrosshairStyle() {
            OffsetEnabled = false,
            Offset = 1f,
            ThicknessEnabled = false,
            Thickness = 1f,
            Scale = 1f,
            OutlineWidth = 0,
            OutlineColor = new Color(0, 0, 0, 0),
            FillColor = Color.white
        }) {}

        public Dot(Canvas canvas, CrosshairStyle style) {
            SetCanvas(canvas);
            BaseSizeHorizontal = BaseSizeVertical = 5f;
            Style = style;
            Style.OffsetEnabled = Style.ThicknessEnabled = false;
            crosshairImage = CreateImage();
        }

        protected override void UpdateImage() {
        }

        public override void Disable() {
            GameObject.Destroy(crosshairImage);
        }

        public void SetTexture(Texture texture) {
            crosshairImage.texture = texture;
        }
    }
}
