using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ColorEffectTest.Effects
{
    public class ArrayEffect : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty
            = RegisterPixelShaderSamplerProperty("Input", typeof(ColormapEffect), 0);


        public static readonly DependencyProperty ArrayProperty =
            DependencyProperty.Register("Array", typeof(float[]), typeof(ColormapEffect),
                new UIPropertyMetadata(0.0, PixelShaderConstantCallback(1)));

        public ArrayEffect()
        {
            var ps = new PixelShader();
            //var path = System.IO.Path.GetFullPath(@"Effects/ColormapEffect.ps");
            var asm = typeof(ColormapEffect).Assembly;
            var asmName = asm.GetName().Name;
            var uri = new Uri(
                "pack://application:,,,/" + asmName + ";component/Effects/ArrayEffect.ps",
                UriKind.RelativeOrAbsolute);
            ps.UriSource = uri;

            this.PixelShader = ps;
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(ArrayProperty);
        }

        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }


        public float[] Array
        {
            get { return (float[])GetValue(ArrayProperty); }
            set { SetValue(ArrayProperty, value); }
        }
    }
}
