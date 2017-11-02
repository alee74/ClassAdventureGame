using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarStats {

	public HealthBar bar;

	public float maxVal;
	public float currentVal;

	public float currVal {
		get { 
			return currentVal;
		}

		set { 
			this.currentVal = value;
			bar.Value = currentVal;
		}
	}


	public float MaxVal {
		get { 
			return maxVal;
		}

		set { 
			this.maxVal = value;
			bar.Maxvalue = maxVal;
		}
	}

}