using System;
using TourismExpanded;
namespace Contracts {
    public class Contract {
        public enum DeadlineType { Floating, Fixed, None }
        protected DeadlineType deadlineType = DeadlineType.Floating;
        protected double dateDeadline = 123456;
        public double TimeDeadline = 21600;
        public double FundsCompletion = 654661;
        public string Passenger = "Asgan";
        public DeadlineType Kind => deadlineType;
        public double Deadline => dateDeadline;
        public void Set(DeadlineType kind, double duration, double absolute) { deadlineType=kind; TimeDeadline=duration; dateDeadline=absolute; }
    }
}
class Program {
 static void Check(bool ok,string name) { if(!ok)throw new Exception(name); Console.WriteLine("PASS "+name); }
 static void Main() {
  var c=new Contracts.Contract();
  Check(DeadlinePolicy.Apply(c,false)&&c.Kind==Contracts.Contract.DeadlineType.None&&c.TimeDeadline==0&&c.Deadline==0,"disabled clears cached six-hour offer and absolute deadline");
  Check(c.FundsCompletion==654661&&c.Passenger=="Asgan","rewards and passenger data preserved");
  Check(!DeadlinePolicy.Apply(c,false),"repeated load/accept is idempotent");
  c.Set(Contracts.Contract.DeadlineType.Floating,21600,123456);
  Check(!DeadlinePolicy.Apply(c,true)&&c.Kind==Contracts.Contract.DeadlineType.Floating&&c.TimeDeadline==21600&&c.Deadline==123456,"enabled leaves calculated deadlines unchanged");
  c.Set(Contracts.Contract.DeadlineType.None,0,123456);
  Check(DeadlinePolicy.Apply(c,false)&&c.Deadline==0,"clears residual absolute deadline despite None type");
 }
}