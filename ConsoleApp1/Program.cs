using System;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            

            for (int i = 1; i <= 10; ++i)
            {
                for (int j = 0; j <= 10; ++j)
                {
                    //int len = 3;
                    //Proccesor proc = new Proccesor(3, i, j);
                    //proc.AddUser(0, 6, 8, 6, 3, 2, 4, 5, 3, 4, 5);
                    //proc.AddUser(0, 5, 3, 2, 1, 2, 3, 4, 5, 6, 8);
                    //proc.AddUser(0, 3, 4, 6, 8, 9, 5, 3, 2, 1, 2);
                    //proc.AddUser(1, 2, 3, 2, 4, 2, 1, 1, 6, 8, 9);
                    //proc.AddUser(1, 9, 3, 3, 4, 2, 1, 6, 7, 5, 4, 3);
                    //proc.AddUser(1, 3, 2, 3, 1, 4, 6, 8, 5, 3, 3, 3);
                    //proc.AddUser(2, 3, 4, 5, 2, 6, 2, 7, 8, 9, 3, 4);
                    //proc.AddUser(2, 2, 1, 2, 3, 3, 4, 5, 6, 3, 5, 4);

                    //int len = 3;
                    //Proccesor proc = new Proccesor(3, i, j);
                    //proc.AddUser(0, 3, 3, 4, 5, 6, 4, 2, 1, 5, 3, 4);
                    //proc.AddUser(1, 4, 2, 5, 6, 2, 1, 1, 3, 3, 4);
                    //proc.AddUser(1, 4, 4, 5, 6, 6, 2, 1, 1, 3, 3, 4, 4);
                    //proc.AddUser(2, 1, 1, 4, 4, 2, 2, 4, 3, 3, 3, 3, 5, 8);
                    //proc.AddUser(2, 4, 5, 1, 3, 3, 3, 2, 2, 4, 1, 4, 6);
                    //proc.AddUser(2, 4, 5, 1, 1, 1, 3, 3, 3, 2, 2, 4, 1, 1);



                    int len = 2;
                    Proccesor proc = new Proccesor(2, i, j);
                    proc.AddUser(0, 5, 3, 2, 1, 5, 2, 3, 2, 1, 6);
                    proc.AddUser(0, 6, 8, 3, 7, 9, 3, 3, 9, 8, 7);
                    proc.AddUser(0, 7, 6, 3, 2, 5, 3, 6, 8, 1, 3, 4);
                    proc.AddUser(1, 4, 3, 4, 2, 1, 1, 2, 2, 3, 4, 6);
                    proc.AddUser(1, 6, 8, 7, 3, 2, 1, 6, 3, 2, 1, 6);



                    //Proccesor proc = new Proccesor(len, i, j);
                    //proc.AddUser(0, 5, 3, 4);
                    //proc.AddUser(1, 2, 3, 3);
                    //proc.AddUser(1, 4, 2, 3);
                    int sum = 0;
                    for (int ii = 0; ii < len; ++ii)
                    {
                        for (int jj = 0; jj < proc[ii].Count; ++jj)
                        {
                            UserTimes user = proc[ii].Peek();
                            for (int k = 0; k < user.requests.Count; ++k) //доделать
                            {
                                sum += user.requests[k];
                            }
                            proc[ii].Enqueue(user);
                            proc[ii].Dequeue();
                        }

                    }

                    Console.WriteLine($"Tatc: {i}, IO: {j}");
                    (int ticks, int totalGaps) = proc.getConsumingTime();//посчиатть эффективное врем
                                                                         //посчитать gaps общее - эффективное
                    

                    Console.WriteLine("Efficience: {0:F2}, Gaps: {1:D}, All: {2:D}", (double)sum / (ticks * proc.Tact), ticks * proc.Tact - sum, (ticks * proc.Tact));
                    Console.WriteLine("-------------------------------");
                }
            }

            


            //Console.WriteLine(proc.getConsumingTime());
        }
    }
}
