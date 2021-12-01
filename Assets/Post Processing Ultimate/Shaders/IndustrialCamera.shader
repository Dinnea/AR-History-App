//{"Values":["0","NTEC/Screen/IndustrialCamera","_MainTex","0",""]}|{"position":{"serializedVersion":"2","x":0.0,"y":0.0,"width":212.0,"height":109.0},"name":"FloatSlider","selected":false,"Values":["Intensity","Effect intensity","0.5","0","1"],"serial":0,"unique":3352,"type":"FloatSliderField"}|{"position":{"serializedVersion":"2","x":0.0,"y":126.0,"width":212.0,"height":109.0},"name":"FloatSlider","selected":false,"Values":["Lines","Lines visibility","0.5","0","1"],"serial":1,"unique":-1,"type":"FloatSliderField"}|{"tempTextures":1,"passes":[{"position":{"serializedVersion":"2","x":0.0,"y":36.0,"width":212.0,"height":16.0},"InputLabels":["Game","tempRT0"],"OutputLabels":["Screen","tempRT0"],"PassLabels":["0"],"VariableLabels":["None"],"Input":0,"Output":0,"Pass":0,"Iterations":1,"Variable":0}],"passOptions":["0"],"inputOptions":["Game","tempRT0"],"outputOptions":["Screen","tempRT0"],"variableOptions":["None"]}
//\	CameraOutput\	3678.618\	310.8589\	192\	215\		False\			null\			null\		False\			null\			null\		False\			null\			null\		True\			21\			0\	CameraInput\	1119.858\	459.5732\	192\	335\		False\			null\			null\		False\			null\			null\		False\			null\			null\		True\			21\			1\		False\			null\			null\		False\			null\			null\		True\			2\			2\	StereoUV\	280.8084\	481.7156\	192\	175\		False\			null\			null\		True\			12\			1\		True\			1\			6\	Variable3\	1495.738\	344.6494\	192\	415\		/0\		/0\		True\			4\			1\		True\			4\			2\		True\			4\			3\		False\			null\			null\		False\			null\			null\		False\			null\			null\		False\			null\			null\		True\			1\			3\	Add\	1900.18\	455.7605\	192\	255\		/5\		True\			5\			1\		True\			3\			0\		True\			3\			1\		True\			3\			2\		False\			null\			null\	Div\	2204.626\	597.9827\	192\	215\		/4\		True\			17\			1\		True\			4\			0\		True\			6\			0\		False\			null\			null\	Value1\	1873.514\	802.4272\	192\	95\		/3.0\		True\			5\			2\	Mul\	2790.857\	751.9126\	192\	215\		/4\		True\			17\			2\		True\			5\			0\		True\			19\			0\		False\			null\			null\	Abs\	2023.833\	959.8884\	192\	135\		True\			15\			1\		True\			9\			0\	Sin\	1715.737\	989.8884\	192\	135\		True\			8\			1\		True\			10\			0\	Mul\	1272.404\	933.2217\	192\	215\		/4\		True\			9\			1\		True\			12\			0\		True\			11\			0\		False\			null\			null\	Value1\	877.6422\	1007.031\	192\	95\		/16.0\		True\			10\			2\	Add\	595.0233\	749.1731\	192\	215\		/4\		True\			10\			1\		True\			2\			1\		True\			13\			0\		False\			null\			null\	Time\	-24.77875\	854.4529\	192\	255\		True\			12\			2\		False\			null\			null\		False\			null\			null\		False\			null\			null\		False\			null\			null\	Floor\	2415.936\	1000.524\	192\	135\		True\			19\			1\		True\			15\			0\	Mul\	2129.547\	1177.567\	192\	215\		/4\		True\			19\			2\		True\			8\			0\		True\			16\			0\		False\			null\			null\	Value1\	1766.651\	1294.454\	192\	95\		/3.0\		True\			15\			2\	Lerp\	2732.365\	291.5957\	192\	215\		True\			21\			2\		True\			5\			0\		True\			7\			0\		True\			18\			0\	_FloatSlider\	2378.078\	477.3101\	192\	95\		/Lines\		/2\		/-1\		True\			17\			3\	Lerp\	2849.151\	1233.618\	192\	215\		True\			7\			2\		True\			14\			0\		True\			15\			0\		True\			20\			0\	Value1\	2444.151\	1363.618\	192\	95\		/0.5\		True\			19\			3\	Lerp\	3442.151\	733.6189\	192\	215\		True\			0\			3\		True\			1\			3\		True\			17\			0\		True\			22\			0\	_FloatSlider\	3176.151\	975.6189\	192\	95\		/Intensity\		/1\		/3352\		True\			21\			3

Shader "NTEC/Screen/IndustrialCamera" {

	SubShader {
		Cull Off ZWrite Off ZTest Always

		Pass {
			HLSLPROGRAM
			#pragma vertex VertDefault
			#pragma fragment Frag

			#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

			TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
			uniform half _Intensity;
			uniform half _Lines;

			half4 Frag (VaryingsDefault i) : SV_Target {
				half3 var0 = 0.0;
				half4 CameraOutput = 0.0;
				var0 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoordStereo).rgb;
				CameraOutput.rgb = lerp(SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoordStereo).rgb,lerp(((var0.x + var0.y + var0.z) / 3.0),(((var0.x + var0.y + var0.z) / 3.0) * lerp(floor((abs(sin(((i.texcoordStereo.y + _Time.x) * 16.0))) * 3.0)),(abs(sin(((i.texcoordStereo.y + _Time.x) * 16.0))) * 3.0),0.5)),_Lines),_Intensity);
				return CameraOutput;
			}
			ENDHLSL
		}
	}
}