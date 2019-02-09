using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ColorEffectTest.Effects
{
    public class ColormapEffect : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty
            = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(ColormapEffect), 0);

        public ColormapEffect()
        {
            var ps = new PixelShader();
            var path = System.IO.Path.GetFullPath(@"Effects/ColormapEffect.ps");
            ps.UriSource = new Uri(path);

            this.PixelShader = ps;
            UpdateShaderValue(InputProperty);
        }

        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }
    }
}
