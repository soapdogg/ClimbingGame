using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DisplayDepth : MonoBehaviour {
	
	public DepthWrapper dw;
	
	private Texture2D tex;
	// Use this for initialization
	void Start () {
		tex = new Texture2D(320,240,TextureFormat.ARGB32,false);
		GetComponent<Renderer>().material.mainTexture = tex;
	}
	
	// Update is called once per frame
	void Update () {
		if (dw.pollDepth())
		{
			tex.SetPixels32(convertDepthToColor(dw.depthImg));
			//tex.SetPixels32(convertPlayersToCutout(dw.segmentations));
			tex.Apply(false);
		}
	}
	
	private Color32[] convertDepthToColor(short[] depthBuf)
	{
		Color32[] img = new Color32[depthBuf.Length];
		for (int pix = 0; pix < depthBuf.Length; pix++)
		{
			img[pix].r = (byte)(depthBuf[pix] / 32);
			img[pix].g = (byte)(depthBuf[pix] / 32);
			img[pix].b = (byte)(depthBuf[pix] / 32);
		}
		return img;
	}
}
