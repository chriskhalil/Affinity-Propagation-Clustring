using System;
using System.Collections.Generic;
using MachineLearning;
using MachineLearning.Clustering;
using System.IO;
namespace AffinityPropagationClustering
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            string config;
            Console.Write("Full File Path:");
           // path=;
            path=Path.GetFullPath(Console.ReadLine());
            Console.WriteLine(path);
            Console.WriteLine("File Modes:\n-s for similarity file\n-d for points file");
            Console.Write("Mode:");
            config=Console.ReadLine();
             if(config == "-s")
             {

                 try{
                 Console.WriteLine("Note: similarity file should not contain the main diagonal values");
                 List<Edge> data=new List<Edge>();
                 AffinityPropagation afp=new AffinityPropagation(Convert.ToInt32(Math.Ceiling(Math.Sqrt(data.Count))));
                 var items=afp.Cluster(data);
                 Print(items);
                  }
                  catch(Exception ex){
                      Console.WriteLine(ex.Message);
                  }

             }
             else if(config =="-d")
             {
                  //read Data from file
                  try{
                  List<Point> data=ReadCSV(path);
                  var sim_matrix=Similarity.SimilarityMatrix(data);
                  AffinityPropagation afp=new AffinityPropagation(data.Count);
                  var items=afp.Cluster(sim_matrix);
                  Print(items);
                  }
                  catch(Exception ex){
                      Console.WriteLine(ex.Message);
                  }
                  
            }
            else{
                Console.WriteLine("\aError:File Type Unkown");
            }


        }
        public static void Print(List<int> clusteredData){
          Console.WriteLine();
          foreach (var s in clusteredData)
          Console.Write($"{s} ");
          Console.WriteLine();
        }
        public static List<Point> ToyDataSet(){
            List<Point> data=new List<Point>();
            data.Add(new Point(-2.3415f,3.6968f));
            data.Add(new Point(-1.1092f,3.1117f));
            data.Add(new Point(-1.5669f,1.8351f));
            data.Add(new Point(-2.6585f,0.6649f));
            data.Add(new Point(-4.0317f,2.8457f));
            data.Add(new Point(-3.081f,2.1011f));
            data.Add(new Point(2.588f,1.7819f));
            data.Add(new Point(3.2923f,3.0585f));
            data.Add(new Point(4.0317f,1.6223f));
            data.Add(new Point(3.081f,-0.6117f));
            data.Add(new Point(0.2641f,0.3989f));
            data.Add(new Point(1.3204f,2.2074f));
            data.Add(new Point(0.1937f,3.6436f));
            data.Add(new Point(1.9542f,-0.5053f));
            data.Add(new Point(1.6373f,1.4096f));
            data.Add(new Point(-0.1232f,-1.516f));
            data.Add(new Point(-1.3556f,-3.0585f));
            data.Add(new Point(0.0176f,-4.016f));
            data.Add(new Point(1.0035f,-3.5904f));
            data.Add(new Point(0.0176f,-2.4202f));
            data.Add(new Point(-1.5317f,-0.9309f));
            data.Add(new Point(-1.1444f,0.5053f));
            data.Add(new Point(0.6162f,-1.516f));
            data.Add(new Point(1.7077f,-2.2074f));
            data.Add(new Point(2.0951f,3.4309f));
            return data;
        }

        public static List<Point>  ReadCSV(string path){
                List<Point> data= new List<Point>(20);
                float x=0,y=0;
                 using (var reader= new StreamReader(path)){

                     try{
                      while(!reader.EndOfStream){
                         string line =reader.ReadLine();
                         var col = line.Split(',');
                         x=float.Parse(col[0]);
                         y=float.Parse(col[1]);
                         data.Add(new Point(x,y));
                       }
                     }
                      catch(FormatException){
                         throw new Exception("0x02:Invalid input, make sure that your input is of the following format\n float,float,float");
                      }
                      catch(Exception){
                         throw new Exception("0x01:Unkown exception in csv module");
                      }
                    }
            return data;
        }
        public static List<Edge> ReadSim(string path){
            List<Edge> data=new List<Edge>(100);
            int i,j;
            float sim=0;
                 using (var reader= new StreamReader(path)){

                     try{
                     while(!reader.EndOfStream){
                         string line =reader.ReadLine();
                         var col = line.Split(',');
                         i=int.Parse(col[0]);
                         j=int.Parse(col[1]);
                         sim=float.Parse(col[2]);
                         data.Add(new Edge(i,j,sim));
                     }
                     }
                     catch(FormatException){
                         throw new Exception("0x02:Invalid input, make sure that your input is of the following format\n int,int,float");
                      }
                      catch(Exception){
                         throw new Exception("0x01:Unkown exception in Sim module");
                      }
                     return data;
                     }
        }
  
    }
}
