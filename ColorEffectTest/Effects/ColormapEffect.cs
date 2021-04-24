using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ColorEffectTest.Effects
{
    public class ColormapEffect : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty
            = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(ColormapEffect), 0);

        public static readonly DependencyProperty ThresholdProperty =
            DependencyProperty.Register("Threshold", typeof(double), typeof(ColormapEffect),
                new UIPropertyMetadata(1.0, PixelShaderConstantCallback(0)));

        public ColormapEffect()
        {
            var ps = new PixelShader();
            var path = System.IO.Path.GetFullPath(@"Effects/ColormapEffect.ps");
            ps.UriSource = new Uri(path);

            this.PixelShader = ps;
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(ThresholdProperty);
        }

        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public double Threshold
        {
            get { return (double)GetValue(ThresholdProperty); }
            set { SetValue(ThresholdProperty, value); }
        }
    }
}
