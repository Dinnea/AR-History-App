//{"Values":["0","NTEC/Screen/Blur","_MainTex","0",""]}|{"position":{"serializedVersion":"2","x":0.0,"y":0.0,"width":212.0,"height":109.0},"name":"IntSlider","selected":false,"Values":["Iterations","Effect Iterations","4","1","16"],"serial":0,"unique":3980,"type":"IntSliderField"}|{"position":{"serializedVersion":"2","x":0.0,"y":126.0,"width":212.0,"height":109.0},"name":"FloatSlider","selected":false,"Values":["Intensity","Effect Intensity","0.5","0","1"],"serial":1,"unique":-1,"type":"FloatSliderField"}|{"tempTextures":0,"passes":[{"position":{"serializedVersion":"2","x":0.0,"y":36.0,"width":212.0,"height":16.0},"InputLabels":["Game"],"OutputLabels":["Screen"],"PassLabels":["0"],"VariableLabels":["None","Iterations"],"Input":0,"Output":0,"Pass":0,"Iterations":0,"Variable":1}],"passOptions":["0"],"inputOptions":["Game"],"outputOptions":["Screen"],"variableOptions":["None","Iterations"]}
//\	CameraOutput\	3654.05\	600.511\	192\	215\		False\			null\			null\		False\			null\			null\		False\			null\			null\		True\			3\			0\	StereoUV\	1905.278\	622.3419\	192\	175\		False\			null\			null\		False\			null\			null\		True\			null\			null\	CameraInput\	2682.507\	585.5801\	192\	335\		False\			null\			null\		False\			null\			null\		False\			null\			null\		True\			null\			null\		False\			null\			null\		False\			null\			null\		True\			11\			0\	Div\	3319.546\	829.6438\	192\	215\		/4\		True\			null\			null\		True\			5\			3\		True\			6\			0\		False\			null\			null\	Iterator\	1102.192\	1041.35\	192\	95\		True\			null\			null\	VarLoop3\	2995.269\	662.66\	192\	415\		/0\		/0\		/0\		False\			null\			null\		False\			null\			null\		False\			null\			null\		True\			null\			null\		True\			2\			3\		True\			6\			0\		False\			null\			null\	Value1\	1947.349\	1161.39\	192\	95\		/90.0\		True\			null\			null\	Custom2\	2395.686\	876.985\	192\	335\		/Assets/Post Processing Ultimate/Functions/Append2.hlsl\		/Append2\		/1\		/1,1\		/Y,X\		False\			null\			null\		False\			null\			null\		True\			null\			null\		True\			15\			0\		True\			16\			0\	Div\	774.7306\	453.8898\	192\	215\		/4\		True\			null\			null\		True\			21\			0\		True\			9\			0\		False\			null\			null\	Value1\	815.3268\	1180.319\	192\	95\		/3.0\		True\			null\			null\	Floor\	1156.874\	497.6992\	192\	135\		True\			null\			null\		True\			8\			0\	Add\	2389.452\	540.755\	192\	215\		/4\		True\			null\			null\		True\			1\			2\		True\			19\			0\		False\			null\			null\	Value1\	1120.446\	829.963\	192\	95\		/-0.003\		True\			null\			null\	Mod\	1395.804\	1335.437\	192\	215\		/4\		True\			null\			null\		True\			21\			0\		True\			9\			0\		False\			null\			null\	Value1\	1198.342\	1763.891\	192\	95\		/0.003\		True\			null\			null\	Add\	2070.926\	930.08\	192\	215\		/4\		True\			null\			null\		True\			23\			0\		True\			17\			0\		False\			null\			null\	Add\	2247.709\	1285.637\	192\	215\		/4\		True\			null\			null\		True\			23\			0\		True\			18\			0\		False\			null\			null\	Mul\	1627.883\	515.4926\	192\	215\		/4\		True\			null\			null\		True\			10\			0\		True\			24\			0\		False\			null\			null\	Mul\	1909.111\	1284.026\	192\	215\		/4\		True\			null\			null\		True\			26\			0\		True\			24\			0\		False\			null\			null\	Mul\	2795.539\	1149.603\	192\	215\		/4\		True\			null\			null\		True\			20\			0\		True\			7\			2\		False\			null\			null\	_FloatSlider\	2514.473\	1355.317\	192\	95\		/Intensity\		/1\		/-1\		True\			null\			null\	Mod\	917.9198\	1311.985\	192\	215\		/4\		True\			null\			null\		True\			4\			0\		True\			22\			0\		False\			null\			null\	Value1\	633.8016\	1628.135\	192\	95\		/9.0\		True\			null\			null\	Mul\	1629.168\	884.818\	192\	215\		/4\		True\			null\			null\		True\			12\			0\		True\			25\			0\		False\			null\			null\	Mul\	1643.811\	1499.46\	192\	215\		/4\		True\			null\			null\		True\			14\			0\		True\			25\			0\		False\			null\			null\	Div\	1138.691\	1497.436\	192\	215\		/4\		True\			null\			null\		True\			4\			0\		True\			22\			0\		False\			null\			null\	Floor\	1522.38\	1188.334\	192\	135\		True\			null\			null\		True\			13\			0

Shader "NTEC/Screen/Blur" {

	SubShader {
		Cull Off ZWrite Off ZTest Always

		Pass {
			HLSLPROGRAM
			#pragma vertex VertDefault
			#pragma fragment Frag

			#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
			#include "../../Post Processing Ultimate/Functions/Append2.hlsl"

			TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
			uniform half _Iterations;
			uniform half _Intensity;

			half4 Frag (VaryingsDefault i) : SV_Target {
				half IteratorVariable = 0.0;
				half3 var0 = 0.0;
				half4 CameraOutput = 0.0;
				for (IteratorVariable = 0.0; IteratorVariable < (0.0 + 90.0); ++IteratorVariable){ var0 += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, (i.texcoordStereo + (_Intensity * Append2(((-0.003 * (IteratorVariable / 9.0)) + (floor(((IteratorVariable % 9.0) / 3.0)) * (0.003 * (IteratorVariable / 9.0)))), ((-0.003 * (IteratorVariable / 9.0)) + (floor(((IteratorVariable % 9.0) % 3.0)) * (0.003 * (IteratorVariable / 9.0)))))))).rgb; }
				CameraOutput.rgb = (var0 / 90.0);
				return CameraOutput;
			}
			ENDHLSL
		}
	}
}