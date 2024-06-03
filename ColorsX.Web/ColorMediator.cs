using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ColorsX.Web
{
    public class ColorMediator
    {
        private DateTime txnStartTime;
        private int txnCount; 
        private double txnLastSecondsElapsed;

        private int readyComponents = 0;
        private int totalComponents = 0;

        public event Action? OnStartLights;
        public void NotifyStartLights()
        {
            txnCount = 0;
            txnStartTime = DateTime.Now;
            txnLastSecondsElapsed = 0;
             
            Task.Run(() =>
            {
                OnStartLights?.Invoke();
            });
        }

        public event Action? OnStopLights;
        public void NotifyStopLights(int numberLights)
        {
            totalComponents = numberLights;
            readyComponents = 0;

            Task.Run(() =>
            {
                OnStopLights?.Invoke();
            });
        }

        public event Action? OnAllStopped;
        public void NotifyStopped()
        {
            if (totalComponents > 0)
            {
                readyComponents++;
                if (readyComponents == totalComponents)
                {
                    {
                        OnAllStopped?.Invoke();
                        totalComponents = 0;
                    }
                }
            }
        }

        public event EventHandler<TxnUpdateEventArgs>? OnTxnUpdate;
        public void NotifyBumpTxn()
        {
            txnCount++;
            double secondsElapsed = (DateTime.Now - txnStartTime).TotalSeconds;

            if (secondsElapsed - txnLastSecondsElapsed >= 5)
            {
                OnTxnUpdate?.Invoke(this, new TxnUpdateEventArgs { TxnCount = txnCount, SecondsElapsed = secondsElapsed });

                txnLastSecondsElapsed = secondsElapsed;
            }
        }

    }

    public class TxnUpdateEventArgs : EventArgs
    {
        public int TxnCount { get; set; }
        public double SecondsElapsed { get; set; }
    }

}
