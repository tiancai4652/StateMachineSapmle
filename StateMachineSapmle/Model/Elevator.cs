using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Machine;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachineSapmle.Model
{
    public class Elevator: BindableBase
    {
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { SetProperty(ref _Message, value); }
        }

        private readonly PassiveStateMachine<States, Events> elevator;

        private enum States
        {
            Healthy,
            OnFloor,
            Moving,
            MovingUp,
            MovingDown,
            DoorOpen,
            DoorClosed,
            Error
        }

        private enum Events
        {
            GoUp,
            GoDown,
            OpenDoor,
            CloseDoor,
            Stop,
            Error,
            Reset
        }

        public Elevator()
        {
            var builder = new StateMachineDefinitionBuilder<States, Events>();

            builder.DefineHierarchyOn(States.Healthy)
                .WithHistoryType(HistoryType.Deep)
                .WithInitialSubState(States.OnFloor)
                .WithSubState(States.Moving);

            builder.DefineHierarchyOn(States.Moving)
                .WithHistoryType(HistoryType.Shallow)
                .WithInitialSubState(States.MovingUp)
                .WithSubState(States.MovingDown);

            builder.DefineHierarchyOn(States.OnFloor)
                .WithHistoryType(HistoryType.None)
                .WithInitialSubState(States.DoorClosed)
                .WithSubState(States.DoorOpen);

            builder.In(States.Healthy)
                .On(Events.Error).Goto(States.Error);

            builder.In(States.Error)
                .On(Events.Reset).Goto(States.Healthy)
                .On(Events.Error);

            builder.In(States.OnFloor)
                .On(Events.CloseDoor).Goto(States.DoorClosed)
                .On(Events.OpenDoor).Goto(States.DoorOpen)
                .On(Events.GoUp).Goto(States.MovingUp)
                .On(Events.GoDown).Goto(States.MovingDown);

            builder.In(States.Moving)
                .On(Events.Stop).Goto(States.OnFloor);

            builder.In(States.Healthy).ExecuteOnEntry(
                () =>
                {
                    AddMessage("Entry States.Healthy");
                });

            builder.In(States.DoorClosed).ExecuteOnEntry(
                () =>
                {
                    AddMessage("Entry States.DoorClosed");
                });

            builder.In(States.DoorOpen).ExecuteOnEntry(
               () =>
               {
                   AddMessage("Entry States.DoorOpen");
               });

            builder.In(States.Error).ExecuteOnEntry(
             () =>
             {
                 AddMessage("Entry States.Error");
             });

            builder.In(States.Moving).ExecuteOnEntry(
           () =>
           {
               AddMessage("Entry States.Moving");
           });

            builder.In(States.MovingDown).ExecuteOnEntry(
         () =>
         {
             AddMessage("Entry States.MovingDown");
         });

            builder.In(States.MovingUp).ExecuteOnEntry(
       () =>
       {
           AddMessage("Entry States.MovingUp");
       });

            builder.In(States.OnFloor).ExecuteOnEntry(
     () =>
     {
         AddMessage("Entry States.OnFloor");
     });




            builder.WithInitialState(States.OnFloor);

            var definition = builder
                .Build();

            elevator = definition
                .CreatePassiveStateMachine("Elevator");

            elevator.Start();
        }

        void CheckHealthy()
        {
            
        }

        void AddMessage(string msg)
        {
            Message += msg;
            Message += Environment.NewLine;
        }

        public void GoToUpperLevel()
        {
            this.elevator.Fire(Events.CloseDoor);
            this.elevator.Fire(Events.GoUp);
            this.elevator.Fire(Events.OpenDoor);
        }

        public void GoToLowerLevel()
        {
            this.elevator.Fire(Events.CloseDoor);
            this.elevator.Fire(Events.GoDown);
            this.elevator.Fire(Events.OpenDoor);
        }

        public void Error()
        {
            this.elevator.Fire(Events.Error);
        }

        public void Stop()
        {
            this.elevator.Fire(Events.Stop);
        }

        public void Reset()
        {
            this.elevator.Fire(Events.Reset);
        }

        private void AnnounceFloor()
        {
            /* announce floor number */
        }

        private void AnnounceOverload()
        {
            /* announce overload */
        }

        private void Beep()
        {
            /* beep */
        }

        private bool CheckOverload()
        {
            return false;
        }
    }
}
