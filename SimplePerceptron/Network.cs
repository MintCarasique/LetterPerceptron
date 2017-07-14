using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePerceptron
{
    class Network
    {
        public int[,] weight;
        public int[,] mul;
        public int[,] input;
        public int sum;
        public int limit = 9;
        int xSize, ySize;
        public Network(int sizeX, int sizeY, int[,] inP)
        {
            weight = new int[sizeX, sizeY];
            mul = new int[sizeX, sizeY];
            this.xSize = sizeX;
            this.ySize = sizeY;
            input = new int[sizeX, sizeY];
            input = inP;
        }
        public void Sum()
        {
            sum = 0;
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    sum += mul[x, y];
                }
            }
        }
        public void mul_w()
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++) // Пробегаем по каждому аксону
                {
                    mul[x, y] = input[x, y] * weight[x, y]; // Умножаем его сигнал (0 или 1) на его собственный вес и сохраняем в массив.
                }
            }
        }
        public bool Res()
        {
            if (sum >= limit)
                return true;
            return false;
        }
        public void incW(int[,] inP)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    weight[x, y] += inP[x, y];
                }
            }
        }
        public void decW(int[,] inP)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    weight[x, y] -= inP[x, y];
                }
            }
        }
    }
}
