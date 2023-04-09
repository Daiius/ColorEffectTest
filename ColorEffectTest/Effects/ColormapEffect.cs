using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ColorEffectTest.Effects
{
    /// <summary>
    /// Pixel shader effect, written with reference to
    /// http://www.kanazawa-net.ne.jp/~pmansato/wpf/wpf_graph_effect.htm
    /// </summary>
    public class ColormapEffect : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty
            = RegisterPixelShaderSamplerProperty("Input", typeof(ColormapEffect), 0);

        public static readonly DependencyProperty ThresholdProperty =
            DependencyProperty.Register("Threshold", typeof(double), typeof(ColormapEffect),
                new UIPropertyMetadata(1.0, PixelShaderConstantCallback(0)));

        public ColormapEffect()
        {
            var ps = new PixelShader();
            //var path = System.IO.Path.GetFullPath(@"Effects/ColormapEffect.ps");
            var asm = typeof(ColormapEffect).Assembly;
            var asmName = asm.GetName().Name;
            var uri = new Uri(
                "pack://application:,,,/" + asmName + ";component/Effects/ColormapEffect.ps",
                UriKind.RelativeOrAbsolute);
            ps.UriSource = uri;

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
