namespace EmptyProjectRazorPagesLayout.Models
{
    public class Driver
    {
        public string Name { get; }
        public int Age { get; }

        public Driver() {
            Name = "";
            Age = 1;
        }

        public Driver(string name, int age) {
            Name = name;
            Age = age;
        }
        public override string ToString() => $"Driver {Name} ({Age} лет)";
    } // class Driver
}
