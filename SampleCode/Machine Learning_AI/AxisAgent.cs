/* Script made by: Ernesto Hernandez 
		Portfolio http://ernestohh.weebly.com/
												*/


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisAgent : Agent {

	[SerializeField]
	private GameObject cube;
	[SerializeField]
	private GameObject sphere;
	[SerializeField]
	private Text text;
	[SerializeField]
	private int solved;
	[SerializeField]
	private int failed;
	[SerializeField]
	private int range;

	public override List<float> CollectState()
	{
		List<float> state = new List<float>();
		state.Add(cube.transform.position.x);
		state.Add(cube.transform.position.z);
		state.Add(cube.transform.position.y);
		state.Add(sphere.transform.position.x);
		state.Add(sphere.transform.position.z);
		state.Add(sphere.transform.position.y);
		return state;
	}

	public override void AgentStep(float[] action)
	{
		if (text != null)
		{
			text.text = string.Format("C:{0} / T:{1} S:[{2}] F:[{3}]", cube.transform.position, sphere.transform.position, solved, failed);
		}

		switch ((int)action[0])
		{
			//Left
			case 0:
				cube.transform.position -= transform.right * (Time.deltaTime * range);
				break;
			//Right
			case 1:
				cube.transform.position += transform.right * (Time.deltaTime * range);
				break;
			//Forward
			case 2:
				cube.transform.position += transform.forward * (Time.deltaTime * range);
				break;
			//Backward
			case 3:
				cube.transform.position -= transform.forward * (Time.deltaTime * range);
				break;
			//Up
			case 4:
				cube.transform.position += transform.up * (Time.deltaTime * range);
				break;
			//Down
			case 5:
				cube.transform.position -= transform.up * (Time.deltaTime * range);
				break;

			default:
				return;
		}

		if(cube.transform.position.x >= (range +0.5f) || cube.transform.position.x <= -(range + 0.5f) 
			|| cube.transform.position.z >= (range + 0.5f) || cube.transform.position.z <= -(range + 0.5f)
			|| cube.transform.position.y >= (range + 0.5f) || cube.transform.position.y <= -(range + 0.5f))
		{
			failed++;
			reward = -1f;
			done = true;
			return;
		}

		float difference = Vector3.Distance(cube.transform.position, sphere.transform.position);
		if (difference <= 1f)
		{
			solved++;
			reward = 1f;
			done = true;
			return;
		}
	}

	public override void AgentReset()
	{
		sphere.transform.position = new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
		cube.transform.position = Vector3.zero;
	}
}
