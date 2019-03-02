using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class StageStep
    {
        public bool IsStarted;
        public bool Completed;

        private Action stepAction;
        private Func<bool> checkCompleted;

        public StageStep(Action stepAction, Func<bool> checkCompleted)
        {
            this.stepAction = stepAction;
            this.checkCompleted = checkCompleted;
        }

        public bool IsInProgress
        {
            get
            {
                return IsStarted && !Completed;
            }
            set
            {
                IsStarted = true;
                Completed = false;
            }
        }

        public void Start()
        {
            IsStarted = true;
            stepAction();
        }

        public bool CheckCompleted()
        {
            var completed = checkCompleted();
            this.Completed = completed;
            return completed;
        }
    }
}
