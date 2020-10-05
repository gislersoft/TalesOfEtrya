using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ConvTest : MonoBehaviour {

	//_NeuralNetwork nn;
 //   float startTime, endTime;
 //   double[,,] test;
 //   string result1;
 //   // Use this for initialization
 //   void Start () {
 //       ApplicationSettings.SetTargetFrameRate();

 //       int h = 128;
 //       int w = 128;
 //       test = new double[h, w, 4];

 //       for (int i = 0; i < h; i++)
 //       {
 //           for (int j = 0; j < w; j++)
 //           {
 //               for (int c = 0; c < 4; c++)
 //               {
 //                   test[i, j, c] = Random.Range(0, 256);
 //               }
 //           }
 //       }

 //       nn = new _NeuralNetwork();
 //       nn.Init();
 //       //nn.Init();
 //       //startTime = Time.realtimeSinceStartup;
 //       //var input = ScreenInput.instance.GetInputTensor();
 //       //var result = nn.Predict(test);
 //       //endTime = Time.realtimeSinceStartup;
 //       //Debug.Log((endTime - startTime).ToString());
 //       //result1 = "";
 //       //for (int i = 0; i < result.GetLength(0); i++)
 //       //{
 //       //    for (int j = 0; j < result.GetLength(1); j++)
 //       //    {
 //       //        result1 += result[i, j] + " ";
 //       //    }
 //       //    result1 += "\n";
 //       //}
 //   }
	
	//// Update is called once per frame
	//void Update () {
 //       if(Input.touchCount > 0 || Input.GetKeyDown(KeyCode.A))
 //       {

 //           startTime = Time.realtimeSinceStartup;
 //           var input = ScreenInput.instance.GetInputTensor();
 //           var result = nn.Predict(input);
 //           endTime = Time.realtimeSinceStartup;
 //           //Debug.Log((endTime - startTime).ToString());
 //           //result1 = "";
 //           //for (int i = 0; i < result.GetLength(0); i++)
 //           //{
 //           //    for (int j = 0; j < result.GetLength(1); j++)
 //           //    {
 //           //        result1 += result[i, j] + " ";
 //           //    }
 //           //    result1 += "\n";
 //           //}
 //       }
	//}

 //   //private void OnGUI()
 //   //{
 //   //    GUI.Label(new Rect(0, 0, 100, 100), (endTime - startTime).ToString());
 //   //    GUI.Label(new Rect(0, 20, 300, 300), result1);
 //   //}

    public class Variable
    {
        public float Value;
        public List<Derivative> Children;
        public float GradValue;

        public Variable(float Value)
        {
            this.Value = Value;
            Children = new List<Derivative>();
            GradValue = float.NaN;
        }

        public float Grad()
        {
            if(float.IsNaN(GradValue))
            {
                GradValue = 0f;
                for (int i = 0; i < Children.Count; i++)
                {
                    GradValue += Children[i].Weight * Children[i].Variable.Grad();
                }
            }

            return GradValue;
        }

        public static Variable operator +(Variable var1, Variable var2)
        {
            if(var1 == null)
            {
                var1 = new Variable(0f);
            }
            if(var2 == null)
            {
                var2 = new Variable(0f);
            }

            Variable z = new Variable(var1.Value + var2.Value);
            var1.Children.Add(new Derivative(1f, z));
            var2.Children.Add(new Derivative(1f, z));

            return z;
        }

        public static Variable operator -(Variable var1, Variable var2)
        {
            if (var1 == null)
            {
                var1 = new Variable(0f);
            }
            if (var2 == null)
            {
                var2 = new Variable(0f);
            }

            Variable z = new Variable(var1.Value - var2.Value);
            var1.Children.Add(new Derivative(1f, z));
            var2.Children.Add(new Derivative(-1f, z));

            return z;
        }

        public static Variable operator -(Variable var1)
        {
            Variable z = new Variable(-var1.Value);
            var1.Children.Add(new Derivative(-1f, z));

            return z;
        }

        public static Variable operator *(Variable var1, Variable var2)
        {
            if (var1 == null)
            {
                var1 = new Variable(1f);
            }
            if (var2 == null)
            {
                var2 = new Variable(1f);
            }

            Variable z = new Variable(var1.Value * var2.Value);
            var1.Children.Add(new Derivative(var2.Value, z));
            var2.Children.Add(new Derivative(var1.Value, z));

            return z;
        }

        public static Variable operator /(Variable var1, Variable var2)
        {
            if (var1 == null)
            {
                var1 = new Variable(1f);
            }
            if (var2 == null)
            {
                var2 = new Variable(1f);
            }

            if(var2.Value == 0)
            {
                var2.Value = 1f;
            }

            Variable z = new Variable(var1.Value / var2.Value);

            var1.Children.Add(new Derivative(1 / var2.Value, z));
            var2.Children.Add(new Derivative(-var1.Value / (var2.Value * var2.Value), z));

            return z;
        }

        public struct Derivative
        {
            public float Weight;
            public Variable Variable;

            public Derivative(float Weight, Variable Variable)
            {
                this.Weight = Weight;
                this.Variable = Variable;
            }
        }

        public static Variable Sin(Variable x)
        {
            Variable z = new Variable(Mathf.Sin(x.Value));
            x.Children.Add(new Derivative(Mathf.Cos(x.Value), z));

            return z;
        }

        public static Variable Exp(Variable x)
        {
            Variable z = new Variable(Mathf.Exp(x.Value));
            x.Children.Add(new Derivative(Mathf.Exp(x.Value), z));

            return z;
        }

        public static Variable Log(Variable x)
        {
            if(x.Value > 0)
            {
                Variable z = new Variable(Mathf.Log(x.Value));
                x.Children.Add(new Derivative(1f / x.Value, z));

                return z;
            }
            else
            {
                Variable z = new Variable(float.NegativeInfinity);
                x.Children.Add(new Derivative(float.NegativeInfinity, z));

                return z;
            }
            
        }

        public static Variable ReLU(Variable x)
        {
            Variable z = new Variable((x.Value > 0) ? x.Value : 0f);

            if(x.Value > 0)
            {
                x.Children.Add(new Derivative(1f, z));
            }
            else
            {
                x.Children.Add(new Derivative(0f, z));
            }

            return z;
        }
    }

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Init();
            Variable k = new Variable(3);
            Variable h = new Variable(Mathf.PI) + Variable.Exp(k);
            Variable a = Variable.Sin(h);
            Variable b = new Variable(10);
            Variable z = Variable.Exp(a) - b;

            z.GradValue = 1f;
            var da = k.Grad();
            Debug.Log(da);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Variable[,] x1 = new Variable[batch_size, 2]
            {
                {
                    new Variable(0),
                    new Variable(0),
                },
                {
                    new Variable(1),
                    new Variable(0),
                },
                 {
                    new Variable(0),
                    new Variable(1),
                },
                  {
                    new Variable(1),
                    new Variable(1),
                }
            };
            Predict(x1, new Variable[batch_size] 
            {
                new Variable(0),
                new Variable(1),
                new Variable(1),
                new Variable(0),
            });
            UpdateNet();
        }
    }

    const int batch_size = 4;
    const int units_1 = 20;
    const int units_2 = 30;
    const int units_3 = 50;
    const int units_4 = 10;
    const int units_5 = 1;

    Variable[,] weights_1;
    Variable[,] weights_2;
    Variable[,] weights_3;
    Variable[,] weights_4;
    Variable[,] weights_5;
    Variable b_1;
    Variable b_2;
    Variable b_3;
    Variable b_4;
    Variable b_5;

    Variable[,] layer1;
    Variable[,] layer2;
    Variable[,] layer3;
    Variable[,] layer4;
    Variable[,] output;
    Variable loss;
    const float lr = 0.1f;

    private void Init()
    {

        weights_1 = new Variable[2, units_1];
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < units_1; j++)
            {
                weights_1[i, j] = new Variable(Random.Range(-1f, 1f));
            }
        }
        b_1 = new Variable(0f);

        weights_2 = new Variable[units_1, units_2];

        for (int i = 0; i < units_1; i++)
        {
            for (int j = 0; j < units_2; j++)
            {
                weights_2[i, j] = new Variable(Random.Range(-1f, 1f));
            }
        }

        b_2 = new Variable(0f);

        weights_3 = new Variable[units_2, units_3];
        for (int i = 0; i < units_2; i++)
        {
            for (int j = 0; j < units_3; j++)
            {
                weights_3[i, j] = new Variable(Random.Range(-1f, 1f));
            }
        }
        b_3 = new Variable(0f);

        weights_4 = new Variable[units_3, units_4];
        for (int i = 0; i < units_3; i++)
        {
            for (int j = 0; j < units_4; j++)
            {
                weights_4[i, j] = new Variable(Random.Range(-1f, 1f));
            }
        }
        b_4 = new Variable(0f);

        weights_5 = new Variable[units_4, units_5];
        for (int i = 0; i < units_4; i++)
        {
            for (int j = 0; j < units_5; j++)
            {
                weights_5[i, j] = new Variable(Random.Range(-1f, 1f));
            }
        }
        b_5 = new Variable(0f);

        layer1 = new Variable[batch_size, units_1];
        layer2 = new Variable[batch_size, units_2];
        layer3 = new Variable[batch_size, units_3];
        layer4 = new Variable[batch_size, units_4];
        output = new Variable[batch_size, units_5];

        loss = new Variable(0);
        loss.GradValue = 1;
    }

    private void Predict(Variable[,] x, Variable[] y)
    {
        #region L1

        for (int i = 0; i < x.GetLength(0); i++)
        {
            for (int j = 0; j < weights_1.GetLength(1); j++)
            {
                
                for (int k = 0; k < weights_1.GetLength(0); k++)
                {
                    layer1[i, j] = layer1[i, j] + x[i, k] * weights_1[k, j];
                }

                layer1[i, j] = layer1[i, j] + b_1;
            }
        }

        for (int i = 0; i < layer1.GetLength(0); i++)
        {
            for (int j = 0; j < layer1.GetLength(1); j++)
            {
                layer1[i, j] = Variable.ReLU(layer1[i, j]);
            }
        }

        #endregion

        #region L2
        

        for (int i = 0; i < layer1.GetLength(0); i++)
        {
            for (int j = 0; j < weights_2.GetLength(1); j++)
            {

                for (int k = 0; k < weights_2.GetLength(0); k++)
                {
                    layer2[i, j] = layer2[i, j] + layer1[i, k] * weights_2[k, j];
                }

                layer2[i, j] = layer2[i, j] + b_2;
            }
        }

        for (int i = 0; i < layer2.GetLength(0); i++)
        {
            for (int j = 0; j < layer2.GetLength(1); j++)
            {
                layer2[i, j] = Variable.ReLU(layer2[i, j]);
            }
        }
        #endregion

        #region L3


        for (int i = 0; i < layer2.GetLength(0); i++)
        {
            for (int j = 0; j < weights_3.GetLength(1); j++)
            {

                for (int k = 0; k < weights_3.GetLength(0); k++)
                {
                    layer3[i, j] = layer3[i, j] + layer2[i, k] * weights_3[k, j];
                }

                layer3[i, j] = layer3[i, j] + b_3;
            }
        }

        for (int i = 0; i < layer3.GetLength(0); i++)
        {
            for (int j = 0; j < layer3.GetLength(1); j++)
            {
                layer3[i, j] = Variable.ReLU(layer3[i, j]);
            }
        }
        #endregion

        #region L4


        for (int i = 0; i < layer3.GetLength(0); i++)
        {
            for (int j = 0; j < weights_4.GetLength(1); j++)
            {

                for (int k = 0; k < weights_4.GetLength(0); k++)
                {
                    layer4[i, j] = layer4[i, j] + layer3[i, k] * weights_4[k, j];
                }

                layer4[i, j] = layer4[i, j] + b_4;
            }
        }

        for (int i = 0; i < layer4.GetLength(0); i++)
        {
            for (int j = 0; j < layer4.GetLength(1); j++)
            {
                layer4[i, j] = Variable.ReLU(layer4[i, j]);
            }
        }
        #endregion

        #region output


        string results = "";
        for (int i = 0; i < layer4.GetLength(0); i++)
        {
            for (int j = 0; j < weights_5.GetLength(1); j++)
            {

                for (int k = 0; k < weights_5.GetLength(0); k++)
                {
                    output[i, j] = output[i, j] + layer4[i, k] * weights_5[k, j];
                }
                output[i, j] = output[i, j] + b_5;
                output[i, j] = new Variable(1f) / (new Variable(1) + Variable.Exp(-output[i, j]));
                results += output[i, j].Value.ToString() + ", ";
            }
        }

        loss = 
            (y[0] - output[0, 0]) * (y[0] - output[0, 0]) +
            (y[1] - output[1, 0]) * (y[1] - output[1, 0]) +
            (y[2] - output[2, 0]) * (y[2] - output[2, 0]) +
            (y[3] - output[3, 0]) * (y[3] - output[3, 0]);

        loss = loss / new Variable(4);
        Debug.Log("Prediction: " + results + " | Loss: " + loss.Value);

        //var result =
        //    (layer4[0, 0].Value * Mathf.Exp(-b_5.Value - weights_5[0, 0].Value * layer4[0, 0].Value)
        //    * ((1f / ((Mathf.Exp(-b_5.Value - weights_5[0, 0].Value * layer4[0, 0].Value)) + 1f)) - output[0, 0].Value)) /
        //    2f * ((Mathf.Exp(-b_5.Value - weights_5[0, 0].Value * layer4[0, 0].Value)) + 1f) * ((Mathf.Exp(-b_5.Value - weights_5[0, 0].Value * layer4[0, 0].Value)) + 1f);
        //Debug.Log(result);
        //loss.GradValue = 1f;
        //Debug.Log(weights_5[0, 0].Grad());
     #endregion
    }

    private void UpdateNet()
    {
        for (int i = 0; i < weights_5.GetLength(0); i++)
        {
            for (int j = 0; j < weights_5.GetLength(1); j++)
            {
                weights_5[i, j].Value -= lr * weights_5[i, j].Grad();
            }
        }

        b_5.Value -= lr * b_5.Grad();

        for (int i = 0; i < weights_4.GetLength(0); i++)
        {
            for (int j = 0; j < weights_4.GetLength(1); j++)
            {
                weights_4[i, j].Value -= lr * weights_4[i, j].Grad();
            }
        }

        b_4.Value -= lr * b_4.Grad();

        for (int i = 0; i < weights_3.GetLength(0); i++)
        {
            for (int j = 0; j < weights_3.GetLength(1); j++)
            {
                weights_3[i, j].Value -= lr * weights_3[i, j].Grad();
            }
        }

        b_3.Value -= lr * b_3.Grad();

        for (int i = 0; i < weights_2.GetLength(0); i++)
        {
            for (int j = 0; j < weights_2.GetLength(1); j++)
            {
                weights_2[i, j].Value -= lr * weights_2[i, j].Grad();
            }
        }
        b_2.Value -= lr * b_2.Grad();

        for (int i = 0; i < weights_1.GetLength(0); i++)
        {
            for (int j = 0; j < weights_1.GetLength(1); j++)
            {
                weights_1[i, j].Value -= lr * weights_1[i, j].Grad();
            }
        }

        b_1.Value -= lr * b_1.Grad();

        
    }

}
