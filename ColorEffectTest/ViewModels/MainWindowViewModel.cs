using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace ColorEffectTest.ViewModels
{
    public enum ArrayTypes
    {
        LeftToRight = 0,
        RightToLeft = 1,
        Stripe = 2,
    }

    public class MainWindowViewModel
    {
        public ReactiveProperty<Effects.ArrayEffect> ArrayEffect { get; set; }
        public ReactiveProperty<WriteableBitmap> ArrayImage { get; set; }
        public ReactiveProperty<ArrayTypes> SelectedArrayType { get; set; }

        private List<byte[]> arrayPatterns;

        private const int ArraySize = 256;

        public MainWindowViewModel()
        {
            InitializeArrayPatterns();
            InitializeProperties();
        }

        private void InitializeArrayPatterns()
        {
            arrayPatterns = new List<byte[]>();

            var leftToRightPattern = new byte[ArraySize];
            for (int i = 0; i < ArraySize; i++) leftToRightPattern[i] = (byte)i;
            arrayPatterns.Add(leftToRightPattern);

            var rightToLeftPattern = new byte[ArraySize];
            for (int i = 0; i < ArraySize; i++) rightToLeftPattern[i] = (byte)(ArraySize - i - 1);
            arrayPatterns.Add(rightToLeftPattern);

            var stripePattern = new byte[ArraySize];
            for (int i = 0; i < ArraySize; i++) stripePattern[i] = (byte)(255 * (0.5 + 0.5*Math.Cos(Math.PI / 25.5 * i)));
            arrayPatterns.Add(stripePattern);
        }

        private void InitializeProperties()
        {
            ArrayImage = new ReactiveProperty<WriteableBitmap>(
                new WriteableBitmap(255, 1, 96.0, 96.0, PixelFormats.Gray8, null)
                );

            var arrayEffect = new Effects.ArrayEffect();
            SetArrayData(arrayEffect , ArrayTypes.LeftToRight);
            ArrayEffect = new ReactiveProperty<Effects.ArrayEffect>(arrayEffect);
            SelectedArrayType = new ReactiveProperty<ArrayTypes>(ArrayTypes.LeftToRight);
            SelectedArrayType.Subscribe(v => SetArrayData(ArrayEffect.Value, v)) ;
            
        }

        private void SetArrayData(Effects.ArrayEffect effect, ArrayTypes type)
        {
            var data = new WriteableBitmap(255, 1, 96.0, 96.0, PixelFormats.Gray8, null);
            data.WritePixels(
                new System.Windows.Int32Rect(0, 0, data.PixelWidth, data.PixelHeight),
                arrayPatterns[(int)type], data.PixelWidth * data.Format.BitsPerPixel, 0
                );
            effect.Array = new ImageBrush(data);

            ArrayImage.Value.WritePixels(
                new System.Windows.Int32Rect(0, 0, data.PixelWidth, data.PixelHeight),
                arrayPatterns[(int)type], data.PixelWidth * data.Format.BitsPerPixel, 0
                );
        }

    }
}
