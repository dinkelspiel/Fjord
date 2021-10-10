using System.Numerics;

namespace Fjord.Modules.Noise {
    public static class Noise {
        public static double[,] make_noise(int width, int height) {
            double[,] map = new double[width, height];
            Random rand = new Random();

            for(var i = 0; i < width; i++) {
                for(var j = 0; j < height; j++) {
                    map[i, j] = rand.NextDouble();
                }
            }

            return map;
        }

        public static double[,] make_perlin_noise(int width, int height) {
            double[,] map1 = new double[width, height];
            double[,] map2 = new double[width / 2, height / 2];
            double[,] map3 = new double[width / 4, height / 4];
            double[,] map4 = new double[width / 6, height / 6];
            double[,] map5 = new double[width / 10, height / 10];
            Random rand = new Random();

            for(var i = 0; i < width; i++) {
                for(var j = 0; j < height; j++) {
                    map1[i, j] = rand.NextDouble();
                }
            }
            
            for(var i = 0; i < width / 2; i++) {
                for(var j = 0; j < height / 2; j++) {
                    map2[i, j] = rand.NextDouble();
                }
            }

            for(var i = 0; i < width / 4; i++) {
                for(var j = 0; j < height / 4; j++) {
                    map3[i, j] = rand.NextDouble();
                }
            }

            for(var i = 0; i < width / 6; i++) {
                for(var j = 0; j < height / 6; j++) {
                    map4[i, j] = rand.NextDouble();
                }
            }

            for(var i = 0; i < width / 10; i++) {
                for(var j = 0; j < height / 10; j++) {
                    map5[i, j] = rand.NextDouble();
                }
            }

            double[,] map = new double[width, height];

            for(var i = 0; i < width; i++) {
                for(var j = 0; j < height; j++) {
                    double value1 = map1[i, j] * 0.6;
                    double value2 = map2[i / 2, j / 2] * 0.3;
                    double value3 = map3[i / 4, j / 4] * 0.15;
                    //double value4 = map4[(int)Math.Clamp(i / 6, 0, Math.Floor((double)i / 6)), (int)Math.Clamp(j / 6, 0, Math.Floor((double)j / 6))] * 0.08;
                    double value5 = map5[i / 10, j / 10] * 0.05;

                    map[i, j] = value1 + value2 + value3 + value5;
                }
            }

            return map;
        }
    }
}