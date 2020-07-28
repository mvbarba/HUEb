using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants 
{	
	// The integer value is equal to the layer mask
	// this color corresponds to
	public enum Color
	{
		None,
		White,
		Blue,
		Green,
		Red,
		LevelSelect
	}

	public static UnityEngine.Color GetColor(Color color) {
		Dictionary<Color, UnityEngine.Color> colorDict = new Dictionary<Color, UnityEngine.Color>();
		
		colorDict.Add(Color.Blue, new UnityEngine.Color((float)21 / 255, (float)101 / 255, (float)192 / 255));
		colorDict.Add(Color.Red, new UnityEngine.Color(1, 0, 0));
		colorDict.Add(Color.Green, new UnityEngine.Color(0, (float)120/255, (float)20/255));
		colorDict.Add(Color.White, new UnityEngine.Color(1, 1, 1));
		colorDict.Add(Color.LevelSelect, new UnityEngine.Color((float)240 / 255, (float)237 / 255, (float)228 / 255));

		UnityEngine.Color returnColor;
		if (colorDict.TryGetValue(color, out returnColor)) {
			return returnColor;
		} else {
			// Return a gross purple if no color found
			return new UnityEngine.Color(1, 0, 1);
		}
	}

	public static LayerMask GetLayerMask(Color color) {
		switch (color) {
			default: {
				return 0;
			}
			case Color.Red: {
				return 14;
			}
			case Color.Blue: {
				return 12;
			}
			case Color.Green: {
				return 13;
			}
		}
	}
}
