using Microsoft.Extensions.Hosting;
using MovingTanks.Models.Classes;
using MovingTanks.Models.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovingTanks.Services
{
    internal class TimedHostedService : IHostedService, IDisposable
    {
        private Timer _timer;

        public TimedHostedService()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        { 
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            for(double i = 0; i < State.Speed; i += 0.1)
            {
                if(!State.CheckPosition())
                {
                    for(int ti = 0; ti < State.Objects.Count; ti++)
                    {
                        if (State.IsObjectOutFromTheField(State.Objects[ti]))
                        {
                            State.TankIsOutFromField(State.Objects[ti] as ITank);
                        }

                        for(int tj = ti + 1; tj < State.Objects.Count; tj++)
                        {
                            if (State.AreObjectsInTouch(State.Objects[ti], State.Objects[tj]))
                            {
                                State.ObjectsAreInTouch(State.Objects[ti], State.Objects[tj]);
                            }
                        }
                    }

                    
                }

                foreach (var item in State.Objects)
                {
                    item.Move();
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
