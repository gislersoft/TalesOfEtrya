using UnityEngine;

public abstract class NN_Layer : ScriptableObject {

	public _Tensor[] inputs;
	public _Tensor[] results;
	public float bias;
	public ActivationFunction activationFunction;
	public int units;

	public int Length { get; protected set;}
	///<summary>
	/// 	Initializes the layer
	/// </summary>
	public abstract void InitLayer (_Tensor[] inputs);

	public abstract _Tensor[] ForwardPass(_Tensor[] inputs);

//    public int units; //dimensionality of the output space
//    public ActivationFunction activationFunction; 
//
//    public abstract Tensor ForwardPass(Tensor input);
//    public abstract Tensor[] ForwardPass(Tensor[] inputs);
//    public abstract void BackwardPass();
//	  public abstract void InitLayer();
//    public abstract void InitLayer(params int[] dimensions);
//    public abstract void InitLayer(Tensor initTensor);
}
