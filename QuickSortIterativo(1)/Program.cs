using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSortIterativo_1_
{
    internal class Program
    {

        public static int comparacoes = 0;
        public static int trocas = 0;
        static void Main(string[] args)
        {
            //define o tamanho de dados de entradas
            //int tamanhoVetor = 10;
            //int tamanhoVetor = 100;
            //int tamanhoVetor = 500;
            int tamanhoVetor = 1000;   
        
            int[] vetor = new int[tamanhoVetor];

            // Preenchimento do vetor com números aleatórios
            Random random = new Random();
            for (int i = 0; i < tamanhoVetor; i++)
            {
                vetor[i] = random.Next(100);
            }

            // Contagem do tempo de execução
            TimeSpan tempoDecorrido = MedirTempoDecorrido(() =>
            {
                Console.WriteLine("Vetor antes da ordenação:");
                ImprimirVetor(vetor);

                QuicksortIterativo(vetor);

                Console.WriteLine("Vetor após a ordenação:");
                ImprimirVetor(vetor);
                Console.WriteLine("O número de comparações  realizadas: " + comparacoes + " comparações");
                Console.WriteLine("O número de trocas realizadas: " + trocas + " trocas");
            });

            Console.WriteLine("Tempo de execução: " + tempoDecorrido.TotalMilliseconds.ToString("F2") + " milessegundos");
            Console.ReadKey();
        }

        public static TimeSpan MedirTempoDecorrido(Action acao)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            acao.Invoke();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        static void QuicksortIterativo(int[] vetor)
        {
            Stack<int> pilha = new Stack<int>();

            int inicio = 0;
            int fim = vetor.Length - 1;

            pilha.Push(inicio);
            pilha.Push(fim);

            while (pilha.Count > 0)
            {
                fim = pilha.Pop();
                inicio = pilha.Pop();

                int pivo = Particionar(vetor, inicio, fim);

                if (pivo - 1 > inicio)
                {
                    pilha.Push(inicio);
                    pilha.Push(pivo - 1);
                }

                if (pivo + 1 < fim)
                {
                    pilha.Push(pivo + 1);
                    pilha.Push(fim);
                }
            }
        }

        static int Particionar(int[] vetor, int inicio, int fim)
        {
            int pivo = vetor[fim];
            int i = inicio - 1;

            for (int j = inicio; j < fim; j++)
            {
                comparacoes++;
                if (vetor[j] < pivo)
                {
                    trocas ++; 
                    i++;
                    Trocar(vetor, i, j);
                }
            }

            Trocar(vetor, i + 1, fim);
            return i + 1;
        }

        static void Trocar(int[] vetor, int i, int j)
        {
            int temp = vetor[i];
            vetor[i] = vetor[j];
            vetor[j] = temp;
        }

        static void ImprimirVetor(int[] vetor)
        {
            for (int i = 0; i < vetor.Length; i++)
            {
                Console.Write(vetor[i] + " | ");
            }
            Console.WriteLine();
        }
    }
}