using UnityEngine;
/// <summary>
/// 
/// <para>This class implementes 3-layered MobileNet, using Depthwise separable convolution.</para>
/// <para>It gets as input a MxNx4 tensor, and outputs the probability of selecting an action as well as the value funtion.</para>
/// <para>It is used in the A3C algorithm proposed in: Asynchronous methods for deep reinforcement learning by DeepMind. https://arxiv.org/abs/1602.01783 </para>
/// 
/// </summary>
[CreateAssetMenu(fileName ="Neural network", menuName ="NN/Mobile net")]
public class _NeuralNetwork : ScriptableObject{
    /*
     * Convolutional layer params
     *  Params:
     *      0 - Kernel height
     *      1 - Kernel width
     *      2 - Input dimensionality
     *      3 - Output dimensionality
     *      4 - Stride
     */
    readonly int[] W_conv1_size = new int[5] { 8, 8, 4, 16, 4};
    readonly int[] W_conv2_size = new int[5] { 4, 4, 16, 32, 2 };
    readonly int[] W_conv3_size = new int[5] { 3, 3, 64, 64, 1 };

    /*
     * Fully-connected layer params
     *  Params:
     *      0 - Input dimensionality
     *      1 - Output dimensionality
     */
    readonly int[] W_fc1_size = new int[2] { 6272, 256 };
    readonly int[] W_fc2_size = new int[2] { 256, 3 };
    //31x31x16
    //14x14x32
    /*
     * Depth wise blocks
     */

    double[,,] W_DW_conv1;
    double[,,,] PW_conv1;

    double[,,] W_DW_conv2;
    double[,,,] PW_conv2;

    double[,,] W_DW_conv3;
    double[,,,] PW_conv3;

    /*
     * Fully-connected layers
     */
    double[,] W_fc_1;
    double[,] W_fc_2;
    public double Value_funtion { get; set; }

    double gradients;
    public bool isInit = false;

    /// <summary>
    /// Initializes the Neural Network given the inputs above
    /// <para>
    ///     The DW convolution blocks are separated into 2 parts
    /// </para>
    /// <para>
    ///     DW Convolution: Given an input tensor of shape HxWxM (3D)
    ///     The DW convolution layer creates C kernels of shape KxK, resulting in a KxKxM tensor (3D)
    ///     It outputs a H'xW'xM tensor (3D)
    /// </para>
    /// <para>
    ///     Pointwise convolution: Given the output of its corresponding DW layer
    ///     The PW convolution creates N 1x1 Kernels with depth M, resulting in a 1x1xMxN Tensor (4D)
    ///     It outputs a H'xW'xN tensor (3D)
    /// </para>
    /// </summary>

    public void Init()
    {
        W_DW_conv1 = new double[W_conv1_size[0], W_conv1_size[1], W_conv1_size[2]];
        PW_conv1 = new double[1, 1, W_conv1_size[2], W_conv1_size[3]];

        W_DW_conv2 = new double[W_conv2_size[0], W_conv2_size[1], W_conv2_size[2]];
        PW_conv2 = new double[1, 1, W_conv2_size[2], W_conv2_size[3]];

        W_DW_conv3 = new double[W_conv3_size[0], W_conv3_size[1], W_conv3_size[2]];
        PW_conv3 = new double[1, 1, W_conv3_size[2], W_conv3_size[3]];

        W_fc_1 = new double[W_fc1_size[0], W_fc1_size[1]];
        W_fc_2 = new double[W_fc2_size[0], W_fc2_size[1]];
        
        NN_Utils.RandomInit(ref W_DW_conv1);
        NN_Utils.RandomInit(ref W_DW_conv2);
        NN_Utils.RandomInit(ref W_DW_conv3);

        NN_Utils.RandomInit(ref PW_conv1);
        NN_Utils.RandomInit(ref PW_conv2);
        NN_Utils.RandomInit(ref PW_conv3);

        NN_Utils.RandomInit(ref W_fc_1);
        NN_Utils.RandomInit(ref W_fc_2);

        gradients = 0;

        isInit = true;
    }

    public void Init(_NeuralNetwork targetNet)
    {
        W_DW_conv1 = (double[,,])targetNet.W_DW_conv1.Clone();
        PW_conv1 = (double[,,,])targetNet.PW_conv1.Clone();

        W_DW_conv2 = (double[,,])targetNet.W_DW_conv2.Clone();
        PW_conv2 = (double[,,,])targetNet.PW_conv2.Clone();

        W_DW_conv3 = (double[,,])targetNet.W_DW_conv2.Clone();
        PW_conv3 = (double[,,,])targetNet.PW_conv2.Clone();

        W_fc_1 = (double[,])targetNet.W_fc_1.Clone();
        W_fc_2 = (double[,])targetNet.W_fc_2.Clone();
    }

    /// <summary>
    ///     Computes the probability of choosing an action given a stack of frames and the value function
    /// </summary>
    /// <param name="inputTensor">
    ///     Input tensor of shape HxWxM
    /// </param>

    public double[,] Predict(double[,,] inputTensor)
    {
        double[,,] dw_h_conv1 = NN_Utils.DepthWiseConv(inputTensor, W_DW_conv1, W_conv1_size[4]);
        double[,,] h_conv1 = NN_Utils.PointWiseConv(dw_h_conv1, PW_conv1);
        NN_Utils.ReLU(ref h_conv1);

        double[,,] dw_h_conv2 = NN_Utils.DepthWiseConv(h_conv1, W_DW_conv2, W_conv2_size[4]);
        double[,,] h_conv2 = NN_Utils.PointWiseConv(dw_h_conv2, PW_conv2);
        NN_Utils.ReLU(ref h_conv2);
        
        double[,] h_conv3_flat = NN_Utils.Flatten(h_conv2);
        
        double[,] h_fc1 = new double[1, W_fc1_size[1]];
        double[,] action_output = new double[1, W_fc2_size[1]];

        NN_Utils.GEMM(h_conv3_flat, W_fc_1, ref h_fc1);
        NN_Utils.ReLU(ref h_fc1);
        NN_Utils.GEMM(h_fc1, W_fc_2, ref action_output);
        NN_Utils.Softmax(ref action_output);

        Value_funtion = 0d;
        for (int i = 0; i < h_fc1.GetLength(0); i++)
        {
            for (int j = 0; j < h_fc1.GetLength(1); j++)
            {
                Value_funtion += h_fc1[i, j];
            }
        }

        return action_output;
    }   
}


 