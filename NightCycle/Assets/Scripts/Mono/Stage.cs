using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Stage
    {
        public bool IsStarted;
        public bool IsComplete;

        private List<StageStep> steps;
        private int currentStepIndex;

        public Stage(int stageIndex, List<StageStep> steps)
        {
            this.StageIndex = stageIndex;
            this.steps = steps;
        }

        public int StageIndex { get; }

        public bool IsInProgress
        {
            get
            {
                return IsStarted && !IsComplete;
            }
            set
            {
                IsStarted = true;
                IsComplete = false;
            }
        }

        public void Start()
        {
            IsStarted = true;
            Run();
        }

        public void Run()
        {
            if (currentStepIndex == steps.Count)
            {
                IsComplete = true;
                return;
            }

            var currentStep = steps[currentStepIndex];
            if (!currentStep.IsStarted)
            {
                currentStep.Start();
            }
            else if (currentStep.IsInProgress)
            {
                currentStep.CheckCompleted();
            }
            else
            {
                currentStepIndex++;
            }
        }
    }
}
