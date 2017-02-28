using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HighScore : IComparable<HighScore>
{
	public string name;
	public float score;

	public HighScore (string name, float score)
	{
		this.name = name;
		this.score = score;
	}

	public HighScore (string formattedHighScore)
	{
		string[] lineSplit = formattedHighScore.Split (',');
		this.name = lineSplit [0];
		this.score = float.Parse (lineSplit [1]);
	}

	public override string ToString ()
	{
		return string.Format ("{0}, {1}", this.name, this.score.ToString ());
	}

	public override bool Equals (object obj)
	{
		if (obj == null)
			return false;
	
		HighScore hs = obj as HighScore;
		if ((System.Object)hs == null)
			return false;
		return Equals (hs);
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}

	public bool Equals (HighScore other)
	{
		return name.Equals (other.name) && score == other.score;
	}

	public int CompareTo (HighScore other)
	{
		if (this.score == other.score) {
			return this.name.CompareTo (other.name);
		}
		return other.score.CompareTo (this.score);
	}
}
