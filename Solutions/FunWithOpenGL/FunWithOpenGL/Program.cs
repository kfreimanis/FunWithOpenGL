using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;

namespace FunWithOpenGL
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var type = typeof(IExample);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);

            IEnumerable<IExample> examples = types.Select(a => (IExample) Activator.CreateInstance(a));
           
            using (MainWindow game = new MainWindow(examples,true))
            {
                game.Run(30.0);
            }
        }
    }

}
