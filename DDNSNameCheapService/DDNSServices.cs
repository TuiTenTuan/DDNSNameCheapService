using System.ServiceProcess;
using System.Threading.Tasks;

namespace DDNSNameCheapService
{
    partial class DDNSServices : ServiceBase
    {
        public DDNSServices()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {

        }

        public Task MainTask()
        {
            return new Task(() => { });
        }
    }
}
