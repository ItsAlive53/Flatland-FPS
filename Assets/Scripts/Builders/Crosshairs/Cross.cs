using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Builders.Crosshairs {
    public class Cross : Crosshair {
        private RawImage topImage;
        private RawImage bottomImage;
        private RawImage leftImage;
        private RawImage rightImage;

        public Cross(Canvas canvas) : this(canvas, new CrosshairStyle() {
            OffsetEnabled = true,
            Offset = 1f,
            ThicknessEnabled = true,
            Thickness = 1f,
            Scale = 1f,
            OutlineWidth = 0,
            OutlineColor = new Color(0, 0, 0, 0),
            FillColor = Color.white
        }) { }

        public Cross(Canvas canvas, CrosshairStyle style) {
            BaseSizeHorizontal = 5f;
            BaseSizeVertical = 21f;
            SetCanvas(canvas);
            Style = style;
            topImage = CreateImage();
            leftImage = CreateImage();
            bottomImage = CreateImage();
            rightImage = CreateImage();

            UpdateImage();
        }

        public void Update() {
            UpdateImage();
        }

        protected override void UpdateImage() {
            var images = new RawImage[] { topImage, leftImage, bottomImage, rightImage };
            for (var i = 0; i < images.Length; i++) {
                images[i].GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
                float rot = 360f - 90f * (4 - i);
                
                images[i].GetComponent<RectTransform>().localRotation = Quaternion.Euler(new Vector3(0, 0, rot));
            }

            topImage.GetComponent<RectTransform>().localPosition = new Vector3(0, Style.Offset, 0);
            leftImage.GetComponent<RectTransform>().localPosition = new Vector3(-Style.Offset, 0, 0);
            bottomImage.GetComponent<RectTransform>().localPosition = new Vector3(0, -Style.Offset, 0);
            rightImage.GetComponent<RectTransform>().localPosition = new Vector3(Style.Offset, 0, 0);
        }

        public void SetTexture(Texture texture) {
            topImage.texture = texture;
            bottomImage.texture = texture;
            leftImage.texture = texture;
            rightImage.texture = texture;
        }
    }
}
