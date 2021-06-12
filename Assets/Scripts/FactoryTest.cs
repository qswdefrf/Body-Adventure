using System;
using System.Linq;
using System.Reflection;
namespace Example {
    // Interface Model
    interface IModel {
        String GetData();
        void Print();
    }
    // Model1
    class Model1 : IModel {
        // protected를 함으로써 상속이 가능하나 외부에서는 할당(new)을 할 수 없다.
        protected Model1() {
        }
        public String GetData() {
            return "Model1";
        }
        public void Print() {
            Console.WriteLine("Thid class is Model1");
        }
    }
    // Model2
    class Model2 : IModel {
        // protected를 함으로써 상속이 가능하나 외부에서는 할당(new)을 할 수 없다.
        protected Model2() {
        }
        public String GetData() {
            return "Model2";
        }
        public void Print() {
            Console.WriteLine("Thid class is Model2");
        }
    }
    // 의존성 주입의 프로퍼티를 명시할 어트리뷰트
    [AttributeUsage(AttributeTargets.Property)]
    public class DI : Attribute {
        public string Name { get; set; }
        public DI(string name) {
            this.Name = name;
        }
    }
    // 의존성 주입 추상 클래스
    abstract class AbstractController {
        public AbstractController() {
            // 선언된 클래스 안의 프로퍼티 내에서 DI 어트리뷰트를 가지고 있는 프로퍼티를 찾는다.
            var properties = this.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.GetCustomAttribute(typeof(DI)) != null)
            .ToList();
            // 각 프로퍼티에 의존성 주입을 시작한다.
            foreach (var property in properties) {
                // 어트리뷰트의 DI값을 취득한다.
                var di = (DI)property.GetCustomAttributes(true).Where(x => x.GetType() == typeof(DI)).FirstOrDefault();
                Type type = Type.GetType(di.Name);
                // 어트리뷰트를 잘못 작성하면 에러난다.
                if (type == null) {
                    throw new NotSupportedException();
                }
                // 생성자가 protected이기 때문에 생성자를 가져와서 Invoke형식으로 할당한다.
                var constructor = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[0], null);
                var instance = constructor.Invoke(null);
                // 프로퍼티가 set 메서드를 가지고 있지 않기 때문에 프로퍼티의 변수를 찾는다.
                var field = this.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name.Contains("<" + property.Name + ">"))
                .First();
                // 변수에 할당한다.
                field.SetValue(this, instance);
            }
        }
    }
    class Controller : AbstractController {
        // 의존성 타입의 예제. (예제를 위하여 어트리뷰트를 이용했으나 config 파일로 선언해도 된다.)
        [DI("Example.Model1")]
        // 접근자는 private이고 set 메서드는 존재하지 않는다. 의존성 주입으로 클래스를 할당한다.
        private IModel Model { get; }
        public void Exec() {
            // Model의 값이 의존성 주입으로 Model1 클래스가 할당 되었기 때문에 Model1의 값이 Data를 취득한다.
            Console.WriteLine("Model data = " + Model.GetData());
            // Model1의 클래스의 Print가 실행된다.
            Model.Print();
        }
    }
    class Program {
        static void Main(string[] args) {
            // 클래스를 선언하고
            var controller = new Controller();
            // Exec의 함수를 실행한다.
            controller.Exec();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}

