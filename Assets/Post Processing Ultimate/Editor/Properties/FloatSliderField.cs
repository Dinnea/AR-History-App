using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace NTEC.PPU{
	public class FloatSliderField : VisualProperty{
		
		private Texture2D nodeBack;
		private Texture2D nodeBlack;
		
		public FloatSliderField(){
			position = new Rect(0, 0, 212, 109);
			type = "FloatSliderField";
			name = "FloatSlider";
			Values.Add("FloatSlider_Name");
			Values.Add("Tooltip");
			Values.Add("0");
			Values.Add("0");
			Values.Add("1");
			sele = new Color(0.25f, 0.25f, 1, 0.5f);
		}
		
		public override void Show(){
			base.Show();
			if (unique == 0){
				unique = Random.Range(1, 4096);
			}
			if (nodeBack == null){
				nodeBack = Resources.Load("Nodes/Property") as Texture2D;
				nodeBlack = Resources.Load("Nodes/PropertyBack") as Texture2D;
			}
			EditorGUI.DrawRect(new Rect(position.x, position.y + 10, position.width, position.height - 10), sele);
			GUI.DrawTexture(new Rect(position.x, position.y + 10, position.width, position.height - 10), nodeBlack);
			GUI.DrawTexture(new Rect(position.x, position.y, position.width, 20), nodeBack);
			EditorGUI.LabelField(new Rect(position.x, position.y, position.width, 20), new GUIContent(name, "Float slider property"), style[0]);
			Values[0] = EditorGUI.TextField(new Rect(position.x + 2, (position.y + 1 * (20) + 2), position.width - 4, 16), Values[0], style[3]).Replace(" ", "");
			Values[1] = EditorGUI.TextField(new Rect(position.x + 2, (position.y + 2 * (20) + 2), position.width - 4, 16), Values[1], style[3]);
			Values[2] = GUI.HorizontalSlider(new Rect(position.x + 2, (position.y + 3 * (20) + 2), 124, 16), float.Parse(Values[2], CultureInfo.InvariantCulture), float.Parse(Values[3], CultureInfo.InvariantCulture), float.Parse(Values[4], CultureInfo.InvariantCulture)).ToString();
			Values[2] = EditorGUI.FloatField(new Rect(position.x + 132, (position.y + 3 * (20) + 2), 78, 16), float.Parse(Values[2], CultureInfo.InvariantCulture), style[3]).ToString();
			Values[3] = EditorGUI.FloatField(new Rect(position.x + 2, (position.y + 4 * (20) + 2), position.width / 2 - 6, 16), float.Parse(Values[3], CultureInfo.InvariantCulture), style[3]).ToString();
			Values[4] = EditorGUI.FloatField(new Rect(position.width / 2 + 4, (position.y + 4 * (20) + 2), position.width / 2 - 6, 16), float.Parse(Values[4], CultureInfo.InvariantCulture), style[3]).ToString();
		}
	}
}