Shader "Custom/CrackShader"
{
	Properties
	{
		[HDR]_BColor("Bright Color", color) = (1,1,1,1)
		_DColor("Dark Color", color) = (0,0,0,0)
		_Noise("Noise", 2D) = "white" {}
		_Speed("Speed", Range(-3,3)) = 0
	}
		SubShader
		{
			Tags{ "Queue" = "Geometry+2" }
			Pass
			{
				Cull Front
				ZWrite On
				ZTest Greater

				stencil
				{
					Ref 1
					Comp Equal
				}

				CGPROGRAM

				#include "UnityCG.cginc"
				#pragma vertex vert
				#pragma fragment frag

				float4 _BColor;
				float4 _DColor;
				sampler2D _Noise;
				float _Speed;

				struct a2v {
					float4 vertex : POSITION;
					float4 texcoord : TEXCOORD0;
				};

				struct v2f {
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				v2f vert(a2v v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.texcoord.xy;

					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					half4 noise = tex2D(_Noise, half2(i.uv.x, i.uv.y + _Time.y * _Speed));
					half4 col = lerp(_DColor, _BColor, noise.r * (1 - i.uv.y));

					return col;
				}


				ENDCG
		}



			//Pass
			//{
			//	//Después de los tags del pase del shader - -> zwrite en off, cull en back, blend 1 - source destination alpha
			//	ZWrite Off
			//	Cull Back
			//	Blend One OneMinusSrcColor
			//}
	}
}