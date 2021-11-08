using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class UserTimes
    {
        public List<int> requests { get; set; }
        public int isIO { get; set; }
        public UserTimes(int[] times)
        {
            this.isIO = 0;
            requests = new List<int>();
            for (int i = 0; i < times.Length; ++i)
            {
                requests.Add(times[i]);
            }
        }
    }

    public class Proccesor
    {
        public int Tact { get; set; }
        public int Io { get; set; }
       
        private List<Queue<UserTimes>> _priorities;
        public Queue<UserTimes> this[int index]
        {
            get
            {
                return _priorities[index];
            }
        }

        public Proccesor(int level, int tact, int io)
        {
            if (tact < 1)
            {
                Console.WriteLine("Tact must be bigger than 1");
                return;
            }

            if (io < 0)
            {
                Console.WriteLine("Input/output time must be bigger or equal than 0");
                return;
            }

            _priorities = new List<Queue<UserTimes>>(level);
            this.Io = io;
            this.Tact = tact;
            for (int i = 0; i < level; ++i)
            {
                _priorities.Add(new Queue<UserTimes>());
            }
        }
        public void AddUser(int priority, params int[] times)
        {
            if (priority < 0 || priority > _priorities.Count)
            {
                //The better to pass the delegate here
                Console.WriteLine($"The priority must be positive and less than {_priorities.Count}");
                return;
            }

            _priorities[priority].Enqueue(new UserTimes(times));
        }

        private int decIo(int j)
        {
            int count = 0;

            for (int i = 0; i < _priorities[j].Count; ++i)
            {
                UserTimes user = _priorities[j].Peek();
                if (user.isIO > 0)
                {
                    user.isIO = user.isIO - this.Tact;
                    ++count;
                }
                else if (user.isIO < 0)
                {
                    user.isIO = 0;
                }
                //for (int k  =0; k < user.requests.Count; ++k)
                //{
                //    Console.WriteLine($"{user.requests[k]}");

                //}
                //Console.WriteLine("- - - - - - - -- - - - -- - - -");
                _priorities[j].Enqueue(user);
                _priorities[j].Dequeue();
            }

            return count;

        }
        public (int, int) getConsumingTime()
        {
            int gaps = 0, ticks = 0, totalGaps = 0;
            int i = 0;

            while (_priorities.Count != 0)
            {
                while (_priorities[i].Count != 0)
                {
                    int count = this.decIo(i);//The problem is here. Cause I need to reduce Io in every user
                    if (count == _priorities[i].Count && i != _priorities.Count - 1)
                    {
                        i = (i + 1) % _priorities.Count;
                        break;
                    }
                    UserTimes user = _priorities[i].Peek();
                    while (user.isIO != 0 && count != _priorities[i].Count)
                    {
                        _priorities[i].Enqueue(user);
                        _priorities[i].Dequeue();
                        user = _priorities[i].Peek();
                    }
                    if (user.requests.Count == 0)
                    {
                        _priorities[i].Dequeue();
                    }
                    else
                    {
                        if (user.isIO == 0 )//5|| (user.isIO < 0 && _priorities[i].Count > 1))
                        {
                            if (user.requests[0] - this.Tact > 0)
                            {
                                user.requests[0] -= this.Tact;

                            }
                            else
                            {
                              
                                gaps = user.requests[0] - this.Tact;
                                user.requests.RemoveAt(0);
                                if (gaps <= 0)
                                {
                                    user.isIO = this.Io + gaps;//можно убрать gaps - не нужно
                                   // Console.WriteLine($"Gap:{gaps}");
                                    totalGaps -= gaps;
                                }
                                
                                
                            }
                        }
                        else if (user.isIO < 0)
                        {
                            user.isIO = 0;
                        }

                        ticks++;
                        if (user.requests.Count == 0)
                        {
                            _priorities[i].Dequeue();
                        }
                        else
                        {
                            _priorities[i].Enqueue(user);
                            _priorities[i].Dequeue();
                        }

                        


                        for (i = i + 1; i < _priorities.Count; ++i)
                        {
                            this.decIo(i);
                        }
                        i = 0;
                        //Console.WriteLine($"___________________________{ticks}");
                        

                        
                    }

                }

                if (_priorities[i].Count == 0)
                {
                    _priorities.RemoveAt(i);
                }
            }

            return (ticks, totalGaps);
        }
    }
}
