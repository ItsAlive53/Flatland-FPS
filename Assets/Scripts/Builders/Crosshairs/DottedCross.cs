using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Builders.Crosshairs {
    public class DottedCross : Crosshair {
        private RawImage middleImage;
        private RawImage topImage;
        private RawImage bottomImage;
        private RawImage leftImage;
        private RawImage rightImage;

        public DottedCross(Canvas canvas) : this(canvas, new CrosshairStyle() {
            OffsetEnabled = true,
            Offset = 1f,
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
            middleImage = CreateImage();
            topImage = CreateImage();
            leftImage = CreateImage();
            bottomImage = CreateImage();
            rightImage = CreateImage();
        }

        protected override void UpdateImage() {
            var images = new RawImage[] { topImage, leftImage, bottomImage, rightImage };
            for (var i = 0; i < images.Length; i++) {
                images[i].GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);

                images[i].GetComponent<RectTransform>().localPosition = new Vector3(0, Style.Offset, 0);
                float rot = 360f - 90f * (4 - i);

                images[i].GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, rot));
            }
        }

        public void SetTexture(Texture dotTexture, Texture crossTexture) {
            middleImage.texture = dotTexture;
            topImage.texture = crossTexture;
            bottomImage.texture = crossTexture;
            leftImage.texture = crossTexture;
            rightImage.texture = crossTexture;
        }
    }
}
