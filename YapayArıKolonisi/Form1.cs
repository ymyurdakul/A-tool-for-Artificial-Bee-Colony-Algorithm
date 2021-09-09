using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace YapayArıKolonisi
{
    public partial class Form1 : Form
    {
        int[] filterCounts = new int[] { 16, 32, 64, 128, 256, 512,124,45,699,988,1021,58,69,96,63, 16, 32, 64, 128, 256, 512, 124, 45, 699, 988, 1021, 58, 69, 96, 63 };
        int[] kernelSizes = new int[] { 1, 3, 5, 7, 9,11,13,15,17,19,18,20,22,23,58,96,97,87,112,522, 16, 32, 64, 128, 256, 512, 124, 45, 699, 988, 1021, 58, 69, 96, 63 };
        List<FoodSource> bestS = new List<FoodSource>();
        int cs = 0;
        int itercount = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {



           
        }
        public void init()
        {
            Random a = new Random();
            //16,32,64,128,256,512
            List<FoodSource> foodSources = new List<FoodSource>();


            for (int i = 0; i < cs; i++)
            {
                int fc = a.Next(0, filterCounts.Length);
                int cs = a.Next(0, kernelSizes.Length);
                foodSources.Add(new FoodSource(filterCounts[fc], kernelSizes[cs], 0));
            }

            for (int i = 0; i < itercount; i++)
            {
                e1(foodSources);
                e2(foodSources);
                e3(foodSources);
            }


        }
        public void e1(List<FoodSource> foodSources)
        {
            Random a = new Random();
            foodSources.ForEach(x => x.calcFitness());
            // Console.WriteLine("---------   İnit -------------");
            for (int i = 0; i < foodSources.Count; i++)
            {
                //foodSources[i].printFoodSource();
            }
            // Console.WriteLine("---------   Employee Bee -------------");
            for (int i = 0; i < foodSources.Count; i++)
            {
                int updatedIndex = a.Next(0, foodSources.Count);
                int partner = a.Next(0, foodSources.Count);
                int updatePosition = a.Next(0, 2);

                //    Console.WriteLine("******************************************************************************************");
                //    Console.WriteLine(" Updated Index : " + updatedIndex + " Partner Index " + partner + " Pos: " + updatePosition);

                FoodSource temp = new FoodSource();
                temp.trial = 0;
                if (updatePosition == 0)
                {
                    temp.x1 = foodSources[partner].x1;
                    temp.x2 = foodSources[updatedIndex].x2;
                }

                if (updatePosition == 1)
                {
                    temp.x2 = foodSources[partner].x2;
                    temp.x1 = foodSources[updatedIndex].x1;
                }
                temp.calcFitness();
                //  Console.WriteLine("Off Spring");
                // temp.printFoodSource();
                // Console.WriteLine("Eski fitness" + foodSources[updatedIndex].fitness + " New Fitness" + temp.fitness);

                if (temp.fitness < foodSources[updatedIndex].fitness)
                {
                    // Console.WriteLine("Güncellendi");
                    foodSources[updatedIndex] = temp;
                }
                else
                {
                    // foodSources[updatedIndex].trial += 1;
                    //  Console.WriteLine("Hatası Arttı");
                }

                //   Console.WriteLine("******************************************************************************************");



            }
            // Console.WriteLine("İşçi Arı Sonrası");
            // foodSources.ForEach(x => x.printFoodSource());

        }
        public void e2(List<FoodSource> foodSources)
        {
            Random a = new Random();
            //  Console.WriteLine("Gözcü Arı");
            double sum = 0;
            foodSources.ForEach(x => sum += x.fitness);
            //  Console.WriteLine("Sum: " + sum);
            double[] probs = new double[foodSources.Count];

            for (int i = 0; i < foodSources.Count; i++)
            {
                probs[i] = foodSources[i].fitness / sum;
                //  Console.WriteLine(probs[i]);
            }
            for (int i = 0; i < foodSources.Count; i++)
            {
                double r = a.NextDouble();
                //      Console.WriteLine(r);
                if (r < probs[i])
                {
                    int updatedIndex = a.Next(0, foodSources.Count);
                    int partner = a.Next(0, foodSources.Count);
                    int updatePosition = a.Next(0, 2);

                    //      Console.WriteLine("******************************************************************************************");
                    //      Console.WriteLine(" Updated Index : " + updatedIndex + " Partner Index " + partner + " Pos: " + updatePosition);

                    FoodSource temp = new FoodSource();
                    temp.trial = 0;
                    if (updatePosition == 0)
                    {
                        temp.x1 = foodSources[partner].x1;
                        temp.x2 = foodSources[updatedIndex].x2;
                    }

                    if (updatePosition == 1)
                    {
                        temp.x2 = foodSources[partner].x2;
                        temp.x1 = foodSources[updatedIndex].x1;
                    }
                    temp.calcFitness();
                    // Console.WriteLine("Off Spring");
                    // temp.printFoodSource();
                    // Console.WriteLine("Eski fitness" + foodSources[updatedIndex].fitness + " New Fitness" + temp.fitness);

                    if (temp.fitness < foodSources[updatedIndex].fitness)
                    {
                        //  Console.WriteLine("Güncellendi");
                        foodSources[updatedIndex] = temp;
                    }
                    else
                    {
                        foodSources[updatedIndex].trial += 1;
                        //  Console.WriteLine("Hatası Arttı");
                    }

                    //   Console.WriteLine("******************************************************************************************");

                }
            }

            double fitness = foodSources[0].fitness;
            FoodSource kralice = foodSources[0];
            for (int i = 0; i < foodSources.Count; i++)
            {
                if (foodSources[i].fitness < kralice.fitness)
                {
                    kralice = foodSources[i];
                }
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("Best");
            kralice.printFoodSource();
            bestS.Add(kralice);
            Console.WriteLine("-----------------------------------------------------------------------------------------");
           /* TextBox text = new TextBox();
            text.Text = kralice.getAsString();
            text.Dock = DockStyle.Top;
            text.Parent = panel1;
            panel1.Controls.Add(text);*/
        }
        public void e3(List<FoodSource> foodSources)
        {
            Random a = new Random();
            for (int i = 0; i < foodSources.Count; i++)
            {
                if (foodSources[i].trial > 1)
                {
                    int fc = a.Next(0, filterCounts.Length - 1);
                    int cs = a.Next(0, kernelSizes.Length - 1);
                    foodSources[i].x1 = filterCounts[fc];
                    foodSources[i].x2 = kernelSizes[cs];
                    foodSources[i].trial = 0;
                    foodSources[i].calcFitness();
                }
            }
        }

        private void btnBaslaBitir_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            bestS.Clear();
            cs = (int)npCs.Value;
            itercount = (int)npIterasyon.Value;
            init();
            while (chart1.Series.Count > 0) { chart1.Series.RemoveAt(0);chart1.Titles.Clear(); }
            while (chart2.Series.Count > 0) { chart2.Series.RemoveAt(0); chart2.Titles.Clear(); }
            // chart1.Series["Iter"].Points.Clear();
            var series1 = new Series("F(x)");
            var series2 = new Series("Fitness");
            chart1.Titles.Add(new Title("İterasyon- Fonksiyon"));
            chart2.Titles.Add(new Title("İterasyon-Fitness"));
            series1.ChartType = SeriesChartType.Line;
            series2.ChartType = SeriesChartType.Line;

            List<int> iterations = new List<int>();
            List<double> bestFitness = new List<double>();
            List<double> bestFx = new List<double>();
            for (int i = 0; i < bestS.Count; i++)
            {
                iterations.Add(i);
                bestFitness.Add(bestS[i].fitness);
                bestFx.Add(bestS[i].fx);
            }
            series1.Points.DataBindXY(iterations,bestFx );
            series2.Points.DataBindXY(iterations,bestFitness );
            chart1.Series.Add(series1);
            chart2.Series.Add(series2);

            FoodSource temp = bestS[0];
            for (int i = 0; i < bestS.Count; i++)
            {
                if (bestS[i].fitness<temp.fitness)
                {
                    temp = bestS[i];
                }

            }
            this.Text = "x1: " + temp.x1 + " x2: " + temp.x2;

            
        }
    }
    public class FoodSource
    {
        public int x1;
        public int x2;
        public double fx;
        public double fitness;
        public int trial;
        public FoodSource() { }
        public FoodSource(int x1, int x2) { this.x1 = x1; this.x2 = x2; }
        public FoodSource(int x1, int x2, int trial) { this.x1 = x1; this.x2 = x2; this.trial = trial; }
        public void calcFitness()
        {
            calcFx();
            this.fitness = 1 / (1 + fx);
        }
        public void calcFx()
        {
            this.fx = x1 + x2;
        }
        public void printFoodSource()
        {
            Console.WriteLine("x1: " + x1 + " x2: " + x2 + " " + " F(x )=" + fx + " Fitness: " + fitness + "Trial " + trial);
        }
        public String getAsString()
        {
           return ("x1: " + x1 + " x2: " + x2 + " " + " F(x )=" + fx + " Fitness: " + fitness + "Trial " + trial);
        }
    }
}
